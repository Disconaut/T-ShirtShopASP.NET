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
function reloadCartCounter() {
    $.ajax('/Shop/CartCounter').then((data) => {
        $('#item-count-crt').text(data);
    });
}

function onModalClose(selector) {
    $(selector).remove();
}

// -- < Cart

function changeQuantity(fieldSelector, action) {
    let quantity = parseInt($(fieldSelector).val());
    if (action === 'increase') {
        quantity += 1;
    } else if(action === 'decrease') {
        quantity -= 1;
    }

    $(fieldSelector).val(quantity);
    $(fieldSelector).change();
}

function onQuantityChange(productId, targetSelector, productTotalSelector, totalSelector, subtotalSelector) {
    const target = $(targetSelector);
    $.ajax(`/EditCart/${productId}/${target.val()}`,
        {
            method: 'POST',
        }).then(data => {
            target.val(data.quantity);
            $(productTotalSelector).text(data.productTotal);
            $(totalSelector).text(data.total);
            $(subtotalSelector).text(data.total);
            reloadCartCounter();
    }).catch(() => {
        alert("Sorry, we cannot update your cart. Please, inform us about this problem.");
    });
}

function onRemoveClick(productId, productSelector, totalSelector, subtotalSelector) {
    $.ajax(`/DeleteFromCart/${productId}`,
        {
            method: 'POST'
        }).then((data) => {
            $(productSelector).remove();
            $(totalSelector).text(data);
            $(subtotalSelector).text(data);
            reloadCartCounter();
    });
}

function onOrderClick() {
    $.ajax('/Shop/Order', { method: 'POST' }).then(() => {
        alert('Your order is successfully created.');
        location.reload();
    }).catch(() => {
        alert('Cannot create order for you. Maybe, you have nothing to order.');
    });
}

// -- />

function main() {
    //var scrollController = new ScrollMagic.Controller();
    //createHeaderScenes(scrollController);
    reloadCartCounter();
    $("main").hide().fadeIn(400, function () {
        if(typeof pageMain === "function")
            pageMain();
    });
}

$(document).ready(() => main());
