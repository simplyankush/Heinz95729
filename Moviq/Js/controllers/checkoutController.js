/*global define, JSON*/

define('controllers/checkoutController', {
    init: function ($, routes, viewEngine, Cart, Books) {
        "use strict";

        // POST /login
        // login
        routes.post(/^\/#\/pay\/?/i, function (context) {
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
                        
                        var result = new Boolean(JSON.parse(data));
                        if (result == true) {                        
                            window.location.href = "/#/deliveritems";


                        }
                        else {
                            viewEngine.setView({
                                template: 't-badcharge',
                                data: { totalPrice: totalamtcheck }
                            });
                        }
                    })
                   
                }
            };
            
            
        });



        routes.get(/^\/#\/deliveritems\/?/i, function (context) {

            $.ajax({
                url: '/api/cart/paid',
                method: 'GET'
            }).done(function (data) {
                var books = new Books(JSON.parse(data));

                var dataModel = function (data) {

                    var self = {};

                    self.boughtItems = books.books;

                  

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

               
                var dataModel = function (data) {

                    var self = {};


                   

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