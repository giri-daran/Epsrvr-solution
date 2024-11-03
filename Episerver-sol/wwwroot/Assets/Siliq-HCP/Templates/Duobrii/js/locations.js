var map;
var markersArray = [];
var current_page = 0;
var new_markersArray = [];
var pagination_results = [];
$(document).ready(function () {
    $('#locator_zip').keypress(function (event) {

        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            validateZipCode();
            return false;
        }

    });
    $('#location_list').on('click', 'strong', function (e) {
        map.panTo(new google.maps.LatLng($(this).data('lat'), $(this).data('lng')));
        map.setZoom(20);
    });

 
    //$('#locator_print').on('click', printList);
});

function initMap() {
    var pagination_pages = document.getElementsByClassName("page-link");
    var page_size = 10;
 
    var originIcon = '/Assets/PhysicianFinder/Templates/duobrii/Images/origin.png';
    var directionIcon = '/Assets/PhysicianFinder/Templates/duobrii/Images/directions-icon.png';
    var nextone = 0;
   
    var start_coords = {};
    if ($('#locator_zip').val()) {
        $.ajax({
            type: "POST",
            url: window.location.origin + '/api/PhysicianFinderElementBlock/Locate',
            data: { zip: $('#locator_zip').val() },
            dataType: 'JSON',
            success: function (data) {
                console.log(data);
                start_coords = { lat: data.lat, lng: data.lng };
                zoom = 0;
                map = new google.maps.Map($('#locator_map')[0], {
                    zoom: 0, center: start_coords
                });
                $('#location_list').html('');
                $('.location_numbers').text(data.locations.length);
                var bounds = new google.maps.LatLngBounds();

                //add 4 points to bounds N,E,S,W
                var radius = parseInt(300) * 1609.344; //miles to metres                
                var origin = new google.maps.LatLng(start_coords);
                var spherical = google.maps.geometry.spherical;
                bounds.extend(spherical.computeOffset(origin, radius, 0));
                bounds.extend(spherical.computeOffset(origin, radius, 90));
                bounds.extend(spherical.computeOffset(origin, radius, 180));
                bounds.extend(spherical.computeOffset(origin, radius, -90));

                var marker = new google.maps.Marker({
                    map: map,
                    position: { lat: data.lat, lng: data.lng },
                    icon: originIcon
                });
               
                $.each(data.locations, function (n, loc) {
                   
                    pagination_results.push(loc);
                 


                    var siliqlist = '<div id="physicianList">' + '<div id="fields">' + '<p class="orange resultsTitle">' + loc.user_3_text + ' ' + loc.user_4_text + '</p>' + '<p class="miles d-md-none"><span class="orange">' + Number(loc.distance).toFixed(2) + ' miles' + '</span></p>' + '<p>' + loc.name + '</p>' + '<p>' + loc.address_1 + ', ' + loc.city + ', ' + loc.state + ' ' + loc.zip + '</p>' + '<p class="phone"><a class="open_call" href="tel:' + loc.phone + '">' + loc.phone + '</p></a>' + '</div>' + '<div id="directions">' + '<p class="miles d-none d-md-block"><span class="orange">' + Number(loc.distance).toFixed(2) + ' miles' + '</span></p>' +'<a class="redirect" href="https://maps.google.com/?q=' + loc.address_1 + ', ' + loc.city + ', ' + loc.state + ' ' + loc.zip + '" target="_blank">' + '<img src="' + directionIcon + '" alt="" width="60">' + '<p class="orange">Directions</p>' + '</a>' + '</div>' + '</div>';


                    new_markersArray[nextone] = siliqlist;
                    var marker_content = loc.name + "<br>" + loc.address_1 + '<br>' + loc.address_2 + '<br>' + loc.city + ', ' + loc.state + ' ' + loc.zip + '<a class="redirect" href="https://maps.google.com/?q=' + loc.address_1 + ', ' + loc.city + ', ' + loc.state + ' ' + loc.zip + '" target="_blank">' + '<p class="orange">Get Directions</p>' + '</a>';
                    if (nextone<10)
                    $('#location_list').append(siliqlist);
                    var label = String(n + 1);
                    var coord = new google.maps.LatLng(loc.lat, loc.lng);
                   // bounds.extend(coord);
                    marker = new google.maps.Marker({
                        position: { lat: loc.lat, lng: loc.lng },
                        map: map,
                        title: loc.practice,
                        icon: { url: '/Assets/PhysicianFinder/Templates/duobrii/Images/destination.png'},
                        //label: { text: label, color: '#fff', fontSize: '16px' }
                    });

                    markersArray[nextone] = marker;

                    markersArray[nextone].infowindow = new google.maps.InfoWindow({
                        content: marker_content
                    })

                    google.maps.event.addListener(markersArray[nextone], 'click', function () {
                        for (var k = 1; k < markersArray.length; k++) {
                            markersArray[k].infowindow.close();
                        }
                        this.infowindow.open(map, this);
                    });

                    nextone++;
                });
                map.fitBounds(bounds);
                map.zoom(12);
            }
        });
    } else {
        start_coords = { lat: 37.0902, lng: -95.7129 }; //centre of US
        map = new google.maps.Map($('#locator_map')[0], {
            zoom: 9, center: start_coords
        });
    }

    for (var i = 0; i < pagination_pages.length; i++) {
        pagination_pages[i].addEventListener('click',
            function (e) {
                $('#location_list').html('');
               // removeMarkers();
                //get page number
                e = e || window.event;
                var target = e.target || e.srcElement,
                    page_number = target.textContent || text.innerText;
                document.getElementById("next_result").classList.remove("disabled");
                document.getElementById("previous_result").classList.remove("disabled");
                /* External Link Interstitial */
                //remove all active classes
                var active = document.getElementsByClassName("active");
                while (active.length)
                    active[0].className = active[0].className.replace(/\bactive\b/g, "");

                if (page_number < 1 || page_number == 1) {
                    document.getElementById("previous_result").classList.add("disabled");
                }
                if (page_number == 3) {
                    document.getElementById("next_result").classList.add("disabled");
                }
                //paginate results
                if (Math.floor(page_number) == page_number && $.isNumeric(page_number)) {
                    current_page = page_number;
                    //get page results for current page
                    this.parentElement.classList.add("active");
                    --page_number;
                    var page_result = pagination_results.slice(page_number * page_size, (page_number + 1) * page_size);
                    for (var i = 0; i < page_size; i++) {
                        var siliqlist = '<div id="physicianList">' + '<div id="fields">' + '<p class="orange resultsTitle">' + page_result[i].user_3_text + ' ' + page_result[i].user_4_text + '</p>' +  '<p class="miles d-md-none"><span class="orange">' + Number(page_result[i].distance).toFixed(2) + ' miles' + '</span></p>' + '<p>' + page_result[i].name + '</p>' + '<p>' + page_result[i].address_1 + ', ' + page_result[i].city + ', ' + page_result[i].state + ' ' + page_result[i].zip + '</p>' + '<p class="phone"><a class="open_call" href="tel:' + page_result[i].phone + '">' + page_result[i].phone + '</p></a>' + '</div>' + '<div id="directions">' +  '<p class="miles d-none d-md-block"><span class="orange">' + Number(page_result[i].distance).toFixed(2) + ' miles' + '</span></p>' + '<a class="redirect" href="https://maps.google.com/?q=' + page_result[i].address_1 + ', ' + page_result[i].city + ', ' + page_result[i].state + ' ' + page_result[i].zip + '" target="_blank">' + '<img src="' + directionIcon + '" alt="" width="60">' + '<p class="orange">Directions</p>' + '</a>' + '</div>' + '</div>';
                        $('#location_list').append(siliqlist);
                    }
                } else {
                    if (page_number == "Previous") {
                        page_number = current_page - 1;
                        if (page_number <= 1) {
                            document.getElementById("previous_result").classList.add("disabled");
                        }
                        --current_page
                    }
                    if (page_number == "Next") {
                        page_number = parseInt(current_page) + 1;
                        ++current_page
                        if (current_page == 3) {
                            document.getElementById("next_result").classList.add("disabled")
                        }
                    }
                    var active = document.getElementsByClassName("active");
                    for (var c = 0; c < pagination_pages.length; c++) {
                        if (pagination_pages[c].innerText == page_number) {
                            pagination_pages[c].parentElement.classList.add("active");
                        }
                    }
                    //get page results for current page
                    --page_number;
                    var page_result = pagination_results.slice(page_number * page_size, (page_number + 1) * page_size);
                    for (var i = 0; i < page_size; i++) {
                        var siliqlist = '<div id="physicianList">' + '<div id="fields">' + '<p class="orange resultsTitle">' + page_result[i].name + '</p>' +  '<p class="miles d-md-none"><span class="orange">' + Number(page_result[i].distance).toFixed(2) + ' miles' + '</span></p>' + '<p>' + page_result[i].name + '</p>' + '<p>' + page_result[i].address_1 + ', ' + page_result[i].city + ', ' + page_result[i].state + ' ' + page_result[i].zip + '</p>' + '<p class="phone"><a class="open_call" href="tel:' + page_result[i].phone + '">' + page_result[i].phone + '</p></a>' + '</div>' + '<div id="directions">' + '<p class="miles d-none d-md-block"><span class="orange">' + Number(page_result[i].distance).toFixed(2) + ' miles' + '</span></p>' + '<a class="redirect" href="https://maps.google.com/?q=' + page_result[i].address_1 + ', ' + page_result[i].city + ', ' + page_result[i].state + ' ' + page_result[i].zip + '" target="_blank">' + '<img src="' + directionIcon + '" alt="" width="60">' + '<p class="orange">Directions</p>' + '</a>' + '</div>' + '</div>';
                        $('#location_list').append(siliqlist);
                    }
                }
                createMarkers(current_page);
            }, false);
    }





}
function printList(e) {
    e.preventDefault();
    var w = window.open('', 'Print', 'menubar=yes,location=yes,resizable=yes,scrollbars=yes,width=600,height=800,status=yes');
    var html = $(".locations_holder").html();

    $(w.document.body).html(html);
    var path = window.location.protocol + '//' + window.location.host + '/Locator/Print.css';
    $(w.document.head).append('<link rel="stylesheet" type="text/css" media="print,screen" href="' + path + '" />');
    var title = 'Locations near ' + $('#locator_zip').val();
    var h1 = $('<h1 />').text(title);
    $(w.document.body).prepend(h1);

    w.focus();
}