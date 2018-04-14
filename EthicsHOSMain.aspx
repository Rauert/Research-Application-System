<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EthicsHOSMain.aspx.cs" Inherits="EthicsHOSMain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ethics Investigators Main</title>
    <style type="text/css">
        table
        {
	        border-collapse: collapse;
	        border: 0.5px solid #000;
        }
    </style>
</head>
<body>
    <form id="HOSMain" runat="server">
    <div id="MainDiv">
    <label id="lblWelcome" runat="server"></label><br />
    <label id="LblAccnt" runat="server"></label><br />
    <input id="btnLogout" type="button" value="Logout" onclick="location.href='EthicsLogIn.aspx';"/><br /><br />
    <h3>Applications - Awaiting Endorsement</h3>
    <div id="divUnsubmitted" runat="server">
    <table id="tblUnsubmitted" border="1" runat="server">
        <tr>
            <th width="50"><p>App ID</p></th>
            <th width="400"><p>Title</p></th>
            <th width="200"><p>Status</p></th>
            <th width="97"><p>View</p></th>
            <th width="97"><p>Endorse/Decline</p></th>
        </tr>
    </table>
    </div>
    </div>
    <br />
    <div runat="server" id="divMsg"></div>
    </form>
</body>
</html>