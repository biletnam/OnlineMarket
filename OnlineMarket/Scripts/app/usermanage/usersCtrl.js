(function (app) {
    'use strict';

    app.controller('usersCtrl', usersCtrl);

    usersCtrl.$inject = ['$scope', 'apiService'];

    function usersCtrl($scope, apiService) {
        $scope.changeRole = changeRole;
        $scope.pageSize = 5;
        $scope.currentPage = 1;
        getUsers();

        function getUsers() {
            apiService.get('/api/usermanager', null,
            usersLoadComplete,
            usersLoadFailed);
        }

        function usersLoadComplete(result) {
            $scope.users = result.data;
        }

        function usersLoadFailed() {

        }

        function changeRole(userId, role) {
            apiService.post('/api/usermanager', angular.toJson({ Id: userId, RoleId: role}),
            changingComplete,
            changingFailed);
        }

        function changingComplete() {
            location.reload();
        }

        function changingFailed() {

        }
    }


})(angular.module('onlineMarket'));