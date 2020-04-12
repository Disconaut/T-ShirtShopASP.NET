var underline = document.getElementsByClassName("section > h1.section-header:first-child > span.header-underline");


$.fn.textWidth = function () {
    var self = $(this),
        children = self.contents().filter(function () { return this.nodeType === 3; }),
        calculator = $('<span></span>'),
        width;

    children.wrap(calculator);
    width = children.parent().width();
    children.unwrap();
    return width;
};

function LineAnimation() {
    var underline = $("section > h1.section-header:first-child > span.header-underline");
    underline.width(0);
    underline.animate({width:"110%"},800);
};


//themeSwitcher.addEventListener("click", function() {
//    if (themeSwitcher.checked) switchTheme("dark");
//    else switchTheme("white");
//});
function main() {
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

$(window).ready(() => main())