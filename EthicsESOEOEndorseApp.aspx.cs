using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

/**
 * The application endorsement page.
 * Allows the application to be Approved, Denied or marked Incomplete.
 * Used by the ESO and EO for low and no low risk applications.
 */
public partial class EthicsESOEOEndorseApp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e) {}

    public void btnBack_ServerClick(object sender, EventArgs e)
    {
        back();
    }

    //Update application and notify stakeholders.
    public void btnSubmit_ServerClick(object sender, EventArgs e)
    {
        string error = ""; //Used to catch email errors.
        string error2 = "";
        if (submitValid()) //Make sure form state is valid.
        {
            MySqlConnection mySqlConnection = new Connector().MySQLConnect();
            MySqlCommand command = mySqlConnection.CreateCommand();

            //Update application.
            if (radioAccept.Checked)
                command.CommandText = @"UPDATE Application SET AppStatus = 8 WHERE AppID = " + Context.Request["AppID"]; //Accepted.
            else if (radioDecline.Checked)
                command.CommandText = @"UPDATE Application SET AppStatus = 7, RejComment = '" + txtRej.Value.ToString() + "' WHERE AppID = " + Context.Request["AppID"]; //Declined.
            else
                command.CommandText = @"UPDATE Application SET AppStatus = 5, RejComment = '" + txtRej.Value.ToString() + "' WHERE AppID = " + Context.Request["AppID"]; //Incomplete.
            int status = command.ExecuteNonQuery();

            //Get application information.
            command.CommandText = @"SELECT a1_ProjTitle, a4_InvestStaffID, a6_ContactStaffID, RiskLow_Bool, RiskNonLow_Bool FROM Application WHERE AppID = " + Context.Request["AppID"];
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                int investID = Convert.ToInt32(reader["a4_InvestStaffID"]);
                int contactID = Convert.ToInt32(reader["a6_ContactStaffID"]);
                string title = reader["a1_ProjTitle"].ToString();
                string riskLevel = "";
                if (Convert.ToBoolean(reader["RiskLow_Bool"]))
                    riskLevel = "Low risk";
                else if (Convert.ToBoolean(reader["RiskNonLow_Bool"]))
                    riskLevel = "Non low risk";
                string msg;
                string subject = "Outcome of review for '" + title + "'";

                //Create email message.
                if (radioAccept.Checked)
                    msg = Notifications.acceptEmail(title, riskLevel);
                else if (radioDecline.Checked)
                    msg = Notifications.declineEmail(title, riskLevel, txtRej.Value.ToString());
                else
                {
                    msg = Notifications.incompleteEmail(title, txtRej.Value.ToString());
                    subject = "Project '" + title + "' is incomplete";
                }
                reader.Dispose();

                //Get PI details.
                command.CommandText = @"SELECT NameFirst, NameLast, Email FROM Staff WHERE StaffID = " + investID;
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    //Send email to PI.
                    error = Notifications.send(reader["Email"].ToString(), subject, msg);
                }

                //If contact person different to primary investigator.
                if (investID != contactID)
                {
                    reader.Dispose();

                    //Get Contact person details.
                    command.CommandText = @"SELECT NameFirst, NameLast, Email FROM Staff WHERE StaffID = " + contactID;
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        //Send email to contact person.
                        error2 = Notifications.send(reader["Email"].ToString(), subject, msg);
                    }
                }
                reader.Dispose();

                //Get CI's details.
                command.CommandText = @"SELECT NameFirst, NameLast, Email FROM Staff INNER JOIN a5_coinvestigators ON Staff.StaffID = a5_coinvestigators.StaffID WHERE AppID = " + Context.Request["AppID"];
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //Email CI's.
                        Notifications.send(reader["Email"].ToString(), subject, msg);
                    }
                }
            }
            reader.Dispose();
            command.Dispose();
            mySqlConnection.Close();
            if (status != 1 || error != "" || error2 != "") //Check for and display errors.
            {
                divMsg.InnerText = "Error saving!";
                divMsg2.InnerText = "Failure to send email!" + Environment.NewLine + error;
                divMsg3.InnerText = "Failure to send email!" + Environment.NewLine + error2;
            }
            else
                back();
        }
        else
            divMsg.InnerText = "Invalid. Select accept, decline or incomplete. If declined or incomplete please make a comment.";
    }

    //Validate selection to determine if change can be made.
    private bool submitValid()
    {
        bool rtn = true;
        if ((radioAccept.Checked == true && radioDecline.Checked == true && radioIncomplete.Checked == true) || (radioAccept.Checked == false && radioDecline.Checked == false && radioIncomplete.Checked == false))
            rtn = false;
        if (radioDecline.Checked == true && txtRej.Value.ToString() == "" || radioIncomplete.Checked == true && txtRej.Value.ToString() == "")
            rtn = false;
        return rtn;
    }

    //Go back to main page.
    private void back()
    {
        String nextAddr = SharedFunctions.getMainPageAddr(Convert.ToInt32(Context.Request["Type"]), Convert.ToInt32(Context.Request["StaffID"]));
        Response.Redirect(nextAddr);
    }
}