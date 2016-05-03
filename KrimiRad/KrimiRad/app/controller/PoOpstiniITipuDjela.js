var DajPoOpstiniITipuDjela = angular.module('KrimiRad.Statistika', [])
    .controller('PoOpstiniITipuDjelaCtrl', ['$scope', 'statistikaService', '$rootScope', function ($scope, statistikaService, $rootScope) {

        $scope.message = 'Ispisi nestoooo';
        //pocetak loadinga
        $rootScope.loading = false;
    }]);
