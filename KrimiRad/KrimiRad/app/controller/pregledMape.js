angular.module('KrimiRad.PregledPrijava', ['ngMap'])
.controller('PregledMapeCtrl', ['$scope',"$rootScope", 'NgMap', 'prijavaService', '$rootScope',function ($scope,$rootScope, NgMap, prijavaService) {
    $rootScope.loading = true;
    NgMap.getMap().then(function (map) {
        prijavaService.getAll().success(function (data) {
            $scope.prijave = data;
        }).finally(function (data) {
            //kraj loadinga
            $rootScope.loading = false;
        });

    });
    $scope.otvoriPrijavu = function (id) {
        $rootScope.loading = true;
        prijavaService.getById(id).success(function (data) {

            $scope.prijava = data;

        }).finally(function (data) {
            $rootScope.loading = false;
        })


        $("#prijaveModal").modal("show");
    }
    //$scope.otvoriPrijavu = function () {

    //    var modalInstance = $modal.open({
    //        templateUrl: '/KrimiRad/Views/Modals/PrijaveModal.cshtml',
    //        backdrop: 'static',
    //        keyboard: false,
    //        controller: function ($scope, $modalInstance) {
    //            $scope.cancel = function () {
    //                $modalInstance.dismiss('cancel');
    //            };
    //            $scope.ok = function () {
    //                $modalInstance.close();
    //            };
    //        }
    //    });
    //}
    //$scope.otvoriPrijavu = function () {
    //    //aletr("radi");
     
  
        //var ModalInstanceCtrl = function ($scope, $modalInstance) {

        //    $scope.ok = function () {
        //        $modalInstance.close();
        //    };

        //    $scope.cancel = function () {
        //        $modalInstance.dismiss('cancel');
        //    };
        //};
        //    var modalInstance = $uibModal.open({
        //        animation: true,
        //        templateUrl: '/KrimiRad/Views/Modals/PrijaveModal.cshtml',
        //        controller: function ($scope, $modalInstance) {
        //            $scope.cancel = function () {
        //                $modalInstance.dismiss('cancel');
        //            };
        //            $scope.ok = function () {
        //                $modalInstance.close();
        //            };
        //        }
        //    });
        //}
            //modalInstance.result.then(function (changed_threshold){
            //    $scope.threshold = changed_threshold;
            //});
    //}
}]);
