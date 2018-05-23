$(document).ready(function () {
    // DemoWithButton();
    AddNewLocale();
    DemoWithButton();
});

// -------------------- LOCALISATION DEMO  ----------------------------
function AddNewLocale(){
    let newLocale = { 
        "zh" : {
            chartTitle: "Chart Title Chinese",
            confirmBtnTxt : "Chinese",
            reloadBtnTxt: "Chinese",
            successTxt: "Chinese",
            errorTxt: "Chinese"
        },
        "fr": {
            chartTitle : "French",
            confirmBtnTxt : "French",
            reloadBtnTxt: "French",
            successTxt: "French",
            errorTxt: "French"
        }
    };

    interactive_captcha.AddNewLocale(newLocale);
    interactive_captcha.SetCurrentLocale("zh", true);
}

// -------------------- DEMO WITHOUT BUTTON  ----------------------------
function DemoWithoutButton(){
    $("#btnSubmit").hide();
    interactive_captcha.InitWithoutButton("test-container");
}

function callbackToClient(data){
    console.log("Check Result Callback");
}

// -------------------- DEMO WITH BUTTON  ----------------------------
function DemoWithButton(){
    btnClickhandler();
    interactive_captcha.Init("test-container", callbackToClient);
}

function CheckResult_Success(data){
    if(data === true){
        interactive_captcha.ShowSuccessMesg();
    }else{
        interactive_captcha.ShowErrorMesg("Please try again!");
    }
}

function CheckResult_Error(data){
    interactive_captcha.ShowErrorMesg("Error in loading captcha");
}

function btnClickhandler(){
    $("#btnSubmit").click(function(){
        interactive_captcha.CheckResultCustom(CheckResult_Success, CheckResult_Error);
    });
}