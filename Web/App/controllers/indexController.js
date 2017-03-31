(function (app) {

    app.controller('indexController', function ($http, $scope, $uibModal) {
        $scope.playerName = playerName;
        $scope.playerSecond = '';
        $scope.tier1 = [];
        $scope.tier2 = [];
        $scope.tier3 = [];
        $scope.games = [];

        $scope.getRating = function() {
            $http.get("/api/Players")
                .then(function(response) {
                    $scope.tier1 = response.data.slice(0, 3);
                    $scope.tier2 = response.data.slice(3, 8);
                    $scope.tier3 = response.data.slice(8, response.data.length);
                });
        };

        $scope.getGamesHistory = function() {
            $http.get("/api/Games")
                .then(function(response) {
                    $scope.games = response.data;
                });
        };

        $scope.getRating();
        $scope.getGamesHistory();

        $scope.addGame = function ($event) {
            $scope.playerSecond = angular.element($event.currentTarget).closest('li').find('.playerNameList').text();
            var modalInstance = $uibModal.open({
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'modalContent.html',
                controller: 'ModalInstanceCtrl',
                controllerAs: '$ctrl',
                size: 'sm',
                scope: $scope,
                resolve: {
                    items: function () {
                    }
                }
            });

            modalInstance.result.then(function () {

            }, function () {
                //$log.info('Modal dismissed at: ' + new Date());
            });
        };
    });

    app.controller('ModalInstanceCtrl', function ($http, $scope, $uibModalInstance) {
        var $ctrl = this;

        $ctrl.ok = function ($event) {
            console.log($scope.playerSecond);
            var p1Score = angular.element($event.currentTarget).closest('.modal-content').find('#playerOneScore').val();
            var p2Score = angular.element($event.currentTarget).closest('.modal-content').find('#playerTwoScore').val();
            var GameResult = {
                P1Score: p1Score,
                P2Score: p2Score,
                P1Name: $scope.playerName,
                P2Name: $scope.playerSecond
            };
            $http({
                method: 'POST',
                url: "/api/Games",
                data: JSON.stringify(GameResult)
            }).then(function () {
                $scope.getRating();
                $scope.getGamesHistory();
            });
            $uibModalInstance.close();
        };

        $ctrl.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    });

})(app);