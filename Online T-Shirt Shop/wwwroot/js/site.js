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


$(window).on("load", () => {
    $("main").hide().fadeIn(500,LineAnimation);
});