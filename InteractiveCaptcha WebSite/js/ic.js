var interactive_captcha = (function(){
    //  var gWebServiceHost = "http://localhost:55155/CaptchaWCF.svc";
    // var gWebServiceHost = "http://122.11.177.14/IC/CaptchaWCF.svc";
     var gWebServiceHost = "http://fmcc.aquametro.com.sg/ic/CaptchaWCF.svc";
    
    var gLocaleList = {
        "en":{
            chartTitle: "Captcha (Click To Rotate)",
            confirmBtnTxt : "Confirm",
            reloadBtnTxt: "Reload",
            successTxt: "Successful!",
            errorTxt: "Please try again!"
        }
    };

    // Default Language is English
    var gLocale = gLocaleList.en;
    
    var gCaptchaId = 0;
    var gRenderId = "";
    var gCallback = null;

    function ajaxGet(methodName, data, successCallBack, errorCallBack){
        let url = gWebServiceHost + "/" + methodName;
        $.ajax({
            type: 'GET',
            url: url,
            data: data,
            dataType: 'json',
            success: successCallBack,
            error : errorCallBack
        });
    }
    
    function ajaxPost(methodName, data, successCallBack, errorCallBack){
        let url = gWebServiceHost + "/" + methodName;
        $.ajax({
            type: 'POST',
            url: url,
            data: JSON.stringify(data),
            dataType: 'json',
            contentType: "application/json;charset=utf-8",
            success: successCallBack,
            error : errorCallBack
        });
    } 

    function ShowLoadingIcon(){
        LowerOpacityContainer();
        $("#icLoading").show();
    }

    function HideLoadingIcon(){
        RestoreOpacityContainer();
        $("#icLoading").hide();
    }

    function LowerOpacityContainer(){
        $(".ic-tile").css("opacity", 0.3);
    }

    function RestoreOpacityContainer(){
        $(".ic-tile").css("opacity", 1);
    }

    var ShowErrorMesg= function(mesg){
        LowerOpacityContainer();
        $("#icErrorTxt").html(mesg);
        $("#icError").fadeIn(1000);        
    };

    var ShowSuccessMesg = function(){
        LowerOpacityContainer();
        $("#icSuccess").fadeIn(1000);
    };

    function DisableConfirmBtn(){
        $("#icConfirm").attr("disabled", true);
        $("#icConfirm").css("opacity", 0.3);
    }

    function EnableConfirmBtn(){
        $("#icConfirm").attr("disabled", false);
        $("#icConfirm").css("opacity", 1);
    }

    var Init = function(renderId, callback){
        gCallback = callback;
        let htmlString = "";
        gRenderId = renderId;
        // Generate the container first
        htmlString += "<div class='ic-container'>";
        htmlString += "<h2 id='icTitle'>" + gLocale.chartTitle + "</h2>";
        htmlString += "<div class='lds-css ng-scope' id='icLoading'><div class='lds-rolling'><div></div></div></div>";
        htmlString += "<div class='ic-textblock' id='icSuccess'>" + 
                      "<p class='ic-text' id='icSuccessTxt'>" + gLocale.successTxt + "</p></div>";
        htmlString += "<div class='ic-textblock' id='icError'>" + 
                      "<p class='ic-text ic-error' id='icErrorTxt'>" + gLocale.errorTxt + "</p></div>";
        htmlString += "<div class='ic-tile' id='icTile'>";
        htmlString += "</div>";
        htmlString += "<button class='ic-button' id='icConfirm'>" + gLocale.confirmBtnTxt + "</button>";
        htmlString += "<button class='ic-button' id='icReload'>" + gLocale.reloadBtnTxt + "</button>";
        htmlString += "</div>";
        $("#" + gRenderId).html(htmlString);
        ShowLoadingIcon();
        btnClick();
        let methodName = "GetCaptcha";
        try{
            ajaxGet(methodName, null, GetCaptcha_Success, GetCaptcha_Error);
        }catch(ex){
            console.log(ex);
            ShowErrorMesg("An error has occurred");
            HideLoadingIcon();
        }
        
    };

    var InitWithoutButton = function(renderId){
        gInitWithoutBtn = true;
        gRenderId = renderId;
        let htmlString = "";
        // Generate the container first
        htmlString += "<div class='ic-container'>";
        htmlString += "<h2 id='icTitle'>" + gLocale.chartTitle + "</h2>";
        htmlString += "<div class='lds-css ng-scope' id='icLoading'><div class='lds-rolling'><div></div></div></div>";
        htmlString += "<div class='ic-textblock' id='icSuccess'>" + 
                      "<p class='ic-text' id='icSuccessTxt'>" + gLocale.successTxt + "</p></div>";
        htmlString += "<div class='ic-textblock' id='icError'>" + 
                      "<p class='ic-text ic-error' id='icErrorTxt'>" + gLocale.errorTxt + "</p></div>";
        htmlString += "<div class='ic-tile' id='icTile'>";
        htmlString += "</div>";
        htmlString += "<button class='ic-button' id='icReload'>" + gLocale.reloadBtnTxt + "</button>";
        htmlString += "</div>";
        $("#" + gRenderId).html(htmlString);
        ShowLoadingIcon();
        btnClick();
        let methodName = "GetCaptcha";
        try{
            ajaxGet(methodName, null, GetCaptcha_Success, GetCaptcha_Error);
        }catch(ex){
            ShowErrorMesg("An error has occurred");
        }
    };

    var AddNewLocale = function(languageObj){
        $.each(languageObj, function(key,value){
            gLocaleList[key] = value;
        });
    };

    var SetCurrentLocale = function(locale, reloadText){
        gLocale = gLocaleList[locale];
        if(reloadText){
            $("#icTitle").html(gLocale.chartTitle);
            $("#icSuccessTxt").html(gLocale.successTxt);
            $("#icErrorTxt").html(gLocale.errorTxt);
            $("#icConfirm").html(gLocale.confirmBtnTxt);
            $("#icReload").html(gLocale.reloadBtnTxt);
        }
    };

    function GetCaptcha_Success(data){
        EnableConfirmBtn();
        let htmlString = "";
        let count = 0;
        let currentCount = 1;
        gCaptchaId = data[0].CaptchaId;
        
        $.each(data, function(key, value){
            if(count === 0){
                htmlString += "<div class='ic-table'>";
            }
            htmlString += "<div class='ic-cell'>";
            htmlString += "<img class='ic-bigOnHover imageclick captcha-image' id='captcha-" + currentCount + "' src='data:image/png;base64," + value.Base64String + "' data-degree='0'/>";
            htmlString += "</div>";
            count++;
            if(count % 3 === 0){
                htmlString += "</div>";
                count = 0;
            }
            currentCount++;
        });
        
        $("#icTile").html(htmlString);
        HideLoadingIcon();
    }

    function GetCaptcha_Error(){
        // ERROR IN GETTING CAPTCHA
        HideLoadingIcon();
        ShowErrorMesg("Cannot connect to captcha server");
        DisableConfirmBtn();
    }

    function HideMesg(){
        $("#icSuccess").hide();
        $("#icError").hide();
    }

    var CheckResultCustom = function(successCallBack, errorCallBack){
        let captchaImages = document.getElementsByClassName('captcha-image');
        let dataString = "";
        for(let i = 0; i < captchaImages.length; i++){
            let image = captchaImages[i];
            let imageId = image.id;
            let currentAngle = $("#" + imageId).data("degree");
            dataString += imageId + "=" + currentAngle + ";";
        }
        let data = { captchaId : gCaptchaId , dataString : dataString };
        let methodName = "CheckResult";
        ajaxPost(methodName, data, successCallBack, errorCallBack);
    };

    var CheckResultDefault = function(){
        let captchaImages = document.getElementsByClassName('captcha-image');
        let dataString = "";
        for(let i = 0; i < captchaImages.length; i++){
            let image = captchaImages[i];
            let imageId = image.id;
            let currentAngle = $("#" + imageId).data("degree");
            dataString += imageId + "=" + currentAngle + ";";
        }
        let data = { captchaId : gCaptchaId , dataString : dataString };
        let methodName = "CheckResult";
        ajaxPost(methodName, data, CheckResult_Success, CheckResult_Error);
    };

    function CheckResult_Success(data){
        HideLoadingIcon();
        if(data === true){
            ShowSuccessMesg();
        }else{
            $("#icError").fadeIn(1000);
            LowerOpacityContainer();
        }
        if(typeof gCallback === "function"){
            gCallback(data);
        }        
    }
    
    function CheckResult_Error(data){
        // ERROR IN GETTING RESULT
        HideLoadingIcon();
        ShowErrorMesg("Error in verifying captcha");
        DisableConfirmBtn();
    }    

    function btnClick(){
        $(".ic-tile").on("click", "img", function(){
            let currentAngle = $(this).data("degree");
            currentAngle += 90;
            $(this).data("degree", currentAngle);
            $(this).css("transform", "rotate(" + currentAngle + "deg)");
        });
    
        $("#icConfirm").click(function () {
            ShowLoadingIcon();
            DisableConfirmBtn();
            CheckResultDefault();
        });
    
        $("#icReload").click(function () {
            Reload();
        });
    }

    var Reload = function(){
        $("#icTile").html("");
        HideMesg();
        ShowLoadingIcon();
        let methodName = "GetCaptcha";
        ajaxGet(methodName, null, GetCaptcha_Success, GetCaptcha_Error);
    };

    var Config = function(data){
        $.each(data, function(key,value){
            switch(key){
                case "ColorConfirmBtn" : 
                $("#icConfirm").css("background-color", value);
                break;

                case "ColorReloadBtn":
                $("#icReload").css("background-color", value);
                break;

                case "ColorConfirmBtnTxt":
                $("#icConfirm").css("color", value);
                break;

                case "ColorReloadBtnTxt":
                $("#icReload").css("color", value);
                break;

                case "ColorConfirmBtnBorder":
                $("#icConfirm").css("border-color", value);
                break;

                case "ColorReloadBtnBorder":
                $("#icReload").css("border-color", value);
                break;

                case "ColorCaptchaBackground":
                $(".ic-container").css("background-color", value);
                break;

                case "ColorCaptchaTitle":
                $("#icTitle").css("color", value);
                break;

                case "ShowCaptchaTitle":
                if(value){
                    $("#icTitle").css("display", "block");
                }else{
                    $("#icTitle").css("display", "none");
                }
                break;

                case "ConfirmBtnBorder":
                $("#icConfirm").css("border", value);
                break;

                case "ReloadBtnBorder":
                $("#icReload").css("border", value);
                break;

                case "OuterBoxShadow":
                $(".ic-container").css("box-shadow", value);
                break;
            }
        });
    };

    return {
        Init : Init,
        InitWithoutButton : InitWithoutButton,
        CheckResultCustom : CheckResultCustom,
        ShowErrorMesg : ShowErrorMesg,
        ShowSuccessMesg : ShowSuccessMesg,
        Reload : Reload,
        Config : Config,
        SetCurrentLocale : SetCurrentLocale,
        AddNewLocale : AddNewLocale,
        gCaptchaId : gCaptchaId
    };
    
}());