<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EthicsPrintApp.aspx.cs" Inherits="EthicsPrintApp" EnableEventValidation="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Application</title>
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
        table p
        {
	        width:99%;
        }
        .style2
        {
           font-size: medium;
        }
    </style>
</head>
<body>
    <form id="frmPrint" runat="server">
    <div>
    <input type="button" id="btnPDF" value="Open PDF" onserverclick="btnPDF_ServerClick" runat="server" class="style2"/>

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
        <td>Interventions and therapies, including clinical  and non-clinical trials (<a href="https://www.nhmrc.gov.au/book/chapter-3-3-interventions-and-therapies-including-clinical-and-non-clinical-trials-and">NS 3.3</a>) </td>
        <td><label for="checkbox"></label>
          <p>
            <label>
              <label id="intTherapy_1" visible="false" runat="server">x</label>
              No </label>
          </p>
         </td>
        <td>
          <p>
            <label>
              <label id="intTherapy_0" visible="false" runat="server">x</label>
              Yes</label>
          </p>
        </td>
      </tr>
      <tr>
        <td>Human genetics (<a href="https://www.nhmrc.gov.au/book/chapter-3-5-human-genetics">NS 3.5</a>)</td>
        <td>
         <p>
          <label>
            <label visible="false" id="genetics_1" runat="server">x</label>
            No</label>
        </p>
        </td>
        <td>
         <p>
          <label>
            <label visible="false" id="genetics_0" runat="server">x</label>
            Yes</label>
          </p>
        </td>
      </tr>
      <tr>
        <td>Women who are pregnant and/or the human fetus (<a href="https://www.nhmrc.gov.au/book/chapter-4-1-women-who-are-pregnant-and-human-foetus">NS 4.1</a>)</td>
        <td><p>
        <label>
            <label visible="false" id="pregnancy_1" runat="server">x</label>
            No</label>
        </p></td>
        <td><p>
          <label>
            <label visible="false" id="pregnancy_0" runat="server">x</label>
            Yes</label>
            </p></td>
      </tr>
      <tr>
        <td><p>People who are highly dependent on medical care who may be unable to give consent (<a href="https://www.nhmrc.gov.au/book/chapter-4-3-people-dependent-or-unequal-relationships">NS 4.3</a> and <a href="https://www.nhmrc.gov.au/book/chapter-4-4-people-highly-dependent-medical-care-who-may-be-unable-give-consent">4.4</a>) </p></td>
        <td>
         <p>
          <label>
            <label visible="false" id="medConsent_1" runat="server">x</label>
            No</label>
         </p>
        </td>
        <td>
         <p>
          <label>
            <label visible="false" id="medConsent_0" runat="server">x</label>
            Yes</label>
          </p>
        </td>
      </tr>
      <tr>
        <td>People with a cognitive impairment, intellectual  disability or a mental illness (<a href="https://www.nhmrc.gov.au/book/chapter-4-5-people-cognitive-impairment-intellectual-disability-or-mental-illness">NS 4.5</a>)</td>
        <td><p>
        <label>
            <label visible="false" id="menDisability_1" runat="server">x</label>
            No</label>
        </p></td>
        <td><p>
          <label>
            <label visible="false" name="menDisability" id="menDisability_0" runat="server">x</label>
            Yes</label>
        </p></td>
      </tr>
      <tr>
        <td>Research specifically targeting Aboriginal or  Torres Strait Islanders (<a href="https://www.nhmrc.gov.au/book/chapter-4-7-aboriginal-and-torres-strait-islander-peoples">NS 4.7</a>)</td>
        <td><p>
        <label>
            <label visible="false" name="indigenous" id="indigenous_1" runat="server">x</label>
            No</label>
        </p></td>
        <td><p>
          <label>
            <label visible="false" name="indigenous" id="indigenous_0" runat="server">x</label>
            Yes</label>
        </p></td>
      </tr>
      <tr>
        <td>People who may be involved in illegal activities (<a href="https://www.nhmrc.gov.au/book/chapter-4-6-people-who-may-be-involved-illegal-activities">NS 4.6</a>)</td>
        <td><p>
        <label>
            <label visible="false" name="illegalAct" id="illegalAct_1" runat="server">x</label>
            No</label>
        </p></td>
        <td><p>
          <label>
            <label visible="false" name="illegalAct" id="illegalAct_0" runat="server">x</label>
            Yes</label>
        </p></td>
      </tr>
  </table>
  <table border="0">
  <tr>
    <td><p>A <strong>&ldquo;yes&rdquo;</strong> response to any of the above  questions would normally indicate your project is <strong>not eligible</strong> for a Low or Negligible Risk review. However, a &ldquo;Yes&rdquo;  answer does not necessarily, automatically, preclude the research from being  reviewed through a low risk review process. If you answered &ldquo;yes&rdquo; to any of the  above questions and you think your study should be reviewed through the low  risk process please justify why in the space below.</p></td></tr>
        <tr><td><p id="txtYesResp" runat="server"></p></td></tr>
        </table>



            <h3>SECTION 1 - General Information</h3>

  <table border="1" class="p">
    <tr>
      <td width="108">1</td>
      <td width="400">Project Title</td>
    </tr>
    <tr>
      <td colspan="2"><p name="projectTitle" id="projectTitle" runat="server"></p></td>
    </tr>
  </table>

  <p>&nbsp;</p>

  <table border="1">
    <tr>
      <td width="108">2</td>
      <td width="400">Please indicate the type of project</td>
    </tr>
    <tr>
    <td colspan="2">
       <p id="txtProjType" runat="server"></p>
      </td>
      </tr>
      <tr>
      <td colspan="2"><p id="projectTypeOther" runat="server" ></p></td>
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
      <td width="108">3a</td>
      <td width="400"><p>Background</p>
      <p>200 word limit</p></td>
    </tr>
    <tr>
      <td colspan="2"><p id="projectBackground" runat="server" ></p></td>
    </tr>
  </table>

  <p>&nbsp;</p>

  <table border="1">
    <tr>
      <td width="108">3b</td>
      <td width="400"><p>Aims and hypothesis</p></td>
    </tr>
    <tr>
      <td colspan="2"><p id="projectAim" runat="server" ></p></td>
    </tr>
  </table>

  <p>&nbsp;</p>
  
  <table border="1">
    <tr>
      <td width="108">3c</td>
      <td width="400"><p>Methods</p>
        <p>200 word limit</p></td>
    </tr>
    <tr>
      <td colspan="2"><p id="projectMethod" runat="server" ></p></td>
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
      <td> <p id="piName" runat="server" ></p></td>
      <td>Staff ID</td>
      <td><p id="piStaffId" runat="server" ></p></td>
    </tr>
    <tr>
      <td>School, Centre or Area</td>
      <td colspan="3"><p id="piSchool" runat="server" ></p></td>
    </tr>
    <tr>
      <td>Email</td>
      <td><p id="piEmail" runat="server" ></p></td>
      <td>Telephone</td>
      <td><p id="piPhone" runat="server" ></p></td>
    </tr>
    <tr>
      <td colspan="3">SOL Research Integrity Professional Development  Program training complete <em>(NOTE: this is  a requirement of approval)</em></td>
      <td><div align="center">
        <label id="integrityCert" runat="server">x</label>
      </div>
        <label for="integrityCert"></label>
          <div align="center">Yes (Please upload certificate)</div>
      </td>
    </tr>
  </table>
  <p>&nbsp;</p>
  <table border="1">
    <tr>
      <td align="center" width="50">5</td>
      <td class="style5"><p>Co-Investigators</p>
      <p><em>If candidacy is approved please attach a copy. Note: candidacy should be  approved before an ethics application is submitted.  NOTE: All Curtin staff and students MUST  complete the SOL Research Integrity Professional Development Program training –  this is a requirement of approval – attach your certificate</em></p></td>
    </tr>
  </table>
  <table border="1" id="tblCI" runat="server">
    <tr>
      <td class="style8">Name</td>
      <td class="style9">Role</td>
      <td>Candidacy approved</td>
      <td>Research Integrity training complete</td>
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
      <td width="400"><p id="txtContact" runat="server"></p></td>
    </tr>
  </table>
  <p>&nbsp;</p>
  <table border="1">
    <tr>
      <td align="center" width="103">6b</td>
      <td colspan="1"><p>Application control</p></td>
    </tr>
    <tr>
      <td>Name:</td>
      <td width="400"><p id="txtControl" runat="server"></p></td>
    </tr>
  </table>



  <h3><strong>
