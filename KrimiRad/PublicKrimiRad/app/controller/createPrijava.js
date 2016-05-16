/// <reference path="C:\OneDrive\GitHub\NWT_KrimiRad\KrimiRad\KrimiRad\Scripts/angular.js" />

angular.module('app').controller('CreatePrijavaCtrl', ['$scope', "NgMap", 'prijavaService', '$rootScope', function ($scope,NgMap, prijavaService, $rootScope) {
    $("#myModal").modal("show");
    $rootScope.loading = true;

    $scope.prijava = {
        Opstina: "",
        Grad: "Sarajevo",
        Adresa: "",
        Latitude: 43.123312,
        Longitude: 18.12331
    };

    
    //mapa----------------------------------------
    var gmarkers = []; //treba samo kao lista referenci markera da se mogu obrisati
    NgMap.getMap().then(function (map) {
        $scope.addMarker = function (ev) {
            //obriše sve markere kad postavlja novi, da je samo jedan aktivan
            removeMarkers();
            var marker = new google.maps.Marker({
                position: ev.latLng,
                map: $scope.map
            });
            gmarkers.push(marker);
            
            //ovdje bi trebalo postavit lokaciju
            $scope.prijava.Latitude = ev.latLng.lat();
            $scope.prijava.Longitude = ev.latLng.lng();   

            //geocoder dobavlja podatke na osnovu latitude i longitude
            var geocoder = new google.maps.Geocoder();            
            geocoder.geocode({
                latLng: ev.latLng
                },
                function (responses) {
                    if (responses && responses.length > 0) {

                        var addr = responses[0].address_components;


                        //ovdje postaviti adresu i opstinu
                        $scope.prijava.Adresa = addr[0].long_name; //adresa
                        $scope.prijava.Grad = addr[2].long_name; //grad
                        $scope.prijava.Opstina = addr[3].long_name; //opstina
                        console.log($scope.prijava);
                        console.log(addr);
                        $scope.$apply();                        
                    }
                    else {
                        console.log('Not getting Any address for given latitude and longitude.');
                    }
                }
            );

        };
    });             
    function removeMarkers() {
        for (i = 0; i < gmarkers.length; i++) {
            gmarkers[i].setMap(null);
        }
    }

    //end mapa-----------------------------------------

    
    $scope.poruka = '';    

    
    prijavaService.getAllTipDjela().success(function (data) {
        $scope.tipoviDjela = data;
    })
    .finally(function (data) {
        $rootScope.loading = false;
    });

    

    $scope.dodajPrijavu = function () {
        $rootScope.loading = true;                
        var fd = new FormData();
        fd.append('file', $scope.medij);
        //prvo upload slike/videa
        prijavaService.createMedij(fd).success(function (data) {

            //ako je uspješan upload, kreiramo prijavu
            $scope.prijava.AlbumId = data.albumId;
            prijavaService.create($scope.prijava).success(function (data) {
               $scope.poruka = data.poruka;                
            })
            .finally(function (data) {                
                $rootScope.loading = false;
            });
        }).finally(function(data) {
            $rootScope.loading = false;
        });
    }



    $scope.sakrijAlert = function() {
            $scope.poruka = '';
    }
}]);