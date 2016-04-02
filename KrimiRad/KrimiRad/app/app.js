/// <reference path="C:\OneDrive\GitHub\NWT_KrimiRad\KrimiRad\KrimiRad\Scripts/angular.js" />

var app = angular.module('app', ["ngRoute", "KrimiRad.TipDjelaController", "KrimiRad.KorisnikController"]);

app.factory('KrimiRadServis', function () {
    return {
        url: 'http://localhost:58808'
    };
});

app.config(function ($routeProvider) {    
    $routeProvider
        .when("/administracija", {
            templateUrl: "/GetViews/GetAdministracija",
            controller: "administracijaController"
        })
       .when("/prijave", {
           templateUrl: "/GetViews/GetPrijave",
           controller: "prijaveController"
       })
       .when("/statistika", {
           templateUrl: "/GetViews/GetStatistika",
           controller: "statistikaController"
       })
       .when("/administracija/tipovidjela", {
           templateUrl: "/Administracija/TipDjela/Index",
           controller: "TipDjelaCtrl"
       })
        .when("/administracija/korisnici", {
            templateUrl: "/Administracija/Korisnik/Index",
            controller: "KorisnikCtrl"
        })
});
