using LitJson;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

#region Other Class
/// <summary>
/// UID에 속해있는 Profile
/// </summary>
public class Profile
{
    public string name { get; private set; }
    public int index { get; private set; }
    public bool imageMode { get; private set; }
    public int defaultImage { get; private set; }

    public Profile(string name, int index, int mode, int _defaultImage)
    {
        this.name = name;
        this.index = index;
        if (mode == 0)
        { // 사진 찍은 사람
            imageMode = false;
        }
        else if (mode == 1)
        { // default image 선택한 사람
            imageMode = true;
        }
        defaultImage = _defaultImage;
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

public class FavoriteList
{
    public FavoriteList(List<int> friendIndex)
    {
        FriendIndex = friendIndex;
    }

    public List<int> FriendIndex;
}

public class HeartAndLike
{
    public List<int> PressHeartProfileIndex;
    public List<int> PressLikeProfileIndex;

    public HeartAndLike(List<int> pressHeartProfileIndex, List<int> pressLikeProfileIndex)
    {
        PressHeartProfileIndex = pressHeartProfileIndex;
        PressLikeProfileIndex = pressLikeProfileIndex;
    }
}
#endregion

/// <summary>
/// SQL 관련 Query문 처리 Class
/// </summary>
public class SQL_Manager : MonoBehaviour
{
    public static SQL_Manager instance = null;

    private MySqlConnection connection;
    private MySqlDataReader reader;

    private string DB_path = string.Empty;   // Json Path (DB)
    public int UID;                         
    public List<Profile> ProfileList = new List<Profile>();
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
        Debug.Log("SQL Awake : " + serverinfo);

        // serverinfo에 받아온 데이터가 없다면 오류
        if (serverinfo.Equals(string.Empty))
        {
            return;
        }

        // if문에서 오류가 없이 지나왔다면 SQL 열어주기
        connection = new MySqlConnection(serverinfo);
        connection.Open(); // 시도: 데이터베이스 연결
    }
    #endregion

    #region Other Method
    #region ConnectServer
    //경로에 파일이 없다면 기본 Default 파일 생성 Method
    private void DefaultData(string path)
    {
        try
        {
            List<server_info> userInfo = new List<server_info>();

            // (database-1.cj4ki2qmepdi.ap-northeast-2.rds.amazonaws.com) = RDS 접속 IP
            userInfo.Add(new server_info("database-1.cj4ki2qmepdi.ap-northeast-2.rds.amazonaws.com", "PushPop", "PushPopDB", "ruddlf4wh", "7958"));

            JsonData data = JsonMapper.ToJson(userInfo);
            File.WriteAllText(path + "/config.json", data.ToString());
        }
        catch(Exception e)
        {
            Debug.Log("DefaultData : " + e.Message);
        }
    }

    // SQL DATA를 불러오는 Method
    // 경로, 파일 탐색 후 Json파일 읽어와서 HeidiSQL 접속
    private string ServerSet(string path)
    {
        try
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
                $" CharSet = utf8;";

            return serverInfo;
        }
        catch(Exception e)
        {
            Debug.Log("ServerSet : " + e.Message);
            return null;
        }
    }

    //SQL이 열려 있는지 확인하는 Method 
    private bool ConnectionCheck(MySqlConnection con)
    {
        try
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
        catch(Exception e)
        {
            Debug.Log("ConnectionCheck : " + e.Message);
            return false;
        }
    }
    #endregion

    #region Profile
    // GUID 중복체크 Method
    private bool GUID_DuplicateCheck(string GUID)
    {
        try
        {
            string sql_cmd = string.Format(@"SELECT GUID FROM User_Info WHERE GUID='{0}';", GUID);
            MySqlCommand cmd_ = new MySqlCommand(sql_cmd, connection);
            reader = cmd_.ExecuteReader();

            if (reader.HasRows)
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
        catch(Exception e)
        {
            Debug.Log("GUID_DuplicateCheck : " + e.Message);
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
                ProfileManager.Instance.UID = UID;                
            }
            if (!reader.IsClosed) reader.Close();
            return; // 회원가입 성공
        }
        catch (Exception e)
        {
            if (!reader.IsClosed) reader.Close();
            Debug.Log("SQL AddUser : " + e.Message);
            return;
        }
    }

