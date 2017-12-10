$(function () {
    if ($("#appointmentDate").datepicker) {
        $("#appointmentDate").datepicker({
            minDate: new Date()
        });
    }
});

$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/api/blog",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {                
            $("#posts").html('');

            $.each(data, function (index, p) {
                var postAsHtml =
                    `<div class="post">
                        <p><span><a href="/blog/articles/${p.id}">${p.title}</a></span></p>
                       </div>`;
                $('#posts').append(postAsHtml);
            }); 
        }
    });
});  