$(document).ready(function () {

 

    $(document).on("click", "#products .price a", function (e) {
        e.preventDefault();

        let id = parseInt($(this).closest(".product-item").attr("data-productId"));

        let basketCount = parseInt($(".basket-count").text());

        $.ajax({
            url: `home/addbasket?id=${id}`,
            type: "Post",
            success: function (res) {
                basketCount++;
                $(".basket-count").text(basketCount);
                Swal.fire({
                    position: 'top-right',
                    icon: 'success',
                    title: 'Success',
                    showConfirmButton: false,
                    timer: 1000
                })

            }
        })

    })


    $(document).on("click", ".delete-basket-item", function (e) {
        let id = parseInt($(this).attr("data-id"));

        $.ajax({
            url: `cart/delete?id=${id}`,
            type: "Post",
            success: function (res) {
                Swal.fire({
                    text: "Deleted",
                    icon: 'warning',
                }).then((result) => {
                    if (result.isConfirmed) {

                        $(".basket-count").text(res.count);
                        $(e.target).closest("tr").remove();
                        $(".grand-total h1 span").text(res.grandTotal);

                        if (res.count === 0) {
                            $(".alert-basket-empty").removeClass("d-none");
                            $(".basket-table").addClass("d-none");
                        }

                        Swal.fire(
                            'Deleted!',
                            'Your file has been deleted.',
                            'success'
                        )
                    }
                })
            }

        })

    })
    


    //update cookie datas when page reload

    function getCookieValue(cookieName) {
        const cookies = document.cookie.split(';');

        for (let i = 0; i < cookies.length; i++) {
            let cookie = cookies[i].trim();

            if (cookie.startsWith(cookieName + '=')) {
                let cookieValue = cookie.substring(cookieName.length + 1);
                // Decoding URL-encoded value
                cookieValue = decodeURIComponent(cookieValue);

                // Example for JSON string
                try {
                    // Parsing JSON data if the cookie value is a JSON string
                    cookieValue = JSON.parse(cookieValue);
                } catch (error) {
                    // If it's not a JSON string, continue with the decoded value
                }

                return cookieValue;
            }
        }

        return null;
    }

    const basketCookieValue = getCookieValue('basket');

    updateBasketCount(basketCookieValue)

    function updateBasketCount(cookieValue) {
        let sum = 0;

        for (const obj of cookieValue) {
            sum += obj.Count;
        }

        $(".basket-count").text(sum);
    }
})