(function (app) {
    "use strict";

    app.controller("loginCtrl", loginCtrl);

    loginCtrl.$inject = ["$scope", "membershipService", "$rootScope", "$location"];

    function loginCtrl($scope, membershipService, $rootScope, $location) {
        $scope.login = login;
        $scope.user = {};

        function login() {
            membershipService.login($scope.user, loginCompleted)
        }

        function loginCompleted(result) {
            if (result.data.success) {
                membershipService.saveCredentials($scope.user);
                $scope.userData.displayUserInfo();
                if ($rootScope.previousState)
                    $location.path($rootScope.previousState);
                else
                    $location.path("/");
            }
            else {
                alert(result.data.message);
            }
        }
    }

})(angular.module("onlineMarket"));