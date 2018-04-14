using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;

public partial class EthicsESOEndorseReviewer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Populate reviewer list.
            MySqlConnection mySqlConnection = new Connector().MySQLConnect();
            MySqlCommand command = mySqlConnection.CreateCommand();
            command.CommandText = "SELECT StaffID, CONCAT(IFNULL(staff.NameLast,''),', ',IFNULL(staff.NameFirst,'')) AS FullName FROM staff ORDER BY FullName";
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);

            sltReviewer.DataSource = ds;
            sltReviewer.DataTextField = "FullName";
            sltReviewer.DataValueField = "StaffID";
            sltReviewer.DataBind();

            ListItem dflt = new ListItem();
            dflt.Text = "-";
            dflt.Value = "0";
            sltReviewer.Items.Insert(0, dflt);
            da.Dispose();

            command.CommandText = @"SELECT Reviewer_StaffID FROM Application WHERE AppID = " + Context.Request["AppID"];
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                if (reader["Reviewer_StaffID"].ToString() != "")
                {
                    for (int i = 0; i < sltReviewer.Items.Count; i++) //Set reviewer ID if one exists.
                    {
                        if (Convert.ToInt32(sltReviewer.Items[i].Value) == Convert.ToInt32(reader["Reviewer_StaffID"]))
                            sltReviewer.Items[i].Selected = true;
                    }
                }
            }
            else
                divMsg.InnerText = "Application not found";
            reader.Dispose();
            command.Dispose();
            mySqlConnection.Close();
            btnSubmitEnable();
        }
    }

    public void btnBack_ServerClick(object sender, EventArgs e)
    {
        save();
        back();
    }

    public void btnSave_ServerClick(object sender, EventArgs e)
    {
        save();
    }

    //Email reviewer and display success/failure.
    public void btnSubmit_ServerClick(object sender, EventArgs e)
    {
        save();
        if (btnSubmit.Disabled == false) //Make sure save action didn't invalidate submission.
        {
            MySqlConnection mySqlConnection = new Connector().MySQLConnect();
            MySqlCommand command = mySqlConnection.CreateCommand();

            //Get project information.
            command.CommandText = @"SELECT a1_ProjTitle FROM application WHERE AppID = " + Request["AppID"];
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                string title = reader["a1_ProjTitle"].ToString();
                reader.Dispose();

                //Get reviewer details.
                command.CommandText = @"SELECT NameFirst, Email FROM Staff WHERE StaffID = " + Convert.ToInt32(sltReviewer.Items[sltReviewer.SelectedIndex].Value);
                reader = command.ExecuteReader();
                if (reader.HasRows) //Populate page with data.
                {
                    reader.Read();
                    string msg = Notifications.reviewEmail(reader["NameFirst"].ToString(), title, Request["AppID"]);

                    //Send email to reviewer with link to application.
                    string error = Notifications.send(reader["Email"].ToString(), "You have been selected to review an ethics application", msg);
                    if (error != "")
                        divMsg.InnerText = "Failure to send email sent to: " + reader["Email"].ToString() + Environment.NewLine + error;
                    else
                        divMsg.InnerText = "Email sent to " + reader["Email"].ToString();
                }
            }
            reader.Dispose();
            command.Dispose();
            mySqlConnection.Dispose();
        }
        else
            divMsg.InnerText = "Please make a valid selection before sending.";
    }

    //Save reviewer to DB. 
    private void save()
    {
        MySqlConnection mySqlConnection = new Connector().MySQLConnect();
        MySqlCommand command = mySqlConnection.CreateCommand();
        int appStatus = 6;
        if (Convert.ToInt32(sltReviewer.Items[sltReviewer.SelectedIndex].Value) == 0)
            appStatus = 4;
        command.CommandText = @"UPDATE Application SET Reviewer_StaffID = " + Convert.ToInt32(sltReviewer.Items[sltReviewer.SelectedIndex].Value) + ", AppStatus = " + appStatus + 
                               " WHERE AppID = " + Context.Request["AppID"];
        int status = command.ExecuteNonQuery();
        command.Dispose();
        mySqlConnection.Close();
        if (status != 1)
        {
            divMsg.InnerText = "Error saving!";
        }
        else
        {
            divMsg.InnerText = "";
            btnSubmitEnable();
        }
    }

    //Enable/Disable the submit button.
    private void btnSubmitEnable()
    {
        if (sltReviewer.SelectedIndex == 0)
            btnSubmit.Disabled = true;
        else
            btnSubmit.Disabled = false;
    }

    //Go back to main page.
    private void back()
    {
        String nextAddr = SharedFunctions.getMainPageAddr(Convert.ToInt32(Context.Request["Type"]), Convert.ToInt32(Context.Request["StaffID"]));
        Response.Redirect(nextAddr);
    }
}