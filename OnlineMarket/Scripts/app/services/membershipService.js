(function (app) {
    'use strict';

    app.factory('membershipService', membershipService);

    membershipService.$inject = ['apiService', '$http', '$base64', '$cookieStore', '$rootScope'];

    function membershipService(apiService, $http, $base64, $cookieStore, $rootScope) {

        var service = {
            login: login,
            register: register,
            saveCredentials: saveCredentials,
            removeCredentials: removeCredentials,
            isUserLoggedIn: isUserLoggedIn,
            isAdmin: isAdmin
        }

        function login(user, completed) {
            apiService.post('/api/account/login', user,
            completed,
            null);
        }

        function register(user, completed) {
            apiService.post('/api/account/register', user,
            completed,
            null);
        }

        function saveCredentials(user) {
            var membershipData = $base64.encode(user.email + ':' + user.password);

            $rootScope.repository = {
                loggedUser: {
                    username: user.email,
                    authdata: membershipData
                }
            };

            $http.defaults.headers.common['Authorization'] = 'Basic ' + membershipData;
            $cookieStore.put('repository', $rootScope.repository);
        }

        function removeCredentials() {
            $rootScope.repository = {};
            $cookieStore.remove('repository');
            $http.defaults.headers.common.Authorization = '';
        };

        function loginFailed(response) {
            notificationService.displayError(response.data);
        }

        function registrationFailed(response) {

            notificationService.displayError('Registration failed. Try again.');
        }

        function isUserLoggedIn() {
            return $rootScope.repository.loggedUser != null;
        }

        function isAdmin(user, completed) {
            apiService.get('/api/account', user,
                completed,
                null)
        }

        return service;
    }
})(angular.module('onlineMarket'));