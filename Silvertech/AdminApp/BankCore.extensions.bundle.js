webpackJsonp([1000], {

/***/ 100000:
/*!*******************************!*\
  !*** ./__extensions_index.ts ***!
  \*******************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports, __webpack_require__) {

            "use strict";

            Object.defineProperty(exports, "__esModule", { value: true });
            //import { GridExtenderModule } from "./grid-extender";
            var custom_fields_1 = __webpack_require__(/*! ./custom-fields */ 100013);
            var commands_extender_1 = __webpack_require__(/*! ./commands-extender */ 100022);
            var toolbar_extender_1 = __webpack_require__(/*! ./toolbar-extender */ 100028);
            var index_1 = __webpack_require__(/*! ./allow-css/index */ 100036);
            /**
             * The entry point of the extensions. Each extension bundle needs to have exactly one export
             * of the Extensions interface and it needs to be placed in the __extensions_index file.
             * Here all of the NgModules are returned and are loaded into the main module.
             */
            var SamplesExtension = /** @class */ (function () {
                function SamplesExtension() {
                }
                /**
                 * On application bootstrap this method is called to get all extensions as angular modules.
                 */
                SamplesExtension.prototype.getNgModules = function () {
                    return [
                        // GridExtenderModule,
                        custom_fields_1.CustomFieldsModule,
                        commands_extender_1.CommandsExtenderModule,
                        toolbar_extender_1.ToolbarExtenderModule,
                        index_1.AllowCSSModule
                    ];
                };
                return SamplesExtension;
            }());
            exports.SamplesExtension = SamplesExtension;


            /***/
}),

/***/ 100001:
/*!*******************************************************************************!*\
  !*** delegated ./node_modules/tslib/tslib.es6.js from dll-reference adminapp ***!
  \*******************************************************************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports, __webpack_require__) {

            module.exports = __webpack_require__(0).__iris_require__('./node_modules/tslib/tslib.es6.js')

            /***/
}),

/***/ 100002:
/*!***************************************************************************************!*\
  !*** delegated ./node_modules/@angular/core/esm5/core.js from dll-reference adminapp ***!
  \***************************************************************************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports, __webpack_require__) {

            module.exports = __webpack_require__(0).__iris_require__('./node_modules/@angular/core/esm5/core.js')

            /***/
}),

/***/ 100003:
/*!***************************!*\
  !*** external "adminapp" ***!
  \***************************/
/*! no static exports found */
/***/ (function (module, exports) {

            module.exports = adminapp;

            /***/
}),

/***/ 100004:
/*!*****************************************************************************************************************!*\
  !*** delegated ./node_modules/progress-sitefinity-adminapp-sdk/app/api/v1/index.js from dll-reference adminapp ***!
  \*****************************************************************************************************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports, __webpack_require__) {

            module.exports = __webpack_require__(0).__iris_require__('./node_modules/progress-sitefinity-adminapp-sdk/app/api/v1/index.js')

            /***/
}),

/***/ 100005:
/*!*******************************************************************************************!*\
  !*** delegated ./node_modules/@angular/common/esm5/common.js from dll-reference adminapp ***!
  \*******************************************************************************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports, __webpack_require__) {

            module.exports = __webpack_require__(0).__iris_require__('./node_modules/@angular/common/esm5/common.js')

            /***/
}),

/***/ 100006:
/*!*****************************************************************************************!*\
  !*** delegated ./node_modules/@angular/common/esm5/http.js from dll-reference adminapp ***!
  \*****************************************************************************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports, __webpack_require__) {

            module.exports = __webpack_require__(0).__iris_require__('./node_modules/@angular/common/esm5/http.js')

            /***/
}),

/***/ 100007:
/*!*************************************************!*\
  !*** ./node_modules/css-loader/lib/css-base.js ***!
  \*************************************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports) {

            /*
                MIT License http://www.opensource.org/licenses/mit-license.php
                Author Tobias Koppers @sokra
            */
            // css base code, injected by the css-loader
            module.exports = function (useSourceMap) {
                var list = [];

                // return the list of modules as css string
                list.toString = function toString() {
                    return this.map(function (item) {
                        var content = cssWithMappingToString(item, useSourceMap);
                        if (item[2]) {
                            return "@media " + item[2] + "{" + content + "}";
                        } else {
                            return content;
                        }
                    }).join("");
                };

                // import a list of modules into the list
                list.i = function (modules, mediaQuery) {
                    if (typeof modules === "string")
                        modules = [[null, modules, ""]];
                    var alreadyImportedModules = {};
                    for (var i = 0; i < this.length; i++) {
                        var id = this[i][0];
                        if (typeof id === "number")
                            alreadyImportedModules[id] = true;
                    }
                    for (i = 0; i < modules.length; i++) {
                        var item = modules[i];
                        // skip already imported module
                        // this implementation is not 100% perfect for weird media query combinations
                        //  when a module is imported multiple times with different media queries.
                        //  I hope this will never occur (Hey this way we have smaller bundles)
                        if (typeof item[0] !== "number" || !alreadyImportedModules[item[0]]) {
                            if (mediaQuery && !item[2]) {
                                item[2] = mediaQuery;
                            } else if (mediaQuery) {
                                item[2] = "(" + item[2] + ") and (" + mediaQuery + ")";
                            }
                            list.push(item);
                        }
                    }
                };
                return list;
            };

            function cssWithMappingToString(item, useSourceMap) {
                var content = item[1] || '';
                var cssMapping = item[3];
                if (!cssMapping) {
                    return content;
                }

                if (useSourceMap && typeof btoa === 'function') {
                    var sourceMapping = toComment(cssMapping);
                    var sourceURLs = cssMapping.sources.map(function (source) {
                        return '/*# sourceURL=' + cssMapping.sourceRoot + source + ' */'
                    });

                    return [content].concat(sourceURLs).concat([sourceMapping]).join('\n');
                }

                return [content].join('\n');
            }

            // Adapted from convert-source-map (MIT)
            function toComment(sourceMap) {
                // eslint-disable-next-line no-undef
                var base64 = btoa(unescape(encodeURIComponent(JSON.stringify(sourceMap))));
                var data = 'sourceMappingURL=data:application/json;charset=utf-8;base64,' + base64;

                return '/*# ' + data + ' */';
            }


            /***/
}),

/***/ 100008:
/*!*******************************************************************************************!*\
  !*** delegated ./node_modules/@angular/router/esm5/router.js from dll-reference adminapp ***!
  \*******************************************************************************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports, __webpack_require__) {

            module.exports = __webpack_require__(0).__iris_require__('./node_modules/@angular/router/esm5/router.js')

            /***/
}),

/***/ 100009:
/*!**********************************************************!*\
  !*** ./custom-fields/custom-field-readonly.component.ts ***!
  \**********************************************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports, __webpack_require__) {

            "use strict";

            Object.defineProperty(exports, "__esModule", { value: true });
            var tslib_1 = __webpack_require__(/*! tslib */ 100001);
            var core_1 = __webpack_require__(/*! @angular/core */ 100002);
            var v1_1 = __webpack_require__(/*! progress-sitefinity-adminapp-sdk/app/api/v1 */ 100004);
            /* The component used to display the field in read only mode.
             * One can use inline template & styles OR templateUrl & styleUrls OR a mixture of that like here. See the -write.component.ts version for the write mode type.
             */
            var CustomInputReadonlyComponent = /** @class */ (function (_super) {
                tslib_1.__extends(CustomInputReadonlyComponent, _super);
                function CustomInputReadonlyComponent() {
                    return _super !== null && _super.apply(this, arguments) || this;
                }
                CustomInputReadonlyComponent.prototype.ngOnInit = function () {
                    var dbvalue = this.getValue();
                    $.ajax({
                        type: 'POST',
                        url: "/Sitefinity/Services/Rates/RatesColumnService.svc/GetColumns/",
                        data: JSON.stringify({ CurrentUrlPath: window.location.href, RowData: dbvalue }),
                        contentType: 'application/json;',
                        dataType: 'json',
                        async: false,
                        success: function (data) {
                            debugger;
                            this.cols = data;
                            debugger;
                        }
                    });
                };
                CustomInputReadonlyComponent = tslib_1.__decorate([
                    core_1.Component({
                        template: __webpack_require__(/*! ./custom-field-readonly.component.html */ 100015),
                        styles: ["\n        .custom-title-input {\n            border: 0;\n            padding: 0;\n            font-weight: 700;\n            font-size: 3em;\n            width: 100%;\n            color: #666;\n        }\n        "]
                    })
                ], CustomInputReadonlyComponent);
                return CustomInputReadonlyComponent;
            }(v1_1.FieldBase));
            exports.CustomInputReadonlyComponent = CustomInputReadonlyComponent;


            /***/
}),

