﻿(function() {
    "use strict";

    angular.module("onlineMarket", ["ngRoute", "ngCookies", "base64", "angularUtils.directives.dirPagination"])
        .config(config)
        .run(run);

    config.$inject = ["$routeProvider", "$locationProvider"];

    function config($routeProvider, $locationProvider) {
        $locationProvider.html5Mode(true);
        $routeProvider
            .when("/", {
                templateUrl: "scripts/app/home/index.html",
                controller: "indexCtrl"
            })
            .when("/operations", {
                templateUrl: "scripts/app/home/index.html",
                controller: "indexCtrl"
            })
            .when("/archive", {
                templateUrl: "scripts/app/home/archive.html",
                controller: "archiveCtrl"
            })
            .when("/users", {
                templateUrl: "scripts/app/usermanage/users.html",
                controller: "usersCtrl"
            })
            .when("/login", {
                templateUrl: "scripts/app/account/login.html",
                controller: "loginCtrl"
            })
            .when("/register", {
                templateUrl: "scripts/app/account/register.html",
                controller: "registerCtrl"
            })
            .when("/demo", {
                templateUrl: "scripts/app/demo/demo.html",
                controller: "demoCtrl"
            })
            .when("/error", {
                templateUrl: "scripts/app/error/notfound.html",
                controller: "notfoundCtrl"
            })
            .when("/checkEmail", {
                templateUrl: "scripts/app/account/checkEmail.html",
                controller: "checkEmailCtrl"
            })
            .when("/confirm/:email/:code", {
                templateUrl: "scripts/app/account/confirm.html",
                controller: "confirmCtrl"
            })
            .otherwise({ redirectTo: "/error" });
    }

    run.$inject = ["$rootScope", "$location", "$cookieStore", "$http"];

    function run($rootScope, $location, $cookieStore, $http) {
        $rootScope.repository = $cookieStore.get("repository") || {};
        if ($rootScope.repository.loggedUser) {
            $http.defaults.headers.common["Authorization"] = $rootScope.repository.loggedUser.authdata;
        }
    }

    isAuthenticated.$inject = ["membershipService", "$rootScope", "$location"];

    function isAuthenticated(membershipService, $rootScope, $location) {
        if (!membershipService.isUserLoggedIn()) {
            $rootScope.previousState = $location.path();
            $location.path("/login");
        }
    }

})();