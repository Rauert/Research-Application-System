<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EthicsESOMain.aspx.cs" Inherits="EthicsESOMain" %>

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
    <form id="frmESOMain" runat="server">
    <div id="MainDiv">
    <label id="lblWelcome" runat="server"></label><br />
    <label id="LblAccnt" runat="server"></label><br />
    <input id="btnLogout" type="button" value="Logout" onclick="location.href='EthicsLogIn.aspx';"/><br />
    <label id="lblSort" runat="server">Sort by: </label>
    <select id="sltSort" runat="server">
  		<option id="AppID" value="AppID">Application ID</option>
  		<option id="a1_ProjTitle" value="a1_ProjTitle">Title</option>
        <option id="School" value="School">Faculty</option>
        <option id="DateEndorsed" value="DateEndorsed">Date Endorsed</option>
      </select>
    <input id="btnSort" type="button" value="Sort" runat="server" onserverclick="btnSort_ServerClick"/><br /><br />
    <h3>Applications - HOS Endorsed</h3>
    <div id="divUnsubmitted" runat="server">
    <table id="tblUnsubmitted" border="1" runat="server">
        <tr>
            <th width="50"><p>App ID</p></th>
            <th width="400"><p>Title</p></th>
            <th width="200"><p>Status</p></th>
            <th width="200"><p>Faculty</p></th>
            <th width="97"><p>Date Endorsed</p></th>
            <th width="97"><p>View</p></th>
            <th width="97"><p>Select Risk Level</p></th>
        </tr>
    </table>
    </div>
    <h3>Applications - Low Risk</h3>
    <div id="divSubmitted" runat="server">
    <table id="tblSubmitted" border="1" runat="server">
        <tr>
            <th width="50"><p>App ID</p></th>
            <th width="400"><p>Title</p></th>
            <th width="200"><p>Status</p></th>
            <th width="200"><p>Faculty</p></th>
            <th width="97"><p>Date Endorsed</p></th>
            <th width="97"><p>View</p></th>
            <th width="97"><p>Select Reviewer</p></th>
            <th width="97"><p>Endorsement/ Completeness</p></th>
        </tr>
    </table>
    </div>
    </div>
    <br />
    <div runat="server" id="divMsg"></div>
    </form>
</body>
</html>