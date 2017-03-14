(function (app) {
    'use strict';

    app.controller('demoCtrl', demoCtrl);

    demoCtrl.$inject = ['$scope', 'apiService', 'indexedDbService', '$timeout'];

    function demoCtrl($scope, apiService, indexedDbService, $timeout) {
        $scope.resourcesToSell = [];

        $scope.buyResource = buyResource;
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
                var results = indexedDbService.get();
                if (results.length == 0) {
                    for (var i in $scope.resourcesToBuy) {
                        $scope.resourcesToSell.push({ title: $scope.resourcesToBuy[i].Title, quantity: 0 })
                    }
                    $timeout(function () {
                        indexedDbService.addMultiple($scope.resourcesToSell);
                    })
                    
                } else {
                    $scope.resourcesToSell = results;
                }
            }); 
        }

        function resourcesLoadFailed(result) {

        }

        function initializeResources() {
            for (var i in $scope.resourcesToBuy) {
                $scope.resourcesToSell.push({ title: $scope.resourcesToBuy[i].title, quantity: 0 });
            }
        }

        function buyResource(title, quantity, price) {
            indexedDbService.buy({ title: title, quantity: quantity, amount: quantity * price, purchase: true }, refreshList)
        }

        function refreshList(deal) {
            $timeout(function () {
                $scope.balance -= deal.amount;
                //$scope.resourcesToSell[indexOf($scope.resourcesToSell, deal.title)].quantity += deal.quantity;
            });
        }

        function indexOf(resources, title) {
            for (var i = 0; i < resources.length; i++) {
                if (resources[i].title == title) {
                    return i;
                }
            }
        }

    }

})(angular.module('onlineMarket'));