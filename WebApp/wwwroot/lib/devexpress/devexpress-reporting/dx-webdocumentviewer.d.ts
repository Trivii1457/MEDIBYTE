
import dxGallery from 'devextreme/ui/gallery';
/**
* DevExpress HTML/JS Analytics Core (dx-analytics-module-header.ts)
* Version: 19.1.6
* Build date: 2019-09-10
* Copyright (c) 2012 - 2019 Developer Express Inc. ALL RIGHTS RESERVED
* License: https://www.devexpress.com/Support/EULAs/NetComponents.xml
*/

import 'jquery';
import 'jqueryui';
import * as ko from 'knockout'
import dxButton from 'devextreme/ui/button';
import dxTextBox from 'devextreme/ui/text_box';
import dxDropDownBox from 'devextreme/ui/drop_down_box';
import dxPopup from 'devextreme/ui/popup';
import { IOptions as dxPopupOptions } from 'devextreme/ui/popup';
import ArrayStore from 'devextreme/data/array_store';
import CustomStore from 'devextreme/data/custom_store';
import DataSource from 'devextreme/data/data_source';

/**
* DevExpress HTML/JS Analytics Core (dx-analytics-core.d.ts)
* Version: 19.1.6
* Build date: 2019-09-10
* Copyright (c) 2012 - 2019 Developer Express Inc. ALL RIGHTS RESERVED
* License: https://www.devexpress.com/Support/EULAs/NetComponents.xml
*/

declare module DevExpress.Analytics {
    module Internal {
        function propertiesVisitor(target: any, visitor: (properties: any[]) => any, visited?: any[], skip?: Array<string>): void;
        function checkModelReady(model: any): boolean;
    }
    module Utils {
        interface IModelReady {
            isModelReady: ko.Computed<boolean>;
        }
        class UndoEngine extends Disposable {
            static _disposeUndoEngineSubscriptionsName: string;
            static tryGetUndoEngine(object: any): UndoEngine;
            private _groupObservers;
            private _getInfoMethodName;
            private _ignoredProperties;
            private _groupPosition;
            private _observers;
            private _subscriptions;
            private _targetSubscription;
            private _visited;
            private _position;
            private _lockedPosition;
            private _inUndoRedo;
            private _model;
            private readonly _modelReady;
            private _disposeObserver;
            private properyChanged;
            private visitProperties;
            private undoChangeSet;
            private redoChangeSet;
            private _disposeChilds;
            private _createDisposeFunction;
            private _callDisposeFunction;
            private _cleanSubscribtions;
            protected validatePropertyName(target: any, propertyName: string): string;
            subscribeProperty(property: any, subscribeChilds: any): ko.Subscription;
            subscribeProperties(properties: any): void;
            subscribe(target: any, info?: any): any[];
            private _removePropertiesSubscriptions;
            constructor(target: any, ignoredProperties?: string[], getInfoMethodName?: string);
            redoEnabled: ko.Observable<boolean>;
            undoEnabled: ko.Observable<boolean>;
            dispose(): void;
            removeTargetSubscription(): void;
            undoAll(): void;
            reset(): void;
            clearHistory(): void;
            undo(): void;
            redo(): void;
            isIngroup: number;
            isDirty: ko.Computed<boolean>;
            start(): void;
            end(): void;
        }
    }
}

declare module DevExpress.Analytics {
    module Internal {
        function _defineProperty(legacyObject: any, realObject: any, propertyName: any, newPropertyName?: any): void;
        function _definePropertyByString(rootObject: any, ...objectPathes: string[]): void;
        function addDisposeCallback(element: Node, callback: () => any): void;
    }
    module Utils {
        interface IDisposable {
            dispose: () => void;
            _disposables?: Array<ko.Subscription | ko.ComputedFunctions | IDisposable>;
        }
        class Disposable implements IDisposable {
            _disposables: Array<ko.Subscription | ko.ComputedFunctions | IDisposable>;
            isDisposing: boolean;
            constructor();
            disposeObservableArray(array: ko.ObservableArray<IDisposable>): void;
            resetObservableArray(array: ko.ObservableArray<any>): void;
            disposeArray(array: IDisposable[]): void;
            dispose(): void;
            removeProperties(): void;
        }
        function deserializeArray<T>(model: any, creator: (item: any) => any): ko.ObservableArray<T>;
        function serializeDate(date: Date): string;
    }
    module Internal {
        function knockoutArrayWrapper<T>(items?: any, ...onChange: Array<(array: any[], event?: string) => void>): ko.ObservableArray<T>;
        function isPlainObject(obj: any): boolean;
        function isEmptyObject(obj: any): boolean;
        function extend(target: any, object1?: any, ...objectN: any[]): any;
        function getPropertyValues(target?: any): any[];
    }
    module Utils {
        interface IEditorInfo {
            header?: string;
            content?: string;
            custom?: string;
            editorType?: any;
            extendedOptions?: any;
        }
        interface ISerializationInfo {
            propertyName: string;
            modelName?: string;
            defaultVal?: any;
            type?: ISerializableModelConstructor;
            info?: ISerializationInfoArray;
            from?: (val: any, serializer?: IModelSerializer) => any;
            toJsonObject?: any;
            array?: boolean;
            link?: boolean;
            editor?: IEditorInfo;
            displayName?: string;
            values?: {
                [key: string]: string;
            } | ko.Observable<{
                [key: string]: string;
            }>;
            valuesArray?: Array<IDisplayedValue>;
            initialize?: (viewModel: any, serilizer?: IModelSerializer) => void;
            validationRules?: Array<any>;
            validatorOptions?: any;
            editorOptions?: any;
            localizationId?: string;
            visible?: any;
            disabled?: any;
            valueStore?: any;
            addHandler?: () => any;
            alwaysSerialize?: boolean;
            template?: string;
            beforeSerialize?: (value: any) => any;
        }
        interface IDisplayedValue {
            value: any;
            displayValue: string;
            localizationId?: string;
        }
        interface ISerializationInfoArray extends Array<ISerializationInfo> {
        }
        interface ISerializableModel {
            _model?: any;
            getInfo?: () => ISerializationInfoArray;
        }
        interface ISerializableModelConstructor extends ISerializableModel {
            new (model?: any, serializer?: IModelSerializer, info?: ISerializationInfoArray): any;
        }
        interface IModelSerializerOptions {
            useRefs?: boolean;
            serializeDate?: (date: Date) => string;
        }
        interface IModelSerializer {
            deserialize(viewModel: ISerializableModel, model: any, serializationsInfo?: ISerializationInfoArray): any;
            serialize(viewModel: ISerializableModel, serializationsInfo?: ISerializationInfoArray, refs?: any): any;
        }
        class ModelSerializer implements IModelSerializer {
            private _options;
            private _refTable;
            private _linkTable;
            private linkObjects;
            constructor(options?: IModelSerializerOptions);
            deserializeProperty(modelPropertyInfo: ISerializationInfo, model: any): any;
            deserialize(viewModel: Utils.ISerializableModel, model: any, serializationsInfo?: Utils.ISerializationInfoArray): void;
            serialize(viewModel: Utils.ISerializableModel, serializationsInfo?: Utils.ISerializationInfoArray, refs?: any): any;
            private _isSerializableValue;
            private _serialize;
        }
        interface IEventHandler {
            type: any;
            value: (ev: any) => any;
        }
        class EventManager<Sender, EventType> extends Utils.Disposable {
            dispose(): void;
            private _handlers;
            call<K extends keyof EventType>(type: K, args: EventType[K]): void;
            addHandler<K extends keyof EventType>(type: K, listener: (this: Sender, ev: EventType[K]) => any): void;
            removeHandler<K extends keyof EventType>(type: K, listener: (this: Sender, ev: EventType[K]) => any): void;
        }
    }
}

declare module DevExpress.Analytics {
    module Internal {
        var removeWinSymbols: boolean;
        var Globalize: any;
        var messages: {};
        var custom_localization_values: {};
        function selectPlaceholder(): any;
        function noDataText(): any;
        function resolveFromPromises<T>(promises: JQueryPromise<any>[], createModel: () => T): JQueryDeferred<T>;
        function applyLocalizationToDevExtreme(currentCulture: string): void;
        function isCustomizedWithUpdateLocalizationMethod(text: string): boolean;
        function localize(val: string): any;
        function formatDate(val: any): any;
        function parseDate(val: any, useDefault?: boolean, format?: string): Date;
    }
    module Utils {
        function addCultureInfo(json: {
            messages: any;
        }): void;
        function _stringEndsWith(string: string, searchString: string): boolean;
        function getLocalization(text: string, id?: string, _removeWinSymbols?: boolean): any;
        function updateLocalization(object: {
            [key: string]: string;
        }): void;
    }
    module Localization {
        function loadMessages(messages: {
            [key: string]: string;
        }): void;
    }
    module Internal {
        var StringId: {
            MasterDetailRelationsEditor: string;
            DataAccessBtnOK: string;
            DataAccessBtnCancel: string;
            DataSourceWizardTitle: string;
            WizardPageConfigureQuery: string;
        };
        interface ILocalizationInfo {
            text: string;
            localizationId: string;
        }
        interface IFileUploadOptions {
            accept?: string;
            type?: string;
            readMode?: string;
        }
        interface IFileUploadResult {
            content: string;
            format: string;
        }
        function uploadFile(options: IFileUploadOptions): JQueryPromise<IFileUploadResult>;
        function compareEditorInfo(editor1: any, editor2: any): boolean;
        function findMatchesInString(textToTest: string, searchPattern: string): RegExpMatchArray;
        function escapeToRegExp(string: String): string;
        function formatUnicorn(text: string, ...args: any[]): string;
        interface IModelAction {
            action: (propertyName: string) => void;
            title: string;
            visible: (propertyName: string) => boolean;
        }
        interface IControlPropertiesViewModel {
            isPropertyDisabled: (name: string) => boolean;
            isPropertyVisible: (name: string) => boolean;
            isPropertyModified: (name: string) => boolean;
            controlType?: string;
            actions: IModelAction[];
            getInfo?: () => Analytics.Utils.ISerializationInfoArray;
        }
        interface IUndoEngine {
            start: () => void;
            end: () => void;
        }
    }
    module Widgets {
        var editorTemplates: any;
        module Internal {
            var propertiesGridEditorsPaddingLeft: number;
        }
        class Editor extends Utils.Disposable {
            _setPadding(position: string, value: any): {};
            _model: ko.Observable<Analytics.Internal.IControlPropertiesViewModel>;
            isVisibleByContent: ko.Observable<boolean>;
            isSearchedProperty: ko.Observable<boolean> | ko.Computed<boolean>;
            isParentSearched: ko.Observable<boolean>;
            rtl: boolean;
            private _validator;
            dispose(): void;
            constructor(modelPropertyInfo: Analytics.Utils.ISerializationInfo, level: any, parentDisabled?: ko.Observable<boolean> | ko.Computed<boolean>, textToSearch?: any);
            private _cachedValue;
            private _assignValue;
            private _init;
            findInfo(viewModel: any): any;
            updateInfo(propertyInfo: Analytics.Utils.ISerializationInfo): boolean;
            update(viewModel: Analytics.Internal.IControlPropertiesViewModel): void;
            getOptions(templateOptions: any): any;
            getValidatorOptions(templateValidatorOptions: any): any;
            _getEditorValidationRules(): any[];
            getValidationRules(): any;
            readonly validationRules: any;
            padding: any;
            level: any;
            textToSearch: ko.Observable<string> | ko.Computed<string>;
            info: ko.Observable<Analytics.Utils.ISerializationInfo>;
            name: string;
            displayName: ko.Computed<string>;
            templateName: string;
            contentTemplateName: string;
            editorTemplate: string;
            viewmodel: any;
            values: ko.Computed<{
                displayValue: string;
                value: string;
            }[]>;
            value: ko.Computed<any>;
            isEditorSelected: ko.Observable<boolean>;
            isPropertyModified: ko.Computed<boolean>;
            disabled: ko.Computed<boolean>;
            visible: ko.Computed<boolean>;
            getPopupServiceActions(): Analytics.Internal.IModelAction[];
            editorOptions: any;
            validatorOptions: any;
            defaultValue: any;
            readonly isComplexEditor: boolean;
            collapsed: ko.Observable<boolean>;
        }
        class PropertyGridEditor extends Editor {
            constructor(info: Analytics.Utils.ISerializationInfo, level: any, parentDisabled?: ko.Computed<boolean>, textToSearch?: any);
            createObjectProperties(): ObjectProperties;
            visibleByName: ko.Computed<boolean>;
            editorCreated: ko.Observable<boolean>;
        }
        class EditorValidator extends Utils.Disposable {
            private _editor;
            private _lastValidatorOptions;
            private _lastModelOverridableRules;
            private _validatorInstance;
            private _onValidatedHandler;
            dispose(): void;
            constructor(_editor: Editor);
            _isValid(validationRules: any, newValue: any): {
                brokenRule?: any;
                isValid?: boolean;
                validationRules?: Array<any>;
                value?: any;
            };
            validatorInstance: any;
            onValidatedHandler: any;
            getValidationRules(): any;
            getValidatorOptions(templateValidatorOptions?: any): any;
            areRulesChanged(overridableRuleSet: Array<{
                type: string;
                message: any;
                validationCallback?: any;
            }>): number | boolean;
            wrapOnValidatorInitialized(options: any): void;
            _onValidatorInitialized(e: any): void;
            _concatValidationRules(validatorOptions: any, validationRules: any): any;
            _wrapValidatorEvents(validatorOptions: any): any;
            assignWithValidation(newValue: any, assignValueFunc: () => void): void;
        }
        class ObjectProperties extends Utils.Disposable {
            private _targetSubscription;
            private _infoSubscription;
            private _getInfoComputed;
            update(viewModel: Analytics.Internal.IControlPropertiesViewModel): void;
            private _cleanEditorsSubscriptions;
            dispose(): void;
            cleanSubscriptions(): void;
            cleanEditors(): void;
            findEditorByInfo(serializationInfo: Analytics.Utils.ISerializationInfo): Editor;
            createEditor(modelPropertyInfo: Analytics.Utils.ISerializationInfo): any;
            createEditors(serializationInfo: Analytics.Utils.ISerializationInfoArray): any[];
            private _createEditors;
            protected _update(target: any, editorsInfo: any, recreateEditors: any): void;
            constructor(target: ko.Observable<any> | ko.Computed<any>, editorsInfo?: {
                editors?: Analytics.Utils.ISerializationInfoArray | ko.Observable<Analytics.Utils.ISerializationInfoArray> | ko.Computed<Analytics.Utils.ISerializationInfoArray>;
            }, level?: number, parentDisabled?: ko.Observable<boolean> | ko.Computed<boolean>, recreateEditors?: boolean, textToSearch?: any);
            level: number;
            rtl: boolean;
            getEditors(): Editor[];
            _textToSearch: any;
            visible: ko.Observable<boolean> | ko.Computed<boolean>;
            _editors: ko.ObservableArray<Editor>;
            private _parentDisabled;
        }
    }
    module Internal {
        class CodeResolver {
            private _queue;
            private _done;
            done(callback: any): void;
            execute(func: any, time?: number): JQueryPromise<{}>;
        }
        var globalResolver: CodeResolver;
    }
    module Widgets {
    }
    module Internal {
        class PopupService {
            data: ko.Observable<any>;
            title: ko.Observable<string>;
            visible: ko.Observable<boolean>;
            actions: ko.ObservableArray<IModelAction>;
            target: ko.Observable<any>;
        }
        interface IEditorAddon {
            templateName: string;
            data: EditorAddOn;
        }
        class EditorAddOn extends Utils.Disposable {
            private _popupService;
            private _editor;
            private _updateActions;
            constructor(editor: Widgets.Editor, popupService: PopupService);
            showPopup: (_: any, element: any) => void;
            visible: ko.Computed<boolean>;
            editorMenuButtonCss: ko.Observable<string> | ko.Computed<string>;
            templateName: string;
        }
    }
    module Widgets {
        class GuidEditor extends Editor {
            _getEditorValidationRules(): any[];
        }
        module Internal {
            function validateGuid(guid: any): boolean;
            function validateNullableGuid(guid: any): boolean;
            var guidValidationRules: {
                type: string;
                validationCallback: (options: any) => boolean;
                message: () => any;
            }[];
            var guidRequiredValidationRules: {
                type: string;
                message: () => any;
            }[];
            var requiredValidationRules: {
                type: string;
                message: () => any;
            }[];
        }
        module Internal {
            class CollectionItemWrapper {
                constructor(editor: any, array: any, index: ko.Observable<number> | ko.Computed<number>, displayNameField?: string);
                editor: any;
                index: ko.Observable<number> | ko.Computed<number>;
                value: ko.Observable | ko.Computed;
                collapsed: ko.Observable<boolean> | ko.Computed<boolean>;
                name: ko.Observable<string> | ko.Computed<string>;
                selected: ko.Observable<boolean> | ko.Computed<boolean>;
            }
            interface ICollectionEditorOptions {
                values: ko.Observable<ko.ObservableArray<any>> | ko.Computed<ko.ObservableArray<any>>;
                addHandler: () => any;
                displayName?: string;
                displayPropertyName?: string;
                hideButtons?: any;
                collapsed?: boolean;
                undoEngine?: ko.Observable<Analytics.Internal.IUndoEngine> | ko.Computed<Analytics.Internal.IUndoEngine>;
                level?: number;
                info?: ko.Observable<Analytics.Utils.ISerializationInfo> | ko.Computed<Analytics.Utils.ISerializationInfo>;
                template?: string;
                editorTemplate?: string;
                textEmptyArray?: Analytics.Internal.ILocalizationInfo;
                isVisibleButton?: (index: any, buttonName: any) => boolean;
                isDisabledButton?: (index: any, buttonName: any) => boolean;
            }
            class CollectionEditorViewModel {
                private _textEmptyArray;
                private _move;
                options: any;
                displayPropertyName: string;
                constructor(options: ICollectionEditorOptions, disabled?: ko.Observable<boolean>);
                getDisplayTextButton(key: string): any;
                getDisplayTextEmptyArray(): any;
                createCollectionItemWrapper(grandfather: any, index: any): CollectionItemWrapper;
                buttonMap: {
                    [keyname: string]: Analytics.Internal.ILocalizationInfo;
                };
                isVisibleButton: (buttonName: any) => boolean;
                isDisabledButton: (buttonName: any) => boolean;
                padding: number;
                values: ko.Observable<any[]> | ko.Computed<any[]>;
                add: (model: any) => void;
                up: (model: any) => void;
                down: (model: any) => void;
                remove: (model: any) => void;
                select: (event: any) => void;
                selectedIndex: ko.Observable<number>;
                collapsed: ko.Observable<boolean> | ko.Computed<boolean>;
                displayName: string;
                alwaysShow: ko.Observable<boolean>;
                showButtons: ko.Computed<boolean>;
                disabled: ko.Observable<boolean> | ko.Computed<boolean>;
            }
            class dxEllipsisEditor extends dxTextBox {
                _$button: JQuery;
                _$buttonIcon: JQuery;
                _modelByElement: any;
                _koContext: any;
                constructor(element: any, options?: any);
                _init(): void;
                _render(): void;
                _renderButton(): void;
                _updateButtonSize(): void;
                _attachButtonEvents(): void;
                _optionChanged(obj: any, value: any): void;
            }
            class dxFileImagePicker extends dxEllipsisEditor {
                _filesinput: any;
                constructor(element: any, options?: any);
                _handleFiles(filesHolder: {
                    files: any;
                }): void;
                _$getInput(): JQuery;
                _render(): void;
                _renderInput(inputContainer: any): void;
                _attachButtonEvents(): void;
                _renderValue(): void;
            }
            var availableUnits: {
                value: string;
                displayValue: string;
                localizationId: string;
            }[];
            class FontModel extends Utils.Disposable {
                updateModel(value: string): void;
                updateValue(value: any): void;
                constructor(value: ko.Observable<string> | ko.Computed<string>);
                family: ko.Observable<string>;
                unit: ko.Observable<string>;
                isUpdateModel: boolean;
                size: ko.Observable<number>;
                modificators: {
                    bold: ko.Observable<boolean>;
                    italic: ko.Observable<boolean>;
                    strikeout: ko.Observable<boolean>;
                    underline: ko.Observable<boolean>;
                };
            }
            var availableFonts: ko.Observable<{
                [key: string]: string;
            }>;
        }
        class FontEditor extends PropertyGridEditor {
            constructor(info: Analytics.Utils.ISerializationInfo, level: any, parentDisabled?: ko.Computed<boolean>, textToSearch?: any);
            createObjectProperties(): ObjectProperties;
        }
        module Metadata {
            var fontInfo: Utils.ISerializationInfoArray;
        }
        module Internal {
            var extendedTemplates: (templates?: {
                [key: string]: string;
            }) => {
                [key: string]: string;
            };
            var availableTemplates: {
                [key: string]: string;
            };
            class SvgTemplatesEngine {
                private static _instance;
                private _data;
                private _templates;
                private _hasTemplate;
                constructor();
                private static readonly Instance;
                static readonly templates: {
                    [key: string]: string;
                };
                static addTemplates(templates: any): void;
                static getExistingTemplate(name: any): any;
            }
            function getTemplate(id: string): string;
        }
    }
}

