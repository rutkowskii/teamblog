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

            $scope.notfsCount = 0;

            var signalrProxy = $.connection.notificationsHub;
            signalrProxy.client.receiveNotifications = function (notfsCount) {
                console.log('received from backend: ' + notfsCount);
                $scope.notfsCount = notfsCount;
                $scope.$apply();
            };

            $.connection.hub.start()
                 .done(function () { console.log('Now connected, connection ID=' + $.connection.hub.id); })
                 .fail(function () { console.log('Could not Connect!'); });
            

            $scope.login = "ANON";
            $scope.notifications = [];
            $scope.anyNotifications = true;
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
        "$scope", "$mdDialog", "$http", "$mdToast", function ($scope, $mdDialog, $http, $mdToast) {
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
                    console.log("we got the following answer  " + answer.title + "  " + answer.content);
                    console.log(JSON.stringify(answer));

                    $http.post("api/posts", answer).then(onPostAddSuccess, onPostAddError);
                    
                }, function () {
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

                //todo customize it. 
                

                //todo reusable, move it 
                function DialogController($scope, $mdDialog) {
                    $scope.hide = function () {
                        $mdDialog.hide();
                    };
                    $scope.newPost = {}; //todo if this is gonna be reusable, get rid of name 'newPost'
                    $scope.allChannelsAvailable = [
                       { name: "Smieszki" },
                       { name: "Dev-general" },
                       { name: "Support" }
                    ]; // todo get it from backend

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