define('controllers/cartController', {
    init: function (routes, viewEngine, Products, Product, Cart) {
        "use strict";

// GET /books/search/?q=searchterm
// search for a book or books
routes.get(/^\/#\/stripe\/charge\/?/i, function (context) {  // /books
    $.ajax({
        url: '/api/stripe/charge/?q=' + context.params.q,
        method: 'GET'
    }).done(function (data) {
        var result = new Boolean(JSON.parse(data));
        if (result == true) {
            //viewEngine.setView({
            //    template: 't-productadded',
            //    data: {}
            //});
           // alert("Cart charged successfully");
        }
        else {
            //viewEngine.setView({
            //    template: 't-productexists',
            //    data: {}
            //});
            //alert("Charging failed");
        }


    });

});
    }
});
