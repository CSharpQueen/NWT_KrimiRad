/// <reference path="C:\OneDrive\GitHub\NWT_KrimiRad\KrimiRad\KrimiRad\Scripts/angular.js" />

var app = angular.module('app', ["ngRoute", "ngMap", "pascalprecht.translate", "ngAnimate", "ui.bootstrap", "vcRecaptcha"]);


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
        //serviceUrl: 'http://localhost:58808',
        //publicsiteUrl: 'http://localhost:58808',
        //adminsiteUrl: 'http://localhost:51580'
        serviceUrl: 'http://service-krimirad.azurewebsites.net',
        publicSiteUrl: 'http://public-krimirad.azurewebsites.net',
        adminSiteUrl: 'http://admin-krimirad.azurewebsites.net'
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


app.directive('ngFileModel', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var model = $parse(attrs.ngFileModel);
            var isMultiple = attrs.multiple;
            var modelSetter = model.assign;
            element.bind('change', function () {
                var values = [];
                angular.forEach(element[0].files, function (item) {
                    var value = {
                        // File Name 
                        name: item.name,
                        //File Size 
                        size: item.size,
                        //File URL to view 
                        url: URL.createObjectURL(item),
                        // File Input Value 
                        _file: item
                    };
                    values.push(value);
                });
                scope.$apply(function () {
                    if (isMultiple) {
                        modelSetter(scope, values);
                    } else {
                        modelSetter(scope, values[0]);
                    }
                });
            });
        }
    };
}]);


app.config(["vcRecaptchaServiceProvider", function (vcRecaptchaServiceProvider) {   
    vcRecaptchaServiceProvider.setSiteKey('6LcxMCETAAAAABuJjJckenKxyVxcDo1vzGe1DAPo')
    vcRecaptchaServiceProvider.setTheme('dark')    
}]);

//ANGULAR
app.run(["$rootScope", function ($rootScope) {

    $rootScope.loading = false;

    $rootScope.$on('$routeChangeStart', function () {

        //show loading gif
        $rootScope.loading = true;

    });

    $rootScope.$on('$routeChangeSuccess', function () {

        //hide loading gif
        $rootScope.loading = false;

    });

    $rootScope.$on('$routeChangeError', function () {

        //hide loading gif
        alert('wtff');
        $rootScope.loading = false;
    });
}]);