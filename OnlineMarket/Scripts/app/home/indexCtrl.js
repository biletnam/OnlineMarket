(function (app) {
    'use strict';

    app.controller('indexCtrl', indexCtrl);

    indexCtrl.$inject = ['$scope', 'apiService'];

    function indexCtrl($scope, apiService) {
        $scope.buyResource = buyResource;
        $scope.sellResource = sellResource;
        $scope.quantity = 1;
        getResources();

        function getResources() {
            apiService.get('/api/operations', { email: $scope.username },
            resourcesLoadComplete,
            resourcesLoadFailed);
        }

        function resourcesLoadComplete(result) {
            $scope.resources = result.data;
        }

        function resourcesLoadFailed() {

        }

        function buyResource(resourceId, quantity, price) {
            apiService.post('/api/deal', angular.toJson({ Email: $scope.username, ResourceId: resourceId, Quantity: quantity, Price: price, IsPurchase: true } ),
            dealComplete,
            dealFailed);
        }

        function sellResource(resourceId, quantity, price) {
            apiService.post('/api/deal', angular.toJson({ Email: $scope.username, ResourceId: resourceId, Quantity: quantity, Price: price, IsPurchase: false }),
            dealComplete,
            dealFailed);
        }

        function dealComplete() {
            location.reload();
        }

        function dealFailed() {

        }
    }
})(angular.module('onlineMarket'));