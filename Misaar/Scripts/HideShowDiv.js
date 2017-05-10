var buttonClose = document.getElementById("send-phone-xbutton");
buttonClose.onclick = hideDiv;

function hideDiv() {
    var targetDiv = document.getElementById("send-phone-form");
    targetDiv.style.opacity = 0;
    targetDiv.style.zIndex = -1;
}

var buttonShow = document.getElementById("phoneButton");
buttonShow.onclick = showDiv;

function showDiv() {
    var targetDiv = document.getElementById("send-phone-form");
    targetDiv.style.opacity = 0.9;
    targetDiv.style.zIndex = 3;
}