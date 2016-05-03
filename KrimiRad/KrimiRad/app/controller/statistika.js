var Statistika = angular.module('KrimiRad.Statistika', [])
    .controller('StatistikaCtrl', ['$scope', 'statistikaService', '$rootScope', function ($scope, statistikaService, $rootScope) {
            $scope.tipDjelaId = '';
            $scope.opstina = '';

            //dobavljanje po opstini i tipu djela
           $scope.PoOpstiniITipuDjela = function() {
                $rootScope.loading = true;               
                statistikaService.dajPoOpstiniITipuDjela($scope.id, $scope.opstina).success(function(data) {

                    //radiš nešta sa podacima, tj stavljaš ih u chart

                }).finally(function(data) {
                    $rootScope.loading = false;
                });
           };
            //END dobavljanje po opstini i tipu djela

        //i sad samo ponavljaš za ostale

            
     }]);
