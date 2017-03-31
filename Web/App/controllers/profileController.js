(function (app) {

    app.controller('profileController', function ($http, $scope, $routeParams) {
        $scope.playerId = $routeParams.playerId;

        $scope.profile = {};

        $scope.getProfile = function () {
            $http.get("/api/Players/" + $scope.playerId)
                .then(function (response) {
                    $scope.profile = response.data;
                });
        };

        $scope.getProfile();

    });

})(app);