/***/ 100010:
/*!*******************************************************!*\
  !*** ./custom-fields/custom-field-write.component.ts ***!
  \*******************************************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports, __webpack_require__) {

            "use strict";

            Object.defineProperty(exports, "__esModule", { value: true });
            var tslib_1 = __webpack_require__(/*! tslib */ 100001);
            var core_1 = __webpack_require__(/*! @angular/core */ 100002);
            var core_2 = __webpack_require__(/*! @angular/core */ 100002);
            var v1_1 = __webpack_require__(/*! progress-sitefinity-adminapp-sdk/app/api/v1 */ 100004);
            var platform_browser_1 = __webpack_require__(/*! @angular/platform-browser */ 100016);
            var common_1 = __webpack_require__(/*! @angular/common */ 100005);
            var http_1 = __webpack_require__(/*! @angular/common/http */ 100006);
            /**
             * The component used to display the field in write mode.
             * One can use inline temlpate & styles OR templateUrl & styleUrls, like here OR mixture of that. See -readonly.component.ts version for the read mode type.
             */
            var CustomInputWriteComponent = /** @class */ (function (_super) {
                tslib_1.__extends(CustomInputWriteComponent, _super);
                function CustomInputWriteComponent(http) {
                    var _this = _super.call(this) || this;
                    _this.http = http;
                    _this._options = { headers: new http_1.HttpHeaders({ 'Content-Type': 'application/json' }) };
                    return _this;
                }
                CustomInputWriteComponent_1 = CustomInputWriteComponent;
                CustomInputWriteComponent.prototype.ngOnInit = function () {
                    var _this = this;
                    var dbvalue = null;
                    try {
                        dbvalue = JSON.parse(this.getValue());
                    }
                    catch (err) {
                        console.log("Unable to parse dbValue");
                    }
                    var postData = JSON.stringify({ CurrentUrlPath: window.location.href, RowData: dbvalue });
                    //this.http.post('http://localhost:60876/Sitefinity/Services/Rates/RatesColumnService.svc/GetColumns/',postData).subscribe();
                    this.http.post('/Sitefinity/Services/Rates/RatesColumnService.svc/GetColumns/', postData, this._options).subscribe(function (data) {
                        _this.cols = data;
                    });
                };
                //save the value from all textboxes separated by +
                CustomInputWriteComponent.prototype.processChange = function () {
                    var blob = JSON.stringify(this.cols);
                    this.writeValue(blob);
                };
                CustomInputWriteComponent = CustomInputWriteComponent_1 = tslib_1.__decorate([
                    core_2.NgModule({
                        declarations: [
                            CustomInputWriteComponent_1
                        ],
                        imports: [
                            platform_browser_1.BrowserModule, common_1.CommonModule, http_1.HttpClient, http_1.HttpHeaders
                        ],
                    }),
                    core_1.Component({
                        template: __webpack_require__(/*! ./custom-field-write.component.html */ 100017),
                        styles: [__webpack_require__(/*! ./custom-field-write.component.css */ 100018)],
                    }),
                    tslib_1.__metadata("design:paramtypes", [http_1.HttpClient])
                ], CustomInputWriteComponent);
                return CustomInputWriteComponent;
                var CustomInputWriteComponent_1;
            }(v1_1.FieldBase));
            exports.CustomInputWriteComponent = CustomInputWriteComponent;


            /***/
}),

/***/ 100011:
/*!****************************************************!*\
  !*** ./commands-extender/print-preview.command.ts ***!
  \****************************************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports, __webpack_require__) {

            "use strict";

            Object.defineProperty(exports, "__esModule", { value: true });
            var tslib_1 = __webpack_require__(/*! tslib */ 100001);
            var core_1 = __webpack_require__(/*! @angular/core */ 100002);
            var router_1 = __webpack_require__(/*! @angular/router */ 100008);
            var Observable_1 = __webpack_require__(/*! rxjs/Observable */ 100025);
            /**
             * Serves as a command that gets invoked when the print preview action
             * in the actions menu is clicked.
             */
            var PrintPreviewCommand = /** @class */ (function () {
                /**
                 * Initializes a new instance of the PrintPreviewCommand.
                 * @param router The router that is used to navigate.
                 */
                function PrintPreviewCommand(router) {
                    this.router = router;
                }
                /**
                 * This method gets invoked when the print preview action is clicked.
                 * @param context The context that contains the data item and the custom properties from the CommandProvider.
                 */
                PrintPreviewCommand.prototype.execute = function (context) {
                    // get the data item from the context.
                    var dataItem = context.data.dataItem;
                    // construct the query params so the component that we navigate to
                    // will know which data item to fetch
                    var currentQueryParams = {
                        entitySet: dataItem.metadata.setName,
                        id: dataItem.key,
                        provider: dataItem.provider
                    };
                    // navigate to the print-preview route using the query params.
                    // return an observable here, because this might be a time consuming operation
                    var url = "/print-preview";
                    var navPromise = this.router.navigate([url], { queryParams: currentQueryParams });
                    return Observable_1.Observable.fromPromise(navPromise);
                };
                PrintPreviewCommand = tslib_1.__decorate([
                    core_1.Injectable(),
                    tslib_1.__metadata("design:paramtypes", [router_1.Router])
                ], PrintPreviewCommand);
                return PrintPreviewCommand;
            }());
            exports.PrintPreviewCommand = PrintPreviewCommand;


            /***/
}),

