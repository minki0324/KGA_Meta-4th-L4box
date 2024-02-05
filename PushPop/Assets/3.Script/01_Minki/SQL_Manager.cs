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
    public User_Info info;

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
        string serverinfo = Serverset(DB_path);
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
    //��ο� ������ ���ٸ� �⺻ Default ���� ���� Method
    private void Default_Data(string path)
    {
        List<server_info> userInfo = new List<server_info>();

        // (testdb.cj4ki2qmepdi.ap-northeast-2.rds.amazonaws.com) = RDS ���� IP
        userInfo.Add(new server_info("testdb.cj4ki2qmepdi.ap-northeast-2.rds.amazonaws.com", "PushPop", "root", "12345678", "3306"));

        JsonData data = JsonMapper.ToJson(userInfo);
        File.WriteAllText(path + "/config.json", data.ToString());
    }

    // SQL DATA�� �ҷ����� Method
    // ���, ���� Ž�� �� Json���� �о�ͼ� HeidiSQL ����
    private string Serverset(string path)
    {
        if (!File.Exists(path)) // ��� Ž��
        {
            Directory.CreateDirectory(path);
        }

        if (!File.Exists(path + "/config.json"))  // ���� Ž��
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

    //SQL�� ���� �ִ��� Ȯ���ϴ� Method 
    private bool Connection_Check(MySqlConnection con)
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

    //Signup ID �ߺ�üũ Method 
    private bool ID_Duplicate_Check(string ID)
    {
        try
        {
            string SQL_command = string.Format(@"SELECT User_ID FROM User_Info WHERE User_ID='{0}';", ID);
            MySqlCommand cmd = new MySqlCommand(SQL_command, connection);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                // �ߺ��Ǵ� ID�� ������ true ��ȯ
                if (reader.HasRows)
                {
                    return true;
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            // ���� �߻� �� ó��
            // �� ���, ȸ������ ������ �ߴ��ϰ� ��
            return false;
        }
        // �ߺ��Ǵ� ID�� ������ false ��ȯ
        return false;
    }

    // Login �Ǿ� �ִ��� üũ Method
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
                    // ���� ���� ��Ȳ
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
            // ���� �߻� �� ó��
            // �� ���, ȸ������ ������ �ߴ��ϰ� ��
            return false;
        }
    }

    /// <summary>
    ///  ȸ������ �޼ҵ�.
    ///  SQL ���ӿ��� Ȯ��, ID �ߺ�üũ �Ŀ� SQL DB�� ȸ������ �õ�
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="PW"></param>
    /// <returns></returns>
    public bool SQL_SignUp(string ID, string PW)
    {
        try
        {
            // 1. SQL ������ ���� �Ǿ� �ִ��� Ȯ��
            if (!Connection_Check(connection))
            {
                return false;
            }

            // 2. ID �ߺ� üũ
            if (ID_Duplicate_Check(ID))
            {
                // �ߺ��� ID UI ���
                Debug.Log("�ߺ��� ID�� �ֽ��ϴ�.");
                return false; // �ߺ��� ID�� �����Ƿ� ȸ������ ����
            }

            // 3. �ű� ����
            string SQL_command = string.Format(@"INSERT INTO User_Info (User_ID, User_PW) VALUES('{0}', '{1}');", ID, PW);
            MySqlCommand cmd = new MySqlCommand(SQL_command, connection);
            cmd.ExecuteNonQuery();

            return true; // ȸ������ ����
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return false;
        }
    }

    /// <summary>
    ///���� DB���� Data�� ������ ���� Method
    ///��ȸ�� �Ǵ� Data�� ���ٸ� bool�� = false
    ///��ȸ�� �Ǵ� Data�� �ִٸ� info�� ����ְ� bool�� = true
    /// </summary>
    public bool SQL_Login(string ID, string PW)
    {
        try
        {
            //1.connection open ��Ȳ���� Ȯ��
            if (!Connection_Check(connection))
            {
                return false;
            }

            //2. ID �ߺ� ���� üũ

            string SQL_command = string.Format(@"SELECT User_ID,User_PW, UID FROM User_Info WHERE User_ID='{0}' AND User_PW = '{1}' ;", ID, PW);
            MySqlCommand cmd = new MySqlCommand(SQL_command, connection);
            reader = cmd.ExecuteReader();

            //Reader ���� �����Ͱ� 1�� �̻� �����ϴ°�?
            if (reader.HasRows)
            {
                //���� �����͸� �ϳ��� ������
                while (reader.Read())
                {
                    string id = reader.GetString("User_ID");
                    string pw = reader.GetString("User_PW");
                    int connect = reader.GetInt32("Connect");
                    if (!id.Equals(string.Empty) || !pw.Equals(string.Empty))
                    {
                        if(Login_Check(id, connect))
                        {
                            Debug.Log("�������� ID �Դϴ�.");
                            return false;
                        }
                        else
                        {
                            //���������� Data�� �ҷ��� ��Ȳ
                            UID = reader.GetInt32("UID");
                            info = new User_Info(ID, UID);
                            if (!reader.IsClosed) reader.Close();
                            return true;
                        }
                    }
                    else//�α��ν���
                    {
                        Debug.Log("�α��� ����");
                        if (!reader.IsClosed) reader.Close();
                        break;
                    }
                }//while
            }//if
            Debug.Log("reader�� ���� �����Ͱ� ����");
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
    /// ������ ���� Method, name �ߺ� üũ�� ���� ���� ���� (�������� ���)
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool SQL_Add_Profile(string name)
    {
        try
        {
            // 1. SQL ������ ���� �Ǿ� �ִ��� Ȯ��
            if (!Connection_Check(connection))
            {
                return false;
            }

            // 2. ������ ����
            string SQL_command = string.Format(@"INSERT INTO Profile (UID, User_name) VALUES('{0}', '{1}');", info.UID, name);
            MySqlCommand cmd = new MySqlCommand(SQL_command, connection);
            cmd.ExecuteNonQuery();

            return true; // ȸ������ ����
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return false;
        }
    }

    /// <summary>
    /// �α��� �� UID�� ��ȸ�Ǵ� ������ ��� ����ϴ� Method
    /// </summary>
    public void SQL_Profile_ListSet()
    {
        try
        {
            // �����ͺ��̽� ���� Ȯ��
            if (!Connection_Check(connection))
            {
                Debug.Log("�����ͺ��̽� ���� ����");
                return;
            }

            // UID�� ����� ������ ��ȸ ���� ����
            string SQL_command = string.Format(@"SELECT User_name, Profile_Index FROM Profile WHERE UID = '{0}';", info.UID);
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
    /// Ranking Table�� Score, Timer�� �Ѱ��ִ� Method.
    /// score�� ������� �Ҷ��� Timer�� null�� �����ϰ�, timer�� ������� �Ҷ��� score�� null�� �����ؼ� ���
    /// </summary>
    public void SQL_Set_Score(string profile_name, int profile_index, int? newScore, float? newTimer, int UID)
    {
        try
        {
            // 1. SQL ������ ���� �Ǿ� �ִ��� Ȯ��
            if (!Connection_Check(connection))
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
    public void SQL_Print_Ranking()
    {
        try
        {
            // 1. SQL ������ ���� �Ǿ� �ִ��� Ȯ��
            if (!Connection_Check(connection))
            {
                return;
            }

            string SQL_command = string.Format(@"SELECT Profile_Name, Profile_Index, Score, Timer FROM Ranking WHERE UID = '{0}';", info.UID);
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
}