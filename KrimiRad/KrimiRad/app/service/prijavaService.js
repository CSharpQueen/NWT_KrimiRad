angular.module("app").service("prijavaService", ["$http", "KrimiRadUrl", function ($http, KrimiRadUrl) {
    var apiUrl = KrimiRadUrl.serviceUrl + "/api/Prijava/";
    var getAll = function () { return $http.get(apiUrl); };
    var getById = function (id) { return $http.get(apiUrl + id); };
    var update = function (tipDjela) { return $http.put(apiUrl + tipDjela.Id, tipDjela); };
    var create = function (tipDjela) { return $http.post(apiUrl, tipDjela); };
    var destroy = function (tipDjela) { return $http.delete(apiUrl + tipDjela.Id); };
    return { getAll: getAll, getById: getById, update: update, create: create, delete: destroy };
}]);

