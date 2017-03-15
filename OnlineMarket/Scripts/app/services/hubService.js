(function (app) {
    
    'use strict';

    app.factory('hubService', hubService);
    
    hubService.$inject = ['$rootScope'];

    function hubService($rootScope) {

        var connection = $.hubConnection("http://localhost:52068");
        var proxy = connection.createHubProxy("AppHub");

        connection.start().done(function () { });

        var service = {
            on: function (eventName, callback) {
                proxy.on(eventName, function (result) {
                    $rootScope.$apply(function () {
                        if (callback) {
                            callback(result);
                        }
                    });
                });
            },
            invoke: function (methodName, callback) {
                proxy.invoke(methodName)
                .done(function (result) {
                    $rootScope.$apply(function () {
                        if (callback) {
                            callback(result);
                        }
                    });
                });
            }
        }

        return service;
    }
})(angular.module('onlineMarket'));