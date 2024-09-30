// namespaces
var dwv = dwv || {};
dwv.tool = dwv.tool || {};
/**
 * The Konva namespace.
 * @external Konva
 * @see https://konvajs.org/
 */
var Konva = Konva || {};

/**
 * Get the display name of the input shape.
 * @param {Object} shape The Konva shape.
 * @return {String} The display name.
 */
dwv.tool.GetShapeDisplayName = function (shape)
{
    var displayName = "shape";
    if ( shape instanceof Konva.Line ) {
        if ( shape.points().length === 4 ) {
            displayName = "line";
        }
        else if ( shape.points().length === 6 ) {
            displayName = "protractor";
        }
        else {
            displayName = "roi";
        }
    }
    else if ( shape instanceof Konva.Rect ) {
        displayName = "rectangle";
    }
    else if ( shape instanceof Konva.Ellipse ) {
        displayName = "ellipse";
    }
    // return
    return displayName;
};

/**
 * Draw group command.
 * @param {Object} group The group draw.
 * @param {String} name The shape display name.
 * @param {Object} layer The layer where to draw the group.
 * @param {Object} silent Whether to send a creation event or not.
 * @constructor
 */
dwv.tool.DrawGroupCommand = function (group, name, layer, silent)
{
    var isSilent = (typeof silent === "undefined") ? false : true;

    // group parent
    var parent = group.getParent();

    /**
     * Get the command name.
     * @return {String} The command name.
     */
    this.getName = function () { return "Draw-"+name; };
    /**
     * Execute the command.
     * @fires dwv.tool.DrawGroupCommand#draw-create
     */
    this.execute = function () {
        // add the group to the parent (in case of undo/redo)
        parent.add(group);
        // draw
        layer.draw();
        // callback
        if (!isSilent) {
            /**
             * Draw create event.
             * @event dwv.tool.DrawGroupCommand#draw-create
             * @type {Object}
             * @property {number} id The id of the create draw.
             */
            this.onExecute({
                'type': 'draw-create',
                'id': group.id()
            });
        }
    };
    /**
     * Undo the command.
     * @fires dwv.tool.DeleteGroupCommand#draw-delete
     */
    this.undo = function () {
        // remove the group from the parent layer
        group.remove();
        // draw
        layer.draw();
        // callback
        this.onUndo({
            'type': 'draw-delete',
            'id': group.id()
        });
    };
}; // DrawGroupCommand class

/**
 * Handle an execute event.
 * @param {Object} event The execute event with type and id.
 */
dwv.tool.DrawGroupCommand.prototype.onExecute = function (/*event*/) {
    // default does nothing.
};
/**
 * Handle an undo event.
 * @param {Object} event The undo event with type and id.
 */
dwv.tool.DrawGroupCommand.prototype.onUndo = function (/*event*/) {
    // default does nothing.
};

/**
 * Move group command.
 * @param {Object} group The group draw.
 * @param {String} name The shape display name.
 * @param {Object} translation A 2D translation to move the group by.
 * @param {Object} layer The layer where to move the group.
 * @constructor
 */
dwv.tool.MoveGroupCommand = function (group, name, translation, layer)
{
    /**
     * Get the command name.
     * @return {String} The command name.
     */
    this.getName = function () { return "Move-"+name; };

    /**
     * Execute the command.
     * @fires dwv.tool.MoveGroupCommand#draw-move
     */
    this.execute = function () {
        // translate group
        group.move(translation);
        // draw
        layer.draw();
        // callback
        /**
         * Draw move event.
         * @event dwv.tool.MoveGroupCommand#draw-move
         * @type {Object}
         * @property {number} id The id of the create draw.
         */
        this.onExecute({
            'type': 'draw-move',
            'id': group.id()
        });
    };
    /**
     * Undo the command.
     * @fires dwv.tool.MoveGroupCommand#draw-move
     */
    this.undo = function () {
        // invert translate group
        var minusTrans = { 'x': -translation.x, 'y': -translation.y};
        group.move(minusTrans);
        // draw
        layer.draw();
        // callback
        this.onUndo({
            'type': 'draw-move',
            'id': group.id()
        });
    };
}; // MoveGroupCommand class

