/// <reference path="C:\OneDrive\GitHub\NWT_KrimiRad\KrimiRad\KrimiRad\Scripts/angular.js" />

angular.module('app').controller('PrijavaCtrl', ['$scope', 'prijavaService', '$rootScope', function ($scope, prijavaService, $rootScope) {
    $scope.poruka = '';
    $scope.prijava = '';

    //pocetak loadinga
    $rootScope.loading = true;


    prijavaService.getAll().success(function (data) {
        $scope.prijave = data;
    }).finally(function (data) {
        //kraj loadinga
        $rootScope.loading = false;
    });

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