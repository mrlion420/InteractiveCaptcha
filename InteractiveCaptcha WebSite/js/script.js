var angle = 0;

$(document).ready(function () {
    click();
});

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
    $(".imageclick").click(function () {
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

    
//function getCaptchaImages() {
//    $.ajax({
//        type: "GET",
//        url: "webservice URL",
//        data: "imagename and degress",
//        dataType: "json",
//        timeout: 5,
//        success: function (response) {
//            if (response === "false" || response === undefined || response === null) {
//                // ERROR 

//            } else {
//                // NO ERROR
//                let urlOne = response[0]; // Image url
//                let urlTwo = response[1]; // image url
//            }
            
//        },
//        error: function () {
//            // ERROR
//        }
//    });
//}
