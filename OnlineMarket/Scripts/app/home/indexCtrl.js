(function (app) {
    "use strict";

    app.controller("indexCtrl", indexCtrl);

    indexCtrl.$inject = ["$scope", "$rootScope", "apiService", "$timeout"];

    function indexCtrl($scope, $rootScope, apiService, $timeout) {
        $scope.buyResource = buyResource;
        $scope.sellResource = sellResource;
        $scope.refillBalance = refillBalance;
        $scope.quantity = 1;
        $scope.amount = 10;
        getResources();

        function getResources() {
            if ($scope.userData.username != null) {
                apiService.get("/api/operations", { email: $scope.userData.username },
                resourcesLoadComplete,
                loadFailed);
            }
        }

        function resourcesLoadComplete(result) {
            if (result.data.success) {
                $rootScope.resources = result.data.operations;
                profitSum();
                $timeout(function () {
                    getNewPrices();
                },200)
                
            } else {
                alert(result.data.message);
            }
        }

        function getNewPrices() {
            if ($scope.userData.username != null) {
                apiService.get("/api/operations/sendcurrentprices", null,
                pricesLoadComplete,
                null);
            }
        }

        function pricesLoadComplete(result) {
            if (!(result.data.success)) {
                alert(result.data.message);
            }
        }

        function loadFailed() {
            alert("Something went wrong.");
        }

        function buyResource(resourceId, resourceTitle, quantity, price) {
            apiService.post("/api/deal", { Email: $scope.userData.username, ResourceId: resourceId, ResourceTitle: resourceTitle, Quantity: quantity, Price: price, IsPurchase: true },
                dealComplete,
                loadFailed);

        }

        function sellResource(resourceId, resourceTitle, quantity, price) {
            apiService.post("/api/deal", { Email: $scope.userData.username, ResourceId: resourceId, ResourceTitle: resourceTitle, Quantity: quantity, Price: price, IsPurchase: false },
                dealComplete,
                loadFailed);
            }

        function refillBalance(amount) {
            apiService.post("/api/account/refillbalance", { Amount: amount, Email: $scope.userData.username },
                dealComplete,
                loadFailed);
        }

        function dealComplete(result) {
            if (result.data.success) {
                if (result.data.add) {
                    $rootScope.resources.Balance += result.data.amount;
                    if (result.data.id != null) {
                        $scope.profitSum += result.data.amount;
                        $rootScope.resources.Profit[result.data.id - 1] += result.data.amount;
                        $rootScope.resources.ResourcesToSell[result.data.id - 1].Quantity -= result.data.quantity;
                    }
                } else {
                    $rootScope.resources.Balance -= result.data.amount;
                    if (result.data.id != null) {
                        $scope.profitSum -= result.data.amount;
                        $rootScope.resources.Profit[result.data.id - 1] -= result.data.amount;
                        $rootScope.resources.ResourcesToSell[result.data.id - 1].Quantity += result.data.quantity;
                    }
                }
            } else {
                alert(result.data.message);
            } 
        }

        function profitSum() {
            $scope.profitSum = 0;
            var profits = $rootScope.resources.Profit;
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
})(angular.module("onlineMarket"));