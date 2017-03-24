'use strict';

//Define the 'camperList' module
angular.module('camperList', []);

var camperListApp = angular.module('camperList', []);

angular.
    module("camperList").
    component("camperList", {
        templateUrl: "/Camper/Camper/AngularIndex",
        controller: ['CamperListService', function CamperListComponentController(CamperListService) {
            var self = this;
            getCampers();
            function getCampers() {
                CamperListService.getCampers()
                    .then(function (campers) {
                        self.campers = campers.data;
                        console.log(self.campers);
                    })
                    //.error(function (error) {
                    //    $scope.status = 'Unable to load camper data: ' + error.message;
                    //    console.log($scope.status);
                    //});
            };
            self.message = 'Hello from AngularJS';
            self.parseMSDate = parseMSDate;
        }
    ]
});


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

