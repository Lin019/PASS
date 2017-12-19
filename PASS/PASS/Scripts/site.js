$(function () {
    CreateUser();
    showMemberInfo();  
    $(document).on('click', 'a.course-page', DirectToCoursePage);
    $(document).on('click', 'a.cancle', deleteCourse);
});

//登出
$(".logout").click(function () {
    DirectToIndex();
});

//修改個人資料
$("#modify").click(function () {
    $("#detail").hide();
    $("#modify").hide();
    $("#detail-form").show();
    $("#detail-form").css("display", "block");
    $("#submit").show();
    $("#submit").css("display", "inline-block");
    $("#student-id").text("帳號: " + userId);
});

//送出個人資料
$("#submit").click(function () {
    //$("#detail").show();
    //$("#modify").show();
    //$("#detail-form").hide();
    //$("#submit").hide();
    setMemberInfo();
    DirectToSite();
});

//開課
$("#add").click(function () {
    $("#add").hide();
    $("#info").show();
    $("#send").css("display", "inline-block");
    $("#send").show();
});

//刪除課堂資料
$(".cancle").click(function () {
    deleteCourse();
    DirectToSite();
});

//送出課堂資料
$("#send").click(function () {
    setCourse();
    DirectToSite();
});

//新增課程
function setCourse()
{
    $("#instructorID").val(userId);
    console.log(userId);
    var formData = $("#info").serializeFormJSON();
    console.log(formData);
    $.ajax({
        type: 'POST',
        url: './SetCourse',
        data: formData,
        success: function (response) {
        }
    });
}

//刪除課程
function deleteCourse()
{
    console.log("id =" + $(this).children(".courseId").first().text());
    $.ajax({
        type: 'POST',
        url: './DeleteCourse',
        data: { "courseID": $(this).children(".courseId").first().text() },
        success: function (response) {
        }
    });
}

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
    $("#student-id-hide").val(userId);
    var formData = $("#detail-form").serializeFormJSON();
    if (formData["password"] == "" || formData["email"] == "") {
        console.log("錯誤");
    }
    else {
        $.ajax({
            type: 'POST',
            url: './SetOneMemberInfo',
            data: formData,
            success: function (response) {
            }
        });
    }
}

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
            GetCourses(response["_id"]); //顯示課程
            JudgeMemberType(response["_memberType"]);
            userId = response["_id"];  
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

//判斷使用者身分
function JudgeMemberType(code)
{
    var messsage = "Welcome , you are ";
    if (code == "0") {
        $("#authority").text(messsage + "developer");
        $("#create-course").css("display", "block");
    }
    else if (code == "1") {
        $("#authority").text(messsage + "manager");
        $("#create-course").css("display", "block");
    }
    else if (code == "2") {
        $("#authority").text(messsage + "student");
    }
    else if (code == "3") {
        $("#authority").text(messsage + "professor");
        $("#create-course").css("display", "block");
    }
}

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

//重新導向到首頁
function DirectToIndex() {
    $.ajax({
        type: 'POST',
        url: './RedirectPage',
        data: { "data": "Index" },
        success: function (response) {
            window.location.href = response.url;
        }
    });
};

//取得所有課程資訊
function GetCourses(id) {
    $.ajax({
        type: 'POST',
        url: './QueryInstructorCourses',
        data: { "instructorID": id },
        success: function (response) {
            console.log(response);
            console.log(response.length);
            if (response.length > 0) {
                for (i = 0; i < response.length; i++) {
                    //設定課程卡片
                    SetCourseCard(response[i]._courseID, response[i]._courseName, response[i]._courseDescription);
                }
            }
        }
    });
}

//設定課程卡片
function SetCourseCard(id, name, description) {
    $.ajax({
        type: 'POST',
        url: './_CourseCard',
        data: "",
        success: function (partialHtml) {
            $("#course-card").append(partialHtml);
            $(".id:last").text(id);
            $(".courseId:last").text(id);
            $(".name:last").text(name);
            $(".description:last").text(description);
        }
    });
}

 //跳到該課程頁面
function DirectToCoursePage() {
    window.location.href = "/Home/Course/?ID=" + $(this).children(".id").first().text();
}

//創造使用者
function CreateUser()
{
    $.ajax({
        type: 'POST',
        url: './CreateUser',
        data: {"id":"teststudent","password":"g51014","name":"student","email":"tset","authority":2},
        success: function (response) { 
        }
    });
}