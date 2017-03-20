(function (app) {
    "use strict";

    app.controller("confirmCtrl", confirmCtrl);

    confirmCtrl.$inject = ["$scope", "apiService", "$routeParams"];

    function confirmCtrl($scope, apiService, $routeParams) {
        $scope.pageSize = 5;
        $scope.currentPage = 1;
        confirm();

        function confirm() {
            apiService.get("/api/account/confirmemail", { email: $routeParams.email, code: $routeParams.code },
                confirmComplete,
                confirmFailed);
        }

        function confirmComplete(result) {
            if (result.data.success) {
                $scope.isConfirmed = true;
            } else {
                $scope.isConfirmed = false;
            }
        }

        function confirmFailed() {
            alert("Something went wrong.");
        }
    }

})(angular.module("onlineMarket"));