<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EthicsEditAppS3.aspx.cs" Inherits="EthicsEditAppS3" %>

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


        <h3><strong> SECTION 3: Ethical considerations specific to research methods or fields</strong></h3>
<p>&nbsp;</p>
<form id="frmS3" name="clinicalTrial" method="post" runat="server">
  <table border="1">
    <tr>
      <td width="15%"><label id="mand13" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label><strong>13</strong></td>
      <td width="85%"><p><strong>Is your study a clinical trial? </strong>(<a href="https://www.nhmrc.gov.au/book/chapter-3-3-interventions-and-therapies-including-clinical-and-non-clinical-trials-and">NS 3.3</a>)</p>
      <em>A clinical trial is defined as any research project  that prospectively assigns human subjects to intervention and comparison groups  to study the cause-and-effect relationship between a medical intervention and a  health outcome. Medical intervention means any intervention used to modify a  health outcome. This definition includes, drugs, surgical procedures, devices,  behavioral treatments, process-of-care change etc.</em></td>
    </tr>
    <tr>
      <td><input type="radio" name="clinicalRadio" id="clinicalRadioNo" value="no" runat="server" disabled="disabled"/>
      </td>
      <td>No – <em>please  skip to question 14</em></td>
    </tr>
    <tr>
      <td><input type="radio" name="clinicalRadio" id="clinicalRadioYes" value="yes" runat="server" disabled="disabled"/>
	</td>
      <td>Yes – <em>please  answer Question 13 subsections below.</em></td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table width="200" border="1" id="tbl3a" runat="server">
    <tr>
      <td width="16%"><label id="mand13a" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label><strong>13a</strong></td>
      <td width="84%"><strong>Will a placebo/non-treatment group be used? </strong>(<a href="https://www.nhmrc.gov.au/book/chapter-3-3-interventions-and-therapies-including-clinical-and-non-clinical-trials-and">NS 3.3.10</a>)</td>
    </tr>
    <tr>
      <td><input type="radio" name="placeboRadio" id="placeboRadioNo" value="no" runat="server" disabled="disabled"/>
      </td>
      <td>No –<em> please outline why a placebo or non-treatment group  will not be used.</em></td>
    </tr>
    <tr>
      <td><input type="radio" name="placeboRadio" id="placeboRadioYes" value="yes" runat="server" disabled="disabled"/></td>
      <td>Yes – <em>please  describe why a placebo or non-treatment group is the best comparator.</em></td>
    </tr>
    <tr>
      <td colspan="2">
      <textarea name="placeboText" id="placeboText" runat="server" disabled="disabled" rows="10" cols="130"></textarea></td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table width="200" border="1" id="tbl3b" runat="server">
    <tr>
      <td width="16%"><label id="mand13b" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label><strong>13b</strong></td>
      <td width="84%"><strong>Has this trial been registered? </strong>(<a href="https://www.nhmrc.gov.au/book/chapter-3-3-interventions-and-therapies-including-clinical-and-non-clinical-trials-and">NS 3.3.12</a>)</td>
    </tr>
    <tr>
      <td><input type="radio" name="trialRegRadio" id="trialRegRadioNo" value="no" runat="server" disabled="disabled"/>
      </td>
      <td>No</td>
    </tr>
    <tr>
      <td><input type="radio" name="trialRegRadio" id="trialRegRadioYes" value="yes" runat="server" disabled="disabled"/>
      </td>
      <td>Yes – <em>please  provide the registration number and the name of the trial registry in the space  below.</em></td>
    </tr>
    <tr>
      <td colspan="2">
      <textarea name="trialRegDetails" id="trialRegDetails" runat="server" disabled="disabled" rows="10" cols="130"></textarea></td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table width="200" border="1" id="tbl3c" runat="server">
    <tr>
      <td width="16%"><label id="mand13c" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label><strong>13c</strong></td>
      <td width="84%"><strong>Are the facilities, expertise and experience available sufficient for the trial to be conducted safely? </strong>(<a href="https://www.nhmrc.gov.au/book/chapter-3-3-interventions-and-therapies-including-clinical-and-non-clinical-trials-and">NS 3.3.5</a>)</td>
    </tr>
    <tr>
      <td><input type="radio" name="trialResRadio" id="trialResRadioNo" value="no" runat="server" disabled="disabled"/>
      </td>
      <td>No – <em>please  indicate how you will address this in the space below.</em></td>
    </tr>
    <tr>
      <td><input type="radio" name="trialResRadio" id="trialResRadioYes" value="yes" runat="server" disabled="disabled"/>
      </td>
      <td>Yes</td>
    </tr>
    <tr>
      <td colspan="2">
      <textarea name="trialResText" id="trialResText" runat="server" disabled="disabled" rows="10" cols="130" ></textarea></td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table border="1" id="tbl3d" runat="server">
    <tr>
      <td width="16%"><label id="mand13d" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label><strong>13d</strong></td>
      <td width="84%"><strong>Does your Participant Information Statement make clear to the participant whether they will have continued access after the trial to  treatment they have received during the trial, and on what terms? </strong>(<a href="https://www.nhmrc.gov.au/book/chapter-3-3-interventions-and-therapies-including-clinical-and-non-clinical-trials-and">NS 3.3.18</a>)</td>
    </tr>
    <tr>
      <td><input type="radio" name="trialTreatRadio" id="trialTreatRadioNo" value="no" runat="server" disabled="disabled"/>
      </td>
      <td>No</td>
    </tr>
    <tr>
      <td><input type="radio" name="trialTreatRadio" id="trialTreatRadioYes" value="yes" runat="server" disabled="disabled"/>
      </td>
      <td>Yes</td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table width="200" border="1">
    <tr>
      <td width="16%"><label id="mand14" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label><strong>14</strong></td>
      <td width="84%"><strong>Does your research use health information (including  biospecimens) that may reveal information that may be important for the health  or future health of the donor(s), their blood relatives or their community? </strong>(<a href="https://www.nhmrc.gov.au/book/chapter-3-4-human-biospecimens-laboratory-based-research">NS 3.4.10</a>, <a href="https://www.nhmrc.gov.au/book/chapter-3-5-human-genetics">3.5.1 and 3.5.2</a>)</td>
    </tr>
    <tr>
      <td><input type="radio" name="healthInfoRadio" id="healthInfoRadioNo" value="no" runat="server" disabled="disabled"/>
      </td>
      <td>No</td>
    </tr>
    <tr>
      <td><input type="radio" name="healthInfoRadio" id="healthInfoRadioYes" value="yes" runat="server" disabled="disabled"/></td>
      <td>Yes – <em>indicate  below how you will address the management of any proposed disclosure or  non-disclosure of that information.</em></td>
    </tr>
    <tr>
      <td colspan="2">
      <textarea name="healthInfoText" id="healthInfoText" runat="server" disabled="disabled" rows="10" cols="130" ></textarea></td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table width="200" border="1">
    <tr>
      <td width="16%"><label id="mand15" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label><strong>15</strong></td>
      <td width="84%"><p><strong>Does your research involve human genetics? </strong>(<a href="https://www.nhmrc.gov.au/book/chapter-3-5-human-genetics">NS 3.5</a>)</p>
      <em>Specific requirements for research involving fetal  tissue are detailed in </em><a href="https://www.nhmrc.gov.au/book/chapter-4-1-women-who-are-pregnant-and-human-foetus"><em>Chapter 4.1</em></a><em> of the National Statement. Research involving  human embryos and gametes, including the derivation of human embryonic stem  cell lines, is separately governed by the Research Involving Human Embryos Act  2002 (Cth) and the Ethical Guidelines on the use of Assisted Reproductive  Technology in Clinical Practice and Research (2007). Please refer to Chapter  3.5 of the National Statement for more information.</em></td>
    </tr>
    <tr>
      <td><input type="radio" name="humanGenRadio" id="humanGenRadioNo" value="no" runat="server" disabled="disabled"/>
      </td>
      <td>No</td>
    </tr>
    <tr>
      <td><input type="radio" name="humanGenRadio" id="humanGenRadioYes" value="yes" runat="server" disabled="disabled"/>
      </td>
      <td>Yes – <em>please  address in the space below the parts of Section 3.5 of the National Statement  that are relevant to this project.</em></td>
    </tr>
    <tr>
      <td colspan="2">
      <textarea name="humanGenText" id="humanGenText" runat="server" disabled="disabled" rows="10" cols="130" ></textarea></td>
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