angular.module("app").service("tipDjelaService", ["$http", "KrimiRadUrl", function ($http, KrimiRadUrl) {
    var apiUrl = KrimiRadUrl.serviceUrl + "/api/Prijava/";
    var getAll = function () { return $http.get(apiUrl); };
    return { getAll: getAll };
}]);