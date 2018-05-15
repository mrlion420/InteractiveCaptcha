var interactive_captcha = (function(){
    var gWebServiceHost = "http://localhost:55155/CaptchaWCF.svc";
    var gCaptchaId = 0;
    var gRenderId = "";

    function ajaxGet(methodName, data, successCallBack, errorCallBack){
        let url = gWebServiceHost + "/" + methodName;
        return $.ajax({
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
        return $.ajax({
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
        $("#icLoading").show();
    }

    function HideLoadingIcon(){
        $("#icLoading").hide();
    }

    var Init = function(renderId){
        let htmlString = "";
        btnClick();
        gRenderId = renderId;
        // Generate the container first
        htmlString += "<div class='ic-container'>";
        htmlString += "<div class='lds-css ng-scope' id='icLoading'><div class='lds-rolling'><div></div></div></div>";
        htmlString += "<div class='ic-textblock' id='icSuccess'>" + 
                      "<p> class='ic-text'>Successful!</p></div>";
        htmlString += "<div class='ic-textblock' id='icError'>" + 
                      "<p class='ic-text ic-error'>Please Try Again!</p></div>";
        htmlString += "<div class='ic-tile' id='icTile'>";
        htmlString += "</div>";
        htmlString += "<button class='ic-button' id='icConfirm'>Confirm</button>";
        htmlString += "<button class='ic-button' id='icReload'>↻</button>";
        htmlString += "</div>";
        $("#" + gRenderId).html(htmlString);
        ShowLoadingIcon();
        let methodName = "GetCaptcha";

        try{
            ajaxGet(methodName, null, GetCaptcha_Success, GetCaptcha_Error);
        }catch(ex){
            $("#captchaError").fadeIn(1000);
        }
        
    };

    function GetCaptcha_Success(data){
        let htmlString = "";
        let count = 0;
        let currentCount = 1;
        gCaptchaId = data[0].CaptchaId;
        
        $.each(data, function(key, value){
            if(count === 0){
                htmlString += "<div class='ic-table'>";
            }
            htmlString += "<div class='ic-cell'>";
            htmlString += "<img class='ic-bigOnHover imageclick captcha-image' id='captcha-" + currentCount + "' src='" + value.URL + "' data-degree='0'/>";
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
        $("#icError").fadeIn(1000);
    }

    var CheckResult = function(){
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
        if(data === true){
            $("#interactiveCaptchaSuccess").fadeIn(1000);
            $(".tile").css("opacity", 0.3);
        }else{
            $("#captchaError").fadeIn(1000);
            $(".tile").css("opacity", 0.3);
        }
    }
    
    function CheckResult_Error(data){
        // ERROR IN GETTING RESULT
    }

    function btnClick(){
        $(".tile").on("click", "img", function(){
            let currentAngle = $(this).data("degree");
            currentAngle += 90;
            $(this).data("degree", currentAngle);
            $(this).css("transform", "rotate(" + currentAngle + "deg)");
        });
    
        $("#confirm").click(function () {
            CheckResult();
        });
    
        $("#icReload").click(function () {
            Init(gRenderId);
        });
    }

    return {
        Init : Init,
        CheckResult : CheckResult
    };
    
}());