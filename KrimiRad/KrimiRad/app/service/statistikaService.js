
angular.module("KrimiRad.Statistika")
.service("statistikaService", ["$http", "KrimiRadUrl", function ($http, KrimiRadUrl) {
    var apiUrl = KrimiRadUrl.serviceUrl + "/api/Statistika/";
    var dajPoOpstiniITipuDjela = function (id, opcina) { return $http.get(apiUrl + "PrijavePoOpstiniITipuDjela?id=" + id + "&opstina=" + opcina); };
    var dajPoDatumu = function (date) { return $http.get(apiUrl + "PrijavePoDatumu?datum=" + date); };
    var dajPoOpstini = function (opstina) { return $http.get(apiUrl + "PrijavePoOpstini?opstina=" + opstina); };
    var dajPoTipuDjela = function (id) { return $http.get(apiUrl + "PrijavePoTipuDjela?id=" + id); };
    var dajBrojDjelaPoOpstinama = function () { return $http.get(apiUrl + "BrojDjelaPoOpstinama"); };
    var dajBrojDjelaPoDatumuZaOpstinu = function () { return $http.get(apiUrl + "BrojDjelaPoDatumuZaOpstinu"); };    
    var dajPrijavePoTipovimaZaOpstinu = function (opstina) { return $http.get(apiUrl + "PrijavePoTipovimaZaOpstinu?opstina=" + opstina); };   
    return {
            dajPrijavePoTipovimaZaOpstinu:dajPrijavePoTipovimaZaOpstinu,
            dajBrojDjelaPoOpstinama: dajBrojDjelaPoOpstinama, 
            dajBrojDjelaPoDatumuZaOpstinu:dajBrojDjelaPoDatumuZaOpstinu, 
            dajPoOpstiniITipuDjela: dajPoOpstiniITipuDjela, 
            dajPoDatumu: dajPoDatumu, 
            dajPoOpstini: dajPoOpstini, 
            dajPoTipuDjela: dajPoTipuDjela 
    };
}]);