declare module DevExpress.Analytics {
    module Utils {
        interface IPathRequest {
            fullPath: string;
            path: string;
            ref?: string;
            id?: string;
            dataSource?: any;
            state?: any;
            pathParts?: string[];
        }
        class PathRequest implements IPathRequest {
            pathParts: string[];
            constructor(fullPath: string, pathParts?: string[]);
            fullPath: string;
            ref: string;
            id: string;
            path: string;
        }
    }
    module Widgets {
        module Internal {
            class ValueEditorHelper {
                private static _getCharFromKeyCode;
                private static _getCaretPosition;
                static editors: {
                    "integer": {
                        regExpEditing: RegExp;
                    };
                    "float": {
                        regExpEditing: RegExp;
                    };
                    "System.Byte": {
                        regExpEditing: RegExp;
                        minValue: any;
                        maxValue: string;
                    };
                    "System.SByte": {
                        regExpEditing: RegExp;
                        minValue: string;
                        maxValue: string;
                    };
                    "System.Int16": {
                        regExpEditing: RegExp;
                        minValue: string;
                        maxValue: string;
                    };
                    "System.UInt16": {
                        regExpEditing: RegExp;
                        minValue: any;
                        maxValue: string;
                    };
                    "System.Int32": {
                        regExpEditing: RegExp;
                        minValue: string;
                        maxValue: string;
                    };
                    "System.UInt32": {
                        regExpEditing: RegExp;
                        minValue: any;
                        maxValue: string;
                    };
                    "System.Int64": {
                        regExpEditing: RegExp;
                        minValue: string;
                        maxValue: string;
                    };
                    "System.UInt64": {
                        regExpEditing: RegExp;
                        minValue: any;
                        maxValue: string;
                    };
                    "System.Single": {
                        regExpEditing: RegExp;
                        minValue: string;
                        maxValue: string;
                    };
                    "System.Double": {
                        regExpEditing: RegExp;
                        minValue: string;
                        maxValue: string;
                    };
                    "System.Decimal": {
                        regExpEditing: RegExp;
                        minValue: string;
                        maxValue: string;
                    };
                };
                private static _validate;
                static validateWidgetValue(e: any, validate: (value: string) => boolean, defaultVal: string): void;
                static getNumberEditorOptions(id: string, specifics: string, extendOptions?: {}): any;
                static getValueEditorOptions(regExpEditing: RegExp, validate: (value: string) => boolean, defaultVal: string, extendOptions?: {}): any;
                static isValid(id: string, specifics: string, value: string): boolean;
                private static _invokeStandardHandler;
            }
        }
    }
    module Internal {
        function integerValueConverter(val: any, defaultValue: any): any;
        interface IValidateExpressionOptions {
            fieldListProvider: Utils.IItemsProvider;
            expression: string;
            path: string;
            rootItems?: string[];
        }
        function validateExpression(options: IValidateExpressionOptions): JQueryPromise<{}>;
        function floatValueConverter(val: any, defaultValue: any): any;
        function isDarkTheme(theme?: string): boolean;
        function setCursorInFunctionParameter(paramCount: any, editor: any, insertValue: any): void;
        function isList(data: Utils.IDataMemberInfo): boolean;
        function getParentContainer(el: HTMLElement, container?: string): JQuery;
        function isNullOrEmptyString(str: string): boolean;
    }
    module Utils {
        interface IDataMemberInfo {
            name: string;
            displayName: string;
            isList?: boolean;
            specifics?: string;
            isSelected?: boolean;
            dataType?: string;
            templateName?: string;
            innerActions?: any;
            noDragable?: any;
            dragData?: any;
            icon?: string;
        }
        interface IItemsProvider {
            getItems: (path: IPathRequest) => JQueryPromise<IDataMemberInfo[]>;
            getItemByPath?: (path: IPathRequest) => JQueryPromise<IDataMemberInfo>;
            getValues?: (path: IPathRequest) => JQueryPromise<any[]>;
        }
        interface IHotKey {
            ctrlKey: boolean;
            keyCode: number;
        }
        interface IAction {
            id?: string;
            text: string;
            textId?: string;
            displayText?: () => string;
            imageClassName: any;
            imageTemplateName?: any;
            container?: string;
            clickAction: (model?: any) => void;
            disabled?: ko.Observable<boolean> | ko.Computed<boolean>;
            hotKey?: IHotKey;
            hasSeparator?: boolean;
            templateName?: string;
            visible?: any;
            zoomStep?: any;
            zoomLevels?: any;
            zoom?: any;
            zoomItems?: any;
            position?: number;
            selected?: ko.Observable<boolean> | ko.Computed<boolean>;
            displayExpr?: (val: any) => string;
            onCustomItemCreating?: (e: {
                text: string;
                customItem: any;
            }) => void;
        }
    }
    module Widgets {
        module Internal {
            interface IAceEditor {
                require(module: string): any;
                edit(element: HTMLElement): any;
            }
            var ace: IAceEditor;
            var aceAvailable: boolean;
        }
        module Internal {
            interface IExpressionEditorItem {
                text: string;
                description?: string;
                descriptionStringId?: string;
            }
            interface IExpressionEditorOperatorItem extends IExpressionEditorItem {
                image?: string;
                hasSeparator?: boolean;
            }
            var operatorNames: Array<IExpressionEditorOperatorItem>;
            interface IExpressionEditorFunctionItem extends IExpressionEditorItem {
                paramCount: number;
                displayName?: string;
            }
            interface IExpressionEditorFunction {
                display: string;
                localizationId?: string;
                items?: {
                    [key: string]: Array<IExpressionEditorFunctionItem>;
                };
                category?: string;
            }
            var functionDisplay: Array<IExpressionEditorFunction>;
            function insertInFunctionDisplay(addins: any): Array<Internal.IExpressionEditorFunction>;
        }
    }
    module Criteria {
        class CriteriaOperator {
            static operators(enums: any): any;
            static parse(stringCriteria: string): CriteriaOperator;
            static create(operatorType: any): CriteriaOperator;
            static and(left: CriteriaOperator, right: CriteriaOperator): CriteriaOperator;
            static or(left: CriteriaOperator, right: CriteriaOperator): CriteriaOperator;
            static getNotValidRange(value: string, errorMessage: string): {
                start: number;
                end: number;
            };
            readonly displayType: string;
            readonly enumType: any;
            operatorType: any;
            type: string;
            operands: any;
            create: (operatorType: any, field: CriteriaOperator) => CriteriaOperator;
            remove: (operand: CriteriaOperator) => void;
            change: (operandType: any, operand: CriteriaOperator, incorrectSpecificsForAggregate: boolean) => CriteriaOperator;
            changeValueType: (type: any, location: Utils.IPropertyLocation) => CriteriaOperator;
            changeValue: (operand: CriteriaOperator, reverse: boolean, location: Utils.IPropertyLocation) => CriteriaOperator;
            assignLeftPart: (criteriaOperator: any) => any;
            assignRightPart: (criteriaOperator: any) => any;
            assignType: (type: any) => void;
            readonly leftPart: any;
            readonly rightPart: any;
            resetrightPart: (value: any) => any;
            assignFrom(criteriaOperator: any, incorrectSpecificsForAggregate?: boolean): void;
            children(): any[];
            accept(visitor: Utils.ICriteriaOperatorVisitor): CriteriaOperator;
        }
    }
    module Widgets {
        module Internal {
            interface ICompletionRootItem {
                name: string;
                needPrefix?: boolean;
                rootPath?: string;
            }
            interface ICodeCompletorOptions {
                editor: any;
                bindingContext: any;
                fieldListProvider: Utils.IItemsProvider;
                path: ko.Observable<string> | ko.Computed<string>;
                functions?: Array<Internal.IExpressionEditorFunction> | ko.ObservableArray<Internal.IExpressionEditorFunction>;
                rootItems?: Array<ICompletionRootItem>;
                getRealExpression?: (path: string, member: string) => JQueryPromise<string>;
            }
            class CodeCompletor extends Utils.Disposable {
                private _options;
                private _fieldListProvider;
                private _path;
                private _editor;
                private _contextPath;
                private _functions;
                private _rootItems;
                private _isInContext;
                private _getPath;
                private _previousSymbol;
                beforeInsertMatch(editor: any, token: any, parentPrefix: any): void;
                insertMatch(editor: any, parentPrefix: any, fieldName: any): void;
                generateFieldDisplayName(parentPrefix: any, displayName: any): string;
                private _convertDataMemberInfoToCompletions;
                private _combinePath;
                private _getParentPrefix;
                private _getRealPath;
                private _getFields;
                private static _cleanupFields;
                getFunctionsCompletions(): any[];
                getAggregateCompletions(): any[];
                getOperatorCompletions(prefix: any): {
                    caption: string;
                    snippet: string;
                    meta: any;
                }[];
                private _addFunctions;
                private _addAggregates;
                private _addOperators;
                private _addParameterOperators;
                private _getOperands;
                private _getOperandsOrOperators;
                private _findStartContextTokenPosition;
                private _findOpenedStartContext;
                private _findOpenedAggregates;
                private _getContextPath;
                private _getCompletions;
                defaultProcess(token: any, text: any, completions: any): JQueryPromise<{}>;
                constructor(_options: ICodeCompletorOptions);
                identifierRegexps: RegExp[];
                getCompletions(aceEditor: any, session: any, pos: any, prefix: any, callback: any): void;
                getDocTooltip(item: any): void;
            }
            function createFunctionCompletion(fnInfo: Internal.IExpressionEditorFunctionItem, name: string, insertValue?: string): {
                caption: string;
                snippet: string;
                meta: any;
                tooltip: any;
                score: number;
                completer: {
                    insertMatch: (editor: any, data: any) => void;
                };
            };
        }
    }
    module Criteria {
        module Utils {
            interface IPropertyLocation {
                index?: number;
                name?: string;
            }
            var operatorTokens: {
                "Plus": string;
                "Minus": string;
                "Equal": string;
                "NotEqual": string;
                "Greater": string;
                "Less": string;
                "LessOrEqual": string;
                "GreaterOrEqual": string;
            };
            function criteriaForEach(operator: CriteriaOperator, callback: (operator: CriteriaOperator, path?: any) => void, path?: string): void;
            interface ICriteriaOperatorVisitor {
                visitGroupOperator?: (element: GroupOperator) => GroupOperator;
                visitOperandProperty?: (element: OperandProperty) => OperandProperty;
                visitConstantValue?: (element: ConstantValue) => ConstantValue;
                visitOperandParameter?: (element: OperandParameter) => OperandParameter;
                visitAggregateOperand?: (element: AggregateOperand) => AggregateOperand;
                visitJoinOperand?: (element: JoinOperand) => JoinOperand;
                visitBetweenOperator?: (element: BetweenOperator) => BetweenOperator;
                visitInOperator?: (element: InOperator) => InOperator;
                visitBinaryOperator?: (element: BinaryOperator) => BinaryOperator;
                visitUnaryOperator?: (element: UnaryOperator) => UnaryOperator;
                visitFunctionOperator?: (element: FunctionOperator) => FunctionOperator;
            }
        }
        interface IOperandPropertyOptions {
            propertyName: string;
            startColumn: any;
            startLine: any;
            originalPropertyLength: any;
        }
        class OperandProperty extends CriteriaOperator {
            constructor(propertyName?: string, startColumn?: number, startLine?: number, originalPropertyLength?: number);
            readonly displayType: string;
            propertyName: string;
            originalPropertyLength: number;
            type: string;
            startPosition: {
                line: number;
                column: number;
            };
            accept(visitor: Utils.ICriteriaOperatorVisitor): OperandProperty;
        }
    }
    module Utils {
        interface IDisplayExpressionConverter {
            toDisplayExpression(path: string, expression: string): JQueryPromise<string>;
            toRealExpression(path: string, expression: string): JQueryPromise<string>;
        }
        interface IDisplayNameProvider {
            getDisplayNameByPath: (path: string, dataMember: string) => JQueryPromise<string>;
            getRealName: (path: string, displayDataMember: string) => JQueryPromise<string>;
        }
    }
    module Internal {
        class DisplayExpressionConverter implements Utils.IDisplayExpressionConverter {
            private displayNameProvider;
            private _replaceNames;
            constructor(displayNameProvider: Utils.IDisplayNameProvider);
            toDisplayExpression(path: string, expression: string): JQueryPromise<string>;
            toRealExpression(path: string, expression: string): JQueryPromise<any>;
        }
    }
    module Criteria {
        class CriteriaOperatorPreprocessor {
            _func: Array<(currentOperand: CriteriaOperator, options: {
                operatorType: string;
                options: any;
            }) => CriteriaOperator>;
            CriteriaOperator(): CriteriaOperator;
            BinaryOperator(options: IBinaryOperatorOptions): BinaryOperator;
            FunctionOperator(options: IFunctionOperatorProperties): FunctionOperator;
            AggregateOperand(options: IAggregateOperandOptions): AggregateOperand;
            GroupOperator(options: IGroupOperatorOptions): GroupOperator;
            InOperator(options: IInOperatorOptions): InOperator;
            ConstantValue(options: IOperandValueOptions): ConstantValue;
            OperandValue(options: IOperandValueOptions): OperandValue;
            OperandParameter(options: IOperandParameterOptions): OperandParameter;
            OperandProperty(options: IOperandPropertyOptions): OperandProperty;
            UnaryOperator(options: IUnaryOperatorOptions): UnaryOperator;
            BetweenOperator(options: IBetweenOperatorOptions): BetweenOperator;
            JoinOperator(options: IJoinOperandOptions): JoinOperand;
            addListener(func: (currentOperand: CriteriaOperator, options: {
                operatorType: string;
                options: any;
            }) => CriteriaOperator): void;
            removeListener(func: (currentOperand: CriteriaOperator, options: {
                operatorType: string;
                options: any;
            }) => CriteriaOperator): void;
            process(operatorType: string, options: any): CriteriaOperator;
        }
        var criteriaCreator: CriteriaOperatorPreprocessor;
        enum Aggregate {
            Count = 0,
            Exists = 1,
            Min = 2,
            Max = 3,
            Avg = 4,
            Sum = 5,
            Single = 6
        }
        interface IAggregateOperandOptions {
            property: CriteriaOperator;
            aggregatedExpression: CriteriaOperator;
            aggregateType: Aggregate;
            condition: any;
        }
        class AggregateOperand extends CriteriaOperator {
            constructor(property: CriteriaOperator, aggregatedExpression: CriteriaOperator, aggregateType: Aggregate, condition: any);
            readonly displayType: string;
            readonly enumType: typeof Aggregate;
            readonly leftPart: CriteriaOperator;
            children(): any[];
            change: (operationType: any, item: CriteriaOperator) => any;
            assignLeftPart: (criteriaOperator: any) => void;
            property: CriteriaOperator;
            condition: CriteriaOperator;
            operatorType: Aggregate;
            aggregatedExpression: any;
            type: string;
            accept(visitor: Utils.ICriteriaOperatorVisitor): AggregateOperand;
        }
        interface IBetweenOperatorOptions {
            property: CriteriaOperator;
            begin: CriteriaOperator;
            end: CriteriaOperator;
        }
        class BetweenOperator extends CriteriaOperator {
            constructor(property: CriteriaOperator, begin: CriteriaOperator, end: CriteriaOperator);
            property: CriteriaOperator;
            begin: CriteriaOperator;
            end: CriteriaOperator;
            readonly leftPart: CriteriaOperator;
            readonly rightPart: CriteriaOperator[];
            resetRightPart: (newVal: any) => void;
            assignLeftPart: (criteriaOperator: any) => void;
            assignRightPart: (criteriaOperator: any) => void;
            readonly displayType: string;
            operatorType: string;
            readonly enumType: typeof BetweenOperator;
            type: string;
            accept(visitor: Utils.ICriteriaOperatorVisitor): BetweenOperator;
        }
        enum BinaryOperatorType {
            Equal = 0,
            NotEqual = 1,
            Greater = 2,
            Less = 3,
            LessOrEqual = 4,
            GreaterOrEqual = 5,
            Like = 6,
            BitwiseAnd = 7,
            BitwiseOr = 8,
            BitwiseXor = 9,
            Divide = 10,
            Modulo = 11,
            Multiply = 12,
            Plus = 13,
            Minus = 14
        }
        interface IBinaryOperatorOptions {
            left: CriteriaOperator;
            right: CriteriaOperator;
            operatorType: BinaryOperatorType;
        }
        class BinaryOperator extends CriteriaOperator {
            constructor(left: CriteriaOperator, right: CriteriaOperator, operatorType: BinaryOperatorType);
            readonly leftPart: CriteriaOperator;
            readonly rightPart: CriteriaOperator;
            assignLeftPart: (criteriaOperator: any) => void;
            assignRightPart: (criteriaOperator: any) => void;
            leftOperand: CriteriaOperator;
            rightOperand: CriteriaOperator;
            operatorType: BinaryOperatorType;
            readonly displayType: any;
            readonly enumType: typeof BinaryOperatorType;
            type: string;
            accept(visitor: Utils.ICriteriaOperatorVisitor): BinaryOperator;
        }
        interface IOperandValueOptions {
            value: any;
        }
        class OperandValue extends CriteriaOperator {
            private _processStringValue;
            constructor(value?: any);
            readonly displayType: any;
            value: any;
            type: string;
            specifics: string;
        }
        class ConstantValue extends OperandValue {
            constructor(value: any);
            accept(visitor: Utils.ICriteriaOperatorVisitor): ConstantValue;
        }
        enum FunctionOperatorType {
            None = 0,
            Custom = 1,
            CustomNonDeterministic = 2,
            Iif = 3,
            IsNull = 4,
            IsNullOrEmpty = 5,
            Trim = 6,
            Len = 7,
            Substring = 8,
            Upper = 9,
            Lower = 10,
            Concat = 11,
            Ascii = 12,
            Char = 13,
            ToStr = 14,
            Replace = 15,
            Reverse = 16,
            Insert = 17,
            CharIndex = 18,
            Remove = 19,
            Abs = 20,
            Sqr = 21,
            Cos = 22,
            Sin = 23,
            Atn = 24,
            Exp = 25,
            Log = 26,
            Rnd = 27,
            Tan = 28,
            Power = 29,
            Sign = 30,
            Round = 31,
            Ceiling = 32,
            Floor = 33,
            Max = 34,
            Min = 35,
            Acos = 36,
            Asin = 37,
            Atn2 = 38,
            BigMul = 39,
            Cosh = 40,
            Log10 = 41,
            Sinh = 42,
            Tanh = 43,
            PadLeft = 44,
            PadRight = 45,
            StartsWith = 46,
            EndsWith = 47,
            Contains = 48,
            ToInt = 49,
            ToLong = 50,
            ToFloat = 51,
            ToDouble = 52,
            ToDecimal = 53,
            LocalDateTimeThisYear = 54,
            LocalDateTimeThisMonth = 55,
            LocalDateTimeLastWeek = 56,
            LocalDateTimeThisWeek = 57,
            LocalDateTimeYesterday = 58,
            LocalDateTimeToday = 59,
            LocalDateTimeNow = 60,
            LocalDateTimeTomorrow = 61,
            LocalDateTimeDayAfterTomorrow = 62,
            LocalDateTimeNextWeek = 63,
            LocalDateTimeTwoWeeksAway = 64,
            LocalDateTimeNextMonth = 65,
            LocalDateTimeNextYear = 66,
            IsOutlookIntervalBeyondThisYear = 67,
            IsOutlookIntervalLaterThisYear = 68,
            IsOutlookIntervalLaterThisMonth = 69,
            IsOutlookIntervalNextWeek = 70,
            IsOutlookIntervalLaterThisWeek = 71,
            IsOutlookIntervalTomorrow = 72,
            IsOutlookIntervalToday = 73,
            IsOutlookIntervalYesterday = 74,
            IsOutlookIntervalEarlierThisWeek = 75,
            IsOutlookIntervalLastWeek = 76,
            IsOutlookIntervalEarlierThisMonth = 77,
            IsOutlookIntervalEarlierThisYear = 78,
            IsOutlookIntervalPriorThisYear = 79,
            IsThisWeek = 80,
            IsThisMonth = 81,
            IsThisYear = 82,
            DateDiffTick = 83,
            DateDiffSecond = 84,
            DateDiffMilliSecond = 85,
            DateDiffMinute = 86,
            DateDiffHour = 87,
            DateDiffDay = 88,
            DateDiffMonth = 89,
            DateDiffYear = 90,
            GetDate = 91,
            GetMilliSecond = 92,
            GetSecond = 93,
            GetMinute = 94,
            GetHour = 95,
            GetDay = 96,
            GetMonth = 97,
            GetYear = 98,
            GetDayOfWeek = 99,
            GetDayOfYear = 100,
            GetTimeOfDay = 101,
            Now = 102,
            UtcNow = 103,
            Today = 104,
            AddTimeSpan = 105,
            AddTicks = 106,
            AddMilliSeconds = 107,
            AddSeconds = 108,
            AddMinutes = 109,
            AddHours = 110,
            AddDays = 111,
            AddMonths = 112,
            AddYears = 113,
            OrderDescToken = 114
        }
        interface IFunctionOperatorProperties {
            operatorType: FunctionOperatorType;
            operands: any[];
        }
        class FunctionOperator extends CriteriaOperator {
            constructor(operatorType: FunctionOperatorType, operands: any);
            toString: (reverse: any) => string;
            operatorType: FunctionOperatorType;
            assignLeftPart: (criteriaOperator: any) => void;
            assignRightPart: (criteriaOperator: any) => void;
            readonly leftPart: any;
            readonly rightPart: any[];
            readonly displayType: string;
            readonly enumType: typeof FunctionOperatorType;
            operands: any[];
            type: string;
            accept(visitor: Utils.ICriteriaOperatorVisitor): FunctionOperator;
        }
        enum GroupOperatorType {
            And = 0,
            Or = 1
        }
        interface IGroupOperatorOptions {
            operation: GroupOperatorType;
            operands: Array<CriteriaOperator>;
        }
        class GroupOperator extends CriteriaOperator {
            constructor(operation: GroupOperatorType, operands: Array<CriteriaOperator>);
            static combine(operation: GroupOperatorType, operands: Array<CriteriaOperator>): CriteriaOperator;
            create: (isGroup: any, property: OperandProperty, specifics?: string) => any;
            change: (operationType: any, item: any, incorrectSpecificsForAggregate?: boolean) => any;
            remove: (operator: CriteriaOperator) => void;
            operatorType: GroupOperatorType;
            assignLeftPart: (operator: CriteriaOperator) => void;
            children(): any[];
            readonly displayType: string;
            readonly enumType: typeof GroupOperatorType;
            operands: any[];
            type: string;
            accept(visitor: Utils.ICriteriaOperatorVisitor): GroupOperator;
        }
        interface IInOperatorOptions {
            criteriaOperator: CriteriaOperator;
            operands: any[];
        }
        class InOperator extends CriteriaOperator {
            constructor(criteriaOperator: CriteriaOperator, operands: any);
            readonly leftPart: CriteriaOperator;
            readonly rightPart: any[];
            assignLeftPart: (criteriaOperator: any) => void;
            assignRightPart: (criteriaOperator: any) => void;
            criteriaOperator: CriteriaOperator;
            readonly displayType: string;
            operatorType: string;
            readonly enumType: typeof InOperator;
            type: string;
            operands: any[];
            accept(visitor: Utils.ICriteriaOperatorVisitor): InOperator;
        }
        interface IJoinOperandOptions {
            joinTypeName: string;
            condition: CriteriaOperator;
            type: Aggregate;
            aggregated: CriteriaOperator;
        }
        class JoinOperand extends CriteriaOperator {
            constructor(joinTypeName: string, condition: CriteriaOperator, type: Aggregate, aggregated: CriteriaOperator);
            static joinOrAggregate(collectionProperty: OperandProperty, condition: CriteriaOperator, type: Aggregate, aggregated: CriteriaOperator): CriteriaOperator;
            joinTypeName: string;
            condition: CriteriaOperator;
            operatorType: Aggregate;
            aggregatedExpression: CriteriaOperator;
            type: string;
            accept(visitor: Utils.ICriteriaOperatorVisitor): JoinOperand;
        }
        interface IOperandParameterOptions {
            parameterName?: string;
            value?: string;
        }
        class OperandParameter extends OperandValue {
            constructor(parameterName?: string, value?: string);
            readonly displayType: string;
            parameterName: string;
            type: string;
            accept(visitor: Utils.ICriteriaOperatorVisitor): OperandParameter;
        }
        enum UnaryOperatorType {
            Minus = 0,
            Plus = 1,
            BitwiseNot = 2,
            Not = 3,
            IsNull = 4
        }
        interface IUnaryOperatorOptions {
            operatorType: UnaryOperatorType;
            operator: CriteriaOperator;
        }
        class UnaryOperator extends CriteriaOperator {
            constructor(operatorType: UnaryOperatorType, operand: CriteriaOperator);
            readonly leftPart: CriteriaOperator;
            operand: CriteriaOperator;
            operatorType: UnaryOperatorType;
            assignFrom(criteriaOperator: any): void;
            readonly displayType: string;
            readonly enumType: typeof UnaryOperatorType;
            type: string;
            accept(visitor: Utils.ICriteriaOperatorVisitor): UnaryOperator;
        }
    }
    module Widgets {
        module Internal {
            interface IExpressionEditorContent {
                data: any;
                name: string;
                isSelected: ko.Observable<boolean> | ko.Computed<boolean>;
                showDescription: boolean;
            }
            interface IExpressionEditorCategory extends Utils.IDisposable {
                displayName: string;
                collapsed?: ko.Observable<boolean> | ko.Computed<boolean>;
                content?: IExpressionEditorContent;
                items?: ko.Observable<IExpressionEditorContent[]> | ko.Computed<IExpressionEditorContent[]>;
                templateName?: string;
            }
            class Tools extends Utils.Disposable {
                private _defaultClick;
                searchPlaceholder: () => any;
                private _generateTab;
                private _localizedExpressionEditorItem;
                private _initDescription;
                private _createFieldsCategory;
                private _createConstantCategory;
                private _createOperatorsCategory;
                private _createFunctionsCategoryContent;
                private _createFunctionsCategoryItem;
                private _createFunctionsCategory;
                private _disposeCategories;
                constructor(onClick: (item: any, element: any) => void, parametersOptions: any, options: ko.Observable<IExpressionOptions> | ko.Computed<IExpressionOptions>, fieldListOptions?: any);
                dispose(): void;
                resetCategoriesSelection: () => void;
                private _categories;
                showDescription: ko.Observable<boolean> | ko.Computed<boolean>;
                toolBox: any[];
                description: ko.Observable<string> | ko.Computed<string>;
            }
            var treeListEditAction: Utils.IAction;
            interface ITreeListOptions {
                itemsProvider: Utils.IItemsProvider;
                selectedPath: ko.Observable<string> | ko.Computed<string>;
                treeListController: ITreeListController;
                templateName?: string;
                rtl?: boolean;
                path?: ko.Observable<string> | ko.Observable<string[]>;
                onItemsVisibilityChanged?: () => void;
                expandRootItems?: boolean;
                pageSize?: number;
                templateHtml?: string;
            }
            class TreeListEllipsisButton {
                private _availableItemsCount;
                private padding;
                private pageSize;
                constructor(_availableItemsCount: ko.Observable<number>, padding: any, pageSize: number);
                templateName: string;
                collapsed: () => boolean;
                visibleItems: () => any[];
                text: () => any;
                renderNext(): void;
            }
            class TreeListItemViewModel extends Utils.Disposable {
                protected resolver: Analytics.Internal.CodeResolver;
                private _rtl;
                private _data;
                private _actions;
                private _pageSize;
                private _actionsSubscription;
                private _templateName;
                private _equal;
                private _treeListController;
                private _loader;
                private _iconName;
                private _getImageClassName;
                private _getImageTemplateName;
                private _getNodeImageClassName;
                private _createItemsObj;
                private _loadItems;
                protected _onItemsChanged(): void;
                _selectItem(itemPath: string): void;
                _find(itemPath: string): void;
                private _applyPadding;
                private _initPaginate;
                constructor(options: ITreeListOptions, path?: string[], onItemsVisibilityChanged?: () => any, rtl?: boolean, resolver?: Analytics.Internal.CodeResolver);
                dragDropHandler: any;
                dblClickHandler: any;
                _path: string[];
                _onItemsVisibilityChanged: () => void;
                level: number;
                parent: TreeListItemViewModel;
                padding: {};
                items: ko.ObservableArray<TreeListItemViewModel>;
                maxItemsCount: ko.Observable<number>;
                visibleItems: ko.Computed<any[]>;
                collapsed: ko.Observable<boolean>;
                nodeImageClass: ko.Computed<string>;
                imageClassName: ko.Computed<string>;
                imageTemplateName: ko.Computed<string>;
                readonly hasItems: boolean;
                data: Utils.IDataMemberInfo;
                readonly name: string;
                readonly path: string;
                readonly pathParts: string[];
                readonly text: string;
                readonly templateName: string;
                actionsTemplate(): any;
                treeListEditAction(): Utils.IAction;
                readonly hasContent: boolean;
                readonly actions: Utils.IAction[];
                readonly isDraggable: boolean;
                readonly treeListController: ITreeListController;
                toggleCollapsed: () => void;
                toggleSelected: any;
                isSelected: ko.Observable<boolean>;
                isHovered: ko.Observable<boolean>;
                isMultiSelected: ko.Observable<boolean>;
                getItems: () => JQueryPromise<TreeListItemViewModel[]>;
                dispose(): void;
                readonly visible: boolean;
                mouseenter(): void;
                mouseleave(): void;
                selectedItems(): TreeListItemViewModel[];
            }
            class TreeListRootItemViewModel extends TreeListItemViewModel {
                private _options;
                dispose(): void;
                private _selectedPathSubscription;
                constructor(_options: ITreeListOptions, path?: string[], onItemsVisibilityChanged?: () => any, rtl?: boolean);
                _onItemsChanged(): void;
            }
            interface ITreeListController {
                itemsFilter: (item: Utils.IDataMemberInfo, path?: string) => boolean;
                hasItems: (item: Utils.IDataMemberInfo) => boolean;
                canSelect: (value: TreeListItemViewModel) => boolean;
                select: (value: TreeListItemViewModel) => void;
                canMultiSelect?: (value: TreeListItemViewModel) => boolean;
                multiSelect?: (value: TreeListItemViewModel, isShiftPressed: boolean, isCtrlPressed: boolean) => void;
                getActions?: (item: TreeListItemViewModel) => Utils.IAction[];
                isDraggable?: (item: TreeListItemViewModel) => boolean;
                dblClickHandler?: (item: TreeListItemViewModel) => void;
                clickHandler?: (item: TreeListItemViewModel) => void;
                dragDropHandler?: any;
                selectedItems?: () => TreeListItemViewModel[];
                showIconsForChildItems?: (item?: TreeListItemViewModel) => boolean;
                searchName?: ko.Observable<string> | ko.Computed<string>;
                dispose?: () => void;
            }
            class TreeListController implements ITreeListController {
                dispose(): void;
                itemsFilter(item: Utils.IDataMemberInfo): boolean;
                hasItems(item: Utils.IDataMemberInfo): boolean;
                canSelect(value: TreeListItemViewModel): boolean;
                select(value: TreeListItemViewModel): void;
                selectedItem: any;
            }
            class ExpressionEditorTreeListController extends TreeListController {
                fieldName: any;
                putSelectionHandler: (item: TreeListItemViewModel, element: any) => void;
                selectionHandler?: (item: TreeListItemViewModel) => void;
                constructor(fieldName: any, putSelectionHandler: (item: TreeListItemViewModel, element: any) => void, selectionHandler?: (item: TreeListItemViewModel) => void);
                itemsFilter(item: Utils.IDataMemberInfo): boolean;
                select(value: Internal.TreeListItemViewModel): void;
                getActions(item: TreeListItemViewModel): {
                    clickAction: (element: any) => void;
                }[];
                canSelect(value: TreeListItemViewModel): boolean;
            }
            class ExpressionEditorParametersTreeListController extends TreeListController {
                customFilter: (item: Utils.IDataMemberInfo) => boolean;
                putSelectionHandler: (selectedItemPath: string, element: any) => void;
                selectionHandler?: (item: TreeListItemViewModel) => void;
                constructor(customFilter: (item: Utils.IDataMemberInfo) => boolean, putSelectionHandler: (selectedItemPath: string, element: any) => void, selectionHandler?: (item: TreeListItemViewModel) => void);
                itemsFilter(item: Utils.IDataMemberInfo): boolean;
                select(value: Internal.TreeListItemViewModel): void;
                getActions(item: TreeListItemViewModel): {
                    clickAction: (element: any) => void;
                }[];
                canSelect(value: TreeListItemViewModel): boolean;
            }
        }
        interface IExpressionOptions {
            value: ko.Observable<string> | ko.Computed<string>;
            path?: ko.Observable<string> | ko.Computed<string>;
            fieldName?: ko.Observable<string> | ko.Computed<string>;
            theme?: string;
            patchFieldName?: (fieldName: string) => string;
            functions?: Array<Internal.IExpressionEditorFunction>;
            rootItems?: Array<Internal.ICompletionRootItem>;
            customizeCategories?: (sender: any, categories: any, dblclick?: any) => void;
            validate?: (criteria: Criteria.CriteriaOperator) => boolean;
            isValid?: ko.Observable<boolean> | ko.Computed<boolean>;
        }
        module Internal {
            function createExpressionEditorCollectionToolOptions(collectionItems: any, toolName: string, displayToolName: string, showDescription: boolean): {
                displayName: any;
                content: {
                    showDescription: boolean;
                    isSelected: ko.Observable<boolean>;
                    data: {
                        items: any;
                        selectedItem: ko.Observable<any>;
                    };
                    name: string;
                };
                dispose: () => any;
            };
            function wrapExpressionValue(path: ko.Observable<string> | ko.Computed<string>, value: ko.Observable<string> | ko.Computed<string>, converter: Utils.IDisplayExpressionConverter, subscriptions: any): ko.Observable<string> | ko.Computed<string>;
        }
        class ExpressionEditor extends Utils.Disposable {
            private options;
            private _displayNameProvider?;
            dispose(): void;
            private _createMainPopupButtons;
            private _getTextArea;
            private _updateTextAreaValue;
            private _updateAceValue;
            private _updateValue;
            private patchFieldName;
            private _parametersPutSelectionHandler;
            private _fieldsPutSelectionHandler;
            private _createToolsOptions;
            constructor(options: IExpressionOptions, fieldListProvider: ko.Observable<Utils.IItemsProvider> | ko.Computed<Utils.IItemsProvider>, disabled?: ko.Observable<boolean> | ko.Computed<boolean>, rtl?: boolean, _displayNameProvider?: Utils.IDisplayNameProvider);
            displayExpressionConverter: Analytics.Internal.DisplayExpressionConverter;
            aceAvailable: boolean;
            tools: Internal.Tools;
            displayValue: ko.Observable<string> | ko.Computed<string>;
            popupVisible: ko.Observable<boolean>;
            title: () => any;
            value: ko.Observable<string> | ko.Computed<string>;
            textAreaValue: ko.Observable<string>;
            theme: string;
            languageHelper: {
                getLanguageMode: () => string;
                createCompleters: (editor: any, bindingContext: any, viewModel: ExpressionEditor) => Internal.CodeCompletor[];
            };
            aceOptions: {
                showLineNumbers: boolean;
                showPrintMargin: boolean;
                enableBasicAutocompletion: boolean;
                enableLiveAutocompletion: boolean;
                showFoldWidgets: boolean;
                highlightActiveLine: boolean;
            };
            additionalOptions: {
                onChange: (session: any) => void;
            };
            callbacks: {
                focus: () => any;
            };
            koOptions: ko.Observable<IExpressionOptions> | ko.Computed<IExpressionOptions>;
            editorContainer: ko.Observable<any> | ko.Computed<any>;
            fieldListProvider: ko.Observable<Utils.IItemsProvider> | ko.Computed<Utils.IItemsProvider>;
            parametersTreeListController: Internal.ExpressionEditorParametersTreeListController;
            save: (sender: any) => void;
            isValid: ko.Observable<boolean> | ko.Computed<boolean>;
            buttonItems: any[];
            rtl: boolean;
            modelValueValid: ko.Observable<boolean> | ko.Computed<boolean>;
            disabled: ko.Observable<boolean> | ko.Computed<boolean>;
            onShown(): void;
            getPopupContainer: typeof Analytics.Internal.getParentContainer;
        }
        module Filtering {
            class CriteriaOperatorSurface<T extends Criteria.CriteriaOperator> extends Utils.Disposable {
                _createLeftPartProperty(value: any): CriteriaOperatorSurface<Criteria.CriteriaOperator>;
                createChildSurface(item: any, path?: any, actions?: any): CriteriaOperatorSurface<Criteria.CriteriaOperator>;
                protected getDisplayType(): string;
                constructor(operator: T, parent: any, fieldListProvider: any, path: any);
                specifics: ko.Observable<string> | ko.Computed<string>;
                dataType: ko.Observable<string> | ko.Computed<string>;
                readonly items: Array<IFilterEditorOperator>;
                readonly displayType: string;
                readonly leftPart: CriteriaOperatorSurface<Criteria.CriteriaOperator>;
                readonly rightPart: any;
                readonly css: string;
                change(type: any, surface: any): void;
                remove(surface: any): void;
                popupService: any;
                canRemove: boolean;
                operatorType: ko.Observable<any>;
                parent: any;
                templateName: string;
                isSelected: ko.Observable<boolean> | ko.Computed<boolean>;
                operatorClass: string;
                helper: FilterEditorHelper;
                reverse: any;
                path: ko.Observable<string> | ko.Computed<string>;
                fieldListProvider: ko.Observable<Utils.IItemsProvider>;
                model: T;
            }
        }
        module Internal {
            class FilterEditorAddOn {
                private _popupService;
                private _action;
                private _updateActions;
                constructor(criteria: Filtering.CriteriaOperatorSurface<Criteria.CriteriaOperator>, popupService: Analytics.Internal.PopupService, action: string, propertyName: any, templateName?: any);
                showPopup: (_: any, element: any) => void;
                popupContentTemplate: string;
                propertyName: string;
                target: Filtering.CriteriaOperatorSurface<Criteria.CriteriaOperator>;
            }
            enum CriteriaSurfaceValidatorState {
                Left = 0,
                Right = 1,
                Unary = 2
            }
            class CriteriaSurfaceValidator {
                customValidate(operator: any, from: CriteriaSurfaceValidatorState): boolean;
                checkLeftPart(leftPart: any): boolean;
                _checkRightPart(criteriaOperator: any): any;
                checkRightPart(rigthPart: any): any;
                aggregateIsValid(criteriaOperator: Criteria.AggregateOperand): any;
                commonOperandValid(criteriaOperator: Criteria.CriteriaOperator): any;
                groupIsValid(criteriaOperator: Criteria.GroupOperator): boolean;
                unaryIsValid(criteriaOperator: Criteria.UnaryOperator): any;
                validateModel(criteriaOperator: Criteria.CriteriaOperator): any;
            }
            class FilterEditorSerializer {
                custom?: (criteriaOperator: Criteria.CriteriaOperator, reverse: boolean) => string;
                serializeGroupOperand(groupOperator: Criteria.GroupOperator, reverse: boolean): any;
                serializeAggregateOperand(aggregateOperand: Criteria.AggregateOperand, reverse: boolean): any;
                serializeOperandProperty(operandProperty: Criteria.OperandProperty): string;
                serializeOperandValue(operandValue: Criteria.OperandValue): any;
                serializeOperandParameter(operandParameter: Criteria.OperandParameter): string;
                serializeBetweenOperator(betweenOperator: Criteria.BetweenOperator, reverse: boolean): any;
                serializeInOperator(inOperator: Criteria.InOperator, reverse: boolean): any;
                serializeBinaryOperator(binaryOperator: Criteria.BinaryOperator, reverse: boolean): any;
                serializeUnaryOperator(unaryOperator: Criteria.UnaryOperator, reverse: boolean): any;
                serializeFunctionOperator(functionOperator: Criteria.FunctionOperator, reverse: boolean): any;
                constructor(operatorTokens?: {
                    "Plus": string;
                    "Minus": string;
                    "Equal": string;
                    "NotEqual": string;
                    "Greater": string;
                    "Less": string;
                    "LessOrEqual": string;
                    "GreaterOrEqual": string;
                }, custom?: (criteriaOperator: Criteria.CriteriaOperator, reverse: boolean) => string);
                serialize(criteriaOperator: Criteria.CriteriaOperator, reverse?: boolean): any;
                deserialize(stringCriteria: string): Criteria.CriteriaOperator;
                deserializeOperand(operand: Criteria.CriteriaOperator): Criteria.CriteriaOperator;
                operatorTokens: any;
            }
            class FilterEditorTreeListController extends TreeListController {
                constructor(selectedItem: ko.Observable<Utils.IDataMemberInfo>);
                itemsFilter(item: Utils.IDataMemberInfo): boolean;
                hasItems(item: Utils.IDataMemberInfo): boolean;
                canSelect(value: TreeListItemViewModel): boolean;
                select(value: TreeListItemViewModel): void;
            }
        }
        module Filtering {
            class AggregateOperandSurface extends CriteriaOperatorSurface<Criteria.AggregateOperand> {
                constructor(operator: Criteria.AggregateOperand, parent: CriteriaOperatorSurface<Criteria.CriteriaOperator>, fieldListProvider: any, path: any);
                readonly leftPart: any;
                readonly rightPart: any;
                dispose(): void;
                contentTemplateName: string;
                property: ko.Observable<any>;
                aggregatedExpression: ko.Observable<any>;
                condition: ko.Observable<any>;
            }
            class BetweenOperandSurface extends CriteriaOperatorSurface<Criteria.BetweenOperator> {
                constructor(operator: Criteria.BetweenOperator, parent: CriteriaOperatorSurface<Criteria.CriteriaOperator>, fieldListProvider: any, path: any);
                readonly leftPart: any;
                readonly rightPart: any[];
                dispose(): void;
                property: ko.Observable<any>;
                end: ko.Observable<any>;
                begin: ko.Observable<any>;
                contentTemplateName: string;
            }
            class BinaryOperandSurface extends CriteriaOperatorSurface<Criteria.BinaryOperator> {
                constructor(operator: Criteria.BinaryOperator, parent: any, fieldListProvider: any, path: any);
                readonly leftPart: CriteriaOperatorSurface<Criteria.CriteriaOperator>;
                readonly rightPart: any;
                dispose(): void;
                contentTemplateName: string;
                leftOperand: ko.Observable<any>;
                rightOperand: ko.Observable<any>;
            }
            class OperandSurfaceBase<T extends Criteria.CriteriaOperator> extends CriteriaOperatorSurface<T> {
                getRealParent(parent: any): any;
                getRealProperty(property: any): any;
                getPropertyName(parent: any, searchProperty: any): Criteria.Utils.IPropertyLocation;
                getConvertableParameters(destinationSpecifics: string): any[];
                constructor(operator: T, parent: CriteriaOperatorSurface<Criteria.CriteriaOperator>, fieldListProvider: any, path: any);
                readonly changeTypeItems: {
                    name: string;
                    instance: any;
                    localizationId: string;
                }[];
                canChange: boolean;
                canRemove: boolean;
                changeValueType: (type: any) => void;
            }
            class FunctionOperandSurface extends OperandSurfaceBase<Criteria.FunctionOperator> {
                constructor(operator: Criteria.FunctionOperator, parent: CriteriaOperatorSurface<Criteria.CriteriaOperator>, fieldListProvider: any, path: any);
                readonly leftPart: CriteriaOperatorSurface<Criteria.CriteriaOperator>;
                readonly rightPart: any[];
                readonly displayType: string;
                dispose(): void;
                canRemove: boolean;
                contentTemplateName: string;
                operands: ko.ObservableArray<any>;
            }
            class GroupOperandSurface extends CriteriaOperatorSurface<Criteria.GroupOperator> {
                constructor(operator: Criteria.GroupOperator, parent: any, fieldListProvider: any, path: any);
                change(type: any, surface: any): void;
                remove(surface: CriteriaOperatorSurface<Criteria.CriteriaOperator>): void;
                create(type: any): void;
                readonly rightPart: CriteriaOperatorSurface<Criteria.CriteriaOperator>[];
                dispose(): void;
                templateName: string;
                operatorClass: string;
                operands: ko.ObservableArray<CriteriaOperatorSurface<Criteria.CriteriaOperator>>;
                createItems: any;
            }
            class InOperandSurface extends CriteriaOperatorSurface<Criteria.InOperator> {
                constructor(operator: Criteria.InOperator, parent: any, fieldListProvider: any, path: any);
                readonly leftPart: any;
                readonly rightPart: any[];
                dispose(): void;
                addValue: () => void;
                contentTemplateName: string;
                operands: ko.ObservableArray<any>;
                criteriaOperator: ko.Observable<any>;
            }
            class OperandParameterSurface extends OperandSurfaceBase<Criteria.OperandParameter> {
                constructor(operator: Criteria.OperandParameter, parent: CriteriaOperatorSurface<Criteria.CriteriaOperator>, fieldListProvider?: any, path?: any);
                changeParameter: (item: Utils.IDataMemberInfo) => void;
                readonly items: any;
                readonly displayType: any;
                operatorClass: string;
                parameterName: ko.Observable<string> | ko.Computed<string>;
                templateName: string;
            }
            class OperandPropertySurface extends OperandSurfaceBase<Criteria.OperandProperty> {
                private _displayName;
                _updateDisplayName(path: any, propertyName: any, displayName: any): void;
                _updateSpecifics(): void;
                constructor(operator: Criteria.OperandProperty, parent: CriteriaOperatorSurface<Criteria.CriteriaOperator>, fieldListProvider?: any, path?: any);
                fieldsOptions: ko.Observable<any>;
                displayName: ko.Computed<string>;
                propertyName: ko.Observable<string>;
                specifics: ko.Observable<string>;
                dataType: ko.Observable<string>;
                readonly items: any;
                readonly displayType: any;
                valueType: ko.Observable<string>;
                changeProperty: (item: Utils.IDataMemberInfo) => void;
                templateName: string;
                operatorClass: string;
            }
            class OperandValueSurface extends OperandSurfaceBase<Criteria.OperandValue> {
                private static _defaultValue;
                private _value;
                private _updateDate;
                readonly items: any[];
                constructor(operator: Criteria.OperandValue, parent: CriteriaOperatorSurface<Criteria.CriteriaOperator>, fieldListProvider: Utils.IItemsProvider, path: any);
                readonly displayType: any;
                changeValue: () => void;
                isDefaultDisplay(): boolean;
                getDefaultValue(): any;
                dataType: ko.Observable<string> | ko.Computed<string>;
                values: ko.Observable<any[]>;
                value: ko.Observable<string> | ko.Computed<string>;
                dataSource: ko.Observable<DataSource> | ko.Computed<DataSource>;
                isEditable: ko.Observable<boolean> | ko.Computed<boolean>;
                templateName: string;
                getNumberEditorOptions: () => any;
            }
            class UnaryOperandSurface extends CriteriaOperatorSurface<Criteria.UnaryOperator> {
                constructor(operator: Criteria.UnaryOperator, parent: any, fieldListProvider?: any, path?: any);
                readonly leftPart: any;
                readonly rightPart: any;
                dispose(): void;
                contentTemplateName: string;
                operand: ko.Observable<any>;
            }
        }
        module Internal {
            function initDisplayText(object: {
                name: string;
                localizationId?: string;
                displayText?: string;
            }): void;
        }
        interface IFilterEditorOperator {
            name: string;
            value: any;
            type: any;
            hidden?: boolean;
            reverse?: boolean;
            localizationId?: string;
            insertVal?: string;
            displayText?: string;
            paramCount?: number;
        }
        class FilterEditorHelper {
            private _initDisplayText;
            constructor(serializer?: any);
            registrateOperator(specific: string, targetEnum: any, value: string, name: string, reverse?: boolean, localizationId?: string): void;
            rtl: boolean;
            parameters: ko.Observable<any[]> | ko.Computed<any[]>;
            canSelectLists: boolean;
            canCreateParameters: boolean;
            canChoiceParameters: boolean;
            canChoiceProperty: boolean;
            serializer: Internal.FilterEditorSerializer;
            criteriaTreeValidator: Internal.CriteriaSurfaceValidator;
            filterEditorOperators: {
                _common: IFilterEditorOperator[];
                string: IFilterEditorOperator[];
                guid: IFilterEditorOperator[];
                integer: IFilterEditorOperator[];
                float: IFilterEditorOperator[];
                date: IFilterEditorOperator[];
                list: IFilterEditorOperator[];
                group: IFilterEditorOperator[];
            };
            onChange: () => void;
            onEditorFocusOut: (criteria: Criteria.CriteriaOperator) => void;
            onSave: (criteria: string) => void;
            onClosing: () => void;
            handlers: {
                create: (criteria: any, popupService: any) => {
                    data: Internal.FilterEditorAddOn;
                    templateName: string;
                };
                change: (criteria: any, popupService: any) => {
                    data: Internal.FilterEditorAddOn;
                    templateName: string;
                };
                changeProperty: (criteria: any, popupService: any) => {
                    data: Internal.FilterEditorAddOn;
                    templateName: string;
                };
                changeValueType: (criteria: any, popupService: any) => {
                    data: Internal.FilterEditorAddOn;
                    templateName: string;
                };
                changeParameter: (criteria: any, popupService: any) => {
                    data: Internal.FilterEditorAddOn;
                    templateName: string;
                };
            };
            generateTreelistOptions(fieldListProvider: any, path: any): any;
            mapper: {
                Aggregate: typeof Filtering.AggregateOperandSurface;
                Property: typeof Filtering.OperandPropertySurface;
                Parameter: typeof Filtering.OperandParameterSurface;
                Value: typeof Filtering.OperandValueSurface;
                Group: typeof Filtering.GroupOperandSurface;
                Between: typeof Filtering.BetweenOperandSurface;
                Binary: typeof Filtering.BinaryOperandSurface;
                Function: typeof Filtering.FunctionOperandSurface;
                In: typeof Filtering.InOperandSurface;
                Unary: typeof Filtering.UnaryOperandSurface;
                Default: typeof Filtering.CriteriaOperatorSurface;
            };
            aceTheme: string;
            getDisplayPropertyName: (path: string, name: string) => JQueryPromise<string>;
        }
        var DefaultFilterEditorHelper: typeof FilterEditorHelper;
        module Internal {
            class FilterEditorCodeCompletor extends CodeCompletor {
                filterEditorAvailable: {
                    operators: Array<{
                        name: string;
                        insertVal: string;
                        paramCount: number;
                    }>;
                    aggregate: Array<{
                        name: string;
                        insertVal: string;
                    }>;
                    functions: Array<{
                        name: string;
                        insertVal: string;
                    }>;
                };
                constructor(options: ICodeCompletorOptions);
                getFunctionsCompletions(): any[];
                getAggregateCompletions(): any[];
                getOperatorCompletions(prefix: any): any[];
            }
        }
        interface IFilterEditorAddon {
            data: Internal.FilterEditorAddOn;
            templateName: string;
        }
        interface IAdvancedState {
            value: ko.Observable<boolean> | ko.Computed<boolean>;
            animated: boolean;
        }
        class FilterEditor extends Utils.Disposable {
            options: ko.Observable<IFilterEditorOptions> | ko.Computed<IFilterEditorOptions>;
            private _displayNameProvider?;
            private _advancedMode;
            private _createMainPopupButtons;
            private _generateOperand;
            private _generateSurface;
            private _validateValue;
            constructor(options: ko.Observable<IFilterEditorOptions> | ko.Computed<IFilterEditorOptions>, fieldListProvider: ko.Observable<Utils.IItemsProvider> | ko.Computed<Utils.IItemsProvider>, rtl?: boolean, _displayNameProvider?: Utils.IDisplayNameProvider);
            change(type: any, surface: any): void;
            readonly helper: FilterEditorHelper;
            readonly path: ko.Observable<string> | ko.Computed<string>;
            displayValue: ko.Observable<string> | ko.Computed<string>;
            modelDisplayValue: ko.Observable<string> | ko.Computed<string>;
            disabled: ko.Observable<boolean> | ko.Computed<boolean>;
            dispose(): void;
            onInput(s: any, e: any): void;
            onFocus(): void;
            onBlur(): void;
            cacheElement($element: JQuery): void;
            updateCriteria(): void;
            onValueChange(value: any): void;
            focusText(): void;
            textFocused: ko.Observable<boolean>;
            aceAvailable: boolean;
            languageHelper: {
                getLanguageMode: () => string;
                createCompleters: (editor: any, bindingContext: any, viewModel: any) => Internal.FilterEditorCodeCompletor[];
            };
            aceOptions: {
                showLineNumbers: boolean;
                showPrintMargin: boolean;
                enableBasicAutocompletion: boolean;
                enableLiveAutocompletion: boolean;
                showGutter: boolean;
            };
            additionalOptions: {
                onChange: (session: any) => void;
                changeTimeout: number;
                onFocus: (_: any) => void;
                onBlur: (_: any) => void;
            };
            editorContainer: ko.Observable<any>;
            textVisible: ko.Observable<boolean>;
            getPopupContainer: (el: any) => JQuery;
            timeout: any;
            animationTimeout: any;
            advancedMode: ko.Computed<boolean>;
            invalidMessage: () => any;
            advancedModeText: () => any;
            modelValueIsValid: ko.Computed<boolean>;
            isSurfaceValid: ko.Computed<boolean>;
            showText: ko.Observable<boolean> | ko.Computed<boolean>;
            displayExpressionConverter: Analytics.Internal.DisplayExpressionConverter;
            isValid: ko.Computed<boolean>;
            fieldListProvider: any;
            createAddButton: (criteria: any) => IFilterEditorAddon;
            createChangeType: (criteria: any) => IFilterEditorAddon;
            createChangeProperty: (criteria: any) => IFilterEditorAddon;
            createChangeParameter: (criteria: any) => IFilterEditorAddon;
            createChangeValueType: (criteria: any) => IFilterEditorAddon;
            operandSurface: ko.Observable<any>;
            operand: any;
            save: () => void;
            popupVisible: ko.Observable<boolean> | ko.Computed<boolean>;
            buttonItems: any[];
            popupService: Analytics.Internal.PopupService;
            value: ko.Observable<string> | ko.Computed<string>;
            rtl: boolean;
        }
        class FilterEditorPlain extends FilterEditor {
            private element;
            private _contentMargins;
            private _topOffset;
            private _defaultActiveTextContentHeightPerc;
            private _defaultActiveTreeContentHeightPerc;
            private _currentActiveTextContentHeightPerc;
            private _currentActiveTreeContentHeightPerc;
            constructor(element: Element, options: ko.Observable<IFilterEditorOptions>, fieldListProvider: ko.Observable<Utils.IItemsProvider>, rtl?: boolean, _displayNameProvider?: Utils.IDisplayNameProvider);
            plainContentHeightPerc: ko.Observable<string>;
            textContentHeightPerc: ko.Observable<any>;
            treeContentHeightPerc: ko.Observable<any>;
        }
        interface IFilterEditorOptions {
            value: ko.Observable<string> | ko.Computed<string>;
            path: ko.Observable<string> | ko.Computed<string>;
            helper?: FilterEditorHelper;
            disabled?: ko.Observable<boolean> | ko.Computed<boolean>;
        }
        class FilterStringOptions implements IFilterEditorOptions {
            private _title;
            constructor(filterString: ko.Observable<string> | ko.Computed<string>, dataMember?: ko.Observable | ko.Computed, disabled?: ko.Observable<boolean> | ko.Computed<boolean>, title?: {
                text: string;
                localizationId?: string;
            });
            popupContainer: string;
            itemsProvider: any;
            disabled: ko.Observable<boolean> | ko.Computed<boolean>;
            resetValue: () => void;
            helper: FilterEditorHelper;
            value: ko.Observable<string> | ko.Computed<string>;
            path: ko.Observable<string> | ko.Computed<string>;
            title: ko.PureComputed<string>;
        }
        module Internal {
            interface IStandardPattern {
                type: string;
                value: any;
                patterns: Array<string>;
            }
            var formatStringStandardPatterns: {
                [key: string]: IStandardPattern;
            };
        }
        interface IPatternItem {
            name: string;
            canRemove: boolean;
        }
        interface IFormatStringEditorActions {
            updatePreview?: (value: string, category: string, pattern: string) => JQueryPromise<string>;
            saveCustomPattern?: (category: string, pattern: string) => JQueryPromise<boolean>;
            removeCustomPattern?: (category: string, pattern: string) => JQueryPromise<boolean>;
        }
        class FormatStringEditor extends Utils.Disposable {
            private _standardPatternSource;
            private _customPatternSource;
            private _lastUpdatePreviewPromise;
            private okAction;
            private _createMainPopupButtons;
            private _convertArray;
            private _scrollToBottom;
            private _updateFormatList;
            private _updateSelection;
            private _updatePreview;
            private _getGeneralPreview;
            private _wrapFormat;
            private _updateCanAddCustomFormat;
            private _initEditor;
            constructor(value: ko.Observable<string>, disabled?: ko.Observable<boolean>, defaultPatterns?: {
                [key: string]: Internal.IStandardPattern;
            }, customPatterns?: {
                [key: string]: Array<string>;
            }, actions?: IFormatStringEditorActions, rtl?: ko.Observable<boolean>, popupContainer?: string);
            updateInputText(propertyName: string, componentInstance: any): void;
            option(name: any, value?: any): any;
            updatePreview(value: string, category: string, pattern: string): JQueryPromise<string>;
            readonly customPatterns: string[];
            readonly isGeneralType: boolean;
            getDisplayText(key: any): any;
            getPopupContainer(el: any): any[];
            currentType: ko.Observable<string>;
            setType: (e: {
                itemData: IPatternItem;
            }) => void;
            setFormat: (e: {
                itemData: IPatternItem;
            }) => void;
            types: Array<IPatternItem>;
            patternList: ko.ObservableArray<IPatternItem>;
            addCustomFormat: () => void;
            removeCustomFormat: (e: any) => void;
            canAddCustomFormat: ko.Observable<boolean>;
            formatPrefix: ko.Observable<string>;
            formatSuffix: ko.Observable<string>;
            previewString: ko.Observable<string>;
            formatResult: ko.Observable<string>;
            selectedFormats: ko.Observable<IPatternItem[]>;
            selectedTypes: ko.Observable<IPatternItem[]>;
            popupService: Analytics.Internal.PopupService;
            popupVisible: ko.Observable<boolean>;
            buttonItems: Array<any>;
            localizationIdMap: {
                [key: string]: Analytics.Internal.ILocalizationInfo;
            };
        }
        module Internal {
            class dxPopupWithAutoHeight extends dxPopup {
                constructor(element: Element, options?: dxPopupOptions);
                _setContentHeight(): void;
            }
        }
    }
    module Internal {
        interface ISearchHighlightOptions {
            text: string | ko.Observable<string>;
            textToSearch: ko.Observable<string> | ko.Computed<string>;
        }
        function cloneHtmlBinding(data: {
            content: any;
        } & Utils.Disposable, element: any, allBindings: any, viewModel: any, bindingContext: any): void;
        class HighlightEngine extends Utils.Disposable {
            private _$spanProtect;
            private _$spanSearch;
            content: ko.Observable<string>;
            private _getHighlightContent;
            constructor(options: ISearchHighlightOptions);
        }
    }
    module Widgets {
        module Internal {
        }
    }
}

