<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EthicsESOEndorseRisk.aspx.cs" Inherits="EthicsESOEndorseRisk" %>

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
            width: 280px;
        }
    </style>
</head>
<body>
    <form id="frmESOEndorseRisk" runat="server">
    <div>
    <table>
        <tr>
        <td style="width: 250px">
            <input type="button" id="btnBack" value="Back to Main" onserverclick="btnBack_ServerClick" runat="server" class="style2"/>
            <input type="button" id="btnSubmit" value="Submit" onserverclick="btnSubmit_ServerClick" runat="server" class="style2"/>
        </td>
        </tr>

        <tr>
        <td style="width: 250px">
        <table border="1" style="width: 200px">
            <tr>
                <th colspan="2">Risk decision</th>
            </tr>
            <tr>
                <td class="style1">Low Risk</td>
                <td><input type="radio" name="radioDecision" id="radioLowRisk" value="Low Risk" runat="server"/></td>
            </tr>
            <tr>
                <td class="style1">Non Low Risk</td>
                <td><input type="radio" name="radioDecision" id="radioNonLowRisk" value="Non Low Risk" runat="server"/></td>
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
