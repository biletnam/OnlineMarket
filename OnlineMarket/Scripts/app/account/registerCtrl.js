(function (app) {
    'use strict';

    app.controller('registerCtrl', registerCtrl);

    registerCtrl.$inject = ['$scope'];

    function registerCtrl($scope) {
        $scope.title = 'register here!';
    }

})(angular.module('onlineMarket'));