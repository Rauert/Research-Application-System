using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Web.UI.HtmlControls;

/**
 * Section 3 Edit/View application page.
 * Displays Section 3 data and saves changes made.
 */
public partial class EthicsEditAppS3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) //Don't rebuild page data on postback.
        {
            MySql.Data.MySqlClient.MySqlConnection mySqlConnection = new Connector().MySQLConnect();
            MySqlCommand command = mySqlConnection.CreateCommand();

            //Populate page with data from Application table.
            command.CommandText = @"SELECT c13_ClinicalNo, c13_ClinicalYes, c14_HealthNo, c14_HealthYes, c14_Health, c15_GeneticsNo, c15_GeneticsYes, c15_Genetics 
                                    FROM Application WHERE AppID = " + Context.Request["AppID"];
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows) //Populate page with data.
            {
                reader.Read();
                clinicalRadioNo.Checked = Convert.ToBoolean(reader["c13_ClinicalNo"]);
                clinicalRadioYes.Checked = Convert.ToBoolean(reader["c13_ClinicalYes"]);
                healthInfoRadioNo.Checked = Convert.ToBoolean(reader["c14_HealthNo"]);
                healthInfoRadioYes.Checked = Convert.ToBoolean(reader["c14_HealthYes"]);
                healthInfoText.InnerText = reader["c14_Health"].ToString();
                humanGenRadioNo.Checked = Convert.ToBoolean(reader["c15_GeneticsNo"]);
                humanGenRadioYes.Checked = Convert.ToBoolean(reader["c15_GeneticsYes"]);
                humanGenText.InnerText = reader["c15_Genetics"].ToString();
                reader.Dispose();

                //Populate page with data from Clinical Trial table (If data exists).
                command.CommandText = @"SELECT Count(*) FROM c13_ClinicalTrial WHERE AppID = " + Context.Request["AppID"];
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    if (Convert.ToInt32(reader[0]) == 1) //If data exists.
                    {
                        reader.Dispose();
                        command.CommandText = @"SELECT c13a_PlaceboNo, c13a_PlaceboYes, c13a_Placebo, c13b_RegisteredNo, c13b_RegisteredYes, c13b_Registered, c13c_SafeConductNo,
                                                c13c_SafeConductYes, c13c_SafeConduct, c13d_ContAccessNo, c13d_ContAccessYes FROM c13_ClinicalTrial 
                                                WHERE AppID = " + Context.Request["AppID"];
                        reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            placeboRadioNo.Checked = Convert.ToBoolean(reader["c13a_PlaceboNo"]);
                            placeboRadioYes.Checked = Convert.ToBoolean(reader["c13a_PlaceboYes"]);
                            placeboText.InnerText = reader["c13a_Placebo"].ToString();
                            trialRegRadioNo.Checked = Convert.ToBoolean(reader["c13b_RegisteredNo"]);
                            trialRegRadioYes.Checked = Convert.ToBoolean(reader["c13b_RegisteredYes"]);
                            trialRegDetails.InnerText = reader["c13b_Registered"].ToString();
                            trialResRadioNo.Checked = Convert.ToBoolean(reader["c13c_SafeConductNo"]);
                            trialResRadioYes.Checked = Convert.ToBoolean(reader["c13c_SafeConductYes"]);
                            trialResText.InnerText = reader["c13c_SafeConduct"].ToString();
                            trialTreatRadioNo.Checked = Convert.ToBoolean(reader["c13d_ContAccessNo"]);
                            trialTreatRadioYes.Checked = Convert.ToBoolean(reader["c13d_ContAccessYes"]);
                        }
                    }
                }
            }
            else
                divMsg.InnerText = "Application not found";
            reader.Dispose();
            command.Dispose();
            mySqlConnection.Close();

            if (Context.Request["Mode"].Equals("W")) //If write access then unlock.
            {
                btnSave.Visible = true;
                clinicalRadioNo.Disabled = false;
                clinicalRadioYes.Disabled = false;
                placeboRadioNo.Disabled = false;
                placeboRadioYes.Disabled = false;
                placeboText.Disabled = false;
                trialRegRadioNo.Disabled = false;
                trialRegRadioYes.Disabled = false;
                trialRegDetails.Disabled = false;
                trialResRadioNo.Disabled = false;
                trialResRadioYes.Disabled = false;
                trialResText.Disabled = false;
                trialTreatRadioNo.Disabled = false;
                trialTreatRadioYes.Disabled = false;
                healthInfoRadioNo.Disabled = false;
                healthInfoRadioYes.Disabled = false;
                healthInfoText.Disabled = false;
                humanGenRadioNo.Disabled = false;
                humanGenRadioYes.Disabled = false;
                humanGenText.Disabled = false;
            }

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

            validate(valid[3]); //Validate entire section.

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
            if (valid[7][0])
                btnS7.Style.Add("color", "Green");
            else
                btnS7.Style.Add("color", "Red");
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

    //Save user changes back to DB.
    private void save()
    {
        divMsg.InnerText = "";
        MySql.Data.MySqlClient.MySqlConnection mySqlConnection = new Connector().MySQLConnect();
        MySqlCommand command = mySqlConnection.CreateCommand();
        command.CommandText = @"UPDATE Application SET c13_ClinicalNo = " + clinicalRadioNo.Checked + ", c13_ClinicalYes = " + clinicalRadioYes.Checked + ", c14_HealthNo = " + healthInfoRadioNo.Checked + 
                               ", c14_HealthYes = " + healthInfoRadioYes.Checked + ", c14_Health = '" + healthInfoText.Value + "', c15_GeneticsNo = " + humanGenRadioNo.Checked + ", c15_GeneticsYes = " + 
                               humanGenRadioYes.Checked + ", c15_Genetics = '" + humanGenText.Value + "'" +
                               "WHERE AppID = " + Context.Request["AppID"];
        int status1 = command.ExecuteNonQuery();

        command.CommandText = @"SELECT Count(*) FROM c13_ClinicalTrial WHERE AppID = " + Context.Request["AppID"];
        MySqlDataReader reader = command.ExecuteReader();

        //Check if a clinical trial.
        if (clinicalRadioYes.Checked)
        {
            if (reader.HasRows)
            {
                reader.Read();
                if (Convert.ToInt32(reader[0]) == 0) //If no data exists append row to clinical trial table.
                {
                    reader.Dispose();
                    command.CommandText = @"INSERT INTO c13_ClinicalTrial (AppID, c13a_PlaceboNo, c13a_PlaceboYes, c13a_Placebo, c13b_RegisteredNo, c13b_RegisteredYes, c13b_Registered, c13c_SafeConductNo,
                                            c13c_SafeConductYes, c13c_SafeConduct, c13d_ContAccessNo, c13d_ContAccessYes) VALUES (" + Context.Request["AppID"] + ", " + placeboRadioNo.Checked + ", " + 
                                            placeboRadioYes.Checked + ", '" + placeboText.Value + "', " + trialRegRadioNo.Checked + ", " + trialRegRadioYes.Checked + ", '" + trialRegDetails.Value + "', " + 
                                            trialResRadioNo.Checked + ", " + trialResRadioYes.Checked + ", '" + trialResText.Value + "', " + trialTreatRadioNo.Checked + ", " + trialTreatRadioYes.Checked + ")";
                    int status2 = command.ExecuteNonQuery();
                    if (status2 != 1)
                    {
                        divMsg.InnerText = "Error saving to Clinical Trial table!";
                    }
                }
                else //Update existing data.
                {
                    reader.Dispose();
                    command.CommandText = @"UPDATE c13_ClinicalTrial SET c13a_PlaceboNo = " + placeboRadioNo.Checked + ", c13a_PlaceboYes = " +
                                            placeboRadioYes.Checked + ", c13a_Placebo = '" + placeboText.Value + "', c13b_RegisteredNo = " + trialRegRadioNo.Checked + ", c13b_RegisteredYes = " + trialRegRadioYes.Checked + 
                                            ", c13b_Registered = '" + trialRegDetails.Value + "', c13c_SafeConductNo = " + trialResRadioNo.Checked + ", c13c_SafeConductYes = " + trialResRadioYes.Checked + 
                                            ", c13c_SafeConduct = '" + trialResText.Value + "', c13d_ContAccessNo = " + trialTreatRadioNo.Checked + ", c13d_ContAccessYes = " + trialTreatRadioYes.Checked + 
                                            " WHERE AppID = " + Context.Request["AppID"];
                    int status3 = command.ExecuteNonQuery();
                    if (status3 != 1)
                    {
                        divMsg.InnerText = "Error saving to Clinical Trial table!";
                    }
                }
            }
        }
        else if (clinicalRadioNo.Checked)
        {
            placeboRadioNo.Checked = false; //Delete does not reset already entered values. So set to null.
            placeboRadioYes.Checked = false;
            placeboText.InnerText = "";
            trialRegRadioNo.Checked = false;
            trialRegRadioYes.Checked = false;
            trialRegDetails.InnerText = "";
            trialResRadioNo.Checked = false;
            trialResRadioYes.Checked = false;
            trialResText.InnerText = "";
            trialTreatRadioNo.Checked = false;
            trialTreatRadioYes.Checked = false;
            if (reader.HasRows)
            {
                reader.Read();
                if (Convert.ToInt32(reader[0]) == 1) //If no data exists delete it.
                {
                    reader.Dispose();
                    command.CommandText = @"DELETE FROM c13_ClinicalTrial WHERE AppID = " + Context.Request["AppID"];
                    int status4 = command.ExecuteNonQuery();
                    if (status4 != 1)
                    {
                        divMsg.InnerText = "Error deleting entry in Clinical Trial table!";
                    }
                }
            }
        }

        if (status1 != 1)
        {
            divMsg.InnerText = "Error saving!";
        }
        else
        {

            validate(SharedFunctions.validateS3(Convert.ToInt32(Context.Request["AppID"])));
        }

        reader.Dispose();
        command.Dispose();
        mySqlConnection.Close();
    }

    //Validate section and display asterix's for unanswered questions.
    private void validate(bool[] valid)
    {
        if (valid[0])
            btnS3.Style.Add("color", "Green");
        else
            btnS3.Style.Add("color", "Red");
        mand13.Visible = !valid[1];
        mand13a.Visible = !valid[2];
        mand13b.Visible = !valid[3];
        mand13c.Visible = !valid[4];
        mand13d.Visible = !valid[5];
        mand14.Visible = !valid[6];
        mand15.Visible = !valid[7];
    }

    //Open printable version of application in new tab.
    //Based on http://stackoverflow.com/questions/10493901/how-to-open-a-page-in-new-tab-on-button-click-in-asp-net
    public void btnPrint_ServerClick(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(
        this.GetType(), "OpenWindow", "window.open('" + "EthicsPrintApp.aspx?AppID=" + Request["AppID"].ToString() + "','_newtab');", true);
    }
}