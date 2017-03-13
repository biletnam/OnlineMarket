(function (app) {
    'use strict';

    app.controller('rootCtrl', rootCtrl);
    rootCtrl.$inject = ['$scope', '$location', 'membershipService', '$rootScope', 'apiService'];

    function rootCtrl($scope, $location, membershipService, $rootScope, apiService) {
        $scope.userData = {};
        $scope.userData.displayUserInfo = displayUserInfo;
        $scope.logout = logout;
        $scope.userData.displayUserInfo();
        $scope.isAdmin = false;
        getRecentActivities();

        function displayUserInfo() {
            $scope.userData.isUserLoggedIn = membershipService.isUserLoggedIn();
            if ($scope.userData.isUserLoggedIn) {
                $scope.username = $rootScope.repository.loggedUser.username;
                membershipService.isUserAdmin({email : $scope.username}, isUserAdmin)    
            }
        }

        function isUserAdmin(result) {
            $scope.isAdmin = result.data;
        }

        function getRecentActivities() {
            apiService.get("/api/archive/getactivities", null,
                activitiesLoadComplete);
        }

        function activitiesLoadComplete(result) {
            $scope.activities = result.data;
        }

        function logout() {
            membershipService.removeCredentials();
            $location.path('#/');
            $scope.userData.displayUserInfo();
        }
    }


})(angular.module('onlineMarket'));