/***/ 100012:
/*!****************************************************!*\
  !*** ./node_modules/style-loader/lib/addStyles.js ***!
  \****************************************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports, __webpack_require__) {

            /*
                MIT License http://www.opensource.org/licenses/mit-license.php
                Author Tobias Koppers @sokra
            */

            var stylesInDom = {};

            var memoize = function (fn) {
                var memo;

                return function () {
                    if (typeof memo === "undefined") memo = fn.apply(this, arguments);
                    return memo;
                };
            };

            var isOldIE = memoize(function () {
                // Test for IE <= 9 as proposed by Browserhacks
                // @see http://browserhacks.com/#hack-e71d8692f65334173fee715c222cb805
                // Tests for existence of standard globals is to allow style-loader
                // to operate correctly into non-standard environments
                // @see https://github.com/webpack-contrib/style-loader/issues/177
                return window && document && document.all && !window.atob;
            });

            var getTarget = function (target) {
                return document.querySelector(target);
            };

            var getElement = (function (fn) {
                var memo = {};

                return function (target) {
                    // If passing function in options, then use it for resolve "head" element.
                    // Useful for Shadow Root style i.e
                    // {
                    //   insertInto: function () { return document.querySelector("#foo").shadowRoot }
                    // }
                    if (typeof target === 'function') {
                        return target();
                    }
                    if (typeof memo[target] === "undefined") {
                        var styleTarget = getTarget.call(this, target);
                        // Special case to return head of iframe instead of iframe itself
                        if (window.HTMLIFrameElement && styleTarget instanceof window.HTMLIFrameElement) {
                            try {
                                // This will throw an exception if access to iframe is blocked
                                // due to cross-origin restrictions
                                styleTarget = styleTarget.contentDocument.head;
                            } catch (e) {
                                styleTarget = null;
                            }
                        }
                        memo[target] = styleTarget;
                    }
                    return memo[target]
                };
            })();

            var singleton = null;
            var singletonCounter = 0;
            var stylesInsertedAtTop = [];

            var fixUrls = __webpack_require__(/*! ./urls */ 100032);

            module.exports = function (list, options) {
                if (typeof DEBUG !== "undefined" && DEBUG) {
                    if (typeof document !== "object") throw new Error("The style-loader cannot be used in a non-browser environment");
                }

                options = options || {};

                options.attrs = typeof options.attrs === "object" ? options.attrs : {};

                // Force single-tag solution on IE6-9, which has a hard limit on the # of <style>
                // tags it will allow on a page
                if (!options.singleton && typeof options.singleton !== "boolean") options.singleton = isOldIE();

                // By default, add <style> tags to the <head> element
                if (!options.insertInto) options.insertInto = "head";

                // By default, add <style> tags to the bottom of the target
                if (!options.insertAt) options.insertAt = "bottom";

                var styles = listToStyles(list, options);

                addStylesToDom(styles, options);

                return function update(newList) {
                    var mayRemove = [];

                    for (var i = 0; i < styles.length; i++) {
                        var item = styles[i];
                        var domStyle = stylesInDom[item.id];

                        domStyle.refs--;
                        mayRemove.push(domStyle);
                    }

                    if (newList) {
                        var newStyles = listToStyles(newList, options);
                        addStylesToDom(newStyles, options);
                    }

                    for (var i = 0; i < mayRemove.length; i++) {
                        var domStyle = mayRemove[i];

                        if (domStyle.refs === 0) {
                            for (var j = 0; j < domStyle.parts.length; j++) domStyle.parts[j]();

                            delete stylesInDom[domStyle.id];
                        }
                    }
                };
            };

            function addStylesToDom(styles, options) {
                for (var i = 0; i < styles.length; i++) {
                    var item = styles[i];
                    var domStyle = stylesInDom[item.id];

                    if (domStyle) {
                        domStyle.refs++;

                        for (var j = 0; j < domStyle.parts.length; j++) {
                            domStyle.parts[j](item.parts[j]);
                        }

                        for (; j < item.parts.length; j++) {
                            domStyle.parts.push(addStyle(item.parts[j], options));
                        }
                    } else {
                        var parts = [];

                        for (var j = 0; j < item.parts.length; j++) {
                            parts.push(addStyle(item.parts[j], options));
                        }

                        stylesInDom[item.id] = { id: item.id, refs: 1, parts: parts };
                    }
                }
            }

            function listToStyles(list, options) {
                var styles = [];
                var newStyles = {};

                for (var i = 0; i < list.length; i++) {
                    var item = list[i];
                    var id = options.base ? item[0] + options.base : item[0];
                    var css = item[1];
                    var media = item[2];
                    var sourceMap = item[3];
                    var part = { css: css, media: media, sourceMap: sourceMap };

                    if (!newStyles[id]) styles.push(newStyles[id] = { id: id, parts: [part] });
                    else newStyles[id].parts.push(part);
                }

                return styles;
            }

            function insertStyleElement(options, style) {
                var target = getElement(options.insertInto)

                if (!target) {
                    throw new Error("Couldn't find a style target. This probably means that the value for the 'insertInto' parameter is invalid.");
                }

                var lastStyleElementInsertedAtTop = stylesInsertedAtTop[stylesInsertedAtTop.length - 1];

                if (options.insertAt === "top") {
                    if (!lastStyleElementInsertedAtTop) {
                        target.insertBefore(style, target.firstChild);
                    } else if (lastStyleElementInsertedAtTop.nextSibling) {
                        target.insertBefore(style, lastStyleElementInsertedAtTop.nextSibling);
                    } else {
                        target.appendChild(style);
                    }
                    stylesInsertedAtTop.push(style);
                } else if (options.insertAt === "bottom") {
                    target.appendChild(style);
                } else if (typeof options.insertAt === "object" && options.insertAt.before) {
                    var nextSibling = getElement(options.insertInto + " " + options.insertAt.before);
                    target.insertBefore(style, nextSibling);
                } else {
                    throw new Error("[Style Loader]\n\n Invalid value for parameter 'insertAt' ('options.insertAt') found.\n Must be 'top', 'bottom', or Object.\n (https://github.com/webpack-contrib/style-loader#insertat)\n");
                }
            }

            function removeStyleElement(style) {
                if (style.parentNode === null) return false;
                style.parentNode.removeChild(style);

                var idx = stylesInsertedAtTop.indexOf(style);
                if (idx >= 0) {
                    stylesInsertedAtTop.splice(idx, 1);
                }
            }

            function createStyleElement(options) {
                var style = document.createElement("style");

                options.attrs.type = "text/css";

                addAttrs(style, options.attrs);
                insertStyleElement(options, style);

                return style;
            }

            function createLinkElement(options) {
                var link = document.createElement("link");

                options.attrs.type = "text/css";
                options.attrs.rel = "stylesheet";

                addAttrs(link, options.attrs);
                insertStyleElement(options, link);

                return link;
            }

            function addAttrs(el, attrs) {
                Object.keys(attrs).forEach(function (key) {
                    el.setAttribute(key, attrs[key]);
                });
            }

            function addStyle(obj, options) {
                var style, update, remove, result;

                // If a transform function was defined, run it on the css
                if (options.transform && obj.css) {
                    result = options.transform(obj.css);

                    if (result) {
                        // If transform returns a value, use that instead of the original css.
                        // This allows running runtime transformations on the css.
                        obj.css = result;
                    } else {
                        // If the transform function returns a falsy value, don't add this css.
                        // This allows conditional loading of css
                        return function () {
                            // noop
                        };
                    }
                }

                if (options.singleton) {
                    var styleIndex = singletonCounter++;

                    style = singleton || (singleton = createStyleElement(options));

                    update = applyToSingletonTag.bind(null, style, styleIndex, false);
                    remove = applyToSingletonTag.bind(null, style, styleIndex, true);

                } else if (
                    obj.sourceMap &&
                    typeof URL === "function" &&
                    typeof URL.createObjectURL === "function" &&
                    typeof URL.revokeObjectURL === "function" &&
                    typeof Blob === "function" &&
                    typeof btoa === "function"
                ) {
                    style = createLinkElement(options);
                    update = updateLink.bind(null, style, options);
                    remove = function () {
                        removeStyleElement(style);

                        if (style.href) URL.revokeObjectURL(style.href);
                    };
                } else {
                    style = createStyleElement(options);
                    update = applyToTag.bind(null, style);
                    remove = function () {
                        removeStyleElement(style);
                    };
                }

                update(obj);

                return function updateStyle(newObj) {
                    if (newObj) {
                        if (
                            newObj.css === obj.css &&
                            newObj.media === obj.media &&
                            newObj.sourceMap === obj.sourceMap
                        ) {
                            return;
                        }

                        update(obj = newObj);
                    } else {
                        remove();
                    }
                };
            }

            var replaceText = (function () {
                var textStore = [];

                return function (index, replacement) {
                    textStore[index] = replacement;

                    return textStore.filter(Boolean).join('\n');
                };
            })();

            function applyToSingletonTag(style, index, remove, obj) {
                var css = remove ? "" : obj.css;

                if (style.styleSheet) {
                    style.styleSheet.cssText = replaceText(index, css);
                } else {
                    var cssNode = document.createTextNode(css);
                    var childNodes = style.childNodes;

                    if (childNodes[index]) style.removeChild(childNodes[index]);

                    if (childNodes.length) {
                        style.insertBefore(cssNode, childNodes[index]);
                    } else {
                        style.appendChild(cssNode);
                    }
                }
            }

            function applyToTag(style, obj) {
                var css = obj.css;
                var media = obj.media;

                if (media) {
                    style.setAttribute("media", media)
                }

                if (style.styleSheet) {
                    style.styleSheet.cssText = css;
                } else {
                    while (style.firstChild) {
                        style.removeChild(style.firstChild);
                    }

                    style.appendChild(document.createTextNode(css));
                }
            }

            function updateLink(link, options, obj) {
                var css = obj.css;
                var sourceMap = obj.sourceMap;

                /*
                    If convertToAbsoluteUrls isn't defined, but sourcemaps are enabled
                    and there is no publicPath defined then lets turn convertToAbsoluteUrls
                    on by default.  Otherwise default to the convertToAbsoluteUrls option
                    directly
                */
                var autoFixUrls = options.convertToAbsoluteUrls === undefined && sourceMap;

                if (options.convertToAbsoluteUrls || autoFixUrls) {
                    css = fixUrls(css);
                }

                if (sourceMap) {
                    // http://stackoverflow.com/a/26603875
                    css += "\n/*# sourceMappingURL=data:application/json;base64," + btoa(unescape(encodeURIComponent(JSON.stringify(sourceMap)))) + " */";
                }

                var blob = new Blob([css], { type: "text/css" });

                var oldSrc = link.href;

                link.href = URL.createObjectURL(blob);

                if (oldSrc) URL.revokeObjectURL(oldSrc);
            }


            /***/
}),

