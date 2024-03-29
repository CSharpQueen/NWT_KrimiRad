﻿
angular.module("app").service("prijavaService", ["$http", "KrimiRadUrl", function ($http, KrimiRadUrl) {
    var apiUrl = KrimiRadUrl.serviceUrl + "/api/Prijava/";
    var getAll = function () { return $http.get(apiUrl); };
    var getAllTipDjela = function () { return $http.get(KrimiRadUrl.serviceUrl + "/api/TipDjela/"); };
    var getById = function (id) { return $http.get(apiUrl + id); };
    var update = function (prijava) { return $http.put(apiUrl + prijava.Id, prijava); };
    var create = function (prijava) { 
        return $http.post(apiUrl, prijava);
    };
    var createMedij = function (medij) {
        return $http.post(apiUrl+"PostMedij", medij, {            
            headers: { 'Content-Type': undefined }
        });
    };
    var destroy = function (prijava) { return $http.delete(apiUrl + prijava.Id); };

    var getAddressData = function (latLng) {
        return $http.get("http://maps.googleapis.com/maps/api/geocode/json?latlng="+latLng + "&sensor=true");

    }

    return { getAll: getAll, getById: getById, getAllTipDjela:getAllTipDjela, update: update, create: create, delete: destroy, createMedij:createMedij, getAddressData:getAddressData };
}]);

