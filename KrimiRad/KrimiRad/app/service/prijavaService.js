angular.module("app").service("prijavaService", ["$http", "KrimiRadUrl", function ($http, KrimiRadUrl) {
    var apiUrl = KrimiRadUrl.serviceUrl + "/api/Prijava/";
    var getAll = function () { return $http.get(apiUrl); };
    var getById = function (id) { return $http.get(apiUrl + id); };
    var update = function (prijava) { return $http.put(apiUrl + prijava.ID, prijava); };
    var create = function (prijava) { return $http.post(apiUrl, prijava); };
    var destroy = function (prijava) { return $http.delete(apiUrl + prijava.ID); };
    var rijesi = function (id) { return $http.post(apiUrl + "Rijesi", id); };
    return { getAll: getAll, getById: getById, update: update, create: create, delete: destroy, rijesi: rijesi };
}]);

