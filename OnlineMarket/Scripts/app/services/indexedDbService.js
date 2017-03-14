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
                db.createObjectStore("userResources", { keyPath: "title"});
            };
            request.onsuccess = function (event) {
                db = request.result;
                resolve();
            }
        });

        function get(resolve) {
            var results;
            var tr = db.transaction("userResources", "readwrite");
            var store = tr.objectStore("userResources");
            var request = store.getAll();
            request.onsuccess = function (event) {
                results = request.result;
                resolve(results);
            };
        }

        function addResource(resource) {
            var tr = db.transaction("userResources", "readwrite");
            var store = tr.objectStore("userResources");
            var request = store.get(resource.title);
            request.onsuccess = function () {
                var oldresource = request.result;
                var newresource = { title: resource.title, quantity: oldresource.quantity + resource.quantity }
                var tr = db.transaction("userResources", "readwrite");
                var store = tr.objectStore("userResources");
                store.put(newresource);
            }
        }

        function removeResource(resource) {
            var tr = db.transaction("userResources", "readwrite");
            var store = tr.objectStore("userResources");
            var request = store.get(resource.title);
            request.onsuccess = function () {
                var oldresource = request.result;
                var newresource = { title: resource.title, quantity: oldresource.quantity - resource.quantity }
                var tr = db.transaction("userResources", "readwrite");
                var store = tr.objectStore("userResources");
                store.put(newresource);
            }
        }

        function addMultiple(resources) {
            for (var i = 0; i < resources.length; i++) {
                var tr = db.transaction("userResources", "readwrite");
                var store = tr.objectStore("userResources");
                store.add(resources[i]);
            }

        }

        function deal(deal, dealLoadComplete) {
            if (db != null) {
                var tr = db.transaction(["deals"], "readwrite");
                var store = tr.objectStore("deals");
                var request = store.add(deal);
                request.onsuccess = function (event) {
                    dealLoadComplete(deal);
                }
            }
        }

        var service = {
            init: init,
            addResource: addResource,
            removeResource: removeResource,
            addMultiple: addMultiple,
            get: get,
            deal: deal,
        };

        return service;
    }
})(angular.module('onlineMarket'));