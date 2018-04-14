<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EthicsCIDeclaration.aspx.cs" Inherits="EthicsCIDeclaration" %>

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
    <form id="frmCIDeclaration" runat="server">
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
                <th colspan="2">Co-Investigator Declaration</th>
            </tr>
    <tr>
      <td colspan="2"><div align="left"><p>I declare that:</p></div>
          <ul>
              <li><div align="left">I have read the application and all associated documents and the information provided in this application is truthful and complete as possible.</div></li>
              <li><div align="left">I undertake to conduct research in accordance with the approved protocol, the most recent National Statement on Ethical Conduct in Human Research, relevant legislation and the policies and procedures of Curtin University.</div></li>
          </ul>
      </td>
    </tr>
            <tr>
                <td class="style1">Confirm</td>
                <td><input type="checkbox" name="radioDeclare" id="radioDeclare" runat="server"/></td>
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