/**
 * Handle an execute event.
 * @param {Object} event The execute event with type and id.
 */
dwv.tool.MoveGroupCommand.prototype.onExecute = function (/*event*/) {
    // default does nothing.
};
/**
 * Handle an undo event.
 * @param {Object} event The undo event with type and id.
 */
dwv.tool.MoveGroupCommand.prototype.onUndo = function (/*event*/) {
    // default does nothing.
};

/**
 * Change group command.
 * @param {String} name The shape display name.
 * @param {Object} func The change function.
 * @param {Object} startAnchor The anchor that starts the change.
 * @param {Object} endAnchor The anchor that ends the change.
 * @param {Object} layer The layer where to change the group.
 * @param {Object} image The associated image.
 * @constructor
 */
dwv.tool.ChangeGroupCommand = function (name, func, startAnchor, endAnchor, layer, image)
{
    /**
     * Get the command name.
     * @return {String} The command name.
     */
    this.getName = function () { return "Change-"+name; };

    /**
     * Execute the command.
     * @fires dwv.tool.ChangeGroupCommand#draw-change
     */
    this.execute = function () {
        // change shape
        func( endAnchor, image );
        // draw
        layer.draw();
        // callback
        /**
         * Draw change event.
         * @event dwv.tool.ChangeGroupCommand#draw-change
         * @type {Object}
         */
        this.onExecute({
            'type': 'draw-change'
        });
    };
    /**
     * Undo the command.
     * @fires dwv.tool.ChangeGroupCommand#draw-change
     */
    this.undo = function () {
        // invert change shape
        func( startAnchor, image );
        // draw
        layer.draw();
        // callback
        this.onUndo({
            'type': 'draw-change'
        });
    };
}; // ChangeGroupCommand class

/**
 * Handle an execute event.
 * @param {Object} event The execute event with type and id.
 */
dwv.tool.ChangeGroupCommand.prototype.onExecute = function (/*event*/) {
    // default does nothing.
};
/**
 * Handle an undo event.
 * @param {Object} event The undo event with type and id.
 */
dwv.tool.ChangeGroupCommand.prototype.onUndo = function (/*event*/) {
    // default does nothing.
};

/**
 * Delete group command.
 * @param {Object} group The group draw.
 * @param {String} name The shape display name.
 * @param {Object} layer The layer where to delete the group.
 * @constructor
 */
dwv.tool.DeleteGroupCommand = function (group, name, layer)
{
    // group parent
    var parent = group.getParent();

    /**
     * Get the command name.
     * @return {String} The command name.
     */
    this.getName = function () { return "Delete-"+name; };
    /**
     * Execute the command.
     * @fires dwv.tool.DeleteGroupCommand#draw-delete
     */
    this.execute = function () {
        // remove the group from its parent
        group.remove();
        // draw
        layer.draw();
        // callback
        /**
         * Draw delete event.
         * @event dwv.tool.DeleteGroupCommand#draw-delete
         * @type {Object}
         * @property {number} id The id of the create draw.
         */
        this.onExecute({
            'type': 'draw-delete',
            'id': group.id()
        });
    };
    /**
     * Undo the command.
     * @fires dwv.tool.DrawGroupCommand#draw-create
     */
    this.undo = function () {
        // add the group to its parent
        parent.add(group);
        // draw
        layer.draw();
        // callback
        this.onUndo({
            'type': 'draw-create',
            'id': group.id()
        });
    };
}; // DeleteGroupCommand class

/**
 * Handle an execute event.
 * @param {Object} event The execute event with type and id.
 */
dwv.tool.DeleteGroupCommand.prototype.onExecute = function (/*event*/) {
    // default does nothing.
};
/**
 * Handle an undo event.
 * @param {Object} event The undo event with type and id.
 */
dwv.tool.DeleteGroupCommand.prototype.onUndo = function (/*event*/) {
    // default does nothing.
};
