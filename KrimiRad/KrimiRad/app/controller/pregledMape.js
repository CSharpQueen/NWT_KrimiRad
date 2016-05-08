angular.module('KrimiRad.PregledPrijava', ['ngMap'])
.controller('PregledMapeCtrl', ['$scope',"$rootScope", 'NgMap', 'prijavaService', '$rootScope',function ($scope,$rootScope, NgMap, prijavaService) {
    $rootScope.loading = true;
    NgMap.getMap().then(function (map) {
        prijavaService.getAll().success(function (data) {
            $scope.prijave = data;
        }).finally(function (data) {
            //kraj loadinga
            $rootScope.loading = false;
        });

    });
    $scope.otvoriPrijavu = function (event) {
        $rootScope.loading = true;

       var v=prijavaService.getById(this.id);
       alert(v);
        console.log(v);

        prijavaService.getById(id).success(function (data) {

            $scope.prijava = data;
            

        }).finally(function (data) {
            $rootScope.loading = false;
        })
           
        $("#prijaveModal").modal("show");
    }
    
}]);
