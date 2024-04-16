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