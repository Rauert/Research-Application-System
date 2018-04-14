<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EthicsEditAppS1.aspx.cs" Inherits="EthicsEditAppS1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Edit Application</title>
    <script language="javascript" type="text/javascript">
        function beforeDelete()
        { return (confirm('Are you sure you want to remove this investigator?')); }
    </script>
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
        .style3
        {
            width: 269px;
        }
        .style5
        {
            width: 757px;
        }
        .style6
        {
            width: 623px;
        }
        .style7
        {
            width: 579px;
        }
        .style8
        {
            width: 268px;
        }
        .style9
        {
            width: 157px;
        }
    </style>
</head>
<body>
    <table id="tblMain" runat="server">
        <tr>
        <td width="300" id="rowMainA1" runat="server"></td>
        <td width="1000" id="rowMainA2" runat="server">
            <input type="button" id="btnBack" value="Back to Main" onserverclick="btnBack_ServerClick" runat="server" class="style2"/>
            <input type="button" id="btnPrint" value="Printer-Friendly View" onserverclick="btnPrint_ServerClick" runat="server" class="style2"/>
            <input type="button" id="btnSave" value="Save" onserverclick="btnSave_ServerClick" runat="server" visible="false" class="style2"/>
        </td>
        </tr>

        <tr>
        <td width="300" valign="top" id="rowMainB1" runat="server">
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
                onserverclick="btnPageChange_ServerClick" runat="server" class="style1"/><br /><br />
        <br /><br />
        <label id="lblColour">Note that sections marked red are incomplete.</label>
        </td>



    <td width="1000" id="rowMainB2" runat="server">


    <h3>SECTION 1 - General Information</h3>
<form id="frmS1" name="S1" method="post" runat="server">
  <table border="1" class="textarea">
    <tr>
      <td width="108"><label id="mand1" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label>1</td>
      <td width="400">Project Title</td>
    </tr>
    <tr>
      <td colspan="2"><textarea name="projectTitle" id="projectTitle" cols="84" rows="5" class="basicInput" runat="server" disabled="disabled"></textarea></td>
    </tr>
  </table>

  <p>&nbsp;</p>

  <table border="1">
    <tr>
      <td width="108"><label id="mand2" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label>2</td>
      <td width="400">Please indicate the type of project</td>
    </tr>
    <tr>
    <td colspan="2">
       <select id="sltProjType" runat="server" disabled="disabled">
  		<option value="Staff project" id="ProjType0">Staff project</option>
  		<option value="Undergraduate other than honours (no candidacy required)" id="ProjType1">Undergraduate other than honours (no candidacy required)</option>
  		<option value="Honours (no candidacy required)" id="ProjType2">Honours (no candidacy required)</option>
        <option value="Masters by coursework (no candidacy required)" id="ProjType3">Masters by coursework (no candidacy required)</option>
        <option value="Masters by research (candidacy required)" id="ProjType4">Masters by research (candidacy required)</option>
        <option value="PhD (candidacy required)" id="ProjType5">PhD (candidacy required)</option>
        <option value="Other (specify)" id="ProjType6">Other (specify)</option>
      </select>
      </td>
      </tr>
      <tr>
      <td colspan="2"><textarea name="projectTypeOther" id="projectTypeOther" cols="84" rows="5" runat="server" disabled="disabled"></textarea></td>
    </tr>
  </table>

  <p>&nbsp;</p>
  <table border="1">
    <tr>
      <td width="108" rowspan="2">3</td>
      <td class="style7">Project Summary</td>
    </tr>
    <tr>
      <td class="style7"><em>Give a concise and simple description, in plain language, of the study in each of the sections below.</em></td>
    </tr>
  </table>
  <p>&nbsp;</p>

  <table border="1">
    <tr>
      <td width="108"><label id="mand3a" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label>3a</td>
      <td width="400"><p>Background</p>
      <p>200 word limit</p></td>
    </tr>
    <tr>
      <td colspan="2"><textarea name="projectBackground" id="projectBackground" cols="84" rows="5" maxlength="200" runat="server" disabled="disabled"></textarea></td>
    </tr>
  </table>

  <p>&nbsp;</p>

  <table border="1">
    <tr>
      <td width="108"><label id="mand3b" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label>3b</td>
      <td width="400"><p>Aims and hypothesis</p></td>
    </tr>
    <tr>
      <td colspan="2"><textarea name="projectAim" id="projectAim" cols="84" rows="5" runat="server" disabled="disabled"></textarea></td>
    </tr>
  </table>

  <p>&nbsp;</p>
  
  <table border="1">
    <tr>
      <td width="108"><label id="mand3c" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label>3c</td>
      <td width="400"><p>Methods</p>
        <p>200 word limit</p></td>
    </tr>
    <tr>
      <td colspan="2"><textarea name="projectMethod" id="projectMethod" cols="84" rows="5" maxlength="200" runat="server" disabled="disabled"></textarea></td>
    </tr>
  </table>
  <p>&nbsp;</p>
  <table border="1">
    <tr>
      <td><div align="center">4</div></td>
      <td colspan="3"><p>Principle Investigator</p>
      <p><em>The principal investigator must be a Curtin staff  member. If this application is for a student project the principal investigator  must be one of the student&rsquo;s supervisors.</em></p></td>
    </tr>
    <tr>
      <td>Name (include title)</td>
      <td> <textarea name="piName" id="piName" cols="40" rows="2" runat="server" disabled="disabled"></textarea></td>
      <td>Staff ID</td>
      <td><textarea name="piStaffId" id="piStaffId" cols="25" rows="2" runat="server" disabled="disabled"></textarea></td>
    </tr>
    <tr>
      <td>School, Centre or Area</td>
      <td colspan="3"><textarea name="piSchool" id="piSchool" cols="40" rows="2" runat="server" disabled="disabled"></textarea></td>
    </tr>
    <tr>
      <td>Email</td>
      <td><textarea name="piEmail" id="piEmail" cols="40" rows="2" runat="server" disabled="disabled"></textarea></td>
      <td>Telephone</td>
      <td><textarea name="piPhone" id="piPhone" cols="40" rows="2" runat="server" disabled="disabled"></textarea></td>
    </tr>
    <tr>
      <td colspan="3">SOL Research Integrity Professional Development  Program training complete <em>(NOTE: this is  a requirement of approval)</em></td>
      <td><div align="center">
        <input type="checkbox" name="integrityCert" id="integrityCert" runat="server" disabled="disabled"/>
      </div>
        <label for="checkbox"></label>
          <div align="center">Yes (Please upload certificate)</div>
      </td>
    </tr>
  </table>
  <p>&nbsp;</p>
  <table border="1">
    <tr>
      <td align="center" width="50">5a</td>
      <td class="style5"><p>Co-Investigators</p>
      <p><em>If candidacy is approved please attach a copy. Note: candidacy should be approved before an ethics application is submitted.  NOTE: All Curtin staff and students MUST complete the SOL Research Integrity Professional Development Program training –  this is a requirement of approval – attach your certificate</em></p></td>
    </tr>
  </table>
  <table border="1" id="tblCI" runat="server">
    <tr>
      <td class="style8">Name</td>
      <td class="style9">Role</td>
      <td>Candidacy approved</td>
      <td>Research Integrity training complete</td>
      <td>Edit</td>
      <td>Delete</td>
    </tr>
  </table>
