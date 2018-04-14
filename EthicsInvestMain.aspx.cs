using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;

/**
 * The Investigator main page.
 * Displays four lists. One for unsubmitted applications were the investigator is lead, one for submitted applications were the investigator is lead,
 *                      one for unsubmitted applications were the investigator is not lead and one for submitted applications were the investigator is not lead,
 */
public partial class EthicsInvestMain : System.Web.UI.Page
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

        //Populate Principal investigator unsubmitted table.
        buildPIUnsubmittedTbl(mySqlConnection);

        //Populate Principal investigator submitted table.
        command.CommandText = @"SELECT Application.AppID, Application.a1_ProjTitle, StatusList.ID, StatusList.AppStatus, RiskLow_Bool, RiskNonLow_Bool 
                                FROM Application LEFT JOIN StatusList ON Application.AppStatus = StatusList.ID 
                                WHERE a4_InvestStaffID = " + staffID + " AND Application.AppStatus != 0 AND Application.AppStatus != 2 AND Application.AppStatus != 5";
        reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read()) //Build each row.
            {
                HtmlTableRow row = new HtmlTableRow();
                string riskLevel = "";

                //Build view button.
                HtmlInputButton btnView = new HtmlInputButton();
                btnView.ID = "ViewPI" + reader["AppID"].ToString();
                btnView.Value = "View";
                btnView.ServerClick += btnView_ServerClick;

                //Determine if low or non low risk.
                if (Convert.ToBoolean(reader["RiskLow_Bool"]))
                    riskLevel = "Low risk";
                else if (Convert.ToBoolean(reader["RiskNonLow_Bool"]))
                    riskLevel = "Non low risk";

                //If status is declined add a hyperlink to the comment.
                HtmlTableCell statusCell = HTMLFactory.buildCell("200", "left", reader["AppStatus"].ToString());
                if (Convert.ToInt32(reader["ID"]) == 7) //Declined.
                {
                    statusCell.InnerText += " - ";
                    HyperLink hl = new HyperLink();
                    hl.Text = "Comment";
                    hl.NavigateUrl = "EthicsEditAppS7.aspx?Mode=R&AppID=" + reader["AppID"].ToString() + "&StaffID=" + staffID + "&Type=" + Context.Request["Type"];
                    statusCell.Controls.Add(hl);
                }

                row.Cells.Add(HTMLFactory.buildCell("50", "left", reader["AppID"].ToString()));
                row.Cells.Add(HTMLFactory.buildCell("400", "left", reader["a1_ProjTitle"].ToString()));
                row.Cells.Add(statusCell);
                row.Cells.Add(HTMLFactory.buildCell("120", "left", riskLevel));
                row.Cells.Add(HTMLFactory.buildCell("97", "center", btnView));

                tblPISubmitted.Rows.Add(row);
            }
        }
        else //Display none.
        {
            if (tblPISubmitted.Rows.Count != 0)
            {
                HtmlTableRow blnkRow = new HtmlTableRow();
                HtmlTableCell blnk = new HtmlTableCell();
                blnk.InnerText = "None";
                blnk.ColSpan = 5;
                blnkRow.Cells.Add(blnk);
                tblPISubmitted.Rows.Add(blnkRow);
            }
        }
        reader.Dispose();

        //Populate Co-investigator unsubmitted table.
        buildCIUnsubmittedTbl(mySqlConnection);

        //Populate Co-investigator submitted table.
        command.CommandText = @"SELECT Application.AppID, Application.a1_ProjTitle, StatusList.ID, StatusList.AppStatus, RiskLow_Bool, RiskNonLow_Bool 
                                FROM Application INNER JOIN a5_coinvestigators ON Application.AppID = a5_coinvestigators.AppID LEFT JOIN StatusList ON Application.AppStatus = StatusList.ID 
                                WHERE a5_CoInvestigators.StaffID = " + staffID + " AND Application.AppStatus != 0 AND Application.AppStatus != 2 AND Application.AppStatus != 5";
        reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read()) //Build each row.
            {
                HtmlTableRow row = new HtmlTableRow();
                string riskLevel = "";

                //Build view button.
                HtmlInputButton btnView = new HtmlInputButton();
                btnView.ID = "ViewCI" + reader["AppID"].ToString();
                btnView.Value = "View";
                btnView.ServerClick += btnView_ServerClick;

                //Determine if low or non low risk.
                if (Convert.ToBoolean(reader["RiskLow_Bool"]))
                    riskLevel = "Low risk";
                else if (Convert.ToBoolean(reader["RiskNonLow_Bool"]))
                    riskLevel = "Non low risk";

                //If status is declined add a hyperlink to the comment.
                HtmlTableCell statusCell = HTMLFactory.buildCell("200", "left", reader["AppStatus"].ToString());
                if (Convert.ToInt32(reader["ID"]) == 7) //Declined.
                {
                    statusCell.InnerText += " - ";
                    HyperLink hl = new HyperLink();
                    hl.Text = "Comment";
                    hl.NavigateUrl = "EthicsEditAppS7.aspx?Mode=R&AppID=" + reader["AppID"].ToString() + "&StaffID=" + staffID + "&Type=" + Context.Request["Type"];
                    statusCell.Controls.Add(hl);
                }

                row.Cells.Add(HTMLFactory.buildCell("50", "left", reader["AppID"].ToString()));
                row.Cells.Add(HTMLFactory.buildCell("400", "left", reader["a1_ProjTitle"].ToString()));
                row.Cells.Add(statusCell);
                row.Cells.Add(HTMLFactory.buildCell("120", "left", riskLevel));
                row.Cells.Add(HTMLFactory.buildCell("97", "center", btnView));

                tblCISubmitted.Rows.Add(row);
            }
        }
        else //Display none.
        {
            if (tblCISubmitted.Rows.Count != 0)
            {
                HtmlTableRow blnkRow = new HtmlTableRow();
                HtmlTableCell blnk = new HtmlTableCell();
                blnk.InnerText = "None";
                blnk.ColSpan = 5;
                blnkRow.Cells.Add(blnk);
                tblCISubmitted.Rows.Add(blnkRow);
            }
        }
        reader.Dispose(); //Cleanup.
        command.Dispose();
        mySqlConnection.Close();
    }

    //Build the PI unsubmitted table.
    private void buildPIUnsubmittedTbl(MySqlConnection mySqlConnection)
    {
        if (tblPIUnsubmitted.Rows.Count > 1)
        {
            for (int i = tblPIUnsubmitted.Rows.Count-1; i > 0; i--)
			{
			    tblPIUnsubmitted.Rows.RemoveAt(i);
			}
        }
        MySqlCommand command = mySqlConnection.CreateCommand();
        int staffID = Convert.ToInt32(Context.Request["StaffID"]);
        command.CommandText = @"SELECT Application.AppID, Application.a1_ProjTitle, StatusList.ID, StatusList.AppStatus, Application.Ownership_StaffID, Application.RiskLow_Bool, 
                                Application.RiskNonLow_Bool
                                FROM Application LEFT JOIN StatusList ON Application.AppStatus = StatusList.ID 
                                WHERE a4_InvestStaffID = " + staffID + " AND (Application.AppStatus = 0 OR Application.AppStatus = 2 OR Application.AppStatus = 5)";
        MySqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read()) //Build each row.
            {
                HtmlTableRow row = new HtmlTableRow();
                string riskLevel = "";

                //Build buttons.
                HtmlInputButton btnView = new HtmlInputButton();
                btnView.ID = "ViewCI" + reader["AppID"].ToString();
                btnView.Value = "View";
                btnView.ServerClick += btnView_ServerClick;

                HtmlInputButton btnEdit = new HtmlInputButton();
                btnEdit.ID = "EditCI" + reader["AppID"].ToString();
                btnEdit.Value = "Edit";
                btnEdit.ServerClick += btnEdit_ServerClick;

                Button btnDel = new Button(); //Need to use an ASP button to add the client side confirmation.
                btnDel.ID = "DelCI" + reader["AppID"].ToString();
                btnDel.Text = "Delete";
                btnDel.OnClientClick = "return(beforeDelete( ))"; //Asks user if they are sure they want to delete.
                btnDel.Click += btnDel_ServerClick;

                //Determine if low or non low risk.
                if (Convert.ToBoolean(reader["RiskLow_Bool"]))
                    riskLevel = "Low risk";
                else if (Convert.ToBoolean(reader["RiskNonLow_Bool"]))
                    riskLevel = "Non low risk";

                //If status is HOS declined add a hyperlink to the comment.
                HtmlTableCell statusCell = HTMLFactory.buildCell("200", "left", reader["AppStatus"].ToString());
                if (Convert.ToInt32(reader["ID"]) == 2 || Convert.ToInt32(reader["ID"]) == 5) //HOS declined or incomplete.
                {
                    statusCell.InnerText += " - ";
                    HyperLink hl = new HyperLink();
                    hl.Text = "Comment";
                    hl.NavigateUrl = "EthicsEditAppS7.aspx?Mode=W&AppID=" + reader["AppID"].ToString() + "&StaffID=" + staffID + "&Type=" + Context.Request["Type"];
                    statusCell.Controls.Add(hl);
                }

                row.Cells.Add(HTMLFactory.buildCell("50", "left", reader["AppID"].ToString()));
                row.Cells.Add(HTMLFactory.buildCell("400", "left", reader["a1_ProjTitle"].ToString()));
                row.Cells.Add(statusCell);
                row.Cells.Add(HTMLFactory.buildCell("120", "left", riskLevel));

                //If CI has been given control, disallow editing.
                if (staffID == Convert.ToInt32(reader["Ownership_StaffID"].ToString()))
                {
                    row.Cells.Add(HTMLFactory.buildCell("97", "center", btnEdit));
                    row.Cells.Add(HTMLFactory.buildCell("97", "center", btnDel));
                }
                else
                {
                    row.Cells.Add(HTMLFactory.buildCell("97", "center", btnView));
                    row.Cells.Add(HTMLFactory.buildCell("97", "center", "No control"));
                }

                tblPIUnsubmitted.Rows.Add(row);
            }
        }
        else //Display none.
        {
            if (tblPIUnsubmitted.Rows.Count != 0)
            {
                HtmlTableRow blnkRow = new HtmlTableRow();
                HtmlTableCell blnk = new HtmlTableCell();
                blnk.InnerText = "None";
                blnk.ColSpan = 6;
                blnkRow.Cells.Add(blnk);
                tblPIUnsubmitted.Rows.Add(blnkRow);
            }
        }
        reader.Dispose(); //Cleanup.
        command.Dispose();
    }

    //Build the CI unsubmitted table.
    private void buildCIUnsubmittedTbl(MySqlConnection mySqlConnection)
    {
        if (tblCIUnsubmitted.Rows.Count > 1)
        {
            for (int i = tblCIUnsubmitted.Rows.Count - 1; i > 0; i--)
            {
                tblCIUnsubmitted.Rows.RemoveAt(i);
            }
        }
        MySqlCommand command = mySqlConnection.CreateCommand();
        int staffID = Convert.ToInt32(Context.Request["StaffID"]);
        command.CommandText = @"SELECT Application.AppID, Application.a1_ProjTitle, StatusList.ID, StatusList.AppStatus, Application.Ownership_StaffID, a5_coinvestigators.Declaration, 
                                Application.RiskLow_Bool, Application.RiskNonLow_Bool 
                                FROM Application INNER JOIN a5_coinvestigators ON Application.AppID = a5_coinvestigators.AppID LEFT JOIN 
                                StatusList ON Application.AppStatus = StatusList.ID 
                                WHERE a5_CoInvestigators.StaffID = " + staffID + " AND (Application.AppStatus = 0 OR Application.AppStatus = 2 OR Application.AppStatus = 5)";
        MySqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read()) //Build each row.
            {
                HtmlTableRow row = new HtmlTableRow();
                string riskLevel = "";

                //Build buttons.
                HtmlInputButton btnView = new HtmlInputButton();
                btnView.ID = "ViewCI" + reader["AppID"].ToString();
                btnView.Value = "View";
                btnView.ServerClick += btnView_ServerClick;

                HtmlInputButton btnDec = new HtmlInputButton();
                btnDec.ID = "DecCI" + reader["AppID"].ToString();
                btnDec.Value = "Declaration";
                btnDec.ServerClick += btnDec_ServerClick;

                HtmlInputButton btnEdit = new HtmlInputButton();
                btnEdit.ID = "EditCI" + reader["AppID"].ToString();
                btnEdit.Value = "Edit";
                btnEdit.ServerClick += btnEdit_ServerClick;

                Button btnDel = new Button(); //Need to use an ASP button to add the client side confirmation.
                btnDel.ID = "DelPI" + reader["AppID"].ToString();
                btnDel.Text = "Delete";
                btnDel.OnClientClick = "return(beforeDelete( ))"; //Asks user if they are sure they want to delete.
                btnDel.Click += btnDel_ServerClick;

                //Determine if low or non low risk.
                if (Convert.ToBoolean(reader["RiskLow_Bool"]))
                    riskLevel = "Low risk";
                else if (Convert.ToBoolean(reader["RiskNonLow_Bool"]))
                    riskLevel = "Non low risk";

                //If status is HOS declined add a hyperlink to the comment.
                HtmlTableCell statusCell = HTMLFactory.buildCell("200", "left", reader["AppStatus"].ToString());
                if (Convert.ToInt32(reader["ID"]) == 2 || Convert.ToInt32(reader["ID"]) == 5) //HOS declined or incomplete.
                {
                    statusCell.InnerText += " - ";
                    HyperLink hl = new HyperLink();
                    hl.Text = "Comment";
                    char mode = 'R';
                    if (staffID == Convert.ToInt32(reader["Ownership_StaffID"].ToString())) //If CI has control, set to write access.
                        mode = 'W';
                    hl.NavigateUrl = "EthicsEditAppS7.aspx?Mode=" + mode + "&AppID=" + reader["AppID"].ToString() + "&StaffID=" + staffID + "&Type=" + Context.Request["Type"];
                    statusCell.Controls.Add(hl);
                }

                row.Cells.Add(HTMLFactory.buildCell("50", "left", reader["AppID"].ToString()));
                row.Cells.Add(HTMLFactory.buildCell("400", "left", reader["a1_ProjTitle"].ToString()));
                row.Cells.Add(statusCell);
                row.Cells.Add(HTMLFactory.buildCell("120", "left", riskLevel));

                //If CI has not made declaration, display a button.
                if (Convert.ToBoolean(reader["Declaration"]) == true)
                    row.Cells.Add(HTMLFactory.buildCell("97", "center", "Completed"));
                else
                    row.Cells.Add(HTMLFactory.buildCell("97", "center", btnDec));

                //If CI has been given control, allow editing.
                if (staffID == Convert.ToInt32(reader["Ownership_StaffID"].ToString()))
                {
                    row.Cells.Add(HTMLFactory.buildCell("97", "center", btnEdit));
                    row.Cells.Add(HTMLFactory.buildCell("97", "center", btnDel));
                }
                else
                {
                    row.Cells.Add(HTMLFactory.buildCell("97", "center", btnView));
                    row.Cells.Add(HTMLFactory.buildCell("97", "center", "No control"));
                }

                tblCIUnsubmitted.Rows.Add(row);
            }
        }
        else //Display none.
        {
            if (tblCIUnsubmitted.Rows.Count != 0)
            {
                HtmlTableRow blnkRow = new HtmlTableRow();
                HtmlTableCell blnk = new HtmlTableCell();
                blnk.InnerText = "None";
                blnk.ColSpan = 7;
                blnkRow.Cells.Add(blnk);
                tblCIUnsubmitted.Rows.Add(blnkRow);
            }
        }
        reader.Dispose(); //Cleanup.
        command.Dispose();
    }

    //Inserts new application into database and redirects to first page of application.
    protected void btnNew_ServerClick(object sender, EventArgs e)
    {
        MySql.Data.MySqlClient.MySqlConnection mySqlConnection = new Connector().MySQLConnect();
        MySqlCommand command = mySqlConnection.CreateCommand();
        int staffID = Convert.ToInt32(Context.Request["StaffID"]);
        command.CommandText = "INSERT INTO Application (a4_InvestStaffID, a6_ContactStaffID, Ownership_StaffID) VALUES (" + staffID + ", " + staffID + ", " + staffID + ")";
        int rtn = command.ExecuteNonQuery(); //rtn indicates number of rows added (ie success or failure).
        command.CommandText = "SELECT LAST_INSERT_ID()";
        MySqlDataReader reader = command.ExecuteReader();

        if (reader.HasRows && rtn == 1)
        {
            reader.Read();
            Response.Redirect("EthicsEditAppTriage.aspx?Mode=W&AppID=" + reader[0] + "&StaffID=" + Context.Request["StaffID"] + "&Type=" + Context.Request["Type"]); //Load application.
        }
        else
            divMsg.InnerText = "Error: Could not add new application to database";
        command.Dispose(); //Cleanup.
        mySqlConnection.Close();
    }

    //Extract App ID from button name and delete.
    public void btnDel_ServerClick(object sender, EventArgs e)
    {
        Button source = (Button)sender;
        MySql.Data.MySqlClient.MySqlConnection mySqlConnection = new Connector().MySQLConnect();
        MySqlCommand command = mySqlConnection.CreateCommand();
        command.CommandText = "DELETE FROM Application WHERE AppID = " + source.ID.Remove(0, 5);
        command.ExecuteNonQuery();
        command.Dispose();
        buildPIUnsubmittedTbl(mySqlConnection); //Rebuild unsubmitted tables.
        buildCIUnsubmittedTbl(mySqlConnection);
        mySqlConnection.Close();
    }

    //Button click handlers, redirect to new page.
    public void btnEdit_ServerClick(object sender, EventArgs e)
    {
        HtmlInputButton source = (HtmlInputButton)sender;
        Response.Redirect("EthicsEditAppTriage.aspx?Mode=W&AppID=" + source.ID.Remove(0,6) + "&StaffID=" + Context.Request["StaffID"] + "&Type=" + Context.Request["Type"]);
    }
    public void btnView_ServerClick(object sender, EventArgs e)
    {
        HtmlInputButton source = (HtmlInputButton)sender;
        Response.Redirect("EthicsEditAppTriage.aspx?Mode=R&AppID=" + source.ID.Remove(0, 6) + "&StaffID=" + Context.Request["StaffID"] + "&Type=" + Context.Request["Type"]);
    }
    public void btnDec_ServerClick(object sender, EventArgs e)
    {
        HtmlInputButton source = (HtmlInputButton)sender;
        Response.Redirect("EthicsCIDeclaration.aspx?AppID=" + source.ID.Remove(0, 5) + "&StaffID=" + Context.Request["StaffID"] + "&Type=" + Context.Request["Type"]);
    }
}