angular.module('app').controller('PregledMapeCtrl', ["$scope", "NgMap", "prijavaService", "$rootScope", function ($scope, NgMap, prijavaService, $rootScope) {
    $rootScope.loading = true;
    NgMap.getMap().then(function (map) {
        prijavaService.getAll().success(function (data) {
            $scope.prijave = data;
        }).finally(function (data) {
            //kraj loadinga
            $rootScope.loading = false;
        });

    });
}]);
