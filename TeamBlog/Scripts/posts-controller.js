angular.module('teamblog')
    .controller("posts",
    [
        "$scope", "$http", function($scope, $http) {
            $scope.dummy = "aaaaaaaaaaaa";
            $scope.posts = [];
            var appendScopePosts = function(response) {
                response.data.forEach(function(item) {
                    $scope.posts.push(item);
                });
            };
            $http.get("api/posts", {}).then(function(response) {
                appendScopePosts(response);
            });
        }
    ]);