﻿$(".add").click(function () {
    $(".info").last().toggle();
    $(".assign").last().toggle();
    $(".add").last().hide();
    $(".send").last().css("display", "inline-block");
    $(".send").last().show();
});

$(".send").click(function () {
    $("div.info").toggle();
    $(".assign").toggle();
    $(".add").show();
    $(".send").hide();
});

$(".icon_info").click(function () {
    $(".hw").toggle();
    $(".score").toggle();
});