SECTION 2 - Themes in research ethics: Risk and benefit, consent
</strong></h3>

  <table width="95%" border="1">
    <tr>
      <td width="18%"><strong>7</strong></td>
      <td width="82%"><p><strong>Potential harm or risk to participants</strong></p>
      <p><em>Outline the potential risks to participants. If potential risks are  identified please explain how this research justifies the burden and risk to  participants (</em><a href="https://www.nhmrc.gov.au/book/chapter-2-1-risk-and-benefit"><em>NS 2.1</em></a><em>).</em><br/>
          <em>Consider illness or injury, potential side effects, but also include  potential embarrassment, economic loss, exposure to prosecution, anything  stressful, noxious or unpleasant, and complaints. Ensure you address these in  your Participant Information Statement.</em><br/>
        <em>Some examples of risks/expected adverse events (See Adverse Event  Guidelines for more information) may include:</em></p>
      <ul>
        <li><em>For a drug-intervention clinical trial there will be side effects of the  drug.</em></li>
      </ul>
      <em>For  psychological based studies risks may be psychological stress due to the  assessment;  there may be a potential for  increased risk of suicidality or self-harm; there may be a potential for worsening  of psychological disorder etc.</em></td>
    </tr>
    <tr>
      <td colspan="2"><p name="potHarmText" id="potHarmText" runat="server" ></p></td>
    </tr>
  </table>

