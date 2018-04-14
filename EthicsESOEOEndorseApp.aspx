<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EthicsESOEOEndorseApp.aspx.cs" Inherits="EthicsESOEOEndorseApp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        table
        {
	        border-collapse: collapse;
	        border: 0.5px solid #000;
        }
        .style1
        {
            width: 330px;
        }
    </style>
</head>
<body>
    <form id="frmESOEndorseLowRisk" runat="server">
    <div>
    <table>
        <tr>
        <td style="width: 420px">
            <input type="button" id="btnBack" value="Back to Main" onserverclick="btnBack_ServerClick" runat="server" class="style2"/>
            <input type="button" id="btnSubmit" value="Submit" onserverclick="btnSubmit_ServerClick" runat="server" class="style2"/>
        </td>
        </tr>

        <tr>
        <td style="width: 420px">
        <table border="1" style="width: 370px">
            <tr>
                <th colspan="2">Review decision</th>
            </tr>
            <tr>
                <td class="style1">Accept</td>
                <td align="center"><input type="radio" name="radioDecision" id="radioAccept" value="Accept" runat="server"/></td>
            </tr>
            <tr>
                <td class="style1">Decline</td>
                <td align="center"><input type="radio" name="radioDecision" id="radioDecline" value="Decline" runat="server"/></td>
            </tr>
            <tr>
                <td class="style1">Incomplete</td>
                <td align="center"><input type="radio" name="radioDecision" id="radioIncomplete" value="Incomplete" runat="server"/></td>
            </tr>
            <tr>
                <td colspan="2" class="style1">Rejection / Incomplete comment:</td>
            </tr>
            <tr>
                <td colspan="2"><textarea id="txtRej" cols="50" rows="10" runat="server"></textarea></td>
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
