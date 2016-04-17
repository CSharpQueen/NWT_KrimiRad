/// <reference path="C:\OneDrive\GitHub\NWT_KrimiRad\KrimiRad\KrimiRad\Scripts/angular.js" />

angular.module('KrimiRad.TipDjelaController', [])
    .controller('TipDjelaCtrl', ['$scope', '$http', 'KrimiRadUrl', function ($scope, $http, KrimiRadUrl) {
        $scope.tipDjela = '';    
        
        $http.get(KrimiRadUrl.serviceUrl + "/api/TipDjela").success(function (data) {
            $scope.tipoviDjela = data;
        })

        $scope.prikaziFormuZaCreate = function () {
            $scope.sta = "dodaj";
            $scope.tipDjela = ''
            $("#myModal").modal("show");
        }

        $scope.dodajTipDjela = function () {
            $http.post(KrimiRadUrl.serviceUrl + "/api/TipDjela", $scope.tipDjela).success(function () {
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
            $http.put(KrimiRadUrl.serviceUrl + "/api/TipDjela?id=" + $scope.tipDjela.Id, $scope.tipDjela).success(function () {
                alert("Uređeno!");                
            })
        }

        $scope.obrisiTipDjela = function (tip) {            
            $http.delete(KrimiRadUrl.serviceUrll + '/api/TipDjela', { id: tip.Id }).success(function () {
                alert("Obrisano!");
            })
        }

    }]);