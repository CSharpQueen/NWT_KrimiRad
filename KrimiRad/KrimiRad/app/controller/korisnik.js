/// <reference path="C:\OneDrive\GitHub\NWT_KrimiRad\KrimiRad\KrimiRad\Scripts/angular.js" />

angular.module('KrimiRad.KorisnikController', [])
    .controller('KorisnikCtrl', ['$scope', '$http', 'KrimiRadServis', function ($scope, $http, KrimiRadServis) {
        $scope.korisnik = '';    
        
        $http.get(KrimiRadServis.url + "/api/Korisnik").success(function (data) {
            $scope.korisnici = data;
            console.log($scope.korisnici)
        })

        $scope.prikaziFormuZaCreate = function () {
            $scope.sta = "dodaj";
            $scope.korisnik = ''
            $("#myModal").modal("show");
        }

        $scope.dodajKorisnika = function () {
            $http.post(KrimiRadServis.url + "/api/Korisnik", $scope.korisnik).success(function () {
                alert("Dodano!");
                $scope.korisnici.push($scope.korisnik);
            })
        }

        $scope.prikaziFormuZaEdit = function (k) {
            $scope.sta = "uredi";
            $scope.korisnik = k;
            $("#myModal").modal("show");
        }

        $scope.urediKorisnika = function () {
            $http.put(KrimiRadServis.url + "/api/Korisnik?id=" + $scope.korisnik.Id , $scope.korisnik).success(function () {
                alert("Uređeno!");                
            })
        }

        $scope.obrisiKorisnika = function (k) {
            $http.delete(KrimiRadServis.url + '/api/Korisnik', { id: k.Id }).success(function () {
                alert("Obrisano!");
            })
        }

    }]);