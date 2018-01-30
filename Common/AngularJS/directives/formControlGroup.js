(function () {
    'use strict';
    
    angular
        .module('app.common')
        .directive('formControlGroup', ['$compile', '$interpolate', formControlGroup]);

    function formControlGroup($compile, $interpolate) {
        return {
            restrict: 'A',
            link: link($compile, $interpolate),
            require: '^form'
        };

        function link($compile, $interpolate) {
            return function (scope, element, attributes, form) {
                var inputName = setupDom(element[0], $interpolate, scope);

                addErrorMessages(form, element, inputName, $compile, scope);

                scope.$watch(watcherFor(form, inputName), updaterFor(element));
            }
        }

        function setupDom(element, $interpolate, scope) {
            var input = element.querySelector('input, textarea, select');
            var type = input.getAttribute('type');
            if (type !== 'checkbox' && type !== 'radio'
                && type !== 'file') {
                input.classList.add('form-control')
            }

            var label = element.querySelector('label');
            label.classList.add('control-label');
            element.classList.add('form-group');

            var inputName = input.getAttribute('name')
            //in the case that the name includes an expression, it needs to be interpolated
            var interpolatedName = $interpolate(inputName)(scope);
            return interpolatedName;
        }

        function addErrorMessages(form, element, name, $compile, scope) {
            var formQualifiedName = form.$name + '.' + name;
            var messages = 
                '<div class="help-block" ng-messages="' + formQualifiedName + '.$error" '
                + 'ng-show="' + formQualifiedName + '.$touched">'
                    + '<span ng-messages-include="/app/Common/templates/messages.html"></span>'
                + '</div>';
            element.append($compile(messages)(scope));
        }

        function watcherFor(form, name) {
            return function () {
                if (name && form[name]) {
                    return form[name].$invalid && form[name].$touched;
                }
            };
        }

        function updaterFor(element) {
            return function (showError) {
                if (showError) {
                    element.addClass('has-error');
                } else {
                    element.removeClass('has-error');
                }
            }
        }
    }
})();