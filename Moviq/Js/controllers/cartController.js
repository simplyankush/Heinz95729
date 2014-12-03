define('controllers/cartController', { init: function (routes, viewEngine, Products, Product, Cart) {
    "use strict";

    // GET /books/search/?q=searchterm
    // search for a book or books
    routes.get(/^\/#\/cart\/add\/?/i, function (context) { 
        $.ajax({
            url: '/api/cart/add/' + context.params.q,
            method: 'GET'
        }).done(function (data) {  
    