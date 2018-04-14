using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

/**
 * The HOS endorsement page.
 * Allows the HOS to Endorse or deny (and comment on) an application.
 */
public partial class EthicsHOSEndorse : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e) {}

    public void btnBack_ServerClick(object sender, EventArgs e)
    {
        back();
    }

    //Update application and notify stakeholders.
    public void btnSubmit_ServerClick(object sender, EventArgs e)
    {
        if (validate()) //Check form status is valid for submission.
        {
            string error = "";
            string error2 = "";
            MySqlConnection mySqlConnection = new Connector().MySQLConnect();
            MySqlCommand command = mySqlConnection.CreateCommand();

            //Update application.
            if (radioAccept.Checked)
                command.CommandText = @"UPDATE Application SET AppStatus = 3, DateEndorsed = '" + DateTime.Today.Year + "-" + DateTime.Today.Month + "-" + DateTime.Today.Day + "' WHERE AppID = " + Context.Request["AppID"]; //Submit to ESO.
            else
                command.CommandText = @"UPDATE Application SET AppStatus = 2, RejComment = '" + txtDeclined.Value + "' WHERE AppID = " + Context.Request["AppID"]; //Rejected.
            int status = command.ExecuteNonQuery();

            if (status != 1)
            {
                command.Dispose();
                mySqlConnection.Close();
                divMsg.InnerText = "Error saving!";
            }
            else
            {
                //Email PI, Contact & CI.
                command.CommandText = @"SELECT a1_ProjTitle, a4_InvestStaffID, a6_ContactStaffID FROM Application WHERE AppID = " + Context.Request["AppID"];
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    string title = reader["a1_ProjTitle"].ToString();
                    int investID = Convert.ToInt32(reader["a4_InvestStaffID"]);
                    int contactID = Convert.ToInt32(reader["a6_ContactStaffID"]);
                    reader.Dispose();

                    //Build email message.
                    string msg = "Application for project '" + title + "' has been accepted by the head of school." + Environment.NewLine + 
                                 "Should you have any queries about the consideration of your project please contact the Ethics Support Officer or the Ethics Office.";
                    if (radioDenied.Checked) { //Build reject email.
                        msg = "Application for project '" + title + "' has been declined by the head of school." + Environment.NewLine +
                              "Comments on your application are outlined below." + Environment.NewLine + txtDeclined.Value.ToString() + Environment.NewLine + Environment.NewLine +
                              "Should you have any queries about the consideration of your project please contact the Ethics Support Officer or the Ethics Office.";
                    }
                    string subject = "Ethics application HOS decision for '" + title + "'";

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

                    //Notify ESO's.
                    if (radioAccept.Checked)
                    {
                        reader.Dispose();

                        //Get ESO's details.
                        command.CommandText = @"SELECT NameFirst, NameLast, Email FROM Staff WHERE AccountType = 2";
                        reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            msg = "Application for project '" + title + "' has been approved by the HOS and is ready for risk level processing.";
                            while (reader.Read())
                            {
                                //Email ESO's.
                                Notifications.send(reader["Email"].ToString(), "New application approved by HOS", msg);
                            }
                        }
                    }
                    if (error != "" || error2 != "") //Check for and display errors.
                    {
                        divMsg2.InnerText = "Failure to send email!" + Environment.NewLine + error;
                        divMsg3.InnerText = "Failure to send email!" + Environment.NewLine + error2;
                    }
                    else
                    {
                        reader.Dispose(); //Cleanup.
                        command.Dispose();
                        mySqlConnection.Close();
                        back();
                    }
                }
                reader.Dispose(); //Cleanup.
                command.Dispose();
                mySqlConnection.Close();
            }
        }
        else
            divMsg.InnerText = "Invalid. Select endorse or decline. If declined please make a comment.";
    }

    //Validate submission.
    private bool validate()
    {
        bool rtn = false;
        if (radioAccept.Checked == true || (radioDenied.Checked == true && txtDeclined.Value.ToString() != ""))
            rtn = true;
        return rtn;
    }

    //Go back to main page.
    private void back()
    {
        string nextAddr = SharedFunctions.getMainPageAddr(Convert.ToInt32(Context.Request["Type"]), Convert.ToInt32(Context.Request["StaffID"]));
        Response.Redirect(nextAddr);
    }
}