define('controllers/cartController', {
    init: function (routes, viewEngine, Products, Product, Cart) {
        "use strict";


        routes.get(/^\/#\/cart\/delete\/?/i, function (context) {  // /books
            $.ajax({
                url: '/api/cart/delete/?q=' + context.params.q,
                method: 'GET'
            }).done(function (data) {
                var result = new Boolean(JSON.parse(data));
                if (result == true) {
                   
                    window.location.href = "/#/checkout";
                   
                }
                else {
                    alert("failed to delete product from cart");
                }


            });

        });

        routes.post(/^\/#\/stripe\/?/i, function (context) {

            viewEngine.setView({
                template: 't-stripe',
                data: { totalPrice: context.params.totalamt }
            });
        });


        // GET /books/search/?q=searchterm
        // search for a book or books
        routes.get(/^\/#\/cart\/add\/?/i, function (context) {  // /books
            $.ajax({
                url: '/api/cart/add/?q=' + context.params.q,
                method: 'GET'
            }).done(function (data) {
                var result = JSON.parse(data);

                if (result == 1) {
                
                    alert("Product added to cart");
                }

                else if (result == 3) {
                    alert("Must be logged in to add items to cart.");
                    viewEngine.setView({
                        template: 't-login',
                        data: {}
                    });
                }
                else {
                   
                    alert("Product exists in cart");
                }


            });

        });
    
    }
});
    