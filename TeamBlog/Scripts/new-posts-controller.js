angular.module('teamblog')
    .controller("new-posts",
    [
        "$scope", "$mdDialog", "$http", "$mdToast", function($scope, $mdDialog, $http, $mdToast) {
            $scope.showNewPostsDialog = function(ev) {
                $mdDialog.show({
                        controller: DialogController,
                        templateUrl: 'new-post.tmpl.html',
                        parent: angular.element(document.body),
                        targetEvent: ev,
                        clickOutsideToClose: true,
                        fullscreen: $scope.customFullscreen // Only for -xs, -sm breakpoints.
                    })
                    .then(function(answer) {
                            $http.post("api/posts", answer).then(onPostAddSuccess, onPostAddError);
                        },
                        function() {
                            $scope.status = 'You cancelled the dialog.';
                        });


                function onPostAddSuccess(response) {
                    $mdToast.show(
                        $mdToast.simple()
                        .textContent('Post added successfully')
                        .hideDelay(3000)
                    );
                }

                function onPostAddError(response) {
                    $mdToast.show(
                        $mdToast.simple()
                        .textContent('Something went wrong :(')
                        .hideDelay(3000)
                    );
                }

                function DialogController($scope, $mdDialog, $http) {
                    $scope.hide = function() {
                        $mdDialog.hide();
                    };
                    $scope.newPost = {}; //todo if this is gonna be reusable, get rid of name 'newPost'
                    $scope.allChannelsAvailable = [];
                    $http.get("api/channels", {}).then(function(response) {
                        response.data.forEach(function(item) {
                            console.log("hey we got an item");
                            $scope.allChannelsAvailable.push(item);
                        });
                    });


                    $scope.cancel = function() {
                        $mdDialog.cancel();
                    };

                    $scope.confirm = function() {
                        $mdDialog.hide($scope.newPost);
                    };

                }
            }
        }
    ])