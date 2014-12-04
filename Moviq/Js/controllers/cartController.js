define('controllers/cartController', {
    init: function (routes, viewEngine, Products, Product, Cart) {
        "use strict";

        // GET /books/search/?q=searchterm
        // search for a book or books
        routes.get(/^\/#\/cart\/add\/?/i, function (context) {  // /books
            $.ajax({
                url: '/api/cart/add/?q=' + context.params.q,
                method: 'GET'
            }).done(function (data) {
                var result = new Boolean (JSON.parse(data));
                if (result == true) {
                    viewEngine.setView({
                        template: 't-productadded',
                        data: {}
                    });

                }
                else {
                    viewEngine.setView({
                        template: 't-productexists',
                        data: {}
                    });
                }


            });

        });
    }
});

    
    
    
    
    
    