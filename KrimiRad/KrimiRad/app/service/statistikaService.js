
angular.module("KrimiRad.Statistika")
.service("statistikaService", ["$http", "KrimiRadUrl", function ($http, KrimiRadUrl) {
    var apiUrl = KrimiRadUrl.serviceUrl + "/api/Statistika/";
    var dajPoOpstiniITipuDjela = function (id, opcina) { return $http.get(apiUrl + "PrijavePoOpstiniITipuDjela?id=" + id + "&opstina=" + opcina); };
    var dajPoDatumu = function (date) { return $http.get(apiUrl + "PrijavePoDatumu?datum=" + date); };
    var dajPoOpstini = function (opstina) { return $http.get(apiUrl + "PrijavePoOpstini?opstina=" + opstina); };
    var dajPoTipuDjela = function (id) { return $http.get(apiUrl + "PrijavePoTipuDjela?id=" + id); };
    var dajBrojDjelaPoOpstinama = function () { return $http.get(apiUrl + "BrojDjelaPoOpstinama"); };
    var dajBrojDjelaPoDatumu = function () { return $http.get(apiUrl + "BrojDjelaPoDatumu"); };
    var dajBrojDjelaPoTipuDjela = function () { return $http.get(apiUrl + "BrojDjelaPoTipuDjela"); };
    var dajPrijavePoTipovimaZaOpstinu = function (opstina) { return $http.get(apiUrl + "PrijavePoTipovimaZaOpstinu?opstina=" + opstina); };
    var dajOmjerRjesenihUPeriodu = function (datumOd, datumDo) { return $http.get(apiUrl + "OmjerRjesenihUPeriodu?datumOd=" + datumOd + "&datumDo=" + datumDo); }
    var dajBrojDjelaPoOpstinamaZaTipDjela = function (tipDjelaId) { return $http.get(apiUrl + "BrojDjelaPoOpstinamaZaTipDjela?tipDjelaId=" + tipDjelaId); };
    var dajBrojDjelaPoDatumimaZaTipDjela = function (tipDjelaId) { return $http.get(apiUrl + "BrojDjelaPoDatumimaZaTipDjela?tipDjelaId=" + tipDjelaId); };
    return {
        dajBrojDjelaPoDatumimaZaTipDjela: dajBrojDjelaPoDatumimaZaTipDjela,
        dajBrojDjelaPoOpstinamaZaTipDjela: dajBrojDjelaPoOpstinamaZaTipDjela,
        dajOmjerRjesenihUPeriodu: dajOmjerRjesenihUPeriodu,
        dajBrojDjelaPoTipuDjela: dajBrojDjelaPoTipuDjela,
        dajPrijavePoTipovimaZaOpstinu: dajPrijavePoTipovimaZaOpstinu,
        dajBrojDjelaPoOpstinama: dajBrojDjelaPoOpstinama,
        dajBrojDjelaPoDatumu: dajBrojDjelaPoDatumu,
        dajPoOpstiniITipuDjela: dajPoOpstiniITipuDjela,
        dajPoDatumu: dajPoDatumu,
        dajPoOpstini: dajPoOpstini,
        dajPoTipuDjela: dajPoTipuDjela
    };
}]);

