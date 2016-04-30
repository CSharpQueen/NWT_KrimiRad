/// <reference path="C:\OneDrive\GitHub\NWT_KrimiRad\KrimiRad\KrimiRad\Scripts/angular.js" />

var app = angular.module('app', ["ngRoute", "ngMap", "pascalprecht.translate"]);


app.controller("appctrl", ["$rootScope", "$scope", "$translate", function ($scope, $rootScope, $translate) {   
    $rootScope.loading = false;
    $scope.selectedLanguage = $translate.preferredLanguage();
    $scope.switchLanguage = function (lang) {
        $translate.use(lang);
        $scope.selectedLanguage = lang;
    }
}]);

app.factory('KrimiRadUrl', function () {
    return {
        serviceUrl: 'http://localhost:58808',
        //publicSiteUrl: 'http://localhost:58808',
        adminSiteUrl: 'http://localhost:51580',
        //serviceUrl: 'http://service-krimirad.azurewebsites.net',
        publicSiteUrl: 'http://public-krimirad.azurewebsites.net'
        //adminSiteUrl: 'http://admin-krimirad.azurewebsites.net'
    };
});


app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    $locationProvider.html5Mode(true);
    $routeProvider
        .when("/prijava", {
            templateUrl: "/Home/CreatePrijava",
            controller: "CreatePrijavaCtrl"
        })

}]);

app.config(["$translateProvider", function ($translateProvider) {

    $translateProvider.useStaticFilesLoader({
        prefix: 'Translations/lang-',
        suffix: '.json'
    });
    $translateProvider.preferredLanguage('bs');
}]);

