
$(document).ready(() => {
    $("#checkBtn").click(() => {
        $("#ausgabe").text(checkForAdmin());
    });
    
});

function checkForAdmin() {
    let u = JSON.parse(document.getElementById("user").innerHTML);
    
    if (u.UserName == "admin") {
        return "Alle Berechtigungen erteilt!";
    }
    else {
        return "Sie sind kein Admin!";
    }
}