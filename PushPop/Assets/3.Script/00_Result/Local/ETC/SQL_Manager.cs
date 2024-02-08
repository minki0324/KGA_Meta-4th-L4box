using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using LitJson;
using System.Globalization;

#region Other Class
/// <summary>
/// ID, PW로 접속하는 User_Info
/// </summary>
public class User_Info
{
    public string User_ID { get; private set; }
    public int UID { get; private set; }

    public User_Info(string id, int uid)
    {
        User_ID = id;
        UID = uid;
    }
}

/// <summary>
/// UID에 속해있는 Profile
/// </summary>
public class Profile
{
    public string name { get; private set; }
    public int index { get; private set; }

    public Profile(string name, int index)
    {
        this.name = name;
        this.index = index;
    }
}

public class Rank
{
    public string name { get; private set; }
    public int index { get; private set; }
    public int score { get; private set; }
    public float timer { get; private set; }

    public Rank(string name, int index, int score, float timer)
    {
        this.name = name;
        this.index = index;
        this.score = score;
        this.timer = timer;
    }
}

// 서버 접속 Class
public class server_info
{
    public string IP { get; private set; }
    public string TableName { get; private set; }
    public string ID { get; private set; }
    public string PW { get; private set; }
    public string PORT { get; private set; }

    public server_info(string ip, string tableName, string id, string pw, string port)
    {
        IP = ip;
        TableName = tableName;
        ID = id;
        PW = pw;
        PORT = port;
    }
}
#endregion

/// <summary>
/// SQL 관련 Query문 처리 Class
/// </summary>
public class SQL_Manager : MonoBehaviour
{
    public static SQL_Manager instance = null;
    public User_Info Info;

    public MySqlConnection connection;
    public MySqlDataReader reader;
    

    public string DB_path = string.Empty;   // Json경로 (DB)
    public int UID;
    public List<Profile> Profile_list = new List<Profile>();
    public List<Rank> Rank_List = new List<Rank>();

