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
    var formData = $(this).serialize();

    if ($(this).find('[id^="quantity-"').val() <= 0) {
        $.post("Shop/DeleteFromCart", formData, function (response) {
            $("#cart-modal").replaceWith(response);
        });
    }
    else {
        $.post("Shop/EditCart", formData, function (response) {
            $("#cart-modal").replaceWith(response);
        });
    }
});

$("#orderBtn").click(function() {
    alert('Order is successful');
})