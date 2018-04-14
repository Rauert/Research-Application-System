using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Web.UI.HtmlControls;

/**
 * The Head od School main page.
 * Displays a list of submitted applicationthe requiring HOS endorsement.
 */
public partial class EthicsHOSMain : System.Web.UI.Page
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

        //Populate table.
        command.CommandText = @"SELECT Application.AppID, Application.a1_ProjTitle, StatusList.AppStatus, RiskLow_Bool, RiskNonLow_Bool 
                                FROM Application LEFT JOIN StatusList ON Application.AppStatus = StatusList.ID 
                                WHERE g_HOS_StaffID = " + staffID + " AND Application.AppStatus = 1";
        reader = command.ExecuteReader();
        if (reader.HasRows) //Build table entries.
        {
            while (reader.Read()) //Build a table row for each row of query.
            {
                HtmlTableRow row = new HtmlTableRow();

                //Build buttons.
                HtmlInputButton btnView = new HtmlInputButton();
                btnView.ID = "ViewPI" + reader["AppID"].ToString();
                btnView.Value = "View";
                btnView.ServerClick += btnView_ServerClick;

                HtmlInputButton btnEndorse = new HtmlInputButton();
                btnEndorse.ID = "Endorse" + reader["AppID"].ToString();
                btnEndorse.Value = "Endorse";
                btnEndorse.ServerClick += btnEndorse_ServerClick;

                row.Cells.Add(HTMLFactory.buildCell("50", "left", reader["AppID"].ToString()));
                row.Cells.Add(HTMLFactory.buildCell("400", "left", reader["a1_ProjTitle"].ToString()));
                row.Cells.Add(HTMLFactory.buildCell("200", "left", reader["AppStatus"].ToString()));
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
                blnk.ColSpan = 5;
                blnkRow.Cells.Add(blnk);
                tblUnsubmitted.Rows.Add(blnkRow);
            }
        }

        reader.Dispose(); //Cleanup.
        command.Dispose();
        mySqlConnection.Close();
    }

    //Button click handlers, redirect to new page.
    public void btnView_ServerClick(object sender, EventArgs e)
    {
        HtmlInputButton source = (HtmlInputButton)sender;
        Response.Redirect("EthicsEditAppTriage.aspx?Mode=R&AppID=" + source.ID.Remove(0, 6) + "&StaffID=" + Context.Request["StaffID"] + "&Type=" + Context.Request["Type"]);
    }
    public void btnEndorse_ServerClick(object sender, EventArgs e)
    {
        HtmlInputButton source = (HtmlInputButton)sender;
        Response.Redirect("EthicsHOSEndorse.aspx?AppID=" + source.ID.Remove(0, 7) + "&StaffID=" + Context.Request["StaffID"] + "&Type=" + Context.Request["Type"]);
    }
}