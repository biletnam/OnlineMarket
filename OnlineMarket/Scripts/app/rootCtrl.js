(function (app) {
    'use strict';

    app.controller('rootCtrl', rootCtrl);
    rootCtrl.$inject = ['$scope', '$location', 'membershipService', '$rootScope', 'apiService'];

    function rootCtrl($scope, $location, membershipService, $rootScope, apiService) {
        $scope.userData = {};
        $scope.userData.displayUserInfo = displayUserInfo;
        $scope.logout = logout;
        $scope.userData.displayUserInfo();
        $scope.userData.isAdmin = false;
        getRecentActivities();

        function displayUserInfo() {
            $scope.userData.isUserLoggedIn = membershipService.isUserLoggedIn();
            if ($scope.userData.isUserLoggedIn) {
                $scope.userData.username = $rootScope.repository.loggedUser.username;
                membershipService.isUserAdmin({ email: $scope.userData.username }, userIsAdmin)
            }
        }

        function userIsAdmin(result) {
            if (result.data.success) {
                $scope.userData.isAdmin = result.data.isAdmin;
            } else {
                alert(result.data.message);
            }
        }

        function getRecentActivities() {
            apiService.get("/api/archive/getactivities", null,
                activitiesLoadComplete,
                activitiesLoadFailed);
        }

        function activitiesLoadComplete(result) {
            if (result.data.success) {
                $scope.activities = result.data.activities;
            } else {
                alert(result.data.message);
            }
        }

        function activitiesLoadFailed() {
            alert("Something went wrong");
        }

        function logout() {
            membershipService.removeCredentials();
            $location.path('#/');
            $scope.userData.displayUserInfo();
        }
    }


})(angular.module('onlineMarket'));