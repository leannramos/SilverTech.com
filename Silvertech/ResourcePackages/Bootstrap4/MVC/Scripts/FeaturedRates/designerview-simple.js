(function () {
    var simpleViewModule = angular.module('designer');
    angular.module('designer').requires.push('sfServices');
    angular.module('designer').requires.push('sfSelectors');


    simpleViewModule.controller('SimpleCtrl', ['$scope', '$filter', 'propertyService',
        'serverContext', 'serviceHelper',
        function ($scope, $filter, propertyService,
            serverContext, serviceHelper) {

            $scope.feedback.showLoadingIndicator = true;

            var tablserviceUrl = serverContext.getRootedUrl('api/default/tables');
            var rowserviceUrl = serverContext.getRootedUrl('/api/default/rowdatas');

            $scope.tables = null;
            $scope.rows = null;
            $scope.columns = null;

            $scope.getTables = function (itemId, param, filter) {
                var url = tablserviceUrl;
                if (itemId && itemId !== serviceHelper.emptyGuid()) {
                    url = url + '(' + itemId + ')';
                }

                if (param) {
                    url = url + '/' + param;
                }

                if (filter) {
                    url = url + '?' + filter;
                }

                return serviceHelper.getResource(url);
            };

            $scope.getRows = function (parentId) {

                var url = rowserviceUrl;
                if (parentId && parentId !== serviceHelper.emptyGuid()) {
                    url = url + '?$filter=ParentId eq ' + parentId;
                }


                return serviceHelper.getResource(url);
            };

            $scope.$watch('table', function (newVal) {
                if (newVal) {
                    $scope.properties.tableId.PropertyValue = newVal.Id;

                    var columnsPromise = $scope.getTables(newVal.Id, null, "$expand=Columns&$select=Columns").get().$promise;
                    columnsPromise.then(function (data) {

                        $scope.columns = data.Columns;

                        if ($scope.properties.ColumnId && $scope.properties.ColumnId.PropertyValue !== serviceHelper.emptyGuid()) {
                            $scope.column = $scope.findById($scope.columns, $scope.properties.ColumnId.PropertyValue);
                        }
                    });



                    $scope.feedback.showLoadingIndicator = true;
                    var rowsPromise = $scope.getRows(newVal.Id).get().$promise;
                    rowsPromise.then(function (data) {
                        $scope.rows = data.value;

                        if ($scope.properties.rowId && $scope.properties.rowId.PropertyValue !== serviceHelper.emptyGuid()) {
                            $scope.row = $scope.findById($scope.rows, $scope.properties.rowId.PropertyValue);
                        }
                    })
                        .finally(function () {
                            $scope.feedback.showLoadingIndicator = false;
                        });
                }
                else if (newVal === null) { // use === to verify newVal is not undefined
                    // clear the data if no item is selected
                    $scope.properties.tableId.PropertyValue = serviceHelper.emptyGuid();
                    $scope.properties.rowId.PropertyValue = serviceHelper.emptyGuid();
                    $scope.rows = null;
                    $scope.row = null;
                }
            });

            $scope.$watch('row', function (newVal) {
                if (newVal) {
                    $scope.properties.rowId.PropertyValue = newVal.Id;
                }
                else if (newVal === null) { // use === to verify newVal is not undefined
                    $scope.properties.rowId.PropertyValue = serviceHelper.emptyGuid();
                    $scope.row = null;
                }
            });

            $scope.$watch('column', function (newVal) {
                if (newVal) {
                    $scope.properties.ColumnId.PropertyValue = newVal.Id;

                }
                else if (newVal === null) { // use === to verify newVal is not undefined
                    $scope.properties.ColumnId.PropertyValue = serviceHelper.emptyGuid();
                    $scope.column = null;
                }
            });

            // filters using the $filter
            // returns the first item of the filtered collection
            $scope.findById = function (items, id) {
                var item = $filter('filter')(items, { Id: id })[0];
                return item;
            };

            propertyService.get()
                .then(function (data) {
                    if (data) {
                        var getTablessPromise = $scope.getTables(null, null, '$select=Id,Title&$expand=Columns').get().$promise;
                        getTablessPromise.then(function (data) {
                            $scope.tables = data.value;

                            if ($scope.properties.tableId && $scope.properties.tableId.PropertyValue !== serviceHelper.emptyGuid()) {
                                $scope.table = $scope.findById($scope.tables, $scope.properties.tableId.PropertyValue);
                            }
                        })
                            .finally(function () {
                                $scope.feedback.showLoadingIndicator = false;
                            });

                        $scope.properties = propertyService.toAssociativeArray(data.Items);
                    }
                },
                    function (data) {
                        $scope.feedback.showError = true;
                        if (data)
                            $scope.feedback.errorMessage = data.Detail;
                    })
                .finally(function () {
                });
        }]);
})();