if (geo_position_js.init()) {
    geo_position_js.getCurrentPosition(success_callback, error_callback, { enableHighAccuracy: true });
}
else {
    alert("Functionality not available");
}

function success_callback(p) {
    //alert('lat=' + p.coords.latitude.toFixed(2) + ';lon=' + p.coords.longitude.toFixed(2));
    $.ajax({
        url: "FourSquareListByCoords",
        data: {
            lat: p.coords.latitude.toFixed(3),
            long: p.coords.longitude.toFixed(3)
        }
    });

    $.getJSON('FourSquareListByCoords', {
        lat: p.coords.latitude.toFixed(3),
        long: p.coords.longitude.toFixed(3)
    }, function (data) {
        var items = [];
        $('#eateryList').html = '';
        $.each(data, function (key, val) {
            if(val.categories[0])
            items.push('<li id="' + key + '">' + val.name + '  (<a href="#">' + val.categories[0].name + '</a>)' + '</li>');
            else {
            items.push('<li id="' + key + '">' + val.name + '</li>');
            }
        });

        $('#eateryList').html(items.join(''));
    });
}

function error_callback(p) {
    alert('error=' + p.message);
}