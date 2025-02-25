/**
* DevExpress HTML/JS Query Builder (dx-querybuilder.d.ts)
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


import DXImport from './dx-analytics-core';
declare module DevExpress.Analytics.Diagram {
    var name: DXImport.Analytics.Utils.ISerializationInfo;
    var text: DXImport.Analytics.Utils.ISerializationInfo;
    var size: DXImport.Analytics.Utils.ISerializationInfo;
    var location: DXImport.Analytics.Utils.ISerializationInfo;
    var sizeLocation: DXImport.Analytics.Utils.ISerializationInfoArray;
    var unknownSerializationsInfo: DXImport.Analytics.Utils.ISerializationInfoArray;
    class ConnectingPointDragHandler extends DXImport.Analytics.Internal.DragDropHandler {
        constructor(surface: ko.Observable<DXImport.Analytics.Elements.ISurfaceContext> | ko.Computed<DXImport.Analytics.Elements.ISurfaceContext>, selection: DXImport.Analytics.Internal.SurfaceSelection, undoEngine: ko.Observable<DXImport.Analytics.Utils.UndoEngine> | ko.Computed<DXImport.Analytics.Utils.UndoEngine>, snapHelper: DXImport.Analytics.Internal.SnapLinesHelper, dragHelperContent: DXImport.Analytics.Internal.DragHelperContent);
        startDrag(control: DXImport.Analytics.Internal.ISelectionTarget): void;
        drag(event: JQueryEventObject, ui: JQueryUI.DraggableEventUIParams): void;
        doStopDrag(): void;
        startConnectingPoint: ConnectingPointSurface;
        newConnector: ConnectorViewModel;
        readonly newConnectorSurface: ConnectorSurface;
    }
    class ConnectionPointDragHandler extends DXImport.Analytics.Internal.DragDropHandler {
        constructor(surface: ko.Observable<DXImport.Analytics.Elements.ISurfaceContext> | ko.Computed<DXImport.Analytics.Elements.ISurfaceContext>, selection: DXImport.Analytics.Internal.SurfaceSelection, undoEngine: ko.Observable<DXImport.Analytics.Utils.UndoEngine> | ko.Computed<DXImport.Analytics.Utils.UndoEngine>, snapHelper: DXImport.Analytics.Internal.SnapLinesHelper, dragHelperContent: DXImport.Analytics.Internal.DragHelperContent);
        startDrag(control: DXImport.Analytics.Internal.ISelectionTarget): void;
        drag(event: JQueryEventObject, ui: JQueryUI.DraggableEventUIParams): void;
        doStopDrag(): void;
        currentConnectionPoint: ConnectionPointSurface;
    }
    class DiagramElementBaseViewModel extends DXImport.Analytics.Elements.ElementViewModel {
        getControlFactory(): DXImport.Analytics.Utils.ControlsFactory;
        constructor(control: any, parent: DXImport.Analytics.Elements.ElementViewModel, serializer?: DXImport.Analytics.Utils.ModelSerializer);
        size: DXImport.Analytics.Elements.Size;
        location: DXImport.Analytics.Elements.Point;
    }
    class DiagramElementViewModel extends DiagramElementBaseViewModel {
        constructor(control: any, parent: DXImport.Analytics.Elements.ElementViewModel, serializer?: DXImport.Analytics.Utils.ModelSerializer);
        connectingPoints: ko.ObservableArray<ConnectingPointViewModel>;
        text: ko.Observable<string> | ko.Computed<string>;
        type: ko.Observable<string> | ko.Computed<string>;
    }
    var diagramElementSerializationInfo: DXImport.Analytics.Utils.ISerializationInfoArray;
    class DiagramElementBaseSurface<M extends DiagramElementBaseViewModel> extends DXImport.Analytics.Elements.SurfaceElementBase<M> {
        static _unitProperties: DXImport.Analytics.Internal.IUnitProperties<DiagramElementBaseViewModel>;
        constructor(control: M, context: DXImport.Analytics.Elements.ISurfaceContext, unitProperties: DXImport.Analytics.Internal.IUnitProperties<M>);
        template: string;
        selectiontemplate: string;
        contenttemplate: string;
        margin: ko.Observable<number>;
        positionWidthWithoutMargins: ko.Computed<number>;
        positionLineHeightWithoutMargins: ko.Computed<number>;
    }
    class DiagramElementSurface extends DiagramElementBaseSurface<DiagramElementViewModel> {
        constructor(control: DiagramElementViewModel, context: DXImport.Analytics.Elements.ISurfaceContext);
        _getChildrenHolderName(): string;
        contenttemplate: string;
    }
    class DiagramViewModel extends DiagramElementBaseViewModel {
        getInfo(): DXImport.Analytics.Utils.ISerializationInfoArray;
        constructor(diagramSource: any);
        controls: ko.ObservableArray<DiagramElementViewModel>;
        name: ko.Observable<string> | ko.Computed<string>;
        pageWidth: ko.Observable<number> | ko.Computed<number>;
        pageHeight: ko.Observable<number> | ko.Computed<number>;
        margins: DXImport.Analytics.Elements.Margins;
    }
    var margins: DXImport.Analytics.Utils.ISerializationInfo;
    var pageWidth: DXImport.Analytics.Utils.ISerializationInfo;
    var pageHeight: DXImport.Analytics.Utils.ISerializationInfo;
    var diagramSerializationsInfo: DXImport.Analytics.Utils.ISerializationInfoArray;
    class DiagramSurface extends DXImport.Analytics.Elements.SurfaceElementBase<DiagramViewModel> implements DXImport.Analytics.Internal.ISelectionTarget, DXImport.Analytics.Elements.ISurfaceContext {
        static _unitProperties: DXImport.Analytics.Internal.IUnitProperties<DiagramViewModel>;
        constructor(diagram: DiagramViewModel, zoom?: ko.Observable<number>);
        measureUnit: ko.Observable<string>;
        dpi: ko.Observable<number>;
        zoom: ko.Observable<number> | ko.Computed<number>;
        controls: ko.ObservableArray<DiagramElementSurface>;
        allowMultiselect: boolean;
        focused: ko.Observable<boolean>;
        selected: ko.Observable<boolean>;
        underCursor: ko.Observable<DXImport.Analytics.Internal.IHoverInfo>;
        checkParent(surfaceParent: DXImport.Analytics.Internal.ISelectionTarget): boolean;
        parent: DXImport.Analytics.Internal.ISelectionTarget;
        templateName: string;
        getChildrenCollection(): ko.ObservableArray<any>;
        margins: DXImport.Analytics.Elements.IMargins;
    }
    class ConnectionPointViewModel extends DiagramElementBaseViewModel {
        constructor(control: any, parent: ConnectorViewModel, serializer?: DXImport.Analytics.Utils.ModelSerializer);
        location: DXImport.Analytics.Elements.Point;
        connectingPoint: ko.Observable<IConnectingPoint>;
    }
    var connectionPointSerializationInfo: DXImport.Analytics.Utils.ISerializationInfoArray;
    class ConnectionPointSurface extends DXImport.Analytics.Elements.SurfaceElementBase<ConnectionPointViewModel> {
        static _unitProperties: DXImport.Analytics.Internal.IUnitProperties<ConnectionPointViewModel>;
        constructor(control: ConnectionPointViewModel, context: DXImport.Analytics.Elements.ISurfaceContext);
        template: string;
        selectiontemplate: string;
        relativeX: ko.Observable<number> | ko.Computed<number>;
        relativeY: ko.Observable<number> | ko.Computed<number>;
        container(): DiagramSurface;
    }
    class ConnectorViewModel extends DiagramElementBaseViewModel {
        static MIN_LINE_THICKNESS: number;
        getX(): number;
        getY(): number;
        getWidth(): number;
        getHeight(): number;
        constructor(control: any, parent: DXImport.Analytics.Elements.ElementViewModel, serializer?: DXImport.Analytics.Utils.ModelSerializer);
        startPoint: ko.Observable<ConnectionPointViewModel> | ko.Computed<ConnectionPointViewModel>;
        endPoint: ko.Observable<ConnectionPointViewModel> | ko.Computed<ConnectionPointViewModel>;
    }
    class ConnectorSurface extends DiagramElementBaseSurface<ConnectorViewModel> {
        constructor(control: ConnectorViewModel, context: DXImport.Analytics.Elements.ISurfaceContext);
        template: string;
        selectiontemplate: string;
        startPoint: ko.Observable<ConnectionPointSurface> | ko.Computed<ConnectionPointSurface>;
        endPoint: ko.Observable<ConnectionPointSurface> | ko.Computed<ConnectionPointSurface>;
    }
    class RoutedConnectorViewModel extends ConnectorViewModel {
        static GRID_SIZE: number;
        private _isUpdating;
        getX(): number;
        getY(): number;
        getWidth(): number;
        getHeight(): number;
        _fixPoint(point: DXImport.Analytics.Elements.IPoint, side: PointSide): void;
        _getStartPointSide(): PointSide;
        _getEndPointSide(): PointSide;
        private _getPower;
        private _getRatio;
        constructor(control: any, parent: DXImport.Analytics.Elements.ElementViewModel, serializer?: DXImport.Analytics.Utils.ModelSerializer);
        seriesNumber: ko.Observable<number>;
        routePoints: ko.Observable<DXImport.Analytics.Elements.IPoint[]>;
        freezeRoute: ko.Observable<boolean>;
        beginUpdate(): void;
        endUpdate(): void;
    }
    interface IRoutePoint {
        x: ko.Observable<number> | ko.Computed<number>;
        y: ko.Observable<number> | ko.Computed<number>;
        modelPoint: DXImport.Analytics.Elements.IPoint;
    }
    class RoutedConnectorSurface extends DiagramElementBaseSurface<RoutedConnectorViewModel> {
        private static _connectorsCount;
        private _connectorID;
        private _createRoutePoint;
        private _createRouteLineWrapper;
        private _updateRoutePoints;
        constructor(control: RoutedConnectorViewModel, context: DXImport.Analytics.Elements.ISurfaceContext);
        template: string;
        selectiontemplate: string;
        startPoint: ko.Observable<ConnectionPointSurface> | ko.Computed<ConnectionPointSurface>;
        endPoint: ko.Observable<ConnectionPointSurface> | ko.Computed<ConnectionPointSurface>;
        showArrow: ko.Observable<boolean> | ko.Computed<boolean>;
        isVisible: ko.Observable<boolean> | ko.Computed<boolean>;
        routePoints: ko.ObservableArray<IRoutePoint>;
        routePointsSet: ko.PureComputed<string>;
        routeLineWrappers: ko.PureComputed<any[]>;
        connectorID: () => number;
    }
    interface IConnectingPoint {
        location: DXImport.Analytics.Elements.IPoint;
        side: ko.Observable<PointSide> | ko.Computed<PointSide>;
    }
    class ConnectingPointViewModel extends DiagramElementBaseViewModel implements IConnectingPoint {
        constructor(control: any, parent: DiagramElementViewModel, serializer?: DXImport.Analytics.Utils.ModelSerializer);
        percentOffsetX: ko.Observable<number> | ko.Computed<number>;
        percentOffsetY: ko.Observable<number> | ko.Computed<number>;
        side: ko.PureComputed<PointSide>;
    }
    var connectingPointSerializationInfo: DXImport.Analytics.Utils.ISerializationInfoArray;
    class ConnectingPointSurface extends DiagramElementBaseSurface<ConnectingPointViewModel> {
        static _unitProperties: DXImport.Analytics.Internal.IUnitProperties<ConnectingPointViewModel>;
        constructor(control: ConnectingPointViewModel, context: DXImport.Analytics.Elements.ISurfaceContext);
        template: string;
        selectiontemplate: string;
        contenttemplate: string;
    }
    var controlsFactory: DXImport.Analytics.Utils.ControlsFactory;
    function registerControls(): void;
    var groups: DXImport.Analytics.Internal.GroupObject;
    function createDiagramDesigner(element: Element, diagramSource: ko.Observable<any>, localization?: any, rtl?: boolean): any;
    enum PointSide {
        East = 0,
        South = 1,
        North = 2,
        West = 3
    }
    function determineConnectingPoints<T extends {
        rightConnectionPoint: Diagram.IConnectingPoint;
        leftConnectionPoint: Diagram.IConnectingPoint;
    }>(startObject: T, endObject: T): {
        start: Diagram.IConnectingPoint;
        end: Diagram.IConnectingPoint;
    };
}

declare module DevExpress {
    module Analytics {
        module Data {
            interface IDBSchemaProvider extends DXImport.Analytics.Utils.IItemsProvider {
                getDbSchema: () => JQueryPromise<DBSchema>;
                getDbTable: (tableName: string) => JQueryPromise<DBTable>;
                getDbStoredProcedures: () => JQueryPromise<DBStoredProcedure[]>;
            }
            module Internal {
                function getDBSchemaCallback(requestWrapper: DevExpress.QueryBuilder.Utils.RequestWrapper, connection: SqlDataConnection, tables: DBTable[]): JQueryPromise<DBSchema>;
                function getDBStoredProceduresCallback(requestWrapper: DevExpress.QueryBuilder.Utils.RequestWrapper, connection: SqlDataConnection): JQueryPromise<DBStoredProcedure[]>;
            }
            class DBSchemaProvider extends DXImport.Analytics.Utils.Disposable implements IDBSchemaProvider {
                private _requestWrapper;
                private _dbSchema;
                private _dbStoredProceduresSchema;
                private _tables;
                private _tableRequests;
                connection: SqlDataConnection;
                private _getDBSchema;
                private _getDBSchemaCallback;
                private _getDBStoredProcedures;
                constructor(connection: SqlDataConnection, _requestWrapper?: QueryBuilder.Utils.RequestWrapper);
                getItems: (IPathRequest: any) => JQueryPromise<DXImport.Analytics.Utils.IDataMemberInfo[]>;
                getDbSchema(): JQueryPromise<DBSchema>;
                getDbStoredProcedures(): JQueryPromise<DBStoredProcedure[]>;
                getDbTable(tableName: string): JQueryPromise<DBTable>;
            }
            class DBStoredProcedure {
                name: string;
                arguments: DBStoredProcedureArgument[];
                constructor(model: any);
            }
            enum DBStoredProcedureArgumentDirection {
                In = 0,
                Out = 1,
                InOut = 2
            }
            class DBStoredProcedureArgument {
                name: string;
                type: DBColumnType;
                direction: DBStoredProcedureArgumentDirection;
                constructor(model: any);
            }
            class DBTable {
                name: string;
                columns: DBColumn[];
                isView: boolean;
                foreignKeys: DBForeignKey[];
                constructor(model: any);
            }
            class ResultSet {
                getInfo(): DXImport.Analytics.Utils.ISerializationInfoArray;
                static from(model: any, serializer?: DXImport.Analytics.Utils.IModelSerializer): ResultSet;
                static toJson(value: any, serializer: any, refs: any): {
                    "DataSet": any;
                };
                constructor(model: any, serializer?: DXImport.Analytics.Utils.IModelSerializer);
                tables: ko.ObservableArray<ResultTable>;
            }
            class ResultTable {
                getInfo(): DXImport.Analytics.Utils.ISerializationInfoArray;
                constructor(model: any, serializer?: DXImport.Analytics.Utils.IModelSerializer);
                tableName: ko.Observable<string> | ko.Computed<string>;
                columns: ko.ObservableArray<{
                    name: ko.Observable<string> | ko.Computed<string>;
                    propertyType: ko.Observable<string>;
                }>;
            }
            module Utils {
                var SqlQueryType: {
                    customSqlQuery: string;
                    tableQuery: string;
                    storedProcQuery: string;
                };
                var JsonSourceType: {
                    fileJsonSource: string;
                    customJsonSource: string;
                    uriJsonSource: string;
                };
                interface ISqlQueryViewModel extends DXImport.Analytics.Utils.ISerializableModel {
                    name: ko.Observable<string> | ko.Computed<string>;
                    parameters: ko.ObservableArray<DataSourceParameter>;
                    type: ko.Observable<string> | ko.Computed<string>;
                    parent: SqlDataSource;
                    generateName: () => string;
                }
            }
            module Internal {
                function generateQueryUniqueName(queries: Utils.ISqlQueryViewModel[], query: Utils.ISqlQueryViewModel): string;
            }
            enum JsonNodeType {
                Object = 0,
                Array = 1,
                Property = 2
            }
            class JsonNode {
                static from(model: any, serializer?: DXImport.Analytics.Utils.IModelSerializer): JsonNode;
                static toJsonNodes(value: JsonNode[], serializer: any, refs: any): any[];
                static toJsonNode(value: JsonNode, serializer: any, refs: any, recoursive?: boolean): any;
                getInfo(): DXImport.Analytics.Utils.ISerializationInfoArray;
                constructor(model: any, serializer?: DXImport.Analytics.Utils.IModelSerializer);
                name: ko.Observable<string> | ko.Computed<string>;
                nodes: JsonNode[];
                selected: ko.Observable<boolean> | ko.Computed<boolean>;
                value: any;
                nodeType: string;
                valueType: string;
                displayName: string;
            }
            class JsonSchemaNode extends JsonNode {
                static from(model: any, serializer?: DXImport.Analytics.Utils.IModelSerializer): JsonSchemaNode;
                static toJson(value: JsonSchemaNode, serializer: any, refs: any): {};
                getInfo(): DXImport.Analytics.Utils.ISerializationInfoArray;
                constructor(model: any, serializer?: DXImport.Analytics.Utils.IModelSerializer);
                nodeType: string;
                valueType: string;
                displayName: any;
                selected: ko.Observable<boolean>;
            }
            class JsonSchemaRootNode extends JsonNode {
                private _rootElementList;
                static from(model: any, serializer?: DXImport.Analytics.Utils.IModelSerializer): JsonSchemaRootNode;
                static toJson(value: JsonSchemaRootNode, serializer: any, refs: any): {};
                getInfo(): DXImport.Analytics.Utils.ISerializationInfoArray;
                constructor(model: any, serializer?: DXImport.Analytics.Utils.IModelSerializer);
                getRootElementPartList(): DXImport.Analytics.Utils.IPathRequest[];
                private _fillRootElementList;
                private _getNextPath;
            }
            class JsonAuthenticationInfo {
                static from(model: any, serializer?: DXImport.Analytics.Utils.IModelSerializer): JsonAuthenticationInfo;
                static toJson(value: JsonAuthenticationInfo, serializer: any, refs: any): any;
                getInfo(): {
                    propertyName: string;
                    modelName: string;
                    defaultVal: string;
                }[];
                constructor(model: any, serializer?: DXImport.Analytics.Utils.IModelSerializer);
                password: ko.Observable<string> | ko.Computed<string>;
                userName: ko.Observable<string> | ko.Computed<string>;
            }
            class JsonParameter extends DXImport.Analytics.Utils.Disposable {
                static toJson(value: JsonParameter, serializer: any, refs: any): any;
                getInfo(): ({
                    propertyName: string;
                    modelName: string;
                    displayValue: string;
                    editor: any;
                } | {
                    propertyName: string;
                    modelName: string;
                    displayValue?: undefined;
                    editor?: undefined;
                })[];
                constructor(model: any, serializer?: DXImport.Analytics.Utils.IModelSerializer);
                itemType: ko.Observable<string> | ko.Computed<string>;
                name: ko.Observable<string> | ko.Computed<string>;
                namePlaceholder: () => any;
                valuePlaceholder: () => any;
                value: ko.Observable<string> | ko.Computed<string>;
            }
            class JsonHeaderParameter extends JsonParameter {
                static from(model: any, serializer?: DXImport.Analytics.Utils.IModelSerializer): JsonHeaderParameter;
                itemType: ko.Observable<string>;
            }
            class JsonQueryParameter extends JsonParameter {
                static from(model: any, serializer?: DXImport.Analytics.Utils.IModelSerializer): JsonQueryParameter;
                itemType: ko.Observable<string>;
            }
            class JsonSource extends DXImport.Analytics.Utils.Disposable {
                private static _URIJSONSOURCE_TYPE;
                private static _CUSTOMJSONSOURCE_TYPE;
                static from(model: any, serializer?: DXImport.Analytics.Utils.IModelSerializer): JsonSource;
                static toJson(value: any, serializer: any, refs: any): any;
                getInfo(): DXImport.Analytics.Utils.ISerializationInfoArray;
                constructor(model?: any, serializer?: DXImport.Analytics.Utils.IModelSerializer);
                sourceType: ko.Observable<string>;
                uri: ko.Observable<string>;
                json: ko.Observable<string>;
                authenticationInfo: JsonAuthenticationInfo;
                headers: ko.ObservableArray<JsonHeaderParameter>;
                queryParameters: ko.ObservableArray<JsonQueryParameter>;
                serialize(includeRootTag?: boolean): any;
                resetSource(): void;
            }
            class JsonDataSource extends DXImport.Analytics.Utils.Disposable implements IDataSourceBase {
                getInfo(): DXImport.Analytics.Utils.ISerializationInfoArray;
                clone(serializer?: DXImport.Analytics.Utils.IModelSerializer): JsonDataSource;
                static from(model: any, serializer?: DXImport.Analytics.Utils.IModelSerializer): JsonDataSource;
                static toJson(value: any, serializer: any, refs: any): any;
                constructor(model: any, serializer?: DXImport.Analytics.Utils.IModelSerializer, requestWrapper?: QueryBuilder.Utils.RequestWrapper);
                getSchema(): JQueryPromise<JsonSchemaRootNode>;
                name: ko.Observable<string> | ko.Computed<string>;
                id: string;
                connectionName: ko.Observable<string> | ko.Computed<string>;
                jsonSchemaProvider: JsonSchemaProvider;
                schema: JsonSchemaRootNode;
                rootElement: ko.Observable<string> | ko.Computed<string>;
                source: JsonSource;
            }
            interface IJsonSchemaProvider extends DXImport.Analytics.Utils.IItemsProvider {
                getJsonSchema: () => JQueryPromise<JsonSchemaRootNode>;
            }
            module Internal {
                function getJsonSchemaCallback(requestWrapper: QueryBuilder.Utils.RequestWrapper, jsonDataSource: JsonDataSource): JQueryPromise<JsonSchemaRootNode>;
            }
            class JsonSchemaProvider extends DXImport.Analytics.Utils.Disposable implements IJsonSchemaProvider {
                private _requestWrapper;
                private _jsonSchemaPromise;
                private _jsonDataSource;
                private _jsonSchema;
                private _getJsonSchema;
                private _getJsonSchemaCallback;
                constructor(jsonDataSource: JsonDataSource, _requestWrapper?: QueryBuilder.Utils.RequestWrapper);
                reset(): void;
                mapToDataMemberContract(nodes: JsonNode[]): DXImport.Analytics.Utils.IDataMemberInfo[];
                getSchemaByPath(pathRequest: DXImport.Analytics.Utils.IPathRequest, jsonSchema: JsonSchemaNode): DXImport.Analytics.Utils.IDataMemberInfo[];
                getItems: (IPathRequest: any) => JQueryPromise<DXImport.Analytics.Utils.IDataMemberInfo[]>;
                getJsonSchema(): JQueryPromise<JsonSchemaRootNode>;
            }
            class ConnectionOptions {
                getInfo(): DXImport.Analytics.Utils.ISerializationInfoArray;
                constructor(model: any, serializer?: DXImport.Analytics.Utils.IModelSerializer);
                closeConnection: ko.Observable<boolean>;
                commandTimeout: ko.Observable<number>;
            }
            module Metadata {
                var customQuerySerializationsInfo: DXImport.Analytics.Utils.ISerializationInfoArray;
            }
            class CustomSqlQuery implements Utils.ISqlQueryViewModel {
                parent: SqlDataSource;
                constructor(model: any, parent: SqlDataSource, serializer?: DXImport.Analytics.Utils.IModelSerializer);
                sqlString: ko.Observable<string> | ko.Computed<string>;
                name: ko.Observable<string> | ko.Computed<string>;
                type: ko.Observable<string> | ko.Computed<string>;
                parameters: ko.ObservableArray<DataSourceParameter>;
                getInfo(): DXImport.Analytics.Utils.ISerializationInfoArray;
                generateName(): string;
            }
            module Metadata {
                var masterDetailRelationSerializationsInfo: DXImport.Analytics.Utils.ISerializationInfoArray;
            }
            class MasterDetailRelation extends DXImport.Analytics.Utils.Disposable {
                dispose(): void;
                private _customName;
                constructor(model: any, serializer?: DXImport.Analytics.Utils.IModelSerializer);
                name: ko.PureComputed<string>;
                masterQuery: ko.Observable<string> | ko.Computed<string>;
                detailQuery: ko.Observable<string> | ko.Computed<string>;
                keyColumns: ko.ObservableArray<{
                    masterColumn: ko.Observable<string> | ko.Computed<string>;
                    detailColumn: ko.Observable<string> | ko.Computed<string>;
                }>;
                createKeyColumn(): void;
                getInfo(): DXImport.Analytics.Utils.ISerializationInfoArray;
            }
            class SqlDataConnection {
                static from(model: any, serializer?: DXImport.Analytics.Utils.IModelSerializer): SqlDataConnection;
                static toJson(value: any, serializer: any, refs: any): any;
                getInfo(): DXImport.Analytics.Utils.ISerializationInfoArray;
                constructor(model: any, serializer?: DXImport.Analytics.Utils.IModelSerializer);
                name: ko.Observable<string>;
                parameteres: ko.Observable<string>;
                fromAppConfig: ko.Observable<boolean>;
                options: ConnectionOptions;
            }
            interface IDataSourceBase {
                name: ko.Observable<string> | ko.Computed<string>;
                id: string;
            }
            class SqlDataSource extends DXImport.Analytics.Utils.Disposable implements IDataSourceBase {
                getInfo(): DXImport.Analytics.Utils.ISerializationInfoArray;
                createQuery(item: any, serializer: any): Utils.ISqlQueryViewModel;
                constructor(model: any, serializer?: DXImport.Analytics.Utils.IModelSerializer, requestWrapper?: QueryBuilder.Utils.RequestWrapper);
                name: ko.Observable<string> | ko.Computed<string>;
                id: string;
                queries: ko.ObservableArray<Utils.ISqlQueryViewModel>;
                relations: ko.ObservableArray<MasterDetailRelation>;
                connection: SqlDataConnection;
                dbSchemaProvider: DBSchemaProvider;
                resultSet: ResultSet;
            }
            module Metadata {
                var storedProcQuerySerializationsInfo: DXImport.Analytics.Utils.ISerializationInfoArray;
            }
            class StoredProcQuery implements Utils.ISqlQueryViewModel {
                parent: SqlDataSource;
                constructor(model: any, parent: SqlDataSource, serializer?: DXImport.Analytics.Utils.IModelSerializer);
                procName: ko.Observable<string> | ko.Computed<string>;
                name: ko.Observable<string> | ko.Computed<string>;
                type: ko.Observable<string> | ko.Computed<string>;
                parameters: ko.ObservableArray<DataSourceParameter>;
                getInfo(): DXImport.Analytics.Utils.ISerializationInfoArray;
                generateName(): string;
            }
            module Metadata {
                var tableQuerySerializationsInfo: DXImport.Analytics.Utils.ISerializationInfoArray;
            }
            class TableQuery implements Utils.ISqlQueryViewModel {
                parent: SqlDataSource;
                constructor(model: any, parent: SqlDataSource, serializer?: DXImport.Analytics.Utils.IModelSerializer);
                name: ko.Observable<string> | ko.Computed<string>;
                type: ko.Observable<string> | ko.Computed<string>;
                filterString: ko.Observable<string> | ko.Computed<string>;
                parameters: ko.ObservableArray<DataSourceParameter>;
                tables(): {
                    name: ko.Observable<string> | ko.Computed<string>;
                    alias: ko.Observable<string>;
                }[];
                getInfo(): DXImport.Analytics.Utils.ISerializationInfoArray;
                generateName(): string;
            }
            module Metadata {
                var dsParameterNameValidationRules: Array<any>;
                var parameterValueSerializationsInfo: {
                    propertyName: string;
                    displayName: string;
                    localizationId: string;
                    editor: any;
                };
                var dsParameterSerializationInfo: DXImport.Analytics.Utils.ISerializationInfoArray;
                function storedProcParameterSerializationsInfo(type: string): any[];
            }
            class DataSourceParameter extends DXImport.Analytics.Utils.Disposable implements DXImport.Analytics.Utils.ISerializableModel {
                private _serializationsInfo;
                private _name;
                static typeValues: any[];
                private _getTypeValue;
                private _tryConvertValue;
                private static _isValueValid;
                getEditorType(type: any): {
                    header?: any;
                    content?: any;
                    custom?: any;
                };
                private _updateValueInfo;
                private _valueInfo;
                private _value;
                private _expressionValue;
                private _previousResultType;
                private _parametersFunctions;
                constructor(model: any, serializer?: DXImport.Analytics.Utils.IModelSerializer, _serializationsInfo?: DXImport.Analytics.Utils.ISerializationInfoArray);
                readonly specifics: any;
                static validateName(nameCandidate: string): boolean;
                isValid: ko.Observable<boolean> | ko.Computed<boolean>;
                name: ko.Computed<string>;
                value: ko.Observable | ko.Computed;
                type: ko.Observable<string> | ko.Computed<string>;
                resultType: ko.Observable<string> | ko.Computed<string>;
                getInfo(): DXImport.Analytics.Utils.ISerializationInfoArray;
                isPropertyVisible(propName: string): boolean;
            }
            enum DBColumnType {
                Unknown = 0,
                Boolean = 1,
                Byte = 2,
                SByte = 3,
                Char = 4,
                Decimal = 5,
                Double = 6,
                Single = 7,
                Int32 = 8,
                UInt32 = 9,
                Int16 = 10,
                UInt16 = 11,
                Int64 = 12,
                UInt64 = 13,
                String = 14,
                DateTime = 15,
                Guid = 16,
                TimeSpan = 17,
                ByteArray = 18
            }
            class DBColumn {
                name: string;
                type: DBColumnType;
                size: string;
                constructor(model: any);
                static GetType(dbColumnType: DBColumnType): "System.String" | "System.DateTime" | "System.Int16" | "System.Int32" | "System.Int64" | "System.Single" | "System.Double" | "System.Decimal" | "System.Boolean" | "System.Guid" | "System.SByte" | "System.Byte" | "System.UInt16" | "System.UInt32" | "System.UInt64" | "System.Char" | "System.TimeSpan" | "System.Byte[]" | "System.Object";
                static GetSpecific(type: string): "String" | "Date" | "Bool" | "Integer" | "Float";
            }
            class DBForeignKey {
                name: string;
                primaryKeyTable: string;
                column: string;
                primaryKeyColumn: string;
                constructor(model: any);
            }
            module Internal {
                function deserializeToCollection<T>(model: any[], createItem: (itemModel: any) => T, collection?: T[]): T[];
            }
            class DBSchema {
                tables: DBTable[];
                procedures: DBStoredProcedure[];
                constructor(model: any);
            }
        }
        module Wizard {
            var DataSourceWizardPageId: {
                ChooseDataSourceTypePage: string;
                ConfigureMasterDetailRelationshipsPage: string;
            };
            var SqlDataSourceWizardPageId: {
                ChooseConnectionPage: string;
                ConfigureQueryPage: string;
                ConfigureParametersPage: string;
                MultiQueryConfigurePage: string;
                MultiQueryConfigureParametersPage: string;
            };
            var JsonDataSourceWizardPageId: {
                ChooseJsonSourcePage: string;
                ChooseJsonSchemaPage: string;
                ChooseConnectionPage: string;
                SpecifyJsonConnectionPage: string;
            };
            var FullscreenDataSourceWizardPageId: {
                ChooseDataSourceTypePage: string;
                SpecifySqlDataSourceSettingsPage: string;
                SpecifyJsonDataSourceSettingsPage: string;
            };
            var FullscreenDataSourceWizardSectionId: {
                SpecifyJsonConnectionPage: string;
                ChooseJsonSchemaPage: string;
                ChooseJsonSourcePage: string;
                ChooseSqlConnectionPage: string;
                ConfigureQueryPage: string;
                ConfigureQueryParametersPage: string;
                ConfigureMasterDetailRelationshipsPage: string;
            };
            interface ISqlDataSourceWizardState {
                name?: string;
                queryName?: string;
                sqlDataSourceJSON?: string;
                relations?: string[];
                customQueries?: string[];
            }
            interface IJsonDataSourceWizardState {
                dataSourceName?: string;
                jsonScheme?: string;
                rootElement?: string;
                jsonSource?: string;
                newConnectionName?: string;
                connectionName?: string;
            }
            interface IDataSourceWizardState {
                dataSourceType?: DataSourceType;
                sqlDataSourceWizard?: ISqlDataSourceWizardState;
                jsonDataSourceWizard?: IJsonDataSourceWizardState;
                dataSourceId?: string;
            }
            function _restoreSqlDataSourceFromState(state?: ISqlDataSourceWizardState, requestWrapper?: QueryBuilder.Utils.RequestWrapper, dataSourceId?: string): _SqlDataSourceWrapper;
            function _restoreJsonDataSourceFromState(state: IJsonDataSourceWizardState, requestWrapper?: QueryBuilder.Utils.RequestWrapper, dataSourceId?: string): Data.JsonDataSource;
            function _createDefaultDataSourceWizardState(sqlDataSourceWizardState?: ISqlDataSourceWizardState, jsonDataSourceWizardState?: IJsonDataSourceWizardState): IDataSourceWizardState;
            interface IWizardPageMetadata<T extends IWizardPage> {
                setState: (data: any, state: any) => void;
                getState: (state: any) => any;
                resetState: (state: any, defaultState?: any) => void;
                create: () => T;
                canNext?: (page: T) => boolean;
                canFinish?: (page: T) => boolean;
                template: string;
                description?: string;
            }
            interface IWizardPage extends DXImport.Analytics.Utils.IDisposable {
                commit: () => JQueryPromise<any>;
                initialize: (state: any) => JQueryPromise<any>;
                canFinish: () => boolean;
                canNext: () => boolean;
                onChange?: (callback: () => void) => void;
            }
            class WizardPageBase<TState = any, TResult = any> extends DXImport.Analytics.Utils.Disposable implements IWizardPage {
                dispose(): void;
                commit(): JQueryPromise<any>;
                protected _onChange: () => void;
                onChange(callback: any): void;
                initialize(state: TState): JQueryPromise<any>;
                canNext(): boolean;
                canFinish(): boolean;
            }
            class _WrappedWizardPage extends DXImport.Analytics.Utils.Disposable {
                pageId: string;
                page: IWizardPage;
                template: string;
                description?: string;
                dispose(): void;
                resetCommitedState(): void;
                private _lastCommitedState;
                private _isInitialized;
                private _initDef;
                isChanged: boolean;
                onChange: (callback: () => void) => void;
                constructor(pageId: string, page: IWizardPage, template: string, description?: string);
                commit(): JQueryPromise<any>;
                initialize(state: any, force?: boolean): JQueryPromise<any>;
            }
            interface ITypeItem {
                text: string;
                imageClassName: string;
                imageTemplateName: string;
                type: number;
            }
            enum DataSourceType {
                NoData = 0,
                Sql = 1,
                Json = 2
            }
            class TypeItem implements ITypeItem {
                constructor(textDefault: string, textID: string, imageClassName: string, imageTemplateName: string, type: number);
                text: string;
                imageClassName: string;
                imageTemplateName: string;
                type: number;
            }
            class ChooseDataSourceTypePage extends WizardPageBase<IDataSourceWizardState, IDataSourceWizardState> {
                constructor(dataSourceTypeOptions: _DataSourceWizardOptions);
                canNext(): boolean;
                canFinish(): boolean;
                _itemClick: (item: ITypeItem) => void;
                _IsSelected: (item: ITypeItem) => boolean;
                _goToNextPage(): void;
                commit(): JQueryPromise<IDataSourceWizardState>;
                initialize(state: IDataSourceWizardState): JQueryPromise<{}>;
                typeItems: ITypeItem[];
                selectedItem: ko.Observable<ITypeItem>;
                _extendCssClass: (rightPath: string) => string;
            }
            function _registerChooseDataSourceTypePage(factory: PageFactory, dataSourceTypeOptions: _DataSourceWizardOptions): void;
            interface IConnectionStringDefinition {
                name: string;
                description?: string;
            }
            class ChooseSqlConnectionPage extends WizardPageBase<ISqlDataSourceWizardState, ISqlDataSourceWizardState> {
                constructor(connectionStrings: ko.ObservableArray<IConnectionStringDefinition>);
                initialize(state: ISqlDataSourceWizardState): JQueryPromise<{}>;
                canNext(): boolean;
                commit(): JQueryPromise<ISqlDataSourceWizardState>;
                _connectionStrings: ko.ObservableArray<IConnectionStringDefinition>;
                _selectedConnectionString: ko.ObservableArray<IConnectionStringDefinition>;
            }
            function _registerChooseSqlConnectionPage(factory: PageFactory, connectionStrings: ko.ObservableArray<IConnectionStringDefinition>): void;
            interface IChooseAvailableItemPageOperation {
                text: string;
                createNew: boolean;
            }
            class ChooseAvailableItemPage extends WizardPageBase {
                items: ko.Subscribable<any[]>;
                constructor(items: ko.Subscribable<any[]>, canCreateNew?: boolean);
                canNext(): boolean;
                canCreateNew: ko.Observable<boolean>;
                selectedItems: ko.ObservableArray<DXImport.Analytics.Internal.IDataSourceInfo>;
                operations: IChooseAvailableItemPageOperation[];
                selectedOperation: ko.Observable<IChooseAvailableItemPageOperation>;
                _createNew: ko.PureComputed<boolean>;
                initialize(state: any): any;
                _displayExpr(item: any): any;
                _getSelectedItem(state?: any): DXImport.Analytics.Internal.IDataSourceInfo;
                readonly createNewOperationText: any;
                readonly existingOperationText: any;
            }
            class ChooseJsonConnectionPage extends ChooseAvailableItemPage {
                commit(): JQueryPromise<IJsonDataSourceWizardState>;
                _getSelectedItem(data: IJsonDataSourceWizardState): DXImport.Analytics.Internal.IDataSourceInfo;
                readonly createNewOperationText: any;
                readonly existingOperationText: any;
            }
            function _registerChooseJsonConnectionPage(factory: PageFactory, wizardOptions: _DataSourceWizardOptions): void;
            class SpecifyJsonConnectionPage extends ChooseJsonConnectionPage {
                constructor(connections: any, canCreateNewJsonDataSource: any);
                commit(): JQueryPromise<IJsonDataSourceWizardState>;
                canNext(): boolean;
                initialize(state: any): JQueryPromise<IWizardPage>;
                _specifySourceData: ChooseJsonSourcePage;
            }
            function _registerSpecifyJsonConnectionPage(factory: PageFactory, connections: ko.ObservableArray<any>, canCreateNewJsonDataSource: boolean): void;
            class ChooseJsonSourcePage extends WizardPageBase<IJsonDataSourceWizardState, IJsonDataSourceWizardState> {
                private _requestWrapper;
                private _jsonStringSettings;
                private _jsonUriSetting;
                private __validationGroup;
                private __validationSummary;
                private _onValidationGroupInitialized;
                private _onValidationGroupDisposing;
                private _onValidationSummaryInitialized;
                private _onValidationSummaryDisposing;
                constructor();
                canNext(): boolean;
                commit(): JQueryPromise<IJsonDataSourceWizardState>;
                initialize(state: IJsonDataSourceWizardState): JQueryPromise<{}>;
                _jsonSourceTitle: any;
                _jsonConnectionTitle: any;
                _connectionNameValidationRules: {
                    type: string;
                    message: any;
                }[];
                _connectionName: ko.Observable<string>;
                _validationGroup: {
                    onInitialized: (args: any) => void;
                    onDisposing: (args: any) => void;
                };
                _validationSummary: {
                    onInitialized: (args: any) => void;
                    onDisposing: (args: any) => void;
                };
                _sources: Array<Analytics.Wizard.IJsonDataSourceType>;
                _selectedSource: ko.PureComputed;
            }
            function _registerChooseJsonSourcePage(factory: PageFactory): void;
            class ChooseJsonSchemaPage extends WizardPageBase<IJsonDataSourceWizardState, IJsonDataSourceWizardState> {
                private _requestWrapper;
                private _rootItems;
                private _fieldListItemsProvider;
                private _fieldSelectedPath;
                private _dataSource;
                private _cachedState;
                private _clear;
                private _createFieldListCallback;
                private _getSchemaToDataMemberInfo;
                private _mapJsonNodesToTreelistItems;
                private _getNodesByPath;
                private _getInnerItemsByPath;
                private _beginInternal;
                private _updatePage;
                private _createTreeNode;
                private _createLeafTreeNode;
                private _resetSelectionRecursive;
                private _mapJsonSchema;
                canNext(): boolean;
                canFinish(): boolean;
                constructor(_requestWrapper?: QueryBuilder.Utils.RequestWrapper);
                commit(): JQueryPromise<IJsonDataSourceWizardState>;
                initialize(state: IJsonDataSourceWizardState): JQueryPromise<{}>;
                reset(): void;
                _rootElementTitle: any;
                _rootElementList: ko.Observable<DXImport.Analytics.Utils.IPathRequest[]>;
                _selectedRootElement: ko.Observable<DXImport.Analytics.Utils.IPathRequest>;
                _fieldListModel: DXImport.Analytics.Widgets.Internal.ITreeListOptions;
            }
            function _registerChooseJsonSchemaPage(factory: PageFactory, requestWrapper?: QueryBuilder.Utils.RequestWrapper): void;
            class _SqlDataSourceWrapper {
                sqlDataSourceJSON?: string;
                sqlDataSource: Data.SqlDataSource;
                private _queryIndex;
                sqlQuery: Data.Utils.ISqlQueryViewModel;
                saveCustomQueries(): string[];
                save(): string;
                customQueries: Data.Utils.ISqlQueryViewModel[];
                constructor(sqlDataSourceJSON?: string, queryName?: string, requestWrapper?: QueryBuilder.Utils.RequestWrapper);
            }
            class ConfigureQueryPage extends WizardPageBase<ISqlDataSourceWizardState, ISqlDataSourceWizardState> {
                private _options;
                static QUERY_TEXT: string;
                static SP_TEXT: string;
                private _proceduresList;
                private _selectStatementControl;
                private _dataSourceWrapper;
                private _connection;
                private _dataSource;
                constructor(_options: _DataSourceWizardOptions);
                canNext(): boolean;
                canFinish(): boolean;
                runQueryBuilder(): void;
                localizeQueryType(queryTypeString: string): any;
                initialize(state: ISqlDataSourceWizardState): JQueryPromise<{}>;
                commit(): JQueryPromise<ISqlDataSourceWizardState>;
                queryControl: ko.Observable<Internal.ISqlQueryControl>;
                runQueryBuilderBtnText: ko.PureComputed<any>;
                popupQueryBuilder: Wizard.Internal.QueryBuilderPopup;
                queryTypeItems: string[];
                selectedQueryType: ko.Observable<string>;
            }
            function _registerConfigureQueryPage(factory: PageFactory, dataSourceWizardOptions: _DataSourceWizardOptions): void;
            class ConfigureQueryParametersPage extends WizardPageBase<ISqlDataSourceWizardState, ISqlDataSourceWizardState> {
                private parametersConverter;
                private _requestWrapper;
                private _sqlDataSourceWrapper;
                private _isParametersValid;
                constructor(parametersConverter: Internal.IParametersViewModelConverter, _requestWrapper: QueryBuilder.Utils.RequestWrapper);
                canNext(): boolean;
                canFinish(): boolean;
                getParameters(): any[];
                initialize(data: ISqlDataSourceWizardState): JQueryPromise<{}>;
                commit(): JQueryPromise<ISqlDataSourceWizardState>;
                removeButtonTitle: any;
                parametersEditorOptions: DXImport.Analytics.Widgets.Internal.ICollectionEditorOptions;
            }
            function _registerConfigureParametersPage(factory: PageFactory, requestWrapper?: QueryBuilder.Utils.RequestWrapper, parametersConverter?: Internal.IParametersViewModelConverter): void;
            class MultiQueryConfigurePage extends WizardPageBase<ISqlDataSourceWizardState, ISqlDataSourceWizardState> {
                private _options;
                private _callbacks;
                private _selectedPath;
                private _connection;
                private _itemsProvider;
                private _customQueries;
                private _checkedQueries;
                private _sqlTextProvider;
                private _sqlDataSourceWrapper;
                private _dataSource;
                private _dataConnection;
                private _addQueryAlgorithm;
                private _addQueryFromTables;
                private _addQueryFromStoredProcedures;
                private _addQueryFromCustomQueries;
                private _getItemsPromise;
                private _resetDataSourceResult;
                private _setQueryCore;
                static _pushQuery(newQuery: Data.Utils.ISqlQueryViewModel, node: Analytics.Wizard.Internal.TreeLeafNode, queries: ko.ObservableArray<Data.Utils.ISqlQueryViewModel>): void;
                static _removeQuery(queries: ko.ObservableArray<Data.Utils.ISqlQueryViewModel>, node: any): void;
                constructor(_options: _MultiQueryDataSourceWizardOptions);
                canNext(): boolean;
                canFinish(): boolean;
                _showStatementPopup: (query: any) => void;
                _AddQueryWithBuilder(): void;
                _runQueryBuilder(): void;
                _loadPanelViewModel(element: HTMLElement): {
                    animation: {
                        show: {
                            type: string;
                            from: number;
                            to: number;
                            duration: number;
                        };
                        hide: {
                            type: string;
                            from: number;
                            to: number;
                            duration: number;
                        };
                    };
                    deferRendering: boolean;
                    message: any;
                    visible: ko.Observable<boolean>;
                    shading: boolean;
                    shadingColor: string;
                    position: {
                        of: JQuery;
                    };
                    container: JQuery;
                };
                _setTableQuery(query: Data.TableQuery, isInProcess?: ko.Observable<boolean>): JQueryPromise<QueryBuilder.Utils.ISelectStatementResponse>;
                _setCustomSqlQuery(query: Data.CustomSqlQuery): void;
                _showQbCallBack: (name?: any, isCustomQuery?: boolean) => void;
                commit(): JQueryPromise<ISqlDataSourceWizardState>;
                initialize(state: ISqlDataSourceWizardState): JQueryPromise<Data.Utils.ISqlQueryViewModel>;
                _popupSelectStatement: {
                    isVisible: ko.Observable<boolean>;
                    title: () => any;
                    query: any;
                    data: ko.Observable<any>;
                    okButtonText: () => any;
                    okButtonHandler: (e: any) => void;
                    aceOptions: {
                        showLineNumbers: boolean;
                        showPrintMargin: boolean;
                        enableBasicAutocompletion: boolean;
                        enableLiveAutocompletion: boolean;
                        readOnly: boolean;
                        highlightSelectedWord: boolean;
                        showGutter: boolean;
                        highlightActiveLine: boolean;
                    };
                    aceAvailable: boolean;
                    additionalOptions: {
                        onChange: (session: any) => void;
                        onValueChange: (editor: any) => void;
                        changeTimeout: number;
                        overrideEditorFocus: boolean;
                        setUseWrapMode: boolean;
                    };
                    languageHelper: {
                        getLanguageMode: () => string;
                        createCompleters: () => any[];
                    };
                    closest(element: HTMLElement, parentSelector: string): JQuery;
                };
                _customResetOptions: () => any;
                _queryEditIndex: ko.Observable<number>;
                disableCustomSql: boolean;
                _scrollViewHeight: string;
                _getItemsAfterCheck: (node: Analytics.Wizard.Internal.TreeNode) => any;
                _fieldListModel: ko.Observable<DXImport.Analytics.Widgets.Internal.ITreeListOptions>;
                _popupQueryBuilder: Analytics.Wizard.Internal.QueryBuilderPopup;
                _customizeDBSchemaTreeListActions: (item: DXImport.Analytics.Utils.IDataMemberInfo, actions: DXImport.Analytics.Utils.IAction[]) => void;
                _hasParametersToEdit: ko.Computed<boolean>;
                _isDataLoadingInProcess: ko.Observable<boolean>;
            }
            function _registerMultiQueryConfigurePage(factory: PageFactory, wizardOptions: _MultiQueryDataSourceWizardOptions): void;
            function _canEditQueryParameters(query: Data.Utils.ISqlQueryViewModel, customQueries: Data.Utils.ISqlQueryViewModel[]): boolean;
            class MultiQueryConfigureParametersPage extends WizardPageBase<ISqlDataSourceWizardState, ISqlDataSourceWizardState> {
                private parametersConverter;
                private _requestWrapper;
                private _sqlDataSourceWrapper;
                private _selectedPath;
                private _isParametersValid;
                private _rootItems;
                private _createNewParameter;
                canNext(): boolean;
                canFinish(): boolean;
                constructor(parametersConverter: Internal.IParametersViewModelConverter, _requestWrapper: QueryBuilder.Utils.RequestWrapper);
                _getParameters(): any;
                initialize(state: ISqlDataSourceWizardState): JQueryPromise<{}>;
                commit(): JQueryPromise<ISqlDataSourceWizardState>;
                _scrollViewHeight: string;
                _fieldListModel: ko.Observable<DXImport.Analytics.Widgets.Internal.ITreeListOptions>;
                _removeButtonTitle: any;
                _parametersEditorOptions: DXImport.Analytics.Widgets.Internal.ICollectionEditorOptions;
            }
            function _registerMultiQueryConfigureParametersPage(factory: PageFactory, requestWrapper?: QueryBuilder.Utils.RequestWrapper, parametersConverter?: Internal.IParametersViewModelConverter): void;
            class ConfigureMasterDetailRelationshipsPage extends WizardPageBase<ISqlDataSourceWizardState, ISqlDataSourceWizardState> {
                private _sqlDataSourceResultSchema;
                dispose(): void;
                private _relations;
                private _resultSet;
                private relationsSubscription;
                private _sqlDataSourceWrapper;
                private _updateRelations;
                constructor(_sqlDataSourceResultSchema: (dataSource: Data.SqlDataSource, queryName?: string, relationsEditing?: boolean) => JQueryPromise<{
                    resultSchemaJSON: string;
                    connectionParameters?: string;
                }>);
                canNext(): boolean;
                canFinish(): boolean;
                private _getResultSet;
                initialize(state: ISqlDataSourceWizardState): JQueryPromise<Data.ResultSet>;
                commit(): JQueryPromise<ISqlDataSourceWizardState>;
                _customResetOptions: () => any;
                _relationsEditor: ko.Observable<QueryBuilder.Widgets.Internal.MasterDetailEditor>;
            }
            function _registerConfigureMasterDetailRelationshipsPage(factory: PageFactory, sqlDataSourceResultSchema: (dataSource: Data.SqlDataSource, queryName?: string, relationsEditing?: boolean) => JQueryPromise<{
                resultSchemaJSON: string;
                connectionParameters?: string;
            }>): void;
            class PageFactory {
                registerMetadata<T extends IWizardPage>(pageId: string, metadata: IWizardPageMetadata<T>): void;
                getMetadata(pageId: any): IWizardPageMetadata<IWizardPage>;
                unregisterMetadata(pageId: string): void;
                reset(): void;
                metadata: {
                    [key: string]: IWizardPageMetadata<IWizardPage>;
                };
            }
            class StateManager {
                private globalState;
                private pageFactory;
                private defaultState;
                private _getPageState;
                constructor(globalState: any, pageFactory: PageFactory);
                setPageState(pageId: string, data: any): void;
                getPageState(pageId: string): any;
                resetPageState(pageId: string): void;
                getCurrentState(): any;
                reset(): void;
            }
            class PageIterator<T = any> extends DXImport.Analytics.Utils.Disposable {
                pageFactory: PageFactory;
                stateManager: StateManager;
                private _onResetPage;
                dispose(): void;
                private _pages;
                private _currentIndex;
                private __resetPages;
                private _nextPage;
                private _getNextExistingPage;
                _resetPages(): void;
                private _getNextNewPage;
                constructor(pageFactory: PageFactory, stateManager: StateManager, _onResetPage?: (page: _WrappedWizardPage) => void);
                _getStartPage(pageId?: string): _WrappedWizardPage;
                _getNextPage(): JQueryPromise<_WrappedWizardPage>;
                _getPreviousPage(): JQueryPromise<_WrappedWizardPage>;
                _goToPage(pageId: string): JQueryPromise<_WrappedWizardPage>;
                _getCurrentPage(): _WrappedWizardPage;
                _getCurrentState(): T;
                getNextPageId(pageId?: string): string;
            }
            interface IWizardEventArgs<Sender> {
                wizard: Sender;
            }
            interface IWizardPageEventArgs<Sender> extends IWizardEventArgs<Sender> {
                page: IWizardPage;
                pageId: string;
            }
            interface IBeforeWizardPageInitializeEventArgs<Sender> extends IWizardPageEventArgs<Sender>, IBeforeWizardInitializeEventArgs<Sender> {
            }
            interface IBeforeWizardInitializeEventArgs<Sender> extends IWizardEventArgs<Sender> {
                state: any;
            }
            interface IBeforeWizardFinishEventArgs {
                state: any;
                wizardModel?: any;
            }
            interface IAfterWizardFinishEventArgs {
                state: any;
                wizardResult?: any;
            }
            interface IWizardEvents<Sender> {
                "afterInitialize": IWizardEventArgs<Sender>;
                "beforeInitialize": IBeforeWizardInitializeEventArgs<Sender>;
                "beforeStart": IWizardEventArgs<Sender>;
                "beforePageInitialize": IBeforeWizardPageInitializeEventArgs<Sender>;
                "afterPageInitialize": IWizardPageEventArgs<Sender>;
                "beforeFinish": IBeforeWizardFinishEventArgs;
                "afterFinish": IAfterWizardFinishEventArgs;
            }
            class BaseWizard extends DXImport.Analytics.Utils.Disposable {
                pageFactory: PageFactory;
                static __loadingStateFunctionName: string;
                static __nextActionFunctionName: string;
                stateManager: StateManager;
                iterator: PageIterator;
                events: DXImport.Analytics.Utils.EventManager<BaseWizard, IWizardEvents<BaseWizard>>;
                private _finishCallback;
                protected _createLoadingState(page: IWizardPage): void;
                protected _createNextAction(page: IWizardPage): void;
                private _loadingTimeout;
                protected _loadingState(active: any): void;
                protected _callBeforeFinishHandler(state: any, wizardModel?: any): void;
                protected _callAfterFinishHandler(state: any, result: any): void;
                onFinish(): void;
                constructor(pageFactory: PageFactory, finishCallback?: (model: IDataSourceWizardState) => JQueryPromise<boolean>);
                initialize(state?: any, createIterator?: (pageFactory: PageFactory, stateManager: StateManager) => PageIterator): void;
                isFirstPage(): boolean;
                canNext(): boolean;
                canFinish(): boolean;
                _initPage(page: _WrappedWizardPage): JQueryPromise<any>;
                start(): void;
                canRunWizard(): boolean;
                nextAction(): void;
                previousAction(): void;
                goToPage(pageId: string): void;
                finishAction(): void;
                isLoading: ko.Observable<boolean>;
                _currentPage: ko.Observable<_WrappedWizardPage>;
                isVisible: ko.Observable<boolean>;
            }
            interface IWizardPageSectionMetadata<T extends IWizardPage> extends IWizardPageMetadata<T> {
                position?: number;
                disabledText?: string;
                recreate?: boolean;
                onChange?: () => void;
            }
            module Internal {
                class WrappedWizardPageSection extends _WrappedWizardPage {
                    pageId: string;
                    page: IWizardPage;
                    onChange: (callback: () => void) => void;
                    constructor(pageId: string, page: IWizardPage, metadata: IWizardPageSectionMetadata<IWizardPage>);
                }
                class WizardPageSection {
                    pageId: string;
                    metadata: IWizardPageSectionMetadata<IWizardPage>;
                    resetPage(): void;
                    setPage(page: _WrappedWizardPage): void;
                    constructor(pageId: string, metadata: IWizardPageSectionMetadata<IWizardPage>);
                    page: ko.Observable<_WrappedWizardPage>;
                }
                class WizardPageSectionIterator {
                    pageFactory: WizardPageSectionFactory;
                    stateManager: StateManager;
                    private _resetPageCallback;
                    private _pagesIds;
                    private _pages;
                    private _resetPages;
                    private _tryResetPageByMetadata;
                    private _resetPage;
                    private _createNewPage;
                    private _getPage;
                    private _getNextPage;
                    private _getPageIndex;
                    resetNextPages(pageId: string): void;
                    constructor(pageFactory: WizardPageSectionFactory, stateManager: StateManager, _resetPageCallback: (pageId: string) => void);
                    getStartPage(): WrappedWizardPageSection;
                    getNextPage(currentPageId: string): JQueryPromise<WrappedWizardPageSection[]>;
                    getCurrentState(): any;
                    getNextPageId(pageId?: string): string | string[];
                }
                class WizardPageSectionFactory extends Wizard.PageFactory {
                    registerMetadata<T extends IWizardPage>(pageId: string, metadata: IWizardPageSectionMetadata<T>): void;
                    metadata: {
                        [key: string]: IWizardPageSectionMetadata<IWizardPage>;
                    };
                }
                class WizardPageProcessor extends DXImport.Analytics.Utils.Disposable {
                    pageFactory: WizardPageSectionFactory;
                    dispose(): void;
                    static __loadingStateFunctionName: string;
                    stateManager: StateManager;
                    iterator: WizardPageSectionIterator;
                    events: DXImport.Analytics.Utils.EventManager<WizardPageProcessor, IWizardEvents<WizardPageProcessor>>;
                    protected _createLoadingState(page: IWizardPage): void;
                    protected _createNextAction(page: IWizardPage): void;
                    private _loadingTimeout;
                    private _changeTimeout;
                    protected _loadingState(active: any): void;
                    protected _extendedNextAction(): void;
                    constructor(pageFactory: WizardPageSectionFactory, _loadingState?: (boolean: any) => void, _nextAction?: () => void);
                    private _resetPageById;
                    initialize(state: IDataSourceWizardState, createIterator?: (pageFactory: WizardPageSectionFactory, stateManager: StateManager) => WizardPageSectionIterator): void;
                    private _canNext;
                    private _canFinish;
                    private _initPage;
                    getPageById(pageId: any): WizardPageSection;
                    start(): void;
                    finishAction(): JQueryPromise<{}>;
                    private _nextAction;
                    sections: WizardPageSection[];
                    isLoading: ko.Observable<boolean>;
                }
            }
            class PopupWizard extends BaseWizard {
                static _getLoadPanelViewModel(element: HTMLElement, observableVisible: ko.Observable<boolean>): {
                    animation: {
                        show: {
                            type: string;
                            from: number;
                            to: number;
                            duration: number;
                        };
                        hide: {
                            type: string;
                            from: number;
                            to: number;
                            duration: number;
                        };
                    };
                    deferRendering: boolean;
                    message: any;
                    visible: ko.Observable<boolean>;
                    shading: boolean;
                    shadingColor: string;
                    position: {
                        of: JQuery;
                    };
                    container: JQuery;
                };
                constructor(pageFactory: any, finishCallback?: any);
                start(): void;
                height: ko.Observable<number>;
                width: ko.Observable<number>;
                _extendCssClass: string;
                _container: (el: HTMLElement) => JQuery;
                nextButton: {
                    text: any;
                    disabled: ko.Computed<boolean>;
                    onClick: () => void;
                };
                cancelButton: {
                    text: any;
                    onClick: () => any;
                };
                previousButton: {
                    text: any;
                    disabled: ko.Computed<boolean>;
                    onClick: () => void;
                };
                finishButton: {
                    text: any;
                    disabled: ko.Computed<boolean>;
                    onClick: () => void;
                };
                _wizardPopupPosition(element: HTMLElement): {
                    of: JQuery;
                };
                _loadPanelViewModel(element: HTMLElement): {
                    animation: {
                        show: {
                            type: string;
                            from: number;
                            to: number;
                            duration: number;
                        };
                        hide: {
                            type: string;
                            from: number;
                            to: number;
                            duration: number;
                        };
                    };
                    deferRendering: boolean;
                    message: any;
                    visible: ko.Observable<boolean>;
                    shading: boolean;
                    shadingColor: string;
                    position: {
                        of: JQuery;
                    };
                    container: JQuery;
                };
                _getLoadPanelViewModel(element: HTMLElement, observableVisible: ko.Observable<boolean>): {
                    animation: {
                        show: {
                            type: string;
                            from: number;
                            to: number;
                            duration: number;
                        };
                        hide: {
                            type: string;
                            from: number;
                            to: number;
                            duration: number;
                        };
                    };
                    deferRendering: boolean;
                    message: any;
                    visible: ko.Observable<boolean>;
                    shading: boolean;
                    shadingColor: string;
                    position: {
                        of: JQuery;
                    };
                    container: JQuery;
                };
                _titleTemplate: string;
                title: string;
            }
            class _DataSourceWizardOptionsBase<T extends Internal.IDataSourceWizardCallbacks> {
                readonly jsonDataSourceAvailable: boolean;
                readonly sqlDataSourceAvailable: boolean;
                connectionStrings: Analytics.Wizard.IDataSourceWizardConnectionStrings;
                callbacks: T;
                rtl: boolean;
                requestWrapper: QueryBuilder.Utils.RequestWrapper;
                disableCustomSql: boolean;
                wizardSettings: IDataSourceWizardSettings;
                queryName: string;
                canCreateNewJsonDataSource: boolean;
            }
            class _DataSourceWizardOptions extends _DataSourceWizardOptionsBase<Internal.IDataSourceWizardCallbacks> {
            }
            interface IDataSourceWizardSettings {
                enableJsonDataSource?: boolean;
                enableSqlDataSource?: boolean;
            }
            class DataSourceWizardSettings implements IDataSourceWizardSettings {
                createDefault(settings?: IDataSourceWizardSettings): IDataSourceWizardSettings;
                enableJsonDataSource?: boolean;
                enableSqlDataSource?: boolean;
            }
            interface IRetrieveQuerySqlCallback {
                (query: Data.TableQuery, isInProcess: ko.Observable<boolean>): JQueryPromise<QueryBuilder.Utils.ISelectStatementResponse>;
            }
            class DataSourceWizardPageIterator extends PageIterator {
                private _dataSourceWizardOptions;
                constructor(pageFactory: PageFactory, stateManager: StateManager, _dataSourceWizardOptions: _DataSourceWizardOptions);
                getNextPageId(pageId: string): any;
            }
            class DataSourceWizard extends PopupWizard {
                private _wizardOptions;
                constructor(pageFactory: any, _wizardOptions: _DataSourceWizardOptions);
                initialize(state: IDataSourceWizardState, createIterator?: (pageFactory: PageFactory, stateManager: StateManager) => PageIterator): void;
                canRunWizard(): boolean;
                _extendCssClass: string;
                title: any;
            }
            function _registerSqlDataSourceWizardPages(factory: PageFactory, dataSourceWizardOptions: _DataSourceWizardOptions): PageFactory;
            function _createSqlDataSourceWizard(factory: PageFactory, dataSourceWizardOptions: _DataSourceWizardOptions): DataSourceWizard;
            class _MultiQueryDataSourceWizardOptions extends _DataSourceWizardOptionsBase<Internal.IMultiQueryDataSourceWizardCallbacks> {
            }
            class MultiQueryDataSourceWizard extends PopupWizard {
                private _wizardOptions;
                constructor(pageFactory: any, _wizardOptions: _MultiQueryDataSourceWizardOptions);
                canRunWizard(): boolean;
                initialize(state: IDataSourceWizardState, createIterator?: (pageFactory: PageFactory, stateManager: StateManager) => PageIterator): void;
                title: any;
                _extendCssClass: string;
            }
            class MultiQueryDataSourceWizardPageIterator<T extends IDataSourceWizardState = IDataSourceWizardState> extends PageIterator<T> {
                private _wizardOptions;
                constructor(pagesFactory: PageFactory, stateManager: StateManager, _wizardOptions: _MultiQueryDataSourceWizardOptions);
                getNextPageId(pageId?: string): string;
            }
            function _registerMultiQueryDataSourcePages(factory: PageFactory, dataSourceWizardOptions: _MultiQueryDataSourceWizardOptions): PageFactory;
            function _createMultiQueryDataSourceWizard(factory: PageFactory, dataSourceWizardOptions: _MultiQueryDataSourceWizardOptions): MultiQueryDataSourceWizard;
            interface IFullscreenWizardPageMetadata<T extends Analytics.Wizard.IWizardPage> extends Analytics.Wizard.IWizardPageMetadata<T> {
                navigationPanelText?: string;
            }
            class FullscreenWizardPageFactory extends Analytics.Wizard.PageFactory {
                registerMetadata<T extends Analytics.Wizard.IWizardPage>(pageId: string, metadata: IFullscreenWizardPageMetadata<T>): void;
                getMetadata(key: string): IFullscreenWizardPageMetadata<Analytics.Wizard.IWizardPage>;
                metadata: {
                    [key: string]: IFullscreenWizardPageMetadata<Analytics.Wizard.IWizardPage>;
                };
            }
            enum WizardSectionPosition {
                Left = 1,
                TopLeft = 2,
                BottomLeft = 3,
                Right = 4,
                TopRight = 5,
                BottomRight = 6,
                Top = 7,
                Bottom = 8
            }
            interface IWizardPageStyle {
                top?: any;
                bottom?: any;
                left?: any;
                right?: any;
                width?: any;
                height?: any;
                display?: any;
            }
            interface IBeforeWizardSectionInitializeEventArgs<Sender> extends IWizardSectionEventArgs<Sender> {
                state: any;
            }
            interface IWizardSectionEventArgs<Sender> {
                section: IWizardPage;
                sectionId: string;
                page: Sender;
            }
            interface IWizardFullscreenPageEvents<Sender> {
                "beforeSectionInitialize": IBeforeWizardSectionInitializeEventArgs<Sender>;
                "afterSectionInitialize": IWizardSectionEventArgs<Sender>;
            }
            class FullscreenWizardPage extends Analytics.Wizard.WizardPageBase {
                dispose(): void;
                private _patchOnChange;
                private _getPageStyle;
                private _sectionsToUnregister;
                private _sectionsToRegister;
                private _sectionPositions;
                private _applyCustomizations;
                protected _setSectionPosition(pageId: string, position?: WizardSectionPosition): void;
                constructor();
                registerSections(): void;
                canNext(): boolean;
                canFinish(): boolean;
                setSectionPosition(sectionId: string, position?: WizardSectionPosition): void;
                registerSection(sectionId: string, metadata: IWizardPageMetadata<IWizardPage>): void;
                unregisterSection(sectionId: string): void;
                _loadPanelViewModel(element: HTMLElement): boolean;
                getNextSectionId(sectionId: string): any;
                initialize(state: any): JQueryPromise<any>;
                _beforeStart(): void;
                commit(): JQueryPromise<any>;
                _getPageDescription(index: number, page: Analytics.Wizard.Internal.WizardPageSection): string;
                _showPageDescription(): boolean;
                _initInProgress: ko.Observable<boolean>;
                _defaultMargin: number;
                _parentMarginOffset: number;
                _sections: Analytics.Wizard.Internal.WizardPageSection[];
                _pageCss: {
                    [key: string]: ko.Observable<IWizardPageStyle>;
                };
                _factory: DevExpress.Analytics.Wizard.Internal.WizardPageSectionFactory;
                _stateManager: Analytics.Wizard.StateManager;
                _sectionsProcessor: Analytics.Wizard.Internal.WizardPageProcessor;
                events: DXImport.Analytics.Utils.EventManager<FullscreenWizardPage, IWizardFullscreenPageEvents<FullscreenWizardPage>>;
            }
            class SpecifyJsonDataSourceSettingsPage extends FullscreenWizardPage {
                private _dataSourceWizardOptions;
                constructor(_dataSourceWizardOptions: _DataSourceWizardOptions);
                registerSections(): void;
                getNextSectionId(sectionId: string): string;
            }
            function _registerSpecifyJsonDataSourceSettingsPage(factory: FullscreenWizardPageFactory, dataSourceWizardOptions: _DataSourceWizardOptions): void;
            class SpecifySqlDataSourceSettingsPage extends FullscreenWizardPage {
                private _dataSourceWizardOptions;
                constructor(_dataSourceWizardOptions: _MultiQueryDataSourceWizardOptions);
                getNextSectionId(sectionId: string): string | any[];
                registerSections(): void;
            }
            function _registerSpecifySqlDataSourceSettingsPage(factory: FullscreenWizardPageFactory, dataSourceWizardOptions: _MultiQueryDataSourceWizardOptions): void;
            class FullscreenWizard extends Analytics.Wizard.PopupWizard {
                private _onCloseCallback;
                constructor(pageFactory: FullscreenWizardPageFactory, finishCallback?: any);
                _onClose(callback: any): void;
                onFinish(): void;
                _initPage(page: any): JQueryPromise<any>;
                _onResetPage(page: Analytics.Wizard._WrappedWizardPage): void;
                start(finishCallback?: (model: any) => JQueryPromise<boolean>): void;
                _pageDescription(): string;
                _description(): string;
                navigationPanel: ko.Observable<WizardNavigationPanel>;
                _steps: FullscreenWizardPage[];
                _extendCssClass: string;
                pageFactory: FullscreenWizardPageFactory;
            }
            interface IWizardNavigationStep {
                pageIds: string[];
                currentPageId: string;
                clickAction: () => void;
                text: string;
                stepIndex: number;
                isActive: ko.Observable<boolean> | ko.Computed<boolean>;
                disabled: ko.Observable<boolean> | ko.Computed<boolean>;
                visible: ko.Observable<boolean> | ko.Computed<boolean>;
            }
            class WizardNavigationPanel extends DXImport.Analytics.Utils.Disposable {
                constructor(wizard: FullscreenWizard);
                resetAll(): void;
                _reset(pageId: string): void;
                _resetNextPages(pageId: string): void;
                _setStepVisible(currentPageIndex: number): void;
                _steps: Array<IWizardNavigationStep>;
                isVisible: ko.Computed<boolean>;
            }
            class FullscreenDataSourceWizard extends FullscreenWizard {
                private _dataSourceWizardOptions;
                constructor(factory: any, _dataSourceWizardOptions: _DataSourceWizardOptions);
                initialize(state: IDataSourceWizardState, createIterator?: (pageFactory: PageFactory, stateManager: StateManager) => PageIterator): void;
                canRunWizard(): boolean;
                _description(): any;
            }
            class FullscreenDataSourceWizardPageIterator extends PageIterator {
                private _dataSourceOptions;
                constructor(factory: any, stateManager: any, _dataSourceOptions: _DataSourceWizardOptions, onResetPage: any);
                getNextPageId(pageId?: string): string;
            }
            function _createDataSourceFullscreenWizard(dataSourceWizardOptions: _MultiQueryDataSourceWizardOptions): FullscreenDataSourceWizard;
            interface IConnectionStringDefinition {
                name: string;
                description?: string;
            }
            interface IDataSourceWizardConnectionStrings {
                sql: ko.ObservableArray<IConnectionStringDefinition>;
                json?: ko.ObservableArray<IConnectionStringDefinition>;
            }
            module Legacy {
                class WizardPage<TWizardModel> {
                    constructor(wizard: WizardViewModel<TWizardModel>, template?: string, title?: string, description?: string);
                    template: string;
                    title: string;
                    description: string;
                    wizard: WizardViewModel<TWizardModel>;
                    isVisible: boolean;
                    actionCancel: Internal.WizardAction;
                    actionPrevious: Internal.WizardAction;
                    actionNext: Internal.WizardAction;
                    actionFinish: Internal.WizardAction;
                    _begin(data: TWizardModel): void;
                    beginAsync(data: TWizardModel): JQueryPromise<any>;
                    commit(data: TWizardModel): void;
                    reset(): void;
                }
                class WizardViewModel<TWizardModel> {
                    static WIZARD_DEFAULT_WIDTH: string;
                    static WIZARD_DEFAULT_HEIGHT: string;
                    static WIZARD_DEFAULT_SCROLLVIEW_HEIGHT: string;
                    protected _data: TWizardModel;
                    private _defaultWizardPage;
                    private _finishCallback;
                    private _pageToken;
                    private _pageIsFirst;
                    private _currentStep;
                    protected _goToPage(task: JQueryPromise<IPageInfo<TWizardModel>>): void;
                    constructor();
                    width: ko.Observable<string>;
                    height: ko.Observable<string>;
                    title: string;
                    headerTemplate: string;
                    extendCssClass: string;
                    steps: WizardPage<TWizardModel>[];
                    renderedSteps: ko.ObservableArray<WizardPage<TWizardModel>>;
                    isVisible: ko.Observable<boolean>;
                    indicatorVisible: ko.Observable<boolean>;
                    titleTemplate: string;
                    dispatcher: IPageDispatcher<TWizardModel>;
                    readonly currentStep: any;
                    container: (element: HTMLElement) => JQuery;
                    loadPanelViewModel(element: HTMLElement): {
                        animation: {
                            show: {
                                type: string;
                                from: number;
                                to: number;
                                duration: number;
                            };
                            hide: {
                                type: string;
                                from: number;
                                to: number;
                                duration: number;
                            };
                        };
                        deferRendering: boolean;
                        message: any;
                        visible: ko.Observable<boolean>;
                        shading: boolean;
                        shadingColor: string;
                        position: {
                            of: JQuery;
                        };
                        container: JQuery;
                    };
                    getLoadPanelViewModel(element: HTMLElement, observableVisible: ko.Observable<boolean>): {
                        animation: {
                            show: {
                                type: string;
                                from: number;
                                to: number;
                                duration: number;
                            };
                            hide: {
                                type: string;
                                from: number;
                                to: number;
                                duration: number;
                            };
                        };
                        deferRendering: boolean;
                        message: any;
                        visible: ko.Observable<boolean>;
                        shading: boolean;
                        shadingColor: string;
                        position: {
                            of: JQuery;
                        };
                        container: JQuery;
                    };
                    wizardPopupPosition(element: HTMLElement): {
                        of: JQuery;
                    };
                    goToNext(): void;
                    goToPrevious(): void;
                    isPreviousButtonDisabled: ko.PureComputed<any>;
                    cancel(): void;
                    start(data: TWizardModel, finishCallback?: (model: TWizardModel) => JQueryPromise<boolean>): void;
                    finish(): void;
                    resetState(): void;
                    static chainCallbacks<T>(first: (data: T) => JQueryPromise<boolean>, second: (data: T) => JQueryPromise<boolean>): (data: T) => JQueryPromise<boolean>;
                }
                class CommonParametersPage<T> extends WizardPage<T> {
                    private _validation;
                    getParameters(): any[];
                    validateParameters(): void;
                    reset(): void;
                    commit(data: T): void;
                }
                interface ITypeItem {
                    text: string;
                    imageClassName: string;
                    imageTemplateName: string;
                    type: number;
                }
                enum DataSourceType {
                    NoData = 0,
                    Sql = 1,
                    Json = 2
                }
                class TypeItem implements ITypeItem {
                    constructor(textDefault: string, textID: string, imageClassName: string, imageTemplateName: string, type: number);
                    text: string;
                    imageClassName: string;
                    imageTemplateName: string;
                    type: number;
                }
                class ChooseDataSourceTypePage<T extends IDataSourceWizardModel> extends WizardPage<T> {
                    private _isActivated;
                    constructor(wizard: WizardViewModel<T>, wizardSettings?: IDataSourceWizardSettings);
                    template: string;
                    description: any;
                    typeItems: ITypeItem[];
                    selectedItem: ko.Observable<ITypeItem>;
                    extendCssClass: (rightPath: string) => string;
                    itemClick: (item: ITypeItem) => void;
                    IsSelected: (item: ITypeItem) => boolean;
                    _begin(data: T): void;
                    commit(data: T): void;
                    reset(): void;
                }
                class MasterDetailRelationsPage extends WizardPage<MultiQueryDataSourceWizardModel> {
                    private _sqlDataSourceResultSchema;
                    private _relations;
                    private _resultSet;
                    private relationsSubscription;
                    constructor(wizard: WizardViewModel<MultiQueryDataSourceWizardModel>, sqlDataSourceResultSchema: (dataSource: Data.SqlDataSource) => JQueryPromise<{
                        resultSchemaJSON: string;
                        connectionParameters?: string;
                    }>);
                    template: string;
                    description: any;
                    relationsEditor: ko.Observable<QueryBuilder.Widgets.Internal.MasterDetailEditor>;
                    private _getResultSet;
                    beginAsync(data: MultiQueryDataSourceWizardModel): JQueryPromise<any>;
                    customResetOptions: () => any;
                    commit(data: MultiQueryDataSourceWizardModel): void;
                }
                class MultiQueryDataSourceWizard extends WizardViewModel<MultiQueryDataSourceWizardModel> {
                    connectionStrings: IDataSourceWizardConnectionStrings;
                    constructor(connectionStrings: IDataSourceWizardConnectionStrings, wizardSettings?: IDataSourceWizardSettings, callbacks?: Internal.IMultiQueryDataSourceWizardCallbacks, disableCustomSql?: boolean, rtl?: boolean);
                    start(wizardModel?: MultiQueryDataSourceWizardModel, finishCallback?: (model: MultiQueryDataSourceWizardModel) => JQueryPromise<boolean>): void;
                    height: ko.Observable<string>;
                    title: any;
                    extendCssClass: string;
                    container: (element: HTMLElement) => JQuery;
                    finishCallback: (any: any) => JQueryPromise<any>;
                    wizardModel: MultiQueryDataSourceWizardModel;
                }
                class MultiQueryConfigurePage extends WizardPage<MultiQueryDataSourceWizardModel> {
                    private _callbacks;
                    private _selectedPath;
                    private _connection;
                    private _itemsProvider;
                    private _customQueries;
                    private _checkedQueries;
                    private _data;
                    private _sqlTextProvider;
                    private _dataSourceClone;
                    private _dataSource;
                    private _dataConnection;
                    private _addQueryAlgorithm;
                    private _addQueryFromTables;
                    private _addQueryFromStoredProcedures;
                    private _addQueryFromCustomQueries;
                    private _getItemsPromise;
                    private _resetDataSourceResult;
                    private _setQueryCore;
                    static pushQuery(newQuery: Data.Utils.ISqlQueryViewModel, node: Internal.TreeLeafNode, queries: ko.ObservableArray<Data.Utils.ISqlQueryViewModel>): void;
                    static removeQuery(queries: ko.ObservableArray<Data.Utils.ISqlQueryViewModel>, node: any): void;
                    constructor(wizard: WizardViewModel<MultiQueryDataSourceWizardModel>, callbacks?: Internal.IDataSourceWizardCallbacks, disableCustomSql?: boolean, rtl?: boolean);
                    template: string;
                    description: any;
                    scrollViewHeight: string;
                    getItemsAfterCheck: (node: Internal.TreeNode) => any;
                    isTablesGenerateColumnsCallBack: ko.ObservableArray<JQueryPromise<any>>;
                    fieldListModel: ko.Observable<DXImport.Analytics.Widgets.Internal.ITreeListOptions>;
                    popupQueryBuilder: Internal.QueryBuilderPopup;
                    customizeDBSchemaTreeListActions: (item: DXImport.Analytics.Utils.IDataMemberInfo, actions: DXImport.Analytics.Utils.IAction[]) => void;
                    popupSelectStatment: {
                        isVisible: ko.Observable<boolean>;
                        title: () => any;
                        query: any;
                        data: ko.Observable<any>;
                        okButtonText: () => any;
                        okButtonHandler: (e: any) => void;
                        aceOptions: {
                            showLineNumbers: boolean;
                            showPrintMargin: boolean;
                            enableBasicAutocompletion: boolean;
                            enableLiveAutocompletion: boolean;
                            readOnly: boolean;
                            highlightSelectedWord: boolean;
                            showGutter: boolean;
                            highlightActiveLine: boolean;
                        };
                        aceAvailable: boolean;
                        additionalOptions: {
                            onChange: (session: any) => void;
                            onValueChange: (editor: any) => void;
                            changeTimeout: number;
                            overrideEditorFocus: boolean;
                            setUseWrapMode: boolean;
                        };
                        languageHelper: {
                            getLanguageMode: () => string;
                            createCompleters: () => any[];
                        };
                        closest(element: HTMLElement, parentSelector: string): JQuery;
                    };
                    showStatementPopup: (query: any) => void;
                    disableCustomSql: boolean;
                    dataSourceClone(): Data.SqlDataSource;
                    AddQueryWithBuilder(): void;
                    runQueryBuilder(): void;
                    hasParametersToEdit: ko.Computed<boolean>;
                    isDataLoadingInProcess: ko.Observable<boolean>;
                    loadPanelViewModel(element: HTMLElement): {
                        animation: {
                            show: {
                                type: string;
                                from: number;
                                to: number;
                                duration: number;
                            };
                            hide: {
                                type: string;
                                from: number;
                                to: number;
                                duration: number;
                            };
                        };
                        deferRendering: boolean;
                        message: any;
                        visible: ko.Observable<boolean>;
                        shading: boolean;
                        shadingColor: string;
                        position: {
                            of: JQuery;
                        };
                        container: JQuery;
                    };
                    setTableQuery(query: Data.TableQuery, isInProcess?: ko.Observable<boolean>): JQueryPromise<QueryBuilder.Utils.ISelectStatementResponse>;
                    setCustomSqlQuery(query: Data.CustomSqlQuery): void;
                    queryEditIndex: ko.Observable<number>;
                    showQbCallBack: (name?: any, isCustomQuery?: boolean) => void;
                    customResetOptions: () => any;
                    beginAsync(data: MultiQueryDataSourceWizardModel): JQueryPromise<any>;
                    commit(data: MultiQueryDataSourceWizardModel): void;
                }
                class MultiQueryConfigureParametersPage extends CommonParametersPage<MultiQueryDataSourceWizardModel> {
                    private parametersConverter;
                    private _selectedPath;
                    private _rootItems;
                    private _createNewParameter;
                    constructor(wizard: any, parametersConverter?: Internal.IParametersViewModelConverter);
                    template: string;
                    description: any;
                    scrollViewHeight: string;
                    fieldListModel: ko.Observable<DXImport.Analytics.Widgets.Internal.ITreeListOptions>;
                    _begin(data: MultiQueryDataSourceWizardModel): void;
                    getParameters(): any;
                    commit(data: MultiQueryDataSourceWizardModel): void;
                }
                class MultiQueryDataSourceWizardModel implements IDataSourceWizardModel {
                    requestWrapper: QueryBuilder.Utils.RequestWrapper;
                    private _queryIndex;
                    sqlDataSource: Data.SqlDataSource;
                    jsonDataSource: Data.JsonDataSource;
                    connectionString: ko.Observable<string>;
                    jsonDataSourceConnectionName: ko.Observable<string>;
                    customQueries: ko.ObservableArray<Data.Utils.ISqlQueryViewModel>;
                    constructor(requestWrapper?: QueryBuilder.Utils.RequestWrapper);
                }
                class SelectOptionalConnectionString<T extends IDataSourceWizardModel> extends WizardPage<T> {
                    availableDataSources: ko.Observable<any[]> | ko.Computed<any[]> | ko.ObservableArray<any>;
                    constructor(wizard: WizardViewModel<T>, availableDataSources: ko.Observable<any[]> | ko.Computed<any[]> | ko.ObservableArray<any>, isDataSourceCreationAvailable: ko.Observable<boolean> | ko.Computed<boolean>);
                    template: string;
                    description: any;
                    selectedDataSource: ko.ObservableArray<DXImport.Analytics.Internal.IDataSourceInfo>;
                    isDataSourceCreationAvailable: ko.Observable<boolean> | ko.Computed<boolean>;
                    dataSourcesListHeight: ko.Computed<number>;
                    dataSourceOperations: {
                        text: any;
                        createNewDataSource: boolean;
                    }[];
                    selectedDataSourceOperation: ko.Observable<{
                        text: any;
                        createNewDataSource: boolean;
                    }>;
                    createNewDataSource: ko.PureComputed<boolean>;
                    _begin(data: T): void;
                    reset(): void;
                    getSelectedDataSource(data?: T): DXImport.Analytics.Internal.IDataSourceInfo[];
                    readonly createNewDataSourceOperationText: any;
                    readonly existingOperationText: any;
                }
                class JsonSelectConnectionString<T extends IDataSourceWizardModel> extends SelectOptionalConnectionString<T> {
                    constructor(wizard: WizardViewModel<T>, jsonDataConnections: ko.ObservableArray<IConnectionStringDefinition>, isDataSourceCreationAvailable: ko.Observable<boolean> | ko.Computed<boolean>);
                    _begin(data: T): void;
                    commit(data: T): void;
                    getSelectedDataSource(data: T): DXImport.Analytics.Internal.IDataSourceInfo[];
                }
                class JsonDataSourceFieldsPage<T extends IDataSourceWizardModel> extends WizardPage<T> {
                    private _rootItems;
                    private _fieldListItemsProvider;
                    private _fieldSelectedPath;
                    private _dataSource;
                    private _subscriptions;
                    private _clear;
                    private _createFieldListCallback;
                    private _isDataSourceChanged;
                    private _getSchemaToDataMemberInfo;
                    private _mapJsonNodesToTreelistItems;
                    private _getNodesByPath;
                    private _getInnerItemsByPath;
                    private _beginInternal;
                    private _createTreeNode;
                    private _createLeafTreeNode;
                    private _resetSelectionRecursive;
                    private _mapJsonSchema;
                    constructor(wizard: WizardViewModel<T>);
                    _begin(data: IDataSourceWizardModel): void;
                    beginAsync(data: IDataSourceWizardModel): JQueryPromise<any>;
                    commit(data: IDataSourceWizardModel): void;
                    reset(): void;
                    template: string;
                    rootElementTitle: any;
                    description: any;
                    rootElementList: ko.Observable<DXImport.Analytics.Utils.IPathRequest[]>;
                    selectedRootElement: ko.Observable<DXImport.Analytics.Utils.IPathRequest>;
                    fieldListModel: DXImport.Analytics.Widgets.Internal.ITreeListOptions;
                }
                class JsonDataSourceJsonSourcePage<T extends IDataSourceWizardModel> extends WizardPage<T> {
                    private _requestWrapper;
                    private _jsonStringSettings;
                    private _jsonUriSetting;
                    constructor(wizard: WizardViewModel<T>);
                    _begin(data: T): void;
                    reset(): void;
                    commit(data: T): void;
                    grid: DXImport.Analytics.Widgets.ObjectProperties;
                    template: string;
                    description: any;
                    jsonSourceTitle: any;
                    sources: Array<IJsonDataSourceType>;
                    _selectedSource: ko.Computed<IJsonDataSourceJsonSourcePageSettings>;
                }
                class ConfigureQueryParametersPage extends CommonParametersPage<DataSourceWizardModel> {
                    private parametersConverter;
                    constructor(wizard: any, parametersConverter?: Internal.IParametersViewModelConverter);
                    template: string;
                    description: any;
                    removeButtonTitle: any;
                    parametersEditorOptions: DXImport.Analytics.Widgets.Internal.ICollectionEditorOptions;
                    getParameters(): any[];
                    _begin(data: DataSourceWizardModel): void;
                    commit(data: DataSourceWizardModel): void;
                }
                class CreateQueryPage extends WizardPage<DataSourceWizardModel> {
                    static QUERY_TEXT: string;
                    static SP_TEXT: string;
                    private _proceduresList;
                    private _selectStatementControl;
                    private _data;
                    private _dataSource;
                    private _connection;
                    private _wrapWizardIndicator;
                    constructor(wizard: WizardViewModel<DataSourceWizardModel>, callbacks?: Internal.IDataSourceWizardCallbacks, disableCustomSql?: boolean, rtl?: boolean);
                    template: string;
                    description: any;
                    queryTypeItems: string[];
                    selectedQueryType: ko.Observable<string> | ko.Computed<string>;
                    queryControl: ko.Observable<Internal.ISqlQueryControl> | ko.Computed<Internal.ISqlQueryControl>;
                    runQueryBuilderBtnText: ko.PureComputed<any>;
                    popupQueryBuilder: Wizard.Internal.QueryBuilderPopup;
                    runQueryBuilder(): void;
                    localizeQueryType(queryTypeString: string): any;
                    _begin(data: DataSourceWizardModel): void;
                    commit(data: DataSourceWizardModel): void;
                }
                class SelectConnectionString<T extends IDataSourceWizardModel> extends WizardPage<T> {
                    private _showPageForSingleConnectionString;
                    constructor(wizard: WizardViewModel<T>, connectionStrings: ko.ObservableArray<IConnectionStringDefinition>, _showPageForSingleConnectionString?: boolean);
                    template: string;
                    description: any;
                    connectionStrings: ko.ObservableArray<IConnectionStringDefinition>;
                    selectedConnectionString: ko.ObservableArray<IConnectionStringDefinition>;
                    _begin(data: T): void;
                    commit(data: T): void;
                }
                class SqlDataSourceWizard extends WizardViewModel<DataSourceWizardModel> {
                    private _wizardModel;
                    private finishCallback;
                    constructor(connectionStrings: IDataSourceWizardConnectionStrings, wizardSettings?: IDataSourceWizardSettings, callbacks?: Internal.IDataSourceWizardCallbacks, disableCustomSql?: boolean, rtl?: boolean);
                    start(wizardModel?: DataSourceWizardModel, finishCallback?: (model: DataSourceWizardModel) => JQueryPromise<boolean>): void;
                    connectionStrings: IDataSourceWizardConnectionStrings;
                    title: any;
                    extendCssClass: string;
                    container: (el: HTMLElement) => JQuery;
                }
                interface IDataSourceWizardModel {
                    sqlDataSource: Data.SqlDataSource;
                    jsonDataSource?: Data.JsonDataSource;
                    jsonDataSourceConnectionName?: ko.Observable<string> | ko.Computed<string>;
                    dataSourceType?: DataSourceType;
                }
                class DataSourceWizardModel implements IDataSourceWizardModel {
                    private _queryIndex;
                    sqlDataSource: Data.SqlDataSource;
                    jsonDataSource: Data.JsonDataSource;
                    jsonDataSourceConnectionName: ko.Observable<any>;
                    sqlQuery: Data.Utils.ISqlQueryViewModel;
                    constructor(dataSource?: Data.SqlDataSource, queryName?: string);
                    getQueryIndex(): number;
                    dataSourceType: DataSourceType;
                }
            }
            module Internal {
                class JsonStringEditor extends DXImport.Analytics.Widgets.Editor {
                    constructor(modelPropertyInfo: DXImport.Analytics.Utils.ISerializationInfo, level: any, parentDisabled: any, textToSearch: any);
                    uploadFile(e: any): void;
                    getUploadTitle(): any;
                    aceEditorHasErrors: ko.Observable<boolean>;
                    aceAvailable: boolean;
                    editorContainer: ko.Observable<any>;
                    _model: ko.Observable<any>;
                    languageHelper: {
                        getLanguageMode: () => string;
                        createCompleters: () => any[];
                    };
                    aceOptions: {
                        showLineNumbers: boolean;
                        highlightActiveLine: boolean;
                        showPrintMargin: boolean;
                        enableBasicAutocompletion: boolean;
                        enableLiveAutocompletion: boolean;
                    };
                    isValid: ko.Computed<any>;
                    additionalOptions: {
                        onChangeAnnotation: (session: any) => void;
                        onBlur: () => void;
                    };
                    jsonStringValidationRules: Array<any>;
                }
            }
            module Internal {
                function getLocalizedValidationErrorMessage(emptyValueErrorMessage: string, localizedPropertyName?: string, subProperty?: string): any;
                interface IJSONSourcePagePropertyDescriptor {
                    value: ko.Observable<any>;
                    displayName: () => string;
                }
                abstract class JsonDataSourceJsonSourcePageSettingsBase extends DXImport.Analytics.Utils.Disposable implements IJsonDataSourceJsonSourceValidatable {
                    dispose(): void;
                    protected _validationGroup: any;
                    protected _validationSummary: any;
                    private _onValidationGroupInitialized;
                    private _onValidationGroupDisposing;
                    private _onValidationSummaryInitialized;
                    private _onValidationSummaryDisposing;
                    protected _repaintSummary(): void;
                    abstract _validatorsReady: ko.Observable<boolean> | ko.Computed<boolean>;
                    _validate(): void;
                    constructor();
                    validationGroup: {
                        onInitialized: (args: any) => void;
                        onDisposing: (args: any) => void;
                        validate: () => void;
                    };
                    validationSummary: {
                        onInitialized: (args: any) => void;
                        onDisposing: (args: any) => void;
                    };
                    isValid: ko.Observable<boolean> | ko.Computed<boolean>;
                    grid: DXImport.Analytics.Widgets.ObjectProperties;
                }
                class JsonDataSourceJsonSourcePageStringSettings extends JsonDataSourceJsonSourcePageSettingsBase implements IJsonDataSourceJsonSourcePageSettings {
                    onChange(_onChange: () => void): any;
                    _validatorsReady: ko.Observable<boolean>;
                    private _isJsonSourceValid;
                    isEmpty(): boolean;
                    reset(): void;
                    setValue(dataSource: Data.JsonDataSource): void;
                    getInfo(): DXImport.Analytics.Utils.ISerializationInfoArray;
                    applySettings(jsonDataSource: Data.JsonDataSource): void;
                    constructor();
                    isValid: ko.Observable<boolean> | ko.Computed<boolean>;
                    validationGroup: any;
                    validationSummary: any;
                    stringSource: ko.Observable<string> | ko.Computed<string>;
                    aceEditorHasErrors: ko.Observable<boolean>;
                    grid: DXImport.Analytics.Widgets.ObjectProperties;
                    cssClass: {
                        'dxrd-wizard-json-string-source-grid': boolean;
                    };
                }
                class JsonDataSourceJsonSourcePageUriSettings extends JsonDataSourceJsonSourcePageSettingsBase implements IJsonDataSourceJsonSourcePageSettings {
                    private _requestWrapper;
                    private _isUriValid;
                    private _lastValidatedJsonSourceJSON;
                    private _authNameValidatorInstance;
                    private _collectionItemNamePlaceholder;
                    private _lastValidateDeferred;
                    private _sourceUriValidatorsReady;
                    private _basicAuthValidatorsReady;
                    private _validationRequested;
                    private _validateUriSource;
                    private _isCollectionValid;
                    private _isHeadersValid;
                    private _isQueryParametersValid;
                    private _isBasicHttpAuthValid;
                    private _noEmptyProperties;
                    private _lastValidationMessage;
                    private _getSerializedUriSource;
                    _sourceUriValidationCallback: (params: any) => boolean;
                    private _getSourceUriInfo;
                    private _getBasicHttpAuthInfo;
                    private _getQueryParametersInfo;
                    private _getHttpHeadersInfo;
                    constructor(_requestWrapper: QueryBuilder.Utils.RequestWrapper);
                    applySettings(jsonDataSource: Data.JsonDataSource): void;
                    getInfo(): DXImport.Analytics.Utils.ISerializationInfoArray;
                    reset(): void;
                    setValue(dataSource: Data.JsonDataSource): void;
                    dispose(): void;
                    onChange(_onChange: () => void): any;
                    isEmpty(): boolean;
                    isValid: ko.PureComputed<boolean>;
                    _validate(): void;
                    _validatorsReady: ko.PureComputed<boolean>;
                    sourceUri: ko.Observable<string>;
                    basicHttpAuth: {
                        password: ko.Observable<string>;
                        userName: ko.Observable<string>;
                    };
                    queryParameters: ko.ObservableArray<Data.JsonQueryParameter>;
                    headers: ko.ObservableArray<Data.JsonHeaderParameter>;
                }
            }
            interface IJsonDataSourceJsonSourcePageSettings extends IJsonDataSourceJsonSourceValidatable {
                isValid(): boolean;
                reset(): void;
                setValue(dataSource: Data.JsonDataSource): void;
                isEmpty(): boolean;
                applySettings(dataSource: Data.JsonDataSource): void;
                cssClass?: string | any;
                grid?: DXImport.Analytics.Widgets.ObjectProperties;
            }
            interface IJsonDataSourceJsonSourceValidatable {
                validationGroup?: {
                    onInitialized: (args: any) => void;
                    validate: () => any;
                    onDisposing: (args: any) => void;
                };
                validationSummary?: {
                    onInitialized: (args: any) => void;
                    onDisposing: (args: any) => void;
                };
                _validatorsReady?: ko.Observable<boolean> | ko.Computed<boolean>;
                _validate?: () => void;
            }
            interface IJsonDataSourceType {
                value: IJsonDataSourceJsonSourcePageSettings;
                displayValue: string;
                localizationId: string;
            }
            module Internal {
                interface IDataSourceWizardCallbacks {
                    selectStatement?: (connection: Data.SqlDataConnection, queryJSON: string) => JQueryPromise<QueryBuilder.Utils.ISelectStatementResponse>;
                    finishCallback?: (wizardModel: any) => JQueryPromise<any>;
                    customQueriesPreset?: (dataSource: Data.SqlDataSource) => JQueryPromise<Data.Utils.ISqlQueryViewModel[]>;
                    customizeQBInitData?: (data: any) => any;
                    validateJsonUri?: (data: any) => any;
                }
                interface IMultiQueryDataSourceWizardCallbacks extends IDataSourceWizardCallbacks {
                    sqlDataSourceResultSchema?: (dataSource: Data.SqlDataSource) => JQueryPromise<{
                        resultSchemaJSON: string;
                        connectionParameters?: string;
                    }>;
                }
                interface IParametersViewModelConverter {
                    createParameterViewModel(parameter: Data.DataSourceParameter): any;
                    getParameterFromViewModel(parameterViewModel: any): Data.DataSourceParameter;
                }
                function subscribeArray<T>(array: ko.ObservableArray<T>, subscribeItem: (value: T, onChange: () => void) => void, onChange: () => void): ko.Subscription;
                function subscribeProperties(properties: Array<ko.Observable<any> | ko.Computed<any>>, onChange: () => void): any[];
                function subscribeObject<T>(object: ko.Observable<T> | ko.Computed<T>, subscribeProperties: (value: T, onChange: () => void) => void, onChange: () => void): ko.Subscription;
                function _createBeforeInitializePageEventArgs<TWizard extends BaseWizard | WizardPageProcessor>(page: _WrappedWizardPage, self: TWizard): IBeforeWizardPageInitializeEventArgs<TWizard>;
                function _createPageEventArgs<TWizard extends BaseWizard | WizardPageProcessor>(page: _WrappedWizardPage, self: TWizard): IWizardPageEventArgs<TWizard>;
                class ParametersTreeListItem extends DXImport.Analytics.Utils.Disposable implements DXImport.Analytics.Utils.IDataMemberInfo {
                    parent: ParametersTreeListRootItem;
                    private _name;
                    constructor(parameter: {
                        name: ko.Observable<string> | ko.Computed<string>;
                    }, parent: ParametersTreeListRootItem);
                    dataSourceParameter: ko.Observable<{
                        name: ko.Observable<string> | ko.Computed<string>;
                    }> | ko.Computed<{
                        name: ko.Observable<string> | ko.Computed<string>;
                    }>;
                    editor: any;
                    isList: boolean;
                    contenttemplate: string;
                    actionsTemplate: string;
                    readonly name: string;
                    readonly displayName: string;
                    query(): Data.Utils.ISqlQueryViewModel;
                }
                class ParametersTreeListRootItem implements DXImport.Analytics.Utils.IDataMemberInfo {
                    private _query;
                    constructor(query: Data.Utils.ISqlQueryViewModel);
                    name: string;
                    displayName: string;
                    isList: boolean;
                    specifics: string;
                    parameters: ko.ObservableArray<ParametersTreeListItem>;
                    removeChild(parameter: any): void;
                    query(): Data.Utils.ISqlQueryViewModel;
                }
                class ParametersTreeListController extends DXImport.Analytics.Widgets.Internal.TreeListController {
                    private _createNewParameter;
                    private _rootItems;
                    constructor(rootItems: ParametersTreeListRootItem[], createNewParameter: (queryName: string, parameters: {
                        name: string;
                    }[]) => any);
                    hasItems(item: DXImport.Analytics.Utils.IDataMemberInfo): boolean;
                    getActions(treeListItem: DXImport.Analytics.Widgets.Internal.TreeListItemViewModel & {
                        data: ParametersTreeListRootItem | ParametersTreeListItem;
                    }): DXImport.Analytics.Utils.IAction[];
                    canSelect(value: DXImport.Analytics.Widgets.Internal.TreeListItemViewModel): boolean;
                }
                interface IAddQueriesTreeListCallbacks {
                    deleteAction: (name: string) => any;
                    showQbCallBack: (name?: string, isCustomQuery?: boolean) => any;
                    disableCustomSql: boolean;
                }
                class DBSchemaItemsProvider implements DXImport.Analytics.Utils.IItemsProvider {
                    private _callBack;
                    private _tables;
                    private _views;
                    private _procedures;
                    private _queries;
                    private _customQueries;
                    private _rootItems;
                    constructor(dbSchemaProvider: Data.DBSchemaProvider, customQueries: ko.ObservableArray<Data.TableQuery>, showQbCallBack: any, disableCustomSql: any, afterCheckToggled: (node: TreeNodeBase) => void);
                    private _checkedRootNodesCount;
                    getItems: (path: DXImport.Analytics.Utils.IPathRequest) => JQueryPromise<DXImport.Analytics.Utils.IDataMemberInfo[]>;
                    hasCheckedItems: ko.PureComputed<boolean>;
                    nextButtonDisabled: ko.PureComputed<boolean>;
                    hasParametersToEdit: ko.PureComputed<boolean>;
                    tables: () => TreeNode;
                    views: () => TreeNode;
                    procedures: () => ParameterTreeNode;
                    queries: () => QueriesTreeNode;
                    customQueries: () => ko.ObservableArray<Data.Utils.ISqlQueryViewModel>;
                }
                class DBSchemaTreeListController extends DXImport.Analytics.Widgets.Internal.TreeListController {
                    private _customizeDBSchemaTreeListActions;
                    constructor(_customizeDBSchemaTreeListActions: (item: DXImport.Analytics.Utils.IDataMemberInfo, actions: DXImport.Analytics.Utils.IAction[]) => void);
                    getActions(value: DXImport.Analytics.Widgets.Internal.TreeListItemViewModel): DXImport.Analytics.Utils.IAction[];
                    canSelect(value: DXImport.Analytics.Widgets.Internal.TreeListItemViewModel): boolean;
                }
                class QueryBuilderPopup {
                    customizeQBInitializationData: (data: any) => any;
                    private _applyQuery;
                    private _query;
                    private _dbSchemaProvider;
                    private _dataSource;
                    private _rtl;
                    constructor(applyNewQuery: IRetrieveQuerySqlCallback, rtl?: boolean, customizeQBInitializationData?: (data: any) => any);
                    designer: ko.Observable<{
                        model: ko.Observable<QueryBuilder.Elements.QueryViewModel>;
                        updateSurface: () => void;
                        showPreview: () => void;
                        dataPreview: any;
                    }>;
                    qbOptions: ko.Observable<any>;
                    okButtonDisabled: ko.PureComputed<boolean>;
                    isVisible: ko.Observable<boolean>;
                    showLoadIndicator: ko.Observable<boolean>;
                    static customizeQueryBuilderActions: (actions: DXImport.Analytics.Utils.IAction[]) => void;
                    show(query: Data.TableQuery, dataSource: Data.SqlDataSource): void;
                    cancelHandler(): void;
                    previewHandler(): void;
                    okHandler(): void;
                    onHiddenHandler(): void;
                    popupViewModel(element: HTMLElement): {
                        visible: ko.Observable<boolean>;
                        title: any;
                        showTitle: boolean;
                        shading: boolean;
                        fullScreen: boolean;
                        width: string;
                        height: string;
                        container: JQuery;
                        position: {
                            of: JQuery;
                        };
                        onHidden: () => void;
                    };
                    getDisplayText(key: any): any;
                    localizationIdMap: {
                        [key: string]: DXImport.Analytics.Internal.ILocalizationInfo;
                    };
                }
                class SelectQuerySqlTextProvider {
                    private _selectStatementCallback;
                    private _connection;
                    constructor(_selectStatementCallback: (connection: Data.SqlDataConnection, queryJSON: string) => JQueryPromise<QueryBuilder.Utils.ISelectStatementResponse>, _connection: () => Data.SqlDataConnection);
                    getQuerySqlText(newQuery: Data.TableQuery): JQueryPromise<QueryBuilder.Utils.ISelectStatementResponse>;
                }
                interface ISqlQueryControl {
                    isNextDisabled: ko.Observable<boolean> | ko.Computed<boolean>;
                    isFinishDisabled: ko.Observable<boolean> | ko.Computed<boolean>;
                    setQuery: (query: Data.Utils.ISqlQueryViewModel, isInProcess?: ko.Observable<boolean>) => void;
                    getQuery: () => Data.Utils.ISqlQueryViewModel;
                    runQueryBuilderDisabled: boolean;
                }
                class SelectStatementQueryControl implements ISqlQueryControl {
                    private _tableQueryString;
                    private _query;
                    private _needToCustomizeParameters;
                    private _sqlTextProvider;
                    constructor(sqlTextProvider: SelectQuerySqlTextProvider, disableCustomSql: any);
                    template: string;
                    aceOptions: {
                        showLineNumbers: boolean;
                        showPrintMargin: boolean;
                        enableBasicAutocompletion: boolean;
                        enableLiveAutocompletion: boolean;
                        readOnly: boolean;
                        highlightSelectedWord: boolean;
                        showGutter: boolean;
                        highlightActiveLine: boolean;
                    };
                    additionalOptions: {
                        onChange: (session: any) => void;
                        onValueChange: (editor: any) => void;
                        changeTimeout: number;
                        overrideEditorFocus: boolean;
                        setUseWrapMode: boolean;
                    };
                    aceAvailable: boolean;
                    languageHelper: {
                        getLanguageMode: () => string;
                        createCompleters: () => any[];
                    };
                    caption: () => any;
                    sqlString: ko.PureComputed<string>;
                    setQuery(query: Data.Utils.ISqlQueryViewModel, isInProcess?: ko.Observable<boolean>): JQueryPromise<QueryBuilder.Utils.ISelectStatementResponse>;
                    getQuery(): Data.Utils.ISqlQueryViewModel;
                    isNextDisabled: ko.Observable<boolean> | ko.Computed<boolean>;
                    isFinishDisabled: ko.Observable<boolean> | ko.Computed<boolean>;
                    readonly runQueryBuilderDisabled: boolean;
                    disableCustomSql: () => boolean;
                }
                class StoredProceduresQueryControl extends DXImport.Analytics.Utils.Disposable implements ISqlQueryControl {
                    private _query;
                    private _needToProcessParameters;
                    private static _availableConvertToParameter;
                    private _selectedProcedure;
                    constructor();
                    template: string;
                    storedProcedures: ko.ObservableArray<Data.DBStoredProcedure>;
                    selectedProcedure: ko.ObservableArray<Data.DBStoredProcedure>;
                    caption: () => any;
                    generateStoredProcedureDisplayName: (procedure: any) => string;
                    scrollActiveItem(e: any): void;
                    static generateStoredProcedureDisplayName(procedure: Data.DBStoredProcedure): string;
                    setQuery(query: Data.StoredProcQuery): void;
                    getQuery(): Data.StoredProcQuery;
                    isNextDisabled: ko.Observable<boolean> | ko.Computed<boolean>;
                    isFinishDisabled: ko.Observable<boolean> | ko.Computed<boolean>;
                    readonly runQueryBuilderDisabled: boolean;
                }
                var defaultObjectDataSourceItemSpecifics: string;
                class TreeNodeBase implements DXImport.Analytics.Utils.IDataMemberInfo {
                    name: string;
                    displayName: string;
                    specifics: string;
                    private _afterCheckToggled;
                    constructor(name: string, displayName: string, specifics: string, isChecked?: boolean, afterCheckToggled?: (node: TreeNodeBase) => void);
                    checked: ko.PureComputed<boolean>;
                    unChecked(): boolean;
                    toggleChecked(): void;
                    setChecked(value: boolean): void;
                    isList: boolean;
                    _checked: ko.Observable<boolean> | ko.Computed<boolean>;
                }
                class TreeLeafNode extends TreeNodeBase {
                    name: string;
                    displayName: string;
                    specifics: string;
                    constructor(name: string, displayName: string, specifics: string, isChecked?: boolean, nodeArguments?: any, afterCheckToggled?: (node: TreeNodeBase) => void);
                    arguments: Data.DBStoredProcedureArgument[];
                    hasQuery: boolean;
                }
                class TreeNode extends TreeNodeBase {
                    constructor(name: string, displayName: string, specifics: string, isChecked: boolean, afterCheckToggled?: (node: TreeNodeBase) => void);
                    initialized(): boolean;
                    setChecked(value: boolean): void;
                    initializeChildren(children: TreeNodeBase[]): void;
                    countChecked: ko.PureComputed<number>;
                    isList: boolean;
                    children: ko.ObservableArray<TreeNodeBase>;
                }
                class ParameterTreeNode extends TreeNode {
                    constructor(name: string, displayName: string, specifics: string, isChecked: boolean, afterCheckToggled?: (node: TreeNodeBase) => void);
                    countChecked: ko.PureComputed<number>;
                    hasParamsToEdit: ko.Observable<boolean>;
                }
                class QueriesTreeNode extends ParameterTreeNode {
                    constructor(name: string, displayName: string, specifics: string, isChecked: boolean, callbacks?: ko.Observable<IAddQueriesTreeListCallbacks>, afterCheckToggled?: (node: TreeNodeBase) => void);
                    addAction: {
                        clickAction: (item: any) => any;
                        imageClassName: string;
                        imageTemplateName: string;
                        templateName: string;
                        text: any;
                    };
                    getActions(context: any): DXImport.Analytics.Utils.IAction[];
                    popoverListItems(): any;
                    showPopover(): void;
                    itemClickAction: (e: {
                        itemData: {
                            name: string;
                            addAction: any;
                        };
                    }) => void;
                    addQuery: any;
                    addCustomQuery: any;
                    popoverVisible: ko.Observable<boolean> | ko.Computed<boolean>;
                    disableCustomSql: () => boolean;
                }
                class TreeQueryNode extends TreeLeafNode {
                    private _name;
                    constructor(name: string, displayName: string, specifics: string, isChecked: boolean, parameters: ko.Observable<Data.DataSourceParameter[]>, callbacks: ko.Observable<IAddQueriesTreeListCallbacks>, afterCheckToggled?: (node: TreeLeafNode) => void);
                    setObservableName(name: ko.Observable<string> | ko.Computed<string>): void;
                    editAction: {
                        clickAction: (item: any) => any;
                        imageClassName: string;
                        imageTemplateName: string;
                        text: any;
                    };
                    removeAction: {
                        clickAction: (item: any) => void;
                        imageClassName: string;
                        imageTemplateName: string;
                        text: any;
                    };
                    getActions(context: any): DXImport.Analytics.Utils.IAction[];
                    editQuery: any;
                    removeQuery: any;
                    parameters: ko.Observable<Data.DataSourceParameter[]>;
                }
                class FieldTreeNode extends Analytics.Wizard.Internal.TreeNodeBase {
                    constructor(name: string, displayName: string, specifics: string, isChecked: boolean, path: string, afterCheckToggled?: (node: Analytics.Wizard.Internal.TreeNodeBase) => void);
                    path: string;
                    visible: ko.Observable<boolean>;
                    isComplex: boolean;
                }
                class DataMemberTreeNode extends Analytics.Wizard.Internal.TreeNode {
                    constructor(name: string, displayName: string, specifics: string, isChecked: boolean, path: string, afterCheckToggled?: (node: DataMemberTreeNode) => void);
                    setChecked(value: boolean): void;
                    path: string;
                    visible: ko.Observable<boolean>;
                    children: ko.ObservableArray<DataMemberTreeNode | FieldTreeNode>;
                    isComplex: boolean;
                }
                class TreeNodeItemsProvider extends DXImport.Analytics.Utils.Disposable implements DXImport.Analytics.Utils.IItemsProvider {
                    private _fullTreeLoaded;
                    protected _rootItems: ko.ObservableArray<Internal.DataMemberTreeNode>;
                    private _checkedRootNodesCount;
                    private _createTree;
                    private _createTreePart;
                    private _setChecked;
                    selectAllItems(onlyRoot?: boolean): JQueryPromise<{}>;
                    selectItemsByPath(path: string): JQueryPromise<{}>;
                    selectItemByPath(path: string): JQueryPromise<{}>;
                    protected _getParentNode(pathRequest: DXImport.Analytics.Utils.IPathRequest): DataMemberTreeNode;
                    protected _getDefaultTreeNodeCheckState(item: DXImport.Analytics.Utils.IDataMemberInfo): boolean;
                    constructor(fieldListProvider: DXImport.Analytics.Internal.FieldListProvider, rootItems: ko.ObservableArray<DXImport.Analytics.Utils.IDataMemberInfo>, generateTreeNode: (item: DXImport.Analytics.Utils.IDataMemberInfo, isChecked: boolean, path: string) => Internal.DataMemberTreeNode, generateTreeLeafNode: (item: DXImport.Analytics.Utils.IDataMemberInfo, isChecked: boolean, path: string) => Internal.FieldTreeNode);
                    hasCheckedItems: ko.Computed<boolean>;
                    getItems: (path: DXImport.Analytics.Utils.IPathRequest, collectChilds?: boolean) => JQueryPromise<DXImport.Analytics.Utils.IDataMemberInfo[]>;
                    getRootItems: () => DataMemberTreeNode[];
                }
                class JsonTreeNodeItemsProvider extends TreeNodeItemsProvider implements DXImport.Analytics.Utils.IItemsProvider {
                    constructor(fieldListProvider: DXImport.Analytics.Internal.FieldListProvider, rootItems: ko.ObservableArray<DXImport.Analytics.Utils.IDataMemberInfo>, generateTreeNode: (item: DXImport.Analytics.Utils.IDataMemberInfo, isChecked: boolean, path: string) => Internal.DataMemberTreeNode, generateTreeLeafNode: (item: DXImport.Analytics.Utils.IDataMemberInfo, isChecked: boolean, path: string) => Internal.FieldTreeNode);
                    protected _getDefaultTreeNodeCheckState(item: DXImport.Analytics.Utils.IDataMemberInfo): boolean;
                    getNodeByPath(pathRequest: DXImport.Analytics.Utils.IPathRequest): DataMemberTreeNode;
                }
            }
            module Legacy {
                interface IPageInfo<TWizardModel> {
                    page: WizardPage<TWizardModel>;
                    token: any;
                    isFirst: boolean;
                    isLast: boolean;
                }
                interface IPageDispatcher<TWizardModel> {
                    getFirstPage(model: TWizardModel): JQueryPromise<IPageInfo<TWizardModel>>;
                    getNextPage(currentToken: any, model: TWizardModel): JQueryPromise<IPageInfo<TWizardModel>>;
                    getPreviousPage(currentToken: any, model: TWizardModel): JQueryPromise<IPageInfo<TWizardModel>>;
                    getPageByIndex(index: number, model: TWizardModel): JQueryPromise<IPageInfo<TWizardModel>>;
                }
                class LegacyPageDispathcer<TWizardModel> implements IPageDispatcher<TWizardModel> {
                    private _steps;
                    constructor(steps: WizardPage<TWizardModel>[]);
                    getFirstPage(model: TWizardModel): JQueryPromise<any>;
                    getNextPage(currentToken: any, model: TWizardModel): JQueryPromise<any>;
                    getPreviousPage(currentToken: any, model: TWizardModel): JQueryPromise<any>;
                    getPageByIndex(index: number, model: TWizardModel): JQueryPromise<any>;
                    private _isPageFirst;
                    private _isPageLast;
                    private _goToFirstVisiblePage;
                }
            }
            module Internal {
                class WizardAction {
                    constructor(handler: () => void, text: string);
                    isVisible: ko.Observable<boolean> | ko.Computed<boolean>;
                    isDisabled: ko.Observable<boolean> | ko.Computed<boolean>;
                    handler: () => void;
                    text: string;
                }
            }
        }
    }
    module QueryBuilder {
        module Widgets {
            var expressionFunctions: DXImport.Analytics.Widgets.Internal.IExpressionEditorFunction[];
            module Internal {
                class UndoEditor extends DXImport.Analytics.Widgets.Editor {
                    constructor(info: DXImport.Analytics.Utils.ISerializationInfo, level: any, parentDisabled?: ko.Computed<boolean>, textToSearch?: any);
                    generateValue(undoEngine: ko.Observable<DXImport.Analytics.Utils.UndoEngine> | ko.Computed<DXImport.Analytics.Utils.UndoEngine>): ko.Computed<any> | ko.Observable<any>;
                    undoValue: ko.Observable | ko.Computed;
                }
            }
            var editorTemplates: {
                bool: {
                    header: string;
                    custom: string;
                };
                combobox: {
                    header: string;
                    custom: string;
                };
                comboboxUndo: {
                    header: string;
                    custom: string;
                    editorType: typeof Internal.UndoEditor;
                };
                text: {
                    header: string;
                    custom: string;
                };
                filterEditor: {
                    header: string;
                    custom: string;
                };
                filterGroupEditor: {
                    header: string;
                    custom: string;
                };
                numeric: {
                    header: string;
                    custom: string;
                };
            };
            module Internal {
                function createDefaultSQLAceOptions(readOnly?: boolean): {
                    showLineNumbers: boolean;
                    showPrintMargin: boolean;
                    enableBasicAutocompletion: boolean;
                    enableLiveAutocompletion: boolean;
                    readOnly: boolean;
                    highlightSelectedWord: boolean;
                    showGutter: boolean;
                    highlightActiveLine: boolean;
                };
                function createDefaultSQLAdditionalOptions(value: any): {
                    onChange: (session: any) => void;
                    onValueChange: (editor: any) => void;
                    changeTimeout: number;
                    overrideEditorFocus: boolean;
                    setUseWrapMode: boolean;
                };
                function createDefaultSQLLanguageHelper(): {
                    getLanguageMode: () => string;
                    createCompleters: () => any[];
                };
                class GroupFilterEditorSerializer extends DXImport.Analytics.Widgets.Internal.FilterEditorSerializer {
                    private _columns;
                    private _columnDisplayName;
                    private _findAggregatedColumn;
                    private _aggregatePropertyName;
                    constructor(_columns: () => Elements.ColumnExpression[]);
                    serializeOperandProperty(operand: DXImport.Analytics.Criteria.OperandProperty): string;
                    deserialize(stringCriteria: string): DXImport.Analytics.Criteria.CriteriaOperator;
                }
                class OperandParameterQBSurface extends DXImport.Analytics.Widgets.Filtering.OperandParameterSurface {
                    static defaultDisplay: () => any;
                    private readonly _parameterType;
                    constructor(operator: DXImport.Analytics.Criteria.OperandParameter, parent: any, fieldListProvider?: any, path?: any);
                    _createParameter(name: string, dataType: string): void;
                    createParameter: () => void;
                    fieldListProvider: ko.Observable<QueryBuilderObjectsProvider>;
                    _parameterName: ko.Observable<string>;
                    isEditable: ko.Observable<boolean> | ko.Computed<boolean>;
                    fieldsOptions: any;
                    helper: QBFilterEditorHelper;
                    canCreateParameters: boolean;
                    isDefaultTextDisplayed(): boolean;
                    defaultDisplay: () => any;
                }
                class OperandPropertyQBSurface extends DXImport.Analytics.Widgets.Filtering.OperandPropertySurface {
                    _updateSpecifics(): void;
                    constructor(operator: DXImport.Analytics.Criteria.OperandProperty, parent: any, fieldListProvider?: QueryBuilderObjectsProvider, path?: any);
                    fieldListProvider: ko.Observable<QueryBuilderObjectsProvider>;
                    static updateSpecifics(propertySurface: {
                        fieldListProvider: ko.Observable<{
                            getColumnInfo: (path: string) => DXImport.Analytics.Utils.IDataMemberInfo;
                        }>;
                        propertyName: ko.Observable<string>;
                        specifics: ko.Observable<string>;
                        dataType: ko.Observable<string>;
                        fieldsOptions?: ko.Observable<{
                            selected: ko.Observable<any>;
                        }>;
                    }): void;
                }
                function isAggregatedExpression(object: {
                    aggregate: ko.Observable<string> | ko.Computed<string>;
                }): boolean;
                interface IQueryBuilderObjectProviderFilter {
                    filterTables(tables: Elements.TableViewModel[]): Elements.TableViewModel[];
                    filterColumns(columns: Elements.ColumnViewModel[]): Elements.ColumnViewModel[];
                    getColumnName(column: Elements.ColumnViewModel): string;
                    getSpecifics(column: Elements.ColumnViewModel): string;
                    getDataType(column: Elements.ColumnViewModel): string;
                }
                class QueryBuilderObjectsProvider implements DXImport.Analytics.Utils.IItemsProvider {
                    constructor(query: ko.Observable<Elements.QueryViewModel>, objectFilter: IQueryBuilderObjectProviderFilter);
                    hasParameter: (name: string) => boolean;
                    createParameter: (name: any, dataType: any) => void;
                    getItems: (IPathRequest: any) => JQueryPromise<DXImport.Analytics.Utils.IDataMemberInfo[]>;
                    getColumnInfo: (propertyName: string) => DXImport.Analytics.Utils.IDataMemberInfo;
                    private static _createTableInfo;
                    private static _createColumnInfo;
                    static whereClauseObjectsFilter: IQueryBuilderObjectProviderFilter;
                    static groupByObjectsFilter: IQueryBuilderObjectProviderFilter;
                }
                class QBFilterEditorHelper extends DXImport.Analytics.Widgets.FilterEditorHelper {
                    constructor(parametersMode: string);
                    newParameters: ko.ObservableArray<Analytics.Data.DataSourceParameter>;
                }
                var QBFilterEditorHelperDefault: typeof QBFilterEditorHelper;
                class QBFilterStringOptions extends DXImport.Analytics.Widgets.FilterStringOptions {
                    constructor(filterString: ko.Observable<string> | ko.Computed<string>, dataMember?: ko.Observable | ko.Computed, disabled?: ko.Observable<boolean> | ko.Computed<boolean>, title?: {
                        text: string;
                        localizationId?: string;
                    });
                    initializeFilterStringHelper(parameters: ko.ObservableArray<Elements.ParameterViewModel> | ko.Computed<Elements.ParameterViewModel[]>, parametersMode: string, serializer?: DXImport.Analytics.Widgets.Internal.FilterEditorSerializer): void;
                    helper: QBFilterEditorHelper;
                }
                class KeyColumnSurface {
                    private _isMaster;
                    constructor(column: ko.Observable<string> | ko.Computed<string>, queryName: string, _isMaster?: boolean);
                    getTitle: () => any;
                    isSelected: ko.Observable<boolean> | ko.Computed<boolean>;
                    setColumn: (resultColumn: {
                        name: ko.Observable<string> | ko.Computed<string>;
                        propertyType: ko.Observable<string> | ko.Computed<string>;
                    }) => void;
                    queryName: string;
                    column: ko.Observable<string> | ko.Computed<string>;
                    selectColumnText: () => any;
                }
                class MasterDetailEditor {
                    private _createMainPopupButtons;
                    constructor(relations: ko.ObservableArray<Analytics.Data.MasterDetailRelation>, resultSet: Analytics.Data.ResultSet, saveCallBack: () => JQueryPromise<{}>);
                    isValid: ko.Observable<boolean>;
                    save: () => void;
                    popupVisible: ko.Observable<boolean>;
                    loadPanelVisible: ko.Observable<boolean>;
                    buttonItems: any[];
                    popupService: DXImport.Analytics.Internal.PopupService;
                    masterQueries: ko.ObservableArray<MasterQuerySurface>;
                    createRelation: (target: MasterQuerySurface) => any;
                    setColumn: (target: KeyColumnSurface) => any;
                    title(): any;
                }
                class MasterDetailEditorPopupManager {
                    private _popupService;
                    private _action;
                    private _popupItems;
                    private _updateActions;
                    constructor(target: any, popupService: DXImport.Analytics.Internal.PopupService, action: string, popupItems: {
                        name: any;
                    }[]);
                    target: any;
                    showPopup: (_: any, element: any) => void;
                }
                class MasterDetailRelationSurface {
                    constructor(relation: Analytics.Data.MasterDetailRelation, parent: MasterQuerySurface);
                    relationName: ko.Observable<string> | ko.Computed<string>;
                    keyColumns: ko.Computed<{
                        master: KeyColumnSurface;
                        detail: KeyColumnSurface;
                    }[]>;
                    isEditable: ko.Observable<boolean> | ko.Computed<boolean>;
                    create: () => void;
                    remove: (data: {
                        master: KeyColumnSurface;
                        detail: KeyColumnSurface;
                    }) => void;
                }
                class MasterQuerySurface {
                    constructor(masterQueryName: string, relations: ko.ObservableArray<Analytics.Data.MasterDetailRelation>);
                    queryName: string;
                    relations: ko.ObservableArray<MasterDetailRelationSurface>;
                    create: (detailQueryItem: {
                        name: string;
                    }) => void;
                    add: (relation: Analytics.Data.MasterDetailRelation) => void;
                    remove: (relationSurface: MasterDetailRelationSurface) => void;
                }
            }
        }
        module Metadata {
            var name: DXImport.Analytics.Utils.ISerializationInfo;
            var alias: DXImport.Analytics.Utils.ISerializationInfo;
            var text: DXImport.Analytics.Utils.ISerializationInfo;
            var selected: DXImport.Analytics.Utils.ISerializationInfo;
            var size: DXImport.Analytics.Utils.ISerializationInfo;
            var location: DXImport.Analytics.Utils.ISerializationInfo;
            var sizeLocation: DXImport.Analytics.Utils.ISerializationInfoArray;
            var unknownSerializationsInfo: DXImport.Analytics.Utils.ISerializationInfoArray;
        }
        module Utils {
            var ActionId: {
                Save: string;
                DataPreview: string;
                SelectStatementPreview: string;
            };
            var HandlerUri: string;
        }
        module Internal {
            function updateQueryBuilderSurfaceContentSize($root: JQuery, surfaceSize: ko.Observable<number> | ko.Computed<number>, surface: ko.Observable<Elements.QuerySurface>, updateLayoutCallbacks?: Array<() => void>): () => void;
            function createIsLoadingFlag(model: ko.Observable<Elements.QueryViewModel>, dbSchemaProvider: ko.Observable<Analytics.Data.IDBSchemaProvider>): ko.PureComputed<boolean>;
            var isJoinsResolvingDisabled: boolean;
            function createQueryBuilder(element: Element, data: {
                querySource: ko.Observable<{}>;
                dbSchemaProvider: ko.Observable<Analytics.Data.DBSchemaProvider>;
                parametersMode?: string;
                parametersItemsProvider?: DXImport.Analytics.Utils.IItemsProvider;
                requestWrapper?: Utils.RequestWrapper;
            }, callbacks?: DXImport.Analytics.Internal.ICommonCustomizationHandler, localization?: any, rtl?: boolean): JQueryDeferred<any>;
            interface ITabModel {
                editableObject: any;
                properties: DXImport.Analytics.Widgets.ObjectProperties;
            }
            interface IItemPropertiesTabModel extends ITabModel {
                fieldListModel: {
                    treeListOptions: ko.Observable<DXImport.Analytics.Widgets.Internal.ITreeListOptions> | ko.Computed<DXImport.Analytics.Widgets.Internal.ITreeListOptions>;
                };
                searchName: ko.Observable<string> | ko.Computed<string>;
                tablesTop: ko.Observable<number> | ko.Computed<number>;
                searchPlaceholder: () => string;
            }
            class AccordionTabInfo extends DXImport.Analytics.Utils.TabInfo {
                static _getSelectedItemPropertyName(model: any): string;
                static _createWrappedObject(query: any, commonModel: any, undoEngine: any, showParameters: boolean): {
                    selectedItem: any;
                    query: {
                        editableObject: any;
                        properties: DXImport.Analytics.Widgets.ObjectProperties;
                    };
                    fields: any;
                    isPropertyVisible: (propertyName: string) => boolean;
                };
                static _createGroups(editableObject: ko.Observable<any>, showParameters: boolean): DXImport.Analytics.Internal.GroupObject;
                static _createQBPropertyGrid(query: ko.Observable<Elements.QueryViewModel> | ko.Computed<Elements.QueryViewModel>, commonModel: IItemPropertiesTabModel, undoEngine: ko.Observable<DXImport.Analytics.Utils.UndoEngine> | ko.Computed<DXImport.Analytics.Utils.UndoEngine>, showParameters: boolean): DXImport.Analytics.Internal.ControlProperties;
                private _getGroupByName;
                constructor(query: ko.Observable<Elements.QueryViewModel> | ko.Computed<Elements.QueryViewModel>, itemPropertiesTabInfoModel: IItemPropertiesTabModel, undoEngine: ko.Observable<DXImport.Analytics.Utils.UndoEngine> | ko.Computed<DXImport.Analytics.Utils.UndoEngine>, focused: ko.Observable | ko.Computed, showParameters: boolean);
                model: DXImport.Analytics.Internal.ControlProperties;
            }
            class ColumnExpressionCollectionHelper {
                static find(collection: ko.ObservableArray<Elements.ColumnExpression>, tableName: string, columnName: string): Elements.ColumnExpression;
                static findByName(collection: ko.ObservableArray<Elements.ColumnExpression>, actualName: string): Elements.ColumnExpression;
                static removeDependend(collection: ko.ObservableArray<Elements.ColumnExpression>, tableName: string): void;
                static setUniqueAlias(collection: any, alias: any): any;
                static addNew(query: Elements.QueryViewModel, collection: ko.ObservableArray<Elements.ColumnExpression>, table: string, column: string): Elements.ColumnExpression;
                static remove(collection: ko.ObservableArray<Elements.ColumnExpression>, tableName: string, columnName: string): void;
            }
        }
        module Utils {
            var controlsFactory: DXImport.Analytics.Utils.ControlsFactory;
        }
        module Internal {
            function registerControls(): void;
            class QueryBuilderTreeListController extends DXImport.Analytics.Widgets.Internal.TreeListController {
                constructor(undoEngine: ko.Observable<DXImport.Analytics.Utils.UndoEngine>, query: ko.Observable<Elements.QueryViewModel>, searchName: ko.Observable<string> | ko.Computed<string>);
                dblClickHandler: (item: DXImport.Analytics.Widgets.Internal.TreeListItemViewModel) => void;
                searchName: ko.Observable<string> | ko.Computed<string>;
            }
        }
        module Utils {
            interface ISelectStatementResponse {
                sqlSelectStatement: string;
                errorMessage: string;
            }
            interface IUriJsonSourceValidationResult {
                isUriValid: boolean;
                faultMessage?: string;
            }
            class RequestWrapper {
                sendRequest<T = any>(action: string, arg: string): JQueryPromise<T>;
                _sendRequest<T = any>(settings: DXImport.Analytics.Internal.IAjaxSettings): JQueryPromise<T>;
                getDbSchema(connection: Analytics.Data.SqlDataConnection, tables?: Analytics.Data.DBTable[]): JQueryPromise<{
                    dbSchemaJSON: string;
                }>;
                getDbStoredProcedures(connection: Analytics.Data.SqlDataConnection): JQueryPromise<{
                    dbSchemaJSON: string;
                }>;
                getSelectStatement(connection: Analytics.Data.SqlDataConnection, queryJSON: string): JQueryPromise<ISelectStatementResponse>;
                getDataPreview(connection: Analytics.Data.SqlDataConnection, queryJSON: string): JQueryPromise<{
                    dataPreviewJSON: string;
                }>;
                getConnectionJSON(connection: Analytics.Data.SqlDataConnection): string;
                rebuildResultSchema(dataSource: Analytics.Data.SqlDataSource, queryName?: string, relationsEditing?: boolean): JQueryPromise<{
                    resultSchemaJSON: string;
                    connectionParameters?: string;
                }>;
                validateJsonUri(jsonDataSource: Analytics.Data.JsonDataSource): JQueryPromise<IUriJsonSourceValidationResult>;
                saveJsonSource(connectionName: string, jsonDataSource: Analytics.Data.JsonDataSource): JQueryPromise<string>;
                getJsonSchema(jsonDataSource: Analytics.Data.JsonDataSource): JQueryPromise<{
                    jsonSchemaJSON: string;
                }>;
            }
        }
        module Internal {
            function wrapGetSelectStatement(callback?: (connection: Analytics.Data.SqlDataConnection, queryJSON: string) => JQueryPromise<Utils.ISelectStatementResponse>): (connection: Analytics.Data.SqlDataConnection, queryJSON: string) => JQueryPromise<Utils.ISelectStatementResponse>;
            function wrapRebuildResultSchema(callback?: (dataSource: Analytics.Data.SqlDataSource, queryName?: string, relationsEditing?: boolean) => JQueryPromise<{
                resultSchemaJSON: string;
                connectionParameters?: string;
            }>): (dataSource: Analytics.Data.SqlDataSource, queryName?: string, relationsEditing?: boolean) => JQueryPromise<{
                resultSchemaJSON: string;
                connectionParameters?: string;
            }>;
        }
        module Elements {
            class QueryElementBaseViewModel extends DXImport.Analytics.Elements.ElementViewModel {
                getControlFactory(): DXImport.Analytics.Utils.ControlsFactory;
                constructor(control: any, parent: DXImport.Analytics.Elements.ElementViewModel, serializer?: DXImport.Analytics.Utils.IModelSerializer);
                size: DXImport.Analytics.Elements.Size;
                location: DXImport.Analytics.Elements.Point;
            }
            module Metadata {
                var allColumnsSerializationInfo: DXImport.Analytics.Utils.ISerializationInfoArray;
            }
            class AllColumnsViewModel extends QueryElementBaseViewModel {
                constructor(parent: TableViewModel, serializer?: DXImport.Analytics.Utils.IModelSerializer);
                selected: ko.Observable<boolean> | ko.Computed<boolean>;
                name: ko.Computed<string>;
                getInfo(): DXImport.Analytics.Utils.ISerializationInfoArray;
            }
            class AllColumnsSurface extends DXImport.Analytics.Elements.SurfaceElementBase<AllColumnsViewModel> {
                constructor(control: AllColumnsViewModel, context: DXImport.Analytics.Elements.ISurfaceContext);
                template: string;
                toggleSelected: () => void;
                selectedWrapper: ko.PureComputed<boolean>;
                isOverAsterisk: ko.PureComputed<boolean>;
                cssClasses: () => {
                    'dxd-state-active': ko.Computed<boolean> | ko.Observable<boolean>;
                    'dxd-state-hovered': boolean;
                };
            }
            module Metadata {
                var AggregationType: {
                    None: string;
                    Count: string;
                    Max: string;
                    Min: string;
                    Avg: string;
                    Sum: string;
                    CountDistinct: string;
                    AvgDistinct: string;
                    SumDistinct: string;
                };
                var columnSerializationInfo: DXImport.Analytics.Utils.ISerializationInfoArray;
            }
            class ColumnViewModel extends QueryElementBaseViewModel {
                private _isAliasAutoGenerated;
                getInfo(): DXImport.Analytics.Utils.ISerializationInfoArray;
                constructor(model: any, dbColumn: Analytics.Data.DBColumn, parent: TableViewModel, serializer?: DXImport.Analytics.Utils.IModelSerializer);
                isPropertyDisabled(name: string): boolean;
                name: ko.Observable<string> | ko.Computed<string>;
                alias: ko.Observable<string> | ko.Computed<string>;
                selected: ko.Observable<boolean> | ko.Computed<boolean>;
                actualName: ko.Computed<string>;
                displayType: ko.Computed<string>;
                dataType: ko.Computed<string>;
                rightConnectionPoint: Analytics.Diagram.IConnectingPoint;
                leftConnectionPoint: Analytics.Diagram.IConnectingPoint;
                sortingType: ko.Computed<string>;
                sortOrder: ko.Computed<number>;
                groupBy: ko.Computed<boolean>;
                aggregate: ko.Observable<string> | ko.Computed<string>;
                readonly specifics: "String" | "Date" | "Bool" | "Integer" | "Float";
            }
            module Metadata {
                var ColumnType: {
                    RecordsCount: string;
                    Column: string;
                    Expression: string;
                    AllColumns: string;
                };
                var columnExpressionSerializationsInfo: ({
                    propertyName: string;
                    modelName: string;
                    defaultVal?: undefined;
                } | {
                    propertyName: string;
                    modelName: string;
                    defaultVal: string;
                } | {
                    propertyName: string;
                    modelName: string;
                    defaultVal: boolean;
                })[];
            }
            class ColumnExpression {
                private _criteria;
                private _dependedTables;
                constructor(model: any, query: QueryViewModel, serializer?: DXImport.Analytics.Utils.IModelSerializer);
                table: ko.Computed<string>;
                column: ko.Observable<string> | ko.Computed<string>;
                expression: ko.Observable<string> | ko.Computed<string>;
                aggregate: ko.Observable<string> | ko.Computed<string>;
                alias: ko.Observable<string> | ko.Computed<string>;
                descending: ko.Observable<boolean> | ko.Computed<boolean>;
                itemType: ko.Observable<string> | ko.Computed<string>;
                actualName(): string;
                getInfo(): DXImport.Analytics.Utils.ISerializationInfoArray;
                isDepended(tableActualName: string): boolean;
            }
            class ColumnSurface extends DXImport.Analytics.Elements.SurfaceElementBase<ColumnViewModel> {
                private _isJoined;
                private _isHovered;
                constructor(control: ColumnViewModel, context: DXImport.Analytics.Elements.ISurfaceContext);
                template: string;
                toggleSelected: () => void;
                selectedWrapper: ko.PureComputed<boolean>;
                isAggregate: ko.PureComputed<boolean>;
                isAscending: ko.PureComputed<boolean>;
                isDescending: ko.PureComputed<boolean>;
                cssClasses: (query: QuerySurface, columnDragHandler: {
                    getDragColumn: () => ColumnViewModel;
                }, parent: TableSurface) => {
                    'dxd-state-active': boolean;
                    'dxd-state-joined': ko.Computed<boolean>;
                    'dxd-state-hovered': ko.Computed<boolean>;
                };
            }
            module Metadata {
                var ConditionType: {
                    Equal: string;
                    NotEqual: string;
                    Greater: string;
                    GreaterOrEqual: string;
                    Less: string;
                    LessOrEqual: string;
                };
                var joinConditionSerializationInfo: DXImport.Analytics.Utils.ISerializationInfoArray;
            }
            class JoinConditionViewModel extends Analytics.Diagram.RoutedConnectorViewModel {
                getControlFactory(): DXImport.Analytics.Utils.ControlsFactory;
                preInitProperties(): void;
                constructor(control: any, relation: RelationViewModel, serializer?: DXImport.Analytics.Utils.ModelSerializer);
                parentColumn: ko.Computed<ColumnViewModel>;
                nestedColumn: ko.Computed<ColumnViewModel>;
                parentColumnName: ko.Observable<string> | ko.Computed<string>;
                nestedColumnName: ko.Observable<string> | ko.Computed<string>;
                operator: ko.Observable<string> | ko.Computed<string>;
                joinType: ko.Observable<string> | ko.Computed<string>;
                left: ko.Computed<string>;
                right: ko.Computed<string>;
            }
            class JoinConditionSurface extends Analytics.Diagram.RoutedConnectorSurface {
                constructor(control: JoinConditionViewModel, context: DXImport.Analytics.Elements.ISurfaceContext);
                container(): QuerySurface;
            }
            module Metadata {
                var ParametersMode: {
                    ReadWrite: string;
                    Read: string;
                    Disabled: string;
                };
            }
            class ParameterViewModel extends Analytics.Data.DataSourceParameter {
                getEditorType(type: any): {
                    header?: any;
                    content?: any;
                    custom?: any;
                };
            }
            class QueryElementBaseSurface<M extends QueryElementBaseViewModel> extends DXImport.Analytics.Elements.SurfaceElementBase<M> {
                static _unitProperties: DXImport.Analytics.Internal.IUnitProperties<QueryElementBaseViewModel>;
                constructor(control: M, context: DXImport.Analytics.Elements.ISurfaceContext, unitProperties: DXImport.Analytics.Internal.IUnitProperties<M>);
                template: string;
                selectiontemplate: string;
                contenttemplate: string;
                margin: ko.Observable<number>;
            }
            class QueryViewModel extends QueryElementBaseViewModel {
                private static pageMargin;
                private static emptyModel;
                private _initializeTable;
                constructor(querySource: any, dbSchemaProvider: Analytics.Data.IDBSchemaProvider, parametersMode?: string, serializer?: DXImport.Analytics.Utils.ModelSerializer);
                isPropertyDisabled(name: string): boolean;
                isValid: ko.Computed<boolean>;
                editableName: ko.Observable<string> | ko.Computed<string>;
                filterString: Widgets.Internal.QBFilterStringOptions;
                _filterString: ko.Observable<string> | ko.Computed<string>;
                groupFilterString: Widgets.Internal.QBFilterStringOptions;
                _groupFilterString: ko.Observable<string> | ko.Computed<string>;
                top: ko.Observable<number> | ko.Computed<number>;
                skip: ko.Observable<number> | ko.Computed<number>;
                pageWidth: ko.Observable<number> | ko.Computed<number>;
                pageHeight: ko.Observable<number> | ko.Computed<number>;
                relations: ko.ObservableArray<RelationViewModel>;
                tables: ko.ObservableArray<TableViewModel>;
                columns: ko.ObservableArray<ColumnExpression>;
                filter: ko.Observable<string> | ko.Computed<string>;
                parameters: ko.ObservableArray<ParameterViewModel> | ko.Computed<ParameterViewModel[]>;
                margins: DXImport.Analytics.Elements.Margins;
                dbSchemaProvider: Analytics.Data.IDBSchemaProvider;
                allColumnsInTablesSelected: ko.Observable<boolean> | ko.Computed<boolean>;
                sorting: ko.ObservableArray<ColumnExpression>;
                grouping: ko.ObservableArray<ColumnExpression>;
                getInfo(): DXImport.Analytics.Utils.ISerializationInfoArray;
                createChild(info: any): DXImport.Analytics.Elements.ElementViewModel;
                aggregatedColumnsCount: ko.Observable<number>;
                getAllColumns(): ColumnViewModel[];
                tryToCreateRelationsByFK(sourceTable: TableViewModel): void;
                addChild(control: DXImport.Analytics.Elements.ElementViewModel): void;
                removeChild(control: DXImport.Analytics.Elements.ElementViewModel): void;
                getTable(name: string): TableViewModel;
                private _findTableInAncestors;
                private _findHead;
                private _isHead;
                private _findAncestorsRelations;
                private _reverseRelations;
                cerateJoinCondition(parentColumn: ColumnViewModel, nestedColumn: ColumnViewModel): JoinConditionViewModel;
                private _validate;
                private _validateTable;
                serialize(includeRootTag?: boolean): any;
                save(): any;
                onSave: (data: any) => void;
            }
            module Metadata {
                var querySerializationsInfo: DXImport.Analytics.Utils.ISerializationInfoArray;
            }
            class QuerySurface extends DXImport.Analytics.Elements.SurfaceElementBase<QueryViewModel> implements DXImport.Analytics.Internal.ISelectionTarget, DXImport.Analytics.Elements.ISurfaceContext {
                static _unitProperties: DXImport.Analytics.Internal.IUnitProperties<QueryViewModel>;
                private _joinedColumns;
                constructor(query: QueryViewModel, zoom?: ko.Observable<number>);
                measureUnit: ko.Observable<string> | ko.Computed<string>;
                dpi: ko.Observable<number> | ko.Computed<number>;
                zoom: ko.Observable<number> | ko.Computed<number>;
                placeholder: () => any;
                tables: ko.ObservableArray<TableSurface>;
                relations: ko.ObservableArray<RelationSurface>;
                allowMultiselect: boolean;
                focused: ko.Observable<boolean>;
                selected: ko.Observable<boolean>;
                underCursor: ko.Observable<DXImport.Analytics.Internal.IHoverInfo>;
                checkParent(surfaceParent: DXImport.Analytics.Internal.ISelectionTarget): boolean;
                pageWidth: ko.Observable<number> | ko.Computed<number>;
                templateName: string;
                getChildrenCollection(): ko.ObservableArray<TableSurface>;
                margins: DXImport.Analytics.Elements.IMargins;
                rtl: ko.Observable<boolean>;
                isJoined(column: ColumnSurface): boolean;
            }
            module Metadata {
                var relationSerializationInfo: DXImport.Analytics.Utils.ISerializationInfoArray;
            }
            class RelationViewModel extends QueryElementBaseViewModel {
                private _getConditionNumber;
                constructor(model: any, query: QueryViewModel, serializer?: DXImport.Analytics.Utils.ModelSerializer);
                parentTableName: ko.Observable<string> | ko.Computed<string>;
                nestedTableName: ko.Observable<string> | ko.Computed<string>;
                parentTable: ko.Observable<TableViewModel>;
                nestedTable: ko.Observable<TableViewModel>;
                joinType: ko.Observable<string> | ko.Computed<string>;
                conditions: ko.ObservableArray<JoinConditionViewModel>;
                getInfo(): DXImport.Analytics.Utils.ISerializationInfoArray;
                addChild(control: DXImport.Analytics.Elements.IElementViewModel): void;
                removeChild(control: DXImport.Analytics.Elements.ElementViewModel): void;
            }
            class RelationSurface extends DXImport.Analytics.Elements.SurfaceElementBase<RelationViewModel> {
                constructor(control: RelationViewModel, context: DXImport.Analytics.Elements.ISurfaceContext);
                conditions: ko.ObservableArray<JoinConditionSurface>;
                template: string;
                _getChildrenHolderName(): string;
            }
            module Metadata {
                var tableSerializationInfo: DXImport.Analytics.Utils.ISerializationInfoArray;
            }
            class TableViewModel extends QueryElementBaseViewModel {
                private serializer?;
                static COLUMNS_OFFSET: number;
                static COLUMN_HEIGHT: number;
                static COLUMN_MARGIN: number;
                static TABLE_MIN_WIDTH: number;
                static TABLE_DEFAULT_HEIGHT: number;
                private _columnsConnectionPointLeftX;
                private _columnsConnectionPointRightX;
                private _columns;
                private _initialized;
                constructor(model: any, parent: QueryViewModel, serializer?: DXImport.Analytics.Utils.ModelSerializer);
                size: DXImport.Analytics.Elements.Size;
                location: DXImport.Analytics.Elements.Point;
                name: ko.Observable<string> | ko.Computed<string>;
                alias: ko.Observable<string> | ko.Computed<string>;
                actualName: ko.PureComputed<string>;
                isReady: ko.Observable<boolean>;
                columns(): ColumnViewModel[];
                asterisk: AllColumnsViewModel;
                allColumnsSelected: ko.Computed<boolean>;
                toggleSelectedColumns(): void;
                isInitialized: ko.PureComputed<boolean>;
                getColumnConnectionPoints(column: ColumnViewModel): {
                    left: DXImport.Analytics.Elements.IPoint;
                    right: DXImport.Analytics.Elements.IPoint;
                };
                getInfo(): DXImport.Analytics.Utils.ISerializationInfoArray;
                getColumn(name: string): ColumnViewModel;
                createColumns(dbTable: Analytics.Data.DBTable): void;
            }
            class TableSurface extends QueryElementBaseSurface<TableViewModel> {
                constructor(control: TableViewModel, context: DXImport.Analytics.Elements.ISurfaceContext);
                columnsAsyncResolver: DXImport.Analytics.Internal.CodeResolver;
                asterisk: AllColumnsSurface;
                columns: ko.Computed<ColumnSurface[]>;
                contenttemplate: string;
                template: string;
                isInitialized: ko.Computed<boolean>;
                toggleSelected: () => void;
                selectedWrapper: ko.PureComputed<boolean>;
                resizable(resizeHandler: any, element: any): any;
            }
        }
        module Internal {
            class ColumnDragHandler extends DXImport.Analytics.Internal.DragDropHandler {
                private querySurface;
                private undoEngine;
                private _dragColumn;
                private _dragConditionSurface;
                private _needToCreateRelation;
                constructor(querySurface: ko.Observable<Elements.QuerySurface>, selection: DXImport.Analytics.Internal.SurfaceSelection, undoEngine: ko.Observable<DXImport.Analytics.Utils.UndoEngine>, snapHelper: DXImport.Analytics.Internal.SnapLinesHelper, dragHelperContent: DXImport.Analytics.Internal.DragHelperContent);
                startDrag(control: DXImport.Analytics.Internal.ISelectionTarget): void;
                setConnectorPoints(cursorPosition: {
                    top: number;
                    left: number;
                }): void;
                drag(event: JQueryEventObject, ui: JQueryUI.DraggableEventUIParams): void;
                doStopDrag(): void;
                dragDropConnector: ko.Observable<Analytics.Diagram.RoutedConnectorSurface>;
                getDragColumn(): Elements.ColumnViewModel;
            }
            class DbObjectDragDropHandler extends DXImport.Analytics.Internal.DragDropHandler {
                private _undoEngine;
                private _querySurface;
                private _drop;
                private _query;
                static getDropCallback: (undoEngine: ko.Observable<DXImport.Analytics.Utils.UndoEngine>, suggestLocation: boolean) => (memberInfo: DXImport.Analytics.Utils.IDataMemberInfo, query: Elements.QueryViewModel) => Elements.TableViewModel;
                constructor(surface: ko.Observable<Elements.QuerySurface>, selection: DXImport.Analytics.Internal.SurfaceSelection, _undoEngine: ko.Observable<DXImport.Analytics.Utils.UndoEngine>, snapHelper: DXImport.Analytics.Internal.SnapLinesHelper, dragHelperContent: DXImport.Analytics.Internal.DragHelperContent);
                startDrag(draggable: any): void;
                doStopDrag(ui: any, draggable: any): void;
                addControl(control: any, dropTargetSurface: any, size: any): void;
            }
        }
    }
}


export default DevExpress