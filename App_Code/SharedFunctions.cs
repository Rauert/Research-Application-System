using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

/**
 * Contains miscellaneous functions common to multiple pages.
 */
public class SharedFunctions
{
	public SharedFunctions() {}

    //Returns the appropriate main page address for the user.
    //Used when logging into the system and when returning from the Edit Pages to the main menu.
    public static string getMainPageAddr(int accountType, int staffID)
    {
        String nextAddr = "";
        switch (accountType)
        {
            case 0: //Investigator
                nextAddr = "EthicsInvestMain.aspx?StaffID=";
                break;
            case 1: //HOS
                nextAddr = "EthicsHOSMain.aspx?StaffID=";
                break;
            case 2: //ESO
                nextAddr = "EthicsESOMain.aspx?StaffID=";
                break;
            case 3: //EO
                nextAddr = "EthicsEOMain.aspx?StaffID=";
                break;
            case 4: //Admin
                nextAddr = "EthicsAdminMain.aspx?StaffID=";
                break;
        }
        nextAddr += staffID + "&Type=" + accountType;
        return nextAddr;
    }

    //Returns the appropriate section address of the application based on the button clicked in the navigation pane.
    public static string getEditAppPageAddr(char pageRef, string mode, int appID, int staffID, int type)
    {
        string nextAddr = "";
        switch (pageRef)
        {
            case 'e': //Triage Section.
                nextAddr = "EthicsEditAppTriage.aspx?Mode=";
                break;
            case '1': //Section 1.
                nextAddr = "EthicsEditAppS1.aspx?Mode=";
                break;
            case '2': //Section 2.
                nextAddr = "EthicsEditAppS2.aspx?Mode=";
                break;
            case '3': //Section 3.
                nextAddr = "EthicsEditAppS3.aspx?Mode=";
                break;
            case '4': //Section 4.
                nextAddr = "EthicsEditAppS4.aspx?Mode=";
                break;
            case '5': //Section 5.
                nextAddr = "EthicsEditAppS5.aspx?Mode=";
                break;
            case '6': //Section 6.
                nextAddr = "EthicsEditAppS6.aspx?Mode=";
                break;
            case '7': //Section 7.
                nextAddr = "EthicsEditAppS7.aspx?Mode=";
                break;
            case 'n': //Section Admin.
                nextAddr = "EthicsEditAppAdmin.aspx?Mode=";
                break;
        }
        nextAddr += mode + "&AppID=" + appID + "&StaffID=" + staffID + "&Type=" + type;
        return nextAddr;
    }

    //Returns the result of all of the section validation functions below when loading an ethics application section.
    //Used to display which sections/questions are incomplete.
    public static bool[][] validateApplication(int appID)
    {
        bool[][] rtn = new bool[8][];
        rtn[0] = validateTriage(appID);
        rtn[1] = validateS1(appID);
        rtn[2] = validateS2(appID);
        rtn[3] = validateS3(appID);
        rtn[4] = validateS4(appID);
        rtn[5] = validateS5(appID);
        rtn[6] = validateS6(appID);
        rtn[7] = validateS7(appID);
        return rtn;
    }

