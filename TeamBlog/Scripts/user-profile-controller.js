angular.module('teamblog').controller("user-profile",
[
    "$scope", "$http", "$window", function($scope, $http, $window) {

        this.announceClick = function(index) {
            console.log("FUNCTION ANNOUNCE CLICK " + index);
        };

        $scope.notfsCount = 0;
        $scope.toggleUser = function() {
            $http.post("api/toggle").then(function(response) {
                $window.location.reload();
            });
        }

        var signalrProxy = $.connection.notificationsHub;
        signalrProxy.client.receiveNotifications = function(notfsCount) {
            console.log('received from backend: ' + notfsCount);
            $scope.notfsCount = notfsCount;
            $scope.$apply();
        };

        $.connection.hub.start()
            .done(function() { console.log('Now connected, connection ID=' + $.connection.hub.id); })
            .fail(function() { console.log('Could not Connect!'); });

        $http.get("api/currentUser", {}).then(function(response) {
            $scope.login = response.data.Name;
        });

        $scope.notifications = [];
        $scope.anyNotifications = true;
    }
]);