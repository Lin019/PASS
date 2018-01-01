﻿$(function () {
    showMemberInfo();  
    $(document).on('click', 'a.course-page', DirectToCoursePage);
    $(document).on('click', 'a.cancle', deleteCourse);
    $(document).on('click', 'a.assignment-page', function (event) { DirectToAssignment($(event.target)) });
});

function DirectToAssignment(source) {
    window.location.href = "/Home/Assignment?CourseID=" + source.parents().siblings("p.course-id").text() + "&Type=2";
}

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
});

//新增課程
function setCourse()
{
    $("#instructorID").val(userId);
    console.log(userId);
    var formData = $("#info").serializeFormJSON();
    console.log(formData);
    if (formData["courseName"] == "") {
        $(".console").text("課程名稱不能為空");
    }
    else if (formData["courseDescription"] == "") {
        $(".console").text("課程敘述不能為空");
    }
    else {
        $.ajax({
            type: 'POST',
            url: './SetCourse',
            data: formData,
            success: function (response) {
                DirectToSite();
            }
        });
    }
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
    /*
    //將密碼第一個字分離出來
    var passwordTemp = password; 
    for (var i = 0; i < password.length - 1; i++)
    {
        passwordTemp = passwordTemp + "*";
    }*/
    return "**********";
}

//設定使用者資訊
function setMemberInfo() {
    $("#name").val($("#user-name").text());
    $("#student-id-hide").val(userId);
    var formData = $("#detail-form").serializeFormJSON();
    //都沒改
    if (formData["password"] == "" && formData["email"] == "") {
        console.log("都沒有");
        formData["password"] = userPassword;
        formData["email"] = userEmail;
    }
    //只改信箱
    else if (formData["password"] == "" && formData["email"] != "")
    {
        console.log("只有信箱");
        formData["password"] = userPassword;
        console.log(formData);        
    }
    //只改密碼
    else if (formData["password"] != "" && formData["email"] == "") {
        console.log("只有密碼");
        formData["email"] = userEmail;
        console.log(formData);
    }
    //送資料
    $.ajax({
        type: 'POST',
        url: './SetOneMemberInfo',
        data: formData,
        success: function (response) {
        }
    });
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
            userPassword = response["_memberPassword"];
            userEmail = response["_memberEmail"];
        }
    });
};

//查詢帳號資料
function VerifyMemberInfo(userID)
{
    $.ajax({
        type: 'POST',
        url: './VerifyOneMemberInfo',
        data: { 'userID': userID },
        success: function (response) {
            //console.log(response);
            if (response == "ID not found") {
                memberExist = false;
            }
            else {
                memberExist = true;
            }
        }
    }); 
}

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
        $(".create-account").css("display", "block");
        $("#title-id").text("新增帳號");
        $("#section-two").text("新增帳號");
    }
    else if (code == "1") {
        $("#authority").text(messsage + "manager");
        $(".create-account").css("display", "block");
        $("#title-id").text("新增帳號");
        $("#section-two").text("新增帳號");
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
            if (response != "Student not found" && response != "User is not Instructor or Student" && response != "Course not found") {
                for (i = 0; i < response.length; i++) {
                    //設定課程卡片
                    SetCourseCard(response[i]._courseID, response[i]._courseName, response[i]._courseDescription);
                }
            }
            else console.log(response);
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
            $(".course-id").last().text(id);
            $(".name:last").text(name);
            $(".description:last").text(description);
        }
    });
}

 //跳到該課程頁面
function DirectToCoursePage() {
    window.location.href = "/Home/Course/?ID=" + $(this).children(".id").first().text();
}

//學生新增icon
$("#add-student").click(function()
{
    EscProfessor();
    ClickStudent();
});

//教授新增icon
$("#add-professor").click(function () {
    EscStudent();
    ClickProfessor();
});

//學生送出icon
$("#send-student").click(function () {
    CreateStudent(); 
});

//教授送出icon
$("#send-professor").click(function () {
    CreateProfessor();
});

//展開學生
function ClickStudent()
{
    $("#send-student").css("display", "inline-block");
    $("#add-student").hide();
    $("#create-student-info").css("display", "block");
    $("#student").text("新增");
}

//展開教授
function ClickProfessor() {
    $("#send-professor").css("display", "inline-block");
    $("#add-professor").hide();
    $("#create-professor-info").css("display", "block");
    $("#professor").text("新增");
}

//收起學生
function EscStudent()
{
    $("#send-student").css("display", "none");
    $("#add-student").show();
    $("#create-student-info").css("display", "none");
    $("#student").text("學生");
    $(".console").text("");
}

//收起教授
function EscProfessor() {
    $("#send-professor").css("display", "none");
    $("#add-professor").show();
    $("#create-professor-info").css("display", "none");
    $("#professor").text("教授");
    $(".console").text("");
}

//創造學生
function CreateStudent()
{
    $("#password-student").val($("#id-student").val());
    var formData = $("#create-student-info").serializeFormJSON();
    VerifyMemberInfo(formData["id"]);
    //console.log(formData);
    //資料不齊全
    if (formData["name"] == "" || formData["id"] == "" || formData["email"] == "") {
        console.log("error");
        $(".console").text("資料不齊全，請重新確認");
    }
    //帳號格式有誤
    else if (formData["id"].length != 9)
    {
        console.log("長度不正確");
        $(".console").text("學號格式有誤，請重新確認");
    }
    //帳號存在
    else if (memberExist)
    {
        $(".console").text("學號已存在，請重新確認");
    }
    else {
        $.ajax({
            type: 'POST',
            url: './CreateUser',
            data: formData,
            success: function (response) {
                console.log(response);
                DirectToSite();
            }
        });
    }
}

//創造教授
function CreateProfessor() {
    $("#password-professor").val($("#id-professor").val());
    var formData = $("#create-professor-info").serializeFormJSON();
    VerifyMemberInfo(formData["id"]);
    //console.log(formData);
    //資料不齊全
    if (formData["name"] == "" || formData["id"] == "" || formData["email"] == "") {
        console.log("error");
        $(".console").text("資料不齊全，請重新確認");
    }
    //帳號格式有誤
    else if (formData["id"].length != 9) {
        console.log("長度不正確");
        $(".console").text("帳號格式有誤，請重新確認");
    }
    //帳號存在
    else if (memberExist) {
        $(".console").text("帳號已存在，請重新確認");
    }
    else {
        $.ajax({
            type: 'POST',
            url: './CreateUser',
            data: formData,
            success: function (response) {
                console.log(response);
                DirectToSite();
            }
        });
    }
}