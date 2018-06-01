$(document).ready(function () {
    DemoWithButton();
    // AddNewLocale();
    // DemoWithButton();
    // ChangeConfig();
});

// -------------------- CONFIG DEMO  ----------------------------

function ChangeConfig(){
    let data = {
        ColorConfirmBtn : "black",
        ColorReloadBtn : "black",
        ColorConfirmBtnTxt : "red",
        ColorReloadBtnTxt : "red",
        ColorConfirmBtnBorder: "yellow",
        ColorReloadBtnBorder : "yellow",
        ColorCaptchaBackground : "lightgrey",
        ColorCaptchaTitle : "red",
        ShowCaptchaTitle : false,
        ConfirmBtnBorder : "4px solid yellow",
        ReloadBtnBorder : "4px solid",
        OuterBoxShadow : "2px 2px 2px 2px blue"
    };
    interactive_captcha.Config(data);
}

// -------------------- LOCALISATION DEMO  ----------------------------
function AddNewLocale(){
    let newLocale = { 
        "zh" : {
            chartTitle: "请点击旋转图片",
            confirmBtnTxt: "确定",
            reloadBtnTxt: "刷新",
            successTxt: "验证成功！",
            errorTxt: "验证错误，请重试！"
        }
    };

    interactive_captcha.AddNewLocale(newLocale);
    interactive_captcha.SetCurrentLocale("zh", true);
}

// -------------------- DEMO WITHOUT BUTTON  ----------------------------
function DemoWithoutButton(){
    btnClickhandler();
    $("#btnSubmit").hide();
    interactive_captcha.InitWithoutButton("test-container");
}

function btnClickhandler(){
    $("#btnSubmit").click(function(){
        interactive_captcha.CheckResultCustom(CheckResult_Success, CheckResult_Error);
    });
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

// -------------------- DEMO WITH BUTTON  ----------------------------
function DemoWithButton(){
    interactive_captcha.Init("test-container", callbackToClient);
}


function callbackToClient(data){
    console.log("Check Result Callback");
}