<p>&nbsp;</p>


  <table width="95%" border="1">
    <tr>
      <td width="18%"><strong>8</strong></td>
      <td width="82%"><p><strong>Risk management strategy</strong></p>
      <p><em>Please outline how you will mitigate the risks  identified above and your plan of action for expected adverse events and other  identified risks. Please also outline your plan of action for unexpected  adverse events. The Human Research Ethics Office will use this information and  follow this procedure should an event or complaint occur. Please refer to  Adverse Event Guidelines for more information.</em></p></td>
    </tr>
    <tr>
      <td colspan="2"><p name="riskManText" id="riskManText" runat="server" ></p></td>
    </tr>
  </table>

<p>&nbsp;</p>


<table border="1">
  <tr>
    <td width="24%"><strong>9</strong></td>
    <td class="style3"><strong>Will participants be given financial or non-financial  incentives? </strong>(<a href="https://www.nhmrc.gov.au/book/chapter-2-2-general-requirements-consent">NS 2.2.10 - 2.2.11</a>)</td>
  </tr>
  <tr>
    <td><label visible="false" name="incentiveRadio" id="incentiveRadioNo" value="no" runat="server">x</label>
      <label for="incentiveRadio">No</label></td>
    <td class="style3">&nbsp;</td>
  </tr>
  <tr>
    <td><label visible="false" name="incentiveRadio" id="incentiveRadioYes" value="yes" runat="server">x</label>
      Yes</td>
    <td class="style3"><em>please provide details below. If a prize is used  please indicate the prize and the chances of winning this prize in the space below  and in the Participant Information Statement. Please refer to the </em><a href="http://legal.curtin.edu.au/comps/"><em>Competitions Toolkit</em></a><em> for further guidance on prizes. Details of the incentives should not be  detailed on the recruitment material; however this information may be included  in the Participants Information Statement.</em></td>
  </tr>
  <tr>
    <td colspan="2"><p name="financeText" id="financeText" runat="server" ></p></td>
    </tr>
</table>


<p>&nbsp;</p>

  <table width="72%" border="1">
    <tr>
      <td width="10%"><strong>10</strong></td>
      <td colspan="5"><p><strong>Please select how you are going to recruit  participants (select all that apply) and in the space below describe your  recruitment process.</strong></p>
      <em>When  you are describing your recruitment processes please indicate who is going to  talk to the potential participants, how they contact the researcher or the  researcher contacts them etc. If you are using telephone calls, flyers, social  media, radio announcements etc., please provide a copy of the information  and/or a transcript. If you are using any form of print media (e.g. flyers,  newsletters, social media etc.) you need to put the ethics approval number and  the Curtin logo on the document. Please refer to the </em><a href="http://brand.curtin.edu.au/"><em>Curtin Brand website</em></a><em> for information on advertising.</em></td>
    </tr>
    <tr>
      <td><label name="database" id="database" runat="server">x</label>
      <label for="database"></label></td>
      <td width="32%"><p>Database/medical  records <em>(please describe the source)</em>:</p></td>
      <td class="style5"><p id="databaseText" 
              runat="server"  style="height: 50px"/></td>
      <td width="7%"><label name="wordMouth" id="wordMouth" runat="server">x</label></td>
      <td width="32%"><p>Snowball recruitment or word of mouth etc.<em>(please list)</em>:</p></td>
      <td width="8%"><p id="wordMouthText" 
              runat="server"  style="height: 50px"></p></td>
    </tr>
    <tr>
      <td><label name="socialMedia" id="socialMedia" runat="server">x</label></td>
      <td><p>Social media  including Facebook, Yammer, LinkedIn, Twitter etc. <em>(please list)</em>:</p></td>
      <td class="style5"><p id="socialMediaText" 
              runat="server"  style="height: 50px"></p></td>
      <td><label name="printMedia" id="printMedia" runat="server">x</label></td>
      <td><p>Print media including flyers, newspapers, newsletters  etc. <em>(please  list sources)</em>:</p></td>
      <td><p id="printMediaText" runat="server" 
               style="height: 50px"></p></td>
    </tr>
    <tr>
      <td><label name="commGroups" id="commGroups" runat="server">x</label></td>
      <td><p>Classroom or  hospital or clinic or community groups etc. <em>(please  list sources)</em>:</p></td>
      <td class="style5"><p id="commGroupsText" 
              runat="server"  style="height: 50px"></p></td>
      <td><label name="radioTV" id="radioTV" runat="server">x</label></td>
      <td><p>Radio/television<em>(please  list sources)</em>:</p></td>
      <td><p id="radioTVText" runat="server" 
               style="height: 50px"></p></td>
    </tr>
    <tr>
      <td><label name="recruitOther" id="recruitOther" runat="server">x</label></td>
      <td><p>Other <em>(please  describe)</em>:</p></td>
      <td colspan="4"><p id="recruitOtherText" 
              runat="server"  style="height: 50px"></p></td>
    </tr>
    <tr>
      <td colspan="6"><p name="recruitTxt" id="recruitTxt" runat="server" rows="10" cols="100" ></p></td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table width="95%" border="1">
    <tr>
      <td width="14%"><strong>11</strong></td>
      <td width="86%"><p><strong>Will participants provide consent? </strong>(<a href="https://www.nhmrc.gov.au/book/chapter-2-2-general-requirements-consent">NS 2.2</a>, <a href="https://www.nhmrc.gov.au/book/national-statement-ethical-conduct-human-research-2007-updated-december-2013/chapter-2-3-qualif">NS 2.3</a>)</p>
      <em>Please provide a copy of the Participant Information Statement  and Consent Forms. If you are recruiting children provide a Parent Information Statement  and Consent Form, and a Child Information Statement and Assent Form if  appropriate. If you are using secondary data and therefore consent is not  required, select &ldquo;no&rdquo; and provide an explanation in the space below.</em></td>
    </tr>
    <tr>
      <td><label visible="false" name="consentRadio" id="consentRadioNo" value="no" runat="server">x</label></td>
      <td>No – <em>please  address section 2.3 of the National Statement below </em></td>
    </tr>
    <tr>
      <td><label visible="false" name="consentRadio" id="consentRadioYes" value="yes" runat="server">x</label>
      <label for="consentRadio"></label></td>
      <td>Yes – <em>please  describe below how you will obtain consent.</em></td>
    </tr>
    <tr>
      <td colspan="2"><p id="consentText" runat="server" ></p></td>
    </tr>
  </table>


