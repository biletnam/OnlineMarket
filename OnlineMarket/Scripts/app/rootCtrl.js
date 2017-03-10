(function (app) {
    'use strict';

    app.controller('rootCtrl', rootCtrl);
    rootCtrl.$inject = ['$scope', '$location', 'membershipService', '$rootScope'];

    function rootCtrl($scope, $location, membershipService, $rootScope) {
        $scope.userData = {};
        $scope.userData.displayUserInfo = displayUserInfo;
        $scope.logout = logout;
        $scope.userData.displayUserInfo();

        function displayUserInfo() {
            $scope.userData.isUserLoggedIn = membershipService.isUserLoggedIn();
            if ($scope.userData.isUserLoggedIn) {
                $scope.username = $rootScope.repository.loggedUser.username;
                membershipService.isAdmin({email : $scope.username}, isAdmin)    
            }
        }

        function isAdmin() {
            $scope.isAdmin = true;
        }

        function logout() {
            membershipService.removeCredentials();
            $location.path('#/');
            $scope.userData.displayUserInfo();
        }
    }


})(angular.module('onlineMarket'));