angular.module('KrimiRad.PregledPrijava', ['ngMap', 'ngAnimate', 'ui.bootstrap'])
.controller('PregledMapeCtrl', ['$scope', "$rootScope", "$location", 'NgMap', 'prijavaService', 'newsFeedService', function ($scope, $rootScope, $location, NgMap, prijavaService, newsFeedService) {
    $rootScope.loading = true;
    $scope.prijava = ''
    $scope.newsFeed = ''
    $scope.currentPage = 1;


    newsFeedService.getPage($scope.currentPage).success(function (data) {
        $scope.newsFeed = data.prijave;
        $scope.totalItems = data.count;        
    }).error(function (data) {
        console.log(data);
    });

    NgMap.getMap().then(function (map) {
        prijavaService.getAll().success(function (data) {
            $scope.prijave = data;
        }).finally(function (data) {
            //kraj loadinga
            $rootScope.loading = false;
        });

    });
    
    $scope.pageChanged = function() {
        newsFeedService.getPage($scope.currentPage).success(function (data) {
        $scope.newsFeed = data.prijave;
        $scope.totalItems = data.count;
        }).error(function (data) {
            console.log(data);
        });
    }

  

    $scope.otvoriPrijavu = function (event) {
        $location.path("/prijave/" + this.id);
    }

}]);


angular.module('KrimiRad.PregledPrijava')
.controller('PrijavaDetalji', ['$scope', "$rootScope", 'NgMap', 'prijavaService', "$route", 'tipDjelaService', function ($scope, $rootScope, NgMap, prijavaService, $route, tipDjelaService) {
    $rootScope.loading = true;
    $scope.prijava = '';    
    $scope.poruka = '';

    prijavaService.getById($route.current.params.prijavaId).success(function (data) {
        $scope.prijava = data;        

        //tipDjelaService.getById($scope.prijava.TipDjelaId).success(function (data) {
        //    $scope.prijava.TipDjela = data;
        //})
    }).finally(function (data) {
        $rootScope.loading = false;
    })

    $scope.rijesi = function() {
        $rootScope.loading = true;
        prijavaService.rijesi($scope.prijava.ID).success(function (data) {            
            console.log(data);
            $scope.poruka = data.poruka;
        }).error(function (data) {
            console.log(data);
            $scope.poruka = data;
        }).finally(function (data) {
            console.log(data);
            $rootScope.loading = false;
        });
    }

    
    $scope.sakrijAlert = function() {
            $scope.poruka = '';
    }

}]);

