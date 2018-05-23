var angle = 0;

$(document).ready(function () {
    prepareImgForClick();
});

function imageClick(img) {

    //var angle = 0,
    //    img = document.getElementById('container');
    //document.getElementById('image').onclick = function () {
    //    angle = (angle + 90) % 360;
    //    img.className = "rotate" + angle;
    //}
    $("#" + img).click(function () {
        angle += 90;
        if (angle > 270) {
            //ensures that degree stays as 0, 90, 180 and 270.
            angle = 0;
        }
        $("#" + img).css("transform", "rotate(" + angle + "deg)");

    })
}

//creates a 3 by 3 boxes. To see the outline uncomment ".box" and ".row" in css.
function makeBlocks() {
    for (var i = 0; i < 3; i++) {
        var row = document.createElement('div');
        row.className = "row";

        for (var j = 0; j < 3; j++) {
            var box = document.createElement('div');
            box.className = "box" + i + j;
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

            //create image element dynamically with minor animations
            var img = document.createElement('img');
            img.src = '/images/smiley.png';
            img.id = 'box' + i + j;
            img.onmouseover = function () { this.style.width = "60px"; };
            img.onmouseout = function () { this.style.width = "50px"; };
            box.appendChild(img);
            //end of image element creation
        }
        document.getElementById('iCap').appendChild(row);
        row.style.display = "block";
        row.style.float = "left";
        row.style.width = "100%";

    }
}


function prepareImgForClick() {
    imageClick("image");
    imageClick("box00");
    imageClick("box01");
    imageClick("box02");
    imageClick("box10");
    imageClick("box11");
    imageClick("box12");
    imageClick("box20");
    imageClick("box21");
    imageClick("box22");
}

function ranGenDegree() {
 
    var numDeg = ["0", "90", "180", "270"];
    return numDeg[Math.floor(Math.random() * 3) + 0];

}


//Contains functions when page loads
window.onload = function () {

    makeBlocks();

};

