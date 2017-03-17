(function (app) {
    "use strict";

    app.controller("usersCtrl", usersCtrl);

    usersCtrl.$inject = ["$scope", "$rootScope", "apiService"];

    function usersCtrl($scope, $rootScope, apiService) {
        $scope.changeRole = changeRole;
        $scope.pageSize = 5;
        $scope.currentPage = 1;
        getUsers();

        function getUsers() {
            apiService.get("/api/usermanager", null,
            usersLoadComplete,
            loadFailed);
        }

        function usersLoadComplete(result) {
            if (result.data.success) {
                $rootScope.users = result.data.users;
            } else {
                alert(result.data.message);
            }
        }

        function loadFailed() {
            alert("Something went wrong");
        }

        function changeRole(userId, role) {
            apiService.post("/api/usermanager", angular.toJson({ Id: userId, RoleId: role}),
            changingComplete,
            loadFailed);
        }

        function changingComplete(result) {
            if (!result.data.success) {
                alert(result.data.message);
            }
        }
    }


})(angular.module("onlineMarket"));