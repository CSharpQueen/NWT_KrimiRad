/// <reference path="C:\OneDrive\GitHub\NWT_KrimiRad\KrimiRad\KrimiRad\Scripts/angular.js" />

angular.module('KrimiRad.Korisnik', ['ngAnimate', 'ui.bootstrap'])
    .controller('KorisnikCtrl', ['$scope', '$http','$rootScope', 'KrimiRadUrl','$location', function ($scope, $http,$rootScope, KrimiRadUrl, $location) {
        $scope.korisnik = '';    
        $scope.poruka = '';        
        $rootScope.loading = true;        
        
        $http.get(KrimiRadUrl.serviceUrl + "/api/Korisnik?page=1").success(function (data) {
            $scope.totalItems = data.count;
            $scope.korisnici = data.korisnici;
            $scope.currentPage = 1;
            $rootScope.loading = false;
        }).finally(function (data) {
            $rootScope.loading = false;
        })
        
        $scope.pageChanged = function () {
            $rootScope.loading = true;
           $http.get(KrimiRadUrl.serviceUrl + "/api/Korisnik?page=" + $scope.currentPage).success(function (data) {
                $scope.totalItems = data.count;
                $scope.korisnici = data.korisnici;                
            }).finally(function(data) { 
                $rootScope.loading = false;
            })
        }

        

        $scope.prikaziFormuZaCreate = function () {
            
            $scope.sta = "dodaj";
            $scope.korisnik = ''    
            $scope.formaZaUnos = true;
        }

        $scope.$on('$routeChangeStart', function (next, current) {            
            if ($location.path() == "/administracija/korisnici") {                                
                formaZaUnos=true;                
            }
        });

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
            $scope.formaZaUnos = true;
        }

        $scope.urediKorisnika = function () {
            $http.put(KrimiRadUrl.serviceUrl + "/api/Korisnik?id=" + $scope.korisnik.ID, $scope.korisnik).success(function (data) {
                $scope.poruka = data.poruka;
            })
        }

        $scope.obrisiKorisnika = function (k) {
            $http.delete(KrimiRadUrl.serviceUrl + '/api/Korisnik/' + k.ID).success(function (data) {
                $scope.poruka = data.poruka;
            })
        }

        $scope.sakrijAlert = function() {
            $scope.poruka = '';
        }
    }]);