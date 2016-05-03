/// <reference path="C:\OneDrive\GitHub\NWT_KrimiRad\KrimiRad\KrimiRad\Scripts/angular.js" />

angular.module('app').controller('CreatePrijavaCtrl', ['$scope', 'prijavaService', '$rootScope', function ($scope, prijavaService, $rootScope) {
    $scope.poruka = '';
    $scope.prijava = '';
    $rootScope.loading = true;
    prijavaService.getAllTipDjela().success(function (data) {
        $scope.tipoviDjela = data;
    })
    .finally(function(data) {
        $rootScope.loading = true;
    });
    $("#myModal").modal("show");
    //pocetak loadinga
 
   
    $scope.dodajPrijavu = function () {
            $rootScope.loading = true;
            prijavaService.create($scope.prijava).success(function (data) {                
                $scope.prijave.push($scope.prijava);
                $scope.poruka = data.poruka;
            })
            .finally(function (data) {
                $rootScope.loading = false;
            });
        }
        
}]);