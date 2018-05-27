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

//$(document).ready(function () {
//    test_Init();
//    test_ShowSuccessMesg();
//});

//function test_Init() {
//    interactive_captcha.Init("testContainer", callback_One);
//}

//function callback_One(data) {

//}

//function test_ShowSuccessMesg() {
//    interactive_captcha.ShowSuccessMesg("Successful!");
//}


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


//TEST Config 1

//$(document).ready(function () {
//    test_Init();
//    test_Config1();
//});

//function test_Init() {
//    interactive_captcha.Init("testContainer", callback_One);
//}

//function callback_One(data) {

//}

//function test_Config1() {
//    let data = {
//        ColorConfirmBtn: "black",
//        ColorReloadBtn: "black",
//        ColorConfirmBtnTxt: "red",
//        ColorReloadBtnTxt: "red",
//        ColorConfirmBtnBorder: "yellow",
//        ColorReloadBtnBorder: "yellow",
//        ColorCaptchaBackground: "lightgrey",
//        ColorCaptchaTitle: "red",
//        ShowCaptchaTitle: false,
//        ConfirmBtnBorder: "4px solid yellow",
//        ReloadBtnBorder: "4px solid",
//        OuterBoxShadow: "2px 2px 2px 2px blue"
//    };
//    interactive_captcha.Config(data);
//}

//TEST Config 2

//$(document).ready(function () {
//    test_Init();
//    test_Config2();
//});

//function test_Init() {
//    interactive_captcha.Init("testContainer", callback_One);
//}

//function callback_One(data) {

//}

//function test_Config2() {
//    let data = {
//        ColorConfirmBtn: "green",
//        ColorReloadBtn: "green",
//        ColorConfirmBtnTxt: "white",
//        ColorReloadBtnTxt: "white",
//        ColorConfirmBtnBorder: "red",
//        ColorReloadBtnBorder: "red",
//        ColorCaptchaBackground: "white",
//        ColorCaptchaTitle: "black",
//        ShowCaptchaTitle: false,
//        ConfirmBtnBorder: "2px solid white",
//        ReloadBtnBorder: "2px solid white",
//        OuterBoxShadow: "2px 2px 2px 2px white"
//    };
//    interactive_captcha.Config(data);
//}


//TEST setcurrentlocale and addnewlocale

//$(document).ready(function () {
//    test_init();
//    test_addnewlocale();
//});

//function test_init() {
//    interactive_captcha.Init("testContainer", callback_one);
//}

//function callback_one(data) {

//}

//function test_addnewlocale() {
//    let newlocale = {
//        "zh": {
//            chartTitle: "请点击旋转图片",
//            confirmBtnTxt: "确定",
//            reloadBtnTxt: "刷新",
//            successTxt: "验证成功！",
//            errorTxt: "验证错误，请重试！"
//        }

//    };

//    interactive_captcha.AddNewLocale(newlocale);
//    interactive_captcha.SetCurrentLocale("zh", true);
//}