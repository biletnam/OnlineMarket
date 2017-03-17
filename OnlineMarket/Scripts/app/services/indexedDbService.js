(function (app) {
    "use strict";

    app.factory("indexedDbService", indexedDbService);

    function indexedDbService() {
        var db = null;

        var init = new Promise(function (resolve, reject) {
            var request = window.indexedDB.open("onlineMarket2", 2);
            request.onerror = reject;
            request.onupgradeneeded = function (event) {
                db = event.target.result;
                db.createObjectStore("deals", { keyPath: "id", autoIncrement: true });
                db.createObjectStore("userResources", { keyPath: "title"});
            };
            request.onsuccess = function () {
                db = request.result;
                resolve();
            }
        });

        function get(resolve, loadFailed) {
            var results;
            var tr = db.transaction("userResources", "readwrite");
            var store = tr.objectStore("userResources");
            var request = store.getAll();
            request.onsuccess = function () {
                results = request.result;
                resolve(results);
            };
            request.onerror = loadFailed;
        }

        function addResource(resource, loadFailed) {
            var tr = db.transaction("userResources", "readwrite");
            var store = tr.objectStore("userResources");
            var request = store.get(resource.title);
            request.onsuccess = function () {
                var oldresource = request.result;
                var newresource = { title: resource.title, quantity: oldresource.quantity + resource.quantity }
                var tr = db.transaction("userResources", "readwrite");
                var store = tr.objectStore("userResources");
                store.put(newresource).onerror = loadFailed;
            }
            request.onerror = loadFailed;
        }

        function removeResource(resource, loadFailed) {
            var tr = db.transaction("userResources", "readwrite");
            var store = tr.objectStore("userResources");
            var request = store.get(resource.title);
            request.onsuccess = function () {
                var oldresource = request.result;
                var newresource = { title: resource.title, quantity: oldresource.quantity - resource.quantity }
                var tr = db.transaction("userResources", "readwrite");
                var store = tr.objectStore("userResources");
                store.put(newresource).onerror = loadFailed;
            }
            request.onerror = loadFailed;
        }

        function addMultiple(resources) {
            for (var i = 0; i < resources.length; i++) {
                var tr = db.transaction("userResources", "readwrite");
                var store = tr.objectStore("userResources");
                store.add(resources[i]);
            }
        }

        function deal(deal, dealLoadComplete, loadFailed) {
            if (db != null) {
                var tr = db.transaction(["deals"], "readwrite");
                var store = tr.objectStore("deals");
                var request = store.add(deal);
                request.onsuccess = function () {
                    dealLoadComplete(deal);
                }
                request.onerror = function () {
                    loadFailed();
                }
            }
        }

        function getDeals(allDealsLoadComplete, loadFailed) {
            var tr = db.transaction("deals", "readwrite");
            var store = tr.objectStore("deals");
            var request = store.getAll();
            request.onsuccess = function () {
                allDealsLoadComplete(request.result);
            }
            request.onerror = function () {
                loadFailed();
            }
        }

        var service = {
            init: init,
            addResource: addResource,
            removeResource: removeResource,
            addMultiple: addMultiple,
            get: get,
            getDeals: getDeals,
            deal: deal
        };

        return service;
    }
})(angular.module("onlineMarket"));