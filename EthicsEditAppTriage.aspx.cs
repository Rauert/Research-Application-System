using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Web.UI.HtmlControls;

/**
 * Section triage Edit/View application page.
 * Displays Section triage data and saves changes made.
 */
public partial class EthicsEditAppTriage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) //Don't rebuild page data on postback.
        {
            MySql.Data.MySqlClient.MySqlConnection mySqlConnection = new Connector().MySQLConnect();
            MySqlCommand command = mySqlConnection.CreateCommand();
            command.CommandText = @"SELECT NS3_3_Yes, NS3_3_No, NS3_5_Yes, NS3_5_No, NS4_1_Yes, NS4_1_No, NS4_34_Yes, NS4_34_No, NS4_5_Yes, NS4_5_No, NS4_7_Yes, NS4_7_No, NS4_6_Yes, NS4_6_No, 
                                    NotLowRisk FROM Application WHERE AppID = " + Context.Request["AppID"];
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows) //Populate page with data.
            {
                reader.Read();
                if (Convert.ToInt32(reader["NS3_3_Yes"]) != 0)
                    intTherapy_0.Checked = true;
                if (Convert.ToInt32(reader["NS3_3_No"]) != 0)
                    intTherapy_1.Checked = true;
                if (Convert.ToInt32(reader["NS3_5_Yes"]) != 0)
                    genetics_0.Checked = true;
                if (Convert.ToInt32(reader["NS3_5_No"]) != 0)
                    genetics_1.Checked = true;
                if (Convert.ToInt32(reader["NS4_1_Yes"]) != 0)
                    pregnancy_0.Checked = true;
                if (Convert.ToInt32(reader["NS4_1_No"]) != 0)
                    pregnancy_1.Checked = true;
                if (Convert.ToInt32(reader["NS4_34_Yes"]) != 0)
                    medConsent_0.Checked = true;
                if (Convert.ToInt32(reader["NS4_34_No"]) != 0)
                    medConsent_1.Checked = true;
                if (Convert.ToInt32(reader["NS4_5_Yes"]) != 0)
                    menDisability_0.Checked = true;
                if (Convert.ToInt32(reader["NS4_5_No"]) != 0)
                    menDisability_1.Checked = true;
                if (Convert.ToInt32(reader["NS4_7_Yes"]) != 0)
                    indigenous_0.Checked = true;
                if (Convert.ToInt32(reader["NS4_7_No"]) != 0)
                    indigenous_1.Checked = true;
                if (Convert.ToInt32(reader["NS4_6_Yes"]) != 0)
                    illegalAct_0.Checked = true;
                if (Convert.ToInt32(reader["NS4_6_No"]) != 0)
                    illegalAct_1.Checked = true;
                txtYesResp.InnerText = reader["NotLowRisk"].ToString();
            }
            else
                divMsg.InnerText = "Application not found";
            reader.Dispose();
            command.Dispose();
            mySqlConnection.Close();

            if (Context.Request["Mode"].Equals("W")) //If write access then unlock.
            {
                btnSave.Visible = true;
                intTherapy_0.Disabled = false;
                intTherapy_1.Disabled = false;
                genetics_0.Disabled = false;
                genetics_1.Disabled = false;
                pregnancy_0.Disabled = false;
                pregnancy_1.Disabled = false;
                medConsent_0.Disabled = false;
                medConsent_1.Disabled = false;
                menDisability_0.Disabled = false;
                menDisability_1.Disabled = false;
                indigenous_0.Disabled = false;
                indigenous_1.Disabled = false;
                illegalAct_0.Disabled = false;
                illegalAct_1.Disabled = false;
                txtYesResp.Disabled = false;
            }

            //Check application validity.
            //Colour the application navigation menus appropriately.
            bool[][] valid = SharedFunctions.validateApplication(Convert.ToInt32(Context.Request["AppID"]));

            validate(valid[0]); //Validate entire section.

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

    //Save the user entered changes back to the database.
    public void save()
    {
        divMsg.InnerText = "";
        MySql.Data.MySqlClient.MySqlConnection mySqlConnection = new Connector().MySQLConnect();
        MySqlCommand command = mySqlConnection.CreateCommand();
        command.CommandText = @"UPDATE Application SET NS3_3_Yes = " + intTherapy_0.Checked + ", NS3_3_No = " + intTherapy_1.Checked + ", NS3_5_Yes = " + genetics_0.Checked + 
                                ", NS3_5_No = " + genetics_1.Checked + ", NS4_1_Yes = " + pregnancy_0.Checked + ", NS4_1_No = " + pregnancy_1.Checked + ", NS4_34_Yes = " + medConsent_0.Checked + 
                                ", NS4_34_No = " + medConsent_1.Checked + ", NS4_5_Yes = " + menDisability_0.Checked + ", NS4_5_No = " + menDisability_1.Checked + ", NS4_7_Yes = " + indigenous_0.Checked + 
                                ", NS4_7_No = " + indigenous_1.Checked + ", NS4_6_Yes = " + illegalAct_0.Checked + ", NS4_6_No = " + illegalAct_1.Checked + ", NotLowRisk = '" + txtYesResp.Value + "' " + 
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
            //Check validity of section.
            validate(SharedFunctions.validateTriage(Convert.ToInt32(Context.Request["AppID"])));
        }
    }

    //Validate section and display asterix's for unanswered questions.
    private void validate(bool[] valid)
    {
        if (valid[0])
            btnTriage.Style.Add("color", "Green");
        else
            btnTriage.Style.Add("color", "Red");
        mand1.Visible = !valid[1];
        mand2.Visible = !valid[2];
        mand3.Visible = !valid[3];
        mand4.Visible = !valid[4];
        mand5.Visible = !valid[5];
        mand6.Visible = !valid[6];
        mand7.Visible = !valid[7];
        mand8.Visible = !valid[8];
    }

    //Open printable version of application in new tab.
    //Based on http://stackoverflow.com/questions/10493901/how-to-open-a-page-in-new-tab-on-button-click-in-asp-net
    public void btnPrint_ServerClick(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(
        this.GetType(), "OpenWindow", "window.open('" + "EthicsPrintApp.aspx?AppID=" + Request["AppID"].ToString() + "','_newtab');", true);
    }
}