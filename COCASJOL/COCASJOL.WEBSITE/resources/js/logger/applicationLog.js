/// <reference name="Ext.Net.Build.Ext.Net.extjs.adapter.ext.ext-base-debug.js" assembly="Ext.Net" />
/// <reference name="Ext.Net.Build.Ext.Net.extjs.ext-all-debug-w-comments.js" assembly="Ext.Net" />

var calendar = {
    setFecha: function () {
        var dateFrom = Ext.getCmp('ff_date_from').getValue();
        if (dateFrom != "")
            dateFrom = dateFrom.dateFormat('d/M/y');
        else
            dateFrom = "";

        var dateTo = Ext.getCmp('ff_date_to').getValue();
        if (dateTo != "")
            dateTo = dateTo.dateFormat('d/M/y');
        else
            dateTo = "";


        var strDate = dateFrom + (dateFrom == "" || dateTo == "" ? "" : " - ") + dateTo;

        Ext.getCmp('ddfFecha').setValue("", strDate);
        GridStore.reload();
    },

    clearFecha: function () {
        this.resetDateFields(Ext.getCmp('ff_date_from'), Ext.getCmp('ff_date_to'));
        this.setFecha();
    },

    validateDateRange: function (field) {
        var v = this.processValue(this.getRawValue()), field;

        if (this.startDateField) {
            field = Ext.getCmp(this.startDateField);
            field.setMaxValue();
            this.dateRangeMax = null;
        } else if (this.endDateField) {
            field = Ext.getCmp(this.endDateField);
            field.setMinValue();
            this.dateRangeMin = null;
        }

        field.validate();
    },

    resetDateFields: function (field1, field2) {
        field1.dateRangeMin = null;
        field2.dateRangeMax = null;
        field1.setMaxValue();
        field2.setMinValue();
        field1.reset();
        field2.reset();
    }
};

var Grid = null;
var GridStore = null;
var EditWindow = null;
var EditForm = null;

var AlertSelMsgTitle = "Atención";
var AlertSelMsg = "Debe seleccionar 1 elemento";

var PageX = {
    _index: 0,

    setReferences: function () {
        Grid = AppLogGridP;
        GridStore = AppLogSt;
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

        AppLogSt.reload();
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

var fnLevel = function (value) {
    var template = "<span class='{0}' style='background-repeat:no-repeat;background-position:left top'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{1}</span>";
    var classname = "icon-comment";
    if (value == "WARN") classname = "icon-useralert";
    if (value == "ERROR" || value == "FATAL") classname = "icon-error";
    if (value == "INFO") classname = "icon-information";

    return String.format(template, classname, value);
};

var KeyUpEvent = function (sender, e) {
    if (e.getKey() != 13) return false;
};