
$(document).ready(function () {
    var x = 0;
    var s = "";
    console.log("What's up!?!");

    var cat = $("#cat");
    cat.hide();
    var button = $("#buyButton");

    button.on("click", function () {
        console.log("Buying item");
    });

    var prodInfo = $(".prod li");
    prodInfo.on("click", function () {
        console.log("U clicked " + $(this).text());
    });

    var $loginToggle = $("#loginToggle");
    var $popupForm = $(".popup-form");

    $loginToggle.on("click", function () {
        $popupForm.fadeToggle(1000);
    });
});