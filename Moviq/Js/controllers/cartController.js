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
                    //viewEngine.setView({
                    //    template: 't-productdeleted',
                    //    data: {}
                    //});
                    window.location.href = "/#/checkout";
                    window.location.reload();
                }
                else {
                    alert("failed to delete product from cart");
                }


            });

        });

        routes.get(/^\/#\/stripe\/?/i, function (context) {
            viewEngine.setView({
                template: 't-stripe',
                data: {}
            });
        });
















        // GET /books/search/?q=searchterm
        // search for a book or books
        routes.get(/^\/#\/cart\/add\/?/i, function (context) {  // /books
            $.ajax({
                url: '/api/cart/add/?q=' + context.params.q,
                method: 'GET'
            }).done(function (data) {
                var result = new Boolean (JSON.parse(data));
                if (result == true) {
                    //viewEngine.setView({
                    //    template: 't-productadded',
                    //    data: {}
                    //});
                    alert("Product added to cart");
                }
                else {
                    //viewEngine.setView({
                    //    template: 't-productexists',
                    //    data: {}
                    //});
                    alert("Product exists in cart");
                }


            });

        });
               
    }
});

    
    
    
    
    
    