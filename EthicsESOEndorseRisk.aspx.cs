using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

/**
 * The risk endorsement page.
 * Allows the decision of low or non low risk to be made by the ESO.
 */
public partial class EthicsESOEndorseRisk : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e) {}

    public void btnBack_ServerClick(object sender, EventArgs e)
    {
        back();
    }

    //Update application and email stakeholders.
    public void btnSubmit_ServerClick(object sender, EventArgs e)
    {
        string error = ""; //Used to catch email errors.
        string error2 = "";
        if (validate()) //Make sure form status is valid.
        {
            MySqlConnection mySqlConnection = new Connector().MySQLConnect();
            MySqlCommand command = mySqlConnection.CreateCommand();

            //Update application.
            command.CommandText = @"UPDATE Application SET AppStatus = 4, RiskLow_Bool = " + radioLowRisk.Checked + ", RiskNonLow_Bool = " + radioNonLowRisk.Checked + 
                                   " WHERE AppID = " + Context.Request["AppID"];
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
                string riskLevel;
                if (radioLowRisk.Checked)
                    riskLevel = "Low risk";
                else
                    riskLevel = "Non low risk";
                string subject = "Risk level decision for '" + title + "'";
                string msg = Notifications.riskEmail(title, riskLevel);
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

                //If contact person different to primary investigator email them to.
                if (investID != contactID)
                {
                    reader.Dispose();

                    //Get Contact persons details.
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

                if (radioNonLowRisk.Checked) //If a non low risk application contact EO's.
                {
                    reader.Dispose();

                    //Get EO's details.
                    command.CommandText = @"SELECT NameFirst, NameLast, Email FROM Staff WHERE AccountType = 3";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        msg = "Application for project '" + title + "' has been assessed as non low risk and is ready for processing.";
                        while (reader.Read())
                        {
                            //Notify EO's.
                            Notifications.send(reader["Email"].ToString(), "New non low risk application", msg);
                        }
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
            divMsg.InnerText = "Please select low risk or non low risk.";
    }

    //Test form valid status.
    private bool validate()
    {
        bool rtn = true;
        if ((radioLowRisk.Checked == true && radioNonLowRisk.Checked == true) || (radioLowRisk.Checked == false && radioNonLowRisk.Checked == false))
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