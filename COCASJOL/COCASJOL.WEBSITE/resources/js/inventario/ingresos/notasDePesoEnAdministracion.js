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

        Ext.getCmp('f_NOTAS_FECHA').setValue("", strDate);
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

var ConfirmMsgTitle = "Nota de Peso";
var ConfirmUpdate = "Seguro desea modificar la notas de peso?";

var ConfirmRegMsgTitle = "Nota de Peso";
var ConfirmRegUpdate = "Seguro desea registrar la notas de peso? Nota: Una vez registrada no se podra modificar.";

var PageX = {
    _index: 0,

    setReferences: function () {
        Grid = NotasGridP;
        GridStore = NotasSt;
        EditWindow = EditarNotasWin;
        EditForm = EditarNotasFormP;
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
            EditNotaDetalleSt.reload({
                callback: function (r, options, success) {
                    if (EditNotaDetalleSt.getCount() <= 0)
                        return;
                    EditDetailX.updateSumTotals();
                }
            });

            var EditSocioId = Ext.getCmp('EditSociosIdTxt');
            var EditNombre = Ext.getCmp('EditNombreTxt');
            var EditDireccionFinca = Ext.getCmp('EditDireccionFincaTxt');

            this.getNombreDeSocio(EditSocioId, EditNombre);
            this.getDireccionDeFinca(EditSocioId, EditDireccionFinca);
            EditPorcentajeDefectoTxt.setValue(Ext.util.Format.number(EditPorcentajeDefectoTxt.getValue().replace(/[\%]/g, ''), '0.00%'));
            EditPorcentajeHumedadTxt.setValue(Ext.util.Format.number(EditPorcentajeHumedadTxt.getValue().replace(/[\%]/g, ''), '0.00%'));
            EditPorcentajeTransporteTxt.setValue(Ext.util.Format.number(EditPorcentajeTransporteTxt.getValue().replace(/[\%]/g, ''), '0.00%'));

            this.checkIfRegistered();
        }
    },

    update: function () {
        if (EditForm.record == null) {
            return;
        }

        Ext.Msg.confirm(ConfirmMsgTitle, ConfirmUpdate, function (btn, text) {
            if (btn == 'yes') {
                Ext.net.DirectMethods.EditNotaDePeso_Click('',
                { success: function () {
                    GridStore.reload();
                    Ext.Msg.alert('Editar Nota de Peso', 'Nota de peso actualizada exitosamente.');
                    EditWindow.hide();
                }
                },
                { failure: function () {
                    Ext.Msg.alert('Editar Nota de Peso', 'Error al actualizar la nota de peso.');
                }
                });
            }
        });
    },

    register: function () {
        if (EditForm.record == null) {
            return;
        }

        Ext.Msg.confirm(ConfirmRegMsgTitle, ConfirmRegUpdate, function (btn, text) {
            if (btn == 'yes') {
                Ext.net.DirectMethods.RegisterNotaDePeso_Click(
                    { success: function () {
                        GridStore.reload();
                        Ext.Msg.alert('Editar Nota de Peso', 'Nota de peso actualizada exitosamente.');
                        EditWindow.hide();
                    }
                    },
                    { failure: function () {
                        Ext.Msg.alert('Editar Nota de Peso', 'Error al actualizar la nota de peso.');
                    }
                    });
            }
        });
    },

    keyUpEvent: function (sender, e) {
        if (e.getKey() == 13)
            GridStore.reload();
    },

    reloadGridStore: function () {
        GridStore.reload();
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

    getDireccionDeFinca: function (sociosIdTxt, direccionFincaTxt) {
        var value = sociosIdTxt.getValue();
        var record = SocioSt.getById(value);

        direccionFincaTxt.setValue(record.data.PRODUCCION_UBICACION_FINCA);
    },

    clearFilter: function () {
        f_NOTAS_ID.reset();
        f_ESTADOS_NOTA_ID.reset();
        f_SOCIOS_ID.reset();
        f_CLASIFICACIONES_CAFE_ID.reset();
        calendar.clearFecha();

        NotasSt.reload();
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

    checkIfRegistered: function () {
        var estadoId = EstadoIdHdn.getValue();

        if (estadoId == EditEstadoNotaCmb.getValue()) {
            rec = this.getRecord();

            if (rec != null) {
                if (rec.data.TRANSACCION_NUMERO == null) {
                    EditRegistrarBtn.setVisible(true);
                    EditGuardarBtn.setVisible(true);
                    EditEstadoNotaCmb.readOnly = false;
                } else {
                    EditRegistrarBtn.setVisible(false);
                    EditGuardarBtn.setVisible(false);
                    EditEstadoNotaCmb.readOnly = true;
                    EditEstadoNotaCmb.triggers[0].hide();
                }
            }
        } else {
            EditRegistrarBtn.setVisible(false);
            EditGuardarBtn.setVisible(true);
            EditEstadoNotaCmb.readOnly = false;
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

var EditDetailGrid = null;
var EditDetailGridStore = null;

var EditDetailX = {
    setReferences: function () {
        EditDetailGridStore = EditNotaDetalleSt;
        EditDetailGrid = EditNotaDetalleGridP;
    },

    updateSumTotals: function () {
        var bagSum = this.getSum(EditDetailGrid, 0);
        var weigthSum = this.getSum(EditDetailGrid, 1);

        EditSumaSacosTxt.setValue(bagSum);
        EditSumaPesoBrutoTxt.setValue(weigthSum);
    },

    getSum: function (grid, index) {
        var dataIndex = grid.getColumnModel().getDataIndex(index),
                    sum = 0;

        grid.getStore().each(function (record) {
            sum += record.get(dataIndex);
        });

        return sum;
    },

    pesoToJson: function () {
        var items = EditDetailGridStore.data;
        var ret = [];
        for (var i = 0; i < items.length; i++) {
            ret.push({ DETALLES_CANTIDAD_SACOS: items.items[i].data.DETALLES_CANTIDAD_SACOS, DETALLES_PESO: items.items[i].data.DETALLES_PESO });
        }

        return Ext.encode(ret);
    }
};