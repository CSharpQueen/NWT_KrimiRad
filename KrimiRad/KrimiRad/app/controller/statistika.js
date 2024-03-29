﻿var Statistika = angular.module('KrimiRad.Statistika', ["chart.js", 'ngAnimate','ui.bootstrap'])
    .controller('StatistikaCtrl', ['$scope', 'statistikaService', 'tipDjelaService', '$rootScope', function ($scope, statistikaService, tipDjelaService, $rootScope) {
        $scope.tipDjelaId = '';
        $scope.opstina = '';
         $scope.options = {    
            minDate: new Date(),
            showWeeks: true
          };

        //BrojDjelaPoOpstinama---------------------------

        $scope.BrojDjelaPoOpstinama = function () {
            $scope.labels = [];
            $scope.data = [];
            $rootScope.loading = true;
            statistikaService.dajBrojDjelaPoOpstinama().success(function (data) {
                for (i = 0; i < data.length; i++) {
                    $scope.labels.push(data[i].Opstina);
                    $scope.data.push(data[i].Count);
                }
            }).finally(function (data) {
                $rootScope.loading = false;
            });
        }
        //END BrojDjelaPoOpstinama---------------------------

        //BrojDjelaPoDatumu-----------------------------
        $scope.BrojDjelaPoDatumu = function() {
            $scope.labels = [];
            $scope.data = [[]];            
            $rootScope.loading = true;
            statistikaService.dajBrojDjelaPoDatumu().success(function (data) {
                for (i = 0; i < data.length; i++) {
                    $scope.labels.push(data[i].Datum);
                    $scope.data[0].push(data[i].Count);
                }

            }).finally(function (data) {
                $rootScope.loading = false;
            });

        }
        //END BrojDjelaPoDatumu-----------------------------

        //BrojDjelaPoTipuDjela-----------------------------------

        $scope.BrojDjelaPoTipuDjela = function () {
            $scope.labels = [];
            $scope.data = [];
            $rootScope.loading = true;
            statistikaService.dajBrojDjelaPoTipuDjela().success(function (data){
                for (i = 0; i < data.length; i++) {
                    $scope.labels.push(data[i].TipDjela);
                    $scope.data.push(data[i].Count);
                }
            }).finally(function (data) {
                $rootScope.loading = false;
            });
        }

        //END BrojDjelaPoTipuDjela-----------------------------------

        //PrijavePoTipovimaZaOpstinu-------------------------------

        $scope.PrijavePoTipovimaZaOpstinu = function(opstina) {
            $scope.labels = [];
            $scope.data = [[]];            
            $rootScope.loading = true;

            statistikaService.dajPrijavePoTipovimaZaOpstinu(opstina).success(function(data) {
                for (i = 0; i < data.length; i++) {
                    $scope.labels.push(data[i].TipDjela);
                    $scope.data[0].push(data[i].Count);
                }

            }).finally(function (data) {
                $rootScope.loading = false;
            });
        }
        
        //END PrijavePoTipovimaZaOpstinu-------------------------------


        //OmjerRjesenihUPeriodu-----------------------

        $scope.OmjerRjesenihUPeriodu = function () {           
            $scope.labels = [];
            $scope.data = [[], []];
            $scope.series = ["Riješenih", "Nerješenih"];
            $rootScope.loading = true;
            statistikaService.dajOmjerRjesenihUPeriodu($scope.datumOd.toJSON(), $scope.datumDo.toJSON()).success(function (data) {

                for (i = 0; i < data.length; i++) {
                    $scope.labels.push(data[i].TipDjela);
                    $scope.data[0].push(data[i].BrojRijesenih);
                    $scope.data[1].push(data[i].BrojNerjesenih);
                }

            }).finally(function (data) {
                $rootScope.loading = false;
            });
        }

        $scope.popup1 = false;
        $scope.popup2 = false;

        $scope.datumOdOpen = function () {
            $scope.popup1 = true;
        }
        $scope.datumDoOpen = function () {
            $scope.popup2 = true;
        }

        //END OmjerRjesenihUPeriodu-----------------------
                

        //BrojDjelaPoOpstinamaZaTipDjela-----------------

        $scope.BrojDjelaPoOpstinamaZaTipDjela = function () {
            $scope.labels = [];
            $scope.data = [];            
            $rootScope.loading = true;
            statistikaService.dajBrojDjelaPoOpstinamaZaTipDjela($scope.tipDjelaId).success(function (data) {

                for (i = 0; i < data.length; i++) {
                    $scope.labels.push(data[i].Opstina);
                    $scope.data.push(data[i].Count);                    
                }

            }).finally(function (data) {
                $rootScope.loading = false;
            });
        }
        //END BrojDjelaPoOpstinamaZaTipDjela-----------------

        //BrojDjelaPoDatumimaZaTipDjela--------------------------

         $scope.BrojDjelaPoDatumimaZaTipDjela = function () {
            $scope.labels = [];
            $scope.data = [[]];            
            $rootScope.loading = true;
            statistikaService.dajBrojDjelaPoDatumimaZaTipDjela($scope.tipDjelaId).success(function (data) {

                for (i = 0; i < data.length; i++) {
                    $scope.labels.push(data[i].Datum);
                    $scope.data[0].push(data[i].Count);                    
                }

            }).finally(function (data) {
                $rootScope.loading = false;
            });
        }

        //END BrojDjelaPoDatumimaZaTipDjela--------------------


        //OmjerRjesenihPoTipuUPeriodu----------------------------------

         $scope.OmjerRjesenihPoTipuUPeriodu = function () {
             $scope.labels = [];
             $scope.data = [[], []];
             $scope.series = ["Riješenih", "Nerješenih"];
             $rootScope.loading = true;
             statistikaService.dajOmjerRjesenihPoTipuUPeriodu($scope.tipDjelaId, $scope.datumOd.toJSON(), $scope.datumDo.toJSON()).success(function (data) {

                 for (i = 0; i < data.length; i++) {
                     $scope.labels.push(data[i].TipDjela);
                     $scope.data[0].push(data[i].BrojRijesenih);
                     $scope.data[1].push(data[i].BrojNerjesenih);
                 }

             }).finally(function (data) {
                 $rootScope.loading = false;
             });
         }


        //END OmjerRjesenihPoTipuUPeriodu------------------------------


         $scope.initTipoviDjela = function () {
            $rootScope.loading = true;
            tipDjelaService.getAll().success(function (data) {
                $scope.tipoviDjela = data;                                
            }).finally(function (data) {
                //kraj loadinga
                $rootScope.loading = false;
            });
         }

         $scope.initOpstine = function () {
             $rootScope.loading = true;
            statistikaService.dajOpstine().success(function (data) {
                $scope.opstine = data;
            }).finally(function (data) {
                $rootScope.loading = false;
            })
        }
    }]);
