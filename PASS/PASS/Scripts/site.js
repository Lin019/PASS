$("#modify").click(function () {
    $("#detail").hide();
    $("#modify").hide();
    $("#detail-form").show();
    $("#detail-form").css("display", "block");
    $("#submit").show();
    $("#submit").css("display", "inline-block");
});

$("#submit").click(function () {
    $("#detail").show();
    $("#modify").show();
    $("#detail-form").hide();
    $("#submit").hide();
    setMemberInfo();
})

//加密
function encrypt(password)
{
    //將密碼第一個字分離出來
    var passwordTemp = password.split("",1); 
    for (var i = 0; i < password.length-1; i++)
    {
        passwordTemp = passwordTemp + "*";
    }
    return passwordTemp;
}

//設定使用者資訊
function setMemberInfo() {
    $("#name").val($("#user-name").text());
    var formData = $("#detail-form").serializeFormJSON();
    $.ajax({
        type: 'POST',
        url: './SetOneMemberInfo',
        data: formData,
        success: function (response) {
        }
    });
}


$(function () {
    showMemberInfo();
});

//顯示使用者資訊
function showMemberInfo() {
    $.ajax({
        type: 'POST',
        url: './GetOneMemberInfo',
        success: function (response) {
            console.log(response);
            var html = "<p>帳號: " + response["_id"] + "</p></br>" +
                       "<p>密碼: " + encrypt(response["_memberPassword"]) + "</p></br>" +
                       "<p>信箱: " + response["_memberEmail"] + "</p>";
            $("#detail").append(html);
            $("#user-name").text(response["_memberName"]);
        }
    });
};

//序列化
(function ($) {
    $.fn.serializeFormJSON = function () {
        var o = {};
        var a = this.serializeArray();
        $.each(a, function () {
            if (o[this.name]) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        return o;
    };
})(jQuery);