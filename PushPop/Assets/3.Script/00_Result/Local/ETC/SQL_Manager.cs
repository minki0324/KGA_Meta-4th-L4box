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
/// ID, PW�� �����ϴ� User_Info
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
/// UID�� �����ִ� Profile
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
#endregion

/// <summary>
/// SQL ���� Query�� ó�� Class
/// </summary>
public class SQL_Manager : MonoBehaviour
{
    public static SQL_Manager instance = null;
    public User_Info Info;

    public MySqlConnection connection;
    public MySqlDataReader reader;
    

    public string DB_path = string.Empty;   // Json��� (DB)
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
            // serverinfo�� �޾ƿ� �����Ͱ� ���ٸ� ����
            if (serverinfo.Equals(string.Empty))
            {
                Debug.Log("SQL Server Json Error!");
                return;
            }

            // if������ ������ ���� �����Դٸ� SQL �����ֱ�
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
    //��ο� ������ ���ٸ� �⺻ Default ���� ���� Method
    private void DefaultData(string path)
    {
        List<server_info> userInfo = new List<server_info>();

        // (testdb.cj4ki2qmepdi.ap-northeast-2.rds.amazonaws.com) = RDS ���� IP
        userInfo.Add(new server_info("testdb.cj4ki2qmepdi.ap-northeast-2.rds.amazonaws.com", "PushPop", "root", "12345678", "3306"));

        JsonData data = JsonMapper.ToJson(userInfo);
        File.WriteAllText(path + "/config.json", data.ToString());
    }

    // SQL DATA�� �ҷ����� Method
    // ���, ���� Ž�� �� Json���� �о�ͼ� HeidiSQL ����
    private string ServerSet(string path)
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
            $" CharSet=utf8;";

