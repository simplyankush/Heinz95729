/*global define, JSON*/

define('controllers/checkoutController', {
    init: function ($, routes, viewEngine, Cart, Books) {
        "use strict";

        // POST /login
        // login
        routes.get(/^\/#\/test\/?/i, function (context) {
            var totalamtcheck = context.params.totalamtcheck;
            //alert(String(totalamtcheck));
            Stripe.setPublishableKey('pk_test_SHUCnuSdIBx8hlpn2m3JohGt');
            // ...


            var $form = $('#payment-form');

            // Disable the submit button to prevent repeated clicks
            $form.find('button').prop('disabled', true);
            
            Stripe.card.createToken($form, stripeResponseHandler);

            // Prevent the form from submitting with the default action
            return false;


            function stripeResponseHandler(status, response) {
                var $form = $('#payment-form');

                if (response.error) {
                    // Show the errors on the form
                    $form.find('.payment-errors').text(response.error.message);
                    $form.find('button').prop('disabled', false);
                } else {
                    // response contains id and card, which contains additional card details
                    var token = response.id;
                    //alert(String(token));
                    // Insert the token into the form so it gets submitted to the server
                    $form.append($('<input type="hidden" name="stripeToken" />').val(token));
                    // and submit

                    $.ajax({
                        url: '/api/pay/?token=' + String(token) + '&amt=' + String(totalamtcheck),
                        method: 'GET'
                    }).done(function (data) {
                        alert('Charged Successfully');
                        var result = new Boolean(JSON.parse(data));
                        if (result == true) {
                            alert('Charged Successfully');
                            window.location.href = "/#/deliveritems";


                        }
                        else {
                            alert("failed to charge card");
                            viewEngine.setView({
                                template: 't-stripe',
                                data: { totalPrice: totalamtcheck }
                            });
                        }
                    })
                    //$form.get(0).submit();
                }
            };
            
            //return true; // ignore
        });



        routes.get(/^\/#\/deliveritems\/?/i, function (context) {

            $.ajax({
                url: '/api/cart/full',
                method: 'GET'
            }).done(function (data) {
                var books = new Books(JSON.parse(data));

                var dataModel = function (data) {

                    var self = {};

                    self.boughtItems = books.books;

                    //self.boughtItems = [    // later changes to the live data
                    //                   { thumbnailLink: '/images/books/beforeIGo.jpg', dllink: 'http://www.gasl.org/refbib/Carroll__Alice_1st.pdf', title: 'Alice', detailsLink: 'http://www.google.com', price: 4.99 },
                    //                   { thumbnailLink: '/images/books/beforeIGo.jpg', dllink: 'http://www.gasl.org/refbib/Carroll__Alice_1st.pdf', title: 'Sample Book: The Sequel', detailsLink: 'http://www.yahoo.com', price: 5.99 },
                    //                   { thumbnailLink: '/images/books/beforeIGo.jpg', dllink: 'http://www.gasl.org/refbib/Carroll__Alice_1st.pdf', title: 'Sample Book: The Exciting End of the Trilogy', detailsLink: 'http://www.bing.com', price: 6.99 }
                    //];

                    //self.testcart = books.books;

                    self.totalPrice = ko.computed(function () {
                        var total = 0, i = 0, current;

                        for (i; i < self.boughtItems().length; i++) {
                            current = self.boughtItems()[i];
                            total += current.price();
                        };

                        return total;
                    });

                    self.totalItem = ko.computed(function () {
                        return self.boughtItems().length;
                    });

                    return self;
                };



                viewEngine.setView({
                    template: 't-deliveritems',
                    data: dataModel()
                });
            })
        });


        routes.get(/^\/#\/checkoutfailed\/?/i, function (context) {
            viewEngine.setView({
                template: 't-badcharge',
                data: {}
            });
        });

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




        });
    }
});