angular.module("app").service("newsFeedService", ["$http", "KrimiRadUrl", function ($http, KrimiRadUrl) {
    var apiUrl = KrimiRadUrl.serviceUrl + "/api/NewsFeed/";
    var getAll = function () { return $http.get(apiUrl); };
    return { getAll: getAll};
}]);