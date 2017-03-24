'use strict';

angular.
    module('campForDisabledApp').
    config(['$locationProvider', '$routeProvider', function config($locationProvider, $routeProvider) {
        $locationProvider.hashPrefix('!');

        $routeProvider.
            when('/', {
                template: '<camper-list></camper-list>'
            });
    }
    ]);