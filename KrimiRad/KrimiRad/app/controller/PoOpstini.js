var DajPoOpstini = angular.module('KrimiRad.Statistika', [])
    .controller('PoOpstiniCtrl', ['$scope', 'statistikaService', '$rootScope', function ($scope, statistikaService, $rootScope) {

        $scope.message = 'Proba';
        //pocetak loadinga
        $rootScope.loading = false;
    }]);
