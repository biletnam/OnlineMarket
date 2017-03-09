(function (app) {
    'use strict';

    app.controller('archiveCtrl', archiveCtrl);

    archiveCtrl.$inject = ['$scope', 'apiService'];

    function archiveCtrl($scope, apiService) {
        getarchive();

        function getarchive() {
            apiService.get('/api/archive/getarchive', { email: $scope.username },
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