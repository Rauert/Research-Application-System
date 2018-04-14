<%@ Page Title="Ethics Application Log In" Language="C#" AutoEventWireup="true"
    CodeFile="EthicsLogIn.aspx.cs" Inherits="_EthicsLogIn" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server"> <title>Ethics Application Log In</title>
<style type="text/css">
    </style>
</head>
<body>
    <form id="frmLogIn" runat="server">
    <div>
    <label id="lblUsername">Username:</label>
    <select id="sltUsername" runat="server" datasourceid=""></select><br />
    <label id="lblPassword">Password:</label>
    <input type="password" id="txtPassword" value="" runat="server"/><br />
    <input id="btnSubmit" type="button" value="Submit" runat="server" onserverclick="btnSubmit_ServerClick"/><br />
    </div>
    <br />
    <div runat="server" id="divNote">Note password is first name of email.</div>
    <br />
    <div runat="server" id="divMsg"></div>
    </form>
</body>
</html>
