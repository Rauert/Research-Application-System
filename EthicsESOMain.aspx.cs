using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using MySql.Data.MySqlClient;

/**
 * The Ethics Support Officer main page.
 * Displays two sortable lists of the HOS endorsed applications needing risk assessment and the low risk applications needing review/approval.
 */
public partial class EthicsESOMain : System.Web.UI.Page
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

        //Populate unsubmitted table.
        buildUnsubmitted();

        //Populate submitted table.
        buildSubmitted();
    }

    //Deletes existing table entries and rebuilds.
    private void buildUnsubmitted()
    {
        if (tblUnsubmitted.Rows.Count > 1) //If rows exist, delete.
        {
            for (int i = tblUnsubmitted.Rows.Count - 1; i > 0; i--)
            {
                tblUnsubmitted.Rows.RemoveAt(i);
            }
        }
        MySqlConnection mySqlConnection = new Connector().MySQLConnect();
        MySqlCommand command = mySqlConnection.CreateCommand();
        command.CommandText = @"SELECT Application.AppID, Application.a1_ProjTitle, Application.DateEndorsed, RiskLow_Bool, RiskNonLow_Bool, SchoolList.School, StatusList.AppStatus 
                                FROM Application LEFT JOIN Staff ON Application.a4_InvestStaffID = Staff.StaffID LEFT JOIN SchoolList ON Staff.School = SchoolList.ID 
                                LEFT JOIN StatusList ON Application.AppStatus = StatusList.ID WHERE Application.AppStatus = 3 
                                ORDER BY " + sltSort.Items[sltSort.SelectedIndex].Value;
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

                HtmlInputButton btnEndorse = new HtmlInputButton();
                btnEndorse.ID = "Risk" + reader["AppID"].ToString();
                btnEndorse.Value = "Select";
                btnEndorse.ServerClick += btnRisk_ServerClick;
                
                row.Cells.Add(HTMLFactory.buildCell("50", "left", reader["AppID"].ToString()));
                row.Cells.Add(HTMLFactory.buildCell("400", "left", reader["a1_ProjTitle"].ToString()));
                row.Cells.Add(HTMLFactory.buildCell("200", "left", reader["AppStatus"].ToString()));
                row.Cells.Add(HTMLFactory.buildCell("200", "left", reader["School"].ToString()));
                row.Cells.Add(HTMLFactory.buildCell("97", "left", reader["DateEndorsed"].ToString()));
                row.Cells.Add(HTMLFactory.buildCell("97", "center", btnView));
                row.Cells.Add(HTMLFactory.buildCell("97", "center", btnEndorse));

                tblUnsubmitted.Rows.Add(row);
            }
        }
        else //Else display none.
        {
            if (tblUnsubmitted.Rows.Count != 0)
            {
                HtmlTableRow blnkRow = new HtmlTableRow();
                HtmlTableCell blnk = new HtmlTableCell();
                blnk.InnerText = "None";
                blnk.ColSpan = 7;
                blnkRow.Cells.Add(blnk);
                tblUnsubmitted.Rows.Add(blnkRow);
            }
        }
        reader.Dispose(); //Cleanup.
        command.Dispose();
        mySqlConnection.Close();
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
                                LEFT JOIN StatusList ON Application.AppStatus = StatusList.ID WHERE (Application.AppStatus = 4 OR Application.AppStatus = 6) AND RiskLow_Bool = 1 ORDER BY " + 
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
                btnReview.Value = "Select";
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
        reader.Dispose(); //Cleanup.
        command.Dispose();
        mySqlConnection.Close();
    }

    public void btnSort_ServerClick(object sender, EventArgs e)
    {
        buildUnsubmitted();
        buildSubmitted();
    }

    //Button click handlers, redirect to new page.
    public void btnView_ServerClick(object sender, EventArgs e)
    {
        HtmlInputButton source = (HtmlInputButton)sender;
        Response.Redirect("EthicsEditAppTriage.aspx?Mode=R&AppID=" + source.ID.Remove(0, 6) + "&StaffID=" + Context.Request["StaffID"] + "&Type=" + Context.Request["Type"]);
    }
    public void btnRisk_ServerClick(object sender, EventArgs e)
    {
        HtmlInputButton source = (HtmlInputButton)sender;
        Response.Redirect("EthicsESOEndorseRisk.aspx?AppID=" + source.ID.Remove(0, 4) + "&StaffID=" + Context.Request["StaffID"] + "&Type=" + Context.Request["Type"]);
    }
    public void btnEndorseReviewer_ServerClick(object sender, EventArgs e)
    {
        HtmlInputButton source = (HtmlInputButton)sender;
        Response.Redirect("EthicsESOEndorseReviewer.aspx?AppID=" + source.ID.Remove(0, 6) + "&StaffID=" + Context.Request["StaffID"] + "&Type=" + Context.Request["Type"]);
    }
    public void btnEndorseLowRisk_ServerClick(object sender, EventArgs e)
    {
        HtmlInputButton source = (HtmlInputButton)sender;
        Response.Redirect("EthicsESOEOEndorseApp.aspx?AppID=" + source.ID.Remove(0, 7) + "&StaffID=" + Context.Request["StaffID"] + "&Type=" + Context.Request["Type"]);
    }
}