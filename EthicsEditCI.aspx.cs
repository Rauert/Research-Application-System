using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

/**
 * The CI edit page.
 * Allows the apllication controller to change the details of the CI.
 */
public partial class EthicsEditCI : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MySqlConnection mySqlConnection = new Connector().MySQLConnect();
            MySqlCommand command = mySqlConnection.CreateCommand();
            command.CommandText = @"SELECT CONCAT(IFNULL(staff.NameLast,''),', ',IFNULL(staff.NameFirst,'')) AS FullName, a5_coinvestigators.Role, a5_coinvestigators.CandidacyAppr, staff.IntegTraining 
                                    FROM a5_coinvestigators INNER JOIN staff ON a5_coinvestigators.StaffID = staff.StaffID 
                                    WHERE a5_coinvestigators.AppID = " + Context.Request["AppID"] + " AND staff.StaffID = " + Context.Request["CIStaffID"];
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows) //Populate page with data.
            {
                reader.Read();
                txtNm.Value = reader["FullName"].ToString();
                if (reader["Role"].ToString() != "")
                    sltNewCIRole.SelectedIndex = Convert.ToInt32(reader["Role"]);
                chkCand.Checked = Convert.ToBoolean(reader["CandidacyAppr"]);
                chkInteg.Checked = Convert.ToBoolean(reader["IntegTraining"]);
            }
            else
                divMsg.InnerText = "Application not found";

            reader.Dispose(); //Cleanup.
            command.Dispose();
            mySqlConnection.Close();
        }
    }

    public void btnBack_ServerClick(object sender, EventArgs e)
    {
        save();
        back();
    }

    public void btnSave_ServerClick(object sender, EventArgs e)
    {
        save();
    }

    //Save changes.
    private void save()
    {
        MySqlConnection mySqlConnection = new Connector().MySQLConnect();
        MySqlCommand command = mySqlConnection.CreateCommand();
        command.CommandText = @"UPDATE a5_coinvestigators SET Role = " + sltNewCIRole.SelectedIndex + ", CandidacyAppr = " + chkCand.Checked + 
                                " WHERE AppID = " + Context.Request["AppID"] + " AND StaffID = " + Context.Request["CIStaffID"];
        int status = command.ExecuteNonQuery();
        command.CommandText = @"UPDATE staff SET IntegTraining = " + chkInteg.Checked + " WHERE StaffID = " + Context.Request["CIStaffID"];
        int status2 = command.ExecuteNonQuery();
        command.Dispose();
        mySqlConnection.Close();
        if (status != 1 || status2 != 1)
        {
            divMsg.InnerText = "Error saving!";
        }
    }

    //Go back to application.
    private void back()
    {
        string nextAddr = SharedFunctions.getEditAppPageAddr('1', "W", Convert.ToInt32(Context.Request["AppID"]),
                                                             Convert.ToInt32(Context.Request["StaffID"]), Convert.ToInt32(Context.Request["Type"]));
        Response.Redirect(nextAddr);
    }
}