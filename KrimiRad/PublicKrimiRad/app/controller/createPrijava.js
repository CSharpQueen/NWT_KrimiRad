/// <reference path="C:\OneDrive\GitHub\NWT_KrimiRad\KrimiRad\KrimiRad\Scripts/angular.js" />

angular.module('app').controller('CreatePrijavaCtrl', ['$scope', 'prijavaService', '$rootScope', function ($scope, prijavaService, $rootScope) {
    $scope.poruka = '';
    $scope.prijava = '';

    //za testiranje
    $scope.prijava = {
        Opstina: "Centar",
        Grad: "Sarajevo",
        Adresa: "Nermine Agović 13",
        Latitude: 18.12331,
        Longitude: 43.123312,
        TipDjelaId: 3
    };

    $rootScope.loading = true;
    prijavaService.getAllTipDjela().success(function (data) {
        $scope.tipoviDjela = data;
    })
    .finally(function (data) {
        $rootScope.loading = false;
    });
    $("#myModal").modal("show");
    //pocetak loadinga


    $scope.dodajPrijavu = function () {
        $rootScope.loading = true;

        var fd = new FormData();
        fd.append('file', $scope.medij);
        //prvo upload slike/videa
        prijavaService.createMedij(fd).success(function (data) {

            //ako je uspješan upload, kreiramo prijavu
            $scope.prijava.AlbumId = data.albumId
            prijavaService.create($scope.prijava).success(function (data) {
                alert(data.poruka);
                //$scope.prijave.push($scope.prijava);
                //$scope.poruka = data.poruka;
            })
            .finally(function (data) {                
                $rootScope.loading = false;
            });
        }).finally(function(data) {
            $rootScope.loading = false;
        });



    }

}]);