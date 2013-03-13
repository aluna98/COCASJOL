/// <reference name="Ext.Net.Build.Ext.Net.extjs.adapter.ext.ext-base-debug.js" assembly="Ext.Net" />
/// <reference name="Ext.Net.Build.Ext.Net.extjs.ext-all-debug-w-comments.js" assembly="Ext.Net" />

var calendar = {
    setFecha: function () {
        var dateFrom = Ext.getCmp('f_DATE_FROM').getValue();
        if (dateFrom != "")
            dateFrom = dateFrom.dateFormat('d/M/y');
        else
            dateFrom = "";

        var dateTo = Ext.getCmp('f_DATE_TO').getValue();
        if (dateTo != "")
            dateTo = dateTo.dateFormat('d/M/y');
        else
            dateTo = "";


        var strDate = dateFrom + (dateFrom == "" || dateTo == "" ? "" : " - ") + dateTo;

        Ext.getCmp('f_LIQUIDACIONES_FECHA').setValue("", strDate);
        GridStore.reload();
    },

    clearFecha: function () {
        this.resetDateFields(Ext.getCmp('f_DATE_FROM'), Ext.getCmp('f_DATE_TO'));
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
var AddWindow = null;
var AddForm = null;
var EditWindow = null;
var EditForm = null;

var AlertSelMsgTitle = "Atención";
var AlertSelMsg = "Debe seleccionar 1 elemento";

var ConfirmMsgTitle = "Hoja de Liquidación";
var ConfirmUpdate = "Seguro desea modificar la hoja de liquidación?";
var ConfirmDelete = "Seguro desea eliminar la hoja de liquidación?";

var PageX = {
    _index: 0,

    setReferences: function () {
        Grid = HojasGridP;
        GridStore = HojasGridP.getStore();
        AddWindow = AgregarHojasWin;
        AddForm = AgregarHojasFormP;
        EditWindow = EditarHojasWin;
        EditForm = EditarHojasFormP;
    },

    add: function () {
        AddWindow.show();
    },

    AddCalculosTotalProducto: function () {
        var TotalLibras = AddTotalLibrasTxt.getValue();
        var PrecioLibra = AddPrecioLibraTxt.getValue();

        AddTotalProductoTxt.setValue(TotalLibras * PrecioLibra);
        AddTotalCalculosFSTxt.setValue(AddTotalProductoTxt.getValue());

        /* Calcular Retención por Capitalización */
        var porcentajeCapitalizacionRet = AddCapitalizacionXRetencionTxt.getValue() / 100;
        AddCapitalizacionXRetencionCantidadTxt.setValue(AddTotalCalculosFSTxt.getValue() * porcentajeCapitalizacionRet);
    },

    insert: function () {
        var fields = AddForm.getForm().getFieldValues(false, "dataIndex");

        Grid.insertRecord(0, fields, false);
        AddForm.getForm().reset();

        /* Update Reporte */
        Ext.net.DirectMethods.UpdateReporteConsolidadoDeInventarioDeCafe();
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

            var SociosId = Ext.getCmp('EditSociosIdTxt');
            var SociosNombre = Ext.getCmp('EditNombreTxt');
            var SociosDireccionFinca = Ext.getCmp('EditDireccionFincaTxt');

            this.getNombreDeSocioEdit(SociosId, SociosNombre);
            this.getDireccionDeFincaEdit(SociosId, SociosDireccionFinca);

            EditTotalCalculosFSTxt.setValue(EditTotalProductoTxt.getValue());
        }
    },

    update: function () {
        if (EditForm.record == null) {
            return;
        }

        Ext.Msg.confirm(ConfirmMsgTitle, ConfirmUpdate, function (btn, text) {
            if (btn == 'yes') {
                EditForm.getForm().updateRecord(EditForm.record);
            }
        });
    },

    remove: function () {
        if (Grid.getSelectionModel().hasSelection()) {
            Ext.Msg.confirm(ConfirmMsgTitle, ConfirmDelete, function (btn, text) {
                if (btn == 'yes') {
                    var record = Grid.getSelectionModel().getSelected();
                    Grid.deleteRecord(record);
                }
            });
        } else {
            var msg = Ext.Msg;
            Ext.Msg.alert(AlertSelMsgTitle, AlertSelMsg);
        }
    },

    keyUpEvent: function (sender, e) {
        if (e.getKey() == 13)
            GridStore.reload();
    },

    reloadGridStore: function () {
        GridStore.reload();
    },

    getNombreDeSocio: function (sociosIdTxt, nombreTxt) {
        var comboBox = sociosIdTxt, value = comboBox.getValue();
        record = comboBox.findRecord(comboBox.valueField, value), index = comboBox.getStore().indexOf(record);

        var nombreCompleto = record.data.SOCIOS_PRIMER_NOMBRE +
                                     (record.data.SOCIOS_SEGUNDO_NOMBRE != '' ? (' ' + record.data.SOCIOS_SEGUNDO_NOMBRE) : '') +
                                     (record.data.SOCIOS_PRIMER_APELLIDO !== '' ? (' ' + record.data.SOCIOS_PRIMER_APELLIDO) : '') +
                                     (record.data.SOCIOS_SEGUNDO_APELLIDO != '' ? (' ' + record.data.SOCIOS_SEGUNDO_APELLIDO) : '');

        nombreTxt.setValue(nombreCompleto);
    },

    getNombreDeSocioEdit: function (sociosIdTxt, nombreTxt) {
        var comboBox = sociosIdTxt, value = comboBox.getValue();
        record = SocioSt.getById(value);

        var nombreCompleto = record.data.SOCIOS_PRIMER_NOMBRE +
                                     (record.data.SOCIOS_SEGUNDO_NOMBRE != '' ? (' ' + record.data.SOCIOS_SEGUNDO_NOMBRE) : '') +
                                     (record.data.SOCIOS_PRIMER_APELLIDO !== '' ? (' ' + record.data.SOCIOS_PRIMER_APELLIDO) : '') +
                                     (record.data.SOCIOS_SEGUNDO_APELLIDO != '' ? (' ' + record.data.SOCIOS_SEGUNDO_APELLIDO) : '');

        nombreTxt.setValue(nombreCompleto);
    },

    getDireccionDeFinca: function (sociosIdTxt, direccionFincaTxt) {
        var comboBox = sociosIdTxt, value = comboBox.getValue();
        record = comboBox.findRecord(comboBox.valueField, value), index = comboBox.getStore().indexOf(record);

        direccionFincaTxt.setValue(record.data.PRODUCCION_UBICACION_FINCA);
    },

    getDireccionDeFincaEdit: function (sociosIdTxt, direccionFincaTxt) {
        var comboBox = sociosIdTxt, value = comboBox.getValue();
        record = SocioSt.getById(value);

        direccionFincaTxt.setValue(record.data.PRODUCCION_UBICACION_FINCA);
    },

    getInventarioFueraDeCatacion: function () {
        Ext.net.DirectMethods.GetCantidadDeInventarioDeCafe();
    },

    loadCapitalizacionXRetencion: function () {
        Ext.net.DirectMethods.LoadCapitalizacionXRetencion();
    },

    clearFilter: function () {
        f_LIQUIDACIONES_ID.reset();
        f_SOCIOS_ID.reset();
        f_CLASIFICACIONES_CAFE_ID.reset();
        calendar.clearFecha();

        HojasSt.reload();
    },

    gridKeyUpEvent: function (sender, e) {
        var k = e.getKey();

        switch (k) {
            case 45: //INSERT
                this.add();
                break;
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
    },

    print: function () {
        var liquidaciones_id = EditHojasIdTxt.getValue();
        window.open('../../Reportes/ReporteHojasDeLiquidacion.aspx?LIQUIDACIONES_ID=' + liquidaciones_id, "_blank", "resizable=yes, scrollbars=yes, top=10, left=10");
    }
};