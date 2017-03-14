(function (app) {
    'use strict';

    app.controller('archiveCtrl', archiveCtrl);

    archiveCtrl.$inject = ['$scope', 'apiService'];

    function archiveCtrl($scope, apiService) {
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
                $scope.deals = result.data.archive;
            } else {
                alert(result.data.message);
            }
        }

        function archiveLoadFailed() {
            alert("Something went wrong.");
        }
    }

})(angular.module('onlineMarket'));