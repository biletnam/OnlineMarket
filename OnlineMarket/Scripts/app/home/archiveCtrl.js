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
            $scope.deals = result.data;
        }

        function archiveLoadFailed() {

        }
    }


})(angular.module('onlineMarket'));