    //Returns validation of triage section and questions as a bool array.
    public static bool[] validateTriage(int appID)
    {
        MySql.Data.MySqlClient.MySqlConnection mySqlConnection = new Connector().MySQLConnect();
        MySqlCommand command = mySqlConnection.CreateCommand();
        bool[] rtn = new bool[9];
        for (int i = 0; i < rtn.Length; i++)
        {
            rtn[i] = true; //Initialize.
        }
        command.CommandText = @"SELECT NS3_3_Yes, NS3_3_No, NS3_5_Yes, NS3_5_No, NS4_1_Yes, NS4_1_No, NS4_34_Yes, NS4_34_No, NS4_5_Yes, NS4_5_No, NS4_7_Yes, NS4_7_No, NS4_6_Yes, NS4_6_No, NotLowRisk 
                                FROM Application WHERE AppID = " + appID;
        MySqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            reader.Read();
            if ((Convert.ToInt32(reader["NS3_3_Yes"]) == 1 && Convert.ToInt32(reader["NS3_3_No"]) == 1) || (Convert.ToInt32(reader["NS3_3_Yes"]) == 0 && Convert.ToInt32(reader["NS3_3_No"]) == 0))
            {
                rtn[0] = false; //Section not complete.
                rtn[1] = false; //NS3_3 not complete.
            }
            if ((Convert.ToInt32(reader["NS3_5_Yes"]) == 1 && Convert.ToInt32(reader["NS3_5_No"]) == 1) || (Convert.ToInt32(reader["NS3_5_Yes"]) == 0 && Convert.ToInt32(reader["NS3_5_No"]) == 0))
            {
                rtn[0] = false; //Section not complete.
                rtn[2] = false; //NS3_5 not complete.
            }
            if ((Convert.ToInt32(reader["NS4_1_Yes"]) == 1 && Convert.ToInt32(reader["NS4_1_No"]) == 1) || (Convert.ToInt32(reader["NS4_1_Yes"]) == 0 && Convert.ToInt32(reader["NS4_1_No"]) == 0))
            {
                rtn[0] = false; //Section not complete.
                rtn[3] = false; //NS4_1 not complete.
            }
            if ((Convert.ToInt32(reader["NS4_34_Yes"]) == 1 && Convert.ToInt32(reader["NS4_34_No"]) == 1) || (Convert.ToInt32(reader["NS4_34_Yes"]) == 0 && Convert.ToInt32(reader["NS4_34_No"]) == 0))
            {
                rtn[0] = false; //Section not complete.
                rtn[4] = false; //NS4_3/4 not complete.
            }
            if ((Convert.ToInt32(reader["NS4_5_Yes"]) == 1 && Convert.ToInt32(reader["NS4_5_No"]) == 1) || (Convert.ToInt32(reader["NS4_5_Yes"]) == 0 && Convert.ToInt32(reader["NS4_5_No"]) == 0))
            {
                rtn[0] = false; //Section not complete.
                rtn[5] = false; //NS4_5 not complete.
            }
            if ((Convert.ToInt32(reader["NS4_7_Yes"]) == 1 && Convert.ToInt32(reader["NS4_7_No"]) == 1) || (Convert.ToInt32(reader["NS4_7_Yes"]) == 0 && Convert.ToInt32(reader["NS4_7_No"]) == 0))
            {
                rtn[0] = false; //Section not complete.
                rtn[6] = false; //NS4_7 not complete.
            }
            if ((Convert.ToInt32(reader["NS4_6_Yes"]) == 1 && Convert.ToInt32(reader["NS4_6_No"]) == 1) || (Convert.ToInt32(reader["NS4_6_Yes"]) == 0 && Convert.ToInt32(reader["NS4_6_No"]) == 0))
            {
                rtn[0] = false; //Section not complete.
                rtn[7] = false; //NS4_6 not complete.
            }
            if (reader["NotLowRisk"].ToString() == "" && (Convert.ToInt32(reader["NS3_3_Yes"]) == 1 || Convert.ToInt32(reader["NS3_5_Yes"]) == 1 || Convert.ToInt32(reader["NS4_1_Yes"]) == 1 ||
                Convert.ToInt32(reader["NS4_34_Yes"]) == 1 || Convert.ToInt32(reader["NS4_5_Yes"]) == 1 || Convert.ToInt32(reader["NS4_7_Yes"]) == 1 || Convert.ToInt32(reader["NS4_6_Yes"]) == 1))
            {
                rtn[0] = false; //Section not complete.
                rtn[8] = false; //NotLowRisk text required and not complete.
            }
        }
        reader.Dispose();
        command.Dispose();
        mySqlConnection.Close();
        return rtn;
    }

    //Returns validation of section 1 and its questions as a bool array.
    public static bool[] validateS1(int appID)
    {
        MySql.Data.MySqlClient.MySqlConnection mySqlConnection = new Connector().MySQLConnect();
        MySqlCommand command = mySqlConnection.CreateCommand();
        bool[] rtn = new bool[6];
        for (int i = 0; i < rtn.Length; i++)
        {
            rtn[i] = true; //Initialize.
        }
        command.CommandText = @"SELECT application.a1_ProjTitle, application.a2_ProjType, application.a2_ProjTypeOther, application.a3a_Background, application.a3b_AimsHypo, application.a3c_Methods, 
                                application.a4_InvestStaffID, staff.IntegTraining 
                                FROM application LEFT JOIN staff ON application.a4_InvestStaffID = staff.StaffID WHERE application.AppID = " + appID;
        MySqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            reader.Read();
            if (reader["a1_ProjTitle"].ToString() == "")
            {
                rtn[0] = false; //Section not complete.
                rtn[1] = false; //a1_ProjTitle not complete.
            }
            if (reader["a2_ProjType"].ToString() == "")
            {
                rtn[0] = false; //Section not complete.
                rtn[2] = false; //a2_ProjType not complete.
            }
            if (reader["a2_ProjType"].ToString() != "" && Convert.ToInt32(reader["a2_ProjType"]) == 6 && reader["a2_ProjTypeOther"].ToString() == "")
            {
                rtn[0] = false; //Section not complete.
                rtn[2] = false; //a2_ProjType not complete.
            }
            if (reader["a3a_Background"].ToString() == "")
            {
                rtn[0] = false; //Section not complete.
                rtn[3] = false; //a3a_Background not complete.
            }
            if (reader["a3b_AimsHypo"].ToString() == "")
            {
                rtn[0] = false; //Section not complete.
                rtn[4] = false; //a3b_AimsHypo not complete.
            }
            if (reader["a3c_Methods"].ToString() == "")
            {
                rtn[0] = false; //Section not complete.
                rtn[5] = false; //a3c_Methods not complete.
            }
        }
        reader.Dispose();
        command.Dispose();
        mySqlConnection.Close();
        return rtn;
    }

    //Returns validation of section 2 and its questions as a bool array.
    public static bool[] validateS2(int appID)
    {
        MySql.Data.MySqlClient.MySqlConnection mySqlConnection = new Connector().MySQLConnect();
        MySqlCommand command = mySqlConnection.CreateCommand();
        bool[] rtn = new bool[7];
        for (int i = 0; i < rtn.Length; i++)
        {
            rtn[i] = true; //Initialize.
        }
        command.CommandText = @"SELECT b7_PotRisk, b8_RiskMan, b9_FinanceNo, b9_FinanceYes, b9_Finance, b10_DB, b10_SocialMed, b10_ClassRm, b10_SnowRec, b10_Print, b10_Radio, b10_Other, b10_Descr, 
                                    b10_DBChk, b10_SocialMedChk, b10_ClassChk, b10_SnowRecChk, b10_PrintChk, b10_RadioChk, b10_OtherChk, b11_ConsentNo, b11_ConsentYes, b11_Consent, b12_DeceptionNo, 
                                    b12_DeceptionYes, b12_Deception FROM Application WHERE AppID = " + appID;
        MySqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            reader.Read();
            if (reader["b7_PotRisk"].ToString() == "")
            {
                rtn[0] = false; //Section not complete.
                rtn[1] = false; //b7 not complete.
            }
            if (reader["b8_RiskMan"].ToString() == "")
            {
                rtn[0] = false; //Section not complete.
                rtn[2] = false; //b8 not complete.
            }
            if ((Convert.ToInt32(reader["b9_FinanceNo"]) == 1 && Convert.ToInt32(reader["b9_FinanceYes"]) == 1) || (Convert.ToInt32(reader["b9_FinanceNo"]) == 0 && Convert.ToInt32(reader["b9_FinanceYes"]) == 0))
            {
                rtn[0] = false; //Section not complete.
                rtn[3] = false; //b9 not complete.
            }
            if (Convert.ToBoolean(reader["b9_FinanceYes"]) == true && reader["b9_Finance"].ToString() == "")
            {
                rtn[0] = false; //Section not complete.
                rtn[3] = false; //b9 text required and not complete.
            }
            if ((Convert.ToInt32(reader["b10_DBChk"]) == 0 && reader["b10_DB"].ToString() == "") && (Convert.ToInt32(reader["b10_SocialMedChk"]) == 0 && reader["b10_SocialMed"].ToString() == "") &&
                (Convert.ToInt32(reader["b10_ClassChk"]) == 0 && reader["b10_ClassRm"].ToString() == "") && (Convert.ToInt32(reader["b10_SnowRecChk"]) == 0 && reader["b10_SnowRec"].ToString() == "") &&
                (Convert.ToInt32(reader["b10_PrintChk"]) == 0 && reader["b10_Print"].ToString() == "") && (Convert.ToInt32(reader["b10_RadioChk"]) == 0 && reader["b10_Radio"].ToString() == "") &&
                (Convert.ToInt32(reader["b10_OtherChk"]) == 0 && reader["b10_Other"].ToString() == "") && reader["b10_Descr"].ToString() == "" ||
                (Convert.ToInt32(reader["b10_DBChk"]) == 0 && reader["b10_DB"].ToString() != "") || (Convert.ToInt32(reader["b10_SocialMedChk"]) == 0 && reader["b10_SocialMed"].ToString() != "") ||
                (Convert.ToInt32(reader["b10_ClassChk"]) == 0 && reader["b10_ClassRm"].ToString() != "") || (Convert.ToInt32(reader["b10_SnowRecChk"]) == 0 && reader["b10_SnowRec"].ToString() != "") ||
                (Convert.ToInt32(reader["b10_PrintChk"]) == 0 && reader["b10_Print"].ToString() != "") || (Convert.ToInt32(reader["b10_RadioChk"]) == 0 && reader["b10_Radio"].ToString() != "") ||
                (Convert.ToInt32(reader["b10_OtherChk"]) == 0 && reader["b10_Other"].ToString() != "") ||
                (Convert.ToInt32(reader["b10_DBChk"]) == 1 && reader["b10_DB"].ToString() == "") || (Convert.ToInt32(reader["b10_SocialMedChk"]) == 1 && reader["b10_SocialMed"].ToString() == "") ||
                (Convert.ToInt32(reader["b10_ClassChk"]) == 1 && reader["b10_ClassRm"].ToString() == "") || (Convert.ToInt32(reader["b10_SnowRecChk"]) == 1 && reader["b10_SnowRec"].ToString() == "") ||
                (Convert.ToInt32(reader["b10_PrintChk"]) == 1 && reader["b10_Print"].ToString() == "") || (Convert.ToInt32(reader["b10_RadioChk"]) == 1 && reader["b10_Radio"].ToString() == "") ||
                (Convert.ToInt32(reader["b10_OtherChk"]) == 1 && reader["b10_Other"].ToString() == ""))
            {
                rtn[0] = false; //Section not complete.
                rtn[4] = false; //No valid entry in b10.
            }
            if ((Convert.ToInt32(reader["b11_ConsentNo"]) == 1 && Convert.ToInt32(reader["b11_ConsentYes"]) == 1) || (Convert.ToInt32(reader["b11_ConsentNo"]) == 0 && Convert.ToInt32(reader["b11_ConsentYes"]) == 0))
            {
                rtn[0] = false; //Section not complete.
                rtn[5] = false; //b11 not complete.
            }
            if (reader["b11_Consent"].ToString() == "")
            {
                rtn[0] = false; //Section not complete.
                rtn[5] = false; //b11 text not complete.
            }
            if ((Convert.ToInt32(reader["b12_DeceptionNo"]) == 1 && Convert.ToInt32(reader["b12_DeceptionYes"]) == 1) || (Convert.ToInt32(reader["b12_DeceptionNo"]) == 0 && Convert.ToInt32(reader["b12_DeceptionYes"]) == 0))
            {
                rtn[0] = false; //Section not complete.
                rtn[6] = false; //b12 not complete.
            }
            if (Convert.ToBoolean(reader["b12_DeceptionYes"]) == true && reader["b12_Deception"].ToString() == "")
            {
                rtn[0] = false; //Section not complete.
                rtn[6] = false; //b12 text required and not complete.
            }
        }
        reader.Dispose();
        command.Dispose();
        mySqlConnection.Close();
        return rtn;
    }

    //Returns validation of section 3 and its questions as a bool array.
    public static bool[] validateS3(int appID)
    {
        MySql.Data.MySqlClient.MySqlConnection mySqlConnection = new Connector().MySQLConnect();
        MySqlCommand command = mySqlConnection.CreateCommand();
        bool[] rtn = new bool[8];
        for (int i = 0; i < rtn.Length; i++)
        {
            rtn[i] = true; //Initialize.
        }
        command.CommandText = @"SELECT c13_ClinicalNo, c13_ClinicalYes, c14_HealthNo, c14_HealthYes, c14_Health, c15_GeneticsNo, c15_GeneticsYes, c15_Genetics 
                                FROM Application WHERE AppID = " + appID;
        MySqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows) //Populate page with data.
        {
            reader.Read();
            if ((Convert.ToInt32(reader["c13_ClinicalNo"]) == 1 && Convert.ToInt32(reader["c13_ClinicalYes"]) == 1) || (Convert.ToInt32(reader["c13_ClinicalNo"]) == 0 && Convert.ToInt32(reader["c13_ClinicalYes"]) == 0))
            {
                rtn[0] = false; //Section not complete.
                rtn[1] = false; //c13 not complete.
            }
            if ((Convert.ToInt32(reader["c14_HealthNo"]) == 1 && Convert.ToInt32(reader["c14_HealthYes"]) == 1) || (Convert.ToInt32(reader["c14_HealthNo"]) == 0 && Convert.ToInt32(reader["c14_HealthYes"]) == 0))
            {
                rtn[0] = false; //Section not complete.
                rtn[6] = false; //c14 not complete.
            }
            if (Convert.ToBoolean(reader["c14_HealthYes"]) == true && reader["c14_Health"].ToString() == "")
            {
                rtn[0] = false; //Section not complete.
                rtn[6] = false; //c14 text required and not complete.
            }
            if ((Convert.ToInt32(reader["c15_GeneticsNo"]) == 1 && Convert.ToInt32(reader["c15_GeneticsYes"]) == 1) || (Convert.ToInt32(reader["c15_GeneticsNo"]) == 0 && Convert.ToInt32(reader["c15_GeneticsYes"]) == 0))
            {
                rtn[0] = false; //Section not complete.
                rtn[7] = false; //c15 not complete.
            }
            if (Convert.ToBoolean(reader["c15_GeneticsYes"]) == true && reader["c15_Genetics"].ToString() == "")
            {
                rtn[0] = false; //Section not complete.
                rtn[7] = false; //c15 text required and not complete.
            }
            reader.Dispose();

            command.CommandText = @"SELECT Count(*) FROM c13_ClinicalTrial WHERE AppID = " + appID;
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                if (Convert.ToInt32(reader[0]) == 1) //If data exists.
                {
                    reader.Dispose();
                    command.CommandText = @"SELECT c13a_PlaceboNo, c13a_PlaceboYes, c13a_Placebo, c13b_RegisteredNo, c13b_RegisteredYes, c13b_Registered, c13c_SafeConductNo,
                                            c13c_SafeConductYes, c13c_SafeConduct, c13d_ContAccessNo, c13d_ContAccessYes FROM c13_ClinicalTrial 
                                            WHERE AppID = " + appID;
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        if ((Convert.ToInt32(reader["c13a_PlaceboNo"]) == 1 && Convert.ToInt32(reader["c13a_PlaceboYes"]) == 1) || (Convert.ToInt32(reader["c13a_PlaceboNo"]) == 0 && Convert.ToInt32(reader["c13a_PlaceboYes"]) == 0))
                        {
                            rtn[0] = false; //Section not complete.
                            rtn[2] = false; //c13a not complete.
                        }
                        if (reader["c13a_Placebo"].ToString() == "")
                        {
                            rtn[0] = false; //Section not complete.
                            rtn[2] = false; //d13a text not complete.
                        }
                        if ((Convert.ToInt32(reader["c13b_RegisteredNo"]) == 1 && Convert.ToInt32(reader["c13b_RegisteredYes"]) == 1) || (Convert.ToInt32(reader["c13b_RegisteredNo"]) == 0 && Convert.ToInt32(reader["c13b_RegisteredYes"]) == 0))
                        {
                            rtn[0] = false; //Section not complete.
                            rtn[3] = false; //c13b not complete.
                        }
                        if (Convert.ToBoolean(reader["c13b_RegisteredYes"]) == true && reader["c13b_Registered"].ToString() == "")
                        {
                            rtn[0] = false; //Section not complete.
                            rtn[3] = false; //c13b text required and not complete.
                        }
                        if ((Convert.ToInt32(reader["c13c_SafeConductNo"]) == 1 && Convert.ToInt32(reader["c13c_SafeConductYes"]) == 1) || (Convert.ToInt32(reader["c13c_SafeConductNo"]) == 0 && Convert.ToInt32(reader["c13c_SafeConductYes"]) == 0))
                        {
                            rtn[0] = false; //Section not complete.
                            rtn[4] = false; //c13c not complete.
                        }
                        if (Convert.ToBoolean(reader["c13c_SafeConductNo"]) == true && reader["c13c_SafeConduct"].ToString() == "")
                        {
                            rtn[0] = false; //Section not complete.
                            rtn[4] = false; //c13c text required and not complete.
                        }
                        if ((Convert.ToInt32(reader["c13d_ContAccessNo"]) == 1 && Convert.ToInt32(reader["c13d_ContAccessYes"]) == 1) || (Convert.ToInt32(reader["c13d_ContAccessNo"]) == 0 && Convert.ToInt32(reader["c13d_ContAccessYes"]) == 0))
                        {
                            rtn[0] = false; //Section not complete.
                            rtn[5] = false; //c13d not complete.
                        }
                    }
                }
            }
        }
        reader.Dispose();
        mySqlConnection.Close();
        return rtn;
    }

    //Returns validation of section 4 and its questions as a bool array.
    public static bool[] validateS4(int appID)
    {
        MySql.Data.MySqlClient.MySqlConnection mySqlConnection = new Connector().MySQLConnect();
        MySqlCommand command = mySqlConnection.CreateCommand();
        bool[] rtn = new bool[13];
        for (int i = 0; i < rtn.Length; i++)
        {
            rtn[i] = true; //Initialize.
        }
        command.CommandText = @"SELECT d16_PregnantNo, d16_PregnantYes, d17_ChildrenNo, d17_ChildrenYes, d18_RelationshipsYes, d18_RelationshipsNo, d18_Relationships, d19_MedCareNo, 
                                d19_MedCareYes, d19_MedCare, d20_ImpairmentNo, d20_ImpairmentYes, d20_Impairment, d21_IllegalNo, d21_IllegalYes, d21_Illegal, d22_AboriginalNo, d22_AboriginalYes 
                                FROM Application WHERE AppID = " + appID;
        MySqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            reader.Read();
            if ((Convert.ToInt32(reader["d16_PregnantNo"]) == 1 && Convert.ToInt32(reader["d16_PregnantYes"]) == 1) || (Convert.ToInt32(reader["d16_PregnantNo"]) == 0 && Convert.ToInt32(reader["d16_PregnantYes"]) == 0))
            {
                rtn[0] = false; //Section not complete.
                rtn[1] = false; //d16 not complete.
            }
            if ((Convert.ToInt32(reader["d17_ChildrenNo"]) == 1 && Convert.ToInt32(reader["d17_ChildrenYes"]) == 1) || (Convert.ToInt32(reader["d17_ChildrenNo"]) == 0 && Convert.ToInt32(reader["d17_ChildrenYes"]) == 0))
            {
                rtn[0] = false; //Section not complete.
                rtn[4] = false; //d17 not complete.
            }
            if ((Convert.ToInt32(reader["d18_RelationshipsNo"]) == 1 && Convert.ToInt32(reader["d18_RelationshipsYes"]) == 1) || (Convert.ToInt32(reader["d18_RelationshipsNo"]) == 0 && Convert.ToInt32(reader["d18_RelationshipsYes"]) == 0))
            {
                rtn[0] = false; //Section not complete.
                rtn[6] = false; //d18 not complete.
            }
            if (Convert.ToBoolean(reader["d18_RelationshipsYes"]) == true && reader["d18_Relationships"].ToString() == "")
            {
                rtn[0] = false; //Section not complete.
                rtn[6] = false; //d18 text required and not complete.
            }
            if ((Convert.ToInt32(reader["d19_MedCareNo"]) == 1 && Convert.ToInt32(reader["d19_MedCareYes"]) == 1) || (Convert.ToInt32(reader["d19_MedCareNo"]) == 0 && Convert.ToInt32(reader["d19_MedCareYes"]) == 0))
            {
                rtn[0] = false; //Section not complete.
                rtn[7] = false; //d19 not complete.
            }
            if (Convert.ToBoolean(reader["d19_MedCareYes"]) == true && reader["d19_MedCare"].ToString() == "")
            {
                rtn[0] = false; //Section not complete.
                rtn[7] = false; //d19 text required and not complete.
            }
            if ((Convert.ToInt32(reader["d20_ImpairmentNo"]) == 1 && Convert.ToInt32(reader["d20_ImpairmentYes"]) == 1) || (Convert.ToInt32(reader["d20_ImpairmentNo"]) == 0 && Convert.ToInt32(reader["d20_ImpairmentYes"]) == 0))
            {
                rtn[0] = false; //Section not complete.
                rtn[8] = false; //d20 not complete.
            }
            if (Convert.ToBoolean(reader["d20_ImpairmentYes"]) == true && reader["d20_Impairment"].ToString() == "")
            {
                rtn[0] = false; //Section not complete.
                rtn[8] = false;  //d20 text required and not complete.
            }
            if ((Convert.ToInt32(reader["d21_IllegalNo"]) == 1 && Convert.ToInt32(reader["d21_IllegalYes"]) == 1) || (Convert.ToInt32(reader["d21_IllegalNo"]) == 0 && Convert.ToInt32(reader["d21_IllegalYes"]) == 0))
            {
                rtn[0] = false; //Section not complete.
                rtn[9] = false; //d21 not complete.
            }
            if (Convert.ToBoolean(reader["d21_IllegalYes"]) == true && reader["d21_Illegal"].ToString() == "")
            {
                rtn[0] = false; //Section not complete.
                rtn[9] = false; //d21 text required and not complete.
            }
            if ((Convert.ToInt32(reader["d22_AboriginalNo"]) == 1 && Convert.ToInt32(reader["d22_AboriginalYes"]) == 1) || (Convert.ToInt32(reader["d22_AboriginalNo"]) == 0 && Convert.ToInt32(reader["d22_AboriginalYes"]) == 0))
            {
                rtn[0] = false; //Section not complete.
                rtn[10] = false; //d22 not complete.
            }
            reader.Dispose();
            command.CommandText = @"SELECT Count(*) FROM d16_Pregnant WHERE AppID = " + appID;
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                if (Convert.ToInt32(reader[0]) == 1) //If data exists.
                {
                    reader.Dispose();
                    command.CommandText = @"SELECT d16a_CareNo, d16a_CareYes, d16a_Care, d16b_SeparationNo, d16b_SeparationYes, d16b_Separation FROM d16_Pregnant WHERE AppID = " + appID;
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        if ((Convert.ToInt32(reader["d16a_CareNo"]) == 1 && Convert.ToInt32(reader["d16a_CareYes"]) == 1) || (Convert.ToInt32(reader["d16a_CareNo"]) == 0 && Convert.ToInt32(reader["d16a_CareYes"]) == 0))
                        {
                            rtn[0] = false; //Section not complete.
                            rtn[2] = false; //d16a not complete.
                        }
                        if (reader["d16a_Care"].ToString() == "")
                        {
                            rtn[0] = false; //Section not complete.
                            rtn[2] = false; //d16a text not complete.
                        }
                        if ((Convert.ToInt32(reader["d16b_SeparationNo"]) == 1 && Convert.ToInt32(reader["d16b_SeparationYes"]) == 1) || (Convert.ToInt32(reader["d16b_SeparationNo"]) == 0 && Convert.ToInt32(reader["d16b_SeparationYes"]) == 0))
                        {
                            rtn[0] = false; //Section not complete.
                            rtn[3] = false; //d16b not complete.
                        }
                        if (Convert.ToBoolean(reader["d16b_SeparationNo"]) == true && reader["d16b_Separation"].ToString() == "")
                        {
                            rtn[0] = false; //Section not complete.
                            rtn[3] = false; //d16b text required and not complete.
                        }
                    }
                }
            }
            reader.Dispose();

            command.CommandText = @"SELECT Count(*) FROM d17_Children WHERE AppID = " + appID;
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                if (Convert.ToInt32(reader[0]) == 1) //If data exists.
                {
                    reader.Dispose();
                    command.CommandText = @"SELECT d17_Why, d17a_WWCNo, d17a_WWCYes FROM d17_Children WHERE AppID = " + appID;
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        if (reader["d17_Why"].ToString() == "")
                        {
                            rtn[0] = false; //Section not complete.
                            rtn[4] = false; //d17 text not complete.
                        }
                        if ((Convert.ToInt32(reader["d17a_WWCNo"]) == 1))
                        {
                            rtn[0] = false; //Section not complete.
                            rtn[5] = false; //d17a not complete. Must have a WWC card.
                        }
                        if ((Convert.ToInt32(reader["d17a_WWCNo"]) == 1 && Convert.ToInt32(reader["d17a_WWCYes"]) == 1) || (Convert.ToInt32(reader["d17a_WWCNo"]) == 0 && Convert.ToInt32(reader["d17a_WWCYes"]) == 0))
                        {
                            rtn[0] = false; //Section not complete.
                            rtn[5] = false; //d17a not complete.
                        }
                    }
                }
            }
            reader.Dispose();

            command.CommandText = @"SELECT Count(*) FROM d22_Aboriginal WHERE AppID = " + appID;
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                if (Convert.ToInt32(reader[0]) == 1) //If data exists.
                {
                    reader.Dispose();
                    command.CommandText = @"SELECT d22a_Proportion, d22b_RecordedNo, d22b_RecordedYes, d22b_Recorded FROM d22_Aboriginal WHERE AppID = " + appID;
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        if (reader["d22a_Proportion"].ToString() == "")
                        {
                            rtn[0] = false; //Section not complete.
                            rtn[11] = false; //d22a not complete.
                        }
                        if ((Convert.ToInt32(reader["d22b_RecordedNo"]) == 1 && Convert.ToInt32(reader["d22b_RecordedYes"]) == 1) || (Convert.ToInt32(reader["d22b_RecordedNo"]) == 0 && Convert.ToInt32(reader["d22b_RecordedYes"]) == 0))
                        {
                            rtn[0] = false; //Section not complete.
                            rtn[12] = false; //d22b not complete.
                        }
                        if (reader["d22b_Recorded"].ToString() == "")
                        {
                            rtn[0] = false; //Section not complete.
                            rtn[12] = false; //d22a text not complete.
                        }
                    }
                }
            }
        }
        reader.Dispose();
        command.Dispose();
        mySqlConnection.Close();
        return rtn;
    }

    //Returns validation of section 5 and its questions as a bool array.
    public static bool[] validateS5(int appID)
    {
        MySql.Data.MySqlClient.MySqlConnection mySqlConnection = new Connector().MySQLConnect();
        MySqlCommand command = mySqlConnection.CreateCommand();
        bool[] rtn = new bool[1];
        rtn[0] = true; //Initialize.
        command.CommandText = @"SELECT e23_ConflictsNo, e23_ConflictsYes, e23_Conflicts FROM Application WHERE AppID = " + appID;
        MySqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            reader.Read();
            if ((Convert.ToInt32(reader["e23_ConflictsNo"]) == 1 && Convert.ToInt32(reader["e23_ConflictsYes"]) == 1) || (Convert.ToInt32(reader["e23_ConflictsNo"]) == 0 && Convert.ToInt32(reader["e23_ConflictsYes"]) == 0))
                rtn[0] = false; //Section not complete.
            if (Convert.ToBoolean(reader["e23_ConflictsYes"]) == true && reader["e23_Conflicts"].ToString() == "")
                rtn[0] = false; //Section not complete.
        }
        reader.Dispose();
        command.Dispose();
        mySqlConnection.Close();
        return rtn;
    }

    //Returns validation of section 6 and its questions as a bool array.
    public static bool[] validateS6(int appID)
    {
        MySql.Data.MySqlClient.MySqlConnection mySqlConnection = new Connector().MySQLConnect();
        MySqlCommand command = mySqlConnection.CreateCommand();
        bool[] rtn = new bool[1];
        rtn[0] = true; //Initialize.
        command.CommandText = @"SELECT Count(*) FROM Attachments WHERE AppID = " + appID;
        MySqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            reader.Read();
            if (Convert.ToInt32(reader[0]) == 0) //If attachments exists.
            {
                rtn[0] = false; //Section not complete.
            }
        }
        reader.Dispose();
        command.Dispose();
        mySqlConnection.Close();
        return rtn;
    }

    //Returns validation of section 7 and its questions as a bool array.
    public static bool[] validateS7(int appID)
    {
        MySql.Data.MySqlClient.MySqlConnection mySqlConnection = new Connector().MySQLConnect();
        MySqlCommand command = mySqlConnection.CreateCommand();
        bool[] rtn = new bool[3];
        for (int i = 0; i < rtn.Length; i++)
        {
            rtn[i] = true; //Initialize.
        }
        command.CommandText = "SELECT AppStatus, g_HOS_StaffID FROM Application WHERE AppID = " + appID;
        MySqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            reader.Read();
            if (Convert.ToInt32(reader["AppStatus"]) == 0 || Convert.ToInt32(reader["AppStatus"]) == 2 || Convert.ToInt32(reader["AppStatus"]) == 5)
            {
                rtn[0] = false; //Section not complete.
                rtn[1] = false; //Declaration not complete.
            }
            if (reader["g_HOS_StaffID"].ToString() == "") //HOS null.
            {
                rtn[0] = false; //Section not complete.
                rtn[2] = false; //HOS not complete.
            }
            else if (Convert.ToInt32(reader["g_HOS_StaffID"]) == 0) //HOS set to '-' in form.
            {
                rtn[0] = false; //Section not complete.
                rtn[2] = false; //HOS not complete.
            }
        }
        reader.Dispose();
        command.Dispose();
        mySqlConnection.Close();
        return rtn;
    }
}