<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EthicsEditAppS5.aspx.cs" Inherits="EthicsEditAppS5" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Edit Application</title>
    <style type="text/css">
        table
        {
	        border-collapse: collapse;
	        width: 85%;
	        border: 0.5px solid #000;
	        text-align: center;
	        margin-left: 5%;
	        margin-right: 5%;
        }
        table textarea
        {
	        width:99%;
        }
        .style1
        {
            font-weight: bold;
            width: 296px;
            font-size: medium;
            color:Red;
        }
        .style2
        {
           font-size: medium;
        }
        .sectionAdmin
        {
            font-weight: bold;
            width: 296px;
            font-size: medium;
            color:Black;
        }
    </style>
</head>
<body>
    <table>
        <tr>
        <td width="300"></td>
        <td width="1000">
            <input type="button" id="btnBack" value="Back to Main" onserverclick="btnBack_ServerClick" runat="server" class="style2"/>
            <input type="button" id="btnPrint" value="Printer-Friendly View" onserverclick="btnPrint_ServerClick" runat="server" class="style2"/>
            <input type="button" id="btnSave" value="Save" onserverclick="btnSave_ServerClick" runat="server" visible="false" class="style2"/>
        </td>
        </tr>

        <tr>
        <td width="300" valign="top">
        <input type="button" id="btnTriage" value="Assessment for Ethics Triage" 
                onserverclick="btnPageChange_ServerClick" runat="server" class="style1"/><br /><br />
        <input type="button" id="btnS1" value="Section 1 - General Info" 
                onserverclick="btnPageChange_ServerClick" runat="server" class="style1"/><br /><br />
        <input type="button" id="btnS2" value="Section 2 - Themes in Research" 
                onserverclick="btnPageChange_ServerClick" runat="server" class="style1"/><br /><br />
        <input type="button" id="btnS3" value="Section 3 - Research Methods/Fields" 
                onserverclick="btnPageChange_ServerClick" runat="server" class="style1"/><br /><br />
        <input type="button" id="btnS4" value="Section 4 - Participants" 
                onserverclick="btnPageChange_ServerClick" runat="server" class="style1"/><br /><br />
        <input type="button" id="btnS5" value="Section 5 - Governance and Review" 
                onserverclick="btnPageChange_ServerClick" runat="server" class="style1"/><br /><br />
        <input type="button" id="btnS6" value="Section 6 - Attachments" 
                onserverclick="btnPageChange_ServerClick" runat="server" class="style1"/><br /><br />
        <input type="button" id="btnS7" value="Section 7 - Declaration & Submission" 
                onserverclick="btnPageChange_ServerClick" runat="server" class="style1"/> <br /><br />
        <br /><br />
        <label id="lblColour">Note that sections marked red are incomplete.</label>
        </td>



        <td width="1000">
        <form id="frmS5" runat="server">
        
        <h3> <strong> SECTION 5 - Process of research governance and ethical review</strong></h3>
<p>&nbsp;</p>

  <table width="200" border="1">
    <tr>
      <td width="16%"><label id="mand1" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label>
                <strong>23</strong></td>
      <td width="84%"><strong>Are there any potential conflicts of interest?</strong></td>
    </tr>
    <tr>
	  <td><input type="radio" name="ethicConflictInterestRadio" id="ethicConflictInterestRadioNo" value="no" runat="server" disabled="disabled"/>
      </td>
      <td>No – <em>please  skip to question next section.</em></td>
    </tr>
    <tr>
	  <td><input type="radio" name="ethicConflictInterestRadio" id="ethicConflictInterestRadioYes" value="yes" runat="server" disabled="disabled"/>
      </td>
      <td>Yes – <em>please  describe in the space below.</em></td>
    </tr>
    <tr>
      <td colspan="2">
        <textarea name="txtConflict" id="txtConflict" runat="server" disabled="disabled" rows="10" cols="130" ></textarea>
      </td>
    </tr>
  </table>
  <br />
    <div runat="server" id="divMsg"></div>

        
        </form>
    </td>
    </tr>
    </table>
</body>
</html>