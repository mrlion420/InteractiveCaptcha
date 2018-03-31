var angle = 0;

$(document).ready(function () {
    //tileClick();
    click();
});

function getRotationDegrees(imageObj) {
 
    var matrix = imageObj.css("-webkit-transform") ||
        obj.css("-moz-transform") ||
        obj.css("-ms-transform") ||
        obj.css("-o-transform") ||
        obj.css("transform");
    if (matrix !== 'none') {
        var values = matrix.split('(')[1].split(')')[0].split(',');
        var a = values[0];
        var b = values[1];
        var angle = Math.round(Math.atan2(b, a) * (180 / Math.PI));
    } else { var angle = 0; }
        
    return angle;
}

function click(){
    $(".imageclick").click(function () {
        let currentAngle = $(this).data("degree");
        currentAngle += 90;
        $(this).data("degree", currentAngle);
        //angle = (angle + 90) % 360
        $(this).css("transform", "rotate(" + currentAngle + "deg)");
    });
}

function getCaptchaImages() {
    $.ajax({
        type: "GET",
        url: "webservice URL",
        data: "imagename and degress",
        dataType: "json",
        timeout : 5s,
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

function tileClick() {
    $("#image1").click(function () {
        let currentAngle = $(this).data("degree");
        currentAngle += 90;
        $(this).data("degree", currentAngle);
        //angle = (angle + 90) % 360
        $("#image1").css("transform", "rotate(" + currentAngle + "deg)");
    })

    $("#image2").click(function () {
        let currentAngle = $(this).data("degree");
        currentAngle += 90;
        $(this).data("degree", currentAngle);
        //angle = (angle + 90) % 360
        $("#image2").css("transform", "rotate(" + currentAngle + "deg)");
    })

    $("#image3").click(function () {
        let currentAngle = $(this).data("degree");
        currentAngle += 90;
        $(this).data("degree", currentAngle);
        //angle = (angle + 90) % 360
        $("#image3").css("transform", "rotate(" + currentAngle + "deg)");
    })

    $("#image4").click(function () {
        let currentAngle = $(this).data("degree");
        currentAngle += 90;
        $(this).data("degree", currentAngle);
        //angle = (angle + 90) % 360
        $("#image4").css("transform", "rotate(" + currentAngle + "deg)");
    })

    $("#image5").click(function () {
        let currentAngle = $(this).data("degree");
        currentAngle += 90;
        $(this).data("degree", currentAngle);
        //angle = (angle + 90) % 360
        $("#image5").css("transform", "rotate(" + currentAngle + "deg)");
    })

    $("#image6").click(function () {
        let currentAngle = $(this).data("degree");
        currentAngle += 90;
        $(this).data("degree", currentAngle);
        //angle = (angle + 90) % 360
        $("#image6").css("transform", "rotate(" + currentAngle + "deg)");
    })

    $("#image7").click(function () {
        let currentAngle = $(this).data("degree");
        currentAngle += 90;
        $(this).data("degree", currentAngle);
        //angle = (angle + 90) % 360
        $("#image7").css("transform", "rotate(" + currentAngle + "deg)");
    })

    $("#image8").click(function () {
        let currentAngle = $(this).data("degree");
        currentAngle += 90;
        $(this).data("degree", currentAngle);
        //angle = (angle + 90) % 360
        $("#image8").css("transform", "rotate(" + currentAngle + "deg)");
    })

    $("#image9").click(function () {
        let currentAngle = $(this).data("degree");
        currentAngle += 90;
        $(this).data("degree", currentAngle);
        //angle = (angle + 90) % 360
        $("#image9").css("transform", "rotate(" + currentAngle + "deg)");
    })
}