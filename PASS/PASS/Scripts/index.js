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

$("#submit").click(function () {
    Login();
});

//導向個人頁面
function DirectToSite() {
    $.ajax({
        type: 'POST',
        url: './RedirectPage',
        data: { "data": "Site" },
        success: function (response) {
            window.location.href = response.url;
        }
    });
};

//登入
function Login() {
    var loginData = $("#login").serializeFormJSON();
    $.ajax({
        type: 'POST',
        url: './Login',
        data: loginData,
        success: function (response) {
            console.log(response);
            DirectToSite();
        }
    });
};
