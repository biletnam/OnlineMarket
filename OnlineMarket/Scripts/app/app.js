(function () {
    'use strict';

    angular.module('onlineMarket', ['ngRoute', 'ngCookies', 'base64'])
        .config(config)
        .run(run)

    config.$inject = ['$routeProvider', '$locationProvider'];

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
            .otherwise({ redirectTo: "/" });
    }

    run.$inject = ['$rootScope', '$location', '$cookieStore', '$http'];

    function run($rootScope, $location, $cookieStore, $http) {
        $rootScope.repository = $cookieStore.get('repository') || {};
        if ($rootScope.repository.loggedUser) {
            $http.defaults.headers.common['Authorization'] = $rootScope.repository.loggedUser.authdata;
        }
    }

    isAuthenticated.$inject = ['membershipService', '$rootScope','$location'];
     
    function isAuthenticated(membershipService, $rootScope, $location) {
        if (!membershipService.isUserLoggedIn()) {
            $rootScope.previousState = $location.path();
            $location.path('/login');
        }
    }

})();