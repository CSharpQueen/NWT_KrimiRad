/// <reference path="C:\OneDrive\GitHub\NWT_KrimiRad\KrimiRad\KrimiRad\Scripts/angular.js" />

angular.module('KrimiRad.Korisnik', [])
    .controller('KorisnikCtrl', ['$scope', '$http','$rootScope', 'KrimiRadUrl', function ($scope, $http,$rootScope, KrimiRadUrl) {
        $scope.korisnik = '';    
        $scope.poruka = '';
        $rootScope.loading = true;
        $http.get(KrimiRadUrl.serviceUrl + "/api/Korisnik").success(function (data) {
            $scope.korisnici = data;
            $rootScope.loading = false;
        }).finally(function(data) { 
            $rootScope.loading = false;
        })

        $scope.prikaziFormuZaCreate = function () {
            $scope.sta = "dodaj";
            $scope.korisnik = ''
            $("#myModal").modal("show");
        }

        $scope.dodajKorisnika = function () {            
            $http.post(KrimiRadUrl.adminSiteUrl + "/account/register", $scope.korisnik).success(function (data, status, headers, config) {
                $scope.poruka=data.poruka;
                $scope.korisnici.push($scope.korisnik);
            }).error(function (data, status, headers, config) {
                $scope.poruka=data.poruka;
            });
        }

        $scope.prikaziFormuZaEdit = function (k) {
            $scope.sta = "uredi";
            $scope.korisnik = k;
            $("#myModal").modal("show");
        }

        $scope.urediKorisnika = function () {
            $http.put(KrimiRadUrl.serviceUrl + "/api/Korisnik?id=" + $scope.korisnik.Id, $scope.korisnik).success(function () {
                alert("Uređeno!");                
            })
        }

        $scope.obrisiKorisnika = function (k) {
            $http.delete(KrimiRadUrl.serviceUrl + '/api/Korisnik', { id: k.Id }).success(function () {
                alert("Obrisano!");
            })
        }

        $scope.sakrijAlert = function() {
            $scope.poruka = '';
        }
    }]);