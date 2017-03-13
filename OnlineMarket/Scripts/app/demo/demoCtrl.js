(function (app) {
    'use strict';

    app.controller('demoCtrl', demoCtrl);

    demoCtrl.$inject = ['$scope', 'apiService', '$timeout'];

    function demoCtrl($scope, apiService, $timeout) {
        $scope.resourcesToBuy = [{ title: 'Wood', price: 14.5 }, { title: "Iron", price: 18.5 }, { title: "Oil", price: 60.8 }];
        $scope.resourcesToSell = [];
        $scope.buyResource = buyResource;
        $scope.quantity = 1;

        var db = null;
        const dbName = "onlineMarket";

        initialize();

        function initialize() {
            $scope.balance = 10000;
            var request = indexedDB.open(dbName, 3);
            request.onerror = function (event) {
            };
            request.onupgradeneeded = function (event) {
                db = event.target.result;

                db.createObjectStore("deals", { keyPath: "id", autoIncrement: true });

                db.createObjectStore("userResources", { keyPath: "id" });
            };
            request.onsuccess = function (event) {
                db = event.target.result;
            }
        }

        function buyResource(title, quantity, price) {
            if (db == null) {
                alert("There is no database");
            } else {
                var deal = { title: title, quantity: quantity, amount: quantity * price };
                var tr = db.transaction(["deals"], "readwrite");
                var store = tr.objectStore("deals");
                var request = store.add(deal);
                request.onsuccess = function (event) {
                    refreshList(deal);
                }
            }
        }

        function refreshList(deal) {
            $timeout(function () {
                $scope.balance -= deal.amount;
            });
        }
        
    }

})(angular.module('onlineMarket'));