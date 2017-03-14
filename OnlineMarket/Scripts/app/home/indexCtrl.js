(function (app) {
    'use strict';

    app.controller('indexCtrl', indexCtrl);

    indexCtrl.$inject = ['$scope', 'apiService', "$timeout"];

    function indexCtrl($scope, apiService, $timeout) {
        $scope.buyResource = buyResource;
        $scope.sellResource = sellResource;
        $scope.refillBalance = refillBalance;
        $scope.quantity = 1;
        $scope.amount = 10;
        getResources();

        function getResources() {
            if ($scope.userData.username != null) {
                apiService.get('/api/operations', { email: $scope.userData.username },
                resourcesLoadComplete,
                loadFailed);
            }
        }

        function resourcesLoadComplete(result) {
            if (result.data.success) {
                $scope.resources = result.data.operations;
                profitSum();
            } else {
                alert(result.data.message);
            }
        }

        function loadFailed() {
            alert("Something went wrong.");
        }

        function buyResource(resourceId, quantity, price) {
            apiService.post('/api/deal', { Email: $scope.userData.username, ResourceId: resourceId, Quantity: quantity, Price: price, IsPurchase: true },
                dealComplete,
                dealFailed);

        }

        function sellResource(resourceId, quantity, price) {
            apiService.post('/api/deal', { Email: $scope.userData.username, ResourceId: resourceId, Quantity: quantity, Price: price, IsPurchase: false },
                dealComplete,
                dealFailed);
            }

        function refillBalance(amount) {
            apiService.post('/api/account/refillbalance', { Amount: amount, Email: $scope.userData.username },
                dealComplete,
                loadFailed);
        }

        function dealComplete(result) {
            if (result.data.success) {
                if (result.data.add) {
                    $scope.resources.Balance += result.data.amount;
                    if (result.data.id != null) {
                        $scope.profitSum += result.data.amount;
                        $scope.resources.Profit[result.data.id - 1] += result.data.amount;
                        $scope.resources.ResourcesToSell[result.data.id-1].Quantity -= result.data.quantity;
                    }
                } else {
                    $scope.resources.Balance -= result.data.amount;
                    if (result.data.id != null) {
                        $scope.profitSum -= result.data.amount;
                        $scope.resources.Profit[result.data.id - 1] -= result.data.amount;
                        $scope.resources.ResourcesToSell[result.data.id-1].Quantity += result.data.quantity;
                    }
                }
            } else {
                alert(result.data.message);
            } 
        }

        function profitSum() {
            $scope.profitSum = 0;
            var profits = $scope.resources.Profit;
            for (var i in profits) {
                $scope.profitSum += profits[i];
            }
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