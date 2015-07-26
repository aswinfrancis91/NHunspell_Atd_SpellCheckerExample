function CheckSpelling(language) {
    debugger;
    $.ajax({
        contentType: 'application/json',
        type: 'POST',
        data: JSON.stringify({ 'synopsis': $("#txtEnglishSynopsis").val(), 'language': language }),
        url: '/Home/CheckSpelling',
        success: function (result) {
            alert(result);
        },
        error: function (error, errorstatus, er) { }
    });
}

//The below methods are the same which we use in TR now.
function CheckEnglishSpelling() {
    debugger;
    AtD.rpc_css_lang = 'en';
    AtD.checkTextAreaCrossAJAX('txtEnglishSynopsis', 'checkEnglishLink', '<img src="../../Images/Spell_Check.png">');
}

function CheckSpanishSpelling() {
    debugger;
    AtD.rpc_css_lang = 'es';
    AtD.checkTextAreaCrossAJAX('txtSpanishSynopsis', 'checkSpanishLink', '<img src="../../Images/Spell_Check.png">');
}

function setCounterValue(id, name) {
    $("#lblCounter").html(name + " : " + document.getElementById(id).value.length);
}