using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

/**
 * Make connection to MySQL database.
 */
public class Connector
{
	public Connector()
    {}

    public MySql.Data.MySqlClient.MySqlConnection MySQLConnect()
    {
        //Based on code from:
        //https://support.godaddy.com/help/article/7331/connecting-to-a-mysql-database-using-asp-net
        MySql.Data.MySqlClient.MySqlConnection mySqlConnection = new MySql.Data.MySqlClient.MySqlConnection();
        
        //Localhost connection string.
        //mySqlConnection.ConnectionString = "Database=ethicsdb;Server=localhost;UID=root;PWD=;Convert Zero Datetime=True;Allow Zero Datetime=True;";

        //Server connection string.
        mySqlConnection.ConnectionString = "Server=MYSQL5005.Smarterasp.net;Database=db_9c5218_pdm;Uid=9c5218_pdm;Pwd=Curtin123;Convert Zero Datetime=True;Allow Zero Datetime=True;";

        try
        {
            mySqlConnection.Open();
            if (mySqlConnection.State == System.Data.ConnectionState.Open)
            {
                // Connection has been made
            }
            else
            {
                throw new Exception("The database connection state is Closed");
            }
        }
        catch (MySql.Data.MySqlClient.MySqlException ex)
        {
            throw ex; //Pass exception to browser.
        }
        catch (Exception ex)
        {
            throw ex; //Pass exception to browser.
        }
        return mySqlConnection;
    }
}