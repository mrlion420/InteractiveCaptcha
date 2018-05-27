////TEST Init ---------------------------

//$(document).ready(function () {
//    test_Init();
//});

//function test_Init() {
//    interactive_captcha.Init("testContainer", callback_One);
//}

//function callback_One(data) {

//}


////TEST InitWithoutButton ------------------------

//$(document).ready(function () {
//    test_InitWithoutButton();
//});

//function test_InitWithoutButton() {
//    interactive_captcha.InitWithoutButton("testContainer");
//}


////TEST CheckResultCustom ----------------------

//$(document).ready(function () {
//    test_InitWithoutButton();
//    btnClickhandler();
//});

//function test_InitWithoutButton() {
//    interactive_captcha.InitWithoutButton("testContainer");
//}

//function btnClickhandler() {
//    $("#btnSubmit").click(function () {
//        interactive_captcha.CheckResultCustom(success_response, error_response);
//    });
//}

//function success_response(response) {
//    alert(response);
//}

//function error_response(response) {
//    alert(response);
//}


////TEST ShowErrorMesg  ------------------

//$(document).ready(function () {
//    test_Init();
//    test_ShowErrorMesg();
//});

//function test_Init() {
//    interactive_captcha.Init("testContainer", callback_One);
//}

//function test_ShowErrorMesg() {
//    interactive_captcha.ShowErrorMesg("Please try again!");
//}

//function callback_One(data) {

//}


//TEST ShowSuccessMesg -------------

$(document).ready(function () {
    test_Init();
    test_ShowSuccessMesg();
});

function test_Init() {
    interactive_captcha.Init("testContainer", callback_One);
}

function callback_One(data) {

}

function test_ShowSuccessMesg() {
    interactive_captcha.ShowSuccessMesg("Successful!");
}


//////TEST Reload --------------------------------

//$(document).ready(function () {
//    test_Init();
//    btnClickhandler();
//});

//function test_Init() {
//    interactive_captcha.Init("testContainer", callback_One);  
//}

//function callback_One(data) {

//}

//function btnClickhandler() {
//    $("#btnSubmit").click(function () {
//        interactive_captcha.Reload();
//    });
//}

//function reload_response(response) {
//    alert(response);
//}




// =====================

//interactive_captcha.SetCallback(callback_Two);

//function callback_Two(data) {

//}