<p>&nbsp;</p>
<table border="1" id="tblNewCIHead" visible="false" runat="server">
    <tr>
      <td align="center" width="50">5b</td>
      <td class="style6"><p>Add new Co-Investigator</p></td>
    </tr>
</table>
<table border="1" id="tblNewCI" visible="false" runat="server">
<tr>
      <td class="style3">Name</td>
      <td>Role</td>
      <td>Candidacy approved</td>
      <td>Add</td>
    </tr>
    <tr>
      <td class="style3"><select name ="sltNewCI" id="sltNewCI" runat="server"></select></td>
      <td><select id="sltNewCIRole" runat="server">
  		<option value="coInvestigator">Co-Investigator</option>
  		<option value="supervisor">Supervisor</option>
  		<option value="student">Student</option>
      </select> </td>
      <td><input type="checkbox" name="chkCandNew" id="chkCandNew" runat="server"/></td>
      <td><input type="button" id="btnAdd" value="Add" onserverclick="btnAdd_ServerClick" runat="server"/></td>
    </tr>
</table>
  <p>&nbsp;</p>
  <table border="1">
    <tr>
      <td align="center" width="103">6a</td>
      <td colspan="1"><p>Contact Person</p></td>
    </tr>
    <tr>
      <td>Name:</td>
      <td width="400"><select id="sltContact" runat="server" disabled="disabled"></select></td>
    </tr>
  </table>
  <p>&nbsp;</p>
  <table border="1">
    <tr>
      <td align="center" width="103">6b</td>
      <td colspan="1"><p>Pass application control</p></td>
    </tr>
    <tr>
      <td>Name:</td>
      <td width="400"><select id="sltControl" runat="server" disabled="disabled"></select></td>
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
