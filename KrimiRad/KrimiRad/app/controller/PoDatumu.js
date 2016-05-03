var DajPoDatumu = angular.module('KrimiRad.Statistika', [])
    .controller('PoDatumuCtrl', ['$scope', 'statistikaService', '$rootScope', function ($scope, statistikaService, $rootScope) {
    
        $scope.message = 'Ispisi nestoooo';
         //pocetak loadinga
        $rootScope.loading = false;
    }]);
 