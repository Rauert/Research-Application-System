using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Web.UI.HtmlControls;
using System.Data;

/**
 * Section 7 Edit/View application page.
 * Select HOS, submits form and displays rejection/incomplete comment if applicable.
 */
public partial class EthicsEditAppS7 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) //Don't rebuild page data on postback.
        {
            MySql.Data.MySqlClient.MySqlConnection mySqlConnection = new Connector().MySQLConnect();
            MySqlCommand command = mySqlConnection.CreateCommand();

            //Build HOS list.
            command.CommandText = "SELECT StaffID, CONCAT(IFNULL(staff.NameLast,''),', ',IFNULL(staff.NameFirst,'')) AS FullName FROM staff WHERE AccountType = 1 ORDER BY FullName";
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);

            sltHOS.DataSource = ds;
            sltHOS.DataTextField = "FullName";
            sltHOS.DataValueField = "StaffID";
            sltHOS.DataBind();

            ListItem dflt = new ListItem();
            dflt.Text = "-";
            dflt.Value = "0";
            sltHOS.Items.Insert(0, dflt);
            da.Dispose();

            command.CommandText = @"SELECT AppStatus, g_HOS_StaffID, RejComment FROM Application WHERE AppID = " + Context.Request["AppID"];
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows) //Populate page with data.
            {
                reader.Read();
                int appStatus = Convert.ToInt32(reader["AppStatus"]);
                if (appStatus == 0 || appStatus == 2 || appStatus == 5)
                    radioDeclare.Checked = false;
                if (appStatus == 2) //Display rejection comment if status is HOS rejected.
                {
                    tblDec.Visible = true;
                    lblDeclined.InnerText = "HOS declined comment:";
                }
                else if (appStatus == 7) //Display rejection comment if status is declined.
                {
                    tblDec.Visible = true;
                    lblDeclined.InnerText = "Declined comment:";
                }
                else if (appStatus == 5) //Display rejection comment if status is incomplete.
                {
                    tblDec.Visible = true;
                    lblDeclined.InnerText = "Incomplete comment:";
                }
                txtDeclined.InnerText = reader["RejComment"].ToString();
                if (reader["g_HOS_StaffID"].ToString() != "")
                {
                    for (int i = 0; i < sltHOS.Items.Count; i++) //Set currently selected HOS.
                    {
                        if (Convert.ToInt32(sltHOS.Items[i].Value) == Convert.ToInt32(reader["g_HOS_StaffID"]))
                            sltHOS.Items[i].Selected = true;
                    }
                }
            }
            else
                Response.Write("Application not found");
            reader.Dispose();
            command.Dispose();
            mySqlConnection.Close();

            if (Context.Request["Mode"].Equals("W")) //If write access then unlock.
            {
                btnSave.Visible = true;
                sltHOS.Disabled = false;
                radioDeclare.Disabled = false;
            }
            else
                btnSubmit.Visible = false;

            //Check application validity.
            //Colour the application navigation menus appropriately.
            bool[][] valid = SharedFunctions.validateApplication(Convert.ToInt32(Context.Request["AppID"]));
            if (valid[0][0])
                btnTriage.Style.Add("color", "Green");
            else
                btnTriage.Style.Add("color", "Red");
            if (valid[1][0])
                btnS1.Style.Add("color", "Green");
            else
                btnS1.Style.Add("color", "Red");
            if (valid[2][0])
                btnS2.Style.Add("color", "Green");
            else
                btnS2.Style.Add("color", "Red");
            if (valid[3][0])
                btnS3.Style.Add("color", "Green");
            else
                btnS3.Style.Add("color", "Red");
            if (valid[4][0])
                btnS4.Style.Add("color", "Green");
            else
                btnS4.Style.Add("color", "Red");
            if (valid[5][0])
                btnS5.Style.Add("color", "Green");
            else
                btnS5.Style.Add("color", "Red");
            if (valid[6][0])
                btnS6.Style.Add("color", "Green");
            else
                btnS6.Style.Add("color", "Red");

            validate(valid[7]); //Validate entire section.

            if (valid[0][0] && valid[1][0] && valid[2][0] && valid[3][0] && valid[4][0] && valid[5][0] && valid[6][0])
                btnSubmit.Disabled = false;
        }
    }

    //Navigate to another page of application.
    public void btnPageChange_ServerClick(object sender, EventArgs e)
    {
        if (Context.Request["Mode"].Equals("W")) //If write access then save.
        {
            save();
        }
        //Build url from button clicked and redirect.
        HtmlInputButton source = (HtmlInputButton)sender;
        string nextAddr = SharedFunctions.getEditAppPageAddr(source.ID[source.ID.Length - 1], Context.Request["Mode"], Convert.ToInt32(Context.Request["AppID"]), Convert.ToInt32(Context.Request["StaffID"]), Convert.ToInt32(Context.Request["Type"]));
        Response.Redirect(nextAddr);
    }

    //Navigate back to main menu.
    public void btnBack_ServerClick(object sender, EventArgs e)
    {
        if (Context.Request["Mode"].Equals("W")) //If write access then save.
        {
            save();
        }
        //Build url for main page and redirect.
        String nextAddr = SharedFunctions.getMainPageAddr(Convert.ToInt32(Context.Request["Type"]), Convert.ToInt32(Context.Request["StaffID"]));
        Response.Redirect(nextAddr);
    }

    public void btnSave_ServerClick(object sender, EventArgs e)
    {
        save();
    }

    //Change status and send emails.
    //Only clickable if all other section completed (ie green).
    public void btnSubmit_ServerClick(object sender, EventArgs e)
    {
        save();
        if (radioDeclare.Checked && sltHOS.SelectedIndex != 0) //Make sure the delaration box is checked and a HOS has been nominated.
        {
            MySqlConnection mySqlConnection = new Connector().MySQLConnect();
            MySqlCommand command = mySqlConnection.CreateCommand();
            command.CommandText = @"SELECT a1_ProjTitle, a4_InvestStaffID, a6_ContactStaffID, g_HOS_StaffID, AppStatus FROM Application WHERE AppID = " + Context.Request["AppID"];
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                //Error strings to catch email error messages.
                string error = "";
                string error2 = "";
                string error3 = "";

                //Extract application information for emails.
                reader.Read();
                string title = reader["a1_ProjTitle"].ToString();
                int investID = Convert.ToInt32(reader["a4_InvestStaffID"]);
                int contactID = Convert.ToInt32(reader["a6_ContactStaffID"]);
                int hosID = Convert.ToInt32(reader["g_HOS_StaffID"]);
                int appStatus = Convert.ToInt32(reader["AppStatus"]);
                reader.Dispose();

                //Update status.
                if (appStatus == 5)
                    command.CommandText = "UPDATE Application SET AppStatus = 4 WHERE AppID = " + Context.Request["AppID"]; //Incomplete application being resubmitted. Send back to ESO/SO.
                else
                    command.CommandText = "UPDATE Application SET AppStatus = 1 WHERE AppID = " + Context.Request["AppID"]; //Send to HOS.
                int status = command.ExecuteNonQuery();
                if (status != 1)
                {
                    divMsg.InnerText = "Error submitting!";
                }
                else
                {
                    //Email PI, Contact, CI & HOS.
                    string msg = "Application for project '" + title + "' has been submitted for processing." + Environment.NewLine +
                                 "Should you have any queries about the consideration of your project please contact the Ethics Support Officer or the Ethics Office.";
                    string subject = "Ethics application '" + title + "' submitted";

                    //Get PI information.
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

                        //Get Contact person information.
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

                    //Get CI's information.
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
                    if (appStatus != 5) //If new application or a HOS declined application.
                    {
                        reader.Dispose();

                        //Get HOS information.
                        command.CommandText = @"SELECT NameFirst, NameLast, Email FROM Staff WHERE StaffID = " + hosID;
                        reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            //Send email to HOS.
                            msg = "Application for project '" + title + "' has been submitted for processing." + Environment.NewLine +
                                  "As head of department you have been selected to make an endorsement decision for this project." + Environment.NewLine +
                                  "Should you have any queries please contact the Ethics Support Officer or the Ethics Office.";
                            error3 = Notifications.send(reader["Email"].ToString(), "New application submitted", msg);
                        }
                    }
                }
                reader.Dispose();
                command.Dispose();
                mySqlConnection.Close();

                //If any errors occured diaplay them, else return to main.
                if (error != "" || error2 != "" || error3 != "")
                {
                    divMsg2.InnerText = "Failure to send email!" + Environment.NewLine + error;
                    divMsg3.InnerText = "Failure to send email!" + Environment.NewLine + error2;
                    divMsg4.InnerText = "Failure to send email!" + Environment.NewLine + error3;
                }
                else
                {
                    string nextAddr = SharedFunctions.getMainPageAddr(Convert.ToInt32(Context.Request["Type"]), Convert.ToInt32(Context.Request["StaffID"]));
                    Response.Redirect(nextAddr);
                }
            }
            reader.Dispose();
            command.Dispose();
            mySqlConnection.Close();
        }
        else
            divMsg.InnerText = "Please tick the confirm button and select a HOS.";
    }

    //Save user changes back to DB.
    private void save()
    {
        divMsg.InnerText = "";
        divMsg2.InnerText = "";
        divMsg3.InnerText = "";
        divMsg4.InnerText = "";
        MySql.Data.MySqlClient.MySqlConnection mySqlConnection = new Connector().MySQLConnect();
        MySqlCommand command = mySqlConnection.CreateCommand();
        command.CommandText = @"UPDATE Application SET g_HOS_StaffID = " + Convert.ToInt32(sltHOS.Items[sltHOS.SelectedIndex].Value) + " WHERE AppID = " + Context.Request["AppID"];
        int status = command.ExecuteNonQuery();
        if (status != 1)
        {
            divMsg.InnerText = "Error saving!";
        }
        else
        {
            //Check validity of section.
            validate(SharedFunctions.validateS7(Convert.ToInt32(Context.Request["AppID"])));
        }
        command.Dispose();
        mySqlConnection.Close();
    }

    //Validate section and display asterix's for unanswered questions.
    private void validate(bool[] valid)
    {
        if (valid[0])
            btnS7.Style.Add("color", "Green");
        else
            btnS7.Style.Add("color", "Red");
        mand1.Visible = !valid[1];
        mand2.Visible = !valid[2];
    }

    //Open printable version of application in new tab.
    //Based on http://stackoverflow.com/questions/10493901/how-to-open-a-page-in-new-tab-on-button-click-in-asp-net
    public void btnPrint_ServerClick(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(
        this.GetType(), "OpenWindow", "window.open('" + "EthicsPrintApp.aspx?AppID=" + Request["AppID"].ToString() + "','_newtab');", true);
    }
}