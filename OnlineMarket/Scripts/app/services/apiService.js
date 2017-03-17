(function (app) {
    "use strict";

    app.factory("apiService", apiService);

    apiService.$inject = ["$http", "$location", "$rootScope"];

    function apiService($http, $location, $rootScope) {
        var service = {
            get: get,
            post: post
        };

        function get(url, config, success, failure) {
            return $http({ method: "GET", url: url, params: config })
                    .then(function (result) {
                        success(result);
                    }, function (error) {
                        if (error.status === "401") {
                            $rootScope.previousState = $location.path();
                            $location.path("/login");
                        }
                        else if (failure != null) {
                            failure(error);
                        }
                    });
        }

        function post(url, data, success, failure) {
            return $http.post(url, data)
                    .then(function (result) {
                        success(result);
                    }, function (error) {
                        if (error.status === "401") {
                            $rootScope.previousState = $location.path();
                            $location.path("/login");
                        }
                        else if (failure != null) {
                            failure(error);
                        }
                    });
        }

        return service;
    }
})(angular.module("onlineMarket"));