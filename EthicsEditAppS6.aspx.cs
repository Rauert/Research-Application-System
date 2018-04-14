using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Web.UI.HtmlControls;
using System.Net;
using System.IO;
using System.Text;
using System.Diagnostics;

/**
 * Section 6 Edit/View application page.
 * Used for uploading files to the application.
 */
public partial class EthicsEditAppS6 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MySqlConnection mySqlConnection = new Connector().MySQLConnect();
        buildTbl(mySqlConnection); //Build table of uploaded files.
        mySqlConnection.Close();

        if (!Page.IsPostBack) //Don't rebuild page data on postback.
        {
            if (Context.Request["Mode"].Equals("W")) //If write access then unlock.
            {
                tblNewUpload.Visible = true;
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
                btnS5.Style.Add("color", "Red");
            if (valid[6][0])
                btnS6.Style.Add("color", "Green");
            else
            {
                btnS6.Style.Add("color", "Red");
                mand1.Visible = true; //Show mandatory label.
            }
            if (valid[7][0])
                btnS7.Style.Add("color", "Green");
            else
                btnS7.Style.Add("color", "Red");
        }
    }

    //Build list of files uploaded.
    private void buildTbl(MySqlConnection mySqlConnection)
    {
        if (tblUploads.Rows.Count > 1)
        {
            for (int i = tblUploads.Rows.Count - 1; i > 0; i--)
            {
                tblUploads.Rows.RemoveAt(i);
            }
        }
        MySqlCommand command = mySqlConnection.CreateCommand();
        int staffID = Convert.ToInt32(Context.Request["StaffID"]);
        command.CommandText = @"SELECT Attachments.ID, FileName, AttchType, FileVersion, FileDate FROM Attachments LEFT JOIN TypeList ON Attachments.FileType = TypeList.ID WHERE AppID = " + Context.Request["AppID"];
        MySqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read()) //Build each row.
            {
                HtmlTableRow row = new HtmlTableRow();

                //Display filename as a hyperlink, so file can be opened.
                HtmlTableCell statusCell = HTMLFactory.buildCell("200", "left", "");
                HyperLink hl = new HyperLink();
                hl.Text = reader["FileName"].ToString();
                hl.NavigateUrl = "http://curtinethics-001-site1.smarterasp.net/Uploads/" + Request["AppID"].ToString() + "_" + reader["FileName"].ToString();
                hl.Target = "_blank";
                statusCell.Controls.Add(hl);

                //Delete button.
                Button btnDel = new Button(); //Need to use an ASP button to add the client side confirmation.
                btnDel.ID = "Del" + reader["ID"].ToString();
                btnDel.Text = "Delete";
                btnDel.OnClientClick = "return(beforeDelete( ))"; //Asks user if they are sure they want to delete.
                btnDel.Click += btnDel_ServerClick;

                row.Cells.Add(statusCell);
                row.Cells.Add(HTMLFactory.buildCell("200", "left", reader["AttchType"].ToString()));
                row.Cells.Add(HTMLFactory.buildCell("97", "left", reader["FileVersion"].ToString()));
                row.Cells.Add(HTMLFactory.buildCell("97", "center", reader["FileDate"].ToString()));

                if (Context.Request["Mode"].Equals("W")) //If write then construct delete button.
                    row.Cells.Add(HTMLFactory.buildCell("97", "center", btnDel));
                else
                    row.Cells.Add(HTMLFactory.buildCell("97", "center", "Read only"));

                tblUploads.Rows.Add(row);
            }
        }
        else
        {
            if (tblUploads.Rows.Count != 0)
            {
                HtmlTableRow blnkRow = new HtmlTableRow();
                HtmlTableCell blnk = new HtmlTableCell();
                blnk.InnerText = "None";
                blnk.ColSpan = 5;
                blnkRow.Cells.Add(blnk);
                tblUploads.Rows.Add(blnkRow);
            }
        }
        reader.Dispose();
    }

    //Delete uploaded file. Must delete from server and DB.
    public void btnDel_ServerClick(object sender, EventArgs e)
    {
        Button source = (Button)sender;
        MySqlConnection mySqlConnection = new Connector().MySQLConnect();
        MySqlCommand command = mySqlConnection.CreateCommand();
        MySqlDataReader reader = null;
        try
        {
            command.CommandText = @"SELECT FileName FROM Attachments WHERE ID = " + source.ID.Remove(0, 3); //Get ID number from button ID.
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                string file = reader["FileName"].ToString();
                File.Delete("h:\\root\\home\\curtinethics-001\\www\\site1\\uploads\\" + Context.Request["AppID"] + "_" + file); //Delete on server.
                reader.Dispose();
                command.CommandText = "DELETE FROM Attachments WHERE ID = " + source.ID.Remove(0, 3);
                int status = command.ExecuteNonQuery(); //Delete in DB.
                command.Dispose();
                if (status != 1)
                    divMsg.InnerText = "Error Deleting!";
                else
                {
                    divMsg.InnerText = file + "' deleted successfully.";
                }
            }
            else
                reader.Dispose();
        }
        catch (Exception ex)
        {
            divMsg.InnerText = "ERROR: " + ex.Message.ToString();
            reader.Dispose();
        }
        buildTbl(mySqlConnection); //Rebuild unsubmitted tables.
        mySqlConnection.Close();
        validate();
    }

    //Upload new file. Must check if exists, upload to server and add to DB.
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (fileUpload.HasFile)
        {
            try
            {
                MySqlConnection mySqlConnection = new Connector().MySQLConnect();
                MySqlCommand command = mySqlConnection.CreateCommand();
                command.CommandText = @"SELECT Count(*) FROM Attachments WHERE AppID = " + Context.Request["AppID"] + " AND FileName = '" + fileUpload.FileName + "'";
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    if (Convert.ToInt32(reader[0]) == 0) //File doesn't exist.
                    {
                        reader.Dispose();
                        fileUpload.SaveAs("h:\\root\\home\\curtinethics-001\\www\\site1\\uploads\\" + Context.Request["AppID"] + "_" + fileUpload.FileName); //Upload to server.
                        command.CommandText = @"INSERT INTO Attachments (AppID, FileName, FileType, FileVersion, FileDate) VALUES (" + Context.Request["AppID"] + ", '" + fileUpload.FileName + 
                                               "', " + sltType.SelectedIndex + ", '" + txtVersion.Value.ToString() + "', '" + txtDate.Value.ToString() + "')";
                        int status = command.ExecuteNonQuery(); //Update DB.
                        if (status != 1)
                            divMsg.InnerText = "Error Uploading!";
                        else
                            divMsg.InnerText = "'" + fileUpload.FileName + "' uploaded successfully.";
                    }
                    else
                        divMsg.InnerText = "'" + fileUpload.FileName + "' already exists.";
                }
                //Reset and cleanup.
                txtDate.Value = "";
                txtVersion.Value = "";
                reader.Dispose();
                command.Dispose();
                buildTbl(mySqlConnection);
                mySqlConnection.Close();
                validate();
            }
            catch (Exception ex)
            {
                divMsg.InnerText = "ERROR: " + ex.Message.ToString();
            }
        }
        else
            divMsg.InnerText = "Please select a file";
    }

    //Validate section and display asterix's for unanswered questions.
    private void validate()
    {
        bool[] valid = SharedFunctions.validateS6(Convert.ToInt32(Context.Request["AppID"]));
        if (valid[0])
        {
            btnS6.Style.Add("color", "Green");
            mand1.Visible = false; //Hide mandatory label.
        }
        else
        {
            btnS6.Style.Add("color", "Red");
            mand1.Visible = true; //Show mandatory label.
        }
    }

    //Navigate to another page of application.
    public void btnPageChange_ServerClick(object sender, EventArgs e)
    {
        //Build url from button clicked and redirect.
        HtmlInputButton source = (HtmlInputButton)sender;
        string nextAddr = SharedFunctions.getEditAppPageAddr(source.ID[source.ID.Length - 1], Context.Request["Mode"], Convert.ToInt32(Context.Request["AppID"]), Convert.ToInt32(Context.Request["StaffID"]), Convert.ToInt32(Context.Request["Type"]));
        Response.Redirect(nextAddr);
    }

    //Navigate back to main menu.
    public void btnBack_ServerClick(object sender, EventArgs e)
    {
        //Build url for main page and redirect.
        String nextAddr = SharedFunctions.getMainPageAddr(Convert.ToInt32(Context.Request["Type"]), Convert.ToInt32(Context.Request["StaffID"]));
        Response.Redirect(nextAddr);
    }

    //Open printable version of application in new tab.
    //Based on http://stackoverflow.com/questions/10493901/how-to-open-a-page-in-new-tab-on-button-click-in-asp-net
    public void btnPrint_ServerClick(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(
        this.GetType(), "OpenWindow", "window.open('" + "EthicsPrintApp.aspx?AppID=" + Request["AppID"].ToString() + "','_newtab');", true);
    }
}