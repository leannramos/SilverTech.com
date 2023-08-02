(function () {
    var designerModule = angular.module('designer');
    designerModule.controller('SimpleCtrl', ['$scope', 'propertyService', 'serverContext',
        function ($scope, propertyService, serverContext) {
            $scope.feedback.showLoadingIndicator = true;
            propertyService.get()
                .then(function (data) {
                    if (data) {
                        $scope.properties = propertyService.toAssociativeArray(data.Items);
                        debugger;
                        $scope.templates = JSON.parse($scope.properties.Templates.PropertyValue);

                    }

                }, function (data) {
                    $scope.feedback.showError = true;
                    if (data) {
                        $scope.feedback.errorMessage = data.Detail;
                    }
                })
                .finally(function () {
                    $scope.feedback.showLoadingIndicator = false;
                });
        }]);
})();