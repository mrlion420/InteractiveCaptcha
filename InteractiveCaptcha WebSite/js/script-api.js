$(document).ready(function () {
    // DemoWithButton();
    DemoWithButton();
});

// -------------------- DEMO WITHOUT BUTTON  ----------------------------
function DemoWithoutButton(){
    $("#btnSubmit").hide();
    interactive_captcha.InitWithoutButton("test-container");
}

function callbackToClient(data){
    alert("Callback to client");
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