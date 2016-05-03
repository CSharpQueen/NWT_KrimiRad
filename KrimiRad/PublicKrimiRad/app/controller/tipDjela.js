/// <reference path="C:\OneDrive\GitHub\NWT_KrimiRad\KrimiRad\KrimiRad\Scripts/angular.js" />

angular.module('app').controller('TipDjelaCtrl', ['$scope', 'tipDjelaService', '$rootScope', function ($scope, tipDjelaService, $rootScope) {
    $scope.poruka = '';
    $scope.tipDjela = '';

    //pocetak loadinga
    $rootScope.loading = true;


    tipDjelaService.getAll().success(function (data) {
        $scope.tipoviDjela = data;
    }).finally(function (data) {
        //kraj loadinga
        $rootScope.loading = false;
    });

        
}]);