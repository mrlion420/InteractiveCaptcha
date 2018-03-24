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
    $("#image").click(function () {
        angle += 90;
        $("#image").css("transform", "rotate(" + angle + "deg)");
    })
}

