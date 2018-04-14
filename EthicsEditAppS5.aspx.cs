using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Web.UI.HtmlControls;

/**
 * Section 5 Edit/View application page.
 * Displays Section 5 data and saves changes made.
 */
public partial class EthicsEditAppS5 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) //Don't rebuild page data on postback.
        {
            MySqlConnection mySqlConnection = new Connector().MySQLConnect();
            MySqlCommand command = mySqlConnection.CreateCommand();
            command.CommandText = @"SELECT e23_ConflictsNo, e23_ConflictsYes, e23_Conflicts FROM Application WHERE AppID = " + Context.Request["AppID"];
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows) //Populate page with data.
            {
                reader.Read();
                ethicConflictInterestRadioNo.Checked = Convert.ToBoolean(reader["e23_ConflictsNo"]);
                ethicConflictInterestRadioYes.Checked = Convert.ToBoolean(reader["e23_ConflictsYes"]);
                txtConflict.InnerText = reader["e23_Conflicts"].ToString();
            }
            else
                Response.Write("Application not found");
            reader.Dispose();
            command.Dispose();
            mySqlConnection.Close();

            if (Context.Request["Mode"].Equals("W")) //If write access then unlock.
            {
                btnSave.Visible = true;
                ethicConflictInterestRadioNo.Disabled = false;
                ethicConflictInterestRadioYes.Disabled = false;
                txtConflict.Disabled = false;
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
            {
                btnS5.Style.Add("color", "Red");
                mand1.Visible = true; //Show mandatory label.
            }
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
        string nextAddr = SharedFunctions.getEditAppPageAddr(source.ID[source.ID.Length - 1], Context.Request["Mode"], Convert.ToInt32(Context.Request["AppID"]), 
                                                             Convert.ToInt32(Context.Request["StaffID"]), Convert.ToInt32(Context.Request["Type"]));
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
        MySql.Data.MySqlClient.MySqlConnection mySqlConnection = new Connector().MySQLConnect();
        MySqlCommand command = mySqlConnection.CreateCommand();
        command.CommandText = @"UPDATE Application SET e23_ConflictsNo = " + ethicConflictInterestRadioNo.Checked + ", e23_ConflictsYes = " + ethicConflictInterestRadioYes.Checked + 
                               ", e23_Conflicts = '" + txtConflict.Value + "' WHERE AppID = " + Context.Request["AppID"];
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
            bool[] valid = SharedFunctions.validateS5(Convert.ToInt32(Context.Request["AppID"]));
            if (valid[0])
            {
                btnS5.Style.Add("color", "Green");
                mand1.Visible = false; //Hide mandatory label.
            }
            else
            {
                btnS5.Style.Add("color", "Red");
                mand1.Visible = true; //Show mandatory label.
            }
        }
    }

    //Open printable version of application in new tab.
    //Based on http://stackoverflow.com/questions/10493901/how-to-open-a-page-in-new-tab-on-button-click-in-asp-net
    public void btnPrint_ServerClick(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(
        this.GetType(), "OpenWindow", "window.open('" + "EthicsPrintApp.aspx?AppID=" + Request["AppID"].ToString() + "','_newtab');", true);
    }
}