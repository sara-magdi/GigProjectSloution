


var AttendenceServer = function () {
    var DeleteAttending = function (GigId, done, fail) {
        $.ajax({
            url: "/api/Attendances/DeleteAttendance/" + GigId,
            type: "Post",
            cache: "false",
            contentType: "application/json; charset=utf-8",
            success: (done),
            error: (fail)
        });

    }
    var CreateAttending = function (GigId, done, fail) {
        var data = { GigId: GigId }
        $.ajax({
            url: "/api/Attendances/Attend/",
            type: "Post",
            data: JSON.stringify(data),
            cache: "false",
            contentType: "application/json; charset=utf-8",
            success: (done),
            error: (fail)
        });
    }
    return {
        CreateAttending: CreateAttending,
        DeleteAttending: DeleteAttending
    }
}();
var GigController = function (AttendenceServer) {
    var button;
    var init = function () {
        $(".Js-toggle-Attendance").click(toggleAttendance);
    };
    var toggleAttendance = function (e) {
        button = $(e.target);
        var GigId = button.attr("data-gig-id");
        if (button.hasClass("btn-outline-info"))
            AttendenceServer.CreateAttending(GigId, done, fail)
        else
            AttendenceServer.DeleteAttending(GigId, done, fail)
    };
    var fail = function () {
        alert("Something fail");
    };
    var done = function () {
        var text = (button.text() == "Going") ? "Going?" : "Going"
        button.toggleClass("btn-info").toggleClass("btn-outline-info").text(text);
    };
    return {
        init: init
    }
}(AttendenceServer)



var FollowServer = function () {
    var DeleteFollow = function (FolloweeId, done, fail) {
        $.ajax({
            url: "/api/Followings/Unfollow/" + FolloweeId,
            type: "Post",
            cache: "false",
            contentType: "application/json",
            success: (done),
            error: (fail)
        });
    };
    var CreateFollow = function (FolloweeId, done, fail) {
        var data = { FolloweeId: FolloweeId }
        $.ajax({
            url: "/api/Followings/Follow",
            type: "Post",
            cache: "false",
            contentType: "application/json",
            data: JSON.stringify(data),
            success: (done),
            error: (fail)
        });
    };
    return {
        DeleteFollow: DeleteFollow,
        CreateFollow: CreateFollow
    }
}();

var FollowController = function (FollowServer) {
    var FollowButton;
    var init = function () {
        $(".js-toggle-follow").click(toggleFollowing);
    };
    var toggleFollowing = function (e) {
        FollowButton = $(e.target);
        FollowButton = $(e.target);
        var FolloweeId = FollowButton.attr("data-user-id");
        if (FollowButton.hasClass("btn-outline-info"))
            FollowServer.CreateFollow(FolloweeId, done, fail);
        else
            FollowServer.DeleteFollow(FolloweeId, done, fail);
    };
    var done = function () {
        var text = (FollowButton.text() == "Follow") ? "Following" : "Follow";
        FollowButton.toggleClass("btn-info").toggleClass("btn-outline-info").text(text);
    };
    var fail = function () {
        alert("Something failed");
    };
    return {
        init: init
    }
}(FollowServer);