    /// <summary>
    /// 프로필 생성 Method, name 중복 체크는 따로 하지 않음 (동명이인 고려)
    /// </summary>
    /// <param name="_profileName"></param>
    /// <returns></returns>
    public int SQL_AddProfile(string _profileName, int _imageMode)
    {
        try
        {
            if (!ConnectionCheck(connection))
            { // server connection check
                return -1;
            }

            // profile Insert into
            string SQL_command = string.Format(@"INSERT INTO Profile (UID, User_name, ImageMode) VALUES('{0}', '{1}', '{2}'); SELECT LAST_INSERT_ID();", UID, _profileName, _imageMode);
            MySqlCommand cmd = new MySqlCommand(SQL_command, connection);
            int profileIndex = Convert.ToInt32(cmd.ExecuteScalar());

            if (profileIndex > 0)
            {
                string favorite_cmd = string.Format(@"INSERT INTO Favorite (UID, Profile_Index, FavoriteList) VALUES('{0}', '{1}', '{2}');", UID, profileIndex, null);
                MySqlCommand favoritecmd_ = new MySqlCommand(favorite_cmd, connection);
                favoritecmd_.ExecuteNonQuery();
                return profileIndex; // 새로 생성된 프로필의 primary key 반환
            }
            else
            {
                return -1; // 프로필 생성 실패를 나타내는 값 반환
            }
        }
        catch (Exception e)
        {
            Debug.Log("SQL AddProfile : " + e.Message);
            return -1;
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public void SQL_UpdateMode(int imageMode, int uid, int profileIndex)
    {
        try
        {
            // 1. SQL 서버에 접속 되어 있는지 확인
            if (!ConnectionCheck(connection))
            {
                return;
            }

            // 2. 프로필 이미지 모드 변경
            string name_command = string.Format(@"UPDATE Profile SET ImageMode = '{0}' WHERE UID = '{1}' AND Profile_Index = '{2}';", imageMode, uid, profileIndex);
            MySqlCommand cmd = new MySqlCommand(name_command, connection);
            cmd.ExecuteNonQuery();

            return; // 프로필 생성 실패를 나타내는 값 반환
        }
        catch (Exception e)
        {
            Debug.Log("SQL UpdateMode : " + e.Message);
            return;
        }
    }

    /// <summary>
    /// 프로필 삭제 Method, UID와 name을 확인해서 삭제
    /// </summary>
    /// <param name="name"></param>
    public void SQL_DeleteProfile(int index)
    {
        try
        {
            // 1. SQL 서버에 접속 되어 있는지 확인
            if (!ConnectionCheck(connection))
            {
                return;
            }

            // 2. 프로필 삭제
            string profile_command = string.Format(@"DELETE FROM Profile WHERE UID = '{0}' AND Profile_Index = '{1}';", UID, index);
            MySqlCommand profile_cmd = new MySqlCommand(profile_command, connection);
            profile_cmd.ExecuteNonQuery();

            // 3. 이미지 삭제
            string image_command = string.Format(@"DELETE FROM Image WHERE UID = '{0}' AND Profile_Index = '{1}';", UID, index);
            MySqlCommand image_cmd = new MySqlCommand(image_command, connection);
            image_cmd.ExecuteNonQuery();

            // 4. PushPush 삭제
            string pushpush_command = string.Format(@"DELETE FROM PushPush WHERE ProfileIndex = '{0}';",  index);
            MySqlCommand pushpush_cmd = new MySqlCommand(pushpush_command, connection);
            pushpush_cmd.ExecuteNonQuery();

            // 5. Favorite 삭제
            string favorite_command = string.Format(@"DELETE FROM Favorite WHERE Profile_Index = '{0}';", index);
            MySqlCommand favorite_cmd = new MySqlCommand(favorite_command, connection);
            favorite_cmd.ExecuteNonQuery();

            // 6. Ranking 삭제
            Ranking.Instance.DeleteRankAndVersus(index);

            // 7. List 초기화
            for(int i = 0; i < ProfileList.Count; i++)
            {
                if(index == ProfileList[i].index)
                {
                    Profile tempProfile = ProfileList[i];
                    ProfileList.Remove(tempProfile);
                }
            }

            // 삭제 성공
            Debug.Log("프로필 삭제 성공");
        }
        catch (Exception e)
        {
            Debug.Log("SQL DeleteProfile : " + e.Message);
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

            ProfileList.Clear();
            // UID에 연결된 프로필 조회 쿼리 실행
            string SQL_command = string.Format(@"SELECT DISTINCT Profile.User_name, Profile.Profile_Index, Profile.ImageMode, Image.DefaultIndex 
                                                                                          FROM Profile 
                                                                                          INNER JOIN Image ON Profile.Profile_Index = Image.Profile_Index 
                                                                                          WHERE Profile.UID = '{0}';", UID);
            MySqlCommand cmd = new MySqlCommand(SQL_command, connection);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string profileName = reader.GetString("User_name");
                    int profileIndex = reader.GetInt32("Profile_Index");
                    int imageMode = reader.GetInt32("ImageMode");
                    int defaultImage = reader.IsDBNull(reader.GetOrdinal("DefaultIndex")) ? -1 : reader.GetInt32("DefaultIndex");
                    // 넘겨줄 리스트 Add해주기
                        ProfileList.Add(new Profile(profileName, profileIndex, imageMode, defaultImage));
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
            Debug.Log("SQL ProfileListSet : " + e.Message);
            if (!reader.IsClosed) reader.Close();
            return;
        }
    }

    /// <summary>
    /// UID, Index를 매개변수로 전달하여 Image를 Binary값으로 DB에 저장하는 Method (사진 찍기 선택)
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
            Debug.Log("AddProfile");
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
            cmd.Parameters.Add("@ImageData", MySqlDbType.MediumBlob).Value = ImageData; // 이미지 데이터를 Blob 타입으로 명시적으로 추가

            cmd.ExecuteNonQuery();
            Debug.Log("AddProfile after");
        }
        catch (Exception e)
        {
            Debug.Log("SQL AddProfileImage : " + e.Message);
            if (!reader.IsClosed) reader.Close();
            return;
        }
    }

    /// <summary>
    /// UID, Index를 매개변수로 전달하여 Image를 Index값으로 DB에 저장하는 Method (이미지 고르기 선택)
    /// </summary>
    /// <param name="defaultImage"></param>
    /// <param name="uid"></param>
    /// <param name="index"></param>
    public void SQL_AddProfileImage(int defaultImage, int uid, int index)
    {
        try
        {
            // 1. 데이터베이스 연결 확인
            if (!ConnectionCheck(connection))
            {
                Debug.Log("데이터베이스 연결 실패");
                return;
            }
            string SQL_command = "INSERT INTO Image (UID, Profile_Index, DefaultIndex) VALUES (@UID, @Index, @DefaultIndex)";
            MySqlCommand cmd = new MySqlCommand(SQL_command, connection);

            // 파라미터 추가
            cmd.Parameters.AddWithValue("@UID", uid);
            cmd.Parameters.AddWithValue("@Index", index);
            cmd.Parameters.AddWithValue("@DefaultIndex", defaultImage);

            cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Debug.Log("SQL AddProfileImage : " + e.Message);
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
        try
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
        catch(Exception e)
        {
            Debug.Log("SQL LoadProfileImage : " + e.Message);
            return null;
        }
    }

    /// <summary>
    /// 프로필 업데이트 Method, UID와 name을 확인해서 새로운 name과 image로 update (사진 찍기 선택)
    /// </summary>
    public void SQL_UpdateProfile(int profileIndex, string newName, int uid, string filename)
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
            string name_command = string.Format(@"UPDATE Profile SET UID = '{0}', User_name = '{1}' WHERE UID = '{2}' AND Profile_Index = '{3}'", uid, newName, uid, profileIndex);
            MySqlCommand cmd = new MySqlCommand(name_command, connection);
            cmd.ExecuteNonQuery();

            // 3. 기존 imageData들을 Null로 초기화 (사진 > 이미지 / 이미지 > 사진의 경우 고려)
            string null_command = @$"UPDATE Image SET ImageData = NULL, DefaultIndex = NULL WHERE UID = '{uid}' AND Profile_Index = '{profileIndex}';";
            MySqlCommand cmd_ = new MySqlCommand(null_command, connection);
            cmd_.ExecuteNonQuery();


            // 4. 이미지 변경
            byte[] ImageData;
            fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            binaryReader = new BinaryReader(fileStream);

            ImageData = binaryReader.ReadBytes((int)fileStream.Length);

            fileStream.Close();
            binaryReader.Close();

            string update_command = @"UPDATE Image SET ImageData = @ImageData WHERE UID = @UID AND Profile_Index = @Index;";
            MySqlCommand cmd__ = new MySqlCommand(update_command, connection);

            // 파라미터 추가
            cmd__.Parameters.AddWithValue("@UID", uid);
            cmd__.Parameters.AddWithValue("@Index", profileIndex);
            cmd__.Parameters.Add("@ImageData", MySqlDbType.MediumBlob).Value = ImageData; // 이미지 데이터를 Blob 타입으로 명시적으로 추가

            cmd__.ExecuteNonQuery();

            return; // 업데이트 성공
    }
        catch (Exception e)
        {
            Debug.Log("SQL UpdateProfile : " + e.Message);
            return;
        }
    }

