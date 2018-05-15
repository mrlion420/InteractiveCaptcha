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

    var Init = function(renderId){
        click();
        gRenderId = renderId;
        let methodName = "GetCaptcha";
        ajaxGet(methodName, null, GetCaptcha_Success, GetCaptcha_Error);
    };

    function GetCaptcha_Success(data){
        let htmlString = "";
        let count = 0;
        let currentCount = 1;
        gCaptchaId = data[0].CaptchaId;
        $.each(data, function(key, value){
            if(count === 0){
                htmlString += "<div class='table'>";
            }
            htmlString += "<div class='cell'>";
            htmlString += "<img class='bigger imageclick captcha-image' id='captcha-" + currentCount + "' src='" + value.URL + "' data-degree='0'/>";
            htmlString += "</div>";
            count++;
            if(count % 3 === 0){
                htmlString += "</div>";
                count = 0;
            }
            currentCount++;
        });
        $("#" + gRenderId).html(htmlString);
    }

    function GetCaptcha_Error(){
        // ERROR IN GETTING CAPTCHA
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
            $("#captchaSuccess").fadeIn(1000);
            $(".tile").css("opacity", 0.3);
        }else{
            $("#captchaError").fadeIn(1000);
            $(".tile").css("opacity", 0.3);
        }
    }
    
    function CheckResult_Error(data){
        // ERROR IN GETTING RESULT
    }

    function click(){
        $(".tile").on("click", "img", function(){
            let currentAngle = $(this).data("degree");
            currentAngle += 90;
            $(this).data("degree", currentAngle);
            $(this).css("transform", "rotate(" + currentAngle + "deg)");
        });
    
        $("#confirm").click(function () {
            CheckResult();
        });
    
        $("#reload").click(function () {
            window.location.reload();
        });
    }

    return {
        Init : Init,
        CheckResult : CheckResult
    };
    
}());