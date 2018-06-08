$(document).ready(function () {
    $("#confirmBtn").hide();
    DemoWithButton();
    DemoButtonHandler();
});
// -------------------- BUTTON CLICK HANDLER  ----------------------------
function DemoButtonHandler(){
    $("#btnChangeLocale").click(function(){
        AddNewLocale();
    });

    $("#btnChangeConfig").click(function(){
        ChangeConfig();
    });

    $("#btnDefaultConfig").click(function(){
        DefaultConfig();
    });

    $("#btnDefaultLocale").click(function(){
        DefaultLocale();
    });

    $("#btnInitWithBtn").click(function(){
        $("#demo-container").html("");
        $("#confirmBtn").hide();
        $("#lblResult").text("");
        DemoWithButton();
    });

    $("#btnInitWithoutBtn").click(function(){
        $("#demo-container").html("");
        $("#confirmBtn").show();
        $("#lblResult").text("");
        DemoWithoutButton();
    });
}

// -------------------- DEFAULT VALUES  ----------------------------
function DefaultConfig(){
    let data = {
        ColorConfirmBtn : "#0c51f5",
        ColorReloadBtn : "#0c51f5",
        ColorConfirmBtnTxt : "white",
        ColorReloadBtnTxt : "white",
        ColorCaptchaBackground : "white",
        ColorCaptchaTitle : "black",
        ShowCaptchaTitle : true,
        ConfirmBtnBorder : "0px",
        ReloadBtnBorder : "0px",
        OuterBoxShadow : "0px 0px 1px 1px grey"
    };
    interactive_captcha.Config(data);
}

function DefaultLocale(){
    interactive_captcha.SetCurrentLocale("en", true);
}

// -------------------- CONFIG DEMO  ----------------------------

function ChangeConfig(){
    let data = {
        ColorConfirmBtn : "black",
        ColorReloadBtn : "black",
        ColorConfirmBtnTxt : "white",
        ColorReloadBtnTxt : "white",
        ColorConfirmBtnBorder: "white",
        ColorReloadBtnBorder : "white",
        ColorCaptchaBackground : "lightgrey",
        ColorCaptchaTitle : "red",
        ShowCaptchaTitle : true,
        ConfirmBtnBorder : "4px solid",
        ReloadBtnBorder : "4px solid",
        OuterBoxShadow : "2px 2px 2px 2px #E7746F"
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

// -------------------- DEMO WITHOUT DEFAULT BUTTON  ----------------------------
function DemoWithoutButton(){
    btnClickhandler();
    interactive_captcha.InitWithoutButton("test-container");
}

function btnClickhandler(){
    $("#btnSubmit").click(function(){
        interactive_captcha.CheckResultCustom(CheckResult_Success, CheckResult_Error);
    });
}


function CheckResult_Success(data){
    $("#lblResult").text(data);
    if(data === true){
        interactive_captcha.ShowSuccessMesg();
    }else{
        interactive_captcha.ShowErrorMesg("Please try again!");
    }
}

function CheckResult_Error(data){
    $("#lblResult").text(data);
    interactive_captcha.ShowErrorMesg("Error in loading captcha");
}

// -------------------- DEMO WITH DEFAULT BUTTON  ----------------------------
function DemoWithButton(){
    interactive_captcha.Init("test-container", callbackToClient);
}


function callbackToClient(data){
    $("#lblResult").text(data);
}
