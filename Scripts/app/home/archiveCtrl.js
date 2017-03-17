(function (app) {
    'use strict';

    app.controller('archiveCtrl', archiveCtrl);

    archiveCtrl.$inject = ['$scope', "$rootScope", 'apiService'];

    function archiveCtrl($scope, $rootScope, apiService) {
        $scope.pageSize = 5;
        $scope.currentPage = 1;
        getarchive();

        function getarchive() {
            apiService.get('/api/archive/getarchive', { email: $scope.userData.username },
            archiveLoadComplete,
            archiveLoadFailed);
        }

        function archiveLoadComplete(result) {
            if (result.data.success) {
                $rootScope.deals = result.data.archive;
            } else {
                alert(result.data.message);
            }
        }

        function archiveLoadFailed() {
            alert("Something went wrong.");
        }
    }

})(angular.module('onlineMarket'));