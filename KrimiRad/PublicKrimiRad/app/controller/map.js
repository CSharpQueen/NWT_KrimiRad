angular.module('app').controller('MapCtrl', ["$scope", "NgMap", "prijavaService", "$rootScope", function ($scope, NgMap, prijavaService, $rootScope) {
    
    $rootScope.loading = true;
    NgMap.getMap().then(function (map) {
        prijavaService.getAll().success(function (data) {
            $scope.map = map;
            $scope.prijave = data;
        }).finally(function (data) {
            //kraj loadinga
            $rootScope.loading = false;
        });        
    });

    $scope.showDetail = function (event, pr) {        
        $scope.prijava = $scope.prijave[this.id];        
        $scope.map.showInfoWindow('foo-iw', this);
    };
}]);