/***/ 100013:
/*!********************************!*\
  !*** ./custom-fields/index.ts ***!
  \********************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports, __webpack_require__) {

            "use strict";

            Object.defineProperty(exports, "__esModule", { value: true });
            var tslib_1 = __webpack_require__(/*! tslib */ 100001);
            var core_1 = __webpack_require__(/*! @angular/core */ 100002);
            var common_1 = __webpack_require__(/*! @angular/common */ 100005);
            var forms_1 = __webpack_require__(/*! @angular/forms */ 100014);
            var custom_field_readonly_component_1 = __webpack_require__(/*! ./custom-field-readonly.component */ 100009);
            var custom_field_write_component_1 = __webpack_require__(/*! ./custom-field-write.component */ 100010);
            var custom_fields_provider_1 = __webpack_require__(/*! ./custom-fields-provider */ 100020);
            /**
             * The custom fields module.
             */
            var CustomFieldsModule = /** @class */ (function () {
                function CustomFieldsModule() {
                }
                CustomFieldsModule = tslib_1.__decorate([
                    core_1.NgModule({
                        declarations: [
                            custom_field_readonly_component_1.CustomInputReadonlyComponent,
                            custom_field_write_component_1.CustomInputWriteComponent
                        ],
                        entryComponents: [
                            // The components need to be registered here as they are instantiated dynamically.
                            custom_field_readonly_component_1.CustomInputReadonlyComponent,
                            custom_field_write_component_1.CustomInputWriteComponent
                        ],
                        providers: [
                            custom_fields_provider_1.CUSTOM_FIELDS_PROVIDER
                        ],
                        imports: [forms_1.FormsModule, common_1.CommonModule]
                    })
                ], CustomFieldsModule);
                return CustomFieldsModule;
            }());
            exports.CustomFieldsModule = CustomFieldsModule;


            /***/
}),

/***/ 100014:
/*!*****************************************************************************************!*\
  !*** delegated ./node_modules/@angular/forms/esm5/forms.js from dll-reference adminapp ***!
  \*****************************************************************************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports, __webpack_require__) {

            module.exports = __webpack_require__(0).__iris_require__('./node_modules/@angular/forms/esm5/forms.js')

            /***/
}),

/***/ 100015:
/*!************************************************************!*\
  !*** ./custom-fields/custom-field-readonly.component.html ***!
  \************************************************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports) {

            module.exports = "<div ng-repeat=\"col in cols\" >\r\n\t<input [(ngModel)]=col class=\"custom-title-input\" [value]=col readonly />\r\n</div>  ";

            /***/
}),

/***/ 100016:
/*!***************************************************************************************************************!*\
  !*** delegated ./node_modules/@angular/platform-browser/esm5/platform-browser.js from dll-reference adminapp ***!
  \***************************************************************************************************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports, __webpack_require__) {

            module.exports = __webpack_require__(0).__iris_require__('./node_modules/@angular/platform-browser/esm5/platform-browser.js')

            /***/
}),

/***/ 100017:
/*!*********************************************************!*\
  !*** ./custom-fields/custom-field-write.component.html ***!
  \*********************************************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports) {

            module.exports = "<div *ngFor=\"let col of cols; let in=index\">\r\n\t <label>{{col.Label}}</label> \r\n\t <input [(ngModel)]=\"cols[in].Value\" class=\"custom-title-input\" (ngModelChange)=\"processChange()\" />\r\n</div>";

            /***/
}),

/***/ 100018:
/*!********************************************************!*\
  !*** ./custom-fields/custom-field-write.component.css ***!
  \********************************************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports, __webpack_require__) {

            // css-to-string-loader: transforms styles from css-loader to a string output

            // Get the styles
            var styles = __webpack_require__(/*! !../node_modules/css-loader!./custom-field-write.component.css */ 100019);

            if (typeof styles === 'string') {
                // Return an existing string
                module.exports = styles;
            } else {
                // Call the custom toString method from css-loader module
                module.exports = styles.toString();
            }

            /***/
}),

/***/ 100019:
/*!**********************************************************************************!*\
  !*** ./node_modules/css-loader!./custom-fields/custom-field-write.component.css ***!
  \**********************************************************************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports, __webpack_require__) {

            exports = module.exports = __webpack_require__(/*! ../node_modules/css-loader/lib/css-base.js */ 100007)(false);
            // imports


            // module
            exports.push([module.i, ".custom-title-input {\r\n    border: 0;\r\n    border-bottom: 2px dashed #38ab63;\r\n    padding: .2em;\r\n    font-weight: 700;\r\n    font-size: 3em;\r\n    width: 100%;\r\n    color: #38ab63;\r\n}\r\n", ""]);

            // exports


            /***/
}),

/***/ 100020:
/*!*************************************************!*\
  !*** ./custom-fields/custom-fields-provider.ts ***!
  \*************************************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports, __webpack_require__) {

            "use strict";

            Object.defineProperty(exports, "__esModule", { value: true });
            var tslib_1 = __webpack_require__(/*! tslib */ 100001);
            var core_1 = __webpack_require__(/*! @angular/core */ 100002);
            var v1_1 = __webpack_require__(/*! progress-sitefinity-adminapp-sdk/app/api/v1 */ 100004);
            var custom_field_readonly_component_1 = __webpack_require__(/*! ./custom-field-readonly.component */ 100009);
            var custom_field_write_component_1 = __webpack_require__(/*! ./custom-field-write.component */ 100010);
            var custom_field_settings_1 = __webpack_require__(/*! ./custom-field.settings */ 100021);
            /**
             * The fields provider provides the overridden fields back to the AdminApp.
             */
            var CustomFieldsProvider = /** @class */ (function () {
                function CustomFieldsProvider() {
                    this.customFieldsMappings = [];
                    this.registerCustomComponents();
                }
                /**
                 * This method gets called before each field is instantiated, allowing custom fields to be plugged in for any type.
                 * @param fieldRegistryKey The metadata needed to determine which field to display.
                 */
                CustomFieldsProvider.prototype.overrideField = function (fieldRegistryKey) {
                    var registration = this.findRegistration(fieldRegistryKey);
                    return registration;
                };
                /**
                 * This method finds an implmentation of the field to be overridden.
                 * @param fieldRegistryKey The metadata needed to determine which field to display.
                 */
                CustomFieldsProvider.prototype.findRegistration = function (fieldRegistryKey) {
                    for (var _i = 0, _a = this.customFieldsMappings; _i < _a.length; _i++) {
                        var pair = _a[_i];
                        if (fieldRegistryKey.fieldName === pair.key.fieldName &&
                            fieldRegistryKey.fieldType === pair.key.fieldType &&
                            fieldRegistryKey.typeName === pair.key.typeName) {
                            return pair.registration;
                        }
                    }
                    return null;
                };
                /**
                 * Initializes the custom field(component) registrations.
                 */
                CustomFieldsProvider.prototype.registerCustomComponents = function () {
                    // The field name is the name which identifies the field uniquely.
                    // The typename is the OData entity set name. It matches the url segment when navigating
                    // to the list view of the specific type.
                    var customInputKey = {
                        fieldName: "RowData",
                        fieldType: "sf-text-area",
                        typeName: "rowdatas"
                    };
                    // The result field registration that will be returened to the AdminApp.
                    var customInputRegistration = {
                        writeComponent: custom_field_write_component_1.CustomInputWriteComponent,
                        readComponent: custom_field_readonly_component_1.CustomInputReadonlyComponent,
                        settingsType: custom_field_settings_1.CustomShortTextSettings
                    };
                    var customFieldRegistrationPair = {
                        key: customInputKey,
                        registration: customInputRegistration
                    };
                    this.customFieldsMappings.push(customFieldRegistrationPair);
                };
                CustomFieldsProvider = tslib_1.__decorate([
                    core_1.Injectable(),
                    tslib_1.__metadata("design:paramtypes", [])
                ], CustomFieldsProvider);
                return CustomFieldsProvider;
            }());
            exports.CustomFieldsProvider = CustomFieldsProvider;
            /**
             * Export a 'multi' class provider so that multiple instances of the same provider can coexist.
             * This allows for more than one provider to be registered within one or more bundles.
             */
            exports.CUSTOM_FIELDS_PROVIDER = {
                provide: v1_1.FIELDS_PROVIDER_TOKEN,
                useClass: CustomFieldsProvider,
                multi: true
            };


            /***/
}),

