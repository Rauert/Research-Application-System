<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EthicsEditAppS2.aspx.cs" Inherits="EthicsEditAppS2" %>

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
        .style3
        {
            width: 219%;
        }
        .style4
        {
            width: 126%;
        }
        #recruitOtherText
        {
            width: 308px;
            margin-left: 0px;
        }
        .style5
        {
            width: 11%;
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
        <br /><br />
        <br /><br />
        <label id="lblColour">Note that sections marked red are incomplete.</label>
        </td>



        <td width="1000">


        <h3>
<strong>SECTION 2 - Themes in research ethics: Risk and benefit, consent
</strong></h3>
<form id="frmS2" name="potentialHarm" method="post" runat="server">
  <table width="95%" border="1">
    <tr>
      <td width="18%"><label id="mand7" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label><strong>7</strong></td>
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
      <td colspan="2"><textarea name="potHarmText" id="potHarmText" runat="server" rows="10" cols="130" disabled="disabled"></textarea></td>
    </tr>
  </table>

<p>&nbsp;</p>


  <table width="95%" border="1">
    <tr>
      <td width="18%"><label id="mand8" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label><strong>8</strong></td>
      <td width="82%"><p><strong>Risk management strategy</strong></p>
      <p><em>Please outline how you will mitigate the risks  identified above and your plan of action for expected adverse events and other  identified risks. Please also outline your plan of action for unexpected  adverse events. The Human Research Ethics Office will use this information and  follow this procedure should an event or complaint occur. Please refer to  Adverse Event Guidelines for more information.</em></p></td>
    </tr>
    <tr>
      <td colspan="2"><textarea name="riskManText" id="riskManText" runat="server" rows="10" cols="130" disabled="disabled"></textarea></td>
    </tr>
  </table>

<p>&nbsp;</p>


<table border="1">
  <tr>
    <td width="24%"><label id="mand9" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label><strong>9</strong></td>
    <td class="style3"><strong>Will participants be given financial or non-financial  incentives? </strong>(<a href="https://www.nhmrc.gov.au/book/chapter-2-2-general-requirements-consent">NS 2.2.10 - 2.2.11</a>)</td>
  </tr>
  <tr>
    <td><input type="radio" name="incentiveRadio" id="incentiveRadioNo" value="no" runat="server" disabled="disabled"/>
      <label for="incentiveRadio">No</label></td>
    <td class="style3">&nbsp;</td>
  </tr>
  <tr>
    <td><input type="radio" name="incentiveRadio" id="incentiveRadioYes" value="yes" runat="server" disabled="disabled"/>
      Yes</td>
    <td class="style3"><em>please provide details below. If a prize is used  please indicate the prize and the chances of winning this prize in the space below  and in the Participant Information Statement. Please refer to the </em><a href="http://legal.curtin.edu.au/comps/"><em>Competitions Toolkit</em></a><em> for further guidance on prizes. Details of the incentives should not be  detailed on the recruitment material; however this information may be included  in the Participants Information Statement.</em></td>
  </tr>
  <tr>
    <td colspan="2"><textarea name="financeText" id="financeText" runat="server" rows="10" cols="130" disabled="disabled"></textarea></td>
    </tr>
</table>


<p>&nbsp;</p>

  <table width="72%" border="1">
    <tr>
      <td width="10%"><label id="mand10" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label><strong>10</strong></td>
      <td colspan="2"><p><strong>Please select how you are going to recruit  participants (select all that apply) and in the space below describe your  recruitment process.</strong></p>
      <em>When  you are describing your recruitment processes please indicate who is going to  talk to the potential participants, how they contact the researcher or the  researcher contacts them etc. If you are using telephone calls, flyers, social  media, radio announcements etc., please provide a copy of the information  and/or a transcript. If you are using any form of print media (e.g. flyers,  newsletters, social media etc.) you need to put the ethics approval number and  the Curtin logo on the document. Please refer to the </em><a href="http://brand.curtin.edu.au/"><em>Curtin Brand website</em></a><em> for information on advertising.</em></td>
    </tr>
    <tr>
      <td><input type="checkbox" name="database" id="database" runat="server" disabled="disabled"/>
      <label for="database"></label></td>
      <td><p>Database/medical records <em>(please describe the source)</em>:</p></td>
      <td><textarea name="databaseText" id="databaseText" runat="server" rows="5" cols="50" disabled="disabled"></textarea></td>
</tr>
    <tr>
      <td><input type="checkbox" name="wordMouth" id="wordMouth" runat="server" disabled="disabled"/></td>
      <td><p>Snowball recruitment or word of mouth etc.<em>(please list)</em>:</p></td>
      <td><textarea name="wordMouthText" id="wordMouthText" runat="server" rows="5" cols="50" disabled="disabled"></textarea></td>
    </tr>
    <tr>
      <td><input type="checkbox" name="socialMedia" id="socialMedia" runat="server" disabled="disabled"/></td>
      <td><p>Social media  including Facebook, Yammer, LinkedIn, Twitter etc. <em>(please list)</em>:</p></td>
      <td><textarea name="socialMediaText" id="socialMediaText" runat="server" rows="5" cols="50" disabled="disabled"></textarea></td>
    </tr>
    <tr>
      <td><input type="checkbox" name="printMedia" id="printMedia" runat="server" disabled="disabled"/></td>
      <td><p>Print media including flyers, newspapers, newsletters  etc. <em>(please  list sources)</em>:</p></td>
      <td><textarea name="printMediaText" id="printMediaText" runat="server" rows="5" cols="50" disabled="disabled"></textarea></td>
    </tr>
    <tr>
      <td><input type="checkbox" name="commGroups" id="commGroups" runat="server" disabled="disabled"/></td>
      <td><p>Classroom or  hospital or clinic or community groups etc. <em>(please  list sources)</em>:</p></td>
      <td><textarea name="commGroupsText" id="commGroupsText" runat="server" rows="5" cols="50" disabled="disabled"></textarea></td>
    </tr>
    <tr>
      <td><input type="checkbox" name="radioTV" id="radioTV" runat="server" disabled="disabled"/></td>
      <td><p>Radio/television<em>(please  list sources)</em>:</p></td>
      <td><textarea name="radioTVText" id="radioTVText" runat="server" rows="5" cols="50" disabled="disabled"></textarea></td>
    </tr>
    <tr>
      <td><input type="checkbox" name="recruitOther" id="recruitOther" runat="server" disabled="disabled"/></td>
      <td><p>Other <em>(please  describe)</em>:</p></td>
      <td><textarea name="recruitOtherText" id="recruitOtherText" runat="server" rows="5" cols="50" disabled="disabled"></textarea></td>
    </tr>
    <tr>
      <td colspan="3"><textarea name="recruitTxt" id="recruitTxt" runat="server" rows="10" cols="100" disabled="disabled"></textarea></td>
    </tr>
  </table>

<p>&nbsp;</p>

  <table width="95%" border="1">
    <tr>
      <td width="14%"><label id="mand11" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label><strong>11</strong></td>
      <td width="86%"><p><strong>Will participants provide consent? </strong>(<a href="https://www.nhmrc.gov.au/book/chapter-2-2-general-requirements-consent">NS 2.2</a>, <a href="https://www.nhmrc.gov.au/book/national-statement-ethical-conduct-human-research-2007-updated-december-2013/chapter-2-3-qualif">NS 2.3</a>)</p>
      <em>Please provide a copy of the Participant Information Statement  and Consent Forms. If you are recruiting children provide a Parent Information Statement  and Consent Form, and a Child Information Statement and Assent Form if  appropriate. If you are using secondary data and therefore consent is not  required, select &ldquo;no&rdquo; and provide an explanation in the space below.</em></td>
    </tr>
    <tr>
      <td><input type="radio" name="consentRadio" id="consentRadioNo" value="no" runat="server" disabled="disabled"/></td>
      <td>No – <em>please  address section 2.3 of the National Statement below </em></td>
    </tr>
    <tr>
      <td><input type="radio" name="consentRadio" id="consentRadioYes" value="yes" runat="server" disabled="disabled"/>
      <label for="radio"></label></td>
      <td>Yes – <em>please  describe below how you will obtain consent.</em></td>
    </tr>
    <tr>
      <td colspan="2"><textarea type="text" name="consentText" id="consentText" runat="server" rows="10" cols="130" disabled="disabled"></textarea></td>
    </tr>
  </table>


<p>&nbsp;</p>

<table border="1">
  <tr>
    <td width="13%"><label id="mand12" runat="server" visible="false" style="font-weight:900;font-size:xx-large;color:Red;">* </label><strong>12</strong></td>
    <td class="style4">Does the research use deception, concealment,  incomplete disclosure, limited disclosure, an opt-out approach, or use of  information, samples, health information etc., without the specified consent  from those persons? (<a href="https://www.nhmrc.gov.au/book/national-statement-ethical-conduct-human-research-2007-updated-december-2013/chapter-2-3-qualif">NS 2.3</a>)</td>
  </tr>
  <tr>
    <td><input type="radio" name="deceptionRadio" id="deceptionRadioNo" value="no" runat="server" disabled="disabled"/>
      <label for="radio"></label></td>
    <td class="style4">No</td>
  </tr>
  <tr>
    <td><input type="radio" name="deceptionRadio" id="deceptionRadioYes" value="yes" runat="server" disabled="disabled"/>
      <label for="radio"></label></td>
    <td class="style4">Yes – <em>please  detail the methods below. Please describe how this method is essential to the  research aims and how participants will be de-briefed after the study.</em></td>
  </tr>
  <tr>
    <td colspan="2"><label for="textfield"></label>
      <textarea type="text" name="deceptionText" id="deceptionText" runat="server" rows="10" cols="130" disabled="disabled"></textarea></td>
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
