<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EthicsHOSEndorse.aspx.cs" Inherits="EthicsHOSEndorse" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        table
        {
	        border-collapse: collapse;
	        border: 0.5px solid #000;
        }
        .style1
        {
            width: 340px;
        }
    </style>
</head>
<body>
    <form id="frmHOSEndorse" runat="server">
    <div>
    <table>
        <tr>
        <td width="500">
            <input type="button" id="btnBack" value="Back to Main" onserverclick="btnBack_ServerClick" runat="server" class="style2"/>
            <input type="button" id="btnSubmit" value="Submit" onserverclick="btnSubmit_ServerClick" runat="server" class="style2"/>
        </td>
        </tr>

        <tr>
        <td width="500">
        <table border="1" style="width: 500px">
            <tr>
                <th colspan="2">Endorsement decision</th>
            </tr>
            <tr> 
                <td colspan="2">
                    <div align="left"><p><em>
                    Where the Head of School/Area or nominee has a conflict of interest with the proposed research, e.g. an investigator on the project, a member of the research group, or a personal relationship to any member of the research team, this Declaration is to be completed by the Deputy Head).</em></p>
                    <p>I declare that:</p>
                      <ul>
                          <li>I am satisfied that the research proposal is ready for submission for ethics approval.</li>
                          <li>The resources to undertake this project are available.</li>
                          <li>The researchers have the skill and expertise to undertake this project appropriately.</li>
                      </ul></div>
                </td>
            </tr>
            <tr>
                <td class="style1">Endorse</td>
                <td><input type="radio" name="radioDecision" id="radioAccept" value="no" runat="server"/></td>
            </tr>
            <tr>
                <td class="style1">Decline</td>
                <td><input type="radio" name="radioDecision" id="radioDenied" value="yes" runat="server"/></td>
            </tr>
            <tr>
            <td class="style1">Comments (If declined)</td>
                <td>
                    <textarea name="txtDeclined" id="txtDeclined" runat="server" rows="10" cols="70" ></textarea>
                </td>
            </tr>
     </table>
     </td>
     </tr>
    </table>
    <br />
    <div runat="server" id="divMsg"></div>
    <br />
    <div runat="server" id="divMsg2"></div>
    <br />
    <div runat="server" id="divMsg3"></div>
    </div>
    </form>
</body>
</html>
