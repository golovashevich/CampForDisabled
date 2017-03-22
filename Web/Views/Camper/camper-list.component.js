'use strict';

var camperListApp = angular.module('camperListApp', []);

camperListApp.controller("CamperListController", ['$scope', 'CamperListService', function CamperListController($scope, CamperListService) {
    getCampers();
    function getCampers() {
        CamperListService.getCampers()
            .then(function (campers) {
                $scope.campers = campers.data;
                console.log($scope.campers);
            })
            //.error(function (error) {
            //    $scope.status = 'Unable to load camper data: ' + error.message;
            //    console.log($scope.status);
            //});
    };
    $scope.message = 'Hello from AngularJS';
    $scope.parseMSDate = parseMSDate;
}]);

camperListApp.directive('ngConfirmOnClick', [
        function () {
            return {
                link: function (scope, element, attr) {
                    var msg = attr.ngConfirmOnClick; 
                    element.bind('click', function (event) {
                        if (!window.confirm(msg)) {
                            event.preventDefault();
                        }
                    });
                }
            };
        }]);


camperListApp.factory('CamperListService', ['$http', function ($http) {
    var CamperListService = {}
    CamperListService.getCampers = function () {
        return $http.post("/Camper/Camper/JsonIndex");
    };
    return CamperListService;
}]);

function parseMSDate(s) {
    // Jump forward past the /Date(, parseInt handles the rest
    return new Date(parseInt(s.substr(6)));
};