/***/ 100021:
/*!************************************************!*\
  !*** ./custom-fields/custom-field.settings.ts ***!
  \************************************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports, __webpack_require__) {

            "use strict";

            Object.defineProperty(exports, "__esModule", { value: true });
            var tslib_1 = __webpack_require__(/*! tslib */ 100001);
            var v1_1 = __webpack_require__(/*! progress-sitefinity-adminapp-sdk/app/api/v1 */ 100004);
            /**
             * A settings object to configure the fields behaviour. This object is assigned to the field via the settings property on the FieldBase class.
             * Examples of such are validations, the title to be shown and so on.
             */
            var CustomShortTextSettings = /** @class */ (function (_super) {
                tslib_1.__extends(CustomShortTextSettings, _super);
                function CustomShortTextSettings() {
                    return _super !== null && _super.apply(this, arguments) || this;
                }
                return CustomShortTextSettings;
            }(v1_1.SettingsBase));
            exports.CustomShortTextSettings = CustomShortTextSettings;


            /***/
}),

/***/ 100022:
/*!************************************!*\
  !*** ./commands-extender/index.ts ***!
  \************************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports, __webpack_require__) {

            "use strict";

            Object.defineProperty(exports, "__esModule", { value: true });
            var tslib_1 = __webpack_require__(/*! tslib */ 100001);
            var core_1 = __webpack_require__(/*! @angular/core */ 100002);
            var router_1 = __webpack_require__(/*! @angular/router */ 100008);
            var common_1 = __webpack_require__(/*! @angular/common */ 100005);
            var http_1 = __webpack_require__(/*! @angular/common/http */ 100006);
            var commands_provider_1 = __webpack_require__(/*! ./commands-provider */ 100023);
            var print_preview_component_1 = __webpack_require__(/*! ./print-preview.component */ 100026);
            var print_preview_command_1 = __webpack_require__(/*! ./print-preview.command */ 100011);
            /**
             * The command extender module.
             */
            var CommandsExtenderModule = /** @class */ (function () {
                function CommandsExtenderModule() {
                }
                CommandsExtenderModule = tslib_1.__decorate([
                    core_1.NgModule({
                        declarations: [
                            print_preview_component_1.PrintPreviewComponent
                        ],
                        entryComponents: [
                            print_preview_component_1.PrintPreviewComponent
                        ],
                        providers: [
                            commands_provider_1.COMMANDS_PROVIDER,
                            print_preview_command_1.PrintPreviewCommand
                        ],
                        imports: [
                            router_1.RouterModule.forChild([{ path: "print-preview", component: print_preview_component_1.PrintPreviewComponent }]),
                            common_1.CommonModule,
                            http_1.HttpClientModule
                        ]
                    })
                ], CommandsExtenderModule);
                return CommandsExtenderModule;
            }());
            exports.CommandsExtenderModule = CommandsExtenderModule;


            /***/
}),

/***/ 100023:
/*!************************************************!*\
  !*** ./commands-extender/commands-provider.ts ***!
  \************************************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports, __webpack_require__) {

            "use strict";

            Object.defineProperty(exports, "__esModule", { value: true });
            var tslib_1 = __webpack_require__(/*! tslib */ 100001);
            var v1_1 = __webpack_require__(/*! progress-sitefinity-adminapp-sdk/app/api/v1 */ 100004);
            var rxjs_1 = __webpack_require__(/*! rxjs */ 100024);
            var core_1 = __webpack_require__(/*! @angular/core */ 100002);
            var print_preview_command_1 = __webpack_require__(/*! ./print-preview.command */ 100011);
            /**
             * The category name in which to place the custom commands.
             */
            var CUSTOM_CATEGORY_NAME = "Custom";
            /**
             * The command model containing the metadata of the command.
             * The ordinal indicates where to place the command in the dropdown.
             */
            var CUSTOM_COMMAND_BASE = {
                name: "Custom",
                title: "Print preview",
                ordinal: -1,
                category: CUSTOM_CATEGORY_NAME
            };
            /**
             * The category model.
             */
            var CUSTOM_CATEGORY = {
                name: CUSTOM_CATEGORY_NAME,
                title: "Custom commands"
            };
            /**
             * The command provider provides the necessary commands back to the AdminApp.
             */
            var DynamicItemIndexCommandProvider = /** @class */ (function () {
                function DynamicItemIndexCommandProvider() {
                }
                /**
                 * The method that gets invoked asking for the command models when the action menu is constructed.
                 * @param data The data needed to determine the types of command to return
                 * and where to place them - in the list or in edit mode
                 */
                DynamicItemIndexCommandProvider.prototype.getCommands = function (data) {
                    // the commands to be returned
                    var commands = [];
                    // we wish to inject this command only in the list view
                    if (data.target === v1_1.CommandsTarget.List && data.dataItem) {
                        var previewCommand = Object.assign({}, CUSTOM_COMMAND_BASE);
                        // assign an injection token or just the class
                        previewCommand.token = {
                            type: print_preview_command_1.PrintPreviewCommand,
                            // assign custom properties to be passed in the context
                            properties: {
                                dataItem: data.dataItem
                            }
                        };
                        commands.push(previewCommand);
                    }
                    // return an observable here, because generating the actions might be a time consuming operation
                    return rxjs_1.Observable.of(commands);
                };
                /**
                 * The method that gets invoked asking for the category models when the action menu is constructed.
                 * Categories are used to group similar commands in the action menu
                 * @param data The data needed to determine the types of command to return and where to place them - in the list or in edit mode
                 */
                DynamicItemIndexCommandProvider.prototype.getCategories = function (data) {
                    return rxjs_1.Observable.of([CUSTOM_CATEGORY]);
                };
                DynamicItemIndexCommandProvider = tslib_1.__decorate([
                    core_1.Injectable()
                ], DynamicItemIndexCommandProvider);
                return DynamicItemIndexCommandProvider;
            }());
            /**
             * Export a 'multi' class provider so that multiple instances of the same provider can coexist.
             * This allows for more than one provider to be registered within one or more bundles.
             */
            exports.COMMANDS_PROVIDER = {
                useClass: DynamicItemIndexCommandProvider,
                multi: true,
                provide: v1_1.COMMANDS_TOKEN
            };


            /***/
}),

/***/ 100024:
/*!***********************************************************************!*\
  !*** delegated ./node_modules/rxjs/Rx.js from dll-reference adminapp ***!
  \***********************************************************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports, __webpack_require__) {

            module.exports = __webpack_require__(0).__iris_require__('./node_modules/rxjs/Rx.js')

            /***/
}),

/***/ 100025:
/*!*******************************************************************************!*\
  !*** delegated ./node_modules/rxjs/Observable.js from dll-reference adminapp ***!
  \*******************************************************************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports, __webpack_require__) {

            module.exports = __webpack_require__(0).__iris_require__('./node_modules/rxjs/Observable.js')

            /***/
}),

