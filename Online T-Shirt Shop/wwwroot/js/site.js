


function HeaderAnimation(target) {
    var text = $(target).children(".letters");
    text.html(function (index, html) {
        return html.replace(/\S/g, '<span class="letter">$&</span>');
    });    

    var tl = gsap.timeline();
    gsap.set("section>h1.section-header:first-child>.letters>.letter", { opacity: 0, scale: 0.2});
    tl.to("section>h1.section-header:first-child>.letters>.letter", { opacity: 1, duration: 1, scale: 1, ease: "expo", delay: (i) => 70 * (i + 1)});
    tl.to("section>h1.section-header", { delay:1, duration:1, ease: "expo"});
    var underline = $(target).children("span.header-underline");
    underline.animate({ width: "110%" }, 700);
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
