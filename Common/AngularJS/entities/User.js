(function () {
    'use strict';

    angular
        .module('app.common')
        .factory('User',['jwtHelper', UserFactory]);

    function UserFactory(jwtHelper) {
        function User(systemId, userName, firstName, lastName, role, email, company, phone, token) {
            this.systemId = systemId;
            this.userName = userName;
            this.firstName = firstName;
            this.lastName = lastName;
            this.role = role;
            this.email = email;
            this.company = company;
            this.phone = phone;
            this.token = token;
            this.loggedIn = loggedIn(this);
        }

        function loggedIn(user) {
            return user.token ? true : false;
        }

        User.create = function (token) {
            var tokenData = jwtHelper.decodeToken(token);

            return new User(
                tokenData.systemid,
                tokenData.sub,
                tokenData.given_name,
                tokenData.family_name,
                tokenData.role,
                tokenData.email,
                tokenData.company,
                tokenData.phonenumber,
                token
            );
        };

        User.createEmpty = function () {
            return new User('', '', '', '', '', '', '', '');
        };

        return User;
    }
})();