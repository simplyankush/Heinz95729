/*global define, JSON*/

define('controllers/checkoutController', {
    init: function ($, routes, viewEngine,Cart) {
    "use strict";

    
        routes.get('/#/checkout', function (context) {
            viewEngine.setView({
                template: 't-checkout',
                data: {
                    testcart: [
                            { thumbnailLink: '/images/books/beforeIGo.jpg', title: 'Sample Book', detailsLink: 'http://www.google.com', price: '4.99' },
                            { thumbnailLink: '/images/books/beforeIGo.jpg', title: 'Sample Book: The Sequel', detailsLink: 'http://www.yahoo.com', price: '5.99' },
                            { thumbnailLink: '/images/books/beforeIGo.jpg', title: 'Sample Book: The Exciting End of the Trilogy', detailsLink: 'http://www.bing.com', price: '6.99' }
                    ],
                    total: ko.computed(function(theCart){
                        var total = 0;
                        for (var item in theCart) {  // not hitting this loop
                                total += item.price;
                                alert(item.price);
                        };
                        //ko.utils.arrayForEach(theCart, function (item) {
                        //    var value = parseFloat(item.price());
                        //    if (!isNaN(value)) {
                        //        total += value;
                        //    }
                        //});
                        return total;
                    }, this.testcart),
                    totalItem: 100
                 }
           });
        });

    
    }
});