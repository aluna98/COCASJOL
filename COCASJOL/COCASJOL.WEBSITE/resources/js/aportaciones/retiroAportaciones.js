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

        Ext.getCmp('f_RETIROS_AP_FECHA').setValue("", strDate);
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

var ConfirmMsgTitle = "Retiro de Aportaciones";
var ConfirmUpdate = "Seguro desea retirar de las aportaciones? Nota: Una vez que se realice la acción no se podran deshacer los cambios.";
var ConfirmDelete = "Seguro desea eliminar las aportaciones?";

var PageX = {
    _index: 0,

    setReferences: function () {
        Grid = RetiroAportacionesGridP;
        GridStore = Grid.getStore();
        AddWindow = AgregarRetiroAportacionWin;
        AddForm = AgregarRetiroAportacionFormP;
        EditWindow = EditarRetiroAportacionWin;
        EditForm = EditarRetiroAportacionFormP;
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

    add: function () {
        AddWindow.show();
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

            var EditSocioId = Ext.getCmp('EditSociosIdTxt');
            var EditNombre = Ext.getCmp('EditNombreTxt');

            this.getNombreDeSocio(EditSocioId, EditNombre);
            this.loadTotalRetiro();
        }
    },

    insert: function () {
        Ext.Msg.confirm(ConfirmMsgTitle, ConfirmUpdate, function (btn, text) {
            if (btn == 'yes') {
                Ext.net.DirectMethods.RetirarBtn_Click(
                {
                    success: function () {
                        AportacionesSt.reload();
                        GridStore.reload();
                        Ext.Msg.alert('Agregar Retiro de Aportaciones', 'Retiro de aportaciones ejecutado exitosamente.');
                        AddForm.getForm().reset();
                    },
                    eventMask: {
                        showMask: true, target: 'customtarget', customTarget: AddForm
                    },
                    failure: function () {
                        Ext.Msg.alert('Agregar Retiro de Aportaciones', 'Error al ejecutar el retiro de aportaciones.');
                    }
                });
            }
        });
    },

    addGetNombreDeSocio: function (sociosIdTxt, nombreTxt) {
        var value = sociosIdTxt.getValue();
        var record = AportacionesSt.getById(value);

        if (record != null) {
            //            AddForm.getForm().loadRecord(record);
            //            AddForm.record = record;
            AddAportacionOrdinariaSaldoTxt.setValue(record.data.APORTACIONES_ORDINARIA_SALDO);
            AddAportacionExtraordinariaSaldoTxt.setValue(record.data.APORTACIONES_EXTRAORDINARIA_SALDO);
            AddAportacionCapRetencionSaldoTxt.setValue(record.data.APORTACIONES_CAPITALIZACION_RETENCION_SALDO);
            AddAportacionInteresesSAportacionesSaldoTxt.setValue(record.data.APORTACIONES_INTERESES_S_APORTACION_SALDO);
            AddAportacionExcedentePeriodoSaldoTxt.setValue(record.data.APORTACIONES_EXCEDENTE_PERIODO_SALDO);
            AddAportacionTotalSaldoTxt.setValue(record.data.APORTACIONES_SALDO_TOTAL);
        } else {
            AddAportacionOrdinariaSaldoTxt.setValue(0);
            AddAportacionExtraordinariaSaldoTxt.setValue(0);
            AddAportacionCapRetencionSaldoTxt.setValue(0);
            AddAportacionInteresesSAportacionesSaldoTxt.setValue(0);
            AddAportacionExcedentePeriodoSaldoTxt.setValue(0);
            AddAportacionTotalSaldoTxt.setValue(0);
        }

        this.getNombreDeSocio(AddSociosIdTxt, AddNombreTxt);

        this.loadTotalRetiro();
    },

    getNombreDeSocio: function (sociosIdTxt, nombreTxt) {
        var value = sociosIdTxt.getValue();
        var record = SocioSt.getById(value);

        var nombreCompleto = record.data.SOCIOS_PRIMER_NOMBRE +
                                     (record.data.SOCIOS_SEGUNDO_NOMBRE != '' ? (' ' + record.data.SOCIOS_SEGUNDO_NOMBRE) : '') +
                                     (record.data.SOCIOS_PRIMER_APELLIDO !== '' ? (' ' + record.data.SOCIOS_PRIMER_APELLIDO) : '') +
                                     (record.data.SOCIOS_SEGUNDO_APELLIDO != '' ? (' ' + record.data.SOCIOS_SEGUNDO_APELLIDO) : '');

        nombreTxt.setValue(nombreCompleto);
    },

    keyUpEvent: function (sender, e) {
        if (e.getKey() == 13)
            GridStore.reload();
    },

    reloadGridStore: function () {
        GridStore.reload();
    },

    clearFilter: function () {
        f_RETIROS_AP_ID.reset();
        f_SOCIOS_ID.reset();
        f_RETIROS_AP_TOTAL_RETIRADO.reset();
        calendar.clearFecha();
        RetiroAportacionesSt.reload();
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

    validarCantidadRetiro: function (aportacionTxt, retiroTxt) {
        var retiro = retiroTxt.getValue();
        var aportacion = aportacionTxt.getValue();

        retiro = retiro == null ? 0 : retiro;
        retiro = retiro < 0 ? 0 : retiro;

        retiro = retiro > aportacion ? aportacion : retiro;

        retiroTxt.setValue(retiro);
    },

    loadTotalRetiro: function () {
        var retiro = AddRetiroAportacionOrdinariaSaldoTxt.getValue() +
            AddRetiroAportacionExtraordinariaSaldoTxt.getValue() +
            AddRetiroAportacionCapRetencionSaldoTxt.getValue() +
            AddRetiroAportacionInteresesSAportacionesSaldoTxt.getValue() +
            AddRetiroAportacionExcedentePeriodoSaldoTxt.getValue();

        AddRetiroAportacionTotalSaldoTxt.setValue(retiro);
    }
};