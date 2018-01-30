(function () {
    'use strict';

    angular
        .module('app.common')
        .directive('submitButton', [submitButton]);

    function submitButton() {
        return {
            restrict: 'E',
            template: //button needs style="position:relative" so that the spinner using spin.js will work
                    '<button style="position:relative" type="button" us-spinner="{radius:7, width:2, length: 6, left: \'120%\'}" spinner-on="isSubmitting" ' +
                    '    class="btn btn-success" ng-disabled="isSubmitting" ng-click="submit()">{{ buttonText }} </button> <br>' +
                    '<span ng-show="outstandingErrorsExist" class="text-danger">Please correct the indicated validation errors</span>',
            require: '^form',
            scope: {
                buttonText: '@',                
                submitFunction: '&' //this function needs to return a promise
            },
            link: link
        }

        function link(scope, element, attrs, form) {
            scope.isSubmitting = false;

            scope.submit = function () {
                if (form.$valid) {
                    scope.isSubmitting = true;
                    var originalButtonText = scope.buttonText;
                    scope.buttonText = 'Submitting...';

                    scope.submitFunction()
                        .then(function () {
                            //the form has been submitted, so set it to pristine
                            form.$setPristine();
                        })
                        .finally(function(){
                            scope.isSubmitting = false;
                            scope.buttonText = originalButtonText;
                        });
                } else {
                    displayValidationErrors(scope, form);
                }
            }
        }

        function displayValidationErrors(scope, form) {
            scope.outstandingErrorsExist = true;
            scope.$watch(watcherFor(form), updaterFor(scope));

            setAllTouched(form);
        }

        function setAllTouched(form) {
            if (!form.$error.required) {
                return;
            }
            form.$error.required.forEach(function (requiredObject) {
                //if required is an array (it's a sub-form) those need to be enumerated as well
                if (Array.isArray(requiredObject.$error.required)) { 
                    setAllTouched(requiredObject);
                } else {
                    requiredObject.$setTouched();
                }
            })
        }

        function watcherFor(form) {
            return function () {
                return form.$invalid;
            };
        }

        function updaterFor(scope) {
            return function (invalid) {
                if (invalid) {
                    scope.outstandingErrorsExist = true;
                } else {
                    scope.outstandingErrorsExist = false;
                }
            }
        }
    }
})();