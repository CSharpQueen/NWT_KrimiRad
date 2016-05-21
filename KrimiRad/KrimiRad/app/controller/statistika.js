var Statistika = angular.module('KrimiRad.Statistika', [])
    .controller('StatistikaCtrl', ['$scope', 'statistikaService', '$rootScope', function ($scope, statistikaService, $rootScope) {
        $scope.tipDjelaId = '';
        $scope.opstina = '';

        $scope.BrojDjelaPoOpstinama = function () {
            $scope.labels = '';
            $scope.data = '';
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

        //dobavljanje po opstini i tipu djela
        $scope.PoOpstiniITipuDjela = function () {
            $rootScope.loading = true;
            statistikaService.dajPoOpstiniITipuDjela($scope.id, $scope.opstina).success(function (data) {

                //stavljanje u chart

            }).finally(function (data) {
                $rootScope.loading = false;
            });
        };
        //END dobavljanje po opstini i tipu djela


        //dobavljanje po opstini 
        $scope.PoOpstini = function () {
            $rootScope.loading = true;
            statistikaService.dajPoOpstini($scope.opstina).success(function (data) {

                //stavljanje podataka u chart

            }).finally(function (data) {
                $rootScope.loading = false;
            });
        };
        //END dobavljanje po opstini 


        //dobavljanje po Tipu djela
        $scope.PoTipuDjela = function () {
            $rootScope.loading = true;
            statistikaService.dajPoTipuDjela($scope.id).success(function (data) {

                //stavljanje podataka u chart

            }).finally(function (data) {
                $rootScope.loading = false;
            });
        };
        //END dobavljanje po tipu djela

        //dobavljanje po datumu
        $scope.PoDatumu = function () {
            $rootScope.loading = true;
            statistikaService.dajPoDatumu($scope.date).success(function (data) {

                //stavljanje podataka u chart

            }).finally(function (data) {
                $rootScope.loading = false;
            });
        };
        //END dobavljanje po datumu



    }]);