declare module DevExpress.Analytics {
    module Utils {
        class ControlsFactory {
            getControlInfo(controlType: string): Elements.IElementMetadata;
            getControlType(model: any): string;
            createControl(model: any, parent: Elements.ElementViewModel, serializer?: IModelSerializer): Elements.IElementViewModel;
            controlsMap: {
                [key: string]: Elements.IElementMetadata;
            };
            registerControl(typeName: string, metadata: Elements.IElementMetadata): void;
            _getPropertyInfo(info: Utils.ISerializationInfoArray, path: string[], position: number): any;
            getPropertyInfo(controlType: string, path: any): any;
        }
        function floatFromModel(val: string): ko.Observable<number>;
        function fromEnum(value: string): ko.Observable<string>;
        function parseBool(val: any): ko.Observable<any>;
        function colorFromString(val: string): ko.Observable<string>;
        function saveAsInt(val: number): string;
        function colorToString(val: string): string;
    }
    module Internal {
        interface IActionsProvider {
            getActions: (context: any) => Utils.IAction[];
        }
        class BaseActionsProvider implements IActionsProvider {
            actions: Utils.IAction[];
            initActions(actions: Utils.IAction[]): void;
            getActions(context: any): Utils.IAction[];
            condition(context: any): boolean;
            setDisabled: (context: any) => void;
        }
        class RequestHelper {
            static generateUri(host: string, uri: string): string;
        }
        class JSDesignerBindingCommon<T> extends Utils.Disposable {
            protected _options: any;
            protected _customEventRaiser?: any;
            sender: T;
            dispose(): void;
            protected _fireEvent(eventName: any, args?: any): void;
            protected _getServerActionUrl(host: any, uri: any): string;
            protected _getAvailableEvents(events: any, prefix?: string): any;
            protected _templateHtml: string;
            protected _getLocalizationRequest(): JQueryPromise<{}>;
            protected _createDisposeFunction(element: HTMLElement): void;
            constructor(_options: any, _customEventRaiser?: any);
        }
    }
    module Elements {
        class Rectangle {
            constructor(left?: number, top?: number, width?: number, height?: number);
            left: ko.Observable<number>;
            top: ko.Observable<number>;
            width: ko.Observable<number>;
            height: ko.Observable<number>;
            className: string;
        }
    }
    module Internal {
        class DragDropHandler extends Utils.Disposable {
            dispose(): void;
            static started: ko.Observable<boolean>;
            surface: ko.Observable<Elements.ISurfaceContext> | ko.Computed<Elements.ISurfaceContext>;
            selection: SurfaceSelection;
            snapHelper: SnapLinesHelper;
            dragHelperContent: DragHelperContent;
            _size: Elements.Size;
            _getAbsoluteSurfacePosition(ui: any): {
                left: number;
                top: number;
            };
            constructor(surface: ko.Observable<Elements.ISurfaceContext> | ko.Computed<Elements.ISurfaceContext>, selection: SurfaceSelection, undoEngine: ko.Observable<Utils.UndoEngine> | ko.Computed<Utils.UndoEngine>, snapHelper?: SnapLinesHelper, dragHelperContent?: DragHelperContent);
            addControl(control: any, dropTargetSurface: any, size: any): void;
            recalculateSize(size: any): void;
            helper(draggable: any, event?: any): void;
            startDrag(draggable: any): void;
            drag(event: JQueryEventObject, ui: JQueryUI.DraggableEventUIParams): void;
            stopDrag: (ui: JQueryUI.ResizableUIParams, draggable: any, event?: any) => void;
            doStopDrag(ui: any, draggable: any, event?: any): void;
            cursor: string;
            containment: string;
            alwaysAlt: boolean;
        }
        interface IShortcutActionList {
            processShortcut: (actions: Utils.IAction[], e: JQueryKeyEventObject) => void;
            toolbarItems: Utils.IAction[] | ko.Observable<Utils.IAction[]> | ko.Computed<Utils.IAction[]>;
            enabled?: ko.Observable<boolean> | ko.Computed<boolean>;
        }
        class KeyboardHelper {
            private _selection;
            private _undoEngine;
            constructor(selection: ISelectionProvider, undoEngine?: ko.Observable<Utils.UndoEngine> | ko.Computed<Utils.UndoEngine>);
            processShortcut(e: JQueryKeyEventObject): boolean;
            processEsc(): void;
            moveSelectedControls(leftUp: boolean, isHoriz: boolean, sign: number): void;
            shortcutMap: {
                [key: number]: (e: any) => boolean;
            };
        }
        class KeyDownHandlersManager {
            private _handlers;
            private _targetElement;
            private readonly _activeHandler;
            private _removeHandler;
            constructor(targetElement: JQuery);
            bindHandler(element: Element, handler: (e: JQueryKeyEventObject) => void): void;
        }
        class DragHelperControlRectangle extends Elements.Rectangle {
            position: number;
            constructor(position: number, left?: number, top?: number, width?: number, height?: number);
        }
        class DragHelperContent extends Elements.Rectangle {
            private _selectionProvider;
            private readonly _isEmpty;
            constructor(selectionProvider: ISelectionProvider);
            reset(): void;
            controls: ko.ObservableArray<Elements.Rectangle | DragHelperControlRectangle>;
            customData: ko.Observable<{}>;
            update(surface: Elements.SurfaceElementBase<any>): void;
            setContent(area: Elements.Rectangle, customData?: {
                template: string;
                data?: any;
            }): void;
            isLocked: ko.Observable<boolean>;
        }
        class SelectionDragDropHandler extends DragDropHandler {
            adjustDropTarget(dropTargetSurface: ISelectionTarget): ISelectionTarget;
            constructor(surface: ko.Observable<Elements.ISurfaceContext> | ko.Computed<Elements.ISurfaceContext>, selection: SurfaceSelection, undoEngine: ko.Observable<Utils.UndoEngine> | ko.Computed<Utils.UndoEngine>, snapHelper: SnapLinesHelper, dragHelperContent: DragHelperContent);
            startDrag(control: ISelectionTarget): void;
            drag(event: JQueryEventObject, ui: JQueryUI.DraggableEventUIParams): void;
            getLocation(adjustedTarget: any, item: any): Elements.IArea;
            ajustLocation(adjustedTarget: any, item: any): void;
            doStopDrag(ui: any, _: any): void;
            create(event: JQueryEventObject, ui: JQueryUI.DraggableEventUIParams): void;
        }
        class ToolboxDragDropHandler extends DragDropHandler {
            private _controlsFactory;
            constructor(surface: ko.Observable<Elements.ISurfaceContext> | ko.Computed<Elements.ISurfaceContext>, selection: SurfaceSelection, undoEngine: ko.Observable<Utils.UndoEngine> | ko.Computed<Utils.UndoEngine>, snapHelper: SnapLinesHelper, dragHelperContent: DragHelperContent, controlsFactory: Utils.ControlsFactory);
            helper(draggable: any): void;
            doStopDrag(ui: any, draggable: any): void;
        }
    }
    module Widgets {
        class ColorPickerEditor extends Widgets.Editor {
            constructor(info: Utils.ISerializationInfo, level: any, parentDisabled?: ko.Computed<boolean>, textToSearch?: any);
            displayValue: ko.Computed<string>;
        }
        class FieldListEditor extends Widgets.Editor {
            constructor(modelPropertyInfo: any, level: any, parentDisabled?: ko.Computed<boolean>, textToSearch?: any);
            path: ko.PureComputed<any>;
            treeListController: Internal.TreeListController;
        }
        module Internal {
            class DataMemberTreeListController implements ITreeListController {
                dispose(): void;
                itemsFilter(item: Utils.IDataMemberInfo): boolean;
                hasItems(item: Utils.IDataMemberInfo): boolean;
                canSelect(value: Widgets.Internal.TreeListItemViewModel): boolean;
                select(value: Widgets.Internal.TreeListItemViewModel): void;
                selectedItem: any;
                suppressActions: boolean;
            }
        }
        class DataMemberEditor extends FieldListEditor {
            constructor(modelPropertyInfo: any, level: any, parentDisabled?: ko.Computed<boolean>, textToSearch?: any);
            treeListController: Internal.DataMemberTreeListController;
        }
        module Internal {
            class RequiredNullableEditor extends Editor {
                _getEditorValidationRules(): any[];
            }
            function createNumericEditor(dotNetTypeFullName: string, specifics: string): {
                header: string;
                editorType: any;
            };
        }
        var coreEditorTemplates: {
            guid: {
                header: string;
                editorType: typeof GuidEditor;
            };
            date: {
                header: string;
                editorType: typeof Internal.RequiredNullableEditor;
            };
            borders: {
                header: string;
            };
            objecteditorCustom: {
                custom: string;
                editorType: typeof PropertyGridEditor;
            };
            field: {
                header: string;
                editorType: typeof FieldListEditor;
            };
            dataMember: {
                header: string;
                editorType: typeof DataMemberEditor;
            };
            filterEditor: {
                header: string;
            };
            formatEditor: {
                header: string;
            };
            expressionEditor: {
                header: string;
            };
            customColorEditor: {
                header: string;
                editorType: typeof ColorPickerEditor;
            };
            sbyte: {
                header: string;
                editorType: any;
            };
            decimal: {
                header: string;
                editorType: any;
            };
            int64: {
                header: string;
                editorType: any;
            };
            int32: {
                header: string;
                editorType: any;
            };
            int16: {
                header: string;
                editorType: any;
            };
            single: {
                header: string;
                editorType: any;
            };
            double: {
                header: string;
                editorType: any;
            };
            byte: {
                header: string;
                editorType: any;
            };
            uint16: {
                header: string;
                editorType: any;
            };
            uint32: {
                header: string;
                editorType: any;
            };
            uint64: {
                header: string;
                editorType: any;
            };
        };
        var editorTemplates: any;
    }
    module Elements {
        interface IArea {
            top?: number;
            left?: number;
            right?: number;
            bottom?: number;
            width?: number;
            height?: number;
        }
        interface ISurfaceContext {
            measureUnit: ko.Observable<string> | ko.Computed<string>;
            pageWidth?: ko.Observable<number> | ko.Computed<number>;
            pageHeight?: ko.Observable<number> | ko.Computed<number>;
            snapGridSize?: ko.Observable<number> | ko.Computed<number>;
            margins?: IMargins;
            zoom?: ko.Observable<number> | ko.Computed<number>;
            dpi?: ko.Observable<number> | ko.Computed<number>;
            isFit?: (dropTarget: Internal.ISelectionTarget) => boolean;
            rtl?: ko.Observable<boolean> | ko.Computed<boolean>;
        }
        class SurfaceElementArea<M extends ElementViewModel> extends Utils.Disposable {
            _control: M;
            _width: ko.Observable<number> | ko.Computed<number>;
            _height: ko.Observable<number> | ko.Computed<number>;
            _x: ko.Observable<number> | ko.Computed<number>;
            _y: ko.Observable<number> | ko.Computed<number>;
            _context: ISurfaceContext;
            _createSurface: (item: ElementViewModel) => any;
            private _container;
            private _getX;
            private _setX;
            getRoot(): ISurfaceContext;
            preInitProperties(control: M, context: ISurfaceContext, unitProperties: Internal.IUnitProperties<M>): void;
            constructor(control: M, context: ISurfaceContext, unitProperties: Internal.IUnitProperties<M>);
            rect: ko.Observable<IArea> | ko.Computed<IArea>;
            container(): SurfaceElementArea<ElementViewModel>;
            beforeRectUpdated(rect: any): any;
            rtlLayout(): boolean;
            getControlModel(): M;
        }
        class SurfaceElementBase<M extends ElementViewModel> extends SurfaceElementArea<M> implements Internal.ISelectionTarget {
            private _countSelectedChildren;
            context: ISurfaceContext;
            constructor(control: M, context: ISurfaceContext, unitProperties: Internal.IUnitProperties<M>);
            focused: ko.Observable<boolean> | ko.Computed<boolean>;
            selected: ko.Observable<boolean> | ko.Computed<boolean>;
            isSelected: ko.Observable<boolean> | ko.Computed<boolean>;
            cssCalculator: Internal.CssCalculator;
            underCursor: ko.Observable<Internal.IHoverInfo> | ko.Computed<Internal.IHoverInfo>;
            readonly parent: any;
            checkParent(surfaceParent: Internal.ISelectionTarget): boolean;
            allowMultiselect: boolean;
            css: ko.Observable<any> | ko.Computed<any>;
            contentCss: ko.Observable<any> | ko.Computed<any>;
            _getChildrenHolderName(): string;
            getChildrenCollection(): ko.ObservableArray<any>;
            absolutePosition: Point;
            updateAbsolutePosition(): void;
            canDrop(): boolean;
            afterUpdateAbsolutePosition(): void;
            absoluteRect: ko.Computed<IArea>;
            getUsefulRect: () => IArea;
            locked: boolean;
        }
        interface IElementMetadata {
            info: Utils.ISerializationInfoArray;
            surfaceType: any;
            type?: any;
            nonToolboxItem?: boolean;
            isToolboxItem?: boolean;
            toolboxIndex?: number;
            defaultVal?: {};
            size?: string;
            isContainer?: boolean;
            isCopyDeny?: boolean;
            isPasteDeny?: boolean;
            isDeleteDeny?: boolean;
            popularProperties?: string[];
            canDrop?: (dropTarget: Internal.ISelectionTarget, dragFrom?: ElementViewModel) => boolean;
            elementActionsTypes?: any;
            parentType?: string;
            displayName?: string;
        }
        interface IElementViewModel {
            controlType: string;
            name: ko.Observable<string> | ko.Computed<string>;
            parentModel: ko.Observable<IElementViewModel>;
            addChild: (element: IElementViewModel) => void;
            addChilds: (array: IElementViewModel[]) => void;
            removeChild: (element: IElementViewModel) => void;
            removeChilds: (array: IElementViewModel[]) => void;
            getNearestParent: (dropTarget: IElementViewModel) => IElementViewModel;
        }
        interface IControlPropertiesViewModel {
            isPropertyDisabled: (name: string) => boolean;
            isPropertyVisible: (name: string) => boolean;
            isPropertyModified: (name: string) => boolean;
            controlType?: string;
            actions: Internal.IModelAction[];
        }
        class ElementViewModel extends Utils.Disposable implements IElementViewModel, IControlPropertiesViewModel {
            getPropertyDefaultValue(propertyName: string): any;
            getPropertyInfo(propertyName: string): Utils.ISerializationInfo;
            getInfo(): Utils.ISerializationInfoArray;
            createControl(model: any, serializer?: Utils.IModelSerializer): IElementViewModel;
            dispose(): void;
            preInitProperties(model: any, parent: ElementViewModel, serializer?: Utils.IModelSerializer): void;
            constructor(model: any, parent: ElementViewModel, serializer?: Utils.IModelSerializer);
            getNearestParent(target: IElementViewModel): any;
            getControlInfo(): IElementMetadata;
            getMetaData(): {
                isContainer: boolean;
                isCopyDeny: boolean;
                isDeleteDeny: boolean;
                canDrop: (dropTarget: Internal.ISelectionTarget, dragFrom?: ElementViewModel) => boolean;
                isPasteDeny: boolean;
            };
            _hasModifiedValue(name: any): any;
            name: ko.Observable<string> | ko.Computed<string>;
            controlType: string;
            createChild(info: {}): ElementViewModel;
            removeChilds(controls: ElementViewModel[]): void;
            addChilds(controls: ElementViewModel[]): void;
            removeChild(control: ElementViewModel): void;
            addChild(control: IElementViewModel): void;
            isPropertyVisible(name: string): boolean;
            isPropertyDisabled(name: string): boolean;
            isPropertyModified(name: string): any;
            getControlFactory(): Utils.ControlsFactory;
            resetValue: (propertyName: string) => void;
            isResettableProperty(propertyName: string): boolean;
            surface: any;
            parentModel: ko.Observable<ElementViewModel>;
            readonly root: ElementViewModel;
            rtl(): boolean;
            actions: Internal.IModelAction[];
            update: ko.Observable<boolean>;
        }
        interface IMargins {
            bottom: ko.Observable<number> | ko.Computed<number>;
            left: ko.Observable<number> | ko.Computed<number>;
            right: ko.Observable<number> | ko.Computed<number>;
            top: ko.Observable<number> | ko.Computed<number>;
        }
        class Margins implements IMargins {
            static defaultVal: string;
            static unitProperties: string[];
            getInfo(): Utils.ISerializationInfo[];
            constructor(left: any, right: any, top: any, bottom: number);
            isEmpty(): boolean;
            static fromString(value?: string): Margins;
            toString(): string;
            bottom: ko.Observable<number> | ko.Computed<number>;
            left: ko.Observable<number> | ko.Computed<number>;
            right: ko.Observable<number> | ko.Computed<number>;
            top: ko.Observable<number> | ko.Computed<number>;
        }
        module Metadata {
            var left: Utils.ISerializationInfo, right: Utils.ISerializationInfo, top: Utils.ISerializationInfo, bottom: Utils.ISerializationInfo, all: Utils.ISerializationInfo;
            var paddingSerializationsInfo: Utils.ISerializationInfo[];
        }
        class PaddingModel extends Utils.Disposable {
            left: ko.Observable<number> | ko.Computed<number>;
            right: ko.Observable<number> | ko.Computed<number>;
            top: ko.Observable<number> | ko.Computed<number>;
            bottom: ko.Observable<number> | ko.Computed<number>;
            dpi: ko.Observable<number> | ko.Computed<number>;
            static defaultVal: string;
            static unitProperties: string[];
            getInfo(): Utils.ISerializationInfo[];
            resetValue(): void;
            isEmpty(): boolean;
            applyFromString(value: string): this;
            static from(val: any): PaddingModel;
            toString(): string;
            _toString(inner?: boolean): string;
            constructor(left?: ko.Observable<number> | ko.Computed<number>, right?: ko.Observable<number> | ko.Computed<number>, top?: ko.Observable<number> | ko.Computed<number>, bottom?: ko.Observable<number> | ko.Computed<number>, dpi?: ko.Observable<number> | ko.Computed<number>);
            all: ko.Computed<number>;
        }
        interface IPoint {
            x: ko.Observable<number> | ko.Computed<number>;
            y: ko.Observable<number> | ko.Computed<number>;
        }
        class Point implements IPoint {
            static unitProperties: string[];
            constructor(x: any, y: number);
            getInfo(): Utils.ISerializationInfoArray;
            static fromString(value?: string): Point;
            toString(): string;
            x: ko.Observable<number> | ko.Computed<number>;
            y: ko.Observable<number> | ko.Computed<number>;
        }
        class SerializableModel extends Utils.Disposable {
            preInitProperties(model: any, serializer?: Utils.IModelSerializer, info?: Utils.ISerializationInfoArray): void;
            constructor(model: any, serializer?: Utils.IModelSerializer, info?: Utils.ISerializationInfoArray);
            getInfo: () => Utils.ISerializationInfoArray;
        }
        interface ISize {
            width: ko.Observable<number> | ko.Computed<number>;
            height: ko.Observable<number> | ko.Computed<number>;
            isPropertyDisabled: (name: string) => void;
        }
        class Size implements ISize {
            static unitProperties: string[];
            constructor(width: any, height: number);
            getInfo(): Utils.ISerializationInfoArray;
            static fromString(value?: string): Size;
            toString(): string;
            isPropertyDisabled: (name: string) => any;
            isPropertyVisible: (name: string) => boolean;
            width: ko.Observable<number> | ko.Computed<number>;
            height: ko.Observable<number> | ko.Computed<number>;
        }
    }
    module Internal {
        function getToolboxItems(controlsMap: {
            [key: string]: Elements.IElementMetadata;
        }): Utils.ToolboxItem[];
        function blur(element: Element): void;
        function guid(): string;
    }
    module Internal {
        class AjaxSetup {
            ajaxSettings: JQueryAjaxSettings;
            sendRequest(settings: JQueryAjaxSettings): JQueryXHR;
        }
    }
    module Utils {
        var ajaxSetup: Internal.AjaxSetup;
    }
    module Internal {
        interface IDisplayedObject {
            name: ko.Observable<string> | ko.Computed<string>;
        }
        interface IDesignControlsHelper extends Utils.IDisposable {
            getControls: (target: any) => ko.ObservableArray<IDisplayedObject>;
            allControls: ko.ObservableArray<IDisplayedObject>;
            getNameProperty?: (model: any) => ko.Observable<string> | ko.Computed<string>;
        }
        class DesignControlsHelper extends Utils.Disposable implements IDesignControlsHelper {
            protected target: any;
            private collectionNames?;
            private _handlers;
            private _setText;
            private _visitedCollections;
            private _subscriptions;
            getNameProperty(model: any): any;
            protected _setName(value: any): void;
            dispose(): void;
            private added;
            private deleted;
            _collectControls(target: any): void;
            constructor(target: any, handlers?: Array<{
                added: (control: any) => void;
                deleted?: (control: any) => void;
            }>, collectionNames?: string[]);
            getControls(target: any): ko.ObservableArray<IDisplayedObject>;
            allControls: ko.ObservableArray<IDisplayedObject>;
        }
        interface IStyleContainer {
            rtl: () => boolean;
        }
        function patchPositionByRTL(position: string, rtl: boolean): string;
        class CssCalculator {
            private _rtlLayout;
            static DEFAULT_BORDER: string;
            private _control;
            private _getPixelValueFromUnit;
            private _patchPosition;
            private _getBorderWidth;
            createBorder(dashStyle: any, width: any, color: any, positions: any, position: any): {};
            createControlBorder(borderStyle: any, width: any, color: any, positions: any, position: any, defaultColor?: string): {};
            createBorders(borderStyle: any, width: any, color: any, positions: any, defaultColor?: string): any;
            createZipCodeFont(height: any): {};
            createFont(fontString: any): {};
            createVerticalAlignment(alignment: string): {};
            createHorizontalAlignment(alignment: string): {};
            createStrokeDashArray(style: any, width: any): string;
            createWordWrap(wordwrap: boolean, multiline: boolean): {};
            createAngle(angle: any): {
                '-webkit-transform': string;
                '-moz-transform': string;
                '-o-transform': string;
                '-ms-transform': string;
                'transform': string;
            };
            constructor(control: IStyleContainer, _rtlLayout: ko.Observable<boolean> | ko.Computed<boolean>);
            borderCss: any;
            fontCss: any;
            zipCodeFontCss: any;
            textAlignmentCss: any;
            foreColorCss: any;
            paddingsCss: any;
            backGroundCss: any;
            stroke: any;
            strokeWidth: any;
            strokeWidthWithWidth: any;
            strokeDashArray: any;
            strokeDashArrayWithWidth: any;
            crossBandBorder: any;
            angle: any;
            wordWrapCss: any;
            cellBorder: any;
            zipCodeAlignment: any;
            contentSizeCss(controlSurfaceWidth: number, controlSurfaceHeight: number, zoom?: number, borders?: string, paddings?: Elements.PaddingModel): {
                top: number;
                left: number;
                right: number;
                bottom: number;
                width: number;
                height: number;
            };
        }
        var editorTypeMapper: {
            "Enum": any;
            "System.String": any;
            "System.Guid": {
                header: string;
                editorType: typeof Widgets.GuidEditor;
            };
            "System.SByte": {
                header: string;
                editorType: any;
            };
            "System.Decimal": {
                header: string;
                editorType: any;
            };
            "System.Int64": {
                header: string;
                editorType: any;
            };
            "System.Int32": {
                header: string;
                editorType: any;
            };
            "System.Int16": {
                header: string;
                editorType: any;
            };
            "System.Single": {
                header: string;
                editorType: any;
            };
            "System.Double": {
                header: string;
                editorType: any;
            };
            "System.Byte": {
                header: string;
                editorType: any;
            };
            "System.UInt16": {
                header: string;
                editorType: any;
            };
            "System.UInt32": {
                header: string;
                editorType: any;
            };
            "System.UInt64": {
                header: string;
                editorType: any;
            };
            "System.Boolean": any;
            "System.DateTime": any;
            "DevExpress.DataAccess.Expression": {
                header: string;
            };
        };
        function getEditorType(typeString: string): {
            header?: any;
            content?: any;
            custom?: any;
        };
        function getTypeNameFromFullName(controlType: string): string;
        function getShortTypeName(controlType: string): string;
        function getControlFullName(value: any): string;
        function getImageClassName(controlType: string, isTemplate?: boolean): string;
        function getUniqueNameForNamedObjectsArray(objects: any[], prefix: string, names?: string[]): string;
        function getUniqueName(names: string[], prefix: string): string;
        interface ILocalizationSettings extends IGlobalizeSettings {
            localization?: {
                [stringId: string]: string;
            };
        }
        interface IGlobalizeSettings {
            currentCulture?: string;
            cldrData?: string;
            cldrSupplemental?: string;
        }
        function initGlobalize(settings: IGlobalizeSettings): void;
        interface IHoverInfo {
            isOver: boolean;
            x: number;
            y: number;
            offsetX?: number;
            offsetY?: number;
            isNotDropTarget?: boolean;
        }
        class HoverInfo implements IHoverInfo {
            private _x;
            private _y;
            isOver: boolean;
            x: number;
            y: number;
        }
        function processTextEditorHotKeys(event: JQueryKeyEventObject, delegates: any): void;
        class InlineTextEdit extends Utils.Disposable {
            private _showInline;
            text: ko.Observable<string> | ko.Computed<string>;
            visible: ko.Observable<boolean> | ko.Computed<boolean>;
            keypressAction: any;
            show: any;
            constructor(selection: ISelectionProvider);
        }
        class ObjectStructureTreeListController implements Widgets.Internal.ITreeListController {
            dispose(): void;
            constructor(propertyNames?: string[], listPropertyNames?: string[]);
            canSelect(value: Widgets.Internal.TreeListItemViewModel): boolean;
            dragDropHandler: DragDropHandler;
            selectedItem: any;
            dblClickHandler: (item: Widgets.Internal.TreeListItemViewModel) => void;
            select: (value: Widgets.Internal.TreeListItemViewModel) => void;
            itemsFilter: (item: Utils.IDataMemberInfo) => boolean;
            hasItems: (item: Utils.IDataMemberInfo) => boolean;
            getActions: (item: Widgets.Internal.TreeListItemViewModel) => Utils.IAction[];
            showIconsForChildItems: (item?: Widgets.Internal.TreeListItemViewModel) => boolean;
        }
        interface IRootItem {
            model: any;
            displayName?: string;
            name: string;
            className: string;
            data?: any;
        }
        class ObjectStructureProviderBase extends Utils.Disposable implements Utils.IItemsProvider {
            getClassName(instance: any): any;
            createItem(currentTarget: any, propertyName: string, propertyValue: any, result: Utils.IDataMemberInfo[]): void;
            getMemberByPath(target: any, path: string): any;
            getObjectPropertiesForPath(target: any, path: string, propertyName?: string): Utils.IDataMemberInfo[];
            createArrayItem(currentTarget: Array<any>, result: Utils.IDataMemberInfo[], propertyName?: any): void;
            getItems: (pathRequest: Utils.IPathRequest) => JQueryPromise<Utils.IDataMemberInfo[]>;
            selectedPath: ko.Observable<string> | ko.Computed<string>;
            selectedMember: ko.Observable | ko.Computed;
        }
        class ObjectExplorerProvider extends ObjectStructureProviderBase {
            getPathByMember: (model: any) => string;
            createArrayItem(currentTarget: Array<any>, result: Utils.IDataMemberInfo[], propertyName?: any): void;
            createItem(currentTarget: any, propertyName: string, propertyValue: any, result: Utils.IDataMemberInfo[]): void;
            constructor(rootITems: IRootItem[], listPropertyNames?: string[], member?: ko.Observable | ko.Computed, getPathByMember?: any);
            path: ko.Observable<string> | ko.Computed<string>;
            listPropertyNames: string[];
        }
        class ObjectStructureProvider extends ObjectStructureProviderBase {
            constructor(target: any, displayName?: string, localizationId?: string);
        }
        var papperKindMapper: {
            A2: {
                width: number;
                height: number;
            };
            A3: {
                width: number;
                height: number;
            };
            A3Extra: {
                width: number;
                height: number;
            };
            A3ExtraTransverse: {
                width: number;
                height: number;
            };
            A3Rotated: {
                width: number;
                height: number;
            };
            A3Transverse: {
                width: number;
                height: number;
            };
            A4: {
                width: number;
                height: number;
            };
            A4Extra: {
                width: number;
                height: number;
            };
            A4Plus: {
                width: number;
                height: number;
            };
            A4Rotated: {
                width: number;
                height: number;
            };
            A4Small: {
                width: number;
                height: number;
            };
            A4Transverse: {
                width: number;
                height: number;
            };
            A5: {
                width: number;
                height: number;
            };
            A5Extra: {
                width: number;
                height: number;
            };
            A5Rotated: {
                width: number;
                height: number;
            };
            A5Transverse: {
                width: number;
                height: number;
            };
            A6: {
                width: number;
                height: number;
            };
            A6Rotated: {
                width: number;
                height: number;
            };
            APlus: {
                width: number;
                height: number;
            };
            B4: {
                width: number;
                height: number;
            };
            B4Envelope: {
                width: number;
                height: number;
            };
            B4JisRotated: {
                width: number;
                height: number;
            };
            B5: {
                width: number;
                height: number;
            };
            B5Envelope: {
                width: number;
                height: number;
            };
            B5Extra: {
                width: number;
                height: number;
            };
            B5JisRotated: {
                width: number;
                height: number;
            };
            B5Transverse: {
                width: number;
                height: number;
            };
            B6Envelope: {
                width: number;
                height: number;
            };
            B6Jis: {
                width: number;
                height: number;
            };
            B6JisRotated: {
                width: number;
                height: number;
            };
            BPlus: {
                width: number;
                height: number;
            };
            C3Envelope: {
                width: number;
                height: number;
            };
            C4Envelope: {
                width: number;
                height: number;
            };
            C5Envelope: {
                width: number;
                height: number;
            };
            C65Envelope: {
                width: number;
                height: number;
            };
            C6Envelope: {
                width: number;
                height: number;
            };
            CSheet: {
                width: number;
                height: number;
            };
            DLEnvelope: {
                width: number;
                height: number;
            };
            DSheet: {
                width: number;
                height: number;
            };
            ESheet: {
                width: number;
                height: number;
            };
            Executive: {
                width: number;
                height: number;
            };
            Folio: {
                width: number;
                height: number;
            };
            GermanLegalFanfold: {
                width: number;
                height: number;
            };
            GermanStandardFanfold: {
                width: number;
                height: number;
            };
            InviteEnvelope: {
                width: number;
                height: number;
            };
            IsoB4: {
                width: number;
                height: number;
            };
            ItalyEnvelope: {
                width: number;
                height: number;
            };
            JapaneseDoublePostcard: {
                width: number;
                height: number;
            };
            JapaneseDoublePostcardRotated: {
                width: number;
                height: number;
            };
            JapanesePostcard: {
                width: number;
                height: number;
            };
            Ledger: {
                width: number;
                height: number;
            };
            Legal: {
                width: number;
                height: number;
            };
            LegalExtra: {
                width: number;
                height: number;
            };
            Letter: {
                width: number;
                height: number;
            };
            LetterExtra: {
                width: number;
                height: number;
            };
            LetterExtraTransverse: {
                width: number;
                height: number;
            };
            LetterPlus: {
                width: number;
                height: number;
            };
            LetterRotated: {
                width: number;
                height: number;
            };
            LetterSmall: {
                width: number;
                height: number;
            };
            LetterTransverse: {
                width: number;
                height: number;
            };
            MonarchEnvelope: {
                width: number;
                height: number;
            };
            Note: {
                width: number;
                height: number;
            };
            Number10Envelope: {
                width: number;
                height: number;
            };
            Number11Envelope: {
                width: number;
                height: number;
            };
            Number12Envelope: {
                width: number;
                height: number;
            };
            Number14Envelope: {
                width: number;
                height: number;
            };
            Number9Envelope: {
                width: number;
                height: number;
            };
            PersonalEnvelope: {
                width: number;
                height: number;
            };
            Prc16K: {
                width: number;
                height: number;
            };
            Prc16KRotated: {
                width: number;
                height: number;
            };
            Prc32K: {
                width: number;
                height: number;
            };
            Prc32KBig: {
                width: number;
                height: number;
            };
            Prc32KBigRotated: {
                width: number;
                height: number;
            };
            Prc32KRotated: {
                width: number;
                height: number;
            };
            PrcEnvelopeNumber1: {
                width: number;
                height: number;
            };
            PrcEnvelopeNumber10: {
                width: number;
                height: number;
            };
            PrcEnvelopeNumber10Rotated: {
                width: number;
                height: number;
            };
            PrcEnvelopeNumber1Rotated: {
                width: number;
                height: number;
            };
            PrcEnvelopeNumber2: {
                width: number;
                height: number;
            };
            PrcEnvelopeNumber2Rotated: {
                width: number;
                height: number;
            };
            PrcEnvelopeNumber3: {
                width: number;
                height: number;
            };
            PrcEnvelopeNumber3Rotated: {
                width: number;
                height: number;
            };
            PrcEnvelopeNumber4: {
                width: number;
                height: number;
            };
            PrcEnvelopeNumber4Rotated: {
                width: number;
                height: number;
            };
            PrcEnvelopeNumber5: {
                width: number;
                height: number;
            };
            PrcEnvelopeNumber5Rotated: {
                width: number;
                height: number;
            };
            PrcEnvelopeNumber6: {
                width: number;
                height: number;
            };
            PrcEnvelopeNumber6Rotated: {
                width: number;
                height: number;
            };
            PrcEnvelopeNumber7: {
                width: number;
                height: number;
            };
            PrcEnvelopeNumber7Rotated: {
                width: number;
                height: number;
            };
            PrcEnvelopeNumber8: {
                width: number;
                height: number;
            };
            PrcEnvelopeNumber8Rotated: {
                width: number;
                height: number;
            };
            PrcEnvelopeNumber9: {
                width: number;
                height: number;
            };
            PrcEnvelopeNumber9Rotated: {
                width: number;
                height: number;
            };
            Quarto: {
                width: number;
                height: number;
            };
            Standard10x11: {
                width: number;
                height: number;
            };
            Standard10x14: {
                width: number;
                height: number;
            };
            Standard11x17: {
                width: number;
                height: number;
            };
            Standard12x11: {
                width: number;
                height: number;
            };
            Standard15x11: {
                width: number;
                height: number;
            };
            Standard9x11: {
                width: number;
                height: number;
            };
            Statement: {
                width: number;
                height: number;
            };
            Tabloid: {
                width: number;
                height: number;
            };
            TabloidExtra: {
                width: number;
                height: number;
            };
            USStandardFanfold: {
                width: number;
                height: number;
            };
        };
        var _addErrorPrefix: boolean;
        function _processError(errorThrown: string, deferred: JQueryDeferred<any>, jqXHR: any, textStatus: any, processErrorCallback?: (message: string, jqXHR: any, textStatus: any) => void): void;
        var _errorProcessor: {
            handlers: any[];
            call: (e: any) => void;
        };
        function processErrorEvent(func: any): Utils.IDisposable;
        interface IResizableOptions extends JQueryUI.ResizableOptions {
            starting?: () => void;
            $element?: Element;
            stopped?: () => void;
            zoom?: number;
            minimumWidth?: ko.Observable<number> | number;
            maximumWidth?: ko.Observable<number> | number;
        }
        class CustomSortedArrayStore extends CustomStore {
            static _sortItems(items: any[], sortPropertyName: string): any[];
            static _createOptions(items: any, sortPropertyName: any): {
                load: (options: any) => JQueryPromise<{}>;
                byKey: (key: any) => any;
            };
            constructor(items: any[], sortPropertyName?: string);
        }
        class SortedArrayStore extends ArrayStore {
            constructor(options: any, sortPropertyName?: string);
        }
        class ControlsStore extends Utils.Disposable {
            private _filter;
            dataSource: ko.Computed<DataSource>;
            constructor(allControls: ko.ObservableArray<any>);
            setFilter(filter: any): void;
            resetFilter(): void;
            visible: ko.Computed<boolean>;
        }
        function findSurface(viewModel: Elements.IElementViewModel): ISelectionTarget;
        function getControlNewAbsolutePositionOnResize(snapHelper: SnapLinesHelper, absolutePosition: {
            top: number;
            left: number;
        }, ui: {
            originalSize: {
                width: number;
                height: number;
            };
            size: {
                width: number;
                height: number;
            };
        }, delta: {
            x: number;
            y: number;
            width: number;
            height: number;
        }): {
            top: number;
            left: number;
            bottom: number;
            right: number;
        };
        function getControlRect(element: JQuery, control: ISelectionTarget, surface: Elements.ISurfaceContext): {
            top: number;
            left: number;
            width: number;
            height: number;
        };
        function minHeightWithoutScroll(element: HTMLElement): number;
        function chooseBetterPositionOf(html: any, designer: any): any;
        function updateSurfaceContentSize(surfaceSize: ko.Observable<number> | ko.Computed<number>, root: Element, rtl?: boolean): () => void;
        function validateName(nameCandidate: any): boolean;
        function replaceInvalidSymbols(text: string): string;
        var nameValidationRules: {
            type: string;
            validationCallback: (options: any) => boolean;
            message: any;
        }[];
        interface ICombinedProperty {
            result: any;
            subscriptions: ko.Subscription[];
        }
        class CombinedObject {
            private static getInfo;
            private static isPropertyDisabled;
            private static isPropertyVisible;
            private static mergeProperty;
            static _createProperty(result: any, propertyName: any, propertyValue: any): void;
            static _merge(controls: any[], undoEngine?: ko.Observable<Utils.UndoEngine> | ko.Computed<Utils.UndoEngine>, customMerge?: any, ignoreProperties?: any): {
                result: {};
                subscriptions: any[];
            };
            static mergeControls(controls: any[], undoEngine?: ko.Observable<Utils.UndoEngine> | ko.Computed<Utils.UndoEngine>, customMerge?: any, ignoreProperties?: string[]): {
                result: any;
                subscriptions: any[];
            };
            static getEditableObject(selectionProvider: ISelectionProvider, undoEngine?: ko.Observable<Utils.UndoEngine> | ko.Computed<Utils.UndoEngine>, customMerge?: any): ko.Observable | ko.Computed;
        }
        interface ISelectingEvent {
            control: ISelectionTarget;
            cancel: boolean;
            ctrlKey?: boolean;
        }
        interface ISelectionTarget {
            rect: ko.Observable<Elements.IArea> | ko.Computed<Elements.IArea>;
            focused: ko.Observable<boolean> | ko.Computed<boolean>;
            selected: ko.Observable<boolean> | ko.Computed<boolean>;
            underCursor: ko.Observable<IHoverInfo> | ko.Computed<IHoverInfo>;
            allowMultiselect: boolean;
            locked: boolean;
            canDrop: () => boolean;
            getControlModel: () => Elements.ElementViewModel;
            checkParent: (surfaceParent: ISelectionTarget) => boolean;
            parent: ISelectionTarget;
            getChildrenCollection: () => ko.ObservableArray<ISelectionTarget>;
        }
        interface ISelectionProvider extends Utils.IDisposable {
            focused: ko.Observable<ISelectionTarget> | ko.Computed<ISelectionTarget>;
            selectedItems: ISelectionTarget[];
            initialize(surface?: ISelectionTarget): any;
            selecting(event: ISelectingEvent): any;
            unselecting(surface: ISelectionTarget): any;
            swapFocusedItem(surface: ISelectionTarget): any;
            ignoreMultiSelectProperties?: string[];
        }
        class SurfaceSelection extends Utils.Disposable implements ISelectionProvider {
            ignoreMultiSelectProperties: string[];
            dispose(): void;
            private _focused;
            private _firstSelected;
            private _selectedControls;
            private _selectedControlsInner;
            private _removeFromSelection;
            private _setFocused;
            private _resetTabPanelFocus;
            constructor(ignoreMultiSelectProperties?: string[]);
            focused: ko.PureComputed<ISelectionTarget>;
            readonly selectedItems: ISelectionTarget[];
            clear(): void;
            reset(): void;
            applySelection(): void;
            selectItems(items: any): void;
            updateSelection(control: ISelectionTarget): void;
            swapFocusedItem(control: ISelectionTarget): void;
            initialize(control?: ISelectionTarget): void;
            clickHandler(control?: ISelectionTarget, event?: {
                ctrlKey: boolean;
            }): void;
            selecting(event: ISelectingEvent): void;
            unselecting(control: ISelectionTarget): void;
            selectionWithCtrl(control: ISelectionTarget): void;
            dropTarget: ISelectionTarget;
            expectClick: boolean;
            disabled: ko.Observable<boolean>;
        }
        function deleteSelection(selection: ISelectionProvider): void;
        function findNextSelection(removedElement: ISelectionTarget): ISelectionTarget;
        class SnapLinesCollector {
            private _verticalSnapLines;
            private _horizontalSnapLines;
            private _snapTargetToIgnore;
            private _appendSnapLine;
            private _collectSnaplines;
            _getCollection(parent: any): {
                rect: ko.Observable<Elements.IArea>;
            }[];
            _enumerateCollection(parent: any, parentAbsoluteProsition: {
                top: number;
                left: number;
            }, callback: (item: any, itemAbsoluteRect: {
                left: number;
                right: number;
                top: number;
                bottom: number;
            }) => void): void;
            collectSnaplines(root: any, snapTargetToIgnore: any): {
                vertical: ISnapLine[];
                horizontal: ISnapLine[];
            };
        }
        class SnapLinesHelper {
            static snapTolerance: number;
            private _snapTolerance;
            private _surfaceContext;
            private _snapLinesCollector;
            private _findClosestSnapLine;
            _getActiveSnapLines(position1: number, position2: number, snapLines: ISnapLine[]): {
                lines: any[];
                distance: number;
            };
            constructor(surface?: ko.Observable<Elements.ISurfaceContext> | ko.Computed<Elements.ISurfaceContext>, snapTolerance?: number, snapLinesCollector?: SnapLinesCollector);
            updateSnapLines(snapTargetToIgnore?: any): void;
            deactivateSnapLines(): void;
            activateSnapLines(position: {
                left: number;
                top: number;
                right: number;
                bottom: number;
            }): {
                left: number;
                top: number;
            };
            snapPosition(position: number, horizontal: boolean): number;
            snapLineSurfaces: SnapLineSurface[];
            verticalSnapLines: ISnapLine[];
            horizontalSnapLines: ISnapLine[];
        }
        interface ISnapLine {
            position: number;
            limitInf: number;
            limSup: number;
        }
        class SnapLineSurface {
            private static _blankPosition;
            private _position;
            transform(): string;
            updatePosition(position: {
                top: number;
                left: number;
                width: number;
                height: number;
            }): void;
            reset(): void;
        }
    }
    module Tools {
        var ActionId: {
            Cut: string;
            Copy: string;
            Paste: string;
            Delete: string;
            Undo: string;
            Redo: string;
            ZoomOut: string;
            ZoomSelector: string;
            ZoomIn: string;
        };
    }
    module Internal {
        class ActionListsBase extends Utils.Disposable implements IShortcutActionList {
            constructor(enabled?: ko.Observable<boolean> | ko.Computed<boolean>);
            processShortcut(actions: Utils.IAction[], e: JQueryKeyEventObject): void;
            shouldIgnoreProcessing(e: JQueryKeyEventObject): boolean;
            enabled: ko.Observable<boolean> | ko.Computed<boolean>;
            toolbarItems: Utils.IAction[] | ko.Observable<Utils.IAction[]> | ko.Computed<Utils.IAction[]>;
        }
        class ActionLists extends ActionListsBase {
            _registerAction(container: Array<Utils.IAction>, action: Utils.IAction): void;
            private _keyboardHelper;
            constructor(surfaceContext: ko.Observable<Elements.ISurfaceContext> | ko.Computed<Elements.ISurfaceContext>, selection: ISelectionProvider, undoEngine: ko.Observable<Utils.UndoEngine> | ko.Computed<Utils.UndoEngine>, customizeActions?: (actions: Utils.IAction[]) => void, enabled?: ko.Observable<boolean> | ko.Computed<boolean>, copyPasteStrategy?: ICopyPasteStrategy, zoomStep?: ko.Observable<number> | ko.Computed<number>);
            processShortcut(actions: Utils.IAction[], e: JQueryKeyEventObject): void;
            menuItems: Utils.IAction[];
        }
        interface ICopyPasteStrategy {
            createChild(pasteTarget: Elements.ElementViewModel, info: {}): Elements.ElementViewModel;
            calculateDelta(selection: ISelectionTarget, pasteTargetSurface: ISelectionTarget, minPoint: Elements.IPoint): {
                x: number;
                y: number;
            };
        }
        var copyPasteStrategy: ICopyPasteStrategy;
        class CopyPasteHandler {
            private _copyPasteStrategy;
            private _selectionProvider;
            private _copyInfo;
            constructor(selectionProvider: ISelectionProvider, _copyPasteStrategy?: ICopyPasteStrategy);
            hasPasteInfo: ko.PureComputed<boolean>;
            canCopy(): boolean;
            canPaste(): boolean;
            copy(): void;
            cut(): void;
            paste(): void;
        }
    }
    module Utils {
        interface ITabPanelOptions {
            tabs: TabInfo[];
            autoSelectTab?: boolean;
            rtl?: boolean;
        }
        class TabPanel extends Utils.Disposable {
            static Position: {
                Left: string;
                Right: string;
            };
            dispose(): void;
            constructor(options: ITabPanelOptions);
            private _resizableOptions;
            getResizableOptions: ($element: Element, panelOffset: string, minWidth: number) => any;
            tabs: TabInfo[];
            selectTab: (e: any) => void;
            isEmpty: ko.Observable<boolean> | ko.Computed<boolean>;
            collapsed: ko.Observable<boolean> | ko.Computed<boolean>;
            width: ko.Observable<number> | ko.Computed<number>;
            headerWidth: ko.Observable<number> | ko.Computed<number>;
            position: ko.Observable<string> | ko.Computed<string>;
            toggleCollapsedImage: ko.Computed<{
                class: string;
                template: string;
            }>;
            toggleCollapsedText: ko.PureComputed<any>;
            cssClasses: (extendOptions: {
                class: string;
                condition: any;
            }) => any;
        }
        interface ITabInfoOptions {
            text: string;
            template: string;
            model: any;
            localizationId?: string;
            imageClassName?: string;
            imageTemplateName?: string;
            visible?: ko.Observable<boolean> | ko.Computed<boolean>;
            disabled?: ko.Observable<boolean> | ko.Computed<boolean>;
        }
        class TabInfo extends Utils.Disposable {
            private _text;
            private _localizationId;
            constructor(options: ITabInfoOptions);
            imageClassName: ko.Observable<string> | ko.Computed<string>;
            imageTemplateName: string;
            active: ko.Observable<boolean> | ko.Computed<boolean>;
            visible: ko.Observable<boolean> | ko.Computed<boolean>;
            disabled: ko.Observable<boolean> | ko.Computed<boolean>;
            template: string;
            model: any;
            readonly text: any;
        }
        interface IToolboxItemInfo {
            "@ControlType": string;
            index: number;
            canDrop?: any;
            displayName?: string;
        }
        class ToolboxItem {
            constructor(info: Utils.IToolboxItemInfo);
            disabled: ko.Observable<boolean>;
            info: Utils.IToolboxItemInfo;
            readonly type: string;
            readonly imageClassName: string;
            readonly imageTemplateName: any;
            readonly index: number;
            readonly displayName: string;
        }
    }
    module Internal {
        function createObservableReverseArrayMapCollection<T>(elementModels: any, target: ko.ObservableArray<T>, createItem: (item: any) => T): any;
        function createObservableArrayMapCollection<T>(elementModels: any, target: ko.ObservableArray<T>, createItem: (item: any) => T): any;
        function deserializeChildArray<T>(model: any, parent: any, creator: any): ko.ObservableArray<T>;
        function getFirstItemByPropertyValue<T>(array: T[], propertyName: string, propertyValue: any, fromIndex?: number): T;
        function findFirstItemMatchesCondition<T>(array: T[], predicate: (item: T) => boolean): T;
        var find: typeof findFirstItemMatchesCondition;
        function binaryIndexOf<T>(ar: T[], el: T, compare: (a: T, b: T) => number): number;
        interface IDataSourceInfo {
            name: string;
            specifics?: string;
            id?: string;
            ref?: string;
            data: any;
            dataSerializer?: string;
            isSqlDataSource?: boolean;
            isJsonDataSource?: boolean;
        }
        interface IItemsExtender {
            beforeItemsFilled: (request: Utils.IPathRequest, items: Utils.IDataMemberInfo[]) => boolean;
            afterItemsFilled?: (request: Utils.IPathRequest, items: Utils.IDataMemberInfo[]) => void;
        }
        class FieldListProvider implements Utils.IItemsProvider {
            private _extenders;
            private _patchRequest;
            private _beforeFieldListCallback;
            private _afterFieldListCallBack;
            constructor(fieldListCallback: (pathRequest: Utils.IPathRequest) => JQueryPromise<Utils.IDataMemberInfo[]>, rootItems: ko.ObservableArray<IDataSourceInfo>, extenders?: IItemsExtender[]);
            getItems: (IPathRequest: any) => JQueryPromise<Utils.IDataMemberInfo[]>;
        }
        var NotifyType: {
            info: string;
            warning: string;
            error: string;
            success: string;
        };
        function NotifyAboutWarning(msg: any, showForUser?: boolean): void;
        function getErrorMessage(jqXHR: any): any;
        function ShowMessage(msg: string, type?: string, displayTime?: number, debugInfo?: string): void;
        function unitsToPixel(val: number, measureUnit: string, zoom?: number): number;
        function pixelToUnits(val: number, measureUnit: string, zoom: number): number;
        interface IUnitProperties<M> {
            [key: string]: (o: M) => ko.Observable<number> | ko.Computed<number>;
        }
        function createUnitProperty(model: any, target: any, propertyName: any, property: any, measureUnit: ko.Observable<string> | ko.Computed<string>, zoom: ko.Observable<number> | ko.Computed<number>, afterCreation?: (property: any) => void): void;
        function createUnitProperties<M>(model: M, target: any, properties: IUnitProperties<M>, measureUnit: ko.Observable<string> | ko.Computed<string>, zoom: ko.Observable<number> | ko.Computed<number>, afterCreation?: (property: any) => void): void;
        type SizeFactorType = "lg" | "md" | "sm" | "xs";
        function copyObservables(from: any, to: any): void;
        function compareObjects(a: any, b: any): boolean;
        var cssTransform: string[];
        var DEBUG: boolean;
        function getFullPath(path: string, dataMember: string): string;
        function loadTemplates(): any;
        function getSizeFactor(width: any): SizeFactorType;
        function appendStaticContextToRootViewModel(root: any, dx?: any): void;
        interface IAjaxSettings {
            uri: string;
            action: string;
            arg: any;
            processErrorCallback?: (message: string, jqXHR: any, textStatus: any) => void;
            ignoreError?: () => boolean;
            customOptions?: any;
            isError?: (data: any) => boolean;
            getErrorMessage?: (jqXHR: any) => string;
        }
        function _ajax(uri: any, action: any, arg: any, processErrorCallback?: (message: string, jqXHR: any, textStatus: any) => void, ignoreError?: () => boolean, customOptions?: any, isError?: (data: any) => boolean, getErrorMessage?: (jqXHR: any) => string): JQueryPromise<any>;
        function _ajaxWithOptions(options: IAjaxSettings): JQueryPromise<any>;
        function ajax(...params: (IAjaxSettings | any)[]): any;
        interface ICommonCustomizationHandler {
            customizeActions?: (actions: Utils.IAction[]) => void;
            customizeLocalization?: (callbacks?: JQueryPromise<any>[]) => void;
            onServerError?: (e: any) => void;
            beforeRender?: (designerModel: any) => void;
        }
        interface INamedValue {
            displayName: string;
            value: any;
        }
        function cutRefs(model: any): any;
        interface IDesignerPart {
            id: string;
            templateName: string;
            model: any;
        }
        var DesignerBaseElements: {
            MenuButton: string;
            Toolbar: string;
            Toolbox: string;
            Surface: string;
            RightPanel: string;
        };
        function generateDefaultParts(model: any): IDesignerPart[];
        function createActionWrappingFunction(wrapperName: string, func: (model: any, originalHandler: (model?: any) => any) => any): (actions: Utils.IAction[]) => void;
        function createDesigner(model: ko.Observable | ko.Computed, surface: ko.Observable<Elements.ISurfaceContext> | ko.Computed<Elements.ISurfaceContext>, controlsFactory: Utils.ControlsFactory, groups?: GroupObject, editors?: Utils.ISerializationInfoArray, parts?: IDesignerPart[], rtl?: boolean, selection?: Internal.SurfaceSelection, designControlsHelper?: Internal.DesignControlsHelper, undoEngine?: ko.Observable<Utils.UndoEngine> | ko.Computed<Utils.UndoEngine>, customMerge?: any, snapLinesCollector?: Internal.SnapLinesCollector, groupLocalizationIDs?: {
            [key: string]: string;
        }): IDesignerModel;
        function localizeNoneString(noneValue: any): any;
        function parseZoom(val: string): number;
        function objectsVisitor(target: any, visitor: (target: any) => any, visited?: any[], skip?: Array<string>): void;
        function collectionsVisitor(target: any, visitor: (target: any, owner?: any) => any, collectionsToProcess?: string[], visited?: any[]): void;
    }
    module Widgets {
        module Internal {
            class dxFieldListPicker extends dxDropDownBox {
                _path: ko.Observable<string>;
                _value: ko.Observable<string>;
                _parentViewport: JQuery;
                _itemsProvider: ko.Observable<any>;
                _defaultPosition: any;
                constructor($element: JQuery, options?: any);
                _showDropDown(): void;
                _getMaxHeight(): number;
                _closeOutsideDropDownHandler(e: any, ignoreContainerClicks: any): void;
                _hideOnBlur(): boolean;
                _popupConfig(): any;
                _setTitle(text: string): void;
                _optionChanged(obj: any, value: any): void;
                _clearValueHandler(): void;
                _renderPopupContent(): void;
            }
        }
    }
    module Internal {
        class BordersModel extends Utils.Disposable {
            private _setAllValues;
            setValue(name: any): void;
            setAll(): void;
            setNone(): void;
            updateModel(value: string): void;
            updateValue(): void;
            constructor(object: {
                value: ko.Observable<string>;
            }, disabled?: ko.Observable<boolean>);
            value: ko.Observable<string> | ko.Computed<string>;
            left: ko.Observable<boolean>;
            right: ko.Observable<boolean>;
            top: ko.Observable<boolean>;
            bottom: ko.Observable<boolean>;
            disabled: ko.Observable<boolean> | ko.Computed<boolean>;
        }
        class ControlProperties extends Widgets.ObjectProperties {
            getEditors(): Widgets.Editor[];
            protected _update(target: any, editorsInfo: any, recreateEditors: any): void;
            cleanEditors(): void;
            dispose(): void;
            createGroups(groups: GroupObject): void;
            constructor(target: ko.Observable<any>, editorsInfo?: {
                groups?: GroupObject;
                editors?: Utils.ISerializationInfoArray;
            }, level?: number, addAddons?: boolean);
            focusedItem: ko.Observable | ko.Computed;
            focusedImageClassName: ko.Observable<string> | ko.Computed<string>;
            displayExpr: (value: any) => string;
            popupService: Analytics.Internal.PopupService;
            groups: Group[];
            createEditorAddOn: (editor: Widgets.Editor) => Analytics.Internal.IEditorAddon;
            editorsRendered: ko.Observable<boolean> | ko.Computed<boolean>;
            isSortingByGroups: ko.Observable<boolean> | ko.Computed<boolean>;
            isSearching: ko.Observable<boolean> | ko.Computed<boolean>;
            allEditorsCreated: ko.Observable<boolean> | ko.Computed<boolean>;
            textToSearch: ko.Observable<string>;
            _searchBox: any;
            searchBox($element: JQuery): void;
            searchPlaceholder: () => any;
            switchSearchBox: () => void;
        }
        type GroupObject = {
            [key: string]: {
                info: Utils.ISerializationInfoArray;
                displayName?: () => string;
            };
        };
        class Group extends Utils.Disposable {
            private _viewModel;
            private _serializationsInfo;
            private _displayName;
            private _localizationId;
            constructor(name: string, serializationsInfo: Utils.ISerializationInfoArray, createEditors: (serializationInfo: Utils.ISerializationInfoArray) => Widgets.Editor[], collapsed?: boolean, displayName?: () => string);
            resetEditors(): void;
            dispose(): void;
            update(viewModel: Elements.ElementViewModel): void;
            displayName: () => string;
            editors: ko.ObservableArray<Widgets.Editor>;
            context: any;
            recreate: () => void;
            collapsed: ko.Observable<boolean> | ko.Computed<boolean>;
            visible: ko.Computed<Boolean>;
            editorsCreated: ko.Observable<boolean>;
            editorsRendered: ko.Observable<boolean>;
        }
        var sizeFake: Utils.ISerializationInfoArray;
        var locationFake: Utils.ISerializationInfoArray;
        interface IDesignerContext {
            model: ko.Observable | ko.Computed;
            surface?: ko.Observable | ko.Computed;
            undoEngine?: ko.Observable<Utils.UndoEngine> | ko.Computed<Utils.UndoEngine>;
        }
        interface IDesignerModel extends Utils.IDisposable {
            model: ko.Observable | ko.Computed;
            rtl: boolean;
            surface: ko.Observable | ko.Computed;
            undoEngine: ko.Observable<Utils.UndoEngine> | ko.Computed<Utils.UndoEngine>;
            selection: Internal.SurfaceSelection;
            snapHelper: Internal.SnapLinesHelper;
            editableObject: ko.Observable<any>;
            dragHelperContent: Internal.DragHelperContent;
            dragDropStarted: ko.Observable<boolean> | ko.Computed<boolean>;
            dragHandler: Internal.SelectionDragDropHandler;
            toolboxDragHandler: Internal.ToolboxDragDropHandler;
            resizeHandler: IResizeHandler;
            toolboxItems: Utils.ToolboxItem[];
            isLoading: ko.Observable<boolean> | ko.Computed<boolean>;
            propertyGrid: ControlProperties;
            popularProperties: Widgets.ObjectProperties;
            controlsHelper: Internal.DesignControlsHelper;
            controlsStore: Internal.ControlsStore;
            tabPanel: Utils.TabPanel;
            contextActionProviders: Internal.IActionsProvider[];
            contextActions: ko.Observable<Utils.IAction[]> | ko.Computed<Utils.IAction[]>;
            appMenuVisible: ko.Observable<boolean> | ko.Computed<boolean>;
            toggleAppMenu: () => void;
            getMenuPopupContainer: (el: HTMLElement) => JQuery;
            getMenuPopupTarget: (el: HTMLElement) => JQuery;
            inlineTextEdit: Internal.InlineTextEdit;
            actionsGroupTitle: () => string;
            updateFont: (values: {
                [key: string]: string;
            }) => void;
            sortFont: () => void;
            surfaceSize: ko.Observable<number> | ko.Computed<number>;
            popularVisible: ko.Computed<boolean>;
            actionLists: Internal.ActionLists;
            parts: IDesignerPart[];
            surfaceClass: (elem: any) => string;
        }
        class DesignerContextGeneratorInternal<T extends IDesignerContext> {
            private _context;
            private _rtl?;
            constructor(_context: T, _rtl?: boolean);
            addElement(propertyName: string, model: any): this;
            addUndoEngine(undoEngine?: ko.Observable<Utils.UndoEngine> | ko.Computed<Utils.UndoEngine>): this;
            addSurface(surface: any): this;
            getContext(): T;
        }
        class DesignerContextGenerator<T extends IDesignerContext> {
            private _rtl?;
            constructor(_rtl?: boolean);
            addModel(model: any): DesignerContextGeneratorInternal<T>;
        }
        interface IDesingerGeneratorSettings {
            generate(): any;
        }
        interface IResizeHandler {
            starting: () => void;
            stopped: () => void;
            disabled?: ko.Observable<boolean> | ko.Computed<boolean>;
            snapHelper?: Internal.SnapLinesHelper;
        }
        class ResizeSettings extends Utils.Disposable implements IDesingerGeneratorSettings {
            private _handler;
            handler: IResizeHandler;
            generate(): {};
        }
        class ContextActionsSettings extends Utils.Disposable implements IDesingerGeneratorSettings {
            private _actionProviders;
            private _actions;
            private _actionUndoEngineWrappingFunction;
            actionProviders: IActionsProvider[];
            actions: ko.Observable<Utils.IAction[]> | ko.Computed<Utils.IAction[]>;
            createDefaultActions(editableObj: any, undoEngine: any): void;
            generate(): {};
        }
        class DragDropSettings extends Utils.Disposable implements IDesingerGeneratorSettings {
            private _model;
            private _dragHelperContent;
            private _dragDropStarted;
            dragHelperContent: DragHelperContent;
            dragDropStarted: boolean | ko.Observable<boolean>;
            addDragDropHandler(propertyName: string, handler: Internal.DragDropHandler): void;
            generate(): {};
        }
        class ControlsHelperSettings extends Utils.Disposable implements IDesingerGeneratorSettings {
            private _selection;
            private _context;
            private _model;
            private controlsHelper;
            constructor(_selection: Internal.SurfaceSelection, _context: IDesignerContext);
            generate(): {};
            addControlsHelper(helper?: Internal.IDesignControlsHelper): this;
            addControlsStore(store?: Internal.ControlsStore): this;
        }
        class MenuSettings extends Utils.Disposable implements IDesingerGeneratorSettings {
            generate(): {};
            private _appMenuVisible;
            toggleAppMenu: () => void;
            stopPropagation: boolean;
            getMenuPopupContainer: (el: HTMLElement) => JQuery;
            getMenuPopupTarget: (el: HTMLElement) => JQuery;
            appMenuVisible: ko.Computed<boolean> | ko.Observable<boolean>;
        }
        class SelectionSettings extends Utils.Disposable implements IDesingerGeneratorSettings {
            private _selection;
            private _snapHelper;
            private _editableObject;
            private _dragDropSettings;
            private _resizeSettings;
            dispose(): void;
            selection: SurfaceSelection;
            snapHelper: SnapLinesHelper;
            editableObject: any;
            addDragDrop(func: (settings: DragDropSettings) => void): void;
            addResize(func: (settings: ResizeSettings) => void): void;
            generate(): any;
        }
        class CommonDesignerGenerator<T extends IDesignerModel> extends Utils.Disposable {
            private _context?;
            private _rtl?;
            private _model;
            private _selectionSettings;
            private _createPopularProperties;
            private _resetModel;
            protected rtl: boolean;
            dispose(): void;
            constructor(_context?: IDesignerContext, _rtl?: boolean);
            initializeContext(context: IDesignerContext): this;
            getPropertyByName<T>(propertyName: string): T;
            addElement(propertyName: string, elementFunc: () => any): this;
            mapOnContext(): this;
            addSelection(func: (settings: SelectionSettings) => void): this;
            addPropertyGrid(propertyGrid?: () => Widgets.ObjectProperties, propertyName?: string): this;
            addControlProperties(editors: any, groups: GroupObject, groupLocalizationIDs: any): this;
            addPopularProperties(controlsFactory: any): this;
            addToolboxItems(items?: () => Utils.ToolboxItem[]): this;
            addTabPanel(panel?: () => Utils.TabPanel, addTabInfo?: () => Utils.TabInfo[]): this;
            addIsLoading(isLoadingFunc?: () => ko.Observable<boolean>): this;
            addControlsHelper(func: (settings: ControlsHelperSettings) => void): this;
            addMenu(func: (settings: MenuSettings) => void): this;
            addContextActions(func: (contextActions: ContextActionsSettings) => void): this;
            addParts(func?: (parts: any) => IDesignerPart[], useDefaults?: boolean): this;
            getModel(): T;
            addActionList(actionListsFunc?: () => Internal.ActionLists): this;
        }
        class dxButtonWithTemplate extends dxButton {
            constructor(element: any, options?: any);
            _getContentData(): any;
            _optionChanged(args: any): void;
        }
    }
}