    /// <summary>
    /// 프로필 업데이트 Method, UID와 name을 확인해서 새로운 name과 image로 update (이미지 고르기 선택)
    /// </summary>
    public void SQL_UpdateProfile(int profileIndex, string newName, int uid, int imageIndex)
    {
        try
        {
            // 1. SQL 서버에 접속 되어 있는지 확인
            if (!ConnectionCheck(connection))
            {
                return;
            }

            // 2. 기존 name찾아서 name 변경
            string name_command = string.Format(@"UPDATE Profile SET UID = '{0}', User_name = '{1}' WHERE UID = '{2}' AND Profile_Index = '{3}'", uid, newName, uid, profileIndex);
            MySqlCommand cmd = new MySqlCommand(name_command, connection);
            cmd.ExecuteNonQuery();

            // 3. 기존 imageData들을 Null로 초기화 (사진 > 이미지 / 이미지 > 사진의 경우 고려)
            string null_command = @$"UPDATE Image SET ImageData = NULL, DefaultIndex = NULL WHERE UID = '{uid}' AND Profile_Index = '{profileIndex}';";
            MySqlCommand cmd_ = new MySqlCommand(null_command, connection);
            cmd_.ExecuteNonQuery();

            // 4. 인덱스 수정
            string update_command = @$"UPDATE Image SET DefaultIndex = '{imageIndex}' WHERE UID = '{uid}' AND Profile_Index = '{profileIndex}';";
            MySqlCommand cmd__ = new MySqlCommand(update_command, connection);

            // 파라미터 추가
            cmd__.Parameters.AddWithValue("@UID", uid);
            cmd__.Parameters.AddWithValue("@Index", profileIndex);
            cmd__.Parameters.AddWithValue("@DefaultIndex", imageIndex);

            cmd__.ExecuteNonQuery();

            return; // 업데이트 성공
        }
        catch (Exception e)
        {
            Debug.Log("SQL UpdateProfile : " + e.Message);
            return;
        }
    }
    #endregion

