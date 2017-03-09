(function (app) {
    'use strict';

    app.controller('indexCtrl', indexCtrl);

    indexCtrl.$inject = ['$scope', 'apiService'];

    function indexCtrl($scope, apiService) {

        getResourcesToBuy();
        function getResourcesToBuy() {
            apiService.get('/api/operations/operations', { email: $scope.username },
            resourcesToBuyLoadComplete,
            resourcesToBuyLoadFailed);
        }

        function resourcesToBuyLoadComplete(result) {
            $scope.resourcesToBuy = result.data;
        }

        function resourcesToBuyLoadFailed() {

        }
    }
})(angular.module('onlineMarket'));