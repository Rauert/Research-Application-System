<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EthicsInvestMain.aspx.cs" Inherits="EthicsInvestMain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ethics Investigators Main</title>
    <script language="javascript" type="text/javascript">
        function beforeDelete()
        { return (confirm('Are you sure you want to delete this application?')); }
    </script>
    <style type="text/css">
        table
        {
	        border-collapse: collapse;
	        border: 0.5px solid #000;
        }
    </style>
</head>
<body>
    <form id="InvestMain" runat="server">
    <div id="MainDiv">
    <label id="lblWelcome" runat="server"></label><br />
    <label id="LblAccnt" runat="server"></label><br />
    <input id="btnLogout" type="button" value="Logout" onclick="location.href='EthicsLogIn.aspx';"/><br /><br />
    <h3>Applications - Lead Investigator</h3>
    <input id="btnNew" type="button" value="New Application" runat="server" onserverclick="btnNew_ServerClick"/><br /><br />
    <h4>Unsubmitted</h4>
    <div id="divPIUnsubmitted" runat="server">
    <table id="tblPIUnsubmitted" border="1" runat="server">
        <tr>
            <th width="50"><p>App ID</p></th>
            <th width="400"><p>Title</p></th>
            <th width="200"><p>Status</p></th>
            <th width="120"><p>Risk Level</p></th>
            <th width="97"><p>Edit</p></th>
            <th width="97"><p>Delete</p></th>
        </tr>
    </table>
    </div>
    <h4>Submitted</h4>
    <div id="divPISubmitted" runat="server">
    <table id="tblPISubmitted" border="1" runat="server">
        <tr>
            <th width="50"><p>App ID</p></th>
            <th width="400"><p>Title</p></th>
            <th width="200"><p>Status</p></th>
            <th width="120"><p>Risk Level</p></th>
            <th width="97"><p>View</p></th>
        </tr>
    </table>
    </div>
    <h3>Applications - Co Investigator</h3>
    <h4>Unsubmitted</h4>
    <div id="divCIUnsubmitted" runat="server">
    <table id="tblCIUnsubmitted" border="1" runat="server">
        <tr>
            <th width="50"><p>App ID</p></th>
            <th width="400"><p>Title</p></th>
            <th width="200"><p>Status</p></th>
            <th width="120"><p>Risk Level</p></th>
            <th width="97"><p>Make Declaration</p></th>
            <th width="97"><p>View/Edit</p></th>
            <th width="97"><p>Delete</p></th>
        </tr>
    </table>
    </div>
    <h4>Submitted</h4>
    <div id="divCISubmitted" runat="server">
    <table id="tblCISubmitted" border="1" runat="server">
        <tr>
            <th width="50"><p>App ID</p></th>
            <th width="400"><p>Title</p></th>
            <th width="200"><p>Status</p></th>
            <th width="120"><p>Risk Level</p></th>
            <th width="97"><p>View</p></th>
        </tr>
    </table>
    </div>
    </div>
    <br />
    <div runat="server" id="divMsg"></div>
    </form>
</body>
</html>
