$(document).on("click", '.select-catagory', function (e) {
    e.preventDefault();
    let aHref = e.target.href;
    console.log(aHref)


    $.ajax(
        {
            type: "GET",
            url: aHref,

            success: function (response) {
                console.log(response)
                $('.shopPageProduct').html(response);

            },
            error: function (err) {
                $(".product-details-modal").html(err.responseText);

            }

        });

})