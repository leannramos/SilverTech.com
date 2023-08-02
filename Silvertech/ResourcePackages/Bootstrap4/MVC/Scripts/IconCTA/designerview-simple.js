(function ($) {

    var simpleViewModule = angular.module('simpleViewModule', ['expander', 'designer', 'ngSanitize']);
    angular.module('designer').requires.push('simpleViewModule');
    angular.module('designer').requires.push('sfFields');
    angular.module('designer').requires.push('sfSelectors');

    simpleViewModule.controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        $scope.feedback.showLoadingIndicator = true;

        propertyService
            .get()
            .then(function (data) {
                if (!data) {
                    return;
                }

                $scope.properties = propertyService.toAssociativeArray(data.Items);

                var isPageSelectMode = $scope.properties.IsPageSelectMode.PropertyValue;
                $scope.properties.IsPageSelectMode.PropertyValue = isPageSelectMode.toString().toLowerCase() === "true";

                var isPageSelectModeTwo = $scope.properties.IsPageSelectModeTwo.PropertyValue;
                $scope.properties.IsPageSelectModeTwo.PropertyValue = isPageSelectModeTwo.toString().toLowerCase() === "true";

                $scope.DefaultGuid = "00000000-0000-0000-0000-000000000000";
                $scope.showLinkOneReset = ($scope.properties.LinkedPageId.PropertyValue !== $scope.DefaultGuid && $scope.properties.LinkedPageId.PropertyValue !== "") || $scope.properties.LinkedUrl.PropertyValue !== "";
                $scope.showLinkTwoReset = ($scope.properties.LinkedPageIdTwo.PropertyValue !== $scope.DefaultGuid && $scope.properties.LinkedPageIdTwo.PropertyValue !== "") || $scope.properties.LinkedUrlTwo.PropertyValue !== "";
                $scope.showImageOneReset = $scope.properties.ImageId.PropertyValue !== $scope.DefaultGuid && $scope.properties.ImageId.PropertyValue !== "";
                $scope.showImageTwoReset = $scope.properties.ImageTwoId.PropertyValue !== $scope.DefaultGuid && $scope.properties.ImageTwoId.PropertyValue !== "";
            },
                function (data) {
                    $scope.feedback.showError = true;
                    if (data)
                        $scope.feedback.errorMessage = data.Detail;
                })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });

        $scope.resetImageOne = function () {
            var imageFields = $("sf-image-field").find("img");
            for (var i = 0; i < imageFields.length; i++) {
                if (imageFields[i].attributes.src != null && $scope.selectedImage != null) {
                    if (imageFields[i].attributes.src.value == $scope.selectedImage.ThumbnailUrl && imageFields[i].parentElement.parentElement.attributes["sf-image"].value == "selectedImage") {
                        $(imageFields[i]).attr("src", "");

                        $scope.selectedImage = null;
                    }
                }
            }

            $scope.properties.ImageId.PropertyValue = $scope.DefaultGuid;
            $scope.showImageOneReset = !$scope.showImageOneReset;
        };

        $scope.resetImageTwo = function () {
            var imageFields = $("sf-image-field").find("img");

            if ($scope.selectedImageTwo != null) {
                for (var i = 0; i < imageFields.length; i++) {
                    if (imageFields[i].attributes.src != null && $scope.selectedImageTwo != null) {
                        if (imageFields[i].attributes.src.value == $scope.selectedImageTwo.ThumbnailUrl && imageFields[i].parentElement.parentElement.attributes["sf-image"].value == "selectedImageTwo") {
                            $(imageFields[i]).attr("src", "");

                            $scope.selectedImageTwo = null;
                        }
                    }
                }
            }
            else {
                for (var i = 0; i < imageFields.length; i++) {
                    if (imageFields[i].attributes.src != null && $scope.selectedImage != null) {
                        if (imageFields[i].attributes.src.value == $scope.selectedImage.ThumbnailUrl && imageFields[i].parentElement.parentElement.attributes["sf-image"].value == "selectedImage") {

                            $(imageFields[i]).attr("src", "");
                            $scope.selectedImage = null;
                        }
                    }
                }
            }

            $scope.properties.ImageTwoId.PropertyValue = $scope.DefaultGuid;
            $scope.showImageTwoReset = !$scope.showImageTwoReset;
        };

        $scope.resetLinkOne = function () {
            $scope.properties.ActionName.PropertyValue = "";
            $scope.properties.ActionTitle.PropertyValue = "";
            $scope.properties.ActionTarget.PropertyValue = "";
            $scope.properties.LinkedUrl.PropertyValue = "";
            $scope.properties.LinkedPageId.PropertyValue = $scope.DefaultGuid;
            $scope.showLinkOneReset = !$scope.showLinkOneReset;
        };

        $scope.resetLinkTwo = function () {
            $scope.properties.ActionNameTwo.PropertyValue = "";
            $scope.properties.ActionTitleTwo.PropertyValue = "";
            $scope.properties.ActionTargetTwo.PropertyValue = "";
            $scope.properties.LinkedUrlTwo.PropertyValue = "";
            $scope.properties.LinkedPageIdTwo.PropertyValue = $scope.DefaultGuid;
            $scope.showLinkTwoReset = !$scope.showLinkTwoReset;
        };

    }]);
})(jQuery);