    #region PushPush
    /// <summary>
    /// PushPush게임 진행 후 Custom한 Object와 Btn들의 정보를 DB에 전달하는 Method
    /// </summary>
    /// <param name="_jsonData"></param>
    /// <param name="_profileIndex"></param>
    public void SQL_AddPushpush(string _jsonData, int _profileIndex)
    {
        try
        {
            if (!reader.IsClosed) reader.Close();
            // 1. SQL 서버에 접속 되어 있는지 확인
            if (!ConnectionCheck(connection))
            {
                return;
            }

            // 2. 현재 ProfileIndex에 대한 레코드 수 확인
            string countQuery = $"SELECT COUNT(*) FROM PushPush WHERE ProfileIndex = {_profileIndex};";
            MySqlCommand countCmd = new MySqlCommand(countQuery, connection);
            int recordCount = Convert.ToInt32(countCmd.ExecuteScalar());

            // 3. 레코드 수가 6개 이상이면 가장 오래된 레코드 삭제
            if (recordCount >= 6)
            {
                string deleteOldestQuery = $"DELETE FROM PushPush WHERE ProfileIndex = {_profileIndex} ORDER BY CreatedAt LIMIT 1;";
                MySqlCommand deleteCmd = new MySqlCommand(deleteOldestQuery, connection);
                deleteCmd.ExecuteNonQuery();
            }

            // 4. 새 레코드 삽입
            string SQL_command = $"INSERT INTO PushPush (ProfileIndex, ObjInfo) VALUES('{_profileIndex}', '{_jsonData}');";
            MySqlCommand cmd = new MySqlCommand(SQL_command, connection);
            cmd.ExecuteNonQuery();

            return; 
        }
        catch (Exception e)
        {
            Debug.Log("SQL AddPushpush : " + e.Message);
            return;
        }
    }

