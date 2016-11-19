angular.module('teamblog', ['ngMaterial'])

    .config(function ($mdThemingProvider) {
        $mdThemingProvider
            .theme('default').dark()
            .primaryPalette('amber')
            .accentPalette('pink')
            .warnPalette('deep-orange');
    })


    .controller("user-profile", [
        "$scope", function ($scope, $mdDialog) {

            this.announceClick = function (index) {
                console.log("FUNCTION ANNOUNCE CLICK " + index);
            };

            $scope.login = "ANON";
            $scope.notifications = [
                "notification a",
                "notification b"
            ];
            $scope.notificationSelected = null;
            $scope.anyNotifications = $scope.notifications.length > 0;
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
                response.data.forEach(function (item) {
                    $scope.posts.push(item);
                });
            };
        }
    ])

    
    .controller("new-posts", [
        "$scope", "$mdDialog", "$http", function ($scope, $mdDialog, $http) {
            $scope.showNewPostsDialog = function (ev) {
                $mdDialog.show({
                    controller: DialogController,
                    templateUrl: 'new-post.tmpl.html',
                    parent: angular.element(document.body),
                    targetEvent: ev,
                    clickOutsideToClose: true,
                    fullscreen: $scope.customFullscreen // Only for -xs, -sm breakpoints.
                })
                .then(function (answer) {
                    console.log("we got the following answer  " + answer.title + "  " + answer.content)
                    $http.post("api/posts", answer);
                    //todo on getting the response, show a toast at the bottom. 
                }, function () {
                    $scope.status = 'You cancelled the dialog.';
                });


                //todo reusable, move it 
                function DialogController($scope, $mdDialog) {
                    $scope.hide = function () {
                        $mdDialog.hide();
                    };
                    $scope.newPost = {};

                    $scope.cancel = function () {
                        $mdDialog.cancel();
                    };

                    $scope.confirm = function () {
                        $mdDialog.hide($scope.newPost);
                    };

                }
            }
        }
    ]);