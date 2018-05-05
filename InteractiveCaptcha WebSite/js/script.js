var webServiceHost = "http://122.11.177.14:9999/CaptchaWCF.svc";

$(document).ready(function () {
    GetCaptchaImages();
    click();
});

function ajaxGet(methodName, data, successCallBack, errorCallBack){
	let url = webServiceHost + "/" + methodName + "?callback=?";
	return $.ajax({
		type: 'GET',
		url: url,
		data: data,
		dataType: 'jsonp',
        success: successCallBack,
        error : errorCallBack
	});
}

function GetCaptchaImages() {
    let methodName = "GetCaptcha";
    ajaxGet(methodName, null, GetCaptcha_Success, GetCaptcha_Error);
}

function GetCaptcha_Success(data){
    let htmlString = "";
    let count = 0;
    let currentCount = 1;
    $.each(data, function(key, value){
        if(count === 0){
            htmlString += "<div class='table'>";
        }
        htmlString += "<div class='cell'>";
        htmlString += "<img class='bigger imageclick' id='image-" + currentCount + "' src='" + value.URL + "' data-degree='0'/>";
        htmlString += "</div>";
        count++;
        if(count % 3 === 0){
            htmlString += "</div>";
            count = 0;
        }
        currentCount++;
    });

    // for(let i = 1; i <= data.length; i++){
    //     if(i === 1){
    //         htmlString += "<div class='table'>";
    //     } else if ( i % 3 === 0){
    //         htmlString += "</div>";
    //         htmlString += "<div class='table'>";
    //     }
    //     htmlString += "<div class='cell'>";
    //     htmlString += "<img class='bigger imageclick' id='image-" + i + "' src='" + data[i- 1].URL + "' data-degree='0'/>";
    //     htmlString += "</div>";
    //     if(i % 3 === 0){
    //         htmlString += "</div>";
    //     }
    // }
    $("#tile").html(htmlString);
}

function GetCaptcha_Error(){
    // ERROR IN GETTING CAPTCHA
}

//function getRotationDegrees(imageObj) {
 
//    var matrix = imageObj.css("-webkit-transform") ||
//        obj.css("-moz-transform") ||
//        obj.css("-ms-transform") ||
//        obj.css("-o-transform") ||
//        obj.css("transform");
//    if (matrix !== 'none') {
//        var values = matrix.split('(')[1].split(')')[0].split(',');
//        var a = values[0];
//        var b = values[1];
//        var angle = Math.round(Math.atan2(b, a) * (180 / Math.PI));
//    } else { var angle = 0; }
        
//    return angle;
//}

function click(){
    $(".tile").on("click", "img", function(){
        let currentAngle = $(this).data("degree");
        currentAngle += 90;
        $(this).data("degree", currentAngle);
        $(this).css("transform", "rotate(" + currentAngle + "deg)");
    });

    $("#confirm").click(function () {
        $("#success").fadeIn(1000);
        $(".tile").css("opacity", 0.3);
    });

    $("#confirmError").click(function () {
        $("#error").fadeIn(1000);
        $(".tile").css("opacity", 0.3);
    })

    $("#reload").click(function () {
        window.location.reload();
    });


}

    
function getCaptchaImages() {
    $.ajax({
        type: "GET",
        url: "webservice URL",
        data: "imagename and degress",
        dataType: "json",
        timeout: 5,
        success: function (response) {
            if (response === "false" || response === undefined || response === null) {
                // ERROR 

            } else {
                // NO ERROR
                let urlOne = response[0]; // Image url
                let urlTwo = response[1]; // image url
            }
            
        },
        error: function () {
            // ERROR
        }
    });
}
