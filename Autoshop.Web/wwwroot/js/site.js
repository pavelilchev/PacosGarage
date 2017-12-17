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

function myMap() {
    var myLatLng = { lat: 43.223020, lng: 27.927074 };

    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 16,
        center: myLatLng,
        scrollwheel: false
    });

    var contentString = `<div class="info-window">
                            <img src="/images/logo.png" alt="Paco's Logo" width="150px" />
                            <p>
                            Address: Petar Raichev 4, Varna<br>
                            Phone: 0886 808 571<br>
                            Email: webmail.paco.garage@gmail.com
                            </p>
                         </div>`;

    var infowindow = new google.maps.InfoWindow({
        content: contentString
    });

    var marker = new google.maps.Marker({
        position: myLatLng,
        map: map,
        title: "Paco's Garage"
    });

    marker.addListener('click', function () {
        infowindow.open(map, marker);
    });
}