    #region Unity Callback
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DB_path = Application.persistentDataPath + "/Database";
        string serverinfo = ServerSet(DB_path);
        try
        {
            // serverinfo에 받아온 데이터가 없다면 오류
            if (serverinfo.Equals(string.Empty))
            {
                Debug.Log("SQL Server Json Error!");
                return;
            }

            // if문에서 오류가 없이 지나왔다면 SQL 열어주기
            connection = new MySqlConnection(serverinfo);
            connection.Open();
            Debug.Log("SQL Server Open Compelate!");
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    private void OnApplicationQuit()
    {
        
    }
    #endregion

    #region Other Method
    #region ConnectServer
    //경로에 파일이 없다면 기본 Default 파일 생성 Method
    private void DefaultData(string path)
    {
        List<server_info> userInfo = new List<server_info>();

        // (testdb.cj4ki2qmepdi.ap-northeast-2.rds.amazonaws.com) = RDS 접속 IP
        userInfo.Add(new server_info("testdb.cj4ki2qmepdi.ap-northeast-2.rds.amazonaws.com", "PushPop", "root", "12345678", "3306"));

        JsonData data = JsonMapper.ToJson(userInfo);
        File.WriteAllText(path + "/config.json", data.ToString());
    }

    // SQL DATA를 불러오는 Method
    // 경로, 파일 탐색 후 Json파일 읽어와서 HeidiSQL 접속
    private string ServerSet(string path)
    {
        if (!File.Exists(path)) // 경로 탐색
        {
            Directory.CreateDirectory(path);
        }

        if (!File.Exists(path + "/config.json"))  // 파일 탐색
        {
            DefaultData(path);
        }
        string Jsonstring = File.ReadAllText(path + "/config.json");

        JsonData itemdata = JsonMapper.ToObject(Jsonstring);
        string serverInfo =
            $"server = {itemdata[0]["IP"]};" + $"" +
            $" Database = {itemdata[0]["TableName"]};" +
            $" Uid = {itemdata[0]["ID"]};" +
            $" Pwd = {itemdata[0]["PW"]};" +
            $" Port = {itemdata[0]["PORT"]};" +
            $" CharSet=utf8;";

        return serverInfo;
    }

    //SQL이 열려 있는지 확인하는 Method 
    private bool ConnectionCheck(MySqlConnection con)
    {
        //현재 MySQLConnection open 이 아니라면?
        if (con.State != System.Data.ConnectionState.Open)
        {
            con.Open();
            if (con.State != System.Data.ConnectionState.Open)
            {
                return false;
            }
        }
        return true;
    }
    #endregion

    #region Profile
    // GUID 중복체크 Method
    private bool GUID_DuplicateCheck(string GUID)
    {
        string sql_cmd = string.Format(@"SELECT GUID FROM User_Info WHERE GUID='{0}';", GUID);
        MySqlCommand cmd_ = new MySqlCommand(sql_cmd, connection);
        reader = cmd_.ExecuteReader();

        if(reader.HasRows)
        {
            if (!reader.IsClosed) reader.Close();
            return true;
        }
        else
        {
            if (!reader.IsClosed) reader.Close();
            return false;
        }
    }

    /// <summary>
    /// 접속시 GUID를 확인하여 첫 접속이면 GUID를 부여해 로그인하고, 기존 GUID가 있다면 해당 GUID를 통해 로그인 하는 Method
    /// </summary>
    /// <param name="GUID"></param>
    public void SQL_AddUser(string GUID)
    {
        try
        {
            // 1. SQL 서버에 접속 되어 있는지 확인
            if (!ConnectionCheck(connection))
            {
                return;
            }

            // 이미 생성된 GUID가 있는지 확인
            if (!GUID_DuplicateCheck(GUID))
            {
                // 2. 기기 UID 생성
                string SQL_command = string.Format(@"INSERT INTO User_Info (GUID) VALUES('{0}')
                                                                                    ON DUPLICATE KEY UPDATE GUID = VALUES(GUID);", GUID);
                MySqlCommand cmd = new MySqlCommand(SQL_command, connection);
                cmd.ExecuteNonQuery();
            }

            // 3. UID 정보 받아오기
            string sql_cmd = string.Format(@"SELECT UID FROM User_Info WHERE GUID = '{0}';", GUID);
            MySqlCommand cmd_ = new MySqlCommand(sql_cmd, connection);
            reader = cmd_.ExecuteReader();
      
            if (reader.HasRows)
            {
                // 4. UID를 클래스의 멤버 변수에 할당
                reader.Read(); 
                UID = reader.GetInt32("UID");
                GameManager.instance.UID = UID;
                Info = new User_Info(GUID, UID);
            }
            if (!reader.IsClosed) reader.Close();
            return; // 회원가입 성공
        }
        catch (Exception e)
        {
            if (!reader.IsClosed) reader.Close();
            Debug.Log(e.Message);
            return;
        }
    }

    /// <summary>
    /// 프로필 생성 Method, name 중복 체크는 따로 하지 않음 (동명이인 고려)
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool SQL_AddProfile(string name)
    {
        try
        {
            // 1. SQL 서버에 접속 되어 있는지 확인
            if (!ConnectionCheck(connection))
            {
                return false;
            }

            // 2. 프로필 생성
            string SQL_command = string.Format(@"INSERT INTO Profile (UID, User_name) VALUES('{0}', '{1}');", Info.UID, name);
            MySqlCommand cmd = new MySqlCommand(SQL_command, connection);
            cmd.ExecuteNonQuery();

            return true; // 프로필 생성 성공
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return false;
        }
    }

    /// <summary>
    /// 프로필 삭제 Method, UID와 name을 확인해서 삭제
    /// </summary>
    /// <param name="name"></param>
    public void SQL_DeleteProfile(string name, int index)
    {
        try
        {
            // 1. SQL 서버에 접속 되어 있는지 확인
            if (!ConnectionCheck(connection))
            {
                return;
            }

            // 2. 프로필 삭제
            string SQL_command = string.Format(@"DELETE FROM Profile WHERE UID = '{0}' AND User_name = '{1}';", Info.UID, name);
            MySqlCommand cmd = new MySqlCommand(SQL_command, connection);
            cmd.ExecuteNonQuery();

            // 3. 이미지 삭제
            string sql_cmd = string.Format(@"DELETE FROM Image WHERE UID = '{0}' AND Profile_Index = '{1}';", Info.UID, index);
            MySqlCommand cmd_ = new MySqlCommand(sql_cmd, connection);
            cmd_.ExecuteNonQuery();

            // 삭제 성공
            Debug.Log("프로필 삭제 성공");
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    /// <summary>
    /// 로그인 후 UID에 조회되는 프로필 목록 출력하는 Method
    /// </summary>
    public void SQL_ProfileListSet()
    {
        try
        {
            // 데이터베이스 연결 확인
            if (!ConnectionCheck(connection))
            {
                Debug.Log("데이터베이스 연결 실패");
                return;
            }

            Profile_list.Clear();
            // UID에 연결된 프로필 조회 쿼리 실행
            string SQL_command = string.Format(@"SELECT User_name, Profile_Index FROM Profile WHERE UID = '{0}';", Info.UID);
            MySqlCommand cmd = new MySqlCommand(SQL_command, connection);
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string profileName = reader.GetString("User_name");
                    int profileIndex = reader.GetInt32("Profile_Index");

                    // 넘겨줄 리스트 Add해주기
                    Profile_list.Add(new Profile(profileName, profileIndex));
                }
                if (!reader.IsClosed) reader.Close();
                return;
            }
            else
            {
                Debug.Log("등록된 프로필이 없습니다.");
                if (!reader.IsClosed) reader.Close();
                return;
            }
        }
        catch (Exception e)
        {
            Debug.Log($"프로필 조회 중 오류 발생: {e.Message}");
            if (!reader.IsClosed) reader.Close();
            return;
        }
    }

    /// <summary>
    /// UID, Index를 매개변수로 전달하여 Image를 Binary값으로 DB에 저장하는 Method
    /// </summary>
    /// <param name="filename"></param>
    /// <param name="uid"></param>
    /// <param name="index"></param>
    public void SQL_AddProfileImage(string filename, int uid, int index)
    {
        FileStream fileStream;
        BinaryReader binaryReader;

        try
        {
            // 1. 데이터베이스 연결 확인
            if (!ConnectionCheck(connection))
            {
                Debug.Log("데이터베이스 연결 실패");
                return;
            }

            byte[] ImageData;
            fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            binaryReader = new BinaryReader(fileStream);

            ImageData = binaryReader.ReadBytes((int)fileStream.Length);

            fileStream.Close();
            binaryReader.Close();

            string SQL_command = "INSERT INTO Image (UID, Profile_Index, ImageData) VALUES (@UID, @Index, @ImageData)";
            MySqlCommand cmd = new MySqlCommand(SQL_command, connection);

            // 파라미터 추가
            cmd.Parameters.AddWithValue("@UID", uid);
            cmd.Parameters.AddWithValue("@Index", index);
            cmd.Parameters.AddWithValue("@ImageData", ImageData);

            cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Debug.Log($"프로필 조회 중 오류 발생: {e.Message}");
            if (!reader.IsClosed) reader.Close();
            return;
        }
    }

    /// <summary>
    /// Profile_index에 따른 프로필 이미지를 출력하는 Method
    /// </summary>
    /// <param name="uid"></param>
    /// <param name="profileIndex"></param>
    /// <returns></returns>
    public Texture2D SQL_LoadProfileImage(int uid, int profileIndex)
    {
        string SQL_command = "SELECT ImageData FROM Image WHERE UID = @UID AND Profile_Index = @ProfileIndex";
        MySqlCommand cmd = new MySqlCommand(SQL_command, connection);

        // 파라미터 추가
        cmd.Parameters.AddWithValue("@UID", uid);
        cmd.Parameters.AddWithValue("@ProfileIndex", profileIndex);

        // 이미지 데이터를 읽기 위해 ExecuteScalar() 메서드를 사용합니다.
        byte[] imageData = (byte[])cmd.ExecuteScalar();

        // ImageData를 Texture2D로 변환
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(imageData); // 바이트 배열로부터 텍스처를 로드합니다.

        return texture;
    }

    /// <summary>
    /// 프로필 업데이트 Method, UID와 name을 확인해서 새로운 name과 image로 update
    /// </summary>
    public void SQL_UpdateProfile(int previousIndex, string previousname, string name, int uid, string filename)
    {
        FileStream fileStream;
        BinaryReader binaryReader;
        try
        {
            // 1. SQL 서버에 접속 되어 있는지 확인
            if (!ConnectionCheck(connection))
            {
                return;
            }

            // 2. 기존 name찾아서 name 변경
            string sql_cmd = string.Format(@"UPDATE Profile SET (UID, User_name) VALUES('{0}', '{1}');", Info.UID, name);
            MySqlCommand cmd = new MySqlCommand(sql_cmd, connection);
            cmd.ExecuteNonQuery();

            // 3. 이미지 변경
            byte[] ImageData;
            fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            binaryReader = new BinaryReader(fileStream);

            ImageData = binaryReader.ReadBytes((int)fileStream.Length);

            fileStream.Close();
            binaryReader.Close();

            string SQL_command = @$"UPDATE Image SET ImageData = '{ImageData}' WHERE UID = '{uid}' AND Profile_Index = '{previousIndex}';";
            MySqlCommand cmd_ = new MySqlCommand(SQL_command, connection);

            // 파라미터 추가
            cmd_.Parameters.AddWithValue("@UID", uid);
            cmd_.Parameters.AddWithValue("@Index", previousIndex);
            cmd_.Parameters.AddWithValue("@ImageData", ImageData);

            cmd_.ExecuteNonQuery();

            return; // 업데이트 성공
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return;
        }
    }

    
    #endregion

    #region Rank
    /// <summary>
    /// Ranking Table에 Score, Timer를 넘겨주는 Method.
    /// score를 보내줘야 할때는 Timer를 null로 설정하고, timer를 보내줘야 할때는 score를 null로 설정해서 사용
    /// </summary>
    public void SQL_SetScore(string profile_name, int profile_index, int? newScore, float? newTimer, int UID)
    {
        try
        {
            // 1. SQL 서버에 접속 되어 있는지 확인
            if (!ConnectionCheck(connection))
            {
                return;
            }

            // 2. 기존 기록 조회
            string selectCommand = string.Format(@"SELECT Score, Timer FROM Ranking WHERE Profile_Name = '{0}' AND Profile_Index = {1} AND UID = {2};", profile_name, profile_index, UID);
            MySqlCommand selectCmd = new MySqlCommand(selectCommand, connection);

            bool hasExistingRecord = false;
            int existingScore = 0;
            float existingTimer = 0;

            using (reader = selectCmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    hasExistingRecord = true;
                    existingScore = reader.IsDBNull(reader.GetOrdinal("Score")) ? 0 : reader.GetInt32("Score");
                    existingTimer = reader.IsDBNull(reader.GetOrdinal("Timer")) ? 0 : reader.GetFloat("Timer");
                    if (!reader.IsClosed) reader.Close();
                }
            }

            // 3. 기록 갱신 또는 삽입
            if (hasExistingRecord)
            {
                // 업데이트 조건 검사: 새로운 점수가 기존 점수보다 크거나, 새로운 타이머 값이 기존 값보다 작은 경우에만 업데이트
                if (!reader.IsClosed) reader.Close();
                bool shouldUpdateScore = newScore.HasValue && newScore.Value > existingScore;
                bool shouldUpdateTime = newTimer.HasValue && (newTimer.Value < existingTimer || existingTimer == 0); // 기존 타이머가 0이면 항상 업데이트

                if (shouldUpdateScore || shouldUpdateTime)
                {
                    // 업데이트 대상 점수와 타이머 결정
                    int scoreToUpdate = shouldUpdateScore ? newScore.Value : existingScore;
                    float timerToUpdate = shouldUpdateTime ? newTimer.Value : existingTimer;

                    string updateCommand = string.Format(
                        @"UPDATE Ranking SET Score = {0}, Timer = {1} WHERE Profile_Name = '{2}' AND Profile_Index = {3} AND UID = {4};",
                        scoreToUpdate, // 업데이트할 새로운 Score 또는 기존 Score 유지
                        string.Format(CultureInfo.InvariantCulture, "{0:0.###}", timerToUpdate), // 업데이트할 새로운 Timer 또는 기존 Timer 유지
                        profile_name, profile_index, UID);

                    new MySqlCommand(updateCommand, connection).ExecuteNonQuery();
                }
            }
            // 4. 기존 기록이 없으면, 새로운 기록 삽입
            else
            {
                if (!reader.IsClosed) reader.Close();
                string insertCommand = string.Format(
                    @"INSERT INTO Ranking (Profile_Name, Profile_Index, UID, Score, Timer) VALUES('{0}', {1}, {2}, {3}, {4});",
                    profile_name, profile_index, UID,
                    newScore.HasValue ? newScore.Value.ToString() : "0", // newScore가 null이면 0을 기본값으로 사용
                    newTimer.HasValue ? string.Format(CultureInfo.InvariantCulture, "{0:0.###}", newTimer.Value) : "0"); // newTimer가 null이면 0을 기본값으로 사용

                new MySqlCommand(insertCommand, connection).ExecuteNonQuery();
            }
            if (!reader.IsClosed) reader.Close();
            return;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            if (!reader.IsClosed) reader.Close();
            return;
        }
    }

    /// <summary>
    /// 호출시 같은 UID상에 존재하는 Profile들의 랭킹을 List에 담는 Method
    /// </summary>
    /// <param name="Mode"></param>
    public void SQL_PrintRanking()
    {
        try
        {
            // 1. SQL 서버에 접속 되어 있는지 확인
            if (!ConnectionCheck(connection))
            {
                return;
            }

            string SQL_command = string.Format(@"SELECT Profile_Name, Profile_Index, Score, Timer FROM Ranking WHERE UID = '{0}';", Info.UID);
            MySqlCommand cmd = new MySqlCommand(SQL_command, connection);
            reader = cmd.ExecuteReader();

            if(reader.HasRows)
            {
                // 2. 랭킹 리스트 담기 전 초기화
                Rank_List.Clear();

                // 3. 읽어온 정보에서 같은 UID에 속해 있는 Profile들의 랭킹 정보를 불러서 List에 담기
                while (reader.Read())
                {
                    string profile_name = reader.GetString("Profile_Name");
                    int profile_index = reader.GetInt32("Profile_Index");
                    int score = reader.GetInt32("Score");
                    float timer = reader.GetFloat("Timer");

                    Rank_List.Add(new Rank(profile_name, profile_index, score, timer));
                }
                if (!reader.IsClosed) reader.Close();
                return;
            }
            else
            {
                Debug.Log("프로필에 등록된 Rank가 없습니다.");
                if (!reader.IsClosed) reader.Close();
                return;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            if (!reader.IsClosed) reader.Close();
            return;
        }
    }
    #endregion
    #endregion
}