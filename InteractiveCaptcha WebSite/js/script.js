var angle = 0;

$(document).ready(function () {
    tileClick();
});

function tileClick() {
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

    $("#image4").click(function () {
        angle += 90;
        $("#image4").css("transform", "rotate(" + angle + "deg)");
    })

    $("#image5").click(function () {
        angle += 90;
        $("#image5").css("transform", "rotate(" + angle + "deg)");
    })

    $("#image6").click(function () {
        angle += 90;
        $("#image6").css("transform", "rotate(" + angle + "deg)");
    })

    $("#image7").click(function () {
        angle += 90;
        $("#image7").css("transform", "rotate(" + angle + "deg)");
    })

    $("#image8").click(function () {
        angle += 90;
        $("#image8").css("transform", "rotate(" + angle + "deg)");
    })

    $("#image9").click(function () {
        angle += 90;
        $("#image9").css("transform", "rotate(" + angle + "deg)");
    })
}