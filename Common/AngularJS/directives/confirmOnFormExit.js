(function () {
    'use strict';

    angular
        .module('app.common')
        .directive('confirmOnFormExit', ['$window', confirmOnFormExit]);

    function confirmOnFormExit($window) {
        var confirmMessage = 'Are you sure you want to leave this page? Any unsaved changes will be lost.';

        return {
            require: '^form',
            restrict: 'A',
            link: function ($scope, elem, attrs, form) {
                $window.onbeforeunload = function () {
                    if (form.$dirty) {
                        return confirmMessage;
                    } 
                }
                $scope.$on('$locationChangeStart', function (event, next, current) {
                    if (form.$dirty) {
                        if (!$window.confirm(confirmMessage)) {
                            event.preventDefault();
                        } else {
                            //user ignored the warning. set the form back to pristine.
                            form.$setPristine();
                        }
                    }
                });
            }
        };
    }
})();