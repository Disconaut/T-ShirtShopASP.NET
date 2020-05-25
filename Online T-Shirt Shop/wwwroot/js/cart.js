$("#cart-modal").modal({ show: cartModalShown, backdrop: false });

$("#item-count-crt").text($("#cart-modal .product").length);

$("#cart-modal").on("shown.bs.modal",
    function() {
        cartModalShown = true;
    });

$("#cart-modal").on("hidden.bs.modal",
    function() {
        cartModalShown = false;
    });

$(".cart-form").submit(function (e) {
    e.preventDefault();
    e.stopPropagation();
    var postUrl = $(this).attr("action");
    var formData = $(this).serialize();

    if ($(this).find('[id^="quantity-"').val() <= 0) {
        $.post("/DeleteFromCart", $(this).serialize(), function (response) {
            $("#cart-modal").replaceWith($(response));
        });
    }
    else {
        $.post(postUrl, formData, function (response) {
            $("#cart-modal").replaceWith($(response));
        });
    }
});