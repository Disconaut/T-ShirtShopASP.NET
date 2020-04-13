function HeaderAnimation(target) {
    var text = $(target).children("span:not(.header-underline)");
    var underline = $(target).children("span.header-underline");
    underline.animate({width:"110%"},700);
};

function createHeaderScenes(scrollController) {
    $("section > h1.section-header:first-child").each(function() {
        var currentHeader = this;
        var scene = new ScrollMagic.Scene({
            triggerElement: currentHeader,
            duration: 0,
            offset: "100%",
            triggerHook: "onEnter"
        }).on("enter",
            (event) => {
                HeaderAnimation(currentHeader);
            }).addIndicators({name:"Header"});

        scrollController.addScene(scene);
    });
}


function main() {
    var scrollController = new ScrollMagic.Controller();
    createHeaderScenes(scrollController);

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

    $("main").hide().fadeIn(400, function () {
        if(typeof pageMain === "function")
            pageMain(scrollController);
    });
}

$(window).on("load", () => main());
