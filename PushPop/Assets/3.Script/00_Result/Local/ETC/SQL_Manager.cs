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
/// UID�� �����ִ� Profile
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
        if(mode.Equals(0))
        { // ���� ���� ���
            imageMode = false;
        }
        else if(mode.Equals(1))
        { // default image ������ ���
            imageMode = true;
        }
        defaultImage = _defaultImage;
    }
}

// ���� ���� Class
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

    public List<int> FriendIndex { get; private set; }
}
#endregion

/// <summary>
/// SQL ���� Query�� ó�� Class
/// </summary>
public class SQL_Manager : MonoBehaviour
{
    public static SQL_Manager instance = null;

    public MySqlConnection connection;
    public MySqlDataReader reader;


    public string DB_path = string.Empty;   // Json Path (DB)
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

        // serverinfo�� �޾ƿ� �����Ͱ� ���ٸ� ����
        if (serverinfo.Equals(string.Empty))
        {
            return;
        }

        // if������ ������ ���� �����Դٸ� SQL �����ֱ�
        connection = new MySqlConnection(serverinfo);
        connection.Open(); // �õ�: �����ͺ��̽� ����
        Debug.Log($"SQL Awake : {connection.State}");
    }

    private void OnApplicationQuit()
    {

    }
    #endregion

    #region Other Method
    #region ConnectServer
    //��ο� ������ ���ٸ� �⺻ Default ���� ���� Method
    private void DefaultData(string path)
    {
        try
        {
            List<server_info> userInfo = new List<server_info>();

            // (database-1.cj4ki2qmepdi.ap-northeast-2.rds.amazonaws.com) = RDS ���� IP
            userInfo.Add(new server_info("database-1.cj4ki2qmepdi.ap-northeast-2.rds.amazonaws.com", "PushPop", "PushPopDB", "ruddlf4wh", "7958"));

            JsonData data = JsonMapper.ToJson(userInfo);
            File.WriteAllText(path + "/config.json", data.ToString());
        }
        catch(Exception e)
        {
            Debug.Log("DefaultData : " + e.Message);
        }
    }

    // SQL DATA�� �ҷ����� Method
    // ���, ���� Ž�� �� Json���� �о�ͼ� HeidiSQL ����
    private string ServerSet(string path)
    {
        try
        {
            if (!File.Exists(path)) // ��� Ž��
            {
                Directory.CreateDirectory(path);
            }

            if (!File.Exists(path + "/config.json"))  // ���� Ž��
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

    //SQL�� ���� �ִ��� Ȯ���ϴ� Method 
    private bool ConnectionCheck(MySqlConnection con)
    {
        try
        {
            //���� MySQLConnection open �� �ƴ϶��?
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
    // GUID �ߺ�üũ Method
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
    /// ���ӽ� GUID�� Ȯ���Ͽ� ù �����̸� GUID�� �ο��� �α����ϰ�, ���� GUID�� �ִٸ� �ش� GUID�� ���� �α��� �ϴ� Method
    /// </summary>
    /// <param name="GUID"></param>
    public void SQL_AddUser(string GUID)
    {
        try
        {
            // 1. SQL ������ ���� �Ǿ� �ִ��� Ȯ��
            if (!ConnectionCheck(connection))
            {
                return;
            }

            // �̹� ������ GUID�� �ִ��� Ȯ��
            if (!GUID_DuplicateCheck(GUID))
            {
                // 2. ��� UID ����
                string SQL_command = string.Format(@"INSERT INTO User_Info (GUID) VALUES('{0}')
                                                                                    ON DUPLICATE KEY UPDATE GUID = VALUES(GUID);", GUID);
                MySqlCommand cmd = new MySqlCommand(SQL_command, connection);
                cmd.ExecuteNonQuery();
            }

            // 3. UID ���� �޾ƿ���
            string sql_cmd = string.Format(@"SELECT UID FROM User_Info WHERE GUID = '{0}';", GUID);
            MySqlCommand cmd_ = new MySqlCommand(sql_cmd, connection);
            reader = cmd_.ExecuteReader();

            if (reader.HasRows)
            {
                // 4. UID�� Ŭ������ ��� ������ �Ҵ�
                reader.Read();
                UID = reader.GetInt32("UID");
                ProfileManager.Instance.UID = UID;                
            }
            if (!reader.IsClosed) reader.Close();
            return; // ȸ������ ����
        }
        catch (Exception e)
        {
            if (!reader.IsClosed) reader.Close();
            Debug.Log("SQL AddUser : " + e.Message);
            return;
        }
    }

    /// <summary>
    /// ������ ���� Method, name �ߺ� üũ�� ���� ���� ���� (�������� ���)
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
                return profileIndex; // ���� ������ �������� primary key ��ȯ
            }
            else
            {
                return -1; // ������ ���� ���и� ��Ÿ���� �� ��ȯ
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
            // 1. SQL ������ ���� �Ǿ� �ִ��� Ȯ��
            if (!ConnectionCheck(connection))
            {
                return;
            }

            // 2. ������ �̹��� ��� ����
            string name_command = string.Format(@"UPDATE Profile SET ImageMode = '{0}' WHERE UID = '{1}' AND Profile_Index = '{2}';", imageMode, uid, profileIndex);
            MySqlCommand cmd = new MySqlCommand(name_command, connection);
            cmd.ExecuteNonQuery();

            return; // ������ ���� ���и� ��Ÿ���� �� ��ȯ
        }
        catch (Exception e)
        {
            Debug.Log("SQL UpdateMode : " + e.Message);
            return;
        }
    }

    /// <summary>
    /// ������ ���� Method, UID�� name�� Ȯ���ؼ� ����
    /// </summary>
    /// <param name="name"></param>
    public void SQL_DeleteProfile(int index)
    {
        try
        {
            // 1. SQL ������ ���� �Ǿ� �ִ��� Ȯ��
            if (!ConnectionCheck(connection))
            {
                return;
            }

            // 2. ������ ����
            string profile_command = string.Format(@"DELETE FROM Profile WHERE UID = '{0}' AND Profile_Index = '{1}';", UID, index);
            MySqlCommand profile_cmd = new MySqlCommand(profile_command, connection);
            profile_cmd.ExecuteNonQuery();

            // 3. �̹��� ����
            string image_command = string.Format(@"DELETE FROM Image WHERE UID = '{0}' AND Profile_Index = '{1}';", UID, index);
            MySqlCommand image_cmd = new MySqlCommand(image_command, connection);
            image_cmd.ExecuteNonQuery();

            // 4. PushPush ����
            string pushpush_command = string.Format(@"DELETE FROM PushPush WHERE ProfileIndex = '{0}';",  index);
            MySqlCommand pushpush_cmd = new MySqlCommand(pushpush_command, connection);
            pushpush_cmd.ExecuteNonQuery();

            // 5. Ranking ����
            Ranking.Instance.DeleteRankAndVersus(index);

            // 6. List �ʱ�ȭ
            for(int i = 0; i < ProfileList.Count; i++)
            {
                if(index == ProfileList[i].index)
                {
                    Profile tempProfile = ProfileList[i];
                    ProfileList.Remove(tempProfile);
                }
            }

            // ���� ����
            Debug.Log("������ ���� ����");
        }
        catch (Exception e)
        {
            Debug.Log("SQL DeleteProfile : " + e.Message);
        }
    }

    /// <summary>
    /// �α��� �� UID�� ��ȸ�Ǵ� ������ ��� ����ϴ� Method
    /// </summary>
    public void SQL_ProfileListSet()
    {
        try
        {
            // �����ͺ��̽� ���� Ȯ��
            if (!ConnectionCheck(connection))
            {
                Debug.Log("�����ͺ��̽� ���� ����");
                return;
            }

            ProfileList.Clear();
            // UID�� ����� ������ ��ȸ ���� ����
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
                    // �Ѱ��� ����Ʈ Add���ֱ�
                        ProfileList.Add(new Profile(profileName, profileIndex, imageMode, defaultImage));
                }
                if (!reader.IsClosed) reader.Close();
                return;
            }
            else
            {
                Debug.Log("��ϵ� �������� �����ϴ�.");
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
    /// UID, Index�� �Ű������� �����Ͽ� Image�� Binary������ DB�� �����ϴ� Method (���� ��� ����)
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
            // 1. �����ͺ��̽� ���� Ȯ��
            if (!ConnectionCheck(connection))
            {
                Debug.Log("�����ͺ��̽� ���� ����");
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

            // �Ķ���� �߰�
            cmd.Parameters.AddWithValue("@UID", uid);
            cmd.Parameters.AddWithValue("@Index", index);
            cmd.Parameters.Add("@ImageData", MySqlDbType.MediumBlob).Value = ImageData; // �̹��� �����͸� Blob Ÿ������ ��������� �߰�

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
    /// UID, Index�� �Ű������� �����Ͽ� Image�� Index������ DB�� �����ϴ� Method (�̹��� ���� ����)
    /// </summary>
    /// <param name="defaultImage"></param>
    /// <param name="uid"></param>
    /// <param name="index"></param>
    public void SQL_AddProfileImage(int defaultImage, int uid, int index)
    {
        try
        {
            // 1. �����ͺ��̽� ���� Ȯ��
            if (!ConnectionCheck(connection))
            {
                Debug.Log("�����ͺ��̽� ���� ����");
                return;
            }
            string SQL_command = "INSERT INTO Image (UID, Profile_Index, DefaultIndex) VALUES (@UID, @Index, @DefaultIndex)";
            MySqlCommand cmd = new MySqlCommand(SQL_command, connection);

            // �Ķ���� �߰�
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
    /// Profile_index�� ���� ������ �̹����� ����ϴ� Method
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

            // �Ķ���� �߰�
            cmd.Parameters.AddWithValue("@UID", uid);
            cmd.Parameters.AddWithValue("@ProfileIndex", profileIndex);

            // �̹��� �����͸� �б� ���� ExecuteScalar() �޼��带 ����մϴ�.
            byte[] imageData = (byte[])cmd.ExecuteScalar();

            // ImageData�� Texture2D�� ��ȯ
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(imageData); // ����Ʈ �迭�κ��� �ؽ�ó�� �ε��մϴ�.
            return texture;
        }
        catch(Exception e)
        {
            Debug.Log("SQL LoadProfileImage : " + e.Message);
            return null;
        }
    }

    /// <summary>
    /// ������ ������Ʈ Method, UID�� name�� Ȯ���ؼ� ���ο� name�� image�� update (���� ��� ����)
    /// </summary>
    public void SQL_UpdateProfile(int profileIndex, string newName, int uid, string filename)
    {
        FileStream fileStream;
        BinaryReader binaryReader;
        try
        {
            // 1. SQL ������ ���� �Ǿ� �ִ��� Ȯ��
            if (!ConnectionCheck(connection))
            {
                return;
            }
            // 2. ���� nameã�Ƽ� name ����
            string name_command = string.Format(@"UPDATE Profile SET UID = '{0}', User_name = '{1}' WHERE UID = '{2}' AND Profile_Index = '{3}'", uid, newName, uid, profileIndex);
            MySqlCommand cmd = new MySqlCommand(name_command, connection);
            cmd.ExecuteNonQuery();

            // 3. ���� imageData���� Null�� �ʱ�ȭ (���� > �̹��� / �̹��� > ������ ��� ���)
            string null_command = @$"UPDATE Image SET ImageData = NULL, DefaultIndex = NULL WHERE UID = '{uid}' AND Profile_Index = '{profileIndex}';";
            MySqlCommand cmd_ = new MySqlCommand(null_command, connection);
            cmd_.ExecuteNonQuery();


            // 4. �̹��� ����
            byte[] ImageData;
            fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            binaryReader = new BinaryReader(fileStream);

            ImageData = binaryReader.ReadBytes((int)fileStream.Length);

            fileStream.Close();
            binaryReader.Close();

            string update_command = @"UPDATE Image SET ImageData = @ImageData WHERE UID = @UID AND Profile_Index = @Index;";
            MySqlCommand cmd__ = new MySqlCommand(update_command, connection);

            // �Ķ���� �߰�
            cmd__.Parameters.AddWithValue("@UID", uid);
            cmd__.Parameters.AddWithValue("@Index", profileIndex);
            cmd__.Parameters.Add("@ImageData", MySqlDbType.MediumBlob).Value = ImageData; // �̹��� �����͸� Blob Ÿ������ ��������� �߰�

            cmd__.ExecuteNonQuery();

            return; // ������Ʈ ����
    }
        catch (Exception e)
        {
            Debug.Log("SQL UpdateProfile : " + e.Message);
            return;
        }
    }

    /// <summary>
    /// ������ ������Ʈ Method, UID�� name�� Ȯ���ؼ� ���ο� name�� image�� update (�̹��� ���� ����)
    /// </summary>
    public void SQL_UpdateProfile(int profileIndex, string newName, int uid, int imageIndex)
    {
        try
        {
            // 1. SQL ������ ���� �Ǿ� �ִ��� Ȯ��
            if (!ConnectionCheck(connection))
            {
                return;
            }

            // 2. ���� nameã�Ƽ� name ����
            string name_command = string.Format(@"UPDATE Profile SET UID = '{0}', User_name = '{1}' WHERE UID = '{2}' AND Profile_Index = '{3}'", uid, newName, uid, profileIndex);
            MySqlCommand cmd = new MySqlCommand(name_command, connection);
            cmd.ExecuteNonQuery();

            // 3. ���� imageData���� Null�� �ʱ�ȭ (���� > �̹��� / �̹��� > ������ ��� ���)
            string null_command = @$"UPDATE Image SET ImageData = NULL, DefaultIndex = NULL WHERE UID = '{uid}' AND Profile_Index = '{profileIndex}';";
            MySqlCommand cmd_ = new MySqlCommand(null_command, connection);
            cmd_.ExecuteNonQuery();

            // 4. �ε��� ����
            string update_command = @$"UPDATE Image SET DefaultIndex = '{imageIndex}' WHERE UID = '{uid}' AND Profile_Index = '{profileIndex}';";
            MySqlCommand cmd__ = new MySqlCommand(update_command, connection);

            // �Ķ���� �߰�
            cmd__.Parameters.AddWithValue("@UID", uid);
            cmd__.Parameters.AddWithValue("@Index", profileIndex);
            cmd__.Parameters.AddWithValue("@DefaultIndex", imageIndex);

            cmd__.ExecuteNonQuery();

            return; // ������Ʈ ����
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
    /// PushPush���� ���� �� Custom�� Object�� Btn���� ������ DB�� �����ϴ� Method
    /// </summary>
    /// <param name="_jsonData"></param>
    /// <param name="_profileIndex"></param>
    public void SQL_AddPushpush(string _jsonData, int _profileIndex)
    {
        try
        {
            if (!reader.IsClosed) reader.Close();
            // 1. SQL ������ ���� �Ǿ� �ִ��� Ȯ��
            if (!ConnectionCheck(connection))
            {
                return;
            }

            // 2. ���� ProfileIndex�� ���� ���ڵ� �� Ȯ��
            string countQuery = $"SELECT COUNT(*) FROM PushPush WHERE ProfileIndex = {_profileIndex};";
            MySqlCommand countCmd = new MySqlCommand(countQuery, connection);
            int recordCount = Convert.ToInt32(countCmd.ExecuteScalar());

            // 3. ���ڵ� ���� 6�� �̻��̸� ���� ������ ���ڵ� ����
            if (recordCount >= 6)
            {
                string deleteOldestQuery = $"DELETE FROM PushPush WHERE ProfileIndex = {_profileIndex} ORDER BY CreatedAt LIMIT 1;";
                MySqlCommand deleteCmd = new MySqlCommand(deleteOldestQuery, connection);
                deleteCmd.ExecuteNonQuery();
            }

            // 4. �� ���ڵ� ����
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
    /// DB�� ����� PushPushObject Class�� List�� �޾ƿ��� Method
    /// </summary>
    /// <param name="_profileIndex"></param>
    /// <returns></returns>
    public List<PushPushObject> SQL_SetPushPush(int _profileIndex)
    {
        try
        {
            if (!reader.IsClosed) reader.Close();
            // 1. SQL ������ ���� �Ǿ� �ִ��� Ȯ��
            if (!ConnectionCheck(connection))
            {
                return null;
            }

            // 2. JSON �����͸� �о�ͼ� PushPushObject ����Ʈ�� ��ȯ
            // �ֱ� ������� �����͸� �����Ͽ� ��ȸ
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

    #region Favorite
    public FavoriteList SQL_ListUpFavorite(int _profileIndex)
    { // ���ã�� ��� ListUp�ϴ� Method
        try
        {
            // 1. SQL ������ ���� �Ǿ� �ִ��� Ȯ��
            if (!ConnectionCheck(connection))
            {
                return null;
            }

            // 2. Profile ���� �޾ƿ���
            string select_cmd = string.Format(@"FavoriteList FROM Favorite WHERE Profile_Index='{0}';", _profileIndex);
            MySqlCommand selectcmd_ = new MySqlCommand(select_cmd, connection);
            reader = selectcmd_.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                string favoriteInfo = reader.GetString("FavoriteList");
                FavoriteList favoriteLists = JsonUtility.FromJson<FavoriteList>(favoriteInfo);
                if (!reader.IsClosed) reader.Close();
                return favoriteLists; // ����Ʈ ����
            }
            else
            {
                if (!reader.IsClosed) reader.Close();
                Debug.Log("Favorite List�� �����ϴ�.");
                return null;
            }
        }
        catch (Exception e)
        {
            Debug.Log("SQL ListUpFavorite : " + e.Message);
            return null;
        }
    }

    public void SQL_UpdateFavoriteList(FavoriteList favoriteList, int _profileIndex)
    {
        try
        {
            // 1. SQL ������ ���� �Ǿ� �ִ��� Ȯ��
            if (!ConnectionCheck(connection))
            {
                return;
            }

            string jsonFavorite = JsonUtility.ToJson(favoriteList);

            string update_cmd = @"UPDATE Favorite SET FavoriteList = @FavoriteList WHERE Profile_Index = @ProfileIndex;";
            MySqlCommand updatecmd_ = new MySqlCommand(update_cmd, connection);

            // �Ķ���� �߰�
            updatecmd_.Parameters.AddWithValue("@FavoriteList", jsonFavorite);
            updatecmd_.Parameters.AddWithValue("@ProfileIndex", _profileIndex);

            updatecmd_.ExecuteNonQuery();
        }
        catch(Exception e)
        {
            Debug.Log("SQL UpdateFavoriteList" + e.Message);
        }
    }
    #endregion

    public void PrintProfileImage(Image _profileImage, bool _imageMode, int _profileIndex)
    { // ������ �ε����� ���� �̹��� ���
        if (_imageMode)
        { // �̹��� ���� ������ �÷��̾��� ��
            for (int i = 0; i < ProfileList.Count; i++)
            {
                if (_profileIndex.Equals(ProfileList[i].index))
                {
                    _profileImage.sprite = ProfileManager.Instance.ProfileImages[ProfileList[i].defaultImage];
                }
            }
        }
        else
        { // ���� ��⸦ ������ �÷��̾��� ��
            Texture2D profileTexture = SQL_LoadProfileImage(ProfileManager.Instance.UID, _profileIndex);
            Sprite profileSprite = ProfileManager.Instance.TextureToSprite(profileTexture);
            _profileImage.sprite = profileSprite;
        }
    }
    #endregion
}