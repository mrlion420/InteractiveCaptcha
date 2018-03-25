var angle = 0;

$(document).ready(function () {
    imageClick();
});

function imageClick() {

    //var angle = 0,
    //    img = document.getElementById('container');
    //document.getElementById('image').onclick = function () {
    //    angle = (angle + 90) % 360;
    //    img.className = "rotate" + angle;
    //}
    $("#image1").click(function () {
        angle += 90;
        $("#image1").css("transform", "rotate(" + angle + "deg)");
    })

    $("#image2").click(function () {
        angle += 90;
        $("#image2").css("transform", "rotate(" + angle + "deg)");
    })

    $("#image3").click(function () {
        angle += 90;
        $("#image3").css("transform", "rotate(" + angle + "deg)");
    })
}
