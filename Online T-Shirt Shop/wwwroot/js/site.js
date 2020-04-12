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

function SetLine() {

    $("section > h1.section-header:first-child > span.header-underline").each(function (index) {
        var textWidth = $(this).parent().textWidth();
        $(this).width(textWidth);
    });

}

$(document).ready(SetLine);