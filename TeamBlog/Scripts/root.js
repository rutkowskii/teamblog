angular
    .module('teamblog', ['ngMaterial', 'ngRoute'])
    .config(function($mdThemingProvider) {
        $mdThemingProvider
            .theme('default').dark()
            .primaryPalette('amber')
            .accentPalette('pink')
            .warnPalette('deep-orange');
    });