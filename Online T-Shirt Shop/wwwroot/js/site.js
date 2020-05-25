//function HeaderAnimation(target) {
//    var text = $(target).children(".header-text");
//    var underline = $(target).children("span.header-underline");
//    text.html(function (index, html) {
//        return html.replace(/\S/g, "<span class='letter'>$&</span>");
//    });

//    window.gsap.timeline().set(target, {opacity: 1})
//        .fromTo(text.children(".letter"),
//            {
//                scale: 0.3,
//                opacity: 0
//            },
//            {
//                scale: 1,
//                opacity: 1,
//                translateZ: 0,
//                ease: "expo",
//                duration: 0.6,
//                delay: (i) => {
//                    return 0.07 * (i + 1);
//                }
//            }).fromTo(underline,
//            {
//                scaleX: 0,
//                opacity: 0.5
//            },
//            {
//                scaleX: 1,
//                opacity: 1,
//                ease: "expo",
//                duration: 0.7
//            });
//};


//function createHeaderScenes(scrollController) {
//    $("section > h1.section-header:first-child").each(function() {
//        var currentHeader = this;
//        var scene = new ScrollMagic.Scene({
//            triggerElement: currentHeader,
//            duration: 0,
//            offset: "100%",
//            triggerHook: "onEnter",
//            reverse: false
//        }).on("enter",
//            (event) => {
//                HeaderAnimation(currentHeader);
//            });

//        scrollController.addScene(scene);
//    });
//}


function main() {
    //var scrollController = new ScrollMagic.Controller();
    //createHeaderScenes(scrollController);

    $("main").hide().fadeIn(400, function () {
        if(typeof pageMain === "function")
            pageMain();
    });
}

$(document).ready(() => main());




$("#cart-form").submit(function () {
    var postUrl = $(this).attr("action");
    var formData = $(this).serialize();

    if (($("#quantity").val() === 1 && $(this).serializeArray()["action"] === "minus") || $("#quantity").val() === 0) {
        $.post("/Delete", $(this).serialize(), function(response) {
            $("#cart-modal").replaceWith($(response));
        });
    }
    else {
        $.post(postUrl, formData, function(response) {
            $("#cart-modal").replaceWith($(response));
        });
    }
});