<p>&nbsp;</p>

<table border="1">
  <tr>
    <td width="13%"><strong>12</strong></td>
    <td class="style4">Does the research use deception, concealment,  incomplete disclosure, limited disclosure, an opt-out approach, or use of  information, samples, health information etc., without the specified consent  from those persons? (<a href="https://www.nhmrc.gov.au/book/national-statement-ethical-conduct-human-research-2007-updated-december-2013/chapter-2-3-qualif">NS 2.3</a>)</td>
  </tr>
  <tr>
    <td><label visible="false" name="deceptionRadio" id="deceptionRadioNo" runat="server">x</label>
      <label for="deceptionRadioNo"></label></td>
    <td class="style4">No</td>
  </tr>
  <tr>
    <td><label visible="false" name="deceptionRadio" id="deceptionRadioYes" runat="server">x</label>
      <label for="deceptionRadioYes"></label></td>
    <td class="style4">Yes – <em>please  detail the methods below. Please describe how this method is essential to the  research aims and how participants will be de-briefed after the study.</em></td>
  </tr>
  <tr>
    <td colspan="2"><label for="textfield"></label>
      <p id="deceptionText" runat="server" ></p></td>
    </tr>
</table>




<h3><strong> SECTION 3: Ethical considerations specific to research methods or fields</strong></h3>
<p>&nbsp;</p>

  <table border="1">
    <tr>
      <td width="15%"><strong>13</strong></td>
      <td width="85%"><p><strong>Is your study a clinical trial? </strong>(<a href="https://www.nhmrc.gov.au/book/chapter-3-3-interventions-and-therapies-including-clinical-and-non-clinical-trials-and">NS 3.3</a>)</p>
      <em>A clinical trial is defined as any research project  that prospectively assigns human subjects to intervention and comparison groups  to study the cause-and-effect relationship between a medical intervention and a  health outcome. Medical intervention means any intervention used to modify a  health outcome. This definition includes, drugs, surgical procedures, devices,  behavioral treatments, process-of-care change etc.</em></td>
    </tr>
    <tr>
      <td><label visible="false" name="clinicalRadio" id="clinicalRadioNo" runat="server">x</label>
      </td>
      <td>No – <em>please  skip to question 14</em></td>
    </tr>
    <tr>
      <td><label visible="false" name="clinicalRadio" id="clinicalRadioYes" runat="server">x</label>
	</td>
      <td>Yes – <em>please  answer Question 13 subsections below.</em></td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table width="200" border="1">
    <tr>
      <td width="16%"><strong>13a</strong></td>
      <td width="84%"><strong>Will a placebo/non-treatment group be used? </strong>(<a href="https://www.nhmrc.gov.au/book/chapter-3-3-interventions-and-therapies-including-clinical-and-non-clinical-trials-and">NS 3.3.10</a>)</td>
    </tr>
    <tr>
      <td><label visible="false" name="placeboRadio" id="placeboRadioNo" runat="server">x</label>
      </td>
      <td>No –<em> please outline why a placebo or non-treatment group  will not be used.</em></td>
    </tr>
    <tr>
      <td><label visible="false" name="placeboRadio" id="placeboRadioYes" runat="server">x</label></td>
      <td>Yes – <em>please  describe why a placebo or non-treatment group is the best comparator.</em></td>
    </tr>
    <tr>
      <td colspan="2">
      <p id="placeboText" runat="server" ></p></td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table width="200" border="1">
    <tr>
      <td width="16%"><strong>13b</strong></td>
      <td width="84%"><strong>Has this trial been registered? </strong>(<a href="https://www.nhmrc.gov.au/book/chapter-3-3-interventions-and-therapies-including-clinical-and-non-clinical-trials-and">NS 3.3.12</a>)</td>
    </tr>
    <tr>
      <td><label visible="false" name="trialRegRadio" id="trialRegRadioNo" runat="server">x</label>
      </td>
      <td>No</td>
    </tr>
    <tr>
      <td><label visible="false" name="trialRegRadio" id="trialRegRadioYes" runat="server">x</label>
      </td>
      <td>Yes – <em>please  provide the registration number and the name of the trial registry in the space  below.</em></td>
    </tr>
    <tr>
      <td colspan="2">
      <p id="trialRegDetails" runat="server" ></p></td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table width="200" border="1">
    <tr>
      <td width="16%"><strong>13c</strong></td>
      <td width="84%"><strong>Are the facilities, expertise and experience available sufficient for the trial to be conducted safely? </strong>(<a href="https://www.nhmrc.gov.au/book/chapter-3-3-interventions-and-therapies-including-clinical-and-non-clinical-trials-and">NS 3.3.5</a>)</td>
    </tr>
    <tr>
      <td><label visible="false" name="trialResRadio" id="trialResRadioNo" runat="server">x</label>
      </td>
      <td>No – <em>please  indicate how you will address this in the space below.</em></td>
    </tr>
    <tr>
      <td><label visible="false" name="trialResRadio" id="trialResRadioYes" runat="server">x</label>
      </td>
      <td>Yes</td>
    </tr>
    <tr>
      <td colspan="2">
      <p id="trialResText" runat="server"  ></p></td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table border="1">
    <tr>
      <td width="16%"><strong>13d</strong></td>
      <td width="84%"><strong>Does your Participant Information Statement make clear to the participant whether they will have continued access after the trial to  treatment they have received during the trial, and on what terms? </strong>(<a href="https://www.nhmrc.gov.au/book/chapter-3-3-interventions-and-therapies-including-clinical-and-non-clinical-trials-and">NS 3.3.18</a>)</td>
    </tr>
    <tr>
      <td><label visible="false" name="trialTreatRadio" id="trialTreatRadioNo" runat="server">x</label>
      </td>
      <td>No</td>
    </tr>
    <tr>
      <td><label visible="false" name="trialTreatRadio" id="trialTreatRadioYes" runat="server">x</label>
      </td>
      <td>Yes</td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table width="200" border="1">
    <tr>
      <td width="16%"><strong>14</strong></td>
      <td width="84%"><strong>Does your research use health information (including  biospecimens) that may reveal information that may be important for the health  or future health of the donor(s), their blood relatives or their community? </strong>(<a href="https://www.nhmrc.gov.au/book/chapter-3-4-human-biospecimens-laboratory-based-research">NS 3.4.10</a>, <a href="https://www.nhmrc.gov.au/book/chapter-3-5-human-genetics">3.5.1 and 3.5.2</a>)</td>
    </tr>
    <tr>
      <td><label visible="false" name="healthInfoRadio" id="healthInfoRadioNo" runat="server">x</label>
      </td>
      <td>No</td>
    </tr>
    <tr>
      <td><label visible="false" name="healthInfoRadio" id="healthInfoRadioYes" runat="server">x</label></td>
      <td>Yes – <em>indicate  below how you will address the management of any proposed disclosure or  non-disclosure of that information.</em></td>
    </tr>
    <tr>
      <td colspan="2">
      <p id="healthInfoText" runat="server" ></p></td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table width="200" border="1">
    <tr>
      <td width="16%"><strong>15</strong></td>
      <td width="84%"><p><strong>Does your research involve human genetics? </strong>(<a href="https://www.nhmrc.gov.au/book/chapter-3-5-human-genetics">NS 3.5</a>)</p>
      <em>Specific requirements for research involving fetal  tissue are detailed in </em><a href="https://www.nhmrc.gov.au/book/chapter-4-1-women-who-are-pregnant-and-human-foetus"><em>Chapter 4.1</em></a><em> of the National Statement. Research involving  human embryos and gametes, including the derivation of human embryonic stem  cell lines, is separately governed by the Research Involving Human Embryos Act  2002 (Cth) and the Ethical Guidelines on the use of Assisted Reproductive  Technology in Clinical Practice and Research (2007). Please refer to Chapter  3.5 of the National Statement for more information.</em></td>
    </tr>
    <tr>
      <td><label visible="false" name="humanGenRadio" id="humanGenRadioNo" runat="server">x</label>
      </td>
      <td>No</td>
    </tr>
    <tr>
      <td><label visible="false" name="humanGenRadio" id="humanGenRadioYes" runat="server">x</label>
      </td>
      <td>Yes – <em>please  address in the space below the parts of Section 3.5 of the National Statement  that are relevant to this project.</em></td>
    </tr>
    <tr>
      <td colspan="2">
      <p id="humanGenText" runat="server" ></p></td>
    </tr>
  </table>



  <h3> <strong> SECTION 4 - Ethical considerations specific to participants</strong></h3>
