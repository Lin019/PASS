﻿$(".add").click(function () {
    $(".info").last().toggle();
    $(".assign").last().toggle();
    $(".add").last().hide();
    $(".create-send").last().css("display", "inline-block");
    $(".create-send").last().show();
});

$(".create-send").click(function () {
    $("div.info").toggle();
    $(".assign").toggle();
    $(".add").show();
    $(".create-send").hide();
});

$(document).on('click', '.icon_info', function () {
    $(".hw").toggle();
    $(".score").toggle();
});
