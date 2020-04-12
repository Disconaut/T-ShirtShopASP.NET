function LineAnimation(target) {
    var underline = $(target);
    underline.animate({width:"110%"},700);
};


function main() {
    var scrollController = new ScrollMagic.Controller();
    scrollController.addScene(new ScrollMagic.Scene({
        triggerElement: 'section > h1.section-header:first-child > span.header-underline',
        duration: 0
    }).on("enter",
        (event) => {
            console.log(event.target.triggerElement());
            LineAnimation(event.target.triggerElement());
        }));

    $("main").hide().fadeIn(400, function() {
        //pageMain();
    });
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

$(window).on("load", () => main());
