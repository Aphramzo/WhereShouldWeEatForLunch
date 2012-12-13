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
        $.each(data, function (key, val) {
            if (val.categories[0])
                items.push('<li id="' + key + '">' + val.name + '  (<a href="#" data-role="button" class="filterByCategory" data-category-id="' + val.categories[0].id + '">' + val.categories[0].name + '</a>)' + '</li>');
            else {
                items.push('<li id="' + key + '">' + val.name + '</li>');
            }
        });

        $('#eateryList').html(items.join(''));

        $('.filterByCategory').click(function (e) {
            var categoryId = $(this).attr('data-category-id');
            loadFourSquareResults(lat, lon, categoryId);
        });
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
    lat = p.coords.latitude.toFixed(3);
    lon = p.coords.longitude.toFixed(3);

    loadFourSquareResults();

}

function showWaiting() {
    $('#eateryList').html('<li>Reticulating Splines.... Please Wait</li>');
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