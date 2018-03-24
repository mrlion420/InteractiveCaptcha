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

//creates a 3 by 3 boxes. To see the outline uncomment ".box" and ".row" in css.
function makeBlocks() {
    for (var i = 0; i < 3; i++) {
        var row = document.createElement('div');
        row.className = "row";

        for (var j = 0; j < 3; j++) {
            var box = document.createElement('div');
            box.className = "box"+i+j;
            row.appendChild(box);
            box.style.borderColor = "black";
            box.style.borderWidth = "1px";
            box.style.borderStyle = "solid";


            box.style.width = "100px";
            box.style.height = "100px";
            box.style.margin = "0";
            box.style.marginTop = "0";
            box.style.cursor = "pointer";
            box.style.display = "inline-block";
            box.style.float = "left";
        }
        document.getElementById('iCap').appendChild(row);
        row.style.display = "block";
        row.style.float = "left";
        row.style.width = "100%";

    }
}

//Contains functions when page loads
window.onload = function () {
    
    makeBlocks();
   

    
};

