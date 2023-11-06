$(document).ready(function () {


    $(document).on("click", ".show-more button", function () {

        let parent = $(".parent-elem");

        let skipCount = $(parent).children().length;

        let productsCount = $(parent).attr("data-count");


        $.ajax({
            url: `shop/loadmore?skipCount=${skipCount}`,
            type: "Get",
            success: function (res) {

                $(parent).append(res);

                skipCount = $(parent).children().length

                if (skipCount >= productsCount) {
                    $(".show-more button").addClass("d-none");
                }

                
            }

        })
    })










})