angular.module('KrimiRad.PregledPrijava', ['ngMap'])
.controller('PregledMapeCtrl', ['$scope',"$rootScope", "$location",'NgMap', 'prijavaService', '$rootScope',function ($scope,$rootScope,$location, NgMap, prijavaService) {
    $rootScope.loading = true;
    $scope.prijava = ''
    NgMap.getMap().then(function (map) {
        prijavaService.getAll().success(function (data) {
            $scope.prijave = data;
        }).finally(function (data) {
            //kraj loadinga
            $rootScope.loading = false;
        });

    });

    $scope.otvoriPrijavu = function (event) {
        $location.path("/prijave/" + this.id);        
    }
    
}]);


angular.module('KrimiRad.PregledPrijava')
.controller('PrijavaDetalji', ['$scope', "$rootScope", 'NgMap', 'prijavaService', "$route", 'tipDjelaService', function ($scope, $rootScope, NgMap, prijavaService, $route, tipDjelaService) {
    $rootScope.loading = true;
    $scope.prijava = '';
    $scope.slike = '';
    prijavaService.getById($route.current.params.prijavaId).success(function (data) {
        $scope.prijava = data;
        $scope.slike = data.Album.Medij;

        tipDjelaService.getById($scope.prijava.TipDjelaId).success(function (data) {
            $scope.prijava.TipDjela = data;
        })
    }).finally(function (data) {
        $rootScope.loading = false;
    })

}]);

