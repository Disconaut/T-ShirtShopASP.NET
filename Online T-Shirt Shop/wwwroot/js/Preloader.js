function plMain() {
    if (sessionStorage.getItem("loaded") === "true") {
        return;
    }

    $(".preloader-overlay").css("display", "flex");

    $(window).on("load",
        function () {
            $(".preloader-overlay").fadeOut().stop().delay(400).fadeOut('slow');
            sessionStorage.setItem("loaded", "true");
        });
}

plMain();