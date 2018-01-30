(function () {
    'use strict';

    angular
        .module('app.common')
        .factory('CurrentUserSvc', ['User', CurrentUserSvc]);

    function CurrentUserSvc(User) {
        var currentUser = User.createEmpty();

        return {
            saveUser: saveUser,
            removeUser: removeUser,
            profile: currentUser
        }

        function saveUser(token) {
            var user = User.create(token);
            currentUser.systemId = user.systemId;
            currentUser.userName = user.userName;
            currentUser.firstName = user.firstName;
            currentUser.lastName = user.lastname;
            currentUser.role = user.role;
            currentUser.email = user.email;
            currentUser.company = user.company;
            currentUser.phone = user.phone;
            currentUser.token = user.token;
            currentUser.loggedIn = user.loggedIn;
        }

        function removeUser() {
            currentUser.systemId = '';
            currentUser.userName = '';
            currentUser.firstName = '';
            currentUser.lastName = '';
            currentUser.role = '';
            currentUser.email = '';
            currentUser.token = '';
            currentUser.loggedIn = false;
        }
    }
})();