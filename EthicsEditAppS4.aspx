<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EthicsEditAppS4.aspx.cs" Inherits="EthicsEditAppS4" %>

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
                onserverclick="btnPageChange_ServerClick" runat="server" class="style1"/>
        <input type="button" id="btnAdmin" value="Admin Section" 
                onserverclick="btnPageChange_ServerClick" runat="server" class="sectionAdmin" visible="false"/><br /><br />
        <br /><br />
        <label id="lblColour">Note that sections marked red are incomplete.</label>
        </td>



        <td width="1000">
        <form id="frmS4" runat="server">
        
        <h3> <strong> SECTION 4 - Ethical considerations specific to participants</strong></h3>
<p>&nbsp;</p>
  <table width="200" border="1">
    <tr>
      <td width="16%"><label id="mand16" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label><strong>16</strong></td>
      <td width="84%"><strong>Does your research involve women who are pregnant and/or  the human fetus?</strong></td>
    </tr>
    <tr>
      <td><input type="radio" name="ethicPregnantRadio" id="ethicPregnantRadioNo" value="no" disabled="disabled" runat="server"/>
      </td>
      <td>No – <em>please  skip to question 17.</em></td>
    </tr>
    <tr>
      <td><input type="radio" name="ethicPregnantRadio" id="ethicPregnantRadioYes" value="yes" disabled="disabled" runat="server"/>
      </td>
      <td>Yes – <em>please  answer Question 16 subsections below.</em></td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table width="200" border="1">
    <tr>
      <td width="16%"><label id="mand16a" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label><strong>16a</strong></td>
      <td width="84%"><strong>Will steps be taken to ensure that the well-being and  care of the woman who is pregnant and her fetus takes precedence over the aims  of the research? </strong>(<a href="https://www.nhmrc.gov.au/book/chapter-4-1-women-who-are-pregnant-and-human-foetus">NS 4.1.1</a>)</td>
    </tr>
    <tr>
	  <td><input type="radio" name="ethicPregnantWellRadio" id="ethicPregnantWellRadioNo" value="no" disabled="disabled" runat="server"/>
      </td>
      <td>No – <em>please  justify why below.</em></td>
    </tr>
    <tr>
	  <td><input type="radio" name="ethicPregnantWellRadio" id="ethicPregnantWellRadioYes" value="yes" disabled="disabled" runat="server"/>
      </td>
      <td>Yes – <em>please  outline the procedures below.</em></td>
    </tr>
    <tr>
      <td colspan="2">
        <textarea name="ethicPregnantWell" id="ethicPregnantWell" runat="server" disabled="disabled" rows="10" cols="130" ></textarea>
      </td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table width="200" border="1">
    <tr>
      <td width="16%"><label id="mand16b" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label><strong>16b</strong></td>
      <td width="84%"><strong>Will the information about research be separate from information about routine clinical care? </strong></td>
    </tr>
    <tr>
	  <td><input type="radio" name="ethicPregnantInfoRadio" id="ethicPregnantInfoRadioNo" value="no" disabled="disabled" runat="server"/>
      </td>
      <td>No – <em>please  justify why the information will not be provided separately below.</em></td>
    </tr>
    <tr>
	  <td><input type="radio" name="ethicPregnantInfoRadio" id="ethicPregnantInfoRadioYes" value="yes" disabled="disabled" runat="server"/>
      </td>
      <td>Yes</td>
    </tr>
    <tr>
      <td colspan="2">
        <textarea name="ethicPregnantInfo" id="ethicPregnantInfo" runat="server" disabled="disabled" rows="10" cols="130" ></textarea>
      </td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table width="200" border="1">
    <tr>
      <td width="16%"><label id="mand17" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label><strong>17</strong></td>
      <td width="84%"><strong>Does your research involve children and young people? </strong>(<a href="https://www.nhmrc.gov.au/book/chapter-4-2-children-and-young-people">NS 4.2</a>)</td>
    </tr>
    <tr>
	  <td><input type="radio" name="ethicYoungRadio" id="ethicYoungRadioNo" value="no" disabled="disabled" runat="server"/>
      </td>
      <td>No – <em>please  skip to question 18.</em></td>
    </tr>
    <tr>
	  <td><input type="radio" name="ethicYoungRadio" id="ethicYoungRadioYes" value="yes" disabled="disabled" runat="server"/>
      </td>
      <td>Yes – <em>in  the space below address why participation of children or young people is indispensable to this research; and how this study has been designed to be appropriate for children or young people.</em></td>
    </tr>
    <tr>
      <td colspan="2">
        <textarea name="ethicYoung" id="ethicYoung" runat="server" disabled="disabled" rows="10" cols="130" ></textarea>
      </td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table width="200" border="1">
    <tr>
      <td width="16%"><label id="mand17a" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label><strong>17a</strong></td>
      <td width="84%"><strong>Do you have a Workng With Children's (WWC) card? </strong></td>
    </tr>
    <tr>
	  <td><input type="radio" name="ethicWWCRadio" id="ethicWWCRadioNo" value="no" disabled="disabled" runat="server"/>
      </td>
      <td>No – <em>it  is a legal requirement to have a WWC’s card. Please arrange to submit your WWC application and provide a receipt or a have a card BEFORE you submit this ethics application. Ethics approval will not be given without a WWC.</em></td>
    </tr>
    <tr>
	  <td><input type="radio" name="ethicWWCRadio" id="ethicWWCRadioYes" value="yes" disabled="disabled" runat="server"/>
      </td>
      <td>Yes – <em>please upload a copy to your application.</em></td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table width="200" border="1">
    <tr>
      <td width="16%"><label id="mand18" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label><strong>18</strong></td>
      <td width="84%"><p><strong>Does your research involve people in dependent or unequal relationships?</strong>(<a href="https://www.nhmrc.gov.au/book/chapter-4-3-people-dependent-or-unequal-relationships">NS 4.3</a>)</p>
      <em>For example: teachers and their students, health care professionals and their patients, employers and their employees.</em></td>
    </tr>
    <tr>
	  <td><input type="radio" name="ethicUnequalRelRadio" id="ethicUnequalRelRadioNo" value="no" disabled="disabled" runat="server"/>
      </td>
      <td>No – <em>please  skip to question 19.</em></td>
    </tr>
    <tr>
	  <td><input type="radio" name="ethicUnequalRelRadio" id="ethicUnequalRelRadioYes" value="yes" disabled="disabled" runat="server"/>
      </td>
      <td>Yes – <em>in  the space below describe the dependent relationship between the participants and the researcher, members of the research team and/or any person involved in the recruitment/consent process; how will the process of obtaining consent enable persons in dependent relationships to give voluntary consent; if a participant choose to withdraw from the research, how will the ongoing dependent relationship with the participant be maintained?</em></td>
    </tr>
    <tr>
      <td colspan="2">
        <textarea name="ethicUnequalRel" id="ethicUnequalRel" runat="server" disabled="disabled" rows="10" cols="130" ></textarea>
      </td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table width="200" border="1">
    <tr>
      <td width="16%"><label id="mand19" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label><strong>19</strong></td>
      <td width="84%"><p><strong>Does your research involve people highly dependent on medical care who may be unable to give consent? </strong>(<a href="https://www.nhmrc.gov.au/book/chapter-4-4-people-highly-dependent-medical-care-who-may-be-unable-give-consent">NS 4.4</a>)</p>
      <em>For example: patients in the emergency department or intensive care, unconscious people, terminal care.</em></td>
    </tr>
    <tr>
	  <td><input type="radio" name="ethicUnableConsentRadio" id="ethicUnableConsentRadioNo" value="no" disabled="disabled" runat="server"/>
      </td>
      <td>No – <em>please  skip to question 20.</em></td>
    </tr>
    <tr>
	  <td><input type="radio" name="ethicUnableConsentRadio" id="ethicUnableConsentRadioYes" value="yes" disabled="disabled" runat="server"/>
      </td>
      <td>Yes – <em>in  the space below describe the recruitment/consent process; and how participation in research is in the best interest of the participant?</em></td>
    </tr>
    <tr>
      <td colspan="2">
        <textarea name="ethicUnableConsent" id="ethicUnableConsent" runat="server" disabled="disabled" rows="10" cols="130" ></textarea>
      </td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table width="200" border="1">
    <tr>
      <td width="16%"><label id="mand20" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label><strong>20</strong></td>
      <td width="84%"><strong>Does your research involve people with a cognitive impairment, an intellectual disability, or a mental illness? </strong>(<a href="https://www.nhmrc.gov.au/book/chapter-4-5-people-cognitive-impairment-intellectual-disability-or-mental-illness">NS 4.5</a>)</td>
    </tr>
    <tr>
	  <td><input type="radio" name="ethicCogImpairmentRadio" id="ethicCogImpairmentRadioNo" value="no" disabled="disabled" runat="server"/>
      </td>
      <td>No – <em>please  skip to question 21.</em></td>
    </tr>
    <tr>
	  <td><input type="radio" name="ethicCogImpairmentRadio" id="ethicCogImpairmentRadioYes" value="yes" disabled="disabled" runat="server"/>
      </td>
      <td>Yes – <em>in  the space below describe the nature of the intellectual or mental impairment e.g. permanent, temporary or fluctuating; describe how the consent process will take into account the nature of the impairment.</em></td>
    </tr>
    <tr>
      <td colspan="2">
        <textarea name="ethicCogImpairment" id="ethicCogImpairment" runat="server" disabled="disabled" rows="10" cols="130" ></textarea>
      </td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table width="200" border="1">
    <tr>
      <td width="16%"><label id="mand21" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label><strong>21</strong></td>
      <td width="84%"><strong>Does your research involve people who may be involved in illegal activities? </strong>(<a href="https://www.nhmrc.gov.au/book/chapter-4-6-people-who-may-be-involved-illegal-activities">NS 4.6</a>)</td>
    </tr>
    <tr>
	  <td><input type="radio" name="ethicIllegalActRadio" id="ethicIllegalActRadioNo" value="no" disabled="disabled" runat="server"/>
      </td>
      <td>No – <em>please  skip to question 22.</em></td>
    </tr>
    <tr>
	  <td><input type="radio" name="ethicIllegalActRadio" id="ethicIllegalActRadioYes" value="yes" disabled="disabled" runat="server"/>
      </td>
      <td>Yes – <em>in  the space below please justify how the risk of discovery of illegal activities is justified by the benefits of the research.</em></td>
    </tr>
    <tr>
      <td colspan="2">
        <textarea name="ethicIllegal" id="ethicIllegal" runat="server" disabled="disabled" rows="10" cols="130" ></textarea>
      </td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table width="200" border="1">
    <tr>
      <td width="16%"><label id="mand22" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label><strong>22</strong></td>
      <td width="84%"><p><strong>Does your research involve people in dependent or unequal relationships?</strong>(<a href="https://www.nhmrc.gov.au/book/chapter-4-7-aboriginal-and-torres-strait-islander-peoples">NS 4.7</a>)</p>
      <em>Note: If your research will incidentally involve Aboriginal and Torres Strait Islanders because your study is on the general population you do not need to fill in this section. Complete this section if you are specifically targeting recruitment of Aboriginal and Torres Strait Islanders, or there is a potential for a high number of Aboriginal and Torres Strait Islanders to be recruited.</em></td>
    </tr>
    <tr>
	  <td><input type="radio" name="ethicAboriginalTorresRadio" id="ethicAboriginalTorresRadioNo" value="no" disabled="disabled" runat="server"/>
      </td>
      <td>No – <em>please  skip to question next section.</em></td>
    </tr>
    <tr>
	  <td><input type="radio" name="ethicAboriginalTorresRadio" id="ethicAboriginalTorresRadioYes" value="yes" disabled="disabled" runat="server"/>
      </td>
      <td>Yes – <em>please  answer Question 22 subsections below.</em></td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table width="200" border="1">
    <tr>
      <td width="16%"><label id="mand22a" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label><strong>22a</strong></td>
      <td width="84%"><strong>What is the estimated proportion of Aboriginal and Torres Strait Islanders in the population from which the participants will be recruited? </strong></td>
    </tr>
    <tr>
      <td colspan="2">
        <textarea name="ethicAboriginalPop" id="ethicAboriginalPop" runat="server" disabled="disabled" rows="10" cols="130" ></textarea>
      </td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table width="200" border="1">
    <tr>
      <td width="16%"><label id="mand22b" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label><strong>22b</strong></td>
      <td width="84%"><strong>Will Aboriginal and Torres Strait Islander status of participants be recorded?</strong></td>
    </tr>
    <tr>
	  <td><input type="radio" name="ethicAboriginalRecordRadio" id="ethicAboriginalRecordRadioNo" value="no" disabled="disabled" runat="server"/>
      </td>
      <td>No – <em>please  justify why below.</em></td>
    </tr>
    <tr>
	  <td><input type="radio" name="ethicAboriginalRecordRadio" id="ethicAboriginalRecordRadioYes" value="yes" disabled="disabled" runat="server"/>
      </td>
      <td>Yes – <em>please  justify why below.</em></td>
    </tr>
    <tr>
      <td colspan="2">
        <textarea name="ethicAboriginalRecord" id="ethicAboriginalRecord" runat="server" disabled="disabled" rows="10" cols="130" ></textarea>
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