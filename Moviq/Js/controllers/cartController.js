define('controllers/cartController', { init: function (routes, viewEngine, Products, Product, Cart) {
    "use strict";

    // GET /books/search/?q=searchterm
    // search for a book or books
    routes.get(/^\/#\/cart\/add\/?/i, function (context) { 
        $.ajax({
            url: '/api/cart/add/' + context.params.splat[0]
            method: 'GET'
    }).done(function (data) {
        var result = JSON.parse(data);
        if (result == true)
        {
            viewEngine.setView({
                template: 't-productadded',
                data: {}
            });
        
        }
        else
        {
            viewEngine.setView({
                template: 't-productexists',
                data: {}
            });
        }
    
