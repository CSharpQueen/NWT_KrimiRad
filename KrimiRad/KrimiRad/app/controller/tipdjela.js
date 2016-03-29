/// <reference path="C:\OneDrive\GitHub\NWT_KrimiRad\KrimiRad\KrimiRad\Scripts/angular.js" />

angular.module('KrimiRad.TipDjelaController', [])
    .controller('TipDjelaCtrl', ['$scope', '$http', function ($scope, $http) {
        $scope.tipDjela = '';

        $http.get("http://localhost:58808/api/TipDjela").success(function (data) {
            $scope.tipoviDjela = data;
        })

        $scope.prikaziFormu = function () {
            $scope.sta = "dodaj";
            $("#myModal").modal("show");
        }

        $scope.dodajTipDjela = function () {
            $http.post("http://localhost:58808/api/TipDjela", $scope.tipDjela).success(function () {
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
            $http.put("http://localhost:58808/api/TipDjela?id=" + $scope.tipDjela.Id , $scope.tipDjela).success(function () {
                alert("Uređeno!");                
            })
        }

        $scope.obrisiTipDjela = function (tip) {            
            $http.delete('http://localhost:58808/api/TipDjela', {id: tip.Id}).success(function() {
                alert("Obrisano!");
            })
        }

    }]);