<p>&nbsp;</p>
  <table width="200" border="1">
    <tr>
      <td width="16%"><strong>16</strong></td>
      <td width="84%"><strong>Does your research involve women who are pregnant and/or  the human fetus?</strong></td>
    </tr>
    <tr>
      <td><label visible="false" name="ethicPregnantRadio" id="ethicPregnantRadioNo" runat="server">x</label>
      </td>
      <td>No – <em>please  skip to question 17.</em></td>
    </tr>
    <tr>
      <td><label visible="false" name="ethicPregnantRadio" id="ethicPregnantRadioYes" runat="server">x</label>
      </td>
      <td>Yes – <em>please answer Question 16 subsections below.</em></td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table width="200" border="1">
    <tr>
      <td width="16%"><strong>16a</strong></td>
      <td width="84%"><strong>Will steps be taken to ensure that the well-being and  care of the woman who is pregnant and her fetus takes precedence over the aims  of the research? </strong>(<a href="https://www.nhmrc.gov.au/book/chapter-4-1-women-who-are-pregnant-and-human-foetus">NS 4.1.1</a>)</td>
    </tr>
    <tr>
	  <td><label visible="false" name="ethicPregnantWellRadio" id="ethicPregnantWellRadioNo" runat="server">x</label>
      </td>
      <td>No – <em>please  justify why below.</em></td>
    </tr>
    <tr>
	  <td><label visible="false" name="ethicPregnantWellRadio" id="ethicPregnantWellRadioYes" runat="server">x</label>
      </td>
      <td>Yes – <em>please  outline the procedures below.</em></td>
    </tr>
    <tr>
      <td colspan="2">
        <p id="ethicPregnantWell" runat="server"  ></p>
      </td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table width="200" border="1">
    <tr>
      <td width="16%"><strong>16b</strong></td>
      <td width="84%"><strong>Will the information about research be separate from information about routine clinical care? </strong></td>
    </tr>
    <tr>
	  <td><label visible="false" name="ethicPregnantInfoRadio" id="ethicPregnantInfoRadioNo" runat="server">x</label>
      </td>
      <td>No – <em>please  justify why the information will not be provided separately below.</em></td>
    </tr>
    <tr>
	  <td><label visible="false" name="ethicPregnantInfoRadio" id="ethicPregnantInfoRadioYes" runat="server">x</label>
      </td>
      <td>Yes</td>
    </tr>
    <tr>
      <td colspan="2">
        <p id="ethicPregnantInfo" runat="server"></p>
      </td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table width="200" border="1">
    <tr>
      <td width="16%"><strong>17</strong></td>
      <td width="84%"><strong>Does your research involve children and young people? </strong>(<a href="https://www.nhmrc.gov.au/book/chapter-4-2-children-and-young-people">NS 4.2</a>)</td>
    </tr>
    <tr>
	  <td><label visible="false" name="ethicYoungRadio" id="ethicYoungRadioNo" runat="server">x</label>
      </td>
      <td>No – <em>please  skip to question 18.</em></td>
    </tr>
    <tr>
	  <td><label visible="false" name="ethicYoungRadio" id="ethicYoungRadioYes" runat="server">x</label>
      </td>
      <td>Yes – <em>in  the space below address why participation of children or young people is indispensable to this research; and how this study has been designed to be appropriate for children or young people.</em></td>
    </tr>
    <tr>
      <td colspan="2">
        <p id="ethicYoung" runat="server"  ></p>
      </td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table width="200" border="1">
    <tr>
      <td width="16%"><strong>17a</strong></td>
      <td width="84%"><strong>Do you have a Workng With Children's (WWC) card? </strong></td>
    </tr>
    <tr>
	  <td><label visible="false" name="ethicWWCRadio" id="ethicWWCRadioNo" runat="server">x</label>
      </td>
      <td>No – <em>it  is a legal requirement to have a WWC’s card. Please arrange to submit your WWC application and provide a receipt or a have a card BEFORE you submit this ethics application. Ethics approval will not be given without a WWC.</em></td>
    </tr>
    <tr>
	  <td><label visible="false" name="ethicWWCRadio" id="ethicWWCRadioYes" runat="server">x</label>
      </td>
      <td>Yes – <em>please upload a copy to your application.</em></td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table width="200" border="1">
    <tr>
      <td width="16%"><strong>18</strong></td>
      <td width="84%"><p><strong>Does your research involve people in dependent or unequal relationships?</strong>(<a href="https://www.nhmrc.gov.au/book/chapter-4-3-people-dependent-or-unequal-relationships">NS 4.3</a>)</p>
      <em>For example: teachers and their students, health care professionals and their patients, employers and their employees.</em></td>
    </tr>
    <tr>
	  <td><label visible="false" name="ethicUnequalRelRadio" id="ethicUnequalRelRadioNo" runat="server">x</label>
      </td>
      <td>No – <em>please  skip to question 19.</em></td>
    </tr>
    <tr>
	  <td><label visible="false" name="ethicUnequalRelRadio" id="ethicUnequalRelRadioYes" runat="server">x</label>
      </td>
      <td>Yes – <em>in  the space below describe the dependent relationship between the participants and the researcher, members of the research team and/or any person involved in the recruitment/consent process; how will the process of obtaining consent enable persons in dependent relationships to give voluntary consent; if a participant choose to withdraw from the research, how will the ongoing dependent relationship with the participant be maintained?</em></td>
    </tr>
    <tr>
      <td colspan="2">
        <p id="ethicUnequalRel" runat="server"  ></p>
      </td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table width="200" border="1">
    <tr>
      <td width="16%"><strong>19</strong></td>
      <td width="84%"><p><strong>Does your research involve people highly dependent on medical care who may be unable to give consent? </strong>(<a href="https://www.nhmrc.gov.au/book/chapter-4-4-people-highly-dependent-medical-care-who-may-be-unable-give-consent">NS 4.4</a>)</p>
      <em>For example: patients in the emergency department or intensive care, unconscious people, terminal care.</em></td>
    </tr>
    <tr>
	  <td><label visible="false" name="ethicUnableConsentRadio" id="ethicUnableConsentRadioNo" runat="server">x</label>
      </td>
      <td>No – <em>please  skip to question 20.</em></td>
    </tr>
    <tr>
	  <td><label visible="false" name="ethicUnableConsentRadio" id="ethicUnableConsentRadioYes" runat="server">x</label>
      </td>
      <td>Yes – <em>in  the space below describe the recruitment/consent process; and how participation in research is in the best interest of the participant?</em></td>
    </tr>
    <tr>
      <td colspan="2">
        <p id="ethicUnableConsent" runat="server"  ></p>
      </td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table width="200" border="1">
    <tr>
      <td width="16%"><strong>20</strong></td>
      <td width="84%"><strong>Does your research involve people with a cognitive impairment, an intellectual disability, or a mental illness? </strong>(<a href="https://www.nhmrc.gov.au/book/chapter-4-5-people-cognitive-impairment-intellectual-disability-or-mental-illness">NS 4.5</a>)</td>
    </tr>
    <tr>
	  <td><label visible="false" name="ethicCogImpairmentRadio" id="ethicCogImpairmentRadioNo" runat="server">x</label>
      </td>
      <td>No – <em>please  skip to question 21.</em></td>
    </tr>
    <tr>
	  <td><label visible="false" name="ethicCogImpairmentRadio" id="ethicCogImpairmentRadioYes" runat="server">x</label>
      </td>
      <td>Yes – <em>in  the space below describe the nature of the intellectual or mental impairment e.g. permanent, temporary or fluctuating; describe how the consent process will take into account the nature of the impairment.</em></td>
    </tr>
    <tr>
      <td colspan="2">
        <p id="ethicCogImpairment" runat="server"  ></p>
      </td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table width="200" border="1">
    <tr>
      <td width="16%"><strong>21</strong></td>
      <td width="84%"><strong>Does your research involve people who may be involved in illegal activities? </strong>(<a href="https://www.nhmrc.gov.au/book/chapter-4-6-people-who-may-be-involved-illegal-activities">NS 4.6</a>)</td>
    </tr>
    <tr>
	  <td><label visible="false" name="ethicIllegalActRadio" id="ethicIllegalActRadioNo" runat="server">x</label>
      </td>
      <td>No – <em>please  skip to question 22.</em></td>
    </tr>
    <tr>
	  <td><label visible="false" name="ethicIllegalActRadio" id="ethicIllegalActRadioYes" runat="server">x</label>
      </td>
      <td>Yes – <em>in  the space below please justify how the risk of discovery of illegal activities is justified by the benefits of the research.</em></td>
    </tr>
    <tr>
      <td colspan="2">
        <p id="ethicIllegal" runat="server"  ></p>
      </td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table width="200" border="1">
    <tr>
      <td width="16%"><strong>22</strong></td>
      <td width="84%"><p><strong>Does your research involve people in dependent or unequal relationships?</strong>(<a href="https://www.nhmrc.gov.au/book/chapter-4-7-aboriginal-and-torres-strait-islander-peoples">NS 4.7</a>)</p>
      <em>Note: If your research will incidentally involve Aboriginal and Torres Strait Islanders because your study is on the general population you do not need to fill in this section. Complete this section if you are specifically targeting recruitment of Aboriginal and Torres Strait Islanders, or there is a potential for a high number of Aboriginal and Torres Strait Islanders to be recruited.</em></td>
    </tr>
    <tr>
	  <td><label visible="false" name="ethicAboriginalTorresRadio" id="ethicAboriginalTorresRadioNo" runat="server">x</label>
      </td>
      <td>No – <em>please  skip to question next section.</em></td>
    </tr>
    <tr>
	  <td><label visible="false" name="ethicAboriginalTorresRadio" id="ethicAboriginalTorresRadioYes" runat="server">x</label>
      </td>
      <td>Yes – <em>please  answer Question 22 subsections below.</em></td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table width="200" border="1">
    <tr>
      <td width="16%"><strong>22a</strong></td>
      <td width="84%"><strong>What is the estimated proportion of Aboriginal and Torres Strait Islanders in the population from which the participants will be recruited? </strong></td>
    </tr>
    <tr>
      <td colspan="2">
        <p id="ethicAboriginalPop" runat="server"  ></p>
      </td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table width="200" border="1">
    <tr>
      <td width="16%"><strong>22b</strong></td>
      <td width="84%"><strong>Will Aboriginal and Torres Strait Islander status of participants be recorded?</strong></td>
    </tr>
    <tr>
	  <td><label visible="false" name="ethicAboriginalRecordRadio" id="ethicAboriginalRecordRadioNo" runat="server">x</label>
      </td>
      <td>No – <em>please  justify why below.</em></td>
    </tr>
    <tr>
	  <td><label visible="false" name="ethicAboriginalRecordRadio" id="ethicAboriginalRecordRadioYes" runat="server">x</label>
      </td>
      <td>Yes – <em>please  justify why below.</em></td>
    </tr>
    <tr>
      <td colspan="2">
        <p id="ethicAboriginalRecord" runat="server"  ></p>
      </td>
    </tr>
  </table>


  <h3> <strong> SECTION 5 - Process of research governance and ethical review</strong></h3>
