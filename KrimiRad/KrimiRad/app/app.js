/// <reference path="C:\OneDrive\GitHub\NWT_KrimiRad\KrimiRad\KrimiRad\Scripts/angular.js" />

var app = angular.module('app', ["ngRoute","pascalprecht.translate", "KrimiRad.TipDjela", "KrimiRad.Korisnik","KrimiRad.PregledPrijava","KrimiRad.Statistika"]);

app.controller("appctrl", ["$rootScope", "$scope", "$translate", function ($scope, $rootScope, $translate) {    
    $rootScope.loading = false;
    $scope.selectedLanguage = $translate.preferredLanguage();
    $scope.switchLanguage = function (lang) {        
        $translate.use(lang);
        $scope.selectedLanguage = lang;
    }    
}]);

app.config(["$translateProvider", function ($translateProvider) {
   
    $translateProvider.useStaticFilesLoader({
        prefix: 'Translations/lang-',
        suffix: '.json'
    });
       
    $translateProvider.preferredLanguage('bs');
    //$translateProvider.useLocalStorage();
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


app.config(['$routeProvider', '$locationProvider',function ($routeProvider, $locationProvider) {
    $locationProvider.html5Mode(true);
    $routeProvider
        .when("/administracija", {
            templateUrl: "/GetViews/GetAdministracija",
            controller: ""
        })
       .when("/prijave", {
           templateUrl: "/GetViews/GetPrijave",
           controller: "PregledMapeCtrl"
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

         .when("/statistika/PoOpstini", {
            templateUrl: "/Statistika/PoOpstini/Index",
            controller: "PoOpstiniCtrl"
        })

        .when("/statistika/PoDatumu", {
            templateUrl: "/Statistika/PoDatumu/Index",
            controller: "PoDatumuCtrl"
        })
        .when("/statistika/PoTipuDjela", {
            templateUrl: "/Statistika/PoTipuDjela/Index",
            controller: "PoTipuDjelaCtrl"
        })
        
        .when("/Manage/ChangePassword", {
            templateUrl: "/Manage/ChangePassword",
            controller: ""
        });
   

}]);
