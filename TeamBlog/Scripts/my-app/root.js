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
        "$scope", function ($scope) {
            $scope.dummy = "aaaaaaaaaaaa";
            $scope.posts = [
                {
                    timestamp: moment().add(-5, 'days'),
                    content: 'content AAAAAAAAAA lorem ipsum lorem ipsum lorem ipsum lorem' +
                        ' ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum ' +
                        'lorem ipsum ',
                    addedBy: 'Piotr RUTEK Rutkowski',
                    channel: 'misc'
                },
                {
                    timestamp: moment().add(-3, 'days'),
                    content: 'content BBBBBBBBBBBB lorem ipsum lorem ipsum lorem ipsum lorem' +
                        ' ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum ' +
                        'lorem ipsum ',
                    addedBy: 'Jan Kowalski',
                    channel: 'dev general'
                },
                {
                    timestamp: moment().add(-1, 'days'),
                    content: 'content CCCCCC CCCCC CCCCCCCCCC lorem ipsum lorem ipsum lorem ipsum lorem' +
                        ' ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum lorem ipsum ' +
                        'lorem ipsum ',
                    addedBy: 'Mariusz Nowak',
                    channel: 'scrum masters'
                }
            ];
        }
    ]);