/***/ 100026:
/*!******************************************************!*\
  !*** ./commands-extender/print-preview.component.ts ***!
  \******************************************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports, __webpack_require__) {

            "use strict";

            Object.defineProperty(exports, "__esModule", { value: true });
            var tslib_1 = __webpack_require__(/*! tslib */ 100001);
            var core_1 = __webpack_require__(/*! @angular/core */ 100002);
            var router_1 = __webpack_require__(/*! @angular/router */ 100008);
            var http_1 = __webpack_require__(/*! @angular/common/http */ 100006);
            var v1_1 = __webpack_require__(/*! progress-sitefinity-adminapp-sdk/app/api/v1 */ 100004);
            /**
             * A component that loads the data item from the OData rest services and displays the title of the data item.
             */
            var PrintPreviewComponent = /** @class */ (function () {
                /**
                 * Initializes a new instance of the PrintPreviewComponent
                 * @param http The http client service used for making http requests.
                 * @param route The current navigated route.
                 */
                function PrintPreviewComponent(http, route) {
                    this.http = http;
                    this.route = route;
                }
                /**
                 * ngOnInit lifecycle hook. Attached here so we can fetch the data on initialization
                 */
                PrintPreviewComponent.prototype.ngOnInit = function () {
                    var _this = this;
                    // get the route params that contain the metadata needed to load the data item
                    var routeParams = this.route.snapshot.queryParams;
                    // http prefix is dynamically replaced with the actual url of sitefinity
                    var url = v1_1.HTTP_PREFIX + "/sf/system/" + routeParams.entitySet + "(" + routeParams.id + ")" + (routeParams.provider ? "?sf_provider=" + routeParams.provider : "");
                    // dealy so there is always a minimum loading time
                    this.http.get(url).delay(500).subscribe(function (dataItem) {
                        _this.dataItem = dataItem;
                    });
                };
                PrintPreviewComponent = tslib_1.__decorate([
                    core_1.Component({
                        template: __webpack_require__(/*! ./print-preview.component.html */ 100027)
                    }),
                    tslib_1.__metadata("design:paramtypes", [http_1.HttpClient,
                    router_1.ActivatedRoute])
                ], PrintPreviewComponent);
                return PrintPreviewComponent;
            }());
            exports.PrintPreviewComponent = PrintPreviewComponent;


            /***/
}),

/***/ 100027:
/*!********************************************************!*\
  !*** ./commands-extender/print-preview.component.html ***!
  \********************************************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports) {

            module.exports = "<div style=\"position: fixed;top: 50%;left: 50%;\">\r\n    <h1 *ngIf=\"dataItem\">{{dataItem.Title}}</h1>\r\n    <h1 *ngIf=\"!dataItem\">Loading..</h1>\r\n</div>\r\n";

            /***/
}),

/***/ 100028:
/*!***********************************!*\
  !*** ./toolbar-extender/index.ts ***!
  \***********************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports, __webpack_require__) {

            "use strict";

            Object.defineProperty(exports, "__esModule", { value: true });
            var tslib_1 = __webpack_require__(/*! tslib */ 100001);
            var core_1 = __webpack_require__(/*! @angular/core */ 100002);
            var common_1 = __webpack_require__(/*! @angular/common */ 100005);
            var toolbar_items_provider_1 = __webpack_require__(/*! ./toolbar-items-provider */ 100029);
            var sitefinity_videos_toolbar_item_provider_1 = __webpack_require__(/*! ./sitefinity-videos-toolbar-item-provider */ 100033);
            /**
             * The toolbar extender module.
             */
            var ToolbarExtenderModule = /** @class */ (function () {
                function ToolbarExtenderModule() {
                }
                ToolbarExtenderModule = tslib_1.__decorate([
                    core_1.NgModule({
                        providers: [
                            toolbar_items_provider_1.EXTERNAL_TOOLBAR_ITEMS_PROVIDER,
                            sitefinity_videos_toolbar_item_provider_1.VIDEO_TOOLBAR_ITEM_PROVIDER
                        ],
                        imports: [
                            common_1.CommonModule
                        ]
                    })
                ], ToolbarExtenderModule);
                return ToolbarExtenderModule;
            }());
            exports.ToolbarExtenderModule = ToolbarExtenderModule;


            /***/
}),

/***/ 100029:
/*!****************************************************!*\
  !*** ./toolbar-extender/toolbar-items-provider.ts ***!
  \****************************************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports, __webpack_require__) {

            "use strict";

            Object.defineProperty(exports, "__esModule", { value: true });
            var tslib_1 = __webpack_require__(/*! tslib */ 100001);
            var core_1 = __webpack_require__(/*! @angular/core */ 100002);
            var v1_1 = __webpack_require__(/*! progress-sitefinity-adminapp-sdk/app/api/v1 */ 100004);
            // This is webpack specific loader syntax for injecting css as <style> tag in header
            __webpack_require__(/*! style-loader!css-loader!./toolbar-items-provider.css */ 100030);
            /**
             * A custom toolbar provider implementation for counting the words in the html editor.
             * Kendo UI Editor custom tools documentation -> https://demos.telerik.com/kendo-ui/editor/custom-tools
             */
            var CustomToolBarItemsProvider = /** @class */ (function () {
                function CustomToolBarItemsProvider() {
                }
                /**
                 * The method that gets invoked when the editor constructs the toolbar actions.
                 * @param editorHost The instance of the editor.
                 */
                CustomToolBarItemsProvider.prototype.getToolBarItems = function (editorHost) {
                    var insertSpeedbump = function () {
                        var popupHtml = '<div class="k-editor-dialog k-popup-edit-form k-edit-form-container" style="width:auto;">' +
                            '<div>' +
                            '<label style="width:35%; display:inline-block;">LinkUrl: </label><input type="text" name="URL" id="linkUrl">' +
                            '</div>' +
                            '<div>' +
                            '<label style="width:35%; display:inline-block;">LinkText: </label><input type="text" name="URL" id="linkText">' +
                            '</div>' +
                            '<div>' +
                            '<label style="width:35%; display:inline-block;">Link Ada Title: </label><input type="text" name="URL" id="linkAdaTitle">' +
                            '</div>' +
                            '<div>' +
                            '<label style="width:35%; display:inline-block;">Link Speedbump Message: </label><textarea name="URL" id="linkRedirectMessage">You have clicked on a link that will take you to a third-party website that is not related to or endorsed by American Heritage Credit Union. American Heritage Credit Union assumes no liability for the products and services, policies securities or content of third party sites accessed through www.americanheritagecu.org.</textarea>' +
                            '</div>' +
                            '<div class="k-edit-buttons k-state-default">' +
                            '<button class="k-dialog-insert k-button k-primary">Insert</button>' +
                            '<button class="k-dialog-close k-button">Cancel</button>' +
                            '</div>' +
                            '</div>';
                        var editor = editorHost.getKendoEditor();
                        //const count = editor.value() ? editor.value().split(" ").length : 0;
                        // Store the editor range object
                        // Needed for IE
                        var storedRange = editor.getRange();
                        // create a modal Window from a new DOM element
                        var popupWindow = $(popupHtml)
                            .appendTo(document.body)
                            .kendoWindow({
                                // modality is recommended in this scenario
                                modal: true,
                                width: 600,
                                resizable: false,
                                title: "Insert Speedbump",
                                // ensure opening animation
                                visible: false,
                                // remove the Window from the DOM after closing animation is finished
                                deactivate: function (e) { e.sender.destroy(); }
                            }).data("kendoWindow")
                            .center().open();
                        // insert the new content in the Editor when the Insert button is clicked
                        popupWindow.element.find(".k-dialog-insert").click(function () {
                            var speedBumpLink = '<a data-redirect-message="'
                                + popupWindow.element.find("#linkRedirectMessage").val() +
                                '" href="'
                                + popupWindow.element.find("#linkUrl").val() +
                                '" target="_blank" title="'
                                + popupWindow.element.find("#linkAdaTitle").val() +
                                '" id="speedbump-link">'
                                + popupWindow.element.find("#linkText").val() + '</a>';
                            editor.selectRange(storedRange);
                            editor.exec("inserthtml", { value: speedBumpLink });
                            //This is a hack. Every KB under the sun was attempted to fix pasteCleanup removing 'class' attributes on paste but none were functional.
                            editor.element.find('#speedbump-link').addClass('speedbump-link').removeAttr('id');
                        });
                        // close the Window when any button is clicked
                        popupWindow.element.find(".k-edit-buttons button").click(function () {
                            // detach custom event handlers to prevent memory leaks
                            popupWindow.element.find(".k-edit-buttons button").off();
                            popupWindow.close();
                        });
                    };
                    /**
                     * A custom toolbar item
                     */
                    var CUSTOM_TOOLBAR_ITEM = {
                        name: "link-vertical",
                        tooltip: "Insert Speedbump",
                        ordinal: 7,
                        exec: insertSpeedbump
                    };
                    return [CUSTOM_TOOLBAR_ITEM];
                };
                CustomToolBarItemsProvider.prototype.getToolBarItemsNamesToRemove = function () {
                    // If you want to remove some toolbar items return their names as strings in the array. Order is insignificant.
                    // Otherwise return an empty array.
                    // Example: return [ "embed" ];
                    // The above code will remove the embed toolbar item from the editor.
                    return [];
                };
                CustomToolBarItemsProvider = tslib_1.__decorate([
                    core_1.Injectable()
                ], CustomToolBarItemsProvider);
                return CustomToolBarItemsProvider;
            }());
            /**
             * Export a 'multi' class provider so that multiple instances of the same provider can coexist.
             * This allows for more than one provider to be registered within one or more bundles.
             */
            exports.EXTERNAL_TOOLBAR_ITEMS_PROVIDER = {
                multi: true,
                provide: v1_1.TOOLBARITEMS_TOKEN,
                useClass: CustomToolBarItemsProvider
            };


            /***/
}),

