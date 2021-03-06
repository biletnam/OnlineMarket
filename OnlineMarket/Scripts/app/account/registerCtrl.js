﻿(function(app) {
    "use strict";

    app.controller("registerCtrl", registerCtrl);

    registerCtrl.$inject = ["$scope", "membershipService", "$rootScope", "$location"];

    function registerCtrl($scope, membershipService, $rootScope, $location) {
        $scope.register = register;
        $scope.user = {};

        function register() {
            membershipService.register($scope.user, registerCompleted);
        }

        function registerCompleted(result) {
            if (result.data.success) {
                $location.path("/checkEmail");
            } else {
                alert(result.data.message);
            }
        }
    }

})(angular.module("onlineMarket"));