(function (app) {
    'use strict';

    app.controller('demoCtrl', demoCtrl);

    demoCtrl.$inject = ['$scope', 'apiService', 'indexedDbService', '$timeout'];

    function demoCtrl($scope, apiService, indexedDbService, $timeout) {
        $scope.resourcesToSell = [];

        $scope.buyResource = buyResource;
        $scope.sellResource = sellResource;
        $scope.quantity = 1;
        $scope.balance = 10000;

        getResources();


        function getResources() {
            apiService.get('/api/operations', null,
            resourcesLoadComplete,
            resourcesLoadFailed);
        }

        function resourcesLoadComplete(result) {
            $scope.resourcesToBuy = result.data;
            indexedDbService.init.then(function () {
                indexedDbService.get(checkResults);
            })
        }

        function checkResults(results) {
            $timeout(function () {
                if (results.length == 0) {
                    initializeResourcesToSell();
                    indexedDbService.addMultiple($scope.resourcesToSell);
                } else {
                    $scope.resourcesToSell = results;
                }
            })
        }

        function resourcesLoadFailed(result) {

        }

        function initializeResourcesToSell() {
            for (var i in $scope.resourcesToBuy) {
                $scope.resourcesToSell.push({ title: $scope.resourcesToBuy[i].Title, quantity: 0 });
            }
        }

        function buyResource(title, quantity, price) {
            indexedDbService.deal({ title: title, quantity: quantity, amount: quantity * price, purchase: true }, buyResourceComplete)
        }

        function sellResource(title, quantity) {
            var price = $scope.resourcesToBuy[indexOf($scope.resourcesToBuy, title)].Price;
            indexedDbService.deal({ title: title, quantity: quantity, amount: quantity * price, purchase: false }, sellResourceComplete)
        }

        function buyResourceComplete(deal) {
            $timeout(function () {
                $scope.balance -= deal.amount;
                indexedDbService.addResource({ title: deal.title, quantity: deal.quantity });
                $scope.resourcesToSell[indexOf($scope.resourcesToSell, deal.title)].quantity += deal.quantity;
            });
        }

        function sellResourceComplete(deal) {
            $timeout(function () {
                $scope.balance += deal.amount;
                indexedDbService.removeResource({ title: deal.title, quantity: deal.quantity });
                $scope.resourcesToSell[indexOf($scope.resourcesToSell, deal.title)].quantity -= deal.quantity;
            })
        }

        function indexOf(resources, title) {
            for (var i = 0; i < resources.length; i++) {
                if (resources[i].Title == title || resources[i].title == title) {
                    return i;
                }
            }
        }

    }

})(angular.module('onlineMarket'));