/***/ 100030:
/*!***********************************************************************************************************!*\
  !*** ./node_modules/style-loader!./node_modules/css-loader!./toolbar-extender/toolbar-items-provider.css ***!
  \***********************************************************************************************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports, __webpack_require__) {


            var content = __webpack_require__(/*! !../node_modules/css-loader!./toolbar-items-provider.css */ 100031);

            if (typeof content === 'string') content = [[module.i, content, '']];

            var transform;
            var insertInto;



            var options = { "hmr": true }

            options.transform = transform
            options.insertInto = undefined;

            var update = __webpack_require__(/*! ../node_modules/style-loader/lib/addStyles.js */ 100012)(content, options);

            if (content.locals) module.exports = content.locals;

            if (false) {
                module.hot.accept("!!../node_modules/css-loader/index.js!./toolbar-items-provider.css", function () {
                    var newContent = require("!!../node_modules/css-loader/index.js!./toolbar-items-provider.css");

                    if (typeof newContent === 'string') newContent = [[module.id, newContent, '']];

                    var locals = (function (a, b) {
                        var key, idx = 0;

                        for (key in a) {
                            if (!b || a[key] !== b[key]) return false;
                            idx++;
                        }

                        for (key in b) idx--;

                        return idx === 0;
                    }(content.locals, newContent.locals));

                    if (!locals) throw new Error('Aborting CSS HMR due to changed css-modules locals.');

                    update(newContent);
                });

                module.hot.dispose(function () { update(); });
            }

            /***/
}),

/***/ 100031:
/*!*******************************************************************************!*\
  !*** ./node_modules/css-loader!./toolbar-extender/toolbar-items-provider.css ***!
  \*******************************************************************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports, __webpack_require__) {

            exports = module.exports = __webpack_require__(/*! ../node_modules/css-loader/lib/css-base.js */ 100007)(false);
            // imports


            // module
            exports.push([module.i, ".k-editor-toolbar .k-i-Words-count::before {\r\n    /* Kendo UI Icons: https://docs.telerik.com/kendo-ui/styles-and-layout/icons-web */\r\n    content: \"\\E696\";\r\n}\r\n", ""]);

            // exports


            /***/
}),

/***/ 100032:
/*!***********************************************!*\
  !*** ./node_modules/style-loader/lib/urls.js ***!
  \***********************************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports) {


            /**
             * When source maps are enabled, `style-loader` uses a link element with a data-uri to
             * embed the css on the page. This breaks all relative urls because now they are relative to a
             * bundle instead of the current page.
             *
             * One solution is to only use full urls, but that may be impossible.
             *
             * Instead, this function "fixes" the relative urls to be absolute according to the current page location.
             *
             * A rudimentary test suite is located at `test/fixUrls.js` and can be run via the `npm test` command.
             *
             */

            module.exports = function (css) {
                // get current location
                var location = typeof window !== "undefined" && window.location;

                if (!location) {
                    throw new Error("fixUrls requires window.location");
                }

                // blank or null?
                if (!css || typeof css !== "string") {
                    return css;
                }

                var baseUrl = location.protocol + "//" + location.host;
                var currentDir = baseUrl + location.pathname.replace(/\/[^\/]*$/, "/");

                // convert each url(...)
                /*
                This regular expression is just a way to recursively match brackets within
                a string.
            
                 /url\s*\(  = Match on the word "url" with any whitespace after it and then a parens
                   (  = Start a capturing group
                     (?:  = Start a non-capturing group
                         [^)(]  = Match anything that isn't a parentheses
                         |  = OR
                         \(  = Match a start parentheses
                             (?:  = Start another non-capturing groups
                                 [^)(]+  = Match anything that isn't a parentheses
                                 |  = OR
                                 \(  = Match a start parentheses
                                     [^)(]*  = Match anything that isn't a parentheses
                                 \)  = Match a end parentheses
                             )  = End Group
                          *\) = Match anything and then a close parens
                      )  = Close non-capturing group
                      *  = Match anything
                   )  = Close capturing group
                 \)  = Match a close parens
            
                 /gi  = Get all matches, not the first.  Be case insensitive.
                 */
                var fixedCss = css.replace(/url\s*\(((?:[^)(]|\((?:[^)(]+|\([^)(]*\))*\))*)\)/gi, function (fullMatch, origUrl) {
                    // strip quotes (if they exist)
                    var unquotedOrigUrl = origUrl
                        .trim()
                        .replace(/^"(.*)"$/, function (o, $1) { return $1; })
                        .replace(/^'(.*)'$/, function (o, $1) { return $1; });

                    // already a full url? no change
                    if (/^(#|data:|http:\/\/|https:\/\/|file:\/\/\/|\s*$)/i.test(unquotedOrigUrl)) {
                        return fullMatch;
                    }

                    // convert the url to a full url
                    var newUrl;

                    if (unquotedOrigUrl.indexOf("//") === 0) {
                        //TODO: should we add protocol?
                        newUrl = unquotedOrigUrl;
                    } else if (unquotedOrigUrl.indexOf("/") === 0) {
                        // path should be relative to the base url
                        newUrl = baseUrl + unquotedOrigUrl; // already starts with '/'
                    } else {
                        // path should be relative to current directory
                        newUrl = currentDir + unquotedOrigUrl.replace(/^\.\//, ""); // Strip leading './'
                    }

                    // send back the fixed url(...)
                    return "url(" + JSON.stringify(newUrl) + ")";
                });

                // send back the fixed css
                return fixedCss;
            };


            /***/
}),

