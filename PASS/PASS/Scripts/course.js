$("#add").click(function () {
    $("#info").toggle();
    $("#assign").toggle();
    $("#add").hide();
    $("#send").css("display", "inline-block");
    $("#send").show();
});

$("#send").click(function () {
    $("#info").toggle();
    $("#assign").toggle();
    $("#add").show();
    $("#send").hide();
});

$("#icon_info").click(function () {
    $("#hw").toggle();
    $("#score").toggle();
});