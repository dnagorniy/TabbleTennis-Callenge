(function (app) {

    app.controller('profileController', function ($http, $scope, $routeParams) {
        $scope.playerId = $routeParams.playerId;

        $scope.profile = {};
        $scope.stats = {};

        $scope.getProfile = function () {
            $http.get("/api/Players/" + $scope.playerId)
                .then(function (response) {
                    $scope.profile = response.data;
                });
        };

        $scope.getStats = function () {
            $http.get("/api/Stats/" + $scope.playerId)
                .then(function (response) {
                    $scope.stats = response.data;
                });
        };

        $scope.getProfile();
        $scope.getStats();

    });

})(app);