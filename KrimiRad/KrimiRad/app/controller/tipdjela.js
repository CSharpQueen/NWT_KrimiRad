/// <reference path="C:\OneDrive\GitHub\NWT_KrimiRad\KrimiRad\KrimiRad\Scripts/angular.js" />

var TipDjela = angular.module('KrimiRad.TipDjela', [])
    .controller('TipDjelaCtrl', ['$scope', 'tipDjelaService', '$rootScope', function ($scope, tipDjelaService, $rootScope) {
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

        $scope.prikaziFormuZaCreate = function () {
            $scope.sta = "dodaj";
            $scope.tipDjela = ''
            $("#myModal").modal("show");
        }

        $scope.dodajTipDjela = function () {
            $rootScope.loading = true;
            tipDjelaService.create($scope.tipDjela).success(function (data) {
                $scope.tipoviDjela.push($scope.tipDjela);
                $scope.poruka = data.poruka;
            })
            .finally(function (data) {
                $rootScope.loading = false;
            });
        }

        $scope.prikaziFormuZaEdit = function (tip) {
            $scope.sta = "uredi";
            $scope.tipDjela = tip;
            $("#myModal").modal("show");
        }

        $scope.urediTipDjela = function () {
            $rootScope.loading = true;
            tipDjelaService.update($scope.tipDjela)
            .success(function (data) {
                alert("Spremljene izmjene!");
            })
            .finally(function (data) {
                $rootScope.loading = false;
            });

        }

        $scope.obrisiTipDjela = function (tip) {
            $rootScope.loading = true;
            tipDjelaService.delete(tip).success(function (data) {
                alert("Obrisano!");
            }).finally(function (data) {
                $rootScope.loading = false;
            });

        }

    }]);