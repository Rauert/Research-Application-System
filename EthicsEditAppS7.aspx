<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EthicsEditAppS7.aspx.cs" Inherits="EthicsEditAppS7" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Edit Application</title>
    <style type="text/css">
        table
        {
	        border-collapse: collapse;
	        width: 85%;
	        border: 0.5px solid #000;
	        text-align: center;
	        margin-left: 5%;
	        margin-right: 5%;
        }
        table textarea
        {
	        width:99%;
        }
        .style1
        {
            font-weight: bold;
            width: 296px;
            font-size: medium;
            color:Red;
        }
        .style2
        {
           font-size: medium;
        }
        .sectionAdmin
        {
            font-weight: bold;
            width: 296px;
            font-size: medium;
            color:Black;
        }
    </style>
</head>
<body>
<form id="frmS7" runat="server">
    <table>
        <tr>
        <td width="300"></td>
        <td width="1000">
            <input type="button" id="btnBack" value="Back to Main" onserverclick="btnBack_ServerClick" runat="server" class="style2"/>
            <input type="button" id="btnPrint" value="Printer-Friendly View" onserverclick="btnPrint_ServerClick" runat="server" class="style2"/>
            <input type="button" id="btnSave" value="Save" onserverclick="btnSave_ServerClick" runat="server" visible="false" class="style2"/>
        </td>
        </tr>

        <tr>
        <td width="300" valign="top">
        <input type="button" id="btnTriage" value="Assessment for Ethics Triage" 
                onserverclick="btnPageChange_ServerClick" runat="server" class="style1"/><br /><br />
        <input type="button" id="btnS1" value="Section 1 - General Info" 
                onserverclick="btnPageChange_ServerClick" runat="server" class="style1"/><br /><br />
        <input type="button" id="btnS2" value="Section 2 - Themes in Research" 
                onserverclick="btnPageChange_ServerClick" runat="server" class="style1"/><br /><br />
        <input type="button" id="btnS3" value="Section 3 - Research Methods/Fields" 
                onserverclick="btnPageChange_ServerClick" runat="server" class="style1"/><br /><br />
        <input type="button" id="btnS4" value="Section 4 - Participants" 
                onserverclick="btnPageChange_ServerClick" runat="server" class="style1"/><br /><br />
        <input type="button" id="btnS5" value="Section 5 - Governance and Review" 
                onserverclick="btnPageChange_ServerClick" runat="server" class="style1"/><br /><br />
        <input type="button" id="btnS6" value="Section 6 - Attachments" 
                onserverclick="btnPageChange_ServerClick" runat="server" class="style1"/><br /><br />
        <input type="button" id="btnS7" value="Section 7 - Declaration & Submission" 
                onserverclick="btnPageChange_ServerClick" runat="server" class="style1"/><br /><br />
        <br /><br />
        <label id="lblColour">Note that sections marked red are incomplete.</label>
        </td>



        <td width="1000">

        <h3>SECTION 7 - Declaration & Submission</h3>

        <table border="1">

        <tr>
          <td colspan="2"><div align="left"><p>I declare that:</p>
              <ul>
                  <li>The information provided in this application is truthful and as complete as possible.</li>
                  <li>I undertake to conduct research in accordance with the approved protocol, the most recent National Statement on Ethical Conduct in Human Research, relevant legislation and the policies and procedures of Curtin University.</li>
                  <li>Where I am the Project Supervisor for research described to be conducted by a student of Curtin University, I declare that I have provided guidance to the student in the design, methodology and consideration of ethical issues of the proposed research; that the student has received the relevant research and ethics training for this project; and, that I will monitor the project during data collection.</li>
                  <li>I make this application on the basis that the information it contains is confidential and will be used by Curtin University for the purposes of ethical review and monitoring of the research project described herein, and to satisfy reporting requirements to regulatory bodies. The information will not be used for any other purpose without my prior consent.</li>
              </ul></div>
          </td>
        </tr>
        <tr>
                <td colspan="1"><label id="mand1" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label>Confirm</td>
                <td colspan="1"><input type="checkbox" name="radioDeclare" id="radioDeclare" checked="checked" runat="server" disabled="disabled"/></td>
        </tr>
        <tr>
            <td colspan="1">
                <label id="lblHOS"><label id="mand2" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label>Choose Head of School: </label>
                
            </td>
            <td colspan="1">
                <select id="sltHOS" runat="server" disabled="disabled"/>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <br /><br />
                    <input type="button" id="btnSubmit" value="Submit" style="font-size:medium; font-weight:bold;" onserverclick="btnSubmit_ServerClick" runat="server" disabled="disabled"/>
                <br /><br />
            </td>
        </tr>
        </table>
        <br />
        <table id="tblDec" border="1" visible="false" runat="server">
        <tr>
            <th colspan="2">
                <label id="lblDeclined" runat="server"></label>
            </th>
        </tr>
        <tr>
            <td colspan="2">
                <textarea name="txtDeclined" id="txtDeclined" runat="server" disabled="disabled" rows="10" cols="130"></textarea>
            </td>
        </tr>
        </table>
        <br />
    <div runat="server" id="divMsg"></div>
    <br />
    <div runat="server" id="divMsg2"></div>
    <br />
    <div runat="server" id="divMsg3"></div>
    <br />
    <div runat="server" id="divMsg4"></div>
    </td>
    </tr>
    </table>
</form>
</body>
</html>