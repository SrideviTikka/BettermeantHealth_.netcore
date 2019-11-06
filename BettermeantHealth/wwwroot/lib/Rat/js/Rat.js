// JavaScript Document

/* Global Variables */

var sections = [];
var currentSectionId = 0;
var currentQuestionId = 0;
var sectionDataSource = null;

var sectionCriteria = [];
var sectionCorrective = [];

var lockedFormQuestionsData = [];
var lockedFormSectionsData = [];
var lockedFormCriteriaData = [];
var totalQuestions = 0;
var currentAnswerQuestions = 1;
var pmCommentsIds = [];
var sponsorCommentsIds = [];
var json123 = '[{"AssesmentId":1,"AssesmentName":"Assessname","Header":[{"questionId":"1","sno":"0","questionText":"Header QD","answerText":"QD1 | Values","controlType":"QD","isMandatory":true},{"questionId":"2","sno":"1","questionText":"Header QT1","answerText":"QT11111","controlType":"QT","isMandatory":true},{"questionId":"3","sno":"2","questionText":"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.<br />","answerText":"","controlType":"QN","isMandatory":""}],"Sections":[{"sectionId":4,"sectionName":"Section-1 ","sectionData":[{"questionId":"7","sno":"1","questionText":"QD-sec1&nbsp;<span>&nbsp;</span><span dotted;=fasff= safasfasfas=style=border-bottom:1px; title=fsafsaf>dasffa fasfasf</span><span>&nbsp;</span>","answerText":"D-sec1","controlType":"QD","riskWeight":"11","isMandatory":true},{"questionId":"8","sno":"2","questionText":"QT-Sec1","answerText":"T-Sec1","controlType":"QT","riskWeight":"","isMandatory":false},{"questionId":"9","sno":"","questionText":"<a href=javascript:void(0) id=idAddQuestionTbl_Row_3 style=display: none;><i class=fa fa-plus-circle=fa-lg= iconspace=></i>Add Row</a><br/><table border=1 class=table id=idAddQuestionTbl_3><thead><tr><td><b><input type=text value=xvzxzxv style=width: 250px;></b></td><td><b><input type=text value=Column2 style=width: 250px;></b></td><td><b><input type=text value=Column3 style=width: 250px;></b></td><td><b><input type=text value=Column4 style=width: 250px;></b></td></tr></thead><tbody><tr><td><input id=tblQuestionInput type=text value=Col1 data= style=width: 250px;></td><td><input id=tblQuestionInput type=text value=222 style=width: 250px;></td><td><input id=tblQuestionInput type=text value=33 style=width: 250px;></td><td><input id=tblQuestionInput type=text value=4444444 style=width: 250px;></td></tr></tbody></table>","answerText":"","controlType":"TBL","riskWeight":"","isMandatory":""},{"questionId":"10","sno":"4","questionText":"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.","answerText":"","controlType":"QN","riskWeight":"","isMandatory":""}],"sectionCriteria":[{"riskScore":"0","criteriaList":["cri1","cri2"],"correctiveList":["corr1","corr2"]},{"riskScore":"1","criteriaList":[],"correctiveList":[]},{"riskScore":"2","criteriaList":[],"correctiveList":[]},{"riskScore":"3","criteriaList":[],"correctiveList":[]},{"riskScore":"4","criteriaList":[],"correctiveList":[]},{"riskScore":"5","criteriaList":[],"correctiveList":[]}]},{"sectionId":5,"sectionName":"Section-2 ","sectionData":[{"questionId":"11","sno":"11","questionText":"Qt-Sec2","answerText":"T-Sec2","controlType":"QT","riskWeight":"222","isMandatory":true},{"questionId":"12","sno":"22","questionText":"QD-sec2","answerText":"D-2","controlType":"QD","riskWeight":"","isMandatory":false}],"sectionCriteria":[{"riskScore":"0","criteriaList":[],"correctiveList":[]},{"riskScore":"1","criteriaList":[],"correctiveList":[]},{"riskScore":"2","criteriaList":[],"correctiveList":[]},{"riskScore":"3","criteriaList":[],"correctiveList":[]},{"riskScore":"4","criteriaList":[],"correctiveList":[]},{"riskScore":"5","criteriaList":[],"correctiveList":[]}]},{"sectionId":6,"sectionName":"Section-3 ","sectionData":[{"questionId":"13","sno":"33","questionText":"QD-Sec3","answerText":"D-Sec3 | D-Sec3N","controlType":"QD","riskWeight":"33","isMandatory":true},{"questionId":"14","sno":"3b","questionText":"QT-Sec3","answerText":"T-Sec3","controlType":"QT","riskWeight":"","isMandatory":false}],"sectionCriteria":[{"riskScore":"0","criteriaList":[],"correctiveList":[]},{"riskScore":"1","criteriaList":[],"correctiveList":[]},{"riskScore":"2","criteriaList":[],"correctiveList":[]},{"riskScore":"3","criteriaList":[],"correctiveList":[]},{"riskScore":"4","criteriaList":[],"correctiveList":[]},{"riskScore":"5","criteriaList":[],"correctiveList":[]}]}],"Footer":[{"questionId":"4","sno":"0","questionText":"Footer QD","answerText":"QD1 | Values","controlType":"QD","isMandatory":true},{"questionId":"5","sno":"1","questionText":"Footer QT1","answerText":"QT11111","controlType":"QT","isMandatory":false},{"questionId":"6","sno":"2","questionText":"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.<br />","answerText":"","controlType":"QN","isMandatory":""}]}]';

function GetParameterValue(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}


jQuery(function ($) {
    function tog(v) { return v ? 'addClass' : 'removeClass'; }
    $(document).on('input', '.clearable', function () {
        $(this)[tog(this.value)]('x');
    }).on('mousemove', '.x', function (e) {
        $(this)[tog(this.offsetWidth - 18 < e.clientX - this.getBoundingClientRect().left)]('onX');
    }).on('touchstart click', '.onX', function (ev) {
        ev.preventDefault();
        $(this).removeClass('x onX').val('').change();
        $("#userfilter").keyup();
        $("#docfilter").keyup();
        $("#regionfilter").keyup();
        $("#clientfilter").keyup();
        $("#assessmentfilter").keyup();
    });
});

function showErrorMessage(msg) {
    $('#spanErrorMessage').text(msg);
    $("#divErrorMessage").show();
    $('#divErrorMessage').delay(3000).fadeOut('slow', function () { $('#spanErrorMessage').text(""); });
}
function showSuccessMessage(msg) {
    $('#spanSuccesMessage').text(msg);
    $("#divSuccesMessage").show();
    $('#divSuccesMessage').delay(3000).fadeOut('slow', function () { $('#spanSuccesMessage').text(""); });
}
function escapeEntities(string) {
    var str = string;
    str = str.replace(/\&/g, "&amp;");
    str = str.replace(/\>/g, "&gt;");
    str = str.replace(/\</g, "&lt;");
    str = str.replace(/\"/g, "&quot;");
    str = str.replace(/\'/g, "&apos;");
    return str;
}
function escapeXML(string) {
    var str = string;
    str = str.replace(/\&/g, "&amp;");
    //str = str.replace(/\>/g, "&gt;");
    //str = str.replace(/\</g, "&lt;");
    str = str.replace(/\"/g, "&quot;");
    str = str.replace(/\'/g, "&apos;");
    return str;
}