/**
* DevExpress HTML/JS Reporting (dx-webdocumentviewer.d.ts)
* Version: 19.1.6
* Build date: 2019-09-10
* Copyright (c) 2012 - 2019 Developer Express Inc. ALL RIGHTS RESERVED
* License: https://www.devexpress.com/Support/EULAs/NetComponents.xml
*/

declare module DevExpress.Reporting {
    module Internal {
        var editorTemplates: {
            csvSeparator: {
                header: any;
                extendedOptions: {
                    placeholder: ko.PureComputed<string>;
                };
            };
        };
    }
    module Metadata {
        var previewBackColor: Analytics.Utils.ISerializationInfo;
        var previewSides: Analytics.Utils.ISerializationInfo;
        var previewBorderColor: Analytics.Utils.ISerializationInfo;
        var previewBorderStyle: Analytics.Utils.ISerializationInfo;
        var previewBorderDashStyle: Analytics.Utils.ISerializationInfo;
        var previewBorderWidth: Analytics.Utils.ISerializationInfo;
        var previewForeColor: Analytics.Utils.ISerializationInfo;
        var previewFont: Analytics.Utils.ISerializationInfo;
        var previewPadding: Analytics.Utils.ISerializationInfo;
        var previewTextAlignment: Analytics.Utils.ISerializationInfo;
        var brickStyleSerializationsInfo: Analytics.Utils.ISerializationInfoArray;
    }
    class ImageSource {
        sourceType: string;
        data: string;
        constructor(sourceType: string, data: string);
        getDataUrl(format?: ko.Observable<string>): string;
        static parse(val: string): ImageSource;
        static toString(val: ImageSource): string;
    }
    interface IKeyValuePair<T> {
        Key: string;
        Value: T;
    }
    function convertMapToKeyValuePair(object: any): any[];
    interface IEnumType {
        enumType: string;
        values: Array<IEnumValue>;
    }
    interface IEnumValue {
        displayName: string;
        name: string;
        value: any;
    }
    class CustomizeExportOptionsEventArgs {
        constructor(options: Viewer.Utils.IPreviewExportOptionsCustomizationArgs);
        HideExportOptionsPanel(): void;
        HideFormat(format: any): void;
        HideProperties(format: any, ...paths: (string | string[])[]): void;
        GetExportOptionsModel(format: any): any;
        _options: Viewer.Utils.IPreviewExportOptionsCustomizationArgs;
    }
    class EventGenerator {
        static generateCustomizeLocalizationCallback(fireEvent: (eventName: any, args?: any) => void): (localizationCallbacks: JQueryPromise<any>[]) => void;
        static generateDesignerEvents(fireEvent: (eventName: any, args?: any) => void): {
            customizeActions: (actions: any) => void;
            customizeParameterEditors: (parameter: any, info: any) => void;
            customizeParameterLookUpSource: (parameter: any, items: any) => any;
            exitDesigner: () => void;
            reportSaving: (args: any) => void;
            reportSaved: (args: any) => void;
            reportOpening: (args: any) => void;
            reportOpened: (args: any) => void;
            tabChanged: (tab: any) => void;
            onServerError: (args: any) => void;
            customizeParts: (parts: any) => void;
            componentAdded: (args: any) => void;
            customizeSaveDialog: (popup: any) => void;
            customizeSaveAsDialog: (popup: any) => void;
            customizeOpenDialog: (popup: any) => void;
            customizeToolbox: (controlsFactory: any) => void;
            customizeLocalization: (localizationCallbacks: JQueryPromise<any>[]) => void;
            customizeFieldListActions: (item: any, actions: any) => void;
            beforeRender: (designerModel: any) => void;
            customizeWizard: (type: string, wizard: any) => void;
        };
        static generatePreviewEvents(fireEvent: (eventName: any, args?: any) => void, prefix?: string): {
            previewClick: (pageIndex: any, brick: any, defaultHandler: any) => boolean;
            documentReady: (documentId: any, reportId: any, pageCount: any) => void;
            editingFieldChanged: (field: any, oldValue: any, newValue: any) => any;
            parametersSubmitted: (model: any, parameters: any) => void;
            parametersReset: (model: any, parameters: any) => void;
            customizeParameterLookUpSource: (parameter: any, items: any) => any;
            customizeParameterEditors: (parameter: any, info: any) => void;
            customizeActions: (actions: any) => void;
            customizeParts: (parts: any) => void;
            customizeExportOptions: (options: any) => void;
            onServerError: (args: any) => void;
        };
    }
    module Internal {
        var cultureInfo: {};
        var generateGuid: () => string;
        function createFullscreenComputed(element: Element, parent: Analytics.Utils.IDisposable): ko.Computed<boolean>;
    }
    module Editing {
        interface IInplaceEditorInfo {
            name: string;
            category: string;
            displayName: string;
            template?: string;
            options?: {};
        }
        var Categories: {
            Image: () => string;
            Numeric: () => string;
            DateTime: () => string;
            Letters: () => string;
        };
        interface IImageEditorRegistrationOptions {
            name: string;
            displayName: string;
            images?: Viewer.Widgets.Internal.IImageEditorItem[];
            customizeActions?: (sender: any, actions: any[]) => void;
            searchEnabled?: boolean;
            imageLoadEnabled?: boolean;
            sizeOptionsEnabled?: boolean;
            clearEnabled?: boolean;
            drawingEnabled?: boolean;
        }
        class EditingFieldExtensions {
            private static _instance;
            private _editors;
            static instance(): EditingFieldExtensions;
            private _registerStandartEditors;
            static registerImageEditor(imageRegistrationOptions: IImageEditorRegistrationOptions): void;
            static registerEditor(name: string, displayName: string, category: string, options?: {}, template?: string, validate?: (value: string) => boolean, defaultVal?: string): void;
            static registerMaskEditor(editorID: string, displayName: string, category: string, mask: string): void;
            static registerRegExpEditor(editorID: string, displayName: string, category: string, regExpEditing: RegExp, regExpFinal: RegExp, defaultVal: string): void;
            static unregisterEditor(editorID: string): void;
            categories(excludeCategories?: string[]): string[];
            editors(): IInplaceEditorInfo[];
            editorsByCategories(categories?: string[]): IInplaceEditorInfo[];
            editor(editorID: string): IInplaceEditorInfo;
        }
    }
    module Export {
        module Metadata {
            var pageBorderColor: Analytics.Utils.ISerializationInfo;
            var pageBorderWidth: Analytics.Utils.ISerializationInfo;
            var pageRange: Analytics.Utils.ISerializationInfo;
            var expotOptionsTitle: Analytics.Utils.ISerializationInfo;
            var htmlTableLayout: Analytics.Utils.ISerializationInfo;
            var docxTableLayout: Analytics.Utils.ISerializationInfo;
            var allowURLsWithJSContent: Analytics.Utils.ISerializationInfo;
            var rasterizationResolution: Analytics.Utils.ISerializationInfo;
            var rasterizeImages: Analytics.Utils.ISerializationInfo;
            var useHRefHyperlinks: Analytics.Utils.ISerializationInfo;
            var exportWatermarks: Analytics.Utils.ISerializationInfo;
            var inlineCss: Analytics.Utils.ISerializationInfo;
            var removeSecondarySymbols: Analytics.Utils.ISerializationInfo;
            var characterSet: Analytics.Utils.ISerializationInfo;
            function getExportModeValues(format?: string, preview?: boolean, merged?: boolean): Array<Analytics.Utils.IDisplayedValue>;
            var exportPageBreaks: Analytics.Utils.ISerializationInfo;
            var rtfExportMode: Analytics.Utils.ISerializationInfo;
            var docxExportMode: Analytics.Utils.ISerializationInfo;
            var htmlExportMode: Analytics.Utils.ISerializationInfo;
            var embedImagesInHTML: Analytics.Utils.ISerializationInfo;
            var imageExportMode: Analytics.Utils.ISerializationInfo;
            var xlsExportMode: Analytics.Utils.ISerializationInfo;
            var xlsxExportMode: Analytics.Utils.ISerializationInfo;
            var textExportMode: Analytics.Utils.ISerializationInfo;
            var xlsTextExportMode: Analytics.Utils.ISerializationInfo;
            var csvTextSeparator: Analytics.Utils.ISerializationInfo;
            var useCustomSeparator: Analytics.Utils.ISerializationInfo;
            var textEncodingType: Analytics.Utils.ISerializationInfo;
            var xlsExportHyperlinks: Analytics.Utils.ISerializationInfo;
            var xlsRawDataMode: Analytics.Utils.ISerializationInfo;
            var xlsShowGridLines: Analytics.Utils.ISerializationInfo;
            var xlsExportOptionsSheetName: Analytics.Utils.ISerializationInfo;
        }
        class CsvExportOptions {
            static from(model: any, serializer?: DevExpress.Analytics.Utils.IModelSerializer): CsvExportOptions;
            static toJson(value: any, serializer: any, refs: any): any;
            getInfo(): Analytics.Utils.ISerializationInfoArray;
            constructor(model: any, serializer?: DevExpress.Analytics.Utils.IModelSerializer);
            isPropertyDisabled(name: string): boolean;
            useCustomSeparator: ko.Observable<boolean> | ko.Computed<boolean>;
            separator: ko.Observable<string> | ko.Computed<string>;
            defaultSeparatorValue: string;
        }
        module Metadata {
            var csvExportOptionsSerializationInfo: Analytics.Utils.ISerializationInfoArray;
        }
        class TextExportOptions {
            static from(model: any, serializer?: DevExpress.Analytics.Utils.IModelSerializer): TextExportOptions;
            static toJson(value: any, serializer: any, refs: any): any;
            getInfo(): Analytics.Utils.ISerializationInfoArray;
            constructor(model: any, serializer?: DevExpress.Analytics.Utils.IModelSerializer);
        }
        module Metadata {
            var textExportOptionsSerializationInfo: Analytics.Utils.ISerializationInfoArray;
        }
        class RtfExportOptions {
            static from(model: any, serializer?: DevExpress.Analytics.Utils.IModelSerializer): RtfExportOptions;
            static toJson(value: any, serializer: any, refs: any): any;
            getInfo(): Analytics.Utils.ISerializationInfoArray;
            constructor(model: any, serializer?: DevExpress.Analytics.Utils.IModelSerializer);
            isPropertyDisabled(name: string): boolean;
            rtfExportMode: ko.Observable<string> | ko.Computed<string>;
        }
        module Metadata {
            var rtfExportOptionsSerializationInfoBase: Analytics.Utils.ISerializationInfoArray;
            var emptyFirstPageHeaderFooter: Analytics.Utils.ISerializationInfo;
            var keepRowHeight: Analytics.Utils.ISerializationInfo;
            var rtfExportOptionsSerializationInfo: Analytics.Utils.ISerializationInfoArray;
        }
        class DocxExportDocumentOptions {
            static from(model: any, serializer?: DevExpress.Analytics.Utils.IModelSerializer): DocxExportDocumentOptions;
            static toJson(value: any, serializer: any, refs: any): any;
            getInfo(): Analytics.Utils.ISerializationInfoArray;
            constructor(model: any, serializer?: DevExpress.Analytics.Utils.IModelSerializer);
        }
        class DocxExportOptions {
            static from(model: any, serializer?: DevExpress.Analytics.Utils.IModelSerializer): DocxExportOptions;
            static toJson(value: any, serializer: any, refs: any): any;
            getInfo(): Analytics.Utils.ISerializationInfoArray;
            constructor(model: any, serializer?: DevExpress.Analytics.Utils.IModelSerializer);
            isPropertyDisabled(name: string): boolean;
            docxExportMode: ko.Observable<string> | ko.Computed<string>;
            tableLayout: ko.Observable<boolean> | ko.Computed<boolean>;
        }
        module Metadata {
            var docxExportDocumentOptionsSerializationInfo: Analytics.Utils.ISerializationInfoArray;
            var docxDocumentOptions: Analytics.Utils.ISerializationInfo;
            var docxExportOptionsSerializationInfo: Analytics.Utils.ISerializationInfoArray;
        }
        class HtmlExportOptions {
            static from(model: any, serializer?: DevExpress.Analytics.Utils.IModelSerializer): HtmlExportOptions;
            static toJson(value: any, serializer: any, refs: any): any;
            getInfo(): Analytics.Utils.ISerializationInfoArray;
            constructor(model: any, serializer?: DevExpress.Analytics.Utils.IModelSerializer);
            isPropertyDisabled(name: string): boolean;
            htmlExportMode: ko.Observable<string> | ko.Computed<string>;
        }
        module Metadata {
            var htmlExportOptionsSerializationInfoBase: Analytics.Utils.ISerializationInfoArray;
            var htmlExportOptionsSerializationInfo: Analytics.Utils.ISerializationInfoArray;
        }
        class ImageExportOptions {
            static from(model: any, serializer?: DevExpress.Analytics.Utils.IModelSerializer): ImageExportOptions;
            static toJson(value: any, serializer: any, refs: any): any;
            getInfo(): Analytics.Utils.ISerializationInfoArray;
            constructor(model: any, serializer?: DevExpress.Analytics.Utils.IModelSerializer);
            isPropertyDisabled(name: string): boolean;
            imageExportMode: ko.Observable<string> | ko.Computed<string>;
        }
        module Metadata {
            var imageExportOptionsSerializationInfoBase: Analytics.Utils.ISerializationInfoArray;
            var imageExportOptionsSerializationInfo: Analytics.Utils.ISerializationInfoArray;
        }
        class MhtExportOptions {
            static from(model: any, serializer?: DevExpress.Analytics.Utils.IModelSerializer): MhtExportOptions;
            static toJson(value: any, serializer: any, refs: any): any;
            getInfo(): Analytics.Utils.ISerializationInfoArray;
            constructor(model: any, serializer?: DevExpress.Analytics.Utils.IModelSerializer);
            isPropertyDisabled(name: string): boolean;
            htmlExportMode: ko.Observable<string> | ko.Computed<string>;
        }
        module Metadata {
            var mhtExportOptionsSerializationInfoBase: Analytics.Utils.ISerializationInfoArray;
            var mhtExportOptionsSerializationInfo: Analytics.Utils.ISerializationInfoArray;
        }
        class PdfExportDocumentOptions {
            static from(model: any, serializer?: DevExpress.Analytics.Utils.IModelSerializer): PdfExportDocumentOptions;
            static toJson(value: any, serializer: any, refs: any): any;
            getInfo(): Analytics.Utils.ISerializationInfoArray;
            constructor(model: any, serializer?: DevExpress.Analytics.Utils.IModelSerializer);
        }
        class PdfPermissionsOptions {
            static from(model: any, serializer?: DevExpress.Analytics.Utils.IModelSerializer): PdfPermissionsOptions;
            static toJson(value: any, serializer: any, refs: any): any;
            getInfo(): Analytics.Utils.ISerializationInfoArray;
            constructor(model: any, serializer?: DevExpress.Analytics.Utils.IModelSerializer);
        }
        class PdfPasswordSecurityOptions {
            static from(model: any, serializer?: DevExpress.Analytics.Utils.IModelSerializer): PdfPasswordSecurityOptions;
            static toJson(value: any, serializer: any, refs: any): any;
            getInfo(): Analytics.Utils.ISerializationInfoArray;
            constructor(model: any, serializer?: DevExpress.Analytics.Utils.IModelSerializer);
            isPropertyDisabled(name: string): boolean;
            hasSensitiveData(): boolean;
            openPassword: ko.Observable<string> | ko.Computed<string>;
            permissionsPassword: ko.Observable<string> | ko.Computed<string>;
        }
        class PdfExportOptions {
            static from(model: any, serializer?: DevExpress.Analytics.Utils.IModelSerializer): PdfExportOptions;
            static toJson(value: any, serializer: any, refs: any): any;
            isPropertyDisabled(propertyName: string): boolean;
            getInfo(): Analytics.Utils.ISerializationInfoArray;
            constructor(model: any, serializer?: DevExpress.Analytics.Utils.IModelSerializer);
            hasSensitiveData(): boolean;
            pdfACompatibility: ko.Observable<string> | ko.Computed<string>;
            pdfPasswordSecurityOptions: PdfPasswordSecurityOptions;
        }
        module Metadata {
            var author: Analytics.Utils.ISerializationInfo;
            var application: Analytics.Utils.ISerializationInfo;
            var title: Analytics.Utils.ISerializationInfo;
            var subject: Analytics.Utils.ISerializationInfo;
            var pdfExportDocumentOptionsSerializationInfo: Analytics.Utils.ISerializationInfoArray;
            var pdfExportPermissionsOptionsSerializationInfo: Analytics.Utils.ISerializationInfoArray;
            var pdfEncryptionLevel: Analytics.Utils.ISerializationInfo;
            var pdfExportPasswordSecurityOptionsSerializationInfo: Analytics.Utils.ISerializationInfoArray;
            var pdfACompatibilityValues: {
                None: string;
                PdfA1b: string;
                PdfA2b: string;
                PdfA3b: string;
            };
            var pdfACompatibility: {
                propertyName: string;
                modelName: string;
                displayName: string;
                localizationId: string;
                editor: any;
                defaultVal: string;
                from: typeof Analytics.Utils.fromEnum;
                valuesArray: {
                    value: string;
                    displayValue: string;
                    localizationId: string;
                }[];
            };
            var pdfExportOptionsSerializationInfo: Analytics.Utils.ISerializationInfoArray;
        }
        class AdditionalRecipientModel implements DevExpress.Analytics.Utils.ISerializableModel {
            static createNew: () => AdditionalRecipientModel;
            getInfo(): Analytics.Utils.ISerializationInfoArray;
            constructor(model: any, serializer?: DevExpress.Analytics.Utils.IModelSerializer);
        }
        module Metadata {
            var nativeFormatOptionsSerializationInfo: Analytics.Utils.ISerializationInfoArray;
            var additionalRecipientSerializationsInfo: Analytics.Utils.ISerializationInfoArray;
            var additionalRecipients: Analytics.Utils.ISerializationInfo;
            var emailOptionsSerializationInfo: Analytics.Utils.ISerializationInfoArray;
        }
        class PrintPreviewOptions {
            static from(model: any, serializer?: DevExpress.Analytics.Utils.IModelSerializer): PrintPreviewOptions;
            static toJson(value: any, serializer: any, refs: any): any;
            getInfo(): Analytics.Utils.ISerializationInfoArray;
            constructor(model: any, serializer?: DevExpress.Analytics.Utils.IModelSerializer);
        }
        module Metadata {
            var printPreviewOptionsSerializationInfo: Analytics.Utils.ISerializationInfoArray;
        }
        class XlsExportOptions {
            static from(model: any, serializer?: DevExpress.Analytics.Utils.IModelSerializer): XlsExportOptions;
            static toJson(value: any, serializer: any, refs: any): any;
            getInfo(): Analytics.Utils.ISerializationInfoArray;
            constructor(model: any, serializer?: DevExpress.Analytics.Utils.IModelSerializer);
            isPropertyDisabled(name: string): boolean;
            hasSensitiveData(): boolean;
            xlsExportMode: ko.Observable<string> | ko.Computed<string>;
            encryptionOptions: {
                password: ko.Observable<string>;
            };
        }
        module Metadata {
            var xlsExportOptionsSerializationInfoCommon: Analytics.Utils.ISerializationInfoArray;
            var xlsExportOptionsSerializationInfoBase: Analytics.Utils.ISerializationInfoArray;
            var xlsExportOptionsSerializationInfo: Analytics.Utils.ISerializationInfoArray;
        }
        class XlsxExportOptions {
            static from(model: any, serializer?: DevExpress.Analytics.Utils.IModelSerializer): XlsxExportOptions;
            static toJson(value: any, serializer: any, refs: any): any;
            getInfo(): Analytics.Utils.ISerializationInfoArray;
            constructor(model: any, serializer?: DevExpress.Analytics.Utils.IModelSerializer);
            isPropertyDisabled(name: string): boolean;
            hasSensitiveData(): boolean;
            xlsxExportMode: ko.Observable<string> | ko.Computed<string>;
            encryptionOptions: {
                password: ko.Observable<string>;
            };
        }
        module Metadata {
            var xlsxExportOptionsSerializationInfo: Analytics.Utils.ISerializationInfoArray;
        }
        class ExportOptions {
            static from(model: any, serializer?: DevExpress.Analytics.Utils.IModelSerializer): ExportOptions;
            static toJson(value: any, serializer: any, refs: any): any;
            getInfo(): Analytics.Utils.ISerializationInfoArray;
            constructor(model: any, serializer?: DevExpress.Analytics.Utils.IModelSerializer);
            csv: CsvExportOptions;
            html: HtmlExportOptions;
            image: ImageExportOptions;
            mht: MhtExportOptions;
            pdf: PdfExportOptions;
            printPreview: PrintPreviewOptions;
            rtf: RtfExportOptions;
            textExportOptions: TextExportOptions;
            xls: XlsExportOptions;
            xlsx: XlsxExportOptions;
            docx: DocxExportOptions;
        }
        module Metadata {
            var exportOptionsSerializationInfo: Analytics.Utils.ISerializationInfoArray;
        }
    }
    module Viewer {
        var ActionId: {
            Design: string;
            FirstPage: string;
            PrevPage: string;
            Pagination: string;
            NextPage: string;
            LastPage: string;
            MultipageToggle: string;
            HighlightEditingFields: string;
            ZoomOut: string;
            ZoomSelector: string;
            ZoomIn: string;
            Print: string;
            PrintPage: string;
            ExportTo: string;
            Search: string;
            FullScreen: string;
        };
        var ExportFormatID: {
            PDF: {
                text: string;
                textId: string;
                format: string;
            };
            XLS: {
                text: string;
                textId: string;
                format: string;
            };
            XLSX: {
                text: string;
                textId: string;
                format: string;
            };
            RTF: {
                text: string;
                textId: string;
                format: string;
            };
            MHT: {
                text: string;
                textId: string;
                format: string;
            };
            HTML: {
                text: string;
                textId: string;
                format: string;
            };
            Text: {
                text: string;
                textId: string;
                format: string;
                propertyName: string;
            };
            CSV: {
                text: string;
                textId: string;
                format: string;
            };
            Image: {
                text: string;
                textId: string;
                format: string;
            };
            DOCX: {
                text: string;
                textId: string;
                format: string;
            };
        };
        var PreviewElements: {
            Toolbar: string;
            Surface: string;
            RightPanel: string;
        };
        var ZoomAutoBy: {
            None: number;
            WholePage: number;
            PageWidth: number;
        };
        module Settings {
            var EditablePreviewEnabled: ko.Observable<boolean>;
            var SearchAvailable: ko.Observable<boolean>;
            var HandlerUri: string;
            var ReportServerDownloadUri: string;
            var ReportServerInvokeUri: string;
            var ReportServerExportUri: string;
            var TimeOut: number;
            var PollingDelay: number;
            var previewDefaultResolution: number;
            var AsyncExportApproach: boolean;
            var MessageHandler: IMessageHandler;
            interface IMessageHandler {
                processError: (message: string, showForUser?: boolean, prefix?: string) => void;
                processMessage: (message: string, showForUser?: boolean) => void;
                processWarning: (message: string, showForUser?: boolean) => void;
            }
        }
        module Internal {
            class PreviewDesignerActions extends Analytics.Utils.Disposable implements Analytics.Internal.IActionsProvider {
                actions: Analytics.Utils.IAction[];
                dispose(): void;
                constructor(reportPreview: ReportPreview, element: Element);
                getActions(context: any): Analytics.Utils.IAction[];
            }
            class ActionLists extends Analytics.Internal.ActionListsBase {
                private _reportPreview;
                constructor(reportPreview: ReportPreview, globalActionProviders: ko.ObservableArray<Analytics.Internal.IActionsProvider>, customizeActions?: (actions: Analytics.Utils.IAction[]) => void, enabled?: ko.Observable<boolean>);
                processShortcut(actions: Analytics.Utils.IAction[], e: JQueryKeyEventObject): void;
                dispose(): void;
                globalActionProviders: ko.ObservableArray<Analytics.Internal.IActionsProvider>;
            }
            class PreviewActions extends Analytics.Utils.Disposable implements Analytics.Internal.IActionsProvider {
                actions: Analytics.Utils.IAction[];
                wrapDisposable<T>(object: T): T;
                constructor(reportPreview: ReportPreview);
                dispose(): void;
                getActions(context: any): Analytics.Utils.IAction[];
            }
            function convertToPercent(childSize: any, parentSize: any): string;
            function brickText(brick: Utils.IBrickNode, editingFieldsProvider?: () => Editing.EditingField[]): any;
            function updateBricksPosition(brick: Utils.IBrickNode, height: any, width: any): void;
            function initializeBrick(brick: Utils.IBrickNode, processClick: (target: Utils.IBrickNode, e?: JQueryEventObject) => void, zoom: ko.Observable<number> | ko.Computed<number>, editingFieldBricks: Utils.IBrickNode[]): void;
            function getCurrentResolution(zoom: any): number;
            function getImageBase64(url: any): JQueryPromise<{}>;
            function getEnumValues(enumType: any): string[];
        }
        module Utils {
            interface IPreviewInitialize {
                reportId?: string;
                documentData?: Internal.IGeneratedDocumentData;
                reportUrl?: string;
                documentId?: string;
                pageSettings?: IPreviewPageInitialSettings;
                exportOptions?: string;
                parametersInfo?: Parameters.IReportParametersInfo;
                rtlReport?: boolean;
                error?: any;
            }
            interface IPreviewModel {
                tabPanel: Analytics.Utils.TabPanel;
                reportPreview: ReportPreview;
                Close: () => void;
                ExportTo: (format?: string, inlineResult?: boolean) => void;
                GetCurrentPageIndex: () => number;
                GetParametersModel: () => Parameters.PreviewParametersViewModel;
                GoToPage: (pageIndex: number) => void;
                OpenReport: (reportUrl: string) => void;
                Print: (pageIndex?: number) => JQueryPromise<boolean>;
                ResetParameters: () => void;
                StartBuild: () => void;
            }
            interface IPreviewPageInitialSettings {
                height?: number;
                width?: number;
                color?: string;
            }
            interface IParametersCustomizationHandler {
                customizeParameterEditors?: (parameter: Parameters.IParameterDescriptor, info: DevExpress.Analytics.Utils.ISerializationInfo) => void;
                customizeParameterLookUpSource?: (parameter: Parameters.IParameterDescriptor, items: Array<DevExpress.Analytics.Utils.IDisplayedValue>) => any;
                parametersReset?: (parametersViewModel: Parameters.PreviewParametersViewModel, parameters: Parameters.IParameter[]) => void;
                parametersSubmitted?: (parametersViewModel: Parameters.PreviewParametersViewModel, parameters: Array<IKeyValuePair<any>>) => void;
            }
            interface IPreviewCustomizationHandler extends IParametersCustomizationHandler, Analytics.Internal.ICommonCustomizationHandler {
                customizeParts?: (parts: Analytics.Internal.IDesignerPart[]) => void;
                previewClick?: (pageIndex: number, brick: Utils.IBrickNode, defaultHandler: () => void) => boolean;
                editingFieldChanged?: (field: Editing.EditingField, oldValue: any, newValue: any) => any;
                documentReady?: (documentId: string, reportId: string, pageCount: number) => void;
                customizeExportOptions?: (options: IPreviewExportOptionsCustomizationArgs) => void;
            }
            interface IPreviewExportOptionsCustomizationArgs {
                exportOptions: Export.ExportOptionsPreview;
                panelVisible: boolean;
            }
            interface IMobileModeSettings {
                readerMode?: boolean;
                animationEnabled?: boolean;
            }
            interface ITabPanelSettings {
                position?: string;
                width?: number;
            }
            interface IRemoteSettings {
                authToken?: string;
                serverUri?: string;
            }
            interface IWebDocumentViewerSettings extends DevExpress.Analytics.Internal.ILocalizationSettings {
                handlerUri?: string;
                allowURLsWithJSContent?: boolean;
                rtl?: boolean;
                isMobile?: boolean;
                mobileModeSettings?: IMobileModeSettings;
                remoteSettings?: IRemoteSettings;
                tabPanelSettings?: ITabPanelSettings;
            }
            interface IWebDocumentViewerModel extends IPreviewInitialize, IWebDocumentViewerSettings, Analytics.Internal.IGlobalizeSettings {
                cultureInfoList?: {
                    [key: string]: string;
                };
            }
            interface IBrickNode {
                top: number;
                topP: string;
                left: number;
                leftP?: string;
                rightP?: string;
                height: number;
                heightP: string;
                width: number;
                widthP: string;
                bricks: IBrickNode[];
                content: Array<IKeyValuePair<any>>;
                indexes: string;
                row: number;
                col: number;
                genlIndex: number;
                active: ko.Observable<boolean> | ko.Computed<boolean>;
                navigation?: IBrickNodeNavigation;
                rtl: boolean;
                efIndex?: number;
                absoluteBounds?: Editing.IBounds;
                text: () => string;
                onClick?: (args?: any) => void;
            }
            interface IBrickNodeNavigation {
                url?: string;
                target?: string;
                indexes?: string;
                pageIndex?: number;
                drillDownKey?: string;
                sortData?: ISortingData;
            }
            interface ISortingData {
                target: string;
                field: string;
                order: Internal.ColumnSortOrder;
            }
            interface IDocumentOperationResult {
                documentId: string;
                succeeded: boolean;
                message?: string;
                customData?: string;
            }
        }
        module Internal {
            interface IProgressStatus {
                requestAgain: boolean;
                completed?: boolean;
                progress?: number;
                error?: string;
            }
            interface IExportProgressStatus extends IProgressStatus {
                token?: string;
                uri?: string;
            }
            interface IDocumentBuildStatus extends IProgressStatus {
                pageCount?: number;
            }
            class PreviewHandlersHelper {
                private _preview;
                constructor(preview: ReportPreview);
                doneStartExportHandler(deffered: any, inlineResult: boolean, response: any, printable?: boolean): void;
                errorStartExportHandler(deffered: any, error: any): void;
                doneExportStatusHandler(deffered: JQueryDeferred<any>, operationId: string, response: any): void;
                errorExportStatusHandler(deffered: any, error: any): void;
                doneStartBuildHandler(deffered: any, response: any): void;
                errorStartBuildHandler(deffered: any, error: any, startBuildOperationId: any): void;
                errorGetBuildStatusHandler(deffered: JQueryDeferred<IDocumentBuildStatus>, error: any, ignoreError: () => boolean): void;
                doneGetBuildStatusHandler(deffered: JQueryDeferred<IDocumentBuildStatus>, documentId: string, response: any, stopProcessingPredicate: () => boolean): void;
            }
            interface IGetPageResponse extends IGetBrickMapResult {
                width: number;
                height: number;
                base64string: string;
            }
            interface IGetBrickMapResult {
                brick: Utils.IBrickNode;
                columnWidthArray: Array<number>;
            }
            enum ColumnSortOrder {
                None = 0,
                Ascending = 1,
                Descending = 2
            }
            interface ISortingFieldInfo {
                fieldName?: string;
                sortOrder?: ColumnSortOrder;
            }
            interface IGeneratedDocumentData {
                documentMap?: Internal.IBookmarkNode;
                drillDownKeys?: Array<IKeyValuePair<boolean>>;
                sortingState?: Array<IKeyValuePair<Array<ISortingFieldInfo>>>;
                exportOptions?: string;
                canPerformContinuousExport?: boolean;
                editingFields?: Array<Editing.IEditingFieldModel>;
            }
            class PreviewRequestWrapper implements Editing.IEditingFieldHtmlProvider {
                private _callbacks?;
                private _reportPreview;
                private _parametersModel;
                private _searchModel;
                constructor(handlers?: {
                    [key: string]: Function;
                }, _callbacks?: Utils.IPreviewCustomizationHandler);
                static getProcessErrorCallback(reportPreview?: ReportPreview, defaultErrorMessage?: string, showMessage?: boolean): (message: string, jqXHR: any, textStatus: any) => void;
                static getPage(url: any, ignoreError?: () => boolean): JQueryPromise<IGetPageResponse>;
                initialize(reportPreview: ReportPreview, parametersModel: Parameters.PreviewParametersViewModel, searchModel: SearchViewModel): void;
                findTextRequest(text: any): JQueryPromise<any>;
                stopBuild(id: any): void;
                sendCloseRequest(documentId: string, reportId?: string): void;
                startBuildRequest(): JQueryPromise<any>;
                getBuildStatusRequest(documentId: any, shouldIgnoreError: () => boolean): JQueryPromise<any>;
                getDocumentData(documentId: any, shouldIgnoreError: () => boolean): JQueryPromise<IGeneratedDocumentData>;
                customDocumentOperation(documentId: string, serializedExportOptions: string, editindFields: any[], customData: string, hideMessageFromUser?: boolean): JQueryPromise<Utils.IDocumentOperationResult>;
                openReport(reportName: any): any;
                drillThrough(customData: string): any;
                getStartExportOperation(arg: string): any;
                getExportStatusRequest(operationId: string): any;
                getEditingFieldHtml(value: string, editingFieldIndex: number): any;
            }
            class PreviewSelection {
                private _element;
                private _page;
                private _click;
                static started: boolean;
                static disabled: boolean;
                private _$element;
                private _$selectionContent;
                private _bodyEvents;
                private _startRect;
                private _updateSelectionContent;
                private _mouseMove;
                private _mouseUp;
                private _mouseDown;
                constructor(_element: HTMLElement, _page: PreviewPage, _click: (pageIndex: number) => void);
                private _dispose;
                dispose: () => void;
            }
            interface IProgressHandler {
                stop?: () => void;
                cancelText?: ko.Observable<string> | ko.Computed<string>;
                progress: ko.Observable<number> | ko.Computed<number>;
                text: ko.Observable<string> | ko.Computed<string>;
                visible: ko.Observable<boolean> | ko.Computed<boolean>;
                startProgress: (onComplete?: () => void, onStop?: () => void) => void;
                inProgress: ko.Observable<boolean> | ko.Computed<boolean>;
                complete: () => void;
            }
            class ProgressViewModel implements IProgressHandler {
                progress: ko.Observable<number>;
                private _forceInvisible;
                private _onComplete;
                stop: () => void;
                inProgress: ko.Observable<boolean>;
                cancelText: ko.Observable<any>;
                text: ko.Observable<string>;
                visible: ko.PureComputed<boolean>;
                complete: () => void;
                startProgress: (onComplete?: () => void, onStop?: () => void) => void;
            }
            function updatePreviewContentSize(previewSize: ko.Observable<number> | ko.Computed<number>, root: Element, rtl?: boolean): (tabPanelPosition?: string) => void;
            function updatePreviewZoomWithAutoFit(width: any, height: any, $element: JQuery, autoFitBy?: number): number;
            class SortingProcessor {
                private _getSortingStage;
                constructor(_getSortingStage: () => Array<IKeyValuePair<Array<ISortingFieldInfo>>>);
                doSorting(sortData: Utils.ISortingData, shiftKey?: boolean, ctrlKey?: boolean): boolean;
                private _applySorting;
                private _appendSorting;
                private _detachSorting;
                private _changeSortOrder;
            }
            interface IPreviewPageOwner {
                _pageWidth: any;
                _pageHeight: any;
                _zoom: ko.Observable<number> | ko.Computed<number>;
                _currentDocumentId: ko.Observable<string> | ko.Computed<string>;
                _unifier: ko.Observable<string> | ko.Computed<string>;
                _pageBackColor?: ko.Observable<string> | ko.Computed<string>;
                _editingFields: ko.Observable<Editing.EditingField[]>;
                loading?: ko.Observable<boolean> | ko.Computed<boolean>;
                processClick?: (target: Utils.IBrickNode) => void;
                _closeDocumentRequests?: {
                    [key: string]: boolean;
                };
            }
            class PreviewPage extends Analytics.Utils.Disposable {
                private _initializeEditingFields;
                private _getPixelRatio;
                private _onPageLoaded;
                private _onPageLoadFailed;
                constructor(preview: IPreviewPageOwner, pageIndex: number, processClick?: (target: Utils.IBrickNode) => void, loading?: ko.Observable<boolean>);
                updateSize(zoom?: number): number;
                clearBricks(): void;
                _setPageImgSrc(documentId: string, unifier: string, zoom?: number): void;
                _clear(): void;
                initializeBrick(brick: Utils.IBrickNode, processClick: (target: Utils.IBrickNode) => void, zoom: ko.Observable<number> | ko.Computed<number>, editingFieldBricks: Utils.IBrickNode[]): void;
                clickToBrick(s: PreviewPage, e: JQueryEventObject): void;
                getBricksFlatList(brick: Utils.IBrickNode): Utils.IBrickNode[];
                editingFields: ko.Computed<Editing.IEditingFieldViewModel[]>;
                isClientVisible: ko.Observable<boolean>;
                documentId: ko.Observable<string> | ko.Computed<string>;
                originalHeight: ko.Observable<number>;
                originalWidth: ko.Observable<number>;
                selectBrick: (path: string, ctrlKey?: boolean) => void;
                resetBrickRecusive: (brick: Utils.IBrickNode) => void;
                getBricks: (pageIndex: number) => void;
                loadingText: string;
                realZoom: ko.Observable<number>;
                actualResolution: number;
                isEmpty: boolean;
                pageIndex: number;
                pageLoading: ko.Observable<boolean> | ko.Computed<boolean>;
                currentScaleFactor: ko.Observable<number>;
                zoom: ko.Observable<number> | ko.Computed<number>;
                width: ko.Observable<number> | ko.Computed<number>;
                height: ko.Observable<number> | ko.Computed<number>;
                color: string;
                imageHeight: ko.Observable<number>;
                imageWidth: ko.Observable<number>;
                imageSrc: ko.Observable<string>;
                displayImageSrc: ko.Observable<string>;
                brick: ko.Observable<Utils.IBrickNode>;
                brickLoading: ko.Observable<boolean>;
                brickColumnWidthArray: Array<number>;
                bricks: ko.Computed<Utils.IBrickNode[]>;
                activeBricks: ko.Computed<Utils.IBrickNode[]>;
                clickableBricks: ko.Computed<Utils.IBrickNode[]>;
                active: ko.Observable<boolean>;
                maxZoom: number;
                disableResolutionReduction: boolean;
                private _lastZoom;
                private _selectedBrickPath;
            }
            interface IPreviewModel {
                rootStyle: string | {
                    [key: string]: boolean;
                };
                reportPreview: ReportPreview;
                parametersModel: Parameters.PreviewParametersViewModel;
                exportModel: Export.ExportOptionsModel;
                searchModel: Internal.SearchViewModel;
                documentMapModel: Internal.DocumentMapModel;
                tabPanel: Analytics.Utils.TabPanel;
                actionLists: Internal.ActionLists;
                rtl: boolean;
                parts?: Analytics.Internal.IDesignerPart[];
                resizeCallback?: () => void;
                updateSurfaceSize?: () => void;
            }
            class PreviewModel extends Analytics.Utils.Disposable implements IPreviewModel {
                rootStyle: string | {
                    [key: string]: boolean;
                };
                reportPreview: ReportPreview;
                parametersModel: Parameters.PreviewParametersViewModel;
                exportModel: Export.ExportOptionsModel;
                searchModel: SearchViewModel;
                documentMapModel: DocumentMapModel;
                tabPanel: Analytics.Utils.TabPanel;
                actionLists: ActionLists;
                rtl: boolean;
                parts?: Analytics.Internal.IDesignerPart[];
                resizeCallback?: () => void;
                updateSurfaceSize?: () => void;
                constructor(options: IPreviewModel);
                _addDisposable(object: any): void;
                dispose(): void;
            }
            function createDesktopPreview(element: Element, callbacks: Utils.IPreviewCustomizationHandler, parametersInfo?: Parameters.IReportParametersInfo, handlerUri?: string, previewVisible?: boolean, applyBindings?: boolean, allowURLsWithJSContent?: boolean, rtl?: boolean, tabPanelSettings?: Utils.ITabPanelSettings): PreviewModel;
            function createPreview(element: Element, callbacks: Utils.IPreviewCustomizationHandler, localization?: Analytics.Internal.ILocalizationSettings, parametersInfo?: Parameters.IReportParametersInfo, handlerUri?: string, previewVisible?: boolean, rtl?: boolean, isMobile?: boolean, mobileModeSettings?: Utils.IMobileModeSettings, applyBindings?: boolean, allowURLsWithJSContent?: boolean, remoteSettings?: Utils.IRemoteSettings, tabPanelSettings?: Utils.ITabPanelSettings): JQueryDeferred<any>;
            function createAndInitPreviewModel(viewerModel: Utils.IWebDocumentViewerModel, element: Element, callbacks?: Utils.IPreviewCustomizationHandler, applyBindings?: boolean): JQueryDeferred<any>;
        }
        module Parameters {
            class MultiValuesHelper {
                items: Array<Analytics.Utils.IDisplayedValue>;
                constructor(value: ko.ObservableArray<any>, items: Array<Analytics.Utils.IDisplayedValue>);
                selectedItems: ko.ObservableArray<any>;
                isSelectedAll: ko.Observable<boolean> | ko.Computed<boolean>;
                maxDisplayedTags: number;
                dataSource: any;
                value: ko.ObservableArray<any>;
            }
            interface IParameter {
                getParameterDescriptor: () => IParameterDescriptor;
                value: ko.Observable | ko.Computed;
                type: any;
                isMultiValue: any;
                allowNull: any;
                multiValueInfo: ko.Observable<Analytics.Utils.ISerializationInfo> | ko.Computed<Analytics.Utils.ISerializationInfo>;
                tag?: any;
            }
            interface IParameterDescriptor {
                description: string;
                name: string;
                type: string;
                value: any;
                visible: boolean;
                multiValue?: boolean;
                allowNull?: boolean;
                tag?: any;
            }
            function getEditorType(typeString: any): any;
            class ParameterHelper {
                private _knownEnums;
                private _customizeParameterEditors;
                private _isKnownEnumType;
                static getSerializationValue(value: any, dateConverter: any): any;
                static createDefaultDataSource(store: ArrayStore): DataSource;
                initialize(knownEnums?: Array<DevExpress.Reporting.IEnumType>, callbacks?: Utils.IParametersCustomizationHandler): void;
                createInfo(parameter: IParameter): DevExpress.Analytics.Utils.ISerializationInfo;
                addShowCleanButton(info: DevExpress.Analytics.Utils.ISerializationInfo, parameter: IParameter): void;
                assignValueStore(info: DevExpress.Analytics.Utils.ISerializationInfo, parameter: IParameter): void;
                createMultiValue(parameter: IParameter, value?: any): {
                    value: ko.Observable<any>;
                    getInfo: () => Analytics.Utils.ISerializationInfo[];
                };
                createMultiValueArray(fromArray: Array<any>, parameter: IParameter, convertSingleValue?: (val: any) => any): ko.ObservableArray<{
                    value: ko.Observable<any>;
                    getInfo: () => Analytics.Utils.ISerializationInfo[];
                }>;
                isEnumType(parameter: IParameter): boolean;
                getItemsSource(parameterDescriptor: IParameterDescriptor, items: Array<Analytics.Utils.IDisplayedValue>, sort?: boolean): any;
                getEnumCollection(parameter: IParameter): Array<Analytics.Utils.IDisplayedValue>;
                getParameterInfo(parameter: IParameter): DevExpress.Analytics.Utils.ISerializationInfo;
                getValueConverter(type: string): (val: any) => any;
                customizeParameterLookUpSource: (parameter: IParameterDescriptor, items: Array<DevExpress.Analytics.Utils.IDisplayedValue>) => any;
                getUnspecifiedDisplayText: () => any;
            }
            interface IPreviewParameterDescriptor extends IParameterDescriptor {
                hasLookUpValues?: boolean;
            }
            class PreviewParameter extends Analytics.Utils.Disposable implements IParameter {
                static _compareValues(value1: any, value2: any): boolean;
                constructor(parameterInfo: IPreviewParameterInfo, parameterHelper: PreviewParameterHelper);
                getParameterDescriptor: () => IParameterDescriptor;
                safeAssignObservable(name: string, value: ko.Observable<any>): void;
                initialize(value: any, parameterHelper: PreviewParameterHelper): void;
                valueInfo: ko.Observable<Analytics.Utils.ISerializationInfo>;
                value: ko.Observable<any>;
                _value: ko.Observable<any>;
                _originalLookUpValues: Array<Analytics.Utils.IDisplayedValue>;
                _originalValue: any;
                tag: any;
                type: string;
                path: string;
                isFilteredLookUpSettings: boolean;
                lookUpValues: ko.ObservableArray<Analytics.Utils.IDisplayedValue>;
                valueStoreCache: any;
                allowNull: boolean;
                isMultiValue: boolean;
                isMultiValueWithLookUp: boolean;
                multiValueInfo: ko.Observable<Analytics.Utils.ISerializationInfo>;
                visible: boolean;
                intTypes: string[];
                floatTypes: string[];
                isTypesCurrentType: (types: string[], type: string) => boolean;
            }
            interface IReportParametersInfo {
                shouldRequestParameters?: boolean;
                parameters?: Array<IPreviewParameterInfo>;
                knownEnums?: Array<Reporting.IEnumType>;
            }
            interface IPreviewParameterInfo {
                Path: string;
                Description: string;
                Name: string;
                Value: any;
                TypeName: string;
                ValueInfo?: any;
                MultiValue?: boolean;
                AllowNull?: boolean;
                IsFilteredLookUpSettings?: boolean;
                LookUpValues?: Array<ILookUpValue>;
                Visible?: boolean;
                Tag?: any;
            }
            interface ILookUpValue {
                Description: string;
                Value: any;
            }
            class PreviewParametersViewModel extends Analytics.Utils.Disposable {
                private _parameters;
                private readonly _visibleParameters;
                private _shouldProcessParameter;
                private _reportPreview;
                private _convertLocalDateToUTC;
                private _getLookUpValueRequest;
                private _getDoneGetLookUpValueHandler;
                private _add;
                private _getFailGetLookUpValueHandler;
                private _setLookUpValues;
                private _getParameterValuesContainedInLookups;
                private _filterParameterValuesContainsInLookups;
                constructor(reportPreview: ReportPreview, parameterHelper?: PreviewParameterHelper);
                tabInfo: DevExpress.Analytics.Utils.TabInfo;
                popupInfo: any;
                parameterHelper: PreviewParameterHelper;
                initialize(originalParametersInfo: IReportParametersInfo): void;
                getPathsAfterPath(parameterPath: string): Array<string>;
                serializeParameters(): Array<IKeyValuePair<any>>;
                restore(): void;
                getInfo: ko.Observable<any>;
                isPropertyDisabled(name: string): boolean;
                getLookUpValues(changedParameterPath: string): void;
                submit: () => void;
                validateAndSubmit: (params: any) => void;
                needToRefreshLookUps: ko.Observable<boolean>;
                isEmpty: ko.Observable<boolean>;
                processInvisibleParameters: boolean;
                parametersLoading: ko.Observable<boolean>;
            }
            class PreviewParameterHelper extends ParameterHelper {
                callbacks?: Utils.IParametersCustomizationHandler;
                mapLookUpValues(type: string, lookUpValues: Array<ILookUpValue>): Array<Analytics.Utils.IDisplayedValue>;
                static fixPropertyName(propertyName: string): string;
                static getPrivatePropertyName(propertyName: string): string;
                createInfo(parameter: PreviewParameter): DevExpress.Analytics.Utils.ISerializationInfo;
                assignValueStore(info: DevExpress.Analytics.Utils.ISerializationInfo, parameter: PreviewParameter): void;
                isEnumType(parameter: PreviewParameter): boolean;
                getValueConverter(type: string): (val: any) => any;
                constructor(knownEnums?: Array<DevExpress.Reporting.IEnumType>, callbacks?: Utils.IParametersCustomizationHandler);
            }
        }
        module Internal {
            var formatSearchResult: (value: IFoundText) => string;
            interface IFoundText {
                pageIndex: number;
                indexes: string;
                id: number;
                text: string;
            }
            interface ISearchResult {
                matches: Array<IFoundText>;
                success: boolean;
            }
            interface ISearchEditorModel {
                findUp: () => void;
                findDown: () => void;
                loading: ko.Observable<boolean> | ko.Computed<boolean>;
                searchText: ko.Observable<string> | ko.Computed<string>;
                focusRequested: ko.Subscribable<boolean>;
            }
            class SearchViewModel extends Analytics.Utils.Disposable implements ISearchEditorModel, Analytics.Internal.IActionsProvider {
                private _cachedRequests;
                private _cachedWholeWordRequests;
                private _cachedCaseSensitiveRequests;
                private _cachedWholeWordWithCaseRequests;
                private _resultNavigator;
                static createResultNavigator: (seacrhModel: SearchViewModel, reportPreview: ReportPreview) => SearchResultNavigator;
                resetSearchResult(): void;
                findTextRequestDone(result: ISearchResult, cache: any): void;
                constructor(reportPreview: ReportPreview);
                getActions(context: any): Analytics.Utils.IAction[];
                tabInfo: Analytics.Utils.TabInfo;
                actions: Analytics.Utils.IAction[];
                findUp: () => void;
                findDown: () => void;
                goToResult: (result: IFoundText) => void;
                focusRequested: ko.Observable<boolean>;
                matchWholeWord: ko.Observable<boolean>;
                matchCase: ko.Observable<boolean>;
                searchUp: ko.Observable<boolean>;
                searchText: ko.Observable<string>;
                searchResult: ko.Observable<IFoundText[]>;
                readonly disabled: boolean;
                loading: ko.Observable<boolean>;
                findNext: () => void;
                clean: () => void;
            }
            interface ISearchResultNavigator {
                next: (up: boolean) => boolean;
                getFirstMatchFromPage: (pageIndex: number, up: boolean, thisPageOnly?: boolean) => IFoundText;
                currentResult: ko.Observable<IFoundText>;
                goToResult: (resultId: number) => void;
                searchResult: ko.Observable<IFoundText[]>;
            }
            class SearchResultNavigator extends Analytics.Utils.Disposable implements ISearchResultNavigator {
                constructor(searchModel: SearchViewModel, reportPreview: ReportPreview);
                next: (up: boolean) => boolean;
                getFirstMatchFromPage: (pageIndex: number, up: boolean, thisPageOnly?: boolean) => IFoundText;
                currentResult: ko.Observable<any>;
                goToResult: (resultId: number) => void;
                searchResult: ko.Observable<IFoundText[]>;
            }
            class dxSearchEditor extends dxTextBox {
                _$button: JQuery;
                _$buttonIcon: JQuery;
                _searchModel: any;
                _activeStateUnit: any;
                _focusRequestRaised: any;
                _koContext: any;
                constructor(element: any, options?: any);
                findNext(searchUp: boolean): boolean;
                _init(): void;
                _render(): void;
                _renderButton(direction: string): void;
                _attachButtonEvents(direction: string): void;
            }
            class DocumentMapItemsProvider implements DevExpress.Analytics.Utils.IItemsProvider {
                constructor(bookmark: IBookmarkNode);
                getItems: (IPathRequest: any) => JQueryPromise<IBookmarkDataMemberInfo[]>;
                private _selectNode;
                static fillNode(bookmark: IBookmarkNode): IBookmarkDataMemberInfo[];
                private _getRootNode;
                bookmarkDict: {};
            }
            class DocumentMapTreeListController implements DevExpress.Analytics.Widgets.Internal.ITreeListController {
                itemsFilter(item: DevExpress.Analytics.Utils.IDataMemberInfo): boolean;
                hasItems(item: DevExpress.Analytics.Utils.IDataMemberInfo): boolean;
                canSelect(value: DevExpress.Analytics.Widgets.Internal.TreeListItemViewModel): boolean;
                select(value: DevExpress.Analytics.Widgets.Internal.TreeListItemViewModel): void;
                showIconsForChildItems(): boolean;
                selectedItem: ko.Observable<Analytics.Widgets.Internal.TreeListItemViewModel>;
                clickHandler: (item: Analytics.Widgets.Internal.TreeListItemViewModel) => any;
            }
            interface IBookmarkNode {
                text: string;
                pageIndex: number;
                indexes: string;
                nodes?: Array<IBookmarkNode>;
            }
            interface IBookmarkDataMemberInfo extends DevExpress.Analytics.Utils.IDataMemberInfo {
                bookmark: IBookmarkNode;
            }
            class DocumentMapModel extends Analytics.Utils.Disposable {
                private _selectedPath;
                private _setSelectedPathByNavigationNode;
                constructor(reportPreview: ReportPreview);
                dispose(): void;
                tabInfo: Analytics.Utils.TabInfo;
                isEmpty: ko.Computed<boolean>;
                documentMapOptions: ko.Computed<Analytics.Widgets.Internal.ITreeListOptions>;
            }
        }
        module Widgets {
            module Internal {
                interface IMultiValueItem extends Analytics.Utils.IDisplayedValue {
                    selected?: ko.Observable<boolean> | ko.Computed<boolean>;
                    toggleSelected?: () => void;
                }
                class MultiValueEditorOptions extends Analytics.Utils.Disposable {
                    private _isValueSelected;
                    constructor(value: ko.Observable<any>, items: Array<Analytics.Utils.IDisplayedValue>);
                    selectedItems: ko.Observable<Array<IMultiValueItem>> | ko.Computed<Array<IMultiValueItem>>;
                    editorValue: ko.Observable<IMultiValueItem> | ko.Computed<IMultiValueItem>;
                    isSelectedAll: ko.Observable<boolean> | ko.Computed<boolean>;
                    _items: Array<IMultiValueItem>;
                    selectedValuesString: ko.Observable<string> | ko.Computed<string>;
                    displayItems: Array<IMultiValueItem>;
                    dataSource: any;
                    updateValue: () => void;
                    onOptionChanged: (e: any) => void;
                    value: ko.Observable | ko.Computed;
                }
            }
            enum PictureEditMode {
                Image = 0,
                Signature = 1,
                ImageAndSignature = 2
            }
            module Internal {
                class ImagePainter {
                    private _drawImage;
                    private _getImageSize;
                    private _getImageCoordinate;
                    constructor(options: {
                        imageSource: ko.Observable<string>;
                        sizeMode: ko.Observable<Editing.ImageSizeMode>;
                        alignment: ko.Observable<Editing.ImageAlignment>;
                    });
                    refresh(context: CanvasRenderingContext2D, scale?: number, contentSize?: any): JQueryPromise<{}>;
                    format: ko.Observable<string>;
                    image: ko.Observable<string> | ko.Computed<string>;
                    sizeMode: ko.Observable<Editing.ImageSizeMode>;
                    alignment: ko.Observable<Editing.ImageAlignment>;
                }
                class SignaturePainter extends DevExpress.Analytics.Utils.Disposable {
                    dispose(): void;
                    private _points;
                    private _lastX;
                    private _lastY;
                    private _drawPath;
                    private _drawCircle;
                    private _drawAllPoints;
                    drawCircle(context: any, x: any, y: any, color: any, width: any): void;
                    drawPath(context: any, x: any, y: any, color: any, width: any): void;
                    resetLastPosition(): void;
                    resetPoints(): void;
                    reset(): void;
                    refresh(context: any): void;
                    constructor();
                    hasPoints: ko.Computed<boolean>;
                }
                interface IPainterOptions {
                    imageSource: string;
                    imageType: string;
                    zoom: ko.Observable<number> | ko.Computed<number>;
                    sizeMode: Editing.ImageSizeMode;
                    alignment: Editing.ImageAlignment;
                    canDraw: ko.Observable<boolean> | ko.Computed<boolean>;
                }
                class Painter extends DevExpress.Analytics.Utils.Disposable {
                    private $element;
                    private _context;
                    private _getContextPoint;
                    private _pointerDownHandler;
                    private _pointerMoveHandler;
                    private _pointerLeaveHandler;
                    private _addEvents;
                    private _removeEvents;
                    private _setCanvasSize;
                    private _cleanCanvas;
                    constructor(options: IPainterOptions);
                    clear(): void;
                    refresh(): void;
                    initSize(element: JQuery, zoom: number): void;
                    initCanvas(element: JQuery, zoom: number): void;
                    getImage(): string;
                    hasSignature(): boolean;
                    dispose(): void;
                    reset(initialImage: any, initialAlignment: any, initialSizeMode: any): void;
                    initialSize: {
                        width: number;
                        height: number;
                    };
                    canDraw: boolean;
                    height: number;
                    format: (newVal?: string) => string;
                    image: ko.Observable<string> | ko.Computed<string>;
                    imageSizeMode: ko.Observable<Editing.ImageSizeMode>;
                    imageAlignment: ko.Observable<Editing.ImageAlignment>;
                    zoom: ko.Observable<number> | ko.Computed<number>;
                    scale: ko.Observable<number> | ko.Computed<number>;
                    lineWidth: ko.Observable<number>;
                    lineColor: ko.Observable<string>;
                    imagePainter: ImagePainter;
                    signaturePainter: SignaturePainter;
                }
                class PictureEditorToolbarItem implements IPictureEditorToolbarItem {
                    constructor(options: IPictureEditorToolbarItemOptions);
                    dispose(): void;
                    id: PictureEditorActionId;
                    icon: string;
                    title: string;
                    isActive: ko.Observable<boolean> | ko.Computed<boolean>;
                    renderedHandler: (element: HTMLElement, model: any) => void;
                    action: (e: any, model: any) => void;
                }
                class PictureEditorToolbarItemWithPopup extends PictureEditorToolbarItem implements IPictureEditorToolbarItemWithPopup {
                    constructor(options: IPictureEditorToolbarItemWithTemplateOptions<IPictureEditorActionPopupOptions>);
                    dispose(): void;
                    component: ko.Observable<IPopupComponent>;
                    template: string;
                    templateOptions: IPictureEditorActionPopup;
                }
                interface IPictureEditorToolbarItem extends IPictureEditorToolbarItemOptions {
                    dispose: () => void;
                }
                interface IPictureEditorToolbarItemWithPopup extends IPictureEditorToolbarItemWithTemplateOptions<IPictureEditorActionPopup> {
                    dispose: () => void;
                }
                interface IPictureEditorToolbarItemWithTemplateOptions<T> extends IPictureEditorToolbarItemOptions {
                    template: string;
                    templateOptions?: T;
                }
                interface IPictureEditorToolbarItemOptions {
                    id: PictureEditorActionId;
                    icon: string;
                    action?: (e: any, model: any) => void;
                    isActive: ko.Observable<boolean> | ko.Computed<boolean>;
                    renderedHandler?: (element: HTMLElement, model: any) => void;
                    title: string;
                }
                interface IPictureEditorActionPopup extends IPictureEditorActionPopupOptions {
                    component: IPopupComponent;
                    onContentReady: (e: {
                        element: any;
                        component: IPopupComponent;
                        model: IPictureEditorActionPopupOptions;
                    }) => void;
                    onShown: (e: {
                        element: any;
                        component: IPopupComponent;
                        model: IPictureEditorActionPopupOptions;
                    }) => void;
                    closeOnOutsideClick: (e: {
                        target: any;
                    }) => boolean;
                }
                interface IPopupComponent {
                    content: () => Element;
                    $element: () => JQuery;
                    dispose: () => void;
                }
                interface IPictureEditorActionPopupOptions {
                    width: string;
                    height: string;
                    contentTemplate: string;
                    contentData: any;
                    container: string;
                    target: string;
                    boundary: string | any;
                    getPositionTarget: () => any;
                    visible: ko.Observable<boolean> | ko.Computed<boolean>;
                }
                enum PictureEditorActionId {
                    OpenFile = 0,
                    PickImage = 1,
                    Alignment = 2,
                    Brush = 3,
                    Clear = 4,
                    Reset = 5
                }
                interface IImageEditorItem {
                    data?: string;
                    url?: string;
                    text?: string;
                    visible?: boolean | ko.Computed<boolean>;
                }
                class PictureEditorActionProvider extends Analytics.Utils.Disposable {
                    private _editorModel;
                    private _popupOptions;
                    static colors: string[];
                    private _getValues;
                    private _getColorValues;
                    private _initPopupOptions;
                    createOpenFileAction(action: (e: any) => void): PictureEditorToolbarItemWithPopup;
                    createImagePickerAction(images: IImageEditorItem[], filterEnabled: boolean, action: (base64: string) => void): PictureEditorToolbarItemWithPopup;
                    createSizingAction(): PictureEditorToolbarItemWithPopup;
                    createBrushAction(): PictureEditorToolbarItemWithPopup;
                    createResetItem(action: () => void): PictureEditorToolbarItem;
                    createClearItem(action: () => void): PictureEditorToolbarItem;
                    constructor(_editorModel: PictureEditorModel, _popupOptions: any);
                }
                class PictureEditorModel extends Analytics.Utils.Disposable {
                    private $element;
                    private _initialImage;
                    private _initialAlignment;
                    private _initialSizeMode;
                    private _pointerDownHandler;
                    private _pointerUpHandler;
                    private _pointerCancelHandler;
                    private _callbacks;
                    private GESTURE_COVER_CLASS;
                    private ACTIVE_POPUP_CLASS;
                    private _cacheLoadFileElement;
                    private _getPopupContent;
                    private _takeFocus;
                    private _releaseFocus;
                    private _wrapButtonAction;
                    private _initActions;
                    private _clearLoadedFile;
                    private _loadImageCallBack;
                    private _loadImage;
                    private _handleFiles;
                    private _addEvents;
                    constructor(options: IPictureEditorOptions, element: HTMLElement);
                    changeActiveButton(selectedItem: any): void;
                    applyBindings(): void;
                    dispose(): void;
                    getImage(): string;
                    reset(image: any, alignment: any, sizeMode: any): void;
                    getCurrentOptions(): IImageEditValue;
                    actionsProvider: PictureEditorActionProvider;
                    editMode: PictureEditMode;
                    actions: Array<IPictureEditorToolbarItem>;
                    painter: Painter;
                    isActive: ko.Observable<boolean> | ko.Computed<boolean>;
                    canDraw: ko.Observable<boolean> | ko.Computed<boolean>;
                    zoom: ko.Observable<number> | ko.Computed<number>;
                }
                interface IPictureEditorCallbacks {
                    onFocusOut: (s: any) => void;
                    onFocusIn?: (s: any) => void;
                    onDraw: (s: any) => void;
                    customizeActions?: (s: PictureEditorModel, actions: Array<IPictureEditorToolbarItem>) => void;
                }
                interface IImageEditValue {
                    sizeMode: Editing.ImageSizeMode;
                    alignment: Editing.ImageAlignment;
                    imageType: string;
                    image: string;
                }
                interface IPictureEditorOptions {
                    image: ko.Observable<string> | ko.Computed<string>;
                    imageType: ko.Observable<string> | ko.Computed<string>;
                    imageMode: ko.Observable<PictureEditMode> | ko.Computed<PictureEditMode>;
                    sizeMode: ko.Observable<Editing.ImageSizeMode> | ko.Computed<Editing.ImageSizeMode>;
                    alignment: ko.Observable<Editing.ImageAlignment> | ko.Computed<Editing.ImageAlignment>;
                    callbacks: IPictureEditorCallbacks;
                    isActive: ko.Observable<boolean> | ko.Computed<boolean>;
                    zoom: ko.Observable<number> | ko.Computed<number>;
                    popupOptions: IPictureEditorPopupTargetOptions;
                }
                interface IPictureEditorPopupTargetOptions {
                    target?: string;
                    container?: string;
                    boundary?: string;
                }
                interface IClickEvent {
                    target: HTMLElement;
                }
            }
            var editorTemplates: {
                multiValue: {
                    header: string;
                    extendedOptions: {
                        placeholder: () => any;
                        onMultiTagPreparing: (args: any) => void;
                    };
                };
                multiValueEditable: {
                    custom: string;
                };
                selectBox: {
                    header: string;
                };
            };
        }
        module Editing {
            class TextEditingFieldViewModelBase {
                keypressAction(data: any, event: any): void;
                hideEditor: (shouldCommit: boolean) => void;
            }
            class TextEditingFieldViewModel extends TextEditingFieldViewModelBase implements IEditingFieldViewModel {
                constructor(field: EditingField, pageWidth: number, pageHeight: number, zoom: ko.Observable<number> | ko.Computed<number>, bounds: IBounds);
                template: string;
                editorTemplate: string;
                field: EditingField;
                data: any;
                textStyle: () => {};
                containerStyle: () => {};
                breakOffsetStyle: () => {};
                borderStyle: () => {};
                zoom: ko.Observable<number> | ko.Computed<number>;
                htmlValue: () => string;
                wordWrap: boolean;
                active: ko.Observable<boolean>;
                activateEditor(viewModel: any, e: any): void;
            }
            enum GlyphStyle {
                StandardBox1 = 0,
                StandardBox2 = 1,
                YesNoBox = 2,
                YesNoSolidBox = 3,
                YesNo = 4,
                RadioButton = 5,
                Smiley = 6,
                Thumb = 7,
                Toggle = 8,
                Star = 9,
                Heart = 10
            }
            enum CheckState {
                Unchecked = 0,
                Checked = 1,
                Indeterminate = 2
            }
            function createCustomGlyphStyleCss(imageSource: DevExpress.Reporting.ImageSource): {};
            function getCheckBoxTemplate(style: string, state: string, customGlyph: {}): any;
            class CheckEditingFieldViewModel extends Analytics.Utils.Disposable implements IEditingFieldViewModel {
                private _editingFieldsProvider;
                private _toggleCheckState;
                constructor(field: EditingField, pageWidth: number, pageHeight: number, zoom: ko.Observable<number> | ko.Computed<number>, editingFieldsProvider: () => EditingField[]);
                template: string;
                field: EditingField;
                containerStyle: () => {};
                checkStyle: () => {};
                checkStateStyleIcon: any;
                customGlyphStyleCss: any;
                zoom: ko.Observable<number> | ko.Computed<number>;
                focused: ko.Observable<boolean>;
                onKeyDown(_: any, e: any): void;
                onBlur(): void;
                onFocus(): void;
                onClick(_: any, e: any): void;
                checked(): boolean;
                toggleCheckState(): void;
            }
            class ImageEditingFieldViewModel extends Analytics.Utils.Disposable implements IEditingFieldViewModel {
                field: EditingField;
                zoom: ko.Observable<number> | ko.Computed<number>;
                protected bounds: IBounds;
                protected popupTarget: string;
                constructor(field: EditingField, pageWidth: number, pageHeight: number, zoom: ko.Observable<number> | ko.Computed<number>, bounds: IBounds);
                getImage(): any;
                getImageType(): any;
                getPictureEditorOptions(): Widgets.Internal.IPictureEditorOptions;
                alignment: ko.Computed<ImageAlignment>;
                sizeMode: ko.Computed<ImageSizeMode>;
                editMode: Widgets.PictureEditMode;
                popupOptions: Widgets.Internal.IPictureEditorPopupTargetOptions;
                template: string;
                isActive: ko.Observable<boolean>;
                containerStyle: () => {};
                callbacks: Widgets.Internal.IPictureEditorCallbacks;
                onKeyDown(_: any, e: any): void;
                onFocusIn(s: Widgets.Internal.PictureEditorModel): void;
                onDraw(s: Widgets.Internal.PictureEditorModel): void;
                onBlur(s: Widgets.Internal.PictureEditorModel): void;
            }
            interface IImageEditingFieldPopupData {
                contentData: PopupImageEditingFieldViewModel;
                paintData: Viewer.Widgets.Internal.IPainterOptions;
                contentTemplate: string;
                isVisible: (element: HTMLElement) => boolean;
                getContainer: () => string;
                getPositionTarget: (element: HTMLElement) => JQuery;
                showContent: ko.Observable<boolean>;
                onShown: (e: {
                    element: any;
                    component: any;
                }) => void;
                onHiding: (e: {
                    element: any;
                    component: any;
                }) => void;
                onContentReady: (e: {
                    element: any;
                    component: Viewer.Widgets.Internal.IPopupComponent;
                }) => void;
                renderedHandler: (element: HTMLElement, model: any) => void;
            }
            class PopupImageEditingFieldViewModel extends Viewer.Editing.ImageEditingFieldViewModel implements Viewer.Editing.IEditingFieldViewModel {
                parentPopupClass: string;
                private _popupInitializedClass;
                private _getPopupContainer;
                private _getPainterModel;
                private _getPictureEditorModel;
                private _resetPictureEditor;
                private _resetPainter;
                isPopupActive(element: any): boolean;
                getPainter(): Widgets.Internal.IPainterOptions;
                getPopupData(): IImageEditingFieldPopupData;
                activateEditor(viewModel: any, e: any): void;
                popupData: IImageEditingFieldPopupData;
                painterData: Viewer.Widgets.Internal.IPainterOptions;
                template: string;
            }
            var DefaultImageEditingFieldViewModel: typeof PopupImageEditingFieldViewModel;
            class CharacterCombEditingFieldViewModel extends TextEditingFieldViewModelBase implements IEditingFieldViewModel {
                field: EditingField;
                constructor(field: EditingField, pageWidth: number, pageHeight: number, zoom: ko.Observable<number> | ko.Computed<number>, bounds: IBounds);
                cells: {
                    text: string;
                    style: any;
                }[];
                rowsCount: number;
                colsCount: number;
                template: string;
                containerStyle: () => {};
                textStyle: () => {};
                zoom: ko.Observable<number> | ko.Computed<number>;
                active: ko.Observable<boolean>;
                activateEditor(viewModel: any, e: any): void;
                static setText(cells: {
                    text: string;
                }[], textAlignment: string, rtl: boolean, text: string, rowsCount: number, colsCount: number): void;
            }
            interface IBounds {
                left: number;
                top: number;
                width: number;
                height: number;
                offset: {
                    x: number;
                    y: number;
                };
            }
            enum ImageAlignment {
                TopLeft = 1,
                TopCenter = 2,
                TopRight = 3,
                MiddleLeft = 4,
                MiddleCenter = 5,
                MiddleRight = 6,
                BottomLeft = 7,
                BottomCenter = 8,
                BottomRight = 9
            }
            enum ImageSizeMode {
                Normal = 0,
                StretchImage = 1,
                ZoomImage = 4,
                Squeeze = 5
            }
            interface IImageSourceBrickData {
                image: string;
                imageType: string;
            }
            interface IImageBrickData extends IImageSourceBrickData {
                alignment: ImageAlignment;
                sizeMode: ImageSizeMode;
            }
            interface IEditingFieldModel {
                id: string;
                groupID: string;
                readOnly: boolean;
                editorName: string;
                editValue: any | IImageBrickData;
                htmlValue: string;
                pageIndex: number;
                brickIndeces: string;
                type: string;
                bounds: IBounds;
                brickOptions: {
                    rtl: boolean;
                    rtlLayout: boolean;
                    formatString: string;
                    wordWrap: boolean;
                    style: string;
                    checkBoxBounds?: IBounds;
                    characterCombBounds?: IBounds[];
                    checkBoxGlyphOptions?: {
                        customGlyphs: {
                            key: number;
                            value: IImageSourceBrickData;
                        }[];
                        glyphStyle: Reporting.Viewer.Editing.GlyphStyle;
                    };
                };
            }
            interface IEditingFieldViewModel {
                template: string;
                field: EditingField;
                dispose?: () => void;
            }
            interface IEditingFieldHtmlProvider {
                getEditingFieldHtml: (value: string, editingFieldIndex: number) => JQueryPromise<string>;
            }
            class EditingField {
                protected _model: IEditingFieldModel;
                private _needToUseHtml;
                private _index;
                private _htmlProvider;
                constructor(model: IEditingFieldModel, index: number, htmlProvider: IEditingFieldHtmlProvider);
                private _refreshHtmlValue;
                editingFieldChanged(field: EditingField, oldVal: any, newVal: any): any;
                readOnly: ko.Observable<boolean> | ko.Computed<boolean>;
                modelValue: ko.Observable | ko.Computed;
                editValue: ko.Computed<any>;
                _editorValue: ko.Observable | ko.Computed;
                htmlValue: ko.Observable<string> | ko.Computed<string>;
                editorName(): string;
                id(): string;
                groupID(): string;
                pageIndex(): number;
                type(): string;
                model(): IEditingFieldModel;
                createViewModel(zoom: ko.Observable<number> | ko.Computed<number>, pageWidth: number, pageHeight: number, editingFieldsProvider: () => EditingField[], bounds: IBounds): IEditingFieldViewModel;
            }
        }
        module Export {
            module Metadata {
                var rtfExportModeMergedPreview: Analytics.Utils.ISerializationInfo;
                var docxExportModeMergedPreview: Analytics.Utils.ISerializationInfo;
                function excludeModesForMergedDocuments(val: string): ko.Observable<string>;
                function excludeDifferentFilesMode(val: string): ko.Observable<string>;
                var htmlExportModePreviewBase: Analytics.Utils.ISerializationInfo;
                var htmlExportModePreview: Analytics.Utils.ISerializationInfo;
                var htmlExportModeMergedPreview: Analytics.Utils.ISerializationInfo;
                var xlsExportModePreviewBase: Analytics.Utils.ISerializationInfo;
                var xlsExportModePreview: Analytics.Utils.ISerializationInfo;
                var xlsExportModeMergedPreview: Analytics.Utils.ISerializationInfo;
                var imageExportModePreviewBase: Analytics.Utils.ISerializationInfo;
                var imageExportModePreview: Analytics.Utils.ISerializationInfo;
                var imageExportModeMergedPreview: Analytics.Utils.ISerializationInfo;
                var xlsxExportModePreviewBase: Analytics.Utils.ISerializationInfo;
                var xlsxExportModePreview: Analytics.Utils.ISerializationInfo;
                var xlsxExportModeMergedPreview: Analytics.Utils.ISerializationInfo;
            }
            class CsvExportOptionsPreview extends Reporting.Export.CsvExportOptions {
                static from(model: any, serializer?: Analytics.Utils.IModelSerializer): CsvExportOptionsPreview;
                isPropertyVisible(name: string): boolean;
                isPropertyDisabled(name: string): boolean;
            }
            class DocxExportOptionsPreview extends Reporting.Export.DocxExportOptions {
                static toJson(value: any, serializer: any, refs: any): any;
                getInfo(): Analytics.Utils.ISerializationInfoArray;
                _getVariableInfo(): Analytics.Utils.ISerializationInfoArray;
            }
            class DocxExportOptionsMergedPreview extends DocxExportOptionsPreview {
                _getVariableInfo(): Analytics.Utils.ISerializationInfoArray;
                isPropertyDisabled(name: string): boolean;
                constructor(model: any, serializer: any);
            }
            class HtmlExportOptionsPreview extends Reporting.Export.HtmlExportOptions {
                static toJson(value: any, serializer: any, refs: any): any;
                getInfo(): Analytics.Utils.ISerializationInfoArray;
                _getVariableInfo(): Analytics.Utils.ISerializationInfoArray;
            }
            class HtmlExportOptionsMergedPreview extends HtmlExportOptionsPreview {
                _getVariableInfo(): Analytics.Utils.ISerializationInfoArray;
                isPropertyDisabled(name: string): boolean;
                constructor(model: any, serializer: any);
            }
            class ImageExportOptionsPreview extends Reporting.Export.ImageExportOptions {
                static toJson(value: any, serializer: any, refs: any): any;
                getInfo(): Analytics.Utils.ISerializationInfoArray;
                _getVariableInfo(): Analytics.Utils.ISerializationInfoArray;
            }
            class ImageExportOptionsMergedPreview extends ImageExportOptionsPreview {
                _getVariableInfo(): Analytics.Utils.ISerializationInfoArray;
                isPropertyDisabled(name: string): boolean;
                constructor(model: any, serializer: any);
            }
            class MhtExportOptionsPreview extends Reporting.Export.MhtExportOptions {
                static toJson(value: any, serializer: any, refs: any): any;
                getInfo(): Analytics.Utils.ISerializationInfoArray;
                _getVariableInfo(): Analytics.Utils.ISerializationInfoArray;
            }
            class MhtExportOptionsMergedPreview extends MhtExportOptionsPreview {
                _getVariableInfo(): Analytics.Utils.ISerializationInfoArray;
                isPropertyDisabled(name: string): boolean;
                constructor(model: any, serializer: any);
            }
            class RtfExportOptionsPreview extends Reporting.Export.RtfExportOptions {
                static toJson(value: any, serializer: any, refs: any): any;
                getInfo(): Analytics.Utils.ISerializationInfoArray;
                _getVariableInfo(): Analytics.Utils.ISerializationInfoArray;
            }
            class RtfExportOptionsMergedPreview extends RtfExportOptionsPreview {
                _getVariableInfo(): Analytics.Utils.ISerializationInfoArray;
                isPropertyDisabled(name: string): boolean;
                constructor(model: any, serializer: any);
            }
            class XlsExportOptionsPreview extends Reporting.Export.XlsExportOptions {
                static toJson(value: any, serializer: any, refs: any): any;
                getInfo(): Analytics.Utils.ISerializationInfoArray;
                _getVariableInfo(): Analytics.Utils.ISerializationInfoArray;
            }
            class XlsExportOptionsMergedPreview extends XlsExportOptionsPreview {
                _getVariableInfo(): Analytics.Utils.ISerializationInfoArray;
                isPropertyDisabled(name: string): boolean;
                constructor(model: any, serializer: any);
            }
            class XlsxExportOptionsPreview extends Reporting.Export.XlsxExportOptions {
                static toJson(value: any, serializer: any, refs: any): any;
                getInfo(): Analytics.Utils.ISerializationInfoArray;
                _getVariableInfo(): Analytics.Utils.ISerializationInfoArray;
            }
            class XlsxExportOptionsMergedPreview extends XlsxExportOptionsPreview {
                _getVariableInfo(): Analytics.Utils.ISerializationInfoArray;
                isPropertyDisabled(name: string): boolean;
                constructor(model: any, serializer: any);
            }
            class ExportOptionsPreview extends Reporting.Export.ExportOptions {
                _generateFromFunction(exportType: any): (model: any, serializer: any) => any;
                _generateInfo(): Analytics.Utils.ISerializationInfoArray;
                hasSensitiveData(): boolean;
                getInfo(): Analytics.Utils.ISerializationInfoArray;
            }
            class ExportOptionsMergedPreview extends ExportOptionsPreview {
                _generateInfo(): Analytics.Utils.ISerializationInfoArray;
            }
            class ExportOptionsModel extends Analytics.Utils.Disposable {
                private _reportPreview;
                constructor(reportPreview: ReportPreview);
                _getExportFormatItems(): Array<{
                    text: string;
                    format: string;
                }>;
                _exportDocumentByFormat(format: any): void;
                getActions(context: any): any[];
                dispose(): void;
                actions: any[];
                tabInfo: DevExpress.Analytics.Utils.TabInfo;
            }
        }
        class ReportPreview extends Analytics.Utils.Disposable {
            predefinedZoomLevels: ko.ObservableArray<number>;
            _pageWidth: ko.Observable<number>;
            _pageHeight: ko.Observable<number>;
            _pageBackColor: ko.Observable<string>;
            _currentReportId: ko.Observable<string>;
            _currentReportUrl: ko.Observable<string>;
            _currentDocumentId: ko.Observable<string>;
            _unifier: ko.Observable<string>;
            _currentOperationId: ko.Observable<string>;
            _stopBuildRequests: {
                [key: string]: boolean;
            };
            _closeDocumentRequests: {
                [key: string]: boolean;
            };
            _editingFields: ko.Observable<Editing.EditingField[]>;
            private _openReportOperationDeferred;
            _startBuildOperationId: string;
            private _editingValuesSubscriptions;
            private _drillDownState;
            private _sortingState;
            private _sortingProcessor;
            private _doDrillDown;
            private _doSorting;
            private _timeouts;
            private _deferreds;
            dispose(): void;
            removeEmptyPages(all?: boolean): void;
            private _initialize;
            createPage(pageIndex: number, processClick?: (target: Utils.IBrickNode) => void, loading?: ko.Observable<boolean>): Internal.PreviewPage;
            private _cleanTabInfo;
            private _clearReportInfo;
            private _window;
            private _initExportWindow;
            private _export;
            private _safelyRunWindowOpen;
            createBrickClickProcessor(cyclePageIndex: number): (brick: Utils.IBrickNode, e?: JQueryEventObject) => void;
            constructor(handlerUri?: string, previewRequestWrapper?: Internal.PreviewRequestWrapper, previewHandlersHelper?: Internal.PreviewHandlersHelper, callbacks?: Utils.IPreviewCustomizationHandler, rtl?: boolean);
            openReport(reportName: string): JQueryPromise<Utils.IPreviewInitialize>;
            drillThrough(customData?: string, closeCurrentReport?: boolean): JQueryPromise<Utils.IPreviewInitialize>;
            initialize(initializeDataPromise: JQueryPromise<Utils.IPreviewInitialize>): JQueryPromise<Utils.IPreviewInitialize>;
            private _deserializeExportOptions;
            deactivate(): void;
            startBuild(): JQueryPromise<boolean>;
            updateExportStatus(progress: number): void;
            customDocumentOperation(customData?: string, hideMessageFromUser?: boolean): JQueryPromise<Utils.IDocumentOperationResult>;
            _initializeStartBuild(): boolean;
            _startBuildRequest(): JQueryPromise<boolean>;
            getExportStatus(operationId: string): JQueryPromise<Internal.IExportProgressStatus>;
            getExportResult(operationId: string, inlineDisposition: boolean, token?: string, printable?: boolean, uri?: string): void;
            getBuildStatus(documentId: string): JQueryPromise<Internal.IDocumentBuildStatus>;
            getDoGetBuildStatusFunc(): (documentId: string) => void;
            getDocumentData(documentId: any): void;
            exportDocumentTo(format: string, inlineResult?: boolean): void;
            printDocument(pageIndex?: number): void;
            stopBuild(documentId?: string): void;
            closeDocument(documentId?: string): void;
            closeReport(reportId?: string): void;
            goToPage(pageIndex: number, forcePageChanging?: boolean, throttle?: number): void;
            private _goToPageTimer;
            getSelectedContent(): string;
            createEditingField(item: any, index: any): Editing.EditingField;
            rtlReport: ko.Observable<boolean>;
            rtlViewer: boolean;
            previewHandlersHelper: Internal.PreviewHandlersHelper;
            currentPage: ko.Observable<Internal.PreviewPage>;
            originalParametersInfo: ko.Observable<Parameters.IReportParametersInfo>;
            pageIndex: ko.Observable<number>;
            showMultipagePreview: ko.Observable<boolean>;
            documentMap: ko.Observable<Internal.IBookmarkNode>;
            exportOptionsModel: ko.Observable<Export.ExportOptionsPreview>;
            pageLoading: ko.Observable<boolean>;
            documentBuilding: ko.Observable<boolean>;
            progressBar: Internal.IProgressHandler;
            pages: ko.ObservableArray<Internal.PreviewPage>;
            customProcessBrickClick: (pageNamber: number, brick: Utils.IBrickNode, defaultHandler: () => void) => boolean;
            brickClickDocumentMapHandler: (navigationBrick: Utils.IBrickNodeNavigation) => void;
            editingFieldChanged: (field: Editing.EditingField, oldVal: any, newVal: any) => void;
            customizeExportOptions: (options: Utils.IPreviewExportOptionsCustomizationArgs) => void;
            isAutoFit: ko.Observable<boolean>;
            autoFitBy: ko.Observable<number>;
            exportDisabled: ko.PureComputed<boolean>;
            _zoom: ko.Observable<number>;
            zoom: ko.PureComputed<any>;
            editingFieldsProvider: () => Editing.EditingField[];
            _currentPageText: ko.PureComputed<any>;
            _getErrorMessage(jqXHR: any): any;
            _processError(error: string, jqXHR?: any, showForUser?: boolean): void;
            _raiseOnSizeChanged: () => void;
            previewSize: ko.Observable<number>;
            onSizeChanged: ko.Observable<any>;
            previewVisible: ko.Observable<boolean>;
            editingFieldsHighlighted: ko.Observable<boolean>;
            canSwitchToDesigner: boolean;
            allowURLsWithJSContent: boolean;
            requestWrapper: Internal.PreviewRequestWrapper;
            zoomStep: ko.Observable<number>;
            emptyDocumentCaption(): any;
            readonly reportId: string;
            readonly reportUrl: string;
            readonly documentId: string;
            exportOptionsTabVisible: ko.Observable<boolean>;
        }
        var MobilePreviewElements: {
            Surface: string;
            Search: string;
            Pages: string;
            MobileActions: string;
            Parameters: string;
        };
        module Mobile {
            module Internal {
                function updatePreviewContentSizeMobile(previewWrapperSize: ko.Observable<{
                    width: number;
                    height: number;
                }>, $root: JQuery): () => void;
                function updateMobilePreviewActionsPosition($actions: JQuery, $viewer: JQuery, $window: JQuery): (viewer: Element) => void;
                var slowdownDisctanceFactor: number;
                var minScale: number;
                class EventProcessor {
                    element: any;
                    slideOptions: ISlideOptions;
                    private _direction;
                    private _startingPositionX;
                    private _startingPositionY;
                    private _getFirstPageOffset;
                    getDirection(x?: any, y?: any): {
                        vertical: boolean;
                        horizontal: boolean;
                        scrollDown: boolean;
                    };
                    setPosition(x: any, y: any): void;
                    initialize(x: number, y: number): void;
                    start(e: JQueryEventObject): void;
                    move(e: JQueryEventObject): void;
                    end(e: JQueryEventObject): void;
                    constructor(element: any, slideOptions: ISlideOptions);
                    applySearchAnimation(value: any): void;
                    isLeftMove: boolean;
                    isRightMove: boolean;
                    latestY: number;
                    latestX: number;
                    $window: JQuery;
                    $element: JQuery;
                    $gallery: JQuery;
                    $galleryblocks: JQuery;
                    $body: JQuery;
                    firstMobilePageOffset: {
                        left: number;
                        top: number;
                    };
                }
                class MobilePaginator {
                    constructor(reportPreview: MobileReportPreview, gallery: GalleryModel);
                    visible: ko.Observable<boolean>;
                    text: ko.Observable<string> | ko.Computed<string>;
                }
                interface IMobileSearchPanel {
                    searchPanelVisible: ko.Observable<boolean> | ko.Computed<boolean>;
                    height: ko.Observable<number> | ko.Computed<number>;
                    editorVisible: ko.Observable<boolean> | ko.Computed<boolean>;
                }
                class MobileSearchViewModel extends Viewer.Internal.SearchViewModel implements IMobileSearchPanel {
                    static maxHeight: number;
                    focusEditor(event: any): void;
                    private _killSubscription;
                    private _updateBricks;
                    constructor(reportPreview: MobileReportPreview, gallery: GalleryModel);
                    updatePagesInBlocks(blocks: Array<IGalleryItemBlock>): void;
                    stopSearching(): void;
                    startSearch(): void;
                    editorVisible: ko.Observable<boolean> | ko.Computed<boolean>;
                    height: ko.Observable<number>;
                    searchPanelVisible: ko.Observable<boolean> | ko.Computed<boolean>;
                    enabled: ko.Observable<boolean> | ko.Computed<boolean>;
                }
                class SearchBarModel extends Analytics.Utils.Disposable {
                    private viewModel;
                    constructor(viewModel: MobileSearchViewModel, element: HTMLDivElement, $searchText: JQuery);
                    dispose(): void;
                }
                interface IGalleryItemBlock {
                    repaint?: boolean;
                    page: Viewer.Internal.PreviewPage;
                    classSet?: any;
                    visible?: boolean;
                    position: ko.Observable<IAbsolutePosition> | ko.Computed<IAbsolutePosition>;
                }
                interface IAbsolutePosition {
                    left: number;
                    top: number;
                    height: number;
                    width: number;
                }
                interface IGalleryItem {
                    blocks: ko.ObservableArray<IGalleryItemBlock>;
                    realIndex?: number;
                }
                class GalleryModel {
                    preview: MobileReportPreview;
                    private previewWrapperSize;
                    private _spacing;
                    private _animationTimeout;
                    private _createBlock;
                    constructor(preview: MobileReportPreview, previewWrapperSize: ko.Observable<{
                        width: number;
                        height: number;
                    }>);
                    updatePagesVisible(preview: MobileReportPreview): void;
                    updateCurrentBlock(): void;
                    updateContent(preview: MobileReportPreview, pagesCount: number): void;
                    updateBlockPositions(blocks: IGalleryItemBlock[], visible: any): void;
                    updateStartBlocks(galleryItem: IGalleryItem, pages: Viewer.Internal.PreviewPage[]): number;
                    updateLastBlocks(galleryItem: IGalleryItem, pages: Viewer.Internal.PreviewPage[]): number;
                    updateBlocks(galleryItem: IGalleryItem, pagesCount: number, preview: MobileReportPreview, index: any, useAnimation?: boolean): void;
                    changePage(preview: MobileReportPreview): void;
                    repaint: ko.Observable<{}>;
                    contentSize: ko.Observable<{
                        width: string;
                        height: string;
                    }> | ko.Computed<{
                        width: string;
                        height: string;
                    }>;
                    horizontal: ko.Observable<number> | ko.Computed<number>;
                    vertical: ko.Observable<number> | ko.Computed<number>;
                    pageCount: number;
                    isAnimated: ko.Observable<boolean> | ko.Computed<boolean>;
                    items: ko.ObservableArray<IGalleryItem>;
                    currentBlockText: ko.Observable<string> | ko.Computed<string>;
                    selectedIndexReal: ko.Observable<number>;
                    loopEnabled: ko.Computed<boolean>;
                    selectedIndex: ko.Observable<number>;
                    animationEnabled: ko.Observable<boolean> | ko.Computed<boolean>;
                    swipeRightEnable: ko.Computed<boolean>;
                    swipeLeftEnable: ko.Computed<boolean>;
                }
                interface BlockItem {
                    element: JQuery;
                    left: number;
                }
                class dxGalleryReportPreview extends dxGallery {
                    private _animationClassName;
                    private blockItems;
                    private currentBlockItem;
                    private gallery;
                    private nextBlockItem;
                    private initializeBlockItems;
                    constructor(element: any, options?: any);
                    repaint(): void;
                    _swipeStartHandler(e: any): void;
                    _getNextIndex(offset: any): number;
                    _setSwipeAnimation(element: BlockItem, difference: any, offset: any, right: boolean): void;
                    _addAnimation(item: any): void;
                    _restoreDefault(item: BlockItem): void;
                    _getItem(index: any, loopTest: any): BlockItem;
                    _swipeUpdateHandler(e: any): void;
                    _swipeEndHandler(e: any): void;
                    _endSwipe(): void;
                }
                class MobilePreviewPage extends Viewer.Internal.PreviewPage {
                    constructor(preview: Viewer.Internal.IPreviewPageOwner, pageIndex: number, processClick?: (target: Viewer.Utils.IBrickNode) => void, loading?: ko.Observable<boolean>);
                    maxZoom: number;
                }
                interface IMobileActionContent {
                    name: string;
                    data: any;
                }
                interface IMobileAction {
                    imageClassName: string;
                    clickAction: () => void;
                    visible?: any;
                    content?: IMobileActionContent;
                }
                class MobileActionList {
                    actions: IMobileAction[];
                    constructor(actions: IMobileAction[]);
                    visible: ko.Observable<boolean>;
                }
                interface IPreviewActionsMobileOptions {
                    reportPreview: MobileReportPreview;
                    exportModel: Viewer.Export.ExportOptionsModel;
                    parametersModel: Viewer.Parameters.PreviewParametersViewModel;
                    searchModel: MobileSearchViewModel;
                    exportTypes: ko.ObservableArray<{
                        text: string;
                        format: string;
                    }>;
                    callbacks: Viewer.Utils.IPreviewCustomizationHandler;
                }
                function getPreviewActionsMobile(options: IPreviewActionsMobileOptions): MobileActionList;
                interface IDesignerModelPart {
                    id: string;
                    templateName: string;
                    model: any;
                }
                interface IMobileDesignerModel {
                    rootStyle: any;
                    reportPreview: MobileReportPreview;
                    parametersModel: Viewer.Parameters.PreviewParametersViewModel;
                    exportModel: Viewer.Export.ExportOptionsModel;
                    searchModel: MobileSearchViewModel;
                    rtl: boolean;
                    gallery?: GalleryModel;
                    paginator?: MobilePaginator;
                    brickEventsDisabled?: ko.Observable<boolean> | ko.Computed<boolean>;
                    slideOptions?: ISlideOptions;
                    parts?: Array<IDesignerModelPart>;
                    updateSurfaceSize?: () => void;
                    availableFormats: ko.ObservableArray<{
                        text: string;
                        format: string;
                    }>;
                }
                function createMobilePreview(element: Element, callbacks: Viewer.Utils.IPreviewCustomizationHandler, parametersInfo?: Viewer.Parameters.IReportParametersInfo, handlerUri?: string, previewVisible?: boolean, applyBindings?: boolean, allowURLsWithJSContent?: boolean, mobileModeSettings?: Viewer.Utils.IMobileModeSettings): IMobileDesignerModel;
            }
            interface ISlideOptions {
                disabled: ko.Observable<boolean> | ko.Computed<boolean>;
                readerMode: boolean;
                animationSettings: IPreviewAnimationSettings;
                searchPanel: Internal.IMobileSearchPanel;
                swipeEnabled: ko.Observable<boolean> | ko.Computed<boolean>;
                reachedTop: ko.Observable<boolean> | ko.Computed<boolean>;
                reachedLeft: ko.Observable<boolean> | ko.Computed<boolean>;
                reachedRight: ko.Observable<boolean> | ko.Computed<boolean>;
                scrollAvailable: ko.Observable<boolean> | ko.Computed<boolean>;
                zoomUpdating: ko.Observable<boolean> | ko.Computed<boolean>;
                galleryIsAnimated: ko.Observable<boolean> | ko.Computed<boolean>;
                autoFitBy: ko.Observable<number> | ko.Computed<number>;
                topOffset: ko.Observable<number> | ko.Computed<number>;
                brickEventsDisabled: ko.Observable<boolean> | ko.Computed<boolean>;
            }
            interface IZoomOptions {
                zoomUpdating: ko.Observable<boolean> | ko.Computed<boolean>;
                zoom: ko.Observable<number> | ko.Computed<number>;
            }
            interface IPreviewAnimationSettings {
                zoomEnabled: ko.Observable<boolean> | ko.Computed<boolean>;
                swipeEnabled: ko.Observable<boolean> | ko.Computed<boolean>;
            }
            class MobileReportPreview extends ReportPreview {
                constructor(handlerUri?: string, previewRequestWrapper?: Viewer.Internal.PreviewRequestWrapper, previewHandlersHelper?: Viewer.Internal.PreviewHandlersHelper, callbacks?: Viewer.Utils.IPreviewCustomizationHandler, rtl?: boolean, mobileSettings?: Viewer.Utils.IMobileModeSettings);
                createPage(pageIndex: number, processClick?: (target: Viewer.Utils.IBrickNode) => void, loading?: ko.Observable<boolean>): Internal.MobilePreviewPage;
                createBrickClickProcessor(cyclePageIndex: number): (brick: Utils.IBrickNode) => void;
                _hasActiveEditingFields(): boolean;
                showActions(s: MobileReportPreview): void;
                onSlide(e: any): void;
                availablePages: ko.Observable<number[]>;
                visiblePages: ko.Computed<Viewer.Internal.PreviewPage[]>;
                goToPage(pageIndex: any, forcePage?: any): void;
                getScrollViewOptions(): {
                    onUpdated: (e: any) => void;
                    direction: string;
                    pushBackValue: number;
                    bounceEnabled: boolean;
                };
                setScrollReached(e: any): void;
                readerMode: boolean;
                animationSettings: IPreviewAnimationSettings;
                topOffset: ko.Observable<number>;
                previewWrapperSize: ko.Observable<{
                    width: number;
                    height: number;
                }>;
                searchPanelVisible: ko.Observable<boolean>;
                interactionDisabled: ko.Observable<boolean> | ko.Computed<boolean>;
                actionsVisible: ko.Observable<boolean>;
                scrollReachedLeft: ko.Observable<boolean>;
                scrollReachedRight: ko.Observable<boolean>;
                scrollReachedTop: ko.Observable<boolean>;
                scrollReachedBottom: ko.Observable<boolean>;
                zoomUpdating: ko.Observable<boolean>;
                mobileZoom: ko.Computed<number>;
            }
            module Internal {
                class ParametersPopupModel extends Analytics.Utils.Disposable {
                    parametersModel: Parameters.PreviewParametersViewModel;
                    private _reportPreview;
                    private _parametersButtonContaner;
                    private _submit;
                    private _reset;
                    private _cancel;
                    constructor(parametersModel: Parameters.PreviewParametersViewModel, _reportPreview: MobileReportPreview);
                    initVisibilityIcons(): void;
                    cacheElementContent(element: any): void;
                    dispose(): void;
                    showIcons: ko.Observable<boolean>;
                    visible: ko.Observable<boolean> | ko.Computed<boolean>;
                    model: Parameters.PreviewParametersViewModel;
                    actionButtons: any;
                    actionIcons: any;
                    cancelDisabled: ko.Computed<boolean>;
                }
            }
        }
        class JSReportViewer {
            private _previewModel;
            previewModel: any;
            constructor(_previewModel: ko.Observable<any>);
            previewExists(): any;
            GetReportPreview(): any;
            GetPreviewModel(): any;
            GetParametersModel(): any;
            OpenReport(reportName: any): any;
            Print(pageIndex: any): any;
            ExportTo(format: any, inlineResult: any): void;
            GetCurrentPageIndex(): any;
            GoToPage(pageIndex: any): void;
            Close(): void;
            ResetParameters(): void;
            StartBuild(): any;
            UpdateLocalization(localization: any): void;
            AdjustControlCore(): void;
        }
        interface IReportViewerOptions {
            viewerModel?: any;
            reportPreview?: any;
            callbacks?: Utils.IPreviewCustomizationHandler;
            parts?: any[];
            requestOptions?: {
                host?: string;
                invokeAction: string;
                getLocalizationAction?: string;
            };
            documentId?: string;
            reportId?: string;
            reportUrl?: any;
        }
        class JSReportViewerBinding extends DevExpress.Analytics.Internal.JSDesignerBindingCommon<JSReportViewer> {
            private _callbacks;
            private _initializeEvents;
            private _initializeCallbacks;
            private _createModel;
            private _applyBindings;
            constructor(_options: IReportViewerOptions, customEventRaiser?: any);
            applyBindings(element: any): void;
        }
    }
}

export default DevExpress;