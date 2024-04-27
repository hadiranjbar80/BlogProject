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