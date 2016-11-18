angular.module('teamblog', ['ui.bootstrap'])
    .controller("user-profile", [
        "$scope", function($scope) {
            $scope.login = "ANON";
            $scope.notifications = [
                "notification a",
                "notification b"
            ];
        }
    ])
    .controller("posts", [
        "$scope", "$http", function ($scope, $http) {
            $scope.dummy = "aaaaaaaaaaaa";
            $scope.posts = [];

            $http.get("api/posts", {}).then(function onSuccess(response) {
                appendScopePosts(response);
            });
            var appendScopePosts = function (response) {
                response.data.forEach(function(item) {
                    $scope.posts.push(item);
                });
            };
        }
    ]);