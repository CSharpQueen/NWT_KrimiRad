
angular.module("KrimiRad.TipDjela")
.service("tipDjelaService", ["$http", "KrimiRadUrl", function ($http, KrimiRadUrl   ) {
    var apiUrl = KrimiRadUrl.serviceUrl + "/api/TipDjela/";
    var getAll = function () { return $http.get(apiUrl); };
    var getById = function (id) { return $http.get(apiUrl + id); };
    var update = function (tipDjela) { return $http.put(apiUrl + tipDjela.ID, tipDjela); };
    var create = function (tipDjela) { return $http.post(apiUrl, tipDjela); };
    var destroy = function (tipDjela) { return $http.delete(apiUrl + tipDjela.ID); };
    var getPage = function (page) { return $http.get(KrimiRadUrl.serviceUrl + "/api/TipDjela?page=" + page); };
    return { getAll: getAll, getById: getById, update: update, create: create, delete: destroy, getPage: getPage };
}]);

