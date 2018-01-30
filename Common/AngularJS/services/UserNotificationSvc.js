(function () {
    'use strict';

    angular
        .module('app.common')
        .factory('UserNotificationSvc', ['toastr', UserNotificationSvc]);

    function UserNotificationSvc(toastr) {
        toastr.options.timeOut = 10000;
        toastr.options.positionClass = "toast-top-right";

        return {
            error: error,
            info: info,
            success: success,
            warning: warning,
            clear: clear,
            topRight: "top-right",
            topCenter: "top-center",
            topLeft: "top-left",
            topFullWidth: "top-full-width",
            bottomRight: "bottom-right",
            bottomCenter: "bottom-center",
            bottomLeft: "bottom-left",
            bottomFullWidth: "bottom-full-width"
        };

        function clear() {
            toastr.remove();
        }

        function error(message, title, duration, position) {
            setToastrOptions(duration, position);
            toastr.error(message, title);
        }

        function info(message, title, duration, position) {
            setToastrOptions(duration, position);
            toastr.info(message, title);
        }

        function success(message, title, duration, position) {
            setToastrOptions(duration, position);
            toastr.success(message, title);
        }

        function warning(message, title, duration, position) {
            setToastrOptions(duration, position);
            toastr.warning(message, title);
        }

        function setToastrOptions(duration, position) {
            if (duration || duration === 0) {
                toastr.options.timeOut = duration;
            }
            if (position) {
                toastr.options.positionClass = "toast-" + position;
            }
        }
    }
})();