<p>&nbsp;</p>

  <table width="200" border="1">
    <tr>
      <td width="16%"><strong>23</strong></td>
      <td width="84%"><strong>Are there any potential conflicts of interest?</strong></td>
    </tr>
    <tr>
	  <td><label visible="false" name="ethicConflictInterestRadio" id="ethicConflictInterestRadioNo" runat="server">x</label>
      </td>
      <td>No – <em>please  skip to question next section.</em></td>
    </tr>
    <tr>
	  <td><label visible="false" name="ethicConflictInterestRadio" id="ethicConflictInterestRadioYes" runat="server">x</label>
      </td>
      <td>Yes – <em>please  describe in the space below.</em></td>
    </tr>
    <tr>
      <td colspan="2">
        <p id="txtConflict" runat="server"  ></p>
      </td>
    </tr>
  </table>


  <h3>SECTION 6 - Attachments</h3>
        <div id="divUploads" runat="server">
    <table id="tblUploads" border="1" runat="server">
        <tr>
            <th width="200"><p>Filename</p></th>
            <th width="200"><p>Attachment Type</p></th>
            <th width="97"><p>File Version</p></th>
            <th width="97"><p>File Date</p></th>
        </tr>
    </table>
    </div>
    <br />
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


                <h3>SECTION 7 - Declaration & Submission</h3>

        <table border="1">

        <tr>
          <td colspan="2"><div align="left"><p>I declare that:</p>
              <ul>
                  <li>The information provided in this application is truthful and as complete as possible.</li>
                  <li>I undertake to conduct research in accordance with the approved protocol, the most recent National Statement on Ethical Conduct in Human Research, relevant legislation and the policies and procedures of Curtin University.</li>
                  <li>Where I am the Project Supervisor for research described to be conducted by a student of Curtin University, I declare that I have provided guidance to the student in the design, methodology and consideration of ethical issues of the proposed research; that the student has received the relevant research and ethics training for this project; and, that I will monitor the project during data collection.</li>
                  <li>I make this application on the basis that the information it contains is confidential and will be used by Curtin University for the purposes of ethical review and monitoring of the research project described herein, and to satisfy reporting requirements to regulatory bodies. The information will not be used for any other purpose without my prior consent.</li>
              </ul></div>
          </td>
        </tr>
        <tr>
                <td colspan="1">Confirm</td>
                <td colspan="1"><label name="radioDeclare" id="radioDeclare" runat="server">x</label></td>
        </tr>
        <tr>
            <td colspan="1">
                <label id="lblHOS">Choose Head of School: </label>
                
            </td>
            <td colspan="1">
                <p id="txtHOS" runat="server"></p>
            </td>
        </tr>
        </table>
        <br />
        <table id="tblDec" border="1" visible="false" runat="server">
        <tr>
            <th colspan="2">
                <label id="lblDeclined" runat="server"></label>
            </th>
        </tr>
        <tr>
            <td colspan="2">
                <p id="txtDeclined" runat="server"></p>
            </td>
        </tr>
        </table>

    </div>
    </form>
</body>
</html>
