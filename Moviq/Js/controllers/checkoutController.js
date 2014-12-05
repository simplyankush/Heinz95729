/*global define, JSON*/

define('controllers/checkoutController', {
    init: function ($, routes, viewEngine,Cart,Books) {
        "use strict";

    
        routes.get('/#/checkout', function (context) {
            $.ajax({
                url: '/api/cart/full',
                method: 'GET'
            }).done(function (data) {
                var books = new Books(JSON.parse(data));

                //alert(String(books));
                var dataModel = function (data) {
                 
                    var self = {};


                    //self.testcart = [    // later changes to the live data
                    //                   { thumbnailLink: '/images/books/beforeIGo.jpg', title: 'Sample Book', detailsLink: 'http://www.google.com', price: 4.99 },
                    //                   { thumbnailLink: '/images/books/beforeIGo.jpg', title: 'Sample Book: The Sequel', detailsLink: 'http://www.yahoo.com', price: 5.99 },
                    //                   { thumbnailLink: '/images/books/beforeIGo.jpg', title: 'Sample Book: The Exciting End of the Trilogy', detailsLink: 'http://www.bing.com', price: 6.99 }
                    //];

                    self.testcart = books.books;

                    self.totalPrice = ko.computed(function () {
                        var total = 0, i = 0, current;

                        

                        for (i; i < self.testcart().length; i++) {
                            current = self.testcart()[i];
                            total += current.price();
                        };
           
                        return total;
                    });

                    self.totalItem = ko.computed(function () {
                        return self.testcart().length;
                    });

                    return self;
                };






                viewEngine.setView({
                    template: 't-checkout',
                    data: dataModel()
                });
            });

    

    
        }
        )}});