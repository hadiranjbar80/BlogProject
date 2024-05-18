/* Article Scripts */

function DeleteArticle(id) {
    $.ajax({
        url: "/Admin/Article/Delete/",
        data: { id }
    }).done(function (res) {
        $("#myModal").modal();
        $("#myModalLabel").html("حذف مقاله");
        $("#myModalBody").html(res);
    })
}

/* User Scripts */

function DeleteUser(id) {
    $.ajax({
        url: "/Admin/ManageUsers/DeleteUser/",
        data: { id }
    }).done(function (res) {
        $("#myModal").modal();
        $("#myModalLabel").html("حذف کاربر");
        $("#myModalBody").html(res);
    })
}


/* Delete Category Scripts */

function DeleteCategory(id) {
    $.ajax({
        url: "/Admin/ArticleCategory/Delete/",
        data: { id }
    }).done(function (res) {
        $("#myModal").modal();
        $("#myModalLabel").html("حذف گروه");
        $("#myModalBody").html(res);
    })
}


/* menu collapse */

/* Set the width of the sidebar to 250px and the left margin of the page content to 250px */
function openNav() {
    document.getElementById("menu-bar").style.display = "block"
    document.getElementById("menu-btn").style.display = "none"

}

/* Set the width of the sidebar to 0 and the left margin of the page content to 0 */
function closeNav() {
    document.getElementById("menu-btn").style.display = "block"
    document.getElementById("menu-bar").style.display = "none";
}

/* Create Comment Section */

$(document).ready(function () {
    $.validator.unobtrusive.parse($("#createCommentForm"));
    CKEDITOR.replace('#commentText')
})

function ReplyComment(id) {
    $("#ParentId").val(id);
    $("html,body").delay(2000).animate({ scrollTop: $('#createCommentForm').offset().top }, 2000);
}

function SaveComment() {
    if ($("#createCommentForm").valid()) {
        CKEDITOR.instances.commentText.updateElement();
        var formData = $("#createCommentForm").serializeArray()
        $.ajax({
            url: "/Comment/CreateComment",
            type: "POST",
            data: formData,
            success: function (response) {
                swal({
                    text: response,
                    icon: "success",
                    button: "باشه"
                });
                $("#createCommentForm")[0].reset();
                CKEDITOR.instances.commentText.setData('');

            },
            error: function (request, status, error) {
                swal(request.responseText);
            }

        });
    }
}

