using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

/**
 * The coinvestigator declaration page.
 * Allows the CI to declare that they will undertake the research in accordance with the approved protocol.
 */
public partial class EthicsCIDeclaration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void btnBack_ServerClick(object sender, EventArgs e)
    {
        back();
    }

    //If declaration ticked update database.
    public void btnSubmit_ServerClick(object sender, EventArgs e)
    {
        MySqlConnection mySqlConnection = new Connector().MySQLConnect();
        MySqlCommand command = mySqlConnection.CreateCommand();
        if (radioDeclare.Checked)
        {
            command.CommandText = @"UPDATE a5_CoInvestigators SET Declaration = true WHERE AppID = " + Context.Request["AppID"] + " AND StaffID = " + Context.Request["StaffID"];
            int status = command.ExecuteNonQuery();

            if (status != 1)
                divMsg.InnerText = "Error saving!";
            else
            {
                command.Dispose();
                mySqlConnection.Close();
                back();
            }
        }
        else
            divMsg.InnerText = "Please check the confirm box.";
        command.Dispose();
        mySqlConnection.Close();
    }

    //Go back to main page.
    private void back()
    {
        String nextAddr = SharedFunctions.getMainPageAddr(Convert.ToInt32(Context.Request["Type"]), Convert.ToInt32(Context.Request["StaffID"]));
        Response.Redirect(nextAddr);
    }
}