    /// <summary>
    /// DB에 저장된 PushPushObject Class를 List로 받아오는 Method
    /// </summary>
    /// <param name="_profileIndex"></param>
    /// <returns></returns>
    public List<PushPushObject> SQL_SetPushPush(int _profileIndex)
    {
        try
        {
            if (!reader.IsClosed) reader.Close();
            // 1. SQL 서버에 접속 되어 있는지 확인
            if (!ConnectionCheck(connection))
            {
                return null;
            }

            // 2. JSON 데이터를 읽어와서 PushPushObject 리스트로 변환
            // 최근 순서대로 데이터를 정렬하여 조회
            string selectCommand = $"SELECT ObjInfo FROM PushPush WHERE ProfileIndex = '{_profileIndex}' ORDER BY CreatedAt DESC;";
            MySqlCommand selectCmd = new MySqlCommand(selectCommand, connection);
            reader = selectCmd.ExecuteReader();

            List<PushPushObject> push = new List<PushPushObject>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string jsonData = reader.GetString("ObjInfo");
                    PushPushObject pushObject = JsonUtility.FromJson<PushPushObject>(jsonData);
                    push.Add(pushObject);
                }
                if (!reader.IsClosed) reader.Close();
            }
            return push;
        }
        catch (Exception e)
        {
            if (!reader.IsClosed) reader.Close();
            Debug.Log("SQL SetPushPush : " + e.Message);
            return null;
        }
    }
    #endregion

    #region Network Connect
    /// <summary>
    /// Client가 접속할 때 Query문을 보내 접속중인 상태를 변경, Network에서 나갈 때 접속중인 상태를 변경
    /// </summary>
    /// <param name="_connectType"></param>
    /// <param name="_profileIndex"></param>
    public void SQL_ConnectCheck(bool _connectType, int _profileIndex)
    {
        try
        {
            // 1. SQL 서버에 접속 되어 있는지 확인
            if (!ConnectionCheck(connection))
            {
                return;
            }

            if (!reader.IsClosed) reader.Close();
            // 2. 매개변수로 받은 불값에 따라 본인의 접속 상태를 변경
            string update_cmd = @"UPDATE Profile SET NetworkConnect = @networkConnect WHERE Profile_Index = @ProfileIndex;";
            MySqlCommand updatecmd_ = new MySqlCommand(update_cmd, connection);

            // 매개변수를 True로 넣으면 접속할 때, False로 넣으면 접속 해제할 때
            string connect = _connectType ? "T" : "F";

            // 파라미터 추가
            updatecmd_.Parameters.AddWithValue("@networkConnect", connect);
            updatecmd_.Parameters.AddWithValue("@ProfileIndex", _profileIndex);

            updatecmd_.ExecuteNonQuery();
        }
        catch(Exception e)
        {
            if (!reader.IsClosed) reader.Close();
            Debug.Log(e.Message);
        }
    }

    /// <summary>
    ///  접속중인 Profile List Setting 하는 메소드
    /// </summary>
    /// <param name="_profileIndex"></param>
    /// <returns></returns>
    public List<Profile> SQL_ConnectListSet(int _profileIndex)
    { 
        try
        {
            // 1. SQL 서버에 접속 되어 있는지 확인
            if (!ConnectionCheck(connection))
            {
                return null;
            }
            if (!reader.IsClosed) reader.Close();

            List<Profile> connectedProfiles = new List<Profile>();

            string select_cmd = string.Format(@"SELECT DISTINCT Profile.User_name, Profile.Profile_Index, Profile.ImageMode, Image.DefaultIndex 
                                                                                          FROM Profile 
                                                                                          INNER JOIN Image ON Profile.Profile_Index = Image.Profile_Index 
                                                                                          WHERE NetworkConnect = '{0}';", "T");
            MySqlCommand selectcmd_ = new MySqlCommand(select_cmd, connection);
            reader = selectcmd_.ExecuteReader();

            if(reader.HasRows)
            {
                while(reader.Read())
                {
                    string profileName = reader.GetString("User_name");
                    int profileIndex = reader.GetInt32("Profile_Index");
                    if (profileIndex == _profileIndex) continue;
                    int imageMode = reader.GetInt32("ImageMode");
                    int defaultImage = reader.IsDBNull(reader.GetOrdinal("DefaultIndex")) ? -1 : reader.GetInt32("DefaultIndex");
                    // 넘겨줄 리스트 Add해주기
                    connectedProfiles.Add(new Profile(profileName, profileIndex, imageMode, defaultImage));
                }
                if (!reader.IsClosed) reader.Close();
                return connectedProfiles;
            }
            else
            {
                Debug.Log("접속 중인 유저가 없습니다.");
                if (!reader.IsClosed) reader.Close();
                return connectedProfiles;
            }    
        }
        catch(Exception e)
        {
            if (!reader.IsClosed) reader.Close();
            Debug.Log(e.Message);
            return null;
        }
    }

    /// <summary>
    /// 서버가 닫힐 때 모든 접속중인 Profile의 Connect를 F로 변경하는 Method
    /// </summary>
    public void SQL_OnServerDisconnected()
    {
        try
        {
            // 1. SQL 서버에 접속 되어 있는지 확인
            if (!ConnectionCheck(connection))
            {
                return;
            }

            string update_cmd = "UPDATE Profile SET NetworkConnect = 'F' WHERE NetworkConnect = 'T';";
            MySqlCommand updateCmd = new MySqlCommand(update_cmd, connection);
            updateCmd.ExecuteNonQuery();

            Debug.Log("모든 프로필의 접속 상태를 F로 변경했습니다.");

            if (!reader.IsClosed) reader.Close();
        }
        catch(Exception e)
        {
            Debug.Log("SQL OnServerDisconnected : " + e.Message);
        }
    }
    #endregion

    #region Favorite
    /// <summary>
    /// 즐겨찾기 리스트를 세팅하는 Method
    /// </summary>
    /// <param name="_profileIndex"></param>
    /// <returns></returns>
    public FavoriteList SQL_FavoriteListSet(int _profileIndex)
    { // 즐겨찾기 목록 ListUp하는 Method
        try
        {
            // 1. SQL 서버에 접속 되어 있는지 확인
            if (!ConnectionCheck(connection))
            {
                return null;
            }
            if (!reader.IsClosed) reader.Close();

            // 2. Profile 정보 받아오기
            string select_cmd = string.Format(@"SELECT FavoriteList FROM Favorite WHERE Profile_Index='{0}';", _profileIndex);
            MySqlCommand selectcmd_ = new MySqlCommand(select_cmd, connection);
            reader = selectcmd_.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                string favoriteInfo = reader.GetString("FavoriteList");
                FavoriteList favoriteLists = JsonUtility.FromJson<FavoriteList>(favoriteInfo);
                if (!reader.IsClosed) reader.Close();
                return favoriteLists; // 리스트 전달
            }
            else
            {
                if (!reader.IsClosed) reader.Close();
                Debug.Log("Favorite List가 없습니다.");
                return null;
            }
        }
        catch (Exception e)
        {
            if (!reader.IsClosed) reader.Close();
            Debug.Log("SQL ListUpFavorite : " + e.Message);
            return null;
        }
    }

    /// <summary>
    /// 즐겨찾기 리스트를 업데이트 하는 Method
    /// </summary>
    /// <param name="favoriteList"></param>
    /// <param name="_profileIndex"></param>
    public void SQL_UpdateFavoriteList(FavoriteList favoriteList, int _profileIndex)
    {
        try
        {
            // 1. SQL 서버에 접속 되어 있는지 확인
            if (!ConnectionCheck(connection))
            {
                return;
            }
            if (!reader.IsClosed) reader.Close();

            // 1. 받아온 FavoriteList를 string으로 변경
            string jsonFavorite = JsonUtility.ToJson(favoriteList);

            // 2. SQL에 갱신된 즐겨찾기 목록 업데이트
            string update_cmd = @"UPDATE Favorite SET FavoriteList = @FavoriteList WHERE Profile_Index = @ProfileIndex;";
            MySqlCommand updatecmd_ = new MySqlCommand(update_cmd, connection);

            // 파라미터 추가
            updatecmd_.Parameters.AddWithValue("@FavoriteList", jsonFavorite);
            updatecmd_.Parameters.AddWithValue("@ProfileIndex", _profileIndex);

            updatecmd_.ExecuteNonQuery();
            if (!reader.IsClosed) reader.Close();
        }
        catch(Exception e)
        {
            if (!reader.IsClosed) reader.Close();
            Debug.Log("SQL UpdateFavoriteList" + e.Message);
        }
    }
    #endregion

    #region Heart And Like
    /// <summary>
    /// 좋아요와 하트를 누른 프로필 인덱스 리스트들을 세팅하는 Method
    /// </summary>
    /// <param name="_profileIndex"></param>
    /// <returns></returns>
    public HeartAndLike SQL_HeartAndLikeListSet(int _profileIndex)
    { // 즐겨찾기 목록 ListUp하는 Method
        try
        {
            // 1. SQL 서버에 접속 되어 있는지 확인
            if (!ConnectionCheck(connection))
            {
                return null;
            }
            if (!reader.IsClosed) reader.Close();

            // 2. Profile 정보 받아오기
            string select_cmd = string.Format(@"SELECT PressHeartAndLikeList FROM Profile WHERE Profile_Index='{0}';", _profileIndex);
            MySqlCommand selectcmd_ = new MySqlCommand(select_cmd, connection);
            reader = selectcmd_.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                string heartAndLikeList = reader.GetString("PressHeartAndLikeList");
                HeartAndLike heartAndLikeLists = JsonUtility.FromJson<HeartAndLike>(heartAndLikeList);
                if (!reader.IsClosed) reader.Close();
                return heartAndLikeLists; // 리스트 전달
            }
            else
            {
                if (!reader.IsClosed) reader.Close();
                Debug.Log("HeartAndLikeList가 없습니다.");
                return null;
            }
        }
        catch (Exception e)
        {
            Debug.Log("SQL HeartAndLikeListSet : " + e.Message);
            return null;
        }
    }

    /// <summary>
    ///  좋아요와 하트 버튼을 누른 프로필 인덱스를 업데이트하는 Method
    /// </summary>
    /// <param name="heartAndLikeList"></param>
    /// <param name="_profileIndex"></param>
    public void SQL_UpdateHeartAndLikeList(HeartAndLike heartAndLikeList, int _profileIndex)
    {
        try
        {
            // 1. SQL 서버에 접속 되어 있는지 확인
            if (!ConnectionCheck(connection))
            {
                return;
            }
            if (!reader.IsClosed) reader.Close();
            string jsonHeartAndLike = JsonUtility.ToJson(heartAndLikeList);
            string update_cmd = @"UPDATE Profile SET PressHeartAndLikeList = @PressHeartAndLikeList WHERE Profile_Index = @ProfileIndex;";
            MySqlCommand updatecmd_ = new MySqlCommand(update_cmd, connection);

            // 파라미터 추가
            updatecmd_.Parameters.AddWithValue("@PressHeartAndLikeList", jsonHeartAndLike);
            updatecmd_.Parameters.AddWithValue("@ProfileIndex", _profileIndex);

            updatecmd_.ExecuteNonQuery();

            if (!reader.IsClosed) reader.Close();
        }
        catch (Exception e)
        {
            if (!reader.IsClosed) reader.Close();
            Debug.Log("SQL UpdateFavoriteList" + e.Message);
            return;
        }
    }

    public void SQL_UpdateHeartAndLikeCount(int _profileIndex, bool _isLikeButton)
    {
        try
        {
            // 1. SQL 서버에 접속 되어 있는지 확인
            if (!ConnectionCheck(connection))
            {
                return;
            }
            if (!reader.IsClosed) reader.Close();

            // 2. Profile을 조회하여 Heart와 Like의 int 값을 받아옴

            // 2. LikeCount 또는 HeartCount를 업데이트하는 쿼리 작성
            string update_cmd = _isLikeButton ?
                "UPDATE Profile SET LikeCount = LikeCount + 1 WHERE Profile_Index = @ProfileIndex;" :
                "UPDATE Profile SET HeartCount = HeartCount + 1 WHERE Profile_Index = @ProfileIndex;";

            MySqlCommand updatecmd_ = new MySqlCommand(update_cmd, connection);

            // 파라미터 추가
            updatecmd_.Parameters.AddWithValue("@ProfileIndex", _profileIndex);

            // 쿼리 실행
            updatecmd_.ExecuteNonQuery();

            if (!reader.IsClosed) reader.Close();
        }
        catch (Exception e)
        {
            if (!reader.IsClosed) reader.Close();
            Debug.Log("SQL UpdateHeartAndLikeCount : " + e.Message);
            return;
        }
    }

    /// <summary>
    /// 현재 모아모아 창이 열려있는 profileindex의 좋아요와 하트 버튼의 개수를 갱신해주는 Method
    /// </summary>
    /// <param name="_profileIndex"></param>
    /// <returns></returns>
    public int[] SQL_HeartAndLikeCount(int _profileIndex)
    {
        try
        {
            // 1. SQL 서버에 접속 되어 있는지 확인
            if (!ConnectionCheck(connection))
            {
                return null;
            }
            if (!reader.IsClosed) reader.Close();

            // 2. Profile을 조회하여 Heart와 Like의 int 값을 받아옴
            string select_cmd = string.Format(@"SELECT LikeCount, HeartCount FROM Profile WHERE Profile_Index='{0}';", _profileIndex);
            MySqlCommand selectcmd_ = new MySqlCommand(select_cmd, connection);
            reader = selectcmd_.ExecuteReader();

            if (reader.Read())
            {
                int heart = reader.GetInt32("HeartCount");
                int like = reader.GetInt32("LikeCount");

                if (!reader.IsClosed) reader.Close();
                return new int[2] { like, heart };
            }
            else
            {
                if (!reader.IsClosed) reader.Close();
                return null;
            }
        }
        catch(Exception e)
        {
            if (!reader.IsClosed) reader.Close();
            Debug.Log("SQL HeartAndLikeCount : " + e.Message);
            return null;
        }
    }

    #endregion

    public void PrintProfileImage(Image _profileImage, bool _imageMode, int _profileIndex)
    { // 프로필 인덱스에 따라 이미지 출력
        if (_imageMode)
        { // 이미지 고르기 선택한 플레이어일 때
            for (int i = 0; i < ProfileList.Count; i++)
            {
                if (_profileIndex.Equals(ProfileList[i].index))
                {
                    _profileImage.sprite = ProfileManager.Instance.ProfileImages[ProfileList[i].defaultImage];
                }
            }
        }
        else
        { // 사진 찍기를 선택한 플레이어일 때
            Texture2D profileTexture = SQL_LoadProfileImage(ProfileManager.Instance.UID, _profileIndex);
            Sprite profileSprite = ProfileManager.Instance.TextureToSprite(profileTexture);
            _profileImage.sprite = profileSprite;
        }
    }
    #endregion
}