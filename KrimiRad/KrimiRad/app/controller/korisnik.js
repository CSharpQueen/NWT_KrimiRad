﻿/// <reference path="C:\OneDrive\GitHub\NWT_KrimiRad\KrimiRad\KrimiRad\Scripts/angular.js" />

angular.module('KrimiRad.KorisnikController', [])
    .controller('KorisnikCtrl', ['$scope', '$http', 'KrimiRadUrl', function ($scope, $http, KrimiRadUrl) {
        $scope.korisnik = '';    
        $scope.poruka = '';
        $http.get(KrimiRadUrl.serviceUrl + "/api/Korisnik").success(function (data) {
            $scope.korisnici = data;
            console.log($scope.korisnici)
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