        return serverInfo;
    }

    //SQL�� ���� �ִ��� Ȯ���ϴ� Method 
    private bool ConnectionCheck(MySqlConnection con)
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
    #endregion

    #region Profile
    // GUID �ߺ�üũ Method
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
                GameManager.instance.UID = UID;
                Info = new User_Info(GUID, UID);
            }
            if (!reader.IsClosed) reader.Close();
            return; // ȸ������ ����
        }
        catch (Exception e)
        {
            if (!reader.IsClosed) reader.Close();
            Debug.Log(e.Message);
            return;
        }
    }

    /// <summary>
    /// ������ ���� Method, name �ߺ� üũ�� ���� ���� ���� (�������� ���)
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool SQL_AddProfile(string name)
    {
        try
        {
            // 1. SQL ������ ���� �Ǿ� �ִ��� Ȯ��
            if (!ConnectionCheck(connection))
            {
                return false;
            }

            // 2. ������ ����
            string SQL_command = string.Format(@"INSERT INTO Profile (UID, User_name) VALUES('{0}', '{1}');", Info.UID, name);
            MySqlCommand cmd = new MySqlCommand(SQL_command, connection);
            cmd.ExecuteNonQuery();

            return true; // ������ ���� ����
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return false;
        }
    }

    /// <summary>
    /// ������ ���� Method, UID�� name�� Ȯ���ؼ� ����
    /// </summary>
    /// <param name="name"></param>
    public void SQL_DeleteProfile(string name, int index)
    {
        try
        {
            // 1. SQL ������ ���� �Ǿ� �ִ��� Ȯ��
            if (!ConnectionCheck(connection))
            {
                return;
            }

            // 2. ������ ����
            string SQL_command = string.Format(@"DELETE FROM Profile WHERE UID = '{0}' AND User_name = '{1}';", Info.UID, name);
            MySqlCommand cmd = new MySqlCommand(SQL_command, connection);
            cmd.ExecuteNonQuery();

            // 3. �̹��� ����
            string sql_cmd = string.Format(@"DELETE FROM Image WHERE UID = '{0}' AND Profile_Index = '{1}';", Info.UID, index);
            MySqlCommand cmd_ = new MySqlCommand(sql_cmd, connection);
            cmd_.ExecuteNonQuery();

            // ���� ����
            Debug.Log("������ ���� ����");
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
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

            Profile_list.Clear();
            // UID�� ����� ������ ��ȸ ���� ����
            string SQL_command = string.Format(@"SELECT User_name, Profile_Index FROM Profile WHERE UID = '{0}';", Info.UID);
            MySqlCommand cmd = new MySqlCommand(SQL_command, connection);
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string profileName = reader.GetString("User_name");
                    int profileIndex = reader.GetInt32("Profile_Index");

                    // �Ѱ��� ����Ʈ Add���ֱ�
                    Profile_list.Add(new Profile(profileName, profileIndex));
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
            Debug.Log($"������ ��ȸ �� ���� �߻�: {e.Message}");
            if (!reader.IsClosed) reader.Close();
            return;
        }
    }

    /// <summary>
    /// UID, Index�� �Ű������� �����Ͽ� Image�� Binary������ DB�� �����ϴ� Method
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
            cmd.Parameters.AddWithValue("@ImageData", ImageData);

            cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Debug.Log($"������ ��ȸ �� ���� �߻�: {e.Message}");
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

    /// <summary>
    /// ������ ������Ʈ Method, UID�� name�� Ȯ���ؼ� ���ο� name�� image�� update
    /// </summary>
    public void SQL_UpdateProfile(int previousIndex, string previousname, string name, int uid, string filename)
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
            string sql_cmd = string.Format(@"UPDATE Profile SET (UID, User_name) VALUES('{0}', '{1}');", Info.UID, name);
            MySqlCommand cmd = new MySqlCommand(sql_cmd, connection);
            cmd.ExecuteNonQuery();

            // 3. �̹��� ����
            byte[] ImageData;
            fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            binaryReader = new BinaryReader(fileStream);

            ImageData = binaryReader.ReadBytes((int)fileStream.Length);

            fileStream.Close();
            binaryReader.Close();

            string SQL_command = @$"UPDATE Image SET ImageData = '{ImageData}' WHERE UID = '{uid}' AND Profile_Index = '{previousIndex}';";
            MySqlCommand cmd_ = new MySqlCommand(SQL_command, connection);

            // �Ķ���� �߰�
            cmd_.Parameters.AddWithValue("@UID", uid);
            cmd_.Parameters.AddWithValue("@Index", previousIndex);
            cmd_.Parameters.AddWithValue("@ImageData", ImageData);

            cmd_.ExecuteNonQuery();

            return; // ������Ʈ ����
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
    /// Ranking Table�� Score, Timer�� �Ѱ��ִ� Method.
    /// score�� ������� �Ҷ��� Timer�� null�� �����ϰ�, timer�� ������� �Ҷ��� score�� null�� �����ؼ� ���
    /// </summary>
    public void SQL_SetScore(string profile_name, int profile_index, int? newScore, float? newTimer, int UID)
    {
        try
        {
            // 1. SQL ������ ���� �Ǿ� �ִ��� Ȯ��
            if (!ConnectionCheck(connection))
            {
                return;
            }

            // 2. ���� ��� ��ȸ
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

            // 3. ��� ���� �Ǵ� ����
            if (hasExistingRecord)
            {
                // ������Ʈ ���� �˻�: ���ο� ������ ���� �������� ũ�ų�, ���ο� Ÿ�̸� ���� ���� ������ ���� ��쿡�� ������Ʈ
                if (!reader.IsClosed) reader.Close();
                bool shouldUpdateScore = newScore.HasValue && newScore.Value > existingScore;
                bool shouldUpdateTime = newTimer.HasValue && (newTimer.Value < existingTimer || existingTimer == 0); // ���� Ÿ�̸Ӱ� 0�̸� �׻� ������Ʈ

                if (shouldUpdateScore || shouldUpdateTime)
                {
                    // ������Ʈ ��� ������ Ÿ�̸� ����
                    int scoreToUpdate = shouldUpdateScore ? newScore.Value : existingScore;
                    float timerToUpdate = shouldUpdateTime ? newTimer.Value : existingTimer;

                    string updateCommand = string.Format(
                        @"UPDATE Ranking SET Score = {0}, Timer = {1} WHERE Profile_Name = '{2}' AND Profile_Index = {3} AND UID = {4};",
                        scoreToUpdate, // ������Ʈ�� ���ο� Score �Ǵ� ���� Score ����
                        string.Format(CultureInfo.InvariantCulture, "{0:0.###}", timerToUpdate), // ������Ʈ�� ���ο� Timer �Ǵ� ���� Timer ����
                        profile_name, profile_index, UID);

                    new MySqlCommand(updateCommand, connection).ExecuteNonQuery();
                }
            }
            // 4. ���� ����� ������, ���ο� ��� ����
            else
            {
                if (!reader.IsClosed) reader.Close();
                string insertCommand = string.Format(
                    @"INSERT INTO Ranking (Profile_Name, Profile_Index, UID, Score, Timer) VALUES('{0}', {1}, {2}, {3}, {4});",
                    profile_name, profile_index, UID,
                    newScore.HasValue ? newScore.Value.ToString() : "0", // newScore�� null�̸� 0�� �⺻������ ���
                    newTimer.HasValue ? string.Format(CultureInfo.InvariantCulture, "{0:0.###}", newTimer.Value) : "0"); // newTimer�� null�̸� 0�� �⺻������ ���

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
    /// ȣ��� ���� UID�� �����ϴ� Profile���� ��ŷ�� List�� ��� Method
    /// </summary>
    /// <param name="Mode"></param>
    public void SQL_PrintRanking()
    {
        try
        {
            // 1. SQL ������ ���� �Ǿ� �ִ��� Ȯ��
            if (!ConnectionCheck(connection))
            {
                return;
            }

            string SQL_command = string.Format(@"SELECT Profile_Name, Profile_Index, Score, Timer FROM Ranking WHERE UID = '{0}';", Info.UID);
            MySqlCommand cmd = new MySqlCommand(SQL_command, connection);
            reader = cmd.ExecuteReader();

            if(reader.HasRows)
            {
                // 2. ��ŷ ����Ʈ ��� �� �ʱ�ȭ
                Rank_List.Clear();

                // 3. �о�� �������� ���� UID�� ���� �ִ� Profile���� ��ŷ ������ �ҷ��� List�� ���
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
                Debug.Log("�����ʿ� ��ϵ� Rank�� �����ϴ�.");
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