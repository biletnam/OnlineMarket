(function (app) {
    'use strict';

    app.factory('indexedDbService', indexedDbService);

    indexedDbService.$inject = ['apiService', '$timeout'];

    function indexedDbService(apiService, $timeout) {


        var db = null;

        var init = new Promise(function (resolve, reject) {
            var request = window.indexedDB.open("onlineMarket2", 2);
            request.onerror = function (event) {

            };
            request.onupgradeneeded = function (event) {
                db = event.target.result;
                db.createObjectStore("deals", { keyPath: "id", autoIncrement: true });
                db.createObjectStore("userResources", { keyPath: "id", autoIncrement: true });
            };
            request.onsuccess = function (event) {
                db = request.result;
            }
            if (db != null) resolve();
        });

        function get() {
            var results;
            var tr = db.transaction("userResources", "readwrite");
            var store = tr.objectStore("userResources");
            var request = store.getAll();
            request.onsuccess = function (event) {
                results = request.result;
            }
            return results;
        }

        function addMultiple(resources) {
            for (var i = 0; i < resources.length; i++) {
                var tr = db.transaction("userResources", "readwrite");
                var store = tr.objectStore("userResources");
                store.put(resources[i]);
                }
            
        }


        function buy(deal, buyLoadComplete) {
            if (db != null) {
                var tr = db.transaction(["deals"], "readwrite");
                var store = tr.objectStore("deals");
                var request = store.add(deal);
                request.onsuccess = function (event) {
                    buyLoadComplete(deal);
                }
            }
        }

        var service = {
            init: init,
            addMultiple: addMultiple,
            get: get,
            buy: buy,
            //sell: sell
        };

        return service;
    }
})(angular.module('onlineMarket'));