/***/ 100033:
/*!*********************************************************************!*\
  !*** ./toolbar-extender/sitefinity-videos-toolbar-item-provider.ts ***!
  \*********************************************************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports, __webpack_require__) {

            "use strict";

            Object.defineProperty(exports, "__esModule", { value: true });
            var tslib_1 = __webpack_require__(/*! tslib */ 100001);
            var core_1 = __webpack_require__(/*! @angular/core */ 100002);
            var v1_1 = __webpack_require__(/*! progress-sitefinity-adminapp-sdk/app/api/v1 */ 100004);
            // This is webpack specific loader syntax for injecting css as <style> tag in header
            __webpack_require__(/*! style-loader!css-loader!./sitefinity-videos-toolbar-item-provider.css */ 100034);
            var TRAILING_BREAK = "<br class='k-br'>";
            exports.ensureTrailingBreaks = function (html) {
                return "" + TRAILING_BREAK + html + TRAILING_BREAK;
            };
            /**
             * A custom toolbar provider implementation for inserting existing videos in the editor.
             * Kendo UI Editor custom tools documentation -> https://demos.telerik.com/kendo-ui/editor/custom-tools
             */
            var VideosToolbarItemProvider = /** @class */ (function () {
                function VideosToolbarItemProvider(selectorService) {
                    this.selectorService = selectorService;
                }
                VideosToolbarItemProvider.prototype.getToolBarItems = function (editorHost) {
                    var _this = this;
                    var CUSTOM_TOOLBAR_ITEM = {
                        name: "Sitefinity-videos",
                        tooltip: "Sitefinity videos",
                        ordinal: 6,
                        exec: function () {
                            var editor = editorHost.getKendoEditor();
                            // Save editor's current range, so we can insert
                            // later the HTML at this position.
                            var currentRange = editor.getRange();
                            var selectorOptions = {
                                multiple: true
                            };
                            // open the selector and subscribe to the result
                            _this.selectorService.openVideoLibrarySelector(selectorOptions).subscribe(function (videos) {
                                if (videos.length) {
                                    // Restore editor's saved position.
                                    editor.selectRange(currentRange);
                                    videos.forEach(function (video) {
                                        var videoElement = document.createElement("video");
                                        // Disable video playing, but show controls,
                                        // so the video can be playable on the frontend.
                                        videoElement.contentEditable = "false";
                                        videoElement.src = video.url;
                                        videoElement.setAttribute("controls", "true");
                                        // Insert the HTML and trigger editor's change, so the
                                        // HTML can be saved.
                                        editor.paste(exports.ensureTrailingBreaks(videoElement.outerHTML));
                                        editor.trigger("change");
                                    });
                                }
                            });
                        }
                    };
                    return [CUSTOM_TOOLBAR_ITEM];
                };
                VideosToolbarItemProvider.prototype.getToolBarItemsNamesToRemove = function () {
                    // If you want to remove some toolbar items return their names as strings in the array. Order is insignificant.
                    // Otherwise return an empty array.
                    // Example: return [ "embed" ];
                    // The above code will remove the embed toolbar item from the editor.
                    return [];
                };
                VideosToolbarItemProvider = tslib_1.__decorate([
                    core_1.Injectable(),
                    tslib_1.__param(0, core_1.Inject(v1_1.SELECTOR_SERVICE)),
                    tslib_1.__metadata("design:paramtypes", [Object])
                ], VideosToolbarItemProvider);
                return VideosToolbarItemProvider;
            }());
            /**
             * Export a 'multi' class provider so that multiple instances of the same provider can coexist.
             * This allows for more than one provider to be registered within one or more bundles.
             */
            exports.VIDEO_TOOLBAR_ITEM_PROVIDER = {
                multi: true,
                provide: v1_1.TOOLBARITEMS_TOKEN,
                useClass: VideosToolbarItemProvider
            };


            /***/
}),

/***/ 100034:
/*!****************************************************************************************************************************!*\
  !*** ./node_modules/style-loader!./node_modules/css-loader!./toolbar-extender/sitefinity-videos-toolbar-item-provider.css ***!
  \****************************************************************************************************************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports, __webpack_require__) {


            var content = __webpack_require__(/*! !../node_modules/css-loader!./sitefinity-videos-toolbar-item-provider.css */ 100035);

            if (typeof content === 'string') content = [[module.i, content, '']];

            var transform;
            var insertInto;



            var options = { "hmr": true }

            options.transform = transform
            options.insertInto = undefined;

            var update = __webpack_require__(/*! ../node_modules/style-loader/lib/addStyles.js */ 100012)(content, options);

            if (content.locals) module.exports = content.locals;

            if (false) {
                module.hot.accept("!!../node_modules/css-loader/index.js!./sitefinity-videos-toolbar-item-provider.css", function () {
                    var newContent = require("!!../node_modules/css-loader/index.js!./sitefinity-videos-toolbar-item-provider.css");

                    if (typeof newContent === 'string') newContent = [[module.id, newContent, '']];

                    var locals = (function (a, b) {
                        var key, idx = 0;

                        for (key in a) {
                            if (!b || a[key] !== b[key]) return false;
                            idx++;
                        }

                        for (key in b) idx--;

                        return idx === 0;
                    }(content.locals, newContent.locals));

                    if (!locals) throw new Error('Aborting CSS HMR due to changed css-modules locals.');

                    update(newContent);
                });

                module.hot.dispose(function () { update(); });
            }

            /***/
}),

/***/ 100035:
/*!************************************************************************************************!*\
  !*** ./node_modules/css-loader!./toolbar-extender/sitefinity-videos-toolbar-item-provider.css ***!
  \************************************************************************************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports, __webpack_require__) {

            exports = module.exports = __webpack_require__(/*! ../node_modules/css-loader/lib/css-base.js */ 100007)(false);
            // imports


            // module
            exports.push([module.i, ".k-editor-toolbar .k-i-Sitefinity-videos::before {\r\n    /* Kendo UI Icons: https://docs.telerik.com/kendo-ui/styles-and-layout/icons-web */\r\n    content: \"\\E659\";\r\n}\r\n", ""]);

            // exports


            /***/
}),

/***/ 100036:
/*!****************************!*\
  !*** ./allow-css/index.ts ***!
  \****************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports, __webpack_require__) {

            "use strict";

            Object.defineProperty(exports, "__esModule", { value: true });
            var tslib_1 = __webpack_require__(/*! tslib */ 100001);
            var core_1 = __webpack_require__(/*! @angular/core */ 100002);
            var common_1 = __webpack_require__(/*! @angular/common */ 100005);
            var allow_css_provider_1 = __webpack_require__(/*! ./allow-css.provider */ 100037);
            /**
             * The toolbar extender module.
             */
            var AllowCSSModule = /** @class */ (function () {
                function AllowCSSModule() {
                }
                AllowCSSModule = tslib_1.__decorate([
                    core_1.NgModule({
                        providers: [
                            allow_css_provider_1.ALLOW_CSS_PROVIDER
                        ],
                        imports: [common_1.CommonModule]
                    })
                ], AllowCSSModule);
                return AllowCSSModule;
            }());
            exports.AllowCSSModule = AllowCSSModule;


            /***/
}),

/***/ 100037:
/*!*****************************************!*\
  !*** ./allow-css/allow-css.provider.ts ***!
  \*****************************************/
/*! no static exports found */
/*! all exports used */
/***/ (function (module, exports, __webpack_require__) {

            "use strict";

            Object.defineProperty(exports, "__esModule", { value: true });
            var tslib_1 = __webpack_require__(/*! tslib */ 100001);
            var core_1 = __webpack_require__(/*! @angular/core */ 100002);
            var v1_1 = __webpack_require__(/*! progress-sitefinity-adminapp-sdk/app/api/v1 */ 100004);
            var AllowCSSProvider = /** @class */ (function () {
                function AllowCSSProvider() {
                }
                AllowCSSProvider.prototype.getToolBarItems = function (editorHost) {
                    return [];
                };
                AllowCSSProvider.prototype.getToolBarItemsNamesToRemove = function () {
                    return [];
                };
                AllowCSSProvider.prototype.configureEditor = function (configuration) {
                    return configuration;
                };
                AllowCSSProvider = tslib_1.__decorate([
                    core_1.Injectable()
                ], AllowCSSProvider);
                return AllowCSSProvider;
            }());
            exports.ALLOW_CSS_PROVIDER = {
                multi: true,
                provide: v1_1.TOOLBARITEMS_TOKEN,
                useClass: AllowCSSProvider
            };


            /***/
})

}, [100000]);
//# sourceMappingURL=sample.extensions.bundle.js.map