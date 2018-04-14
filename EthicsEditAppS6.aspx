<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EthicsEditAppS6.aspx.cs" Inherits="EthicsEditAppS6" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Edit Application</title>
    <script language="javascript" type="text/javascript">
        function beforeDelete()
        { return (confirm('Are you sure you want to delete this file?')); }
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
        #btnUpload
        {
            height: 22px;
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
                onserverclick="btnPageChange_ServerClick" runat="server" class="style1"/><br /><br />
        <br /><br />
        <label id="lblColour">Note that sections marked red are incomplete.</label>
        </td>



        <td width="1000">
        <form id="frmS6" runat="server">
        <h3>SECTION 6 - Attachments</h3>
        <div id="divUploads" runat="server">
    <table id="tblUploads" border="1" runat="server">
        <tr>
            <th width="200"><p>Filename</p></th>
            <th width="200"><p>Attachment Type</p></th>
            <th width="97"><p>File Version</p></th>
            <th width="97"><p>File Date</p></th>
            <th width="97"><p>Delete</p></th>
        </tr>
    </table>
    </div>
    <br />
        <table id="tblNewUpload" border="1" visible="false" runat="server">
        <tr><th colspan="2">
            <p><label id="mand1" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label>Upload file</p>
        </th></tr>
        <tr>
            <td>
                <label>Select file: </label>
                <asp:FileUpload ID="fileUpload" runat="server" />
            </td>
            <td>
                <label>Date: </label>
                <input type="text" id="txtDate" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
            <label>Type: </label>
            <select id="sltType" runat="server">
  		        <option value="Protocol/research proposal">Protocol/research proposal</option>
  		        <option value="Recruitment material">Recruitment material</option>
  		        <option value="Participant Information statement and consent form/s">Participant Information statement and consent form/s</option>
                <option value="Parent Information statement and consent form/s">Parent Information statement and consent form/s</option>
                <option value="Child Information statement and assent form/s">Child Information statement and assent form/s</option>
                <option value="Questionnaires/survey instrument/s">Questionnaires/survey instrument/s</option>
                <option value="Translations where languages other than English are used">Translations where languages other than English are used</option>
                <option value="Recruitment materials">Recruitment materials</option>
                <option value="Investigator brochure or Product Information (for drug intervention studies)">Investigator brochure or Product Information (for drug intervention studies)</option>
                <option value="Working with Children’s Card">Working with Children’s Card</option>
                <option value="SOL Research Integrity Professional Development Program training certificate/s">SOL Research Integrity Professional Development Program training certificate/s</option>
            </select>
            </td>
            <td>
                <label>Version: </label>
                <input type="text" id="txtVersion" runat="server" />
            </td>
        </tr>
        <tr><td colspan="2">
            <input type="button" id="btnUpload" value="Upload" onserverclick="btnUpload_Click" runat="server" />
        </td></tr>
        </table>
    <div runat="server" id="divMsg"></div>
    <br />
        <table border="1">

        <tr>
          <td colspan="2">
          <p><strong>NOTES</strong></p>
    <div align="left"><ol>
        <li>In the footer of all your documents (e.g. protocol, recruitment material, information statements and consent forms, questionnaires etc) you should include:</li>
        </ol>
        <ul>
            <li>Name of the document</li>
            <li>Version number</li>
            <li>Date</li>
        </ul>
        <ol>
        <li>Refer to the guidelines for Participant Information Statements and Consent Forms. Remember to include a phrase similar to the following:
        <br/>
        Curtin University Human Research Ethics Committee (HREC) has approved this study (HREC number XX/XXXX). Should you wish to discuss the study with someone not directly involved, in particular, any matters concerning the conduct of the study or your rights as a participant, or you wish to make a confidential complaint, you may contact the Ethics Officer on cuhumanethics@gmail.com.</li>
        <li>Refer to the <a href="http://brand.curtin.edu.au/">Curtin Brand website</a>  for information on advertising for recruitment. All forms of print media must contain the HREC approval number.</li>
    </ol></div>

          </td>
        </tr>
        </table>
        </form>
    </td>
    </tr>

    </table>
</body>
</html>