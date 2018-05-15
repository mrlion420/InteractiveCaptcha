$(document).ready(function () {
    interactive_captcha.InitWithoutButton("here");
    // $("#icConfirm").click(function () {
    //     interactive_captcha.CheckResultCustom(CheckResult_Success, CheckResult_Error);
    // });
    interactive_captcha.setCallback(test);
});

function CheckResult_Success(data){
    if(data === true){
        interactive_captcha.ShowSuccessMesg();
    }else{
        interactive_captcha.ShowErrorMesg("Wrong Captcha. Please click reload button");
        // interactive_captcha.Reload();
    }
}

function CheckResult_Error(data){

}

function test(data){
    
}