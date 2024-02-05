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
    public User_Info info;

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
        string serverinfo = Serverset(DB_path);
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
    //경로에 파일이 없다면 기본 Default 파일 생성 Method
    private void Default_Data(string path)
    {
        List<server_info> userInfo = new List<server_info>();

        // (testdb.cj4ki2qmepdi.ap-northeast-2.rds.amazonaws.com) = RDS 접속 IP
        userInfo.Add(new server_info("testdb.cj4ki2qmepdi.ap-northeast-2.rds.amazonaws.com", "PushPop", "root", "12345678", "3306"));

        JsonData data = JsonMapper.ToJson(userInfo);
        File.WriteAllText(path + "/config.json", data.ToString());
    }

    // SQL DATA를 불러오는 Method
    // 경로, 파일 탐색 후 Json파일 읽어와서 HeidiSQL 접속
    private string Serverset(string path)
    {
        if (!File.Exists(path)) // 경로 탐색
        {
            Directory.CreateDirectory(path);
        }

        if (!File.Exists(path + "/config.json"))  // 파일 탐색
        {
            Default_Data(path);
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
    private bool Connection_Check(MySqlConnection con)
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

    //Signup ID 중복체크 Method 
    private bool ID_Duplicate_Check(string ID)
    {
        try
        {
            string SQL_command = string.Format(@"SELECT User_ID FROM User_Info WHERE User_ID='{0}';", ID);
            MySqlCommand cmd = new MySqlCommand(SQL_command, connection);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                // 중복되는 ID가 있으면 true 반환
                if (reader.HasRows)
                {
                    return true;
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            // 예외 발생 시 처리
            // 이 경우, 회원가입 과정을 중단하게 됨
            return false;
        }
        // 중복되는 ID가 없으면 false 반환
        return false;
    }

    // Login 되어 있는지 체크 Method
    private bool Login_Check(string ID, int Connect)
    {
        try
        {
            string SQL_command = string.Format(@"SELECT User_ID, Connect FROM User_Info WHERE User_ID='{0}' AND Connect='{1}';", ID, Connect);
            MySqlCommand cmd = new MySqlCommand(SQL_command, connection);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                int connect = reader.GetInt32("Connect");
                if (connect == 1)
                {
                    // 접속 중인 상황
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            // 예외 발생 시 처리
            // 이 경우, 회원가입 과정을 중단하게 됨
            return false;
        }
    }

    /// <summary>
    ///  회원가입 메소드.
    ///  SQL 접속여부 확인, ID 중복체크 후에 SQL DB로 회원가입 시도
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="PW"></param>
    /// <returns></returns>
    public bool SQL_SignUp(string ID, string PW)
    {
        try
        {
            // 1. SQL 서버에 접속 되어 있는지 확인
            if (!Connection_Check(connection))
            {
                return false;
            }

            // 2. ID 중복 체크
            if (ID_Duplicate_Check(ID))
            {
                // 중복된 ID UI 출력
                Debug.Log("중복된 ID가 있습니다.");
                return false; // 중복된 ID가 있으므로 회원가입 실패
            }

            // 3. 신규 가입
            string SQL_command = string.Format(@"INSERT INTO User_Info (User_ID, User_PW) VALUES('{0}', '{1}');", ID, PW);
            MySqlCommand cmd = new MySqlCommand(SQL_command, connection);
            cmd.ExecuteNonQuery();

            return true; // 회원가입 성공
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return false;
        }
    }

    /// <summary>
    ///직접 DB에서 Data를 가지고 오는 Method
    ///조회가 되는 Data가 없다면 bool값 = false
    ///조회가 되는 Data가 있다면 info에 담아주고 bool값 = true
    /// </summary>
    public bool SQL_Login(string ID, string PW)
    {
        try
        {
            //1.connection open 상황인지 확인
            if (!Connection_Check(connection))
            {
                return false;
            }

            //2. ID 중복 접속 체크

            string SQL_command = string.Format(@"SELECT User_ID,User_PW, UID FROM User_Info WHERE User_ID='{0}' AND User_PW = '{1}' ;", ID, PW);
            MySqlCommand cmd = new MySqlCommand(SQL_command, connection);
            reader = cmd.ExecuteReader();

            //Reader 읽은 데이터가 1개 이상 존재하는가?
            if (reader.HasRows)
            {
                //읽은 데이터를 하나씩 나열함
                while (reader.Read())
                {
                    string id = reader.GetString("User_ID");
                    string pw = reader.GetString("User_PW");
                    int connect = reader.GetInt32("Connect");
                    if (!id.Equals(string.Empty) || !pw.Equals(string.Empty))
                    {
                        if(Login_Check(id, connect))
                        {
                            Debug.Log("접속중인 ID 입니다.");
                            return false;
                        }
                        else
                        {
                            //정상적으로 Data를 불러온 상황
                            UID = reader.GetInt32("UID");
                            info = new User_Info(ID, UID);
                            if (!reader.IsClosed) reader.Close();
                            return true;
                        }
                    }
                    else//로그인실패
                    {
                        Debug.Log("로그인 실패");
                        if (!reader.IsClosed) reader.Close();
                        break;
                    }
                }//while
            }//if
            Debug.Log("reader에 읽은 데이터가 없음");
            if (!reader.IsClosed) reader.Close();
            return false;
        }
        catch (Exception e)
        {
            if (!reader.IsClosed) reader.Close();
            Debug.Log(e.Message);
            return false;
        }
    }

    /// <summary>
    /// 프로필 생성 Method, name 중복 체크는 따로 하지 않음 (동명이인 고려)
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool SQL_Add_Profile(string name)
    {
        try
        {
            // 1. SQL 서버에 접속 되어 있는지 확인
            if (!Connection_Check(connection))
            {
                return false;
            }

            // 2. 프로필 생성
            string SQL_command = string.Format(@"INSERT INTO Profile (UID, User_name) VALUES('{0}', '{1}');", info.UID, name);
            MySqlCommand cmd = new MySqlCommand(SQL_command, connection);
            cmd.ExecuteNonQuery();

            return true; // 회원가입 성공
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return false;
        }
    }

    /// <summary>
    /// 로그인 후 UID에 조회되는 프로필 목록 출력하는 Method
    /// </summary>
    public void SQL_Profile_ListSet()
    {
        try
        {
            // 데이터베이스 연결 확인
            if (!Connection_Check(connection))
            {
                Debug.Log("데이터베이스 연결 실패");
                return;
            }

            // UID에 연결된 프로필 조회 쿼리 실행
            string SQL_command = string.Format(@"SELECT User_name, Profile_Index FROM Profile WHERE UID = '{0}';", info.UID);
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
    /// Ranking Table에 Score, Timer를 넘겨주는 Method.
    /// score를 보내줘야 할때는 Timer를 null로 설정하고, timer를 보내줘야 할때는 score를 null로 설정해서 사용
    /// </summary>
    public void SQL_Set_Score(string profile_name, int profile_index, int? newScore, float? newTimer, int UID)
    {
        try
        {
            // 1. SQL 서버에 접속 되어 있는지 확인
            if (!Connection_Check(connection))
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
    public void SQL_Print_Ranking()
    {
        try
        {
            // 1. SQL 서버에 접속 되어 있는지 확인
            if (!Connection_Check(connection))
            {
                return;
            }

            string SQL_command = string.Format(@"SELECT Profile_Name, Profile_Index, Score, Timer FROM Ranking WHERE UID = '{0}';", info.UID);
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
}