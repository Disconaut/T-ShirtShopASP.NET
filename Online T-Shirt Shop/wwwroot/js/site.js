function HeaderAnimation() {
    var underline = $("section > h1.section-header:first-child > span.header-underline");
    underline.animate({width:"110%"},700);
};


function main() {
    $("main").hide().fadeIn(400, LineAnimation);
    var lastScroll = $(window).scrollTop();

    $(window).scroll(() => {
        var currentScroll = $(window).scrollTop();
        if (currentScroll <= 0) {
            $("body").removeClass("downscroll");
            return;
        }
        if (currentScroll < lastScroll) {
            $("body").removeClass("downscroll");
        } else {
            $("body").addClass("downscroll");
        }
        lastScroll = currentScroll;
    });
}

$(window).ready(() => main());
