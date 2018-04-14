using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Odbc;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Net;

/**
 * Login page.
 * Displays list of all users in system and allows login to the main pages.
 */
public partial class _EthicsLogIn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ClientScript.RegisterHiddenField("__EVENTTARGET", "btnSubmit"); //Hidden field that makes the submit button run when enter is pressed.
        MySql.Data.MySqlClient.MySqlConnection mySqlConnection = new Connector().MySQLConnect(); //Create new connection to database.
        MySqlCommand command = mySqlConnection.CreateCommand();
        command.CommandText = "SELECT * FROM Staff ORDER BY Email";

        MySqlDataAdapter da = new MySqlDataAdapter(command);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (!Page.IsPostBack) //Stops resetting position of the email selector.
        {
            //Bind list of emails for display. Obvious security issue in a deployed app.
            sltUsername.DataSource = ds;
            sltUsername.DataTextField = "Email";
            sltUsername.DataValueField = "Email";
            sltUsername.DataBind();
        }
        da.Dispose(); //Cleanup.
        command.Dispose();
        mySqlConnection.Close();
    }

    //Test password and email address combination is correct.
    protected void btnSubmit_ServerClick(object sender, EventArgs e)
    {
        MySql.Data.MySqlClient.MySqlConnection mySqlConnection = new Connector().MySQLConnect();
        MySqlCommand command = mySqlConnection.CreateCommand();
        command.CommandText = "SELECT * FROM Staff WHERE Email = '" + sltUsername.Items[sltUsername.SelectedIndex].Value + "' AND Password = '" + txtPassword.Value + "'";
        MySqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            reader.Read();
            String nextAddr = SharedFunctions.getMainPageAddr(Convert.ToInt32(reader["AccountType"]), Convert.ToInt32(reader["StaffID"])); //Construct address based on account type.
            reader.Dispose(); //Cleanup.
            command.Dispose();
            mySqlConnection.Close();
            Response.Redirect(nextAddr); //Log in.
        }
        else
        {
            divMsg.InnerText = "Incorrect password"; //Notify user.
            reader.Dispose(); //Cleanup.
            command.Dispose();
            mySqlConnection.Close();
        }
    }
}
