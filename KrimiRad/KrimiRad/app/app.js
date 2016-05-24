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
        publicSiteUrl: 'http://localhost:58808',
        adminSiteUrl: 'http://localhost:51580',
        //serviceUrl: 'http://service-krimirad.azurewebsites.net',
        //publicSiteUrl: 'http://public-krimirad.azurewebsites.net',
        //adminSiteUrl: 'http://admin-krimirad.azurewebsites.net'
    };
});

app.config(['$httpProvider', function($httpProvider) {
        $httpProvider.defaults.useXDomain = true;
        delete $httpProvider.defaults.headers.common['X-Requested-With'];
    }
]);


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

        .when("/prijave/:prijavaId", {
            templateUrl: "/GetViews/GetPrijavaDetalji",
            controller: "PrijavaDetalji"
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

         .when("/statistika/PoTipuIOpstini", {
             templateUrl: "/Statistika/GetView/PoOpstiniITipuDjela",
            controller: "StatistikaCtrl"
        })
       
         .when("/statistika/PoOpstini", {
             templateUrl: "/Statistika/GetView/PoOpstini",
            controller: "StatistikaCtrl"
        })

          .when("/statistika/PoTipuDjela", {
             templateUrl: "/Statistika/GetView/PoTipuDjela",
            controller: "StatistikaCtrl"
        })
        
          .when("/statistika/PoDatumu", {
             templateUrl: "/Statistika/GetView/PoDatumu",
            controller: "StatistikaCtrl"
        })
         .when("/statistika/BrojDjelaPoOpstinama", {
             templateUrl: "/Statistika/GetView/BrojDjelaPoOpstinama",
            controller: "StatistikaCtrl"
        })
        .when("/statistika/BrojDjelaPoDatumuZaOpstinu", {
             templateUrl: "/Statistika/GetView/BrojDjelaPoDatumuZaOpstinu",
            controller: "StatistikaCtrl"
        })    
        .when("/Manage/ChangePassword", {
            templateUrl: "/Manage/ChangePassword",
            controller: ""
        });        

}]);

//ANGULAR
app.run(function($rootScope) {
   
   $rootScope.loading = false; 

   $rootScope.$on('$routeChangeStart', function() {

      //show loading gif
      $rootScope.loading = true;

   });

   $rootScope.$on('$routeChangeSuccess', function() {

      //hide loading gif
      $rootScope.loading = false;

   });

   $rootScope.$on('$routeChangeError', function() {

       //hide loading gif
       alert('wtff');
       $rootScope.loading = false;
   });
});