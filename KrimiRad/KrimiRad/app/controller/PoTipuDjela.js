var DajPoTipuDjela = angular.module('KrimiRad.Statistika', [])
    .controller('PoTipuDjelaCtrl', ['$scope', 'statistikaService', '$rootScope', function ($scope, statistikaService, $rootScope) {
    
        $scope.message = 'Ispisi nestoooo';
         //pocetak loadinga
        $rootScope.loading = false;
    }]);
 