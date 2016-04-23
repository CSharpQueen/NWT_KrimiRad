/// <reference path="C:\OneDrive\GitHub\NWT_KrimiRad\KrimiRad\KrimiRad\Scripts/angular.js" />

var TipDjela = angular.module('KrimiRad.TipDjela', [])
    .controller('TipDjelaCtrl', ['$scope', 'tipDjelaService', function ($scope, tipDjelaService) {

        $scope.tipDjela = '';

        tipDjelaService.getAll().success(function (data) {
            $scope.tipoviDjela = data;
        });

        $scope.prikaziFormuZaCreate = function () {
            $scope.sta = "dodaj";
            $scope.tipDjela = ''
            $("#myModal").modal("show");
        }

        $scope.dodajTipDjela = function () {
            tipDjelaService.create($scope.tipDjela).success(function (data) {
                alert("Dodano!");
                $scope.tipoviDjela.push($scope.tipDjela);                
            })
        }

        $scope.prikaziFormuZaEdit = function (tip) {
            $scope.sta = "uredi";
            $scope.tipDjela = tip;
            $("#myModal").modal("show");
        }

        $scope.urediTipDjela = function () {
            tipDjelaService.update($scope.tipDjela);
            alert("Spremljene izmjene!");
        }

        $scope.obrisiTipDjela = function (tip) {
            tipDjelaService.delete(tip);
            alert("Obrisano!");
        }

    }]);