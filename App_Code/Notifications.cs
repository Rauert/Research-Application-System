using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

/**
 * Class for creating and sending email messgaes.
 */
public class Notifications
{
	public Notifications(){}

    //Returns formatted email for notification of risk level decision.
    public static string riskEmail(string title, string riskLevel)
    {
        return ("Thank you for your application submitted to the Human Research Ethics Office for the project: " + title + Environment.NewLine +
                "Your application will be assessed through the " + riskLevel + " ethics approvals process." + Environment.NewLine + Environment.NewLine +
                "Should you have any queries about the consideration of your project please contact the Ethics Support Officer or the Ethics Office.");
    }

    //Returns formatted email for reviewer notification.
    public static string reviewEmail(string firstName, string title, string appID)
    {
        return ("Hi " + firstName + "," + Environment.NewLine + Environment.NewLine + "You have been nominated to review the ethics application titled: " +
                title + Environment.NewLine + "Please find application at the following address:" + Environment.NewLine + Environment.NewLine +
                "http://curtinethics-001-site1.smarterasp.net/EthicsPrintApp.aspx?AppID=" + appID + Environment.NewLine + Environment.NewLine + 
                "Should you have any queries about your selection for project review please contact the Ethics Support Officer or the Ethics Office.");
    }

    //Returns formatted email for application accepted notification.
    public static string acceptEmail(string title, string riskLevel)
    {
        return ("Thank you for your application submitted to the Human Research Ethics Office for the project: " + title + Environment.NewLine + 
                "Your application has been approved through the " + riskLevel + " ethics approvals." + Environment.NewLine + 
                "Please note the following conditions of approval:" + Environment.NewLine + "1. Approval is granted for a period of four years from " +
                DateTime.Today.ToShortDateString() + Environment.NewLine + "2. Research must be conducted as stated in the approved protocol." + Environment.NewLine + 
                "3. Any amendments to the approved protocol must be approved by the Ethics Office." + Environment.NewLine + Environment.NewLine + 
                "Should you have any queries about the consideration of your project please contact the Ethics Support Officer or the Ethics Office.");
    }

    //Returns formatted email for application declined notification.
    public static string declineEmail(string title, string riskLevel, string comment)
    {
        return ("Thank you for your application submitted to the Human Research Ethics Office for the project: " + title + Environment.NewLine + 
                "Your application has been processed through the " + riskLevel + " ethics approvals. Your application has not been successful." + Environment.NewLine + 
                "Comments on your application are outlined below." + Environment.NewLine + comment + Environment.NewLine + Environment.NewLine +
                "Should you have any queries about the consideration of your project please contact the Ethics Support Officer or the Ethics Office.");
    }

    //Returns formatted email for application incomplete notification.
    public static string incompleteEmail(string title, string comment)
    {
        return ("Thank you for your application submitted to the Human Research Ethics Office for the project: " + title + Environment.NewLine +
                "Your application has been deemed incomplete." + Environment.NewLine +
                "Comments on your application are outlined below." + Environment.NewLine + comment + Environment.NewLine + Environment.NewLine + 
                "Should you have any queries about the consideration of your project please contact the Ethics Support Officer or the Ethics Office.");
    }

    //Send an email. Based on code at link below:
    //http://stackoverflow.com/questions/18988348/error-in-sending-email-via-a-smtp-client
    //Windows live server settings:
    //http://email.about.com/od/Outlook.com/f/What-Are-The-Outlook-com-Smtp-Server-Settings.htm
    //To deal with exceptions the exception message is passed back to the source, where a failure to send email message is reported to the user.
    public static string send(string email, string subject, string msg)
    {
        string error = "";

        try
        {
            MailMessage message = new System.Net.Mail.MailMessage();
            string fromEmail = "curtinethics@outlook.com";
            string fromPW = "Curtin123";
            string toEmail = email;
            message.From = new MailAddress(fromEmail);
            message.To.Add(toEmail);
            message.Subject = subject;
            message.Body = msg + Environment.NewLine + email;
            message.Body = msg;

            SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(fromEmail, fromPW);

            smtpClient.Send(message.From.ToString(), message.To.ToString(), message.Subject, message.Body);
        }
        catch (Exception ex)
        {
            error = ("Error sending!" + Environment.NewLine + ex.ToString());
        }

        return error;
    }
}