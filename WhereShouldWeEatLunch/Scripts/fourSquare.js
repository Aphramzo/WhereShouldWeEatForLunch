var lat;
var lon;

function loadFourSquareResults(categoryId) {
    showWaiting();
    $.getJSON('/eatery/FourSquareListByCoords', {
        lat: lat,
        long: lon,
        categoryId: categoryId
    }, function (data) {
        var items = [];
        $('#eateryList').html = '';
        var template = $('#collapsableTemplate').html();
        //template = '<div class="collapsible" ><h3>{0}</h3><p><a href="http://maps.google.com/?daddr={2},{3}">Directions ({1} mi.)</a><br />{4}<br />{5}</p></div>';

        $.each(data, function (key, val) {
            var phone = '';
            var menuUrl = '';
            if (val.contact) {
                phone = val.contact.formattedPhone;
            }
            if (val.menu) {
                menuUrl = '<a target="_blank" href="{0}">View Menu</a>'.format(val.menu.mobileUrl);
            }
            items.push(template.format(val.name, val.Distance.toFixed(2), val.location.lat, val.location.lng, phone, menuUrl));
        });

        $('#eateryList').html(items.join(''));
        $('#eateryList .collapsible').collapsible();

    });
    }

if (geo_position_js.init()) {
    geo_position_js.getCurrentPosition(success_callback, error_callback, { enableHighAccuracy: true });
}
else {
    alert("Functionality not available");
}

function success_callback(p) {
    //alert('lat=' + p.coords.latitude.toFixed(2) + ';lon=' + p.coords.longitude.toFixed(2));
    lat = p.coords.latitude;
    lon = p.coords.longitude;

    loadFourSquareResults();

}

function showWaiting() {
    $('#eateryList').html('Reticulating Splines.... Please Wait');
}

function error_callback(p) {
    alert('error=' + p.message);
}

$(document).ready(function () {
    $('#foodStyle').change(function () {
        //nasty way to deal with mobile leaving the anything selection on
        var cats = $(this).val().join().trim();
        if (cats.indexOf("0,") == 0) {
            cats = cats.substring(2, cats.length);
        }
        loadFourSquareResults(cats);
    });
});

String.prototype.format = function () {
    var args = arguments;
    return this.replace(/{(\d+)}/g, function (match, number) {
        return typeof args[number] != 'undefined'
      ? args[number]
      : match
    ;
    });
};