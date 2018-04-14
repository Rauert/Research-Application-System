using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using MySql.Data.MySqlClient;

/**
 * The Admin main page.
 * Displays a sortable list of all applications in the system.
 */
public partial class EthicsAdminMain : System.Web.UI.Page
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

    //Deletes existing table entries and rebuilds on postback.
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
                                LEFT JOIN StatusList ON Application.AppStatus = StatusList.ID ORDER BY " + sltSort.Items[sltSort.SelectedIndex].Value; //Uses selected sort value.
        MySqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows) //Build table entries.
        {
            while (reader.Read()) //Build a table row for each row of query.
            {
                HtmlTableRow row = new HtmlTableRow();

                row.Cells.Add(HTMLFactory.buildCell("50", "left", reader["AppID"].ToString()));
                row.Cells.Add(HTMLFactory.buildCell("400", "left", reader["a1_ProjTitle"].ToString()));
                row.Cells.Add(HTMLFactory.buildCell("200", "left", reader["AppStatus"].ToString()));
                row.Cells.Add(HTMLFactory.buildCell("200", "left", reader["School"].ToString()));

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
                blnk.ColSpan = 4;
                blnkRow.Cells.Add(blnk);
                tblSubmitted.Rows.Add(blnkRow);
            }
        }
        reader.Dispose(); //Cleanup.
        command.Dispose();
        mySqlConnection.Close();
    }

    //Rebuild table whith selected sort value.
    public void btnSort_ServerClick(object sender, EventArgs e)
    {
        buildSubmitted();
    }
}