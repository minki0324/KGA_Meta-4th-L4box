using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using LitJson;

public class user_info
{
    public string User_ID { get; private set; }
    public int UID { get; private set; }

    public user_info(string id, int uid)
    {
        User_ID = id;
        UID = uid;
    }
}

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

/// <summary>
/// SQL ���� Query�� ó�� Class
/// </summary>
public class SQL_Manager : MonoBehaviour
{
    public static SQL_Manager instance = null;
    public user_info info;

    public MySqlConnection connection;
    public MySqlDataReader reader;

    public string DB_path = string.Empty;
    public string ServerIP = string.Empty;
    public int UID;
    public List<Profile> Profile_list = new List<Profile>();

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
                    if (!id.Equals(string.Empty) || !pw.Equals(string.Empty))
                    {
                        //���������� Data�� �ҷ��� ��Ȳ
                        UID = reader.GetInt32("UID");
                        info = new user_info(ID, UID);
                        Debug.Log(id);
                        Debug.Log(UID);
                        Debug.Log("�α��� ����");
                        if (!reader.IsClosed) reader.Close();
                        return true;
                    }
                    else//�α��ν���
                    {
                        Debug.Log("�α��� ����");
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
            string SQL_command = string.Format(@"INSERT INTO Profile (User_Index, User_name) VALUES('{0}', '{1}');", info.UID, name);
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
            string SQL_command = string.Format(@"SELECT User_name, Profile_Index FROM Profile WHERE User_Index = '{0}';", info.UID);
            MySqlCommand cmd = new MySqlCommand(SQL_command, connection);
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string profileName = reader.GetString("User_name");
                    int profileIndex = reader.GetInt32("Profile_Index");

                    // ������ ���� ��� (��: �ܼ� �α�, UI ������Ʈ ��)
                    Debug.Log($"Profile Name: {profileName}, Profile ID: {profileIndex}");

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
    /// �����ʿ� Score�� �Ѱ��ִ� Method
    /// </summary>
    public void SQL_Set_Score()
    {
        try
        {
            // 1. SQL ������ ���� �Ǿ� �ִ��� Ȯ��
            if (!Connection_Check(connection))
            {
                return;
            }

            // 2. ������ ����
            string SQL_command = string.Format(@"INSERT INTO Ranking (User_ID, Score) VALUES('{0}', '{1}');", info.UID, name);
            MySqlCommand cmd = new MySqlCommand(SQL_command, connection);
            cmd.ExecuteNonQuery();

            return; // ȸ������ ����
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return;
        }
    }
    #endregion
}