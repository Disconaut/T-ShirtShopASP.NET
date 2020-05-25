$("#product-form").submit(function (e) {
    e.preventDefault();
    e.stopPropagation();
    var postUrl = $(this).attr("action");
    var formData = $(this).serialize();

    $.post(postUrl, formData, function (response) {
        $("#cart-modal").replaceWith($(response));
    });
});