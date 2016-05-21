angular.module("app").service("newsFeedService", ["$http", "KrimiRadUrl", function ($http, KrimiRadUrl) {
    var apiUrl = KrimiRadUrl.serviceUrl + "/api/NewsFeed/";
    var getPage = function (page) { return $http.get(apiUrl + "?page=" + page); };
    return { getPage: getPage};
}]);