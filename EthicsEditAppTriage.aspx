<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EthicsEditAppTriage.aspx.cs" Inherits="EthicsEditAppTriage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        <form id="triageForm" method="post" runat="server">
    <table>
        <tr>
        <td width="300"></td>
        <td width="1000">
            <input type="button" id="btnBack" value="Back to Main" onserverclick="btnBack_ServerClick" runat="server" class="style2"/>
            <input type="button" id="btnPrint" value="Printer-Friendly View" onserverclick="btnPrint_ServerClick" runat="server" class="style2"/>
            <input type="button" id="btnSave" value="Save" onserverclick="btnSave_ServerClick" runat="server" visible="false" class="style2"/>&nbsp;
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

<h3><strong>Assessment of applications for ethics triage</strong></h3>
<table border="1">
      <tr>
        <td width="423">Does the study include any of the seven following types of research and/or participants?</td>
        <td width="40"><div align="center">No</div></td>
        <td width="40"><div align="center">Yes</div></td>
      </tr>
      <tr>
        <td colspan="3"><em>According to section 5.1.6 (b) of the National  Statement if your study involves any of the following groups the project MUST  be reviewed by the HREC.</em></td>
      </tr>
      <tr>
        <td><label id="mand1" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label>Interventions and therapies, including clinical  and non-clinical trials (<a href="https://www.nhmrc.gov.au/book/chapter-3-3-interventions-and-therapies-including-clinical-and-non-clinical-trials-and">NS 3.3</a>) </td>
        <td><label for="checkbox"></label>
          <p>
            <label>
              <input type="radio" name="intTherapy" value="no" id="intTherapy_1" runat="server" disabled="disabled"/>
              No </label>
          </p>
         </td>
        <td>
          <p>
            <label>
              <input type="radio" name="intTherapy" value="yes" id="intTherapy_0" runat="server" disabled="disabled"/>
              Yes</label>
          </p>
        </td>
      </tr>
      <tr>
        <td><label id="mand2" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label>Human genetics (<a href="https://www.nhmrc.gov.au/book/chapter-3-5-human-genetics">NS 3.5</a>)</td>
        <td>
         <p>
          <label>
            <input type="radio" name="genetics" value="no" id="genetics_1" runat="server" disabled="disabled"/>
            No</label>
        </p>
        </td>
        <td>
         <p>
          <label>
            <input type="radio" name="genetics" value="yes" id="genetics_0" runat="server" disabled="disabled"/>
            Yes</label>
          </p>
        </td>
      </tr>
      <tr>
        <td><label id="mand3" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label>Women who are pregnant and/or the human fetus (<a href="https://www.nhmrc.gov.au/book/chapter-4-1-women-who-are-pregnant-and-human-foetus">NS 4.1</a>)</td>
        <td><p>
        <label>
            <input type="radio" name="pregnancy" value="no" id="pregnancy_1" runat="server" disabled="disabled"/>
            No</label>
        </p></td>
        <td><p>
          <label>
            <input type="radio" name="pregnancy" value="yes" id="pregnancy_0" runat="server" disabled="disabled"/>
            Yes</label>
            </p></td>
      </tr>
      <tr>
        <td><p><label id="mand4" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label>People who are highly dependent on medical care who may be unable to give consent (<a href="https://www.nhmrc.gov.au/book/chapter-4-3-people-dependent-or-unequal-relationships">NS 4.3</a> and <a href="https://www.nhmrc.gov.au/book/chapter-4-4-people-highly-dependent-medical-care-who-may-be-unable-give-consent">4.4</a>) </p></td>
        <td>
         <p>
          <label>
            <input type="radio" name="medConsent" value="no" id="medConsent_1" runat="server" disabled="disabled"/>
            No</label>
         </p>
        </td>
        <td>
         <p>
          <label>
            <input type="radio" name="medConsent" value="yes" id="medConsent_0" runat="server" disabled="disabled"/>
            Yes</label>
          </p>
        </td>
      </tr>
      <tr>
        <td><label id="mand5" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label>People with a cognitive impairment, intellectual  disability or a mental illness (<a href="https://www.nhmrc.gov.au/book/chapter-4-5-people-cognitive-impairment-intellectual-disability-or-mental-illness">NS 4.5</a>)</td>
        <td><p>
        <label>
            <input type="radio" name="menDisability" value="no" id="menDisability_1" runat="server" disabled="disabled"/>
            No</label>
        </p></td>
        <td><p>
          <label>
            <input type="radio" name="menDisability" value="yes" id="menDisability_0" runat="server" disabled="disabled"/>
            Yes</label>
        </p></td>
      </tr>
      <tr>
        <td><label id="mand6" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label>Research specifically targeting Aboriginal or  Torres Strait Islanders (<a href="https://www.nhmrc.gov.au/book/chapter-4-7-aboriginal-and-torres-strait-islander-peoples">NS 4.7</a>)</td>
        <td><p>
        <label>
            <input type="radio" name="indigenous" value="no" id="indigenous_1" runat="server" disabled="disabled"/>
            No</label>
        </p></td>
        <td><p>
          <label>
            <input type="radio" name="indigenous" value="yes" id="indigenous_0" runat="server" disabled="disabled"/>
            Yes</label>
        </p></td>
      </tr>
      <tr>
        <td><label id="mand7" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label>People who may be involved in illegal activities (<a href="https://www.nhmrc.gov.au/book/chapter-4-6-people-who-may-be-involved-illegal-activities">NS 4.6</a>)</td>
        <td><p>
        <label>
            <input type="radio" name="illegalAct" value="no" id="illegalAct_1" runat="server" disabled="disabled"/>
            No</label>
        </p></td>
        <td><p>
          <label>
            <input type="radio" name="illegalAct" value="yes" id="illegalAct_0" runat="server" disabled="disabled"/>
            Yes</label>
        </p></td>
      </tr>
  </table>
  <table border="0">
  <tr>
    <td><p><label id="mand8" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label>A <strong>&ldquo;yes&rdquo;</strong> response to any of the above  questions would normally indicate your project is <strong>not eligible</strong> for a Low or Negligible Risk review. However, a &ldquo;Yes&rdquo;  answer does not necessarily, automatically, preclude the research from being  reviewed through a low risk review process. If you answered &ldquo;yes&rdquo; to any of the  above questions and you think your study should be reviewed through the low  risk process please justify why in the space below.</p></td></tr>
        <tr><td><textarea id="txtYesResp" rows="10" cols="80" runat="server" disabled="disabled"></textarea></td></tr>
        </table>
        <br />
    <div runat="server" id="divMsg"></div>
</form>
    </td>
    </tr>
    </table>
</body>
</html>
