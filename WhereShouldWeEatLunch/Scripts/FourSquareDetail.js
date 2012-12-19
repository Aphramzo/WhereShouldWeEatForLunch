$(document).ready(function () {
    if (/Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent)) {
        window.mySwipe = new Swipe(
            document.getElementById('slider')
        );
    }
});