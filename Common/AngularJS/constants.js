(function () {
    'use strict';

    angular
        .module('app.common')
        .constant('constants', {
            version: '1.0.0'
        })
        .constant("toastr", toastr)
        .constant("angular", angular);;
})();