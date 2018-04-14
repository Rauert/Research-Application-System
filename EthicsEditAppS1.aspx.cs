using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Web.UI.HtmlControls;
using System.Data;

/**
 * Section 1 Edit/View application page.
 * Displays Section 1 data and saves changes made.
 * Allows passing of application control and management of CI's.
 */
public partial class EthicsEditAppS1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MySqlConnection mySqlConnection = new Connector().MySQLConnect();
        if (Context.Request["Mode"].Equals("W")) //Build CI table based on R/W status.
            buildWriteCITable(mySqlConnection);
        else
            buildReadCITable(mySqlConnection);
        if (!Page.IsPostBack) //Don't rebuild page data on postback.
        {
            MySqlCommand command = mySqlConnection.CreateCommand();

            //Populate contact person list.
            command.CommandText = "SELECT StaffID, CONCAT(IFNULL(staff.NameLast,''),', ',IFNULL(staff.NameFirst,'')) AS FullName FROM staff WHERE AccountType = 0 ORDER BY FullName";
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);

            sltContact.DataSource = ds;
            sltContact.DataTextField = "FullName";
            sltContact.DataValueField = "StaffID";
            sltContact.DataBind();
            da.Dispose();

            //Populate CI list. Ensure the PI can't be selected.
            command.CommandText = @"SELECT a4_InvestStaffID FROM application WHERE AppID = " + Context.Request["AppID"];
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                string piID = reader["a4_InvestStaffID"].ToString();
                reader.Dispose();

                command.CommandText = @"SELECT StaffID, CONCAT(IFNULL(staff.NameLast,''),', ',IFNULL(staff.NameFirst,'')) AS FullName FROM staff WHERE AccountType = 0 AND StaffID <> " +
                                        piID + " ORDER BY FullName";
                da = new MySqlDataAdapter(command);
                ds = new DataSet();
                da.Fill(ds);

                sltNewCI.DataSource = ds;
                sltNewCI.DataTextField = "FullName";
                sltNewCI.DataValueField = "StaffID";
                sltNewCI.DataBind();

                da.Dispose();

                buildControlSlt(mySqlConnection);
            }
            else
                reader.Dispose();

            //Populate page with Section 1 data.
            command.CommandText = @"SELECT application.a1_ProjTitle, application.a2_ProjType, application.a2_ProjTypeOther, application.a3a_Background, 
                                    application.a3b_AimsHypo, application.a3c_Methods, application.a4_InvestStaffID, application.a6_ContactStaffID, 
                                    application.Ownership_StaffID, projtypelist.ProjType, staff.CurtinID, CONCAT(IFNULL(staff.NameFirst,''), 
                                    IF(staff.NameMiddle IS NOT NULL, ' ', ''), IFNULL(staff.NameMiddle,''), ' ', IFNULL(staff.NameLast,'')) AS FullName, 
                                    staff.Phone, staff.Email, staff.IntegTraining, schoollist.School 
                                    FROM application LEFT JOIN projtypelist ON application.a2_ProjType = projtypelist.ID LEFT JOIN 
                                    (staff LEFT JOIN schoollist ON staff.School = schoollist.ID) ON application.a4_InvestStaffID = staff.StaffID 
                                    WHERE application.AppID = " + Context.Request["AppID"];
            reader = command.ExecuteReader();
            if (reader.HasRows) //Populate page with data.
            {
                reader.Read();
                projectTitle.InnerText = reader["a1_ProjTitle"].ToString();
                if (reader["a2_ProjType"].ToString() != "")
                    sltProjType.SelectedIndex = Convert.ToInt32(reader["a2_ProjType"]);
                projectTypeOther.InnerText = reader["a2_ProjTypeOther"].ToString();
                projectBackground.InnerText = reader["a3a_Background"].ToString();
                projectAim.InnerText = reader["a3b_AimsHypo"].ToString();
                projectMethod.InnerText = reader["a3c_Methods"].ToString();
                piName.InnerText = reader["FullName"].ToString();
                piStaffId.InnerText = reader["CurtinID"].ToString();
                integrityCert.Checked = Convert.ToBoolean(reader["IntegTraining"]);
                piSchool.InnerText = reader["School"].ToString();
                piEmail.InnerText = reader["Email"].ToString();
                piPhone.InnerText = reader["Phone"].ToString();

                //Find and set valuw of contact list.
                for (int i = 0; i < sltContact.Items.Count; i++)
			    {
			        if (Convert.ToInt32(sltContact.Items[i].Value) == Convert.ToInt32(reader["a6_ContactStaffID"]))
                        sltContact.Items[i].Selected = true;
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
                projectTitle.Disabled = false;
                sltProjType.Disabled = false;
                projectTypeOther.Disabled = false;
                projectBackground.Disabled = false;
                projectAim.Disabled = false;
                projectMethod.Disabled = false;
                sltContact.Disabled = false;
                integrityCert.Disabled = false;
                sltControl.Disabled = false;
                tblNewCIHead.Visible = true;
                tblNewCI.Visible = true;
            }
            else //Hide functions.
            {
                btnAdd.Visible = false;
                sltNewCI.Visible = false;
                sltNewCIRole.Visible = false;
                chkCandNew.Visible = false;
            }

            //Check application validity.
            //Colour the application navigation menus appropriately.
            bool[][] valid = SharedFunctions.validateApplication(Convert.ToInt32(Context.Request["AppID"]));
            if (valid[0][0])
                btnTriage.Style.Add("color", "Green");
            else
                btnTriage.Style.Add("color", "Red");

            validate(valid[1]); //Validate entire section.

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

    //Builds list of investigators that control can be passed to.
    private void buildControlSlt(MySqlConnection mySqlConnection)
    {
        MySqlCommand command = mySqlConnection.CreateCommand();
        command.CommandText = @"SELECT a4_InvestStaffID FROM application WHERE AppID = " + Context.Request["AppID"];
        MySqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            reader.Read();
            int piID = Convert.ToInt32(reader["a4_InvestStaffID"]);
            reader.Dispose();
            if (piID == Convert.ToInt32(Context.Request["StaffID"])) //If current user is the PI allow passing control to CI's.
            {
                command.CommandText = @"SELECT DISTINCT Staff.StaffID, CONCAT(IFNULL(staff.NameLast,''),', ',IFNULL(staff.NameFirst,'')) AS FullName 
                                        FROM staff LEFT JOIN a5_coinvestigators ON staff.StaffID = a5_coinvestigators.StaffID 
                                        WHERE a5_coinvestigators.AppID = " + Context.Request["AppID"] + " OR Staff.StaffID = " + piID + " ORDER BY FullName";
            }
            else //Current user is a CI allow passing control back to PI only.
            {
                command.CommandText = @"SELECT DISTINCT Staff.StaffID, CONCAT(IFNULL(staff.NameLast,''),', ',IFNULL(staff.NameFirst,'')) AS FullName 
                                        FROM staff LEFT JOIN a5_coinvestigators ON staff.StaffID = a5_coinvestigators.StaffID 
                                        WHERE (a5_coinvestigators.AppID = " + Context.Request["AppID"] + " AND a5_coinvestigators.StaffID = " +
                                        Context.Request["StaffID"] + ") OR Staff.StaffID = " + piID + " ORDER BY FullName";
            }

            MySqlDataAdapter da = new MySqlDataAdapter(command);
            DataSet ds = new DataSet();
            da = new MySqlDataAdapter(command);
            ds = new DataSet();
            da.Fill(ds);

            sltControl.DataSource = ds;
            sltControl.DataTextField = "FullName";
            sltControl.DataValueField = "StaffID";
            sltControl.DataBind();

            da.Dispose();

            command.CommandText = @"SELECT Ownership_StaffID FROM application WHERE application.AppID = " + Context.Request["AppID"];
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                //Find and set currently selected.
                for (int i = 0; i < sltControl.Items.Count; i++)
                {
                    if (Convert.ToInt32(sltControl.Items[i].Value) == Convert.ToInt32(reader["Ownership_StaffID"]))
                        sltControl.Items[i].Selected = true;
                }
            }
        }
        reader.Dispose(); //Cleanup.
        command.Dispose();
    }

    //Build Write access CI table, contains buttons to edit and delete.
    private void buildWriteCITable(MySqlConnection mySqlConnection)
    {
        for (int i = tblCI.Rows.Count-1; i > 0; i--)
        {
            tblCI.Rows.RemoveAt(i);
        }
        MySqlCommand command = mySqlConnection.CreateCommand();
        command.CommandText = @"SELECT staff.StaffID, CONCAT(IFNULL(staff.NameLast,''),', ',IFNULL(staff.NameFirst,'')) AS FullName, a5_coinvestigators.CandidacyAppr, Staff.IntegTraining, RoleList.Role
                                FROM a5_coinvestigators LEFT JOIN staff ON a5_coinvestigators.StaffID = staff.StaffID LEFT JOIN RoleList ON a5_coinvestigators.Role = RoleList.ID 
                                WHERE a5_coinvestigators.AppID = " + Context.Request["AppID"];
        MySqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows) //Populate CI table.
        {
            while (reader.Read())
            {
                HtmlInputButton btnEdit = new HtmlInputButton();
                btnEdit.ID = "EditCI" + reader["StaffID"].ToString();
                btnEdit.Value = "Edit";
                btnEdit.ServerClick += btnEditCI_ServerClick;

                Button btnDel = new Button(); //Need to use an ASP button to add the client side confirmation.
                btnDel.ID = "DelCI" + reader["StaffID"].ToString();
                btnDel.Text = "Delete";
                btnDel.OnClientClick = "return(beforeDelete( ))"; //Asks user if they are sure they want to delete.
                btnDel.Click += btnDelCI_ServerClick;

                HtmlInputCheckBox chkCand = new HtmlInputCheckBox();
                chkCand.ID = "chkCand" + reader["StaffID"].ToString();
                chkCand.Disabled = true;
                HtmlInputCheckBox chkIntg = new HtmlInputCheckBox();
                chkIntg.ID = "chkIntg" + reader["StaffID"].ToString();
                chkIntg.Disabled = true;

                HtmlTableRow row = new HtmlTableRow();
                row.Cells.Add(HTMLFactory.buildCell("", "left", reader["FullName"].ToString()));
                row.Cells.Add(HTMLFactory.buildCell("", "left", reader["Role"].ToString()));
                row.Cells.Add(HTMLFactory.buildCell("", "left", Convert.ToBoolean(reader["CandidacyAppr"]), chkCand));
                row.Cells.Add(HTMLFactory.buildCell("", "left", Convert.ToBoolean(reader["IntegTraining"]), chkIntg));
                row.Cells.Add(HTMLFactory.buildCell("", "center", btnEdit));

                //If CI has control don't let her/him delete self.
                if (Convert.ToInt32(reader["StaffID"]) == Convert.ToInt32(Context.Request["StaffID"]))
                    row.Cells.Add(HTMLFactory.buildCell("", "center", ""));
                else
                    row.Cells.Add(HTMLFactory.buildCell("", "center", btnDel));

                tblCI.Rows.Insert(1, row);
            }
        }
        else //Display none.
        {
            if (tblCI.Rows.Count != 0)
            {
                HtmlTableRow blnkRow = new HtmlTableRow();
                HtmlTableCell blnk = new HtmlTableCell();
                blnk.InnerText = "None";
                blnk.ColSpan = 6;
                blnkRow.Cells.Add(blnk);
                tblCI.Rows.Add(blnkRow);
            }
        }
        reader.Dispose();
    }

    //Build Read access CI table.
    private void buildReadCITable(MySqlConnection mySqlConnection)
    {
        MySqlCommand command = mySqlConnection.CreateCommand();
        command.CommandText = @"SELECT staff.StaffID, CONCAT(IFNULL(staff.NameLast,''),', ',IFNULL(staff.NameFirst,'')) AS FullName, a5_coinvestigators.CandidacyAppr, Staff.IntegTraining, RoleList.Role
                                FROM a5_coinvestigators LEFT JOIN staff ON a5_coinvestigators.StaffID = staff.StaffID LEFT JOIN RoleList ON a5_coinvestigators.Role = RoleList.ID 
                                WHERE a5_coinvestigators.AppID = " + Context.Request["AppID"];
        MySqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows) //Populate CI table.
        {
            while (reader.Read())
            {
                HtmlInputCheckBox chkCand = new HtmlInputCheckBox();
                chkCand.ID = "chkCand" + reader["StaffID"].ToString();
                chkCand.Disabled = true;
                HtmlInputCheckBox chkIntg = new HtmlInputCheckBox();
                chkIntg.ID = "chkIntg" + reader["StaffID"].ToString();
                chkIntg.Disabled = true;

                HtmlTableRow row = new HtmlTableRow();
                row.Cells.Add(HTMLFactory.buildCell("", "left", reader["FullName"].ToString()));
                row.Cells.Add(HTMLFactory.buildCell("", "left", reader["Role"].ToString()));
                row.Cells.Add(HTMLFactory.buildCell("", "left", Convert.ToBoolean(reader["CandidacyAppr"]), chkCand));
                row.Cells.Add(HTMLFactory.buildCell("", "left", Convert.ToBoolean(reader["IntegTraining"]), chkIntg));
                row.Cells.Add(HTMLFactory.buildCell("", "center", ""));
                row.Cells.Add(HTMLFactory.buildCell("", "center", ""));

                tblCI.Rows.Insert(1, row);
            }
        }
        else //Display none.
        {
            if (tblCI.Rows.Count != 0)
            {
                HtmlTableRow blnkRow = new HtmlTableRow();
                HtmlTableCell blnk = new HtmlTableCell();
                blnk.InnerText = "None";
                blnk.ColSpan = 6;
                blnkRow.Cells.Add(blnk);
                tblCI.Rows.Add(blnkRow);
            }
        }
        reader.Dispose();
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
        back();
    }

    //Build url for main page and redirect.
    private void back()
    {
        String nextAddr = SharedFunctions.getMainPageAddr(Convert.ToInt32(Context.Request["Type"]), Convert.ToInt32(Context.Request["StaffID"]));
        Response.Redirect(nextAddr);
    }

    public void btnSave_ServerClick(object sender, EventArgs e)
    {
        save();
    }

    //Redirect to edit CI page.
    public void btnEditCI_ServerClick(object sender, EventArgs e)
    {
        HtmlInputButton source = (HtmlInputButton)sender;
        save();
        Response.Redirect("EthicsEditCI.aspx?AppID=" + Context.Request["AppID"] + "&StaffID=" + Context.Request["StaffID"] + "&Type=" + Context.Request["Type"] + "&CIStaffID=" + source.ID.Remove(0, 6));
    }

    //Delete CI from CI table in DB and rebuild table.
    public void btnDelCI_ServerClick(object sender, EventArgs e)
    {
        Button source = (Button)sender;
        MySqlConnection mySqlConnection = new Connector().MySQLConnect();
        MySqlCommand command = mySqlConnection.CreateCommand();
        command.CommandText = "DELETE FROM a5_coinvestigators WHERE AppID = " + Context.Request["AppID"] + " AND StaffID = " + source.ID.Remove(0, 5);
        command.ExecuteNonQuery();
        command.Dispose();
        buildWriteCITable(mySqlConnection);
        buildControlSlt(mySqlConnection);
        mySqlConnection.Close();
    }

    //Add new CI to DB and rebuild table.
    public void btnAdd_ServerClick(object sender, EventArgs e)
    {
        if (Convert.ToInt32(sltNewCI.Value) != -1) //If a new CI has been selected.
        {
            MySqlConnection mySqlConnection = new Connector().MySQLConnect();
            //Check if CI already exists.
            MySqlCommand command = mySqlConnection.CreateCommand();
            command.CommandText = @"SELECT Count(*) FROM a5_coinvestigators WHERE AppID = " + Context.Request["AppID"] + " AND StaffID = " + Convert.ToInt32(sltNewCI.Value);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                if (Convert.ToInt32(reader[0]) == 0) //If not already an entry for this CI.
                {
                    reader.Dispose();
                    command.CommandText = @"INSERT INTO a5_coinvestigators (AppID, StaffID, Role, CandidacyAppr) VALUES (" + Context.Request["AppID"] + ", " + 
                                            Convert.ToInt32(sltNewCI.Value) + ", " + sltNewCIRole.SelectedIndex + ", " + chkCandNew.Checked + ")";
                    command.ExecuteNonQuery();
                }
            }
            reader.Dispose();
            command.Dispose();
            buildWriteCITable(mySqlConnection);
            buildControlSlt(mySqlConnection);
        }
    }

    //Save the user entered changes back to the database.
    public void save()
    {
        divMsg.InnerText = "";
        if (sltProjType.SelectedIndex != 6)
            projectTypeOther.InnerText = "";
        MySqlConnection mySqlConnection = new Connector().MySQLConnect();
        MySqlCommand command = mySqlConnection.CreateCommand();
        command.CommandText = @"UPDATE Application SET a1_ProjTitle = '" + projectTitle.Value + "', a2_ProjType = " + sltProjType.SelectedIndex + ", a2_ProjTypeOther = '" + projectTypeOther.Value +
                                "', a3a_Background = '" + projectBackground.Value + "', a3b_AimsHypo = '" + projectAim.Value + "', a3c_Methods = '" + projectMethod.Value + "', a6_ContactStaffID = " +
                                sltContact.Items[sltContact.SelectedIndex].Value + ", Ownership_StaffID = " + sltControl.Items[sltControl.SelectedIndex].Value + " WHERE AppID = " + Context.Request["AppID"];
        int status = command.ExecuteNonQuery();

        if (status != 1)
        {
            divMsg.InnerText = "Error saving!";
        }
        else
        {
            //Check if section now valid.
            validate(SharedFunctions.validateS1(Convert.ToInt32(Context.Request["AppID"])));
        }

        command.CommandText = @"UPDATE Staff SET IntegTraining = " + integrityCert.Checked + " WHERE StaffID = " + Context.Request["StaffID"];
        int status2 = command.ExecuteNonQuery();

        if (status2 != 1)
        {
            divMsg.InnerText = "Error saving!";
        }

        command.Dispose();
        mySqlConnection.Close();
        //If control has been passed redirect to main.
        if (Convert.ToInt32(sltControl.Items[sltControl.SelectedIndex].Value) != Convert.ToInt32(Context.Request["StaffID"]))
            back();
    }

    //Validate section and display asterix's for unanswered questions.
    private void validate(bool[] valid)
    {
        if (valid[0])
            btnS1.Style.Add("color", "Green");
        else
            btnS1.Style.Add("color", "Red");
        mand1.Visible = !valid[1];
        mand2.Visible = !valid[2];
        mand3a.Visible = !valid[3];
        mand3b.Visible = !valid[4];
        mand3c.Visible = !valid[5];
    }

    //Open printable version of application in new tab.
    //Based on http://stackoverflow.com/questions/10493901/how-to-open-a-page-in-new-tab-on-button-click-in-asp-net
    public void btnPrint_ServerClick(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(
        this.GetType(), "OpenWindow", "window.open('" + "EthicsPrintApp.aspx?AppID=" + Request["AppID"].ToString() + "','_newtab');", true);
    }
}