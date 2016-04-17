/// <reference path="C:\OneDrive\GitHub\NWT_KrimiRad\KrimiRad\KrimiRad\Scripts/angular.js" />

var app = angular.module('app', ["ngRoute", "KrimiRad.TipDjelaController", "KrimiRad.KorisnikController"]);

app.factory('KrimiRadUrl', function () {
    return {
        //serviceUrl: 'http://localhost:58808',
        //publicSiteUrl: 'http://localhost:58808',
        //adminSiteUrl: 'http://localhost:51580',
        serviceUrl: 'http://service-krimirad.azurewebsites.net',
        publicSiteUrl: 'http://public-krimirad.azurewebsites.net',
        adminSiteUrl: 'http://admin-krimirad.azurewebsites.net'
    };
});

app.config(['$routeProvider', '$locationProvider',function ($routeProvider, $locationProvider) {
    
    $routeProvider
        .when("/administracija", {
            templateUrl: "/GetViews/GetAdministracija",
            controller: ""
        })
       .when("/prijave", {
           templateUrl: "/GetViews/GetPrijave",
           controller: ""
       })
       .when("/statistika", {
           templateUrl: "/GetViews/GetStatistika",
           controller: ""
       })
       .when("/administracija/tipovidjela", {
           templateUrl: "/Administracija/TipDjela/Index",
           controller: "TipDjelaCtrl"
       })
        .when("/administracija/korisnici", {
            templateUrl: "/Administracija/Korisnik/Index",
            controller: "KorisnikCtrl"
        })
        .when("/Manage/ChangePassword", {
            templateUrl: "/Manage/ChangePassword",
            controller: ""
        });
        $locationProvider.html5Mode(true);
}]);
