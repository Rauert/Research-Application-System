using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Web.UI.HtmlControls;

/**
 * Section 4 Edit/View application page.
 * Displays Section 4 data and saves changes made.
 */
public partial class EthicsEditAppS4 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) //Don't rebuild page data on postback.
        {
            MySqlConnection mySqlConnection = new Connector().MySQLConnect();
            MySqlCommand command = mySqlConnection.CreateCommand();

            //Populate page with data from Application table.
            command.CommandText = @"SELECT d16_PregnantNo, d16_PregnantYes, d17_ChildrenNo, d17_ChildrenYes, d18_RelationshipsYes, d18_RelationshipsNo, d18_Relationships, d19_MedCareNo, 
                                    d19_MedCareYes, d19_MedCare, d20_ImpairmentNo, d20_ImpairmentYes, d20_Impairment, d21_IllegalNo, d21_IllegalYes, d21_Illegal, d22_AboriginalNo, d22_AboriginalYes 
                                    FROM Application WHERE AppID = " + Context.Request["AppID"];
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows) //Populate page with data.
            {
                reader.Read();
                ethicPregnantRadioNo.Checked = Convert.ToBoolean(reader["d16_PregnantNo"]);
                ethicPregnantRadioYes.Checked = Convert.ToBoolean(reader["d16_PregnantYes"]);
                ethicYoungRadioNo.Checked = Convert.ToBoolean(reader["d17_ChildrenNo"]);
                ethicYoungRadioYes.Checked = Convert.ToBoolean(reader["d17_ChildrenYes"]);
                ethicUnequalRelRadioYes.Checked = Convert.ToBoolean(reader["d18_RelationshipsYes"]);
                ethicUnequalRelRadioNo.Checked = Convert.ToBoolean(reader["d18_RelationshipsNo"]);
                ethicUnequalRel.InnerText = reader["d18_Relationships"].ToString();
                ethicUnableConsentRadioNo.Checked = Convert.ToBoolean(reader["d19_MedCareNo"]);
                ethicUnableConsentRadioYes.Checked = Convert.ToBoolean(reader["d19_MedCareYes"]);
                ethicUnableConsent.InnerText = reader["d19_MedCare"].ToString();
                ethicCogImpairmentRadioNo.Checked = Convert.ToBoolean(reader["d20_ImpairmentNo"]);
                ethicCogImpairmentRadioYes.Checked = Convert.ToBoolean(reader["d20_ImpairmentYes"]);
                ethicCogImpairment.InnerText = reader["d20_Impairment"].ToString();
                ethicIllegalActRadioNo.Checked = Convert.ToBoolean(reader["d21_IllegalNo"]);
                ethicIllegalActRadioYes.Checked = Convert.ToBoolean(reader["d21_IllegalYes"]);
                ethicIllegal.InnerText = reader["d21_Illegal"].ToString();
                ethicAboriginalTorresRadioNo.Checked = Convert.ToBoolean(reader["d22_AboriginalNo"]);
                ethicAboriginalTorresRadioYes.Checked = Convert.ToBoolean(reader["d22_AboriginalYes"]);

                //Populate page with data from Pregnant table (If data exists).
                reader.Dispose();
                command.CommandText = @"SELECT Count(*) FROM d16_Pregnant WHERE AppID = " + Context.Request["AppID"];
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    if (Convert.ToInt32(reader[0]) == 1) //If data exists.
                    {
                        reader.Dispose();
                        command.CommandText = @"SELECT d16a_CareNo, d16a_CareYes, d16a_Care, d16b_SeparationNo, d16b_SeparationYes, d16b_Separation FROM d16_Pregnant 
                                                WHERE AppID = " + Context.Request["AppID"];
                        reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            ethicPregnantWellRadioNo.Checked = Convert.ToBoolean(reader["d16a_CareNo"]);
                            ethicPregnantWellRadioYes.Checked = Convert.ToBoolean(reader["d16a_CareYes"]);
                            ethicPregnantWell.InnerText = reader["d16a_Care"].ToString();
                            ethicPregnantInfoRadioNo.Checked = Convert.ToBoolean(reader["d16b_SeparationNo"]);
                            ethicPregnantInfoRadioYes.Checked = Convert.ToBoolean(reader["d16b_SeparationYes"]);
                            ethicPregnantInfo.InnerText = reader["d16b_Separation"].ToString();
                        }
                    }
                }
                reader.Dispose();

                //Populate page with data from Children table (If data exists).
                command.CommandText = @"SELECT Count(*) FROM d17_Children WHERE AppID = " + Context.Request["AppID"];
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    if (Convert.ToInt32(reader[0]) == 1) //If data exists.
                    {
                        reader.Dispose();
                        command.CommandText = @"SELECT d17_Why, d17a_WWCNo, d17a_WWCYes FROM d17_Children 
                                                WHERE AppID = " + Context.Request["AppID"];
                        reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            ethicYoung.InnerText = reader["d17_Why"].ToString();
                            ethicWWCRadioNo.Checked = Convert.ToBoolean(reader["d17a_WWCNo"]);
                            ethicWWCRadioYes.Checked = Convert.ToBoolean(reader["d17a_WWCYes"]);
                        }
                    }
                }
                reader.Dispose();

                //Populate page with data from Aboriginal table (If data exists).
                command.CommandText = @"SELECT Count(*) FROM d22_Aboriginal WHERE AppID = " + Context.Request["AppID"];
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    if (Convert.ToInt32(reader[0]) == 1) //If data exists.
                    {
                        reader.Dispose();
                        command.CommandText = @"SELECT d22a_Proportion, d22b_RecordedNo, d22b_RecordedYes, d22b_Recorded FROM d22_Aboriginal 
                                                WHERE AppID = " + Context.Request["AppID"];
                        reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            ethicAboriginalPop.InnerText = reader["d22a_Proportion"].ToString();
                            ethicAboriginalRecordRadioNo.Checked = Convert.ToBoolean(reader["d22b_RecordedNo"]);
                            ethicAboriginalRecordRadioYes.Checked = Convert.ToBoolean(reader["d22b_RecordedYes"]);
                            ethicAboriginalRecord.InnerText = reader["d22b_Recorded"].ToString();
                        }
                    }
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
                ethicPregnantRadioNo.Disabled = false;
                ethicPregnantRadioYes.Disabled = false;
                ethicPregnantWellRadioNo.Disabled = false;
                ethicPregnantWellRadioYes.Disabled = false;
                ethicPregnantWell.Disabled = false;
                ethicPregnantInfoRadioNo.Disabled = false;
                ethicPregnantInfoRadioYes.Disabled = false;
                ethicPregnantInfo.Disabled = false;
                ethicYoungRadioNo.Disabled = false;
                ethicYoungRadioYes.Disabled = false;
                ethicYoung.Disabled = false;
                ethicWWCRadioNo.Disabled = false;
                ethicWWCRadioYes.Disabled = false;
                ethicUnequalRelRadioNo.Disabled = false;
                ethicUnequalRelRadioYes.Disabled = false;
                ethicUnequalRel.Disabled = false;
                ethicUnableConsentRadioNo.Disabled = false;
                ethicUnableConsentRadioYes.Disabled = false;
                ethicUnableConsent.Disabled = false;
                ethicCogImpairmentRadioNo.Disabled = false;
                ethicCogImpairmentRadioYes.Disabled = false;
                ethicCogImpairment.Disabled = false;
                ethicIllegalActRadioNo.Disabled = false;
                ethicIllegalActRadioYes.Disabled = false;
                ethicIllegal.Disabled = false;
                ethicAboriginalTorresRadioNo.Disabled = false;
                ethicAboriginalTorresRadioYes.Disabled = false;
                ethicAboriginalPop.Disabled = false;
                ethicAboriginalRecordRadioNo.Disabled = false;
                ethicAboriginalRecordRadioYes.Disabled = false;
                ethicAboriginalRecord.Disabled = false;
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

            validate(valid[4]); //Validate entire section.

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
        divMsg.InnerText = ""; //Clear errors.
        MySql.Data.MySqlClient.MySqlConnection mySqlConnection = new Connector().MySQLConnect();
        MySqlCommand command = mySqlConnection.CreateCommand();
        command.CommandText = @"UPDATE Application SET d16_PregnantNo = " + ethicPregnantRadioNo.Checked + ", d16_PregnantYes = " + ethicPregnantRadioYes.Checked + ", d17_ChildrenNo = " + ethicYoungRadioNo.Checked +
                               ", d17_ChildrenYes = " + ethicYoungRadioYes.Checked + ", d18_RelationshipsYes = " + ethicUnequalRelRadioYes.Checked + ", d18_RelationshipsNo = " +
                               ethicUnequalRelRadioNo.Checked + ", d18_Relationships = '" + ethicUnequalRel.Value + "', d19_MedCareNo = " + ethicUnableConsentRadioNo.Checked + ", d19_MedCareYes = " +
                               ethicUnableConsentRadioYes.Checked  + ", d19_MedCare = '" + ethicUnableConsent.Value + "', d20_ImpairmentNo = " + ethicCogImpairmentRadioNo.Checked + ", d20_ImpairmentYes = " +
                               ethicCogImpairmentRadioYes.Checked  + ", d20_Impairment = '" + ethicCogImpairment.Value + "', d21_IllegalNo = " + ethicIllegalActRadioNo.Checked + ", d21_IllegalYes = " +
                               ethicIllegalActRadioYes.Checked  + ", d21_Illegal = '" + ethicIllegal.Value + "', d22_AboriginalNo = " + ethicAboriginalTorresRadioNo.Checked + ", d22_AboriginalYes = " + 
                               ethicAboriginalTorresRadioYes.Checked + " WHERE AppID = " + Context.Request["AppID"];
        int status1 = command.ExecuteNonQuery();

        command.CommandText = @"SELECT Count(*) FROM d16_Pregnant WHERE AppID = " + Context.Request["AppID"];
        MySqlDataReader reader = command.ExecuteReader();

        //Check if pregnant women used.
        if (ethicPregnantRadioYes.Checked)
        {
            if (reader.HasRows)
            {
                reader.Read();
                if (Convert.ToInt32(reader[0]) == 0) //If no data exists append row to pregnant table.
                {
                    reader.Dispose();
                    command.CommandText = @"INSERT INTO d16_Pregnant (AppID, d16a_CareNo, d16a_CareYes, d16a_Care, d16b_SeparationNo, d16b_SeparationYes, d16b_Separation) VALUES (" + Context.Request["AppID"] + ", " +
                                            ethicPregnantWellRadioNo.Checked + ", " + ethicPregnantWellRadioYes.Checked + ", '" + ethicPregnantWell.Value + "', " + ethicPregnantInfoRadioNo.Checked + ", " + 
                                            ethicPregnantInfoRadioYes.Checked + ", '" + ethicPregnantInfo.Value + "')";
                    int status2 = command.ExecuteNonQuery();
                    if (status2 != 1)
                    {
                        divMsg.InnerText = "Error saving to Pregnant Women table!";
                    }
                }
                else //Update existing data.
                {
                    reader.Dispose();
                    command.CommandText = @"UPDATE d16_Pregnant SET d16a_CareNo = " + ethicPregnantWellRadioNo.Checked + ", d16a_CareYes = " + ethicPregnantWellRadioYes.Checked + ", d16a_Care = '" + 
                                            ethicPregnantWell.Value + "', d16b_SeparationNo = " + ethicPregnantInfoRadioNo.Checked + ", d16b_SeparationYes = " + 
                                            ethicPregnantInfoRadioYes.Checked + ", d16b_Separation = '" + ethicPregnantInfo.Value + "' WHERE AppID = " + Context.Request["AppID"];
                    int status3 = command.ExecuteNonQuery();
                    if (status3 != 1)
                    {
                        divMsg.InnerText = "Error saving to Pregnant Women table!";
                    }
                }
            }
        }
        else if (ethicPregnantRadioNo.Checked)
        {
            ethicPregnantWellRadioNo.Checked = false; //Delete does not reset already entered values. So set to null.
            ethicPregnantWellRadioYes.Checked = false;
            ethicPregnantWell.InnerText = "";
            ethicPregnantInfoRadioNo.Checked = false;
            ethicPregnantInfoRadioYes.Checked = false;
            ethicPregnantInfo.InnerText = "";
            if (reader.HasRows)
            {
                reader.Read();
                if (Convert.ToInt32(reader[0]) == 1) //If no data exists delete it.
                {
                    reader.Dispose();
                    command.CommandText = @"DELETE FROM d16_Pregnant WHERE AppID = " + Context.Request["AppID"];
                    int status4 = command.ExecuteNonQuery();
                    if (status4 != 1)
                    {
                        divMsg.InnerText = "Error deleting entry in Pregnant Women table!";
                    }
                }
            }
        }
        reader.Dispose();

        command.CommandText = @"SELECT Count(*) FROM d17_Children WHERE AppID = " + Context.Request["AppID"];
        reader = command.ExecuteReader();

        //Check if a children used.
        if (ethicYoungRadioYes.Checked)
        {
            if (reader.HasRows)
            {
                reader.Read();
                if (Convert.ToInt32(reader[0]) == 0) //If no data exists append row to children table.
                {
                    reader.Dispose();
                    command.CommandText = @"INSERT INTO d17_Children (AppID, d17_Why, d17a_WWCNo, d17a_WWCYes) VALUES (" + Context.Request["AppID"] + ", '" +
                                            ethicYoung.Value + "', " + ethicWWCRadioNo.Checked + ", " + ethicWWCRadioYes.Checked + ")";
                    int status5 = command.ExecuteNonQuery();
                    if (status5 != 1)
                    {
                       divMsg.InnerText = "Error saving to Children table!";
                    }
                }
                else //Update existing data.
                {
                    reader.Dispose();
                    command.CommandText = @"UPDATE d17_Children SET d17_Why = '" + ethicYoung.Value + "', d17a_WWCNo = " + ethicWWCRadioNo.Checked + ", d17a_WWCYes = " + ethicWWCRadioYes.Checked +
                                           " WHERE AppID = " + Context.Request["AppID"];
                    int status6 = command.ExecuteNonQuery();
                    if (status6 != 1)
                    {
                        divMsg.InnerText = "Error saving to Children table!";
                    }
                }
            }
        }
        else if (ethicYoungRadioNo.Checked)
        {
            ethicYoung.InnerText = ""; //Delete does not reset already entered values. So set to null.
            ethicWWCRadioNo.Checked = false;
            ethicWWCRadioYes.Checked = false;
            if (reader.HasRows)
            {
                reader.Read();
                if (Convert.ToInt32(reader[0]) == 1) //If no data exists delete it.
                {
                    reader.Dispose();
                    command.CommandText = @"DELETE FROM d17_Children WHERE AppID = " + Context.Request["AppID"];
                    int status7 = command.ExecuteNonQuery();
                    if (status7 != 1)
                    {
                        divMsg.InnerText = "Error deleting entry in Children table!";
                    }
                }
            }
        }
        reader.Dispose();

        command.CommandText = @"SELECT Count(*) FROM d22_Aboriginal WHERE AppID = " + Context.Request["AppID"];
        reader = command.ExecuteReader();

        //Check if a Aboriginal's used.
        if (ethicAboriginalTorresRadioYes.Checked)
        {
            if (reader.HasRows)
            {
                reader.Read();
                if (Convert.ToInt32(reader[0]) == 0) //If no data exists append row to Aboriginal table.
                {
                    reader.Dispose();
                    command.CommandText = @"INSERT INTO d22_Aboriginal (AppID, d22a_Proportion, d22b_RecordedNo, d22b_RecordedYes, d22b_Recorded) VALUES (" + Context.Request["AppID"] + ", '" +
                                            ethicAboriginalPop.Value + "', " + ethicAboriginalRecordRadioNo.Checked + ", " + ethicAboriginalRecordRadioYes.Checked + ", '" + ethicAboriginalRecord.Value + "')";
                    int status8 = command.ExecuteNonQuery();
                    if (status8 != 1)
                    {
                        divMsg.InnerText = "Error saving to Aboriginal table!";
                    }
                }
                else //Update existing data.
                {
                    reader.Dispose();
                    command.CommandText = @"UPDATE d22_Aboriginal SET d22a_Proportion = '" + ethicAboriginalPop.Value + "', d22b_RecordedNo = " + ethicAboriginalRecordRadioNo.Checked + ", d22b_RecordedYes = " + 
                    ethicAboriginalRecordRadioYes.Checked + ", d22b_Recorded = '" + ethicAboriginalRecord.Value + "' WHERE AppID = " + Context.Request["AppID"];
                    int status9 = command.ExecuteNonQuery();
                    if (status9 != 1)
                    {
                        divMsg.InnerText = "Error saving to Aboriginal table!";
                    }
                }
            }
        }
        else if (ethicAboriginalTorresRadioNo.Checked)
        {
            ethicAboriginalPop.InnerText = ""; //Delete does not reset already entered values. So set to null.
            ethicAboriginalRecordRadioNo.Checked = false;
            ethicAboriginalRecordRadioYes.Checked = false;
            ethicAboriginalRecord.InnerText = "";
            if (reader.HasRows)
            {
                reader.Read();
                if (Convert.ToInt32(reader[0]) == 1) //If no data exists delete it.
                {
                    reader.Dispose();
                    command.CommandText = @"DELETE FROM d22_Aboriginal WHERE AppID = " + Context.Request["AppID"];
                    int status10 = command.ExecuteNonQuery();
                    if (status10 != 1)
                    {
                        divMsg.InnerText = "Error deleting entry in Aboriginal table!";
                    }
                }
            }
        }

        if (status1 != 1)
        {
            divMsg.InnerText = "Error saving!";
        }
        else
        {
            //Check validity of section.
            validate(SharedFunctions.validateS4(Convert.ToInt32(Context.Request["AppID"])));
        }

        reader.Dispose();
        command.Dispose();
        mySqlConnection.Close();
    }

    //Validate section and display asterix's for unanswered questions.
    private void validate(bool[] valid)
    {
        if (valid[0])
            btnS4.Style.Add("color", "Green");
        else
            btnS4.Style.Add("color", "Red");
        mand16.Visible = !valid[1];
        mand16a.Visible = !valid[2];
        mand16b.Visible = !valid[3];
        mand17.Visible = !valid[4];
        mand17a.Visible = !valid[5];
        mand18.Visible = !valid[6];
        mand19.Visible = !valid[7];
        mand20.Visible = !valid[8];
        mand21.Visible = !valid[9];
        mand22.Visible = !valid[10];
        mand22a.Visible = !valid[11];
        mand22b.Visible = !valid[12];
    }

    //Open printable version of application in new tab.
    //Based on http://stackoverflow.com/questions/10493901/how-to-open-a-page-in-new-tab-on-button-click-in-asp-net
    public void btnPrint_ServerClick(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(
        this.GetType(), "OpenWindow", "window.open('" + "EthicsPrintApp.aspx?AppID=" + Request["AppID"].ToString() + "','_newtab');", true);
    }
}