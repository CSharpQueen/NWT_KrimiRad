/// <reference path="C:\OneDrive\GitHub\NWT_KrimiRad\KrimiRad\KrimiRad\Scripts/angular.js" />

var TipDjela = angular.module('KrimiRad.TipDjela', ['ngAnimate', 'ui.bootstrap'])
    .controller('TipDjelaCtrl', ['$scope', 'tipDjelaService', '$rootScope', '$location', function ($scope, tipDjelaService, $rootScope, $location) {
        $scope.poruka = '';
        $scope.tipDjela = '';
        //pocetak loadinga
        $rootScope.loading = true;
        $scope.formaZaUnos = false;

        tipDjelaService.getPage(1).success(function (data) {
            $scope.tipoviDjela = data.tipoviDjela;            
            $scope.totalItems = data.count;
            $scope.currentPage = 1;
        }).finally(function (data) {
            //kraj loadinga
            $rootScope.loading = false;
        });

        $scope.pageChanged = function () {
            $rootScope.loading = true;
            tipDjelaService.getPage($scope.currentPage).success(function (data) {                
                $scope.tipoviDjela = data.tipoviDjela;
                $scope.totalItems = data.count;
            }).finally(function (data) {
                //kraj loadinga
                $rootScope.loading = false;
            });
        }

       
        $scope.prikaziFormuZaCreate = function () {
            $scope.sta = "dodaj";
            $scope.tipDjela = ''
            $scope.formaZaUnos = true;
        }

        $scope.$on('$routeChangeStart', function (next, current) {
            if ($location.path() == "/administracija/tipovidjela") {
                formaZaUnos = true;
            }
        });

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
            $scope.formaZaUnos = true;
        }

        $scope.urediTipDjela = function () {
            $rootScope.loading = true;            

            tipDjelaService.update($scope.tipDjela)
            .success(function (data) {
                $scope.poruka = data.poruka;
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