/// <reference name="Ext.Net.Build.Ext.Net.extjs.adapter.ext.ext-base-debug.js" assembly="Ext.Net" />
/// <reference name="Ext.Net.Build.Ext.Net.extjs.ext-all-debug-w-comments.js" assembly="Ext.Net" />

var Grid = null;
var GridStore = null;
var EditWindow = null;
var EditForm = null;

var AlertSelMsgTitle = "Atención";
var AlertSelMsg = "Debe seleccionar 1 elemento";

var PageX = {
    _index: 0,

    setReferences: function () {
        Grid = InventarioCafeGridP;
        GridStore = InventarioCafeSt;
        EditWindow = EditarInventarioCafeWin;
        EditForm = EditarTipoFormP;
    },

    getIndex: function () {
        return this._index;
    },

    setIndex: function (index) {
        if (index > -1 && index < Grid.getStore().getCount()) {
            this._index = index;
        }
    },

    getRecord: function () {
        var rec = Grid.getStore().getAt(this.getIndex());  // Get the Record

        if (rec != null) {
            return rec;
        }
    },

    edit: function () {
        if (Grid.getSelectionModel().hasSelection()) {
            var record = Grid.getSelectionModel().getSelected();
            var index = Grid.store.indexOf(record);
            this.setIndex(index);
            this.open();
        } else {
            var msg = Ext.Msg;
            Ext.Msg.alert(AlertSelMsgTitle, AlertSelMsg);
        }
    },

    edit2: function (index) {
        this.setIndex(index);
        this.open();
    },

    next: function () {
        this.edit2(this.getIndex() + 1);
    },

    previous: function () {
        this.edit2(this.getIndex() - 1);
    },

    open: function () {
        rec = this.getRecord();

        if (rec != null) {
            EditWindow.show();
            EditForm.getForm().loadRecord(rec);
            EditForm.record = rec;
        }
    },

    keyUpEvent: function (sender, e) {
        if (e.getKey() == 13)
            GridStore.reload();
    },

    reloadGridStore: function () {
        GridStore.reload();
    },

    clearFilter: function () {
        f_SOCIOS_ID.reset();
        f_CLASIFICACIONES_CAFE_ID.reset();
        f_INVENTARIO_CANTIDAD.reset();

        InventarioCafeSt.reload();
    },

    gridKeyUpEvent: function (sender, e) {
        var k = e.getKey();

        switch (k) {
            case 13: //ENTER
                this.edit();
                break;
            default:
                break;
        }

    },

    navHome: function () {
        if (Grid.getStore().getTotalCount() == 0)
            Grid.getStore().reload();
        PagingToolbar1.moveFirst();
    },

    navPrev: function () {
        if (Grid.getStore().getTotalCount() > 0)
            PagingToolbar1.movePrevious();
    },

    navNext: function () {
        if (Grid.getStore().getTotalCount() > 0)
            if (Grid.getStore().getTotalCount() > (PagingToolbar1.cursor + PagingToolbar1.pageSize))
                PagingToolbar1.moveNext();
    },

    navEnd: function () {
        if (Grid.getStore().getTotalCount() == 0)
            Grid.getStore().reload();
        PagingToolbar1.moveLast();
    }
};