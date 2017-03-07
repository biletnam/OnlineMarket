(function (app) {
    'use strict';

    app.controller('indexCtrl', indexCtrl);

    indexCtrl.$inject = ['$scope'];

    function indexCtrl($scope) {
        $scope.title = "Hello, visitor!";
    }


})(angular.module('onlineMarket'));