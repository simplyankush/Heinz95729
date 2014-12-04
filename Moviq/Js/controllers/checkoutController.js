/*global define, JSON*/

define('controllers/checkoutController', {
    init: function ($, routes, viewEngine,Cart) {
    "use strict";

    
    routes.get('/#/checkout', function (context) {
            viewEngine.setView({
                template: 't-checkout',
                data: dataModel()
           });
    });

    var dataModel = function (data) {
        var self = {};

        self.testcart = [    // later changes to the live data
                           { thumbnailLink: '/images/books/beforeIGo.jpg', title: 'Sample Book', detailsLink: 'http://www.google.com', price: 4.99 },
                           { thumbnailLink: '/images/books/beforeIGo.jpg', title: 'Sample Book: The Sequel', detailsLink: 'http://www.yahoo.com', price: 5.99 },
                           { thumbnailLink: '/images/books/beforeIGo.jpg', title: 'Sample Book: The Exciting End of the Trilogy', detailsLink: 'http://www.bing.com', price: 6.99 }
        ];

        self.totalPrice = ko.computed(function () {
            var total = 0, i = 0, current;
            
            for (i; i<self.testcart.length; i++) {
                current = self.testcart[i];
                total += current.price;
            };
           
            return total;
        });

        self.totalItem = ko.computed(function () {
            return self.testcart.length;
        });

        return self;
    };

    
    }
});