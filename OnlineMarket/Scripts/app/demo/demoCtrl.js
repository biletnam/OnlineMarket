(function (app) {
    'use strict';

    app.controller('demoCtrl', demoCtrl);

    demoCtrl.$inject = ['$scope', 'apiService', 'indexedDbService', '$timeout', "$rootScope"];

    function demoCtrl($scope, apiService, indexedDbService, $timeout, $rootScope) {
        $scope.resourcesToSell = [];

        $scope.buyResource = buyResource;
        $scope.sellResource = sellResource;
        $scope.quantity = 1;
        $scope.balance = 10000;
        $scope.mistake = "";

        getResources();

        function getResources() {
            apiService.get('/api/operations', null,
            resourcesLoadComplete,
            loadFailed);
        }

        function getBalance(deals) {
            $timeout(function () {
                for (var i in deals) {
                    $scope.balance = deals[i].purchase ? $scope.balance - deals[i].amount : $scope.balance + deals[i].amount;
                }
            }) 
        }

        function getNewPrices() {
            if ($scope.userData.username != null) {
                apiService.get('/api/operations/sendcurrentprices', null,
                pricesLoadComplete,
                null);
            }
        }

        function pricesLoadComplete(result) {
            if (!(result.data.success)) {
                alert(result.data.message);
            }
        }

        function resourcesLoadComplete(result) {
            if (result.data.success) {
                $rootScope.resourcesToBuy = result.data.resources;
                indexedDbService.init.then(function() {
                        indexedDbService.get(checkResults, loadFailed);
                        indexedDbService.getDeals(getBalance, loadFailed);
                    },
                    loadFailed);
                getNewPrices();
            } else {
                alert(result.data.message);
            }
           
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

        function loadFailed(result) {
            $timeout(function () {
                alert("Something is wrong, please, try later.");
            })
        }

        function initializeResourcesToSell() {
            for (var i in $rootScope.resourcesToBuy) {
                $scope.resourcesToSell.push({ title: $rootScope.resourcesToBuy[i].Title, quantity: 0 });
            }
        }

        function buyResource(title, quantity, price) {
            indexedDbService.deal({ title: title, quantity: quantity, amount: quantity * price, purchase: true }, buyResourceComplete, loadFailed)
        }

        function sellResource(title, quantity) {
            var price = $rootScope.resourcesToBuy[indexOf($rootScope.resourcesToBuy, title)].Price;
            indexedDbService.deal({ title: title, quantity: quantity, amount: quantity * price, purchase: false }, sellResourceComplete, loadFailed)
        }

        function buyResourceComplete(deal) {
            $timeout(function () {
                $scope.balance -= deal.amount;
                indexedDbService.addResource({ title: deal.title, quantity: deal.quantity }, loadFailed);
                $scope.resourcesToSell[indexOf($scope.resourcesToSell, deal.title)].quantity += deal.quantity;
            });
        }

        function sellResourceComplete(deal) {
            $timeout(function () {
                $scope.balance += deal.amount;
                indexedDbService.removeResource({ title: deal.title, quantity: deal.quantity }, loadFailed);
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