<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EthicsEditCI.aspx.cs" Inherits="EthicsEditCI" %>

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
        <td style="width: 450px">
            <input type="button" id="btnBack" value="Back" onserverclick="btnBack_ServerClick" runat="server" class="style2"/>
            <input type="button" id="btnSave" value="Save" onserverclick="btnSave_ServerClick" runat="server" class="style2"/>
        </td>
        </tr>

        <tr>
        <td style="width: 450px">
        <table border="1" style="width: 400px">
            <tr>
                <th colspan="2">Edit Co-investigator details</th>
            </tr>
            <tr>
                <td class="style1">Name:</td>
                <td><input type="text" name="txtNm" id="txtNm" disabled="disabled" value="" runat="server"/></td>
            </tr>
            <tr>
            <td class="style1">Role:</td>
            <td>
                <select id="sltNewCIRole" runat="server">
  		            <option value="coInvestigator">Co-Investigator</option>
  		            <option value="supervisor">Supervisor</option>
  		            <option value="student">Student</option>
                </select>
            </td>
            </tr>
            <tr>
                <td class="style1">Candidacy approved:</td>
                <td align="center"><input type="checkbox" name="chkCand" id="chkCand" runat="server"/></td>
            </tr>
            <tr>
                <td class="style1">Research Integrity training complete:</td>
                <td align="center"><input type="checkbox" name="chkInteg" id="chkInteg" runat="server"/></td>
            </tr>
     </table>     
     </td>
     </tr>
    </table>
    <br />
    <div runat="server" id="divMsg"></div>
    </div>
    </form>
</body>
</html>
