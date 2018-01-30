(function () {
    'use strict';

    angular
        .module('app.common')
        .directive('datePicker', [datePicker]);

    function datePicker() {
        return {
            restrict: 'E',
            template:
                '<ng-form name="{{pickerId}}">' +
                    '<div form-control-group class="col-md-2">' +
                        '<label for="{{pickerId}}">{{pickerTitle}}</label>' +
                        '<div class="input-group">' +
                            '<input type="text" uib-datepicker-popup="{{format}}" id="{{pickerId}}" name="{{pickerId}}" placeholder="mm/dd/yyyy" '+
                                'is-open="opened" datepicker-options="options" close-text="Close" ng-required="{{isRequired}}" ng-model="dateModel" ' +
                                'ui-mask="99/99/9999" ui-options model-view-value="true"' +
                                'alt-input-formats="altInputFormats" ng-disabled="isDisabled" ng-change="changeHandler()"/>' +
                            '<span class="input-group-btn">' +
                                '<button type="button" class="btn btn-default" ng-click="open()" ng-disabled="isDisabled">' +
                                    '<i class="glyphicon glyphicon-calendar"></i>' +
                                '</button>' +
                            '</span>' +
                        '</div>' +
                    '</div>' +
                '</ng-form>',
            scope: {
                pickerId: '@',
                pickerTitle: '@',
                isRequired: '@',
                isDisabled: '=',
                maxDateYearsOffset: '@', //sets a max date equal to the supplied number of years before today's date
                dateModel: '='
            },
            link: link
        }

        function link($scope, $element, $attrs) {
            initialize($scope);

            $scope.open = function () {
                $scope.opened = true;
            }

            $scope.changeHandler = function () {
                if ($scope.options.maxDate && $scope.dateModel) {
                    var pickerId = $scope.pickerId;
                    $scope[pickerId][pickerId].$setValidity('inDateRange', $scope.dateModel.getTime() <= $scope.options.maxDate.getTime());
                }
            }
        }

        function initialize($scope) {
            var maxDate = getYearsOffsetDate($scope.maxDateYearsOffset);

            $scope.$watch('dateModel', function (newValue, oldValue) {
                //If value is not Date object then convert it
                if (newValue && !(newValue instanceof Date)) {
                    var convertedDateModel = new Date($scope.dateModel);
                    $scope.dateModel = convertedDateModel;
                }
            });

            $scope.format = 'MM/dd/yyyy';
            $scope.altInputFormats = ['yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
            $scope.options = {
                datepickerMode: 'year',
                maxDate: maxDate,
                initDate: maxDate
            }            
        }

        function getYearsOffsetDate(yearsOffset) {
            if (yearsOffset) {
                var intOffset = parseInt(yearsOffset);
                var maxDate = calculateYearsOffsetDate(intOffset);
                return maxDate;
            } else {
                return null;
            }
        }

        function calculateYearsOffsetDate(yearsOffset) {
            var now = new Date();
            now.setFullYear(now.getFullYear() - yearsOffset);
            return now;
        }
    }
})();