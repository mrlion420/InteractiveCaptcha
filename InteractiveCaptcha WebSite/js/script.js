function getCaptchaImages(){
    // Get image
    let data = [];
    let htmlString ="";
    for(let i = 0; i < 9; i++){
        htmlString += "<div id='tile" + i + "'>";
        htmlString += "<img src='" + data[i] +"' class='bigger'/>";
        htmlString += "</div>";
    }

    $("#container").html(htmlString);
}