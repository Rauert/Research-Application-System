<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EthicsESOEndorseReviewer.aspx.cs" Inherits="EthicsESOEndorseReviewer" %>

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
    <form id="frmESOEndorseReviewer" runat="server">
    <div>
    <table>
        <tr>
        <td style="width: 410px">
            <input type="button" id="btnBack" value="Back to Main" onserverclick="btnBack_ServerClick" runat="server"/>
            <input type="button" id="btnSave" value="Save" onserverclick="btnSave_ServerClick" runat="server"/>
            <input type="button" id="btnSubmit" value="Email" onserverclick="btnSubmit_ServerClick" runat="server"/>
        </td>
        </tr>

        <tr>
        <td style="width: 410px">
        <table border="1" style="width: 400px">
            <tr>
                <th colspan="2">Reviewer</th>
            </tr>
            <tr>
                <td>Select: </td>
                <td><select id="sltReviewer" runat="server"/></td>
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
