using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using MySql.Data.MySqlClient;

/**
 * The Ethics Officer main page.
 * Displays a sortable list of the non low risk applications needing review/approval.
 */
public partial class EthicsEOMain : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MySqlConnection mySqlConnection = new Connector().MySQLConnect();
        MySqlCommand command = mySqlConnection.CreateCommand();
        int staffID = Convert.ToInt32(Context.Request["StaffID"]);

        //Populate labels.
        command.CommandText = "SELECT * FROM Staff LEFT JOIN AccountTypeList ON Staff.AccountType = AccountTypeList.ID WHERE StaffID = " + staffID;
        MySqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            reader.Read();
            lblWelcome.InnerText = "Welcome " + reader["NameFirst"].ToString() + " " + reader["NameLast"].ToString();
            LblAccnt.InnerText = "Account Type: " + reader["AccntType"].ToString();
        }
        else
            divMsg.InnerText = "Error: Could not find staff member in database";
        reader.Dispose();
        command.Dispose();
        mySqlConnection.Close();

        //Populate table.
        buildSubmitted();
    }

    //Deletes existing table entries and rebuilds.
    private void buildSubmitted()
    {
        if (tblSubmitted.Rows.Count > 1) //If rows exist, delete.
        {
            for (int i = tblSubmitted.Rows.Count - 1; i > 0; i--)
            {
                tblSubmitted.Rows.RemoveAt(i);
            }
        }
        MySqlConnection mySqlConnection = new Connector().MySQLConnect();
        MySqlCommand command = mySqlConnection.CreateCommand();
        command.CommandText = @"SELECT Application.AppID, Application.a1_ProjTitle, Application.DateEndorsed, RiskLow_Bool, RiskNonLow_Bool, SchoolList.School, StatusList.AppStatus 
                                FROM Application LEFT JOIN Staff ON Application.a4_InvestStaffID = Staff.StaffID LEFT JOIN SchoolList ON Staff.School = SchoolList.ID 
                                LEFT JOIN StatusList ON Application.AppStatus = StatusList.ID WHERE (Application.AppStatus = 4 OR Application.AppStatus = 6) AND RiskNonLow_Bool = 1 ORDER BY " +
                                sltSort.Items[sltSort.SelectedIndex].Value;
        MySqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows) //Build table entries.
        {
            while (reader.Read()) //Build a table row for each row of query.
            {
                HtmlTableRow row = new HtmlTableRow();

                HtmlInputButton btnView = new HtmlInputButton();
                btnView.ID = "ViewPI" + reader["AppID"].ToString();
                btnView.Value = "View";
                btnView.ServerClick += btnView_ServerClick;

                HtmlInputButton btnReview = new HtmlInputButton();
                btnReview.ID = "Review" + reader["AppID"].ToString();
                btnReview.Value = "Email";
                btnReview.ServerClick += btnEndorseReviewer_ServerClick;

                HtmlInputButton btnEndorse = new HtmlInputButton();
                btnEndorse.ID = "Endorse" + reader["AppID"].ToString();
                btnEndorse.Value = "Select";
                btnEndorse.ServerClick += btnEndorseLowRisk_ServerClick;

                row.Cells.Add(HTMLFactory.buildCell("50", "left", reader["AppID"].ToString()));
                row.Cells.Add(HTMLFactory.buildCell("400", "left", reader["a1_ProjTitle"].ToString()));
                row.Cells.Add(HTMLFactory.buildCell("200", "left", reader["AppStatus"].ToString()));
                row.Cells.Add(HTMLFactory.buildCell("200", "left", reader["School"].ToString()));
                row.Cells.Add(HTMLFactory.buildCell("97", "left", reader["DateEndorsed"].ToString()));
                row.Cells.Add(HTMLFactory.buildCell("97", "center", btnView));
                row.Cells.Add(HTMLFactory.buildCell("97", "center", btnReview));
                row.Cells.Add(HTMLFactory.buildCell("97", "center", btnEndorse));

                tblSubmitted.Rows.Add(row);
            }
        }
        else //Else display none.
        {
            if (tblSubmitted.Rows.Count != 0)
            {
                HtmlTableRow blnkRow = new HtmlTableRow();
                HtmlTableCell blnk = new HtmlTableCell();
                blnk.InnerText = "None";
                blnk.ColSpan = 8;
                blnkRow.Cells.Add(blnk);
                tblSubmitted.Rows.Add(blnkRow);
            }
        }
        divMsg.InnerText = ""; //Reset notification.
        reader.Dispose(); //Cleanup.
        command.Dispose();
        mySqlConnection.Close();
    }

    //Rebuild table with new sort criteria.
    public void btnSort_ServerClick(object sender, EventArgs e)
    {
        buildSubmitted();
    }

    //Emails notification to HREC.
    public void btnEndorseReviewer_ServerClick(object sender, EventArgs e)
    {
        divMsg.InnerText = "";
        string error = "";
        HtmlInputButton source = (HtmlInputButton)sender;
        MySqlConnection mySqlConnection = new Connector().MySQLConnect();
        MySqlCommand command = mySqlConnection.CreateCommand();
        command.CommandText = @"SELECT a1_ProjTitle FROM Application WHERE AppID = " + source.ID.Remove(0, 6);
        MySqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            reader.Read();
            string title = reader["a1_ProjTitle"].ToString();
            string msg = "Project '" + title + "' has been assessed as non low risk and requires HREC review." + Environment.NewLine + 
                         "Please find application at the following address:" + Environment.NewLine + Environment.NewLine +
                         "http://curtinethics-001-site1.smarterasp.net/EthicsPrintApp.aspx?AppID=" + source.ID.Remove(0, 6) + Environment.NewLine + Environment.NewLine + 
                         "Should you have any queries please contact the Ethics Support Officer or the Ethics Office.";
            reader.Dispose();

            //Send email to HREC.
            error = Notifications.send("hrec@mailinator.com", "Review required for " + title, msg);
            if (error == "")
                divMsg.InnerText = "Email sent to HREC for '" + title + "'"; //Set notification.
            else
                divMsg.InnerText = error;
        }
    }

    //Button click handlers, redirect to new page.
    public void btnView_ServerClick(object sender, EventArgs e)
    {
        HtmlInputButton source = (HtmlInputButton)sender;
        Response.Redirect("EthicsEditAppTriage.aspx?Mode=R&AppID=" + source.ID.Remove(0, 6) + "&StaffID=" + Context.Request["StaffID"] + "&Type=" + Context.Request["Type"]);
    }
    public void btnEndorseLowRisk_ServerClick(object sender, EventArgs e)
    {
        HtmlInputButton source = (HtmlInputButton)sender;
        Response.Redirect("EthicsESOEOEndorseApp.aspx?AppID=" + source.ID.Remove(0, 7) + "&StaffID=" + Context.Request["StaffID"] + "&Type=" + Context.Request["Type"]);
    }
}