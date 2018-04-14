using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Web.UI.HtmlControls;

/**
 * Section 2 Edit/View application page.
 * Displays Section 2 data and saves changes made.
 */
public partial class EthicsEditAppS2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) //Don't rebuild page data on postback.
        {
            MySqlConnection mySqlConnection = new Connector().MySQLConnect();
            MySqlCommand command = mySqlConnection.CreateCommand();
            command.CommandText = @"SELECT b7_PotRisk, b8_RiskMan, b9_FinanceNo, b9_FinanceYes, b9_Finance, b10_DB, b10_SocialMed, b10_ClassRm, b10_SnowRec, b10_Print, b10_Radio, b10_Other, b10_Descr, 
                                    b10_DBChk, b10_SocialMedChk, b10_ClassChk, b10_SnowRecChk, b10_PrintChk, b10_RadioChk, b10_OtherChk, b11_ConsentNo, b11_ConsentYes, b11_Consent, b12_DeceptionNo, 
                                    b12_DeceptionYes, b12_Deception FROM Application WHERE AppID = " + Context.Request["AppID"];
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows) //Populate page with data.
            {
                reader.Read();
                potHarmText.InnerText = reader["b7_PotRisk"].ToString();
                riskManText.InnerText = reader["b8_RiskMan"].ToString();
                incentiveRadioNo.Checked = Convert.ToBoolean(reader["b9_FinanceNo"]);
                incentiveRadioYes.Checked = Convert.ToBoolean(reader["b9_FinanceYes"]);
                financeText.InnerText = reader["b9_Finance"].ToString();
                database.Checked = Convert.ToBoolean(reader["b10_DBChk"]);
                databaseText.InnerText = reader["b10_DB"].ToString();
                wordMouth.Checked = Convert.ToBoolean(reader["b10_SnowRecChk"]);
                wordMouthText.InnerText = reader["b10_SnowRec"].ToString();
                socialMedia.Checked = Convert.ToBoolean(reader["b10_SocialMedChk"]);
                socialMediaText.InnerText = reader["b10_SocialMed"].ToString();
                printMedia.Checked = Convert.ToBoolean(reader["b10_PrintChk"]);
                printMediaText.InnerText = reader["b10_Print"].ToString();
                commGroups.Checked = Convert.ToBoolean(reader["b10_ClassChk"]);
                commGroupsText.InnerText = reader["b10_ClassRm"].ToString();
                radioTV.Checked = Convert.ToBoolean(reader["b10_RadioChk"]);
                radioTVText.InnerText = reader["b10_Radio"].ToString();
                recruitOther.Checked = Convert.ToBoolean(reader["b10_OtherChk"]);
                recruitOtherText.InnerText = reader["b10_Other"].ToString();
                recruitTxt.InnerText = reader["b10_Descr"].ToString();
                consentRadioNo.Checked = Convert.ToBoolean(reader["b11_ConsentNo"]);
                consentRadioYes.Checked = Convert.ToBoolean(reader["b11_ConsentYes"]);
                consentText.InnerText = reader["b11_Consent"].ToString();
                deceptionRadioNo.Checked = Convert.ToBoolean(reader["b12_DeceptionNo"]);
                deceptionRadioYes.Checked = Convert.ToBoolean(reader["b12_DeceptionYes"]);
                deceptionText.InnerText = reader["b12_Deception"].ToString();
            }
            else
                divMsg.InnerText = "Application not found";
            reader.Dispose();
            command.Dispose();
            mySqlConnection.Close();

            if (Context.Request["Mode"].Equals("W")) //If write access then unlock.
            {
                btnSave.Visible = true;
                potHarmText.Disabled = false;
                riskManText.Disabled = false;
                incentiveRadioNo.Disabled = false;
                incentiveRadioYes.Disabled = false;
                financeText.Disabled = false;
                database.Disabled = false;
                databaseText.Disabled = false;
                wordMouth.Disabled = false;
                wordMouthText.Disabled = false;
                socialMedia.Disabled = false;
                socialMediaText.Disabled = false;
                printMedia.Disabled = false;
                printMediaText.Disabled = false;
                commGroups.Disabled = false;
                commGroupsText.Disabled = false;
                radioTV.Disabled = false;
                radioTVText.Disabled = false;
                recruitOther.Disabled = false;
                recruitOtherText.Disabled = false;
                recruitTxt.Disabled = false;
                consentRadioNo.Disabled = false;
                consentRadioYes.Disabled = false;
                consentText.Disabled = false;
                deceptionRadioNo.Disabled = false;
                deceptionRadioYes.Disabled = false;
                deceptionText.Disabled = false;
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

            validate(valid[2]); //Validate entire section.

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
        command.CommandText = @"UPDATE Application SET b7_PotRisk = '" + potHarmText.Value + "', b8_RiskMan = '" + riskManText.Value + "', b9_FinanceNo = " + incentiveRadioNo.Checked + ", b9_FinanceYes = " + 
                                incentiveRadioYes.Checked + ", b9_Finance = '" + financeText.Value + "', b10_DBChk = " + database.Checked + ", b10_DB = '" + databaseText.Value + "', b10_SnowRecChk = " + wordMouth.Checked + 
                                ", b10_SnowRec = '" + wordMouthText.Value + "', b10_SocialMedChk = " + socialMedia.Checked + ", b10_SocialMed = '" + socialMediaText.Value + "', b10_PrintChk = " + printMedia.Checked + 
                                ", b10_Print = '" + printMediaText.Value + "', b10_ClassChk = " + commGroups.Checked + ", b10_ClassRm = '" + commGroupsText.Value + "', b10_RadioChk = " + radioTV.Checked + 
                                ", b10_Radio = '" + radioTVText.Value + "', b10_OtherChk = " + recruitOther.Checked + ", b10_Other = '" + recruitOtherText.Value + "', b10_Descr = '" + recruitTxt.Value + 
                                "', b11_ConsentNo = " + consentRadioNo.Checked + ", b11_ConsentYes = " + consentRadioYes.Checked + ", b11_Consent = '" + consentText.Value + "', b12_DeceptionNo = " + 
                                deceptionRadioNo.Checked + ", b12_DeceptionYes = " + deceptionRadioYes.Checked + ", b12_Deception = '" + deceptionText.Value + "'" +
                                "WHERE AppID = " + Context.Request["AppID"];

        int status = command.ExecuteNonQuery();
        command.Dispose();
        mySqlConnection.Close();
        if (status != 1)
        {
            divMsg.InnerText = "Error saving!";
        }
        else
        {
            //Check if section is now valid.
            validate(SharedFunctions.validateS2(Convert.ToInt32(Context.Request["AppID"])));
        }
    }

    //Validate section and display asterix's for unanswered questions.
    private void validate(bool[] valid)
    {
        if (valid[0])
            btnS2.Style.Add("color", "Green");
        else
            btnS2.Style.Add("color", "Red");
        mand7.Visible = !valid[1];
        mand8.Visible = !valid[2];
        mand9.Visible = !valid[3];
        mand10.Visible = !valid[4];
        mand11.Visible = !valid[5];
        mand12.Visible = !valid[6];
    }

    //Open printable version of application in new tab.
    //Based on http://stackoverflow.com/questions/10493901/how-to-open-a-page-in-new-tab-on-button-click-in-asp-net
    public void btnPrint_ServerClick(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(
        this.GetType(), "OpenWindow", "window.open('" + "EthicsPrintApp.aspx?AppID=" + Request["AppID"].ToString() + "','_newtab');", true);
    }
}