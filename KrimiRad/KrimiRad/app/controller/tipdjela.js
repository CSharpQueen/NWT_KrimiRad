/// <reference path="C:\OneDrive\GitHub\NWT_KrimiRad\KrimiRad\KrimiRad\Scripts/angular.js" />

angular.module('KrimiRad.TipDjelaController', [])
    .controller('TipDjelaCtrl', ['$scope', '$http', 'KrimiRadServis', function ($scope, $http, KrimiRadServis) {
        $scope.tipDjela = '';    
        
        $http.get(KrimiRadServis.url + "/api/TipDjela").success(function (data) {
            $scope.tipoviDjela = data;
        })

        $scope.prikaziFormuZaCreate = function () {
            $scope.sta = "dodaj";
            $scope.tipDjela = ''
            $("#myModal").modal("show");
        }

        $scope.dodajTipDjela = function () {
            $http.post(KrimiRadServis.url + "/api/TipDjela", $scope.tipDjela).success(function () {
                alert("Dodano!");
                $scope.tipoviDjela.push($scope.tipDjela);
            })
        }

        $scope.prikaziFormuZaEdit = function (tip) {
            console.log(tip.Id);
            $scope.sta = "uredi";
            $scope.tipDjela = tip;
            $("#myModal").modal("show");
        }

        $scope.urediTipDjela = function () {
            $http.put(KrimiRadServis.url + "/api/TipDjela?id=" + $scope.tipDjela.Id , $scope.tipDjela).success(function () {
                alert("Uređeno!");                
            })
        }

        $scope.obrisiTipDjela = function (tip) {            
            $http.delete(KrimiRadServis.url + '/api/TipDjela', {id: tip.Id}).success(function() {
                alert("Obrisano!");
            })
        }

    }]);