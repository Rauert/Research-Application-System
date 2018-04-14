using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Web.UI.HtmlControls;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

/**
 * The print application page.
 * Displays whole application in a printer friendly version on one page.
 * Contains a button for conversion to PDF.
 */
public partial class EthicsPrintApp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MySql.Data.MySqlClient.MySqlConnection mySqlConnection = new Connector().MySQLConnect();
        MySqlCommand command = mySqlConnection.CreateCommand();

        //Populate page with data from Application table.
        command.CommandText = @"SELECT NS3_3_Yes, NS3_3_No, NS3_5_Yes, NS3_5_No, NS4_1_Yes, NS4_1_No, NS4_34_Yes, NS4_34_No, NS4_5_Yes, NS4_5_No, NS4_7_Yes, NS4_7_No, NS4_6_Yes, NS4_6_No, 
                                NotLowRisk, application.a1_ProjTitle, application.a2_ProjType, application.a2_ProjTypeOther, application.a3a_Background, 
                                application.a3b_AimsHypo, application.a3c_Methods, application.a4_InvestStaffID, application.a6_ContactStaffID, 
                                application.Ownership_StaffID, projtypelist.ProjType, staff.CurtinID, CONCAT(IFNULL(staff.NameFirst,''), 
                                IF(staff.NameMiddle IS NOT NULL, ' ', ''), IFNULL(staff.NameMiddle,''), ' ', IFNULL(staff.NameLast,'')) AS FullName, 
                                staff.Phone, staff.Email, staff.IntegTraining, schoollist.School, b7_PotRisk, b8_RiskMan, b9_FinanceNo, b9_FinanceYes, b9_Finance, b10_DB, b10_SocialMed, 
                                b10_ClassRm, b10_SnowRec, b10_Print, b10_Radio, b10_Other, b10_Descr, b10_DBChk, b10_SocialMedChk, b10_ClassChk, b10_SnowRecChk, b10_PrintChk, b10_RadioChk, 
                                b10_OtherChk, b11_ConsentNo, b11_ConsentYes, b11_Consent, b12_DeceptionNo, b12_DeceptionYes, b12_Deception, c13_ClinicalNo, c13_ClinicalYes, c14_HealthNo, 
                                c14_HealthYes, c14_Health, c15_GeneticsNo, c15_GeneticsYes, c15_Genetics, d16_PregnantNo, d16_PregnantYes, d17_ChildrenNo, d17_ChildrenYes, d18_RelationshipsYes, 
                                d18_RelationshipsNo, d18_Relationships, d19_MedCareNo, d19_MedCareYes, d19_MedCare, d20_ImpairmentNo, d20_ImpairmentYes, d20_Impairment, d21_IllegalNo, d21_IllegalYes, 
                                d21_Illegal, d22_AboriginalNo, d22_AboriginalYes, e23_ConflictsNo, e23_ConflictsYes, e23_Conflicts, AppStatus, g_HOS_StaffID, RejComment 
                                FROM application LEFT JOIN projtypelist ON application.a2_ProjType = projtypelist.ID LEFT JOIN 
                                (staff LEFT JOIN schoollist ON staff.School = schoollist.ID) ON application.a4_InvestStaffID = staff.StaffID 
                                WHERE application.AppID = " + Context.Request["AppID"];
        MySqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows) //Populate page with data.
        {
            reader.Read();
            if (Convert.ToInt32(reader["NS3_3_Yes"]) != 0)
                intTherapy_0.Visible = true;
            if (Convert.ToInt32(reader["NS3_3_No"]) != 0)
                intTherapy_1.Visible = true;
            if (Convert.ToInt32(reader["NS3_5_Yes"]) != 0)
                genetics_0.Visible = true;
            if (Convert.ToInt32(reader["NS3_5_No"]) != 0)
                genetics_1.Visible = true;
            if (Convert.ToInt32(reader["NS4_1_Yes"]) != 0)
                pregnancy_0.Visible = true;
            if (Convert.ToInt32(reader["NS4_1_No"]) != 0)
                pregnancy_1.Visible = true;
            if (Convert.ToInt32(reader["NS4_34_Yes"]) != 0)
                medConsent_0.Visible = true;
            if (Convert.ToInt32(reader["NS4_34_No"]) != 0)
                medConsent_1.Visible = true;
            if (Convert.ToInt32(reader["NS4_5_Yes"]) != 0)
                menDisability_0.Visible = true;
            if (Convert.ToInt32(reader["NS4_5_No"]) != 0)
                menDisability_1.Visible = true;
            if (Convert.ToInt32(reader["NS4_7_Yes"]) != 0)
                indigenous_0.Visible = true;
            if (Convert.ToInt32(reader["NS4_7_No"]) != 0)
                indigenous_1.Visible = true;
            if (Convert.ToInt32(reader["NS4_6_Yes"]) != 0)
                illegalAct_0.Visible = true;
            if (Convert.ToInt32(reader["NS4_6_No"]) != 0)
                illegalAct_1.Visible = true;
            txtYesResp.InnerText = reader["NotLowRisk"].ToString();

            projectTitle.InnerText = reader["a1_ProjTitle"].ToString();
            if (reader["a2_ProjType"].ToString() != "")
                txtProjType.InnerText = reader["ProjType"].ToString();
            projectTypeOther.InnerText = reader["a2_ProjTypeOther"].ToString();
            projectBackground.InnerText = reader["a3a_Background"].ToString();
            projectAim.InnerText = reader["a3b_AimsHypo"].ToString();
            projectMethod.InnerText = reader["a3c_Methods"].ToString();
            piName.InnerText = reader["FullName"].ToString();
            piStaffId.InnerText = reader["CurtinID"].ToString();
            integrityCert.Visible = Convert.ToBoolean(reader["IntegTraining"]);
            piSchool.InnerText = reader["School"].ToString();
            piEmail.InnerText = reader["Email"].ToString();
            piPhone.InnerText = reader["Phone"].ToString();

            potHarmText.InnerText = reader["b7_PotRisk"].ToString();
            riskManText.InnerText = reader["b8_RiskMan"].ToString();
            incentiveRadioNo.Visible = Convert.ToBoolean(reader["b9_FinanceNo"]);
            incentiveRadioYes.Visible = Convert.ToBoolean(reader["b9_FinanceYes"]);
            financeText.InnerText = reader["b9_Finance"].ToString();
            database.Visible = Convert.ToBoolean(reader["b10_DBChk"]);
            databaseText.InnerText = reader["b10_DB"].ToString();
            wordMouth.Visible = Convert.ToBoolean(reader["b10_SnowRecChk"]);
            wordMouthText.InnerText = reader["b10_SnowRec"].ToString();
            socialMedia.Visible = Convert.ToBoolean(reader["b10_SocialMedChk"]);
            socialMediaText.InnerText = reader["b10_SocialMed"].ToString();
            printMedia.Visible = Convert.ToBoolean(reader["b10_PrintChk"]);
            printMediaText.InnerText = reader["b10_Print"].ToString();
            commGroups.Visible = Convert.ToBoolean(reader["b10_ClassChk"]);
            commGroupsText.InnerText = reader["b10_ClassRm"].ToString();
            radioTV.Visible = Convert.ToBoolean(reader["b10_RadioChk"]);
            radioTVText.InnerText = reader["b10_Radio"].ToString();
            recruitOther.Visible = Convert.ToBoolean(reader["b10_OtherChk"]);
            recruitOtherText.InnerText = reader["b10_Other"].ToString();
            recruitTxt.InnerText = reader["b10_Descr"].ToString();
            consentRadioNo.Visible = Convert.ToBoolean(reader["b11_ConsentNo"]);
            consentRadioYes.Visible = Convert.ToBoolean(reader["b11_ConsentYes"]);
            consentText.InnerText = reader["b11_Consent"].ToString();
            deceptionRadioNo.Visible = Convert.ToBoolean(reader["b12_DeceptionNo"]);
            deceptionRadioYes.Visible = Convert.ToBoolean(reader["b12_DeceptionYes"]);
            deceptionText.InnerText = reader["b12_Deception"].ToString();

            clinicalRadioNo.Visible = Convert.ToBoolean(reader["c13_ClinicalNo"]);
            clinicalRadioYes.Visible = Convert.ToBoolean(reader["c13_ClinicalYes"]);
            healthInfoRadioNo.Visible = Convert.ToBoolean(reader["c14_HealthNo"]);
            healthInfoRadioYes.Visible = Convert.ToBoolean(reader["c14_HealthYes"]);
            healthInfoText.InnerText = reader["c14_Health"].ToString();
            humanGenRadioNo.Visible = Convert.ToBoolean(reader["c15_GeneticsNo"]);
            humanGenRadioYes.Visible = Convert.ToBoolean(reader["c15_GeneticsYes"]);
            humanGenText.InnerText = reader["c15_Genetics"].ToString();

            ethicPregnantRadioNo.Visible = Convert.ToBoolean(reader["d16_PregnantNo"]);
            ethicPregnantRadioYes.Visible = Convert.ToBoolean(reader["d16_PregnantYes"]);
            ethicYoungRadioNo.Visible = Convert.ToBoolean(reader["d17_ChildrenNo"]);
            ethicYoungRadioYes.Visible = Convert.ToBoolean(reader["d17_ChildrenYes"]);
            ethicUnequalRelRadioYes.Visible = Convert.ToBoolean(reader["d18_RelationshipsYes"]);
            ethicUnequalRelRadioNo.Visible = Convert.ToBoolean(reader["d18_RelationshipsNo"]);
            ethicUnequalRel.InnerText = reader["d18_Relationships"].ToString();
            ethicUnableConsentRadioNo.Visible = Convert.ToBoolean(reader["d19_MedCareNo"]);
            ethicUnableConsentRadioYes.Visible = Convert.ToBoolean(reader["d19_MedCareYes"]);
            ethicUnableConsent.InnerText = reader["d19_MedCare"].ToString();
            ethicCogImpairmentRadioNo.Visible = Convert.ToBoolean(reader["d20_ImpairmentNo"]);
            ethicCogImpairmentRadioYes.Visible = Convert.ToBoolean(reader["d20_ImpairmentYes"]);
            ethicCogImpairment.InnerText = reader["d20_Impairment"].ToString();
            ethicIllegalActRadioNo.Visible = Convert.ToBoolean(reader["d21_IllegalNo"]);
            ethicIllegalActRadioYes.Visible = Convert.ToBoolean(reader["d21_IllegalYes"]);
            ethicIllegal.InnerText = reader["d21_Illegal"].ToString();
            ethicAboriginalTorresRadioNo.Visible = Convert.ToBoolean(reader["d22_AboriginalNo"]);
            ethicAboriginalTorresRadioYes.Visible = Convert.ToBoolean(reader["d22_AboriginalYes"]);

            ethicConflictInterestRadioNo.Visible = Convert.ToBoolean(reader["e23_ConflictsNo"]);
            ethicConflictInterestRadioYes.Visible = Convert.ToBoolean(reader["e23_ConflictsYes"]);
            txtConflict.InnerText = reader["e23_Conflicts"].ToString();

            //Display reject comment if applicable.
            int appStatus = Convert.ToInt32(reader["AppStatus"]);
            if (appStatus == 0 || appStatus == 2 || appStatus == 5)
                radioDeclare.Visible = false;
            if (appStatus == 2) //Display rejection comment if status is HOS rejected.
            {
                tblDec.Visible = true;
                lblDeclined.InnerText = "HOS declined comment:";
            }
            else if (appStatus == 7) //Display rejection comment if status is declined.
            {
                tblDec.Visible = true;
                lblDeclined.InnerText = "Declined comment:";
            }
            else if (appStatus == 5) //Display rejection comment if status is incomplete.
            {
                tblDec.Visible = true;
                lblDeclined.InnerText = "Incomplete comment:";
            }
            txtDeclined.InnerText = reader["RejComment"].ToString();

            //Get staff ID's to display names.
            int contactID = Convert.ToInt32(reader["a6_ContactStaffID"]);
            int ownerID = Convert.ToInt32(reader["Ownership_StaffID"]);
            int hosID = 0;
            if (reader["g_HOS_StaffID"].ToString() != "") //Check for null.
                hosID = Convert.ToInt32(reader["g_HOS_StaffID"]);

            reader.Dispose();

            //Populate page with data from Clinical Trial table (If data exists).
            command.CommandText = @"SELECT Count(*) FROM c13_ClinicalTrial WHERE AppID = " + Context.Request["AppID"];
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                if (Convert.ToInt32(reader[0]) == 1) //If data exists.
                {
                    reader.Dispose();
                    command.CommandText = @"SELECT c13a_PlaceboNo, c13a_PlaceboYes, c13a_Placebo, c13b_RegisteredNo, c13b_RegisteredYes, c13b_Registered, c13c_SafeConductNo,
                                            c13c_SafeConductYes, c13c_SafeConduct, c13d_ContAccessNo, c13d_ContAccessYes FROM c13_ClinicalTrial 
                                            WHERE AppID = " + Context.Request["AppID"];
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        placeboRadioNo.Visible = Convert.ToBoolean(reader["c13a_PlaceboNo"]);
                        placeboRadioYes.Visible = Convert.ToBoolean(reader["c13a_PlaceboYes"]);
                        placeboText.InnerText = reader["c13a_Placebo"].ToString();
                        trialRegRadioNo.Visible = Convert.ToBoolean(reader["c13b_RegisteredNo"]);
                        trialRegRadioYes.Visible = Convert.ToBoolean(reader["c13b_RegisteredYes"]);
                        trialRegDetails.InnerText = reader["c13b_Registered"].ToString();
                        trialResRadioNo.Visible = Convert.ToBoolean(reader["c13c_SafeConductNo"]);
                        trialResRadioYes.Visible = Convert.ToBoolean(reader["c13c_SafeConductYes"]);
                        trialResText.InnerText = reader["c13c_SafeConduct"].ToString();
                        trialTreatRadioNo.Visible = Convert.ToBoolean(reader["c13d_ContAccessNo"]);
                        trialTreatRadioYes.Visible = Convert.ToBoolean(reader["c13d_ContAccessYes"]);
                    }
                }
            }
            reader.Dispose();

            //Populate page with data from Pregnant table (If data exists).
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
                        ethicPregnantWellRadioNo.Visible = Convert.ToBoolean(reader["d16a_CareNo"]);
                        ethicPregnantWellRadioYes.Visible = Convert.ToBoolean(reader["d16a_CareYes"]);
                        ethicPregnantWell.InnerText = reader["d16a_Care"].ToString();
                        ethicPregnantInfoRadioNo.Visible = Convert.ToBoolean(reader["d16b_SeparationNo"]);
                        ethicPregnantInfoRadioYes.Visible = Convert.ToBoolean(reader["d16b_SeparationYes"]);
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
                        ethicWWCRadioNo.Visible = Convert.ToBoolean(reader["d17a_WWCNo"]);
                        ethicWWCRadioYes.Visible = Convert.ToBoolean(reader["d17a_WWCYes"]);
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
                        ethicAboriginalRecordRadioNo.Visible = Convert.ToBoolean(reader["d22b_RecordedNo"]);
                        ethicAboriginalRecordRadioYes.Visible = Convert.ToBoolean(reader["d22b_RecordedYes"]);
                        ethicAboriginalRecord.InnerText = reader["d22b_Recorded"].ToString();
                    }
                }
            }
            reader.Dispose();

            //Display PI, Contact and HOS names.
            command.CommandText = @"SELECT CONCAT(IFNULL(staff.NameLast,''),', ',IFNULL(staff.NameFirst,'')) AS FullName FROM staff WHERE StaffID = " + contactID;
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                txtContact.InnerText = reader["FullName"].ToString();
            }
            reader.Dispose();
            command.CommandText = @"SELECT CONCAT(IFNULL(staff.NameLast,''),', ',IFNULL(staff.NameFirst,'')) AS FullName FROM staff WHERE StaffID = " + ownerID;
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                txtControl.InnerText = reader["FullName"].ToString();
            }
            if (hosID > 0) //If HOS has been selected.
            {
                reader.Dispose();
                command.CommandText = @"SELECT CONCAT(IFNULL(staff.NameLast,''),', ',IFNULL(staff.NameFirst,'')) AS FullName FROM staff WHERE StaffID = " + hosID;
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    txtHOS.InnerText = reader["FullName"].ToString();
                }
            }
        }
        reader.Dispose();

        buildReadCITable(mySqlConnection); //Populate CI table.
        buildUploadTbl(mySqlConnection); //Populate Uploads table.
        mySqlConnection.Close();
    }

    //Build list of CI's.
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
                string chkCand = "";
                string chkIntg = "";

                if (Convert.ToBoolean(reader["CandidacyAppr"]))
                    chkCand = "x";
                if (Convert.ToBoolean(reader["IntegTraining"]))
                    chkIntg = "x";

                HtmlTableRow row = new HtmlTableRow();
                row.Cells.Add(HTMLFactory.buildCell("", "left", reader["FullName"].ToString()));
                row.Cells.Add(HTMLFactory.buildCell("", "left", reader["Role"].ToString()));
                row.Cells.Add(HTMLFactory.buildCell("", "center", chkCand));
                row.Cells.Add(HTMLFactory.buildCell("", "center", chkIntg));

                tblCI.Rows.Insert(1, row);
            }
        }
        else
        {
            if (tblCI.Rows.Count != 0)
            {
                HtmlTableRow blnkRow = new HtmlTableRow();
                HtmlTableCell blnk = new HtmlTableCell();
                blnk.InnerText = "None";
                blnk.ColSpan = 4;
                blnkRow.Cells.Add(blnk);
                tblCI.Rows.Add(blnkRow);
            }
        }
        reader.Dispose();
    }

    //Build list of files uploaded.
    private void buildUploadTbl(MySqlConnection mySqlConnection)
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

                row.Cells.Add(statusCell);
                row.Cells.Add(HTMLFactory.buildCell("200", "left", reader["AttchType"].ToString()));
                row.Cells.Add(HTMLFactory.buildCell("97", "left", reader["FileVersion"].ToString()));
                row.Cells.Add(HTMLFactory.buildCell("97", "center", reader["FileDate"].ToString()));

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
                blnk.ColSpan = 4;
                blnkRow.Cells.Add(blnk);
                tblUploads.Rows.Add(blnkRow);
            }
        }
        reader.Dispose();
    }

    //Override required to create PDF.
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    //Convert to PDF using iTextSharp PDF Library. Based on code from:
    //http://simplyaspnet.blogspot.in/2013/09/how-to-export-html-page-to-pdf-using.html
    public void btnPDF_ServerClick(object sender, EventArgs e)
    {
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=ConvertedPDF.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        this.Page.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);

        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
    }
}