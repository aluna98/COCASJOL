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
var ConfirmDelete = "Seguro desea eliminar la notas de peso?";

var PageX = {
    _index: 0,

    nameRenderer: function (value, metadata, record) {
        var nombre =
        record.get('SOCIOS_PRIMER_NOMBRE') + ' ' +
        record.get('SOCIOS_SEGUNDO_NOMBRE') + ' ' +
        record.get('SOCIOS_PRIMER_APELLIDO') + ' ' +
        record.get('SOCIOS_SEGUNDO_APELLIDO');

        return nombre;
    },

    setReferences: function () {
        Grid = NotasGridP;
        GridStore = NotasSt;
        AddWindow = AgregarNotasWin;
        AddForm = AgregarNotasFormP;
        EditWindow = EditarNotasWin;
        EditForm = EditarNotasFormP;
    },

    add: function () {
        AddWindow.show();
    },

    insert: function () {
        var fields = AddForm.getForm().getFieldValues(false, "dataIndex");

        var pesoJson = AddDetailX.pesoToJson();


        Ext.net.DirectMethods.AddNotaDePeso_Click(pesoJson,
        {
            success: function () {
                GridStore.reload();
                Ext.Msg.alert('Agregar Nota de Peso', 'Nota de peso agregada exitosamente.');
                AddForm.getForm().reset();
                AddNotaDetalleSt.removeAll();
            },
            eventMask: {
                showMask: true, target: 'customtarget', customTarget: AddForm
            },
            failure: function () {
                Ext.Msg.alert('Agregar Nota de Peso', 'Error al agregar nota de peso.');
            }
        });
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

            EditPorcentajeDefectoTxt.setValue(Ext.util.Format.number(EditPorcentajeDefectoTxt.getValue().replace(/[\%]/g, ''), '0.00%'));
            EditPorcentajeHumedadTxt.setValue(Ext.util.Format.number(EditPorcentajeHumedadTxt.getValue().replace(/[\%]/g, ''), '0.00%'));
            EditEstadoNotaActualTxt.setValue(EditEstadoNotaCmb.getValue());
            this.editInterpretStateConfig();
        }
    },

    editResetStateConfig: function () {
        EditFechaNotaTxt.setDisabled(false);
        EditEstadoNotaCmb.setDisabled(false);
        EditSociosIdTxt.setDisabled(false);
        EditClasificacionCafeCmb.setDisabled(false);
        EditNombreTxt.setVisible(true);
        EditDireccionFincaTxt.setVisible(true);
        EditTotalRecibidoTextoTxt.setVisible(true);

        EditTipoTransporteRdGrp.setDisabled(false);
        EditPropiRadio.setDisabled(false);
        EditCooperativaRadio.setDisabled(false);

        EditPorcentajeTransporteTxt.setDisabled(false);
        EditDescuentoTransporteTxt.setVisible(true);

        EditPorcentajeHumedadTxt.setDisabled(false);
        EditDescuentoHumedadTxt.setVisible(true);
        EditPorcentajeDefectoTxt.setDisabled(false);
        EditDescuentoDefectoTxt.setVisible(true);

        EditAgregarDetalleBtn.setDisabled(false);
        EditEditarDetalleBtn.setDisabled(false);
        EditEliminarDetalleBtn.setDisabled(false);

        EditSacosRetenidosTxt.setDisabled(false);
        EditTaraTxt.setDisabled(false);
        EditDescuentoTxt.setVisible(true);
        EditTotalRecibidoTxt.setVisible(true);

        EditRegistrarBtn.setVisible(true);
        ImprimirBtn.setVisible(true);
    },

    editInterpretStateConfig: function () {
        var comboBox = EditEstadoNotaCmb, value = comboBox.getValue();
        record = EstadosNotaSt.getById(value);

        var ESTADOS_DETALLE_ENABLE_FECHA = !record.data.ESTADOS_DETALLE_ENABLE_FECHA,
            ESTADOS_DETALLE_ENABLE_ESTADO = record.data.ESTADOS_DETALLE_ENABLE_ESTADO,
            ESTADOS_DETALLE_ENABLE_SOCIO_ID = record.data.ESTADOS_DETALLE_ENABLE_SOCIO_ID,
            ESTADOS_DETALLE_ENABLE_CLASIFICACION_CAFE = !record.data.ESTADOS_DETALLE_ENABLE_CLASIFICACION_CAFE,
            ESTADOS_DETALLE_SHOW_INFO_SOCIO = record.data.ESTADOS_DETALLE_SHOW_INFO_SOCIO,
            ESTADOS_DETALLE_ENABLE_FORMA_ENTREGA = !record.data.ESTADOS_DETALLE_ENABLE_FORMA_ENTREGA,
            ESTADOS_DETALLE_ENABLE_DETALLE = !record.data.ESTADOS_DETALLE_ENABLE_DETALLE,
            ESTADOS_DETALLE_ENABLE_SACOS_RETENIDOS = !record.data.ESTADOS_DETALLE_ENABLE_SACOS_RETENIDOS,
            ESTADOS_DETALLE_ENABLE_TARA = !record.data.ESTADOS_DETALLE_ENABLE_TARA,
            ESTADOS_DETALLE_SHOW_DESCUENTOS = record.data.ESTADOS_DETALLE_SHOW_DESCUENTOS,
            ESTADOS_DETALLE_SHOW_TOTAL = record.data.ESTADOS_DETALLE_SHOW_TOTAL,
            ESTADOS_DETALLE_ENABLE_REGISTRAR_BTN = record.data.ESTADOS_DETALLE_ENABLE_REGISTRAR_BTN,
            ESTADOS_DETALLE_ENABLE_IMPRIMIR_BTN = record.data.ESTADOS_DETALLE_ENABLE_IMPRIMIR_BTN;

        EditFechaNotaTxt.setDisabled(ESTADOS_DETALLE_ENABLE_FECHA);

        EditEstadoNotaCmb.getStore().reload({
            callback: function (r, options, success) {
                EditEstadoNotaCmb.setValue(EditEstadoNotaCmb.getValue());
            }
        });

        switch (ESTADOS_DETALLE_ENABLE_ESTADO) {
            case 1:
            case 2:
                //EditEstadoNotaCmb.setDisabled(false);
                break;

            default:
                //EditEstadoNotaCmb.setDisabled(true);
                break;
        }

        EditSociosIdTxt.getStore().reload({
            callback: function (r, options, success) {
                EditSociosIdTxt.setValue(EditSociosIdTxt.getValue());
                PageX.getNombreDeSocio(Ext.getCmp('EditSociosIdTxt'), Ext.getCmp('EditNombreTxt'));
                PageX.getDireccionDeFinca(Ext.getCmp('EditSociosIdTxt'), Ext.getCmp('EditDireccionFincaTxt'));
            }
        });

        switch (ESTADOS_DETALLE_ENABLE_SOCIO_ID) {
            case 1:
            case 2:
                EditSociosIdTxt.setDisabled(false);
                break;

            default:
                EditSociosIdTxt.setDisabled(true);
                break;
        }

        EditClasificacionCafeCmb.setDisabled(ESTADOS_DETALLE_ENABLE_CLASIFICACION_CAFE);

        EditNombreTxt.setVisible(ESTADOS_DETALLE_SHOW_INFO_SOCIO);
        EditDireccionFincaTxt.setVisible(ESTADOS_DETALLE_SHOW_INFO_SOCIO);
        EditTotalRecibidoTextoTxt.setVisible(ESTADOS_DETALLE_SHOW_TOTAL);

        EditTipoTransporteRdGrp.setDisabled(ESTADOS_DETALLE_ENABLE_FORMA_ENTREGA);
        EditPropiRadio.setDisabled(ESTADOS_DETALLE_ENABLE_FORMA_ENTREGA);
        EditCooperativaRadio.setDisabled(ESTADOS_DETALLE_ENABLE_FORMA_ENTREGA);

        EditPorcentajeTransporteTxt.setDisabled(ESTADOS_DETALLE_ENABLE_FORMA_ENTREGA);
        EditDescuentoTransporteTxt.setVisible(ESTADOS_DETALLE_SHOW_DESCUENTOS);

        EditPorcentajeHumedadTxt.setDisabled(ESTADOS_DETALLE_ENABLE_FORMA_ENTREGA);
        EditDescuentoHumedadTxt.setVisible(ESTADOS_DETALLE_SHOW_DESCUENTOS);
        EditPorcentajeDefectoTxt.setDisabled(ESTADOS_DETALLE_ENABLE_FORMA_ENTREGA);
        EditDescuentoDefectoTxt.setVisible(ESTADOS_DETALLE_SHOW_DESCUENTOS);

        EditAgregarDetalleBtn.setDisabled(ESTADOS_DETALLE_ENABLE_DETALLE);
        EditEditarDetalleBtn.setDisabled(ESTADOS_DETALLE_ENABLE_DETALLE);
        EditEliminarDetalleBtn.setDisabled(ESTADOS_DETALLE_ENABLE_DETALLE);

        EditSacosRetenidosTxt.setDisabled(ESTADOS_DETALLE_ENABLE_SACOS_RETENIDOS);
        EditDescuentoTxt.setVisible(ESTADOS_DETALLE_SHOW_DESCUENTOS);
        EditTaraTxt.setDisabled(ESTADOS_DETALLE_ENABLE_TARA);
        EditTotalRecibidoTxt.setVisible(ESTADOS_DETALLE_SHOW_TOTAL);

        EditRegistrarBtn.setVisible(ESTADOS_DETALLE_ENABLE_REGISTRAR_BTN);
        ImprimirBtn.setVisible(ESTADOS_DETALLE_ENABLE_IMPRIMIR_BTN);
    },

    update: function () {
        if (EditForm.record == null) {
            return;
        }

        Ext.Msg.confirm(ConfirmMsgTitle, ConfirmUpdate, function (btn, text) {
            if (btn == 'yes') {
                var fields = EditForm.getForm().getFieldValues(false, "dataIndex");

                var pesoJson = EditDetailX.pesoToJson();

                Ext.net.DirectMethods.EditNotaDePeso_Click(pesoJson,
                {
                    success: function () {
                        GridStore.reload();
                        Ext.Msg.alert('Editar Nota de Peso', 'Nota de peso actualizada exitosamente.');
                        EditWindow.hide();
                    },
                    eventMask: {
                        showMask: true, target: 'customtarget', customTarget: EditForm
                    },
                    failure: function () {
                        Ext.Msg.alert('Editar Nota de Peso', 'Error al actualizar la nota de peso.');
                    }
                });
            }
        });
    },

    remove: function () {
        if (Grid.getSelectionModel().hasSelection()) {
            Ext.Msg.confirm(ConfirmMsgTitle, ConfirmDelete, function (btn, text) {
                if (btn == 'yes') {
                    var rec = Grid.getSelectionModel().getSelected();
                    Ext.net.DirectMethods.DeleteNotaDePeso_Click(rec.data.NOTAS_ID,
                    {
                        success: function () {
                            GridStore.reload();
                            Ext.Msg.alert('Eliminar Nota de Peso', 'Nota de peso eliminada exitosamente.');
                            EditWindow.hide();
                        },
                        eventMask: {
                            showMask: true, target: 'customtarget', customTarget: Grid
                        },
                        failure: function () {
                            Ext.Msg.alert('Eliminar Nota de Peso', 'Error al eliminar la nota de peso.');
                        }
                    });
                }
            });
        } else {
            var msg = Ext.Msg;
            Ext.Msg.alert(AlertSelMsgTitle, AlertSelMsg);
        }
    },

    register: function () {
        if (EditForm.record == null) {
            return;
        }

        Ext.Msg.confirm(ConfirmRegMsgTitle, ConfirmRegUpdate, function (btn, text) {
            if (btn == 'yes') {
                Ext.net.DirectMethods.RegisterNotaDePeso_Click(
                {
                    success: function () {
                        GridStore.reload();
                        Ext.Msg.alert('Registrar Nota de Peso', 'Nota de peso registrada exitosamente.');
                    },
                    eventMask: {
                        showMask: true, target: 'customtarget', customTarget: EditForm
                    },
                    failure: function () {
                        Ext.Msg.alert('Registrar Nota de Peso', 'Error al registrar la nota de peso.');
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
        var comboBox = sociosIdTxt, value = comboBox.getValue();
        record = comboBox.findRecord(comboBox.valueField, value), index = comboBox.getStore().indexOf(record);

        if (record == null || record == undefined)
            return;

        var nombreCompleto = record.data.SOCIOS_PRIMER_NOMBRE +
                                     (record.data.SOCIOS_SEGUNDO_NOMBRE != '' ? (' ' + record.data.SOCIOS_SEGUNDO_NOMBRE) : '') +
                                     (record.data.SOCIOS_PRIMER_APELLIDO !== '' ? (' ' + record.data.SOCIOS_PRIMER_APELLIDO) : '') +
                                     (record.data.SOCIOS_SEGUNDO_APELLIDO != '' ? (' ' + record.data.SOCIOS_SEGUNDO_APELLIDO) : '');

        nombreTxt.setValue(nombreCompleto);
    },

    getDireccionDeFinca: function (sociosIdTxt, direccionFincaTxt) {
        var comboBox = sociosIdTxt, value = comboBox.getValue();
        record = comboBox.findRecord(comboBox.valueField, value), index = comboBox.getStore().indexOf(record);

        if (record == null || record == undefined)
            return;

        direccionFincaTxt.setValue(record.data.PRODUCCION_UBICACION_FINCA);
    },

    getClasificacionDeCafeDeSocio: function (sociosIdTxt, clasificacionCafeTxt) {
        var comboBox = sociosIdTxt, value = comboBox.getValue();
        record = comboBox.findRecord(comboBox.valueField, value), index = comboBox.getStore().indexOf(record);

        clasificacionCafeTxt.setValue(record.data.CLASIFICACIONES_CAFE_ID);
    },

    clearFilter: function () {
        f_NOTAS_ID.reset();
        f_SOCIOS_ID.reset();
        f_CLASIFICACIONES_CAFE_ID.reset();
        calendar.clearFecha();

        NotasSt.reload();
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
            case 46: //DELETE
                this.remove();
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
        var notas_id = EditNotaIdTxt.getValue();
        window.open('../../Reportes/ImprimirNotasDePeso.aspx?NOTAS_ID=' + notas_id, "_blank", "resizable=yes, scrollbars=yes, top=10, left=10");
    }
};


var AddDetailGrid = null;
var AddDetailGridStore = null;
var AddDetailAddWindow = null;
var AddDetailAddForm = null;
var AddDetailEditWindow = null;
var AddDetailEditForm = null;
var AddDetailAddCalculateDefectWindow = null;
var AddDetailAddCalculateDefectForm = null;

var AlertSelMsgTitle = "Atención";
var AlertSelMsg = "Debe seleccionar 1 elemento";

var ConfirmMsgTitle1 = "Datos de Peso";
var ConfirmUpdate1 = "Seguro desea modificar los datos de peso?";
var ConfirmDelete1 = "Seguro desea eliminar los datos de peso?";

var AddDetailX = {
    _index: 0,

    setReferences: function () {
        AddDetailGrid = AddNotaDetalleGridP;
        AddDetailGridStore = AddNotaDetalleSt;
        AddDetailAddWindow = AddDetailAgregarDetallesWin;
        AddDetailAddForm = AddDetailAgregarDetallesFormPnl;
        AddDetailEditWindow = AddDetailEditarDetallesWin;
        AddDetailEditForm = AddDetailEditarDetallesFormPnl;
        AddDetailAddCalculateDefectWindow = AddCalculateDefectWin;
        AddDetailAddCalculateDefectForm = AddCalculateDefectFormPnl;
    },

    add: function () {
        AddDetailAddWindow.show();
        Ext.getCmp('AddDetailAddBagTxt').focus(false, 200);
    },

    insert: function () {
        var fields = AddDetailAddForm.getForm().getFieldValues(false, "dataIndex");
        AddDetailGrid.insertRecord(0, fields, true);
        AddDetailAddForm.getForm().reset();
        Ext.getCmp('AddDetailAddBagTxt').focus(false, 200);
    },

    insertDefectCalculation: function () {
        var sample = AddCalculateSampleTxt.getValue();
        var badSample = AddCalculateBadSampleTxt.getValue();

        if (badSample > 0) {
            var res = (badSample / sample) * 100;
            AddPorcentajeDefectoTxt.setValue(Ext.util.Format.number(res, '0.00%'));
            AddDetailAddCalculateDefectWindow.hide();
            Ext.getCmp('AddPorcentajeDefectoTxt').focus(false, 200);
        } else
            Ext.Msg.alert('Porcentaje de Defecto', 'El valor de gramos malos debe ser mayor a cero.');
    },

    getIndex: function () {
        return this._index;
    },

    setIndex: function (index) {
        if (index > -1 && index < AddDetailGrid.getStore().getCount()) {
            this._index = index;
        }
    },

    getRecord: function () {
        var rec = AddDetailGrid.getStore().getAt(this.getIndex());  // Get the Record

        if (rec != null) {
            return rec;
        }
    },

    edit: function () {
        if (AddDetailGrid.getSelectionModel().hasSelection()) {
            var record = AddDetailGrid.getSelectionModel().getSelected();
            var index = AddDetailGrid.store.indexOf(record);
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
            AddDetailEditWindow.show();
            AddDetailEditForm.getForm().loadRecord(rec);
            AddDetailEditForm.record = rec;
            Ext.getCmp('AddDetailEditBagTxt').focus(false, 200);
        }
    },

    openDefectCalculation: function () {
        AddDetailAddCalculateDefectWindow.show();
        Ext.getCmp('AddCalculateSampleTxt').focus(false, 200);
    },

    update: function () {
        if (AddDetailEditForm.record == null) {
            return;
        }

        Ext.Msg.confirm(ConfirmMsgTitle1, ConfirmUpdate1, function (btn, text) {
            if (btn == 'yes') {
                AddDetailEditForm.getForm().updateRecord(AddDetailEditForm.record);
                Ext.getCmp('AddDetailEditBagTxt').focus(false, 200);
            }
        });
    },

    remove: function () {
        if (AddDetailGrid.getSelectionModel().hasSelection()) {
            Ext.Msg.confirm(ConfirmMsgTitle1, ConfirmDelete1, function (btn, text) {
                if (btn == 'yes') {
                    var record = AddDetailGrid.getSelectionModel().getSelected();
                    AddDetailGrid.deleteRecord(record);
                    AddEliminarDetalleBtn.focus(false, 200);
                }
            });
        } else {
            var msg = Ext.Msg;
            Ext.Msg.alert(AlertSelMsgTitle, AlertSelMsg);
        }
    },

    keyUpEventAddCalculateDefect: function (sender, e) {
        if (e.getKey() == 13) {
            if (AddDetailAddCalculateDefectForm.getForm().isValid())
                this.insertDefectCalculation();
        }
    },

    keyUpEventAddDetail: function (sender, e) {
        if (e.getKey() == 13) {
            if (AddDetailAddForm.getForm().isValid())
                this.insert();
        }
    },

    keyUpEventEditDetail: function (sender, e) {
        if (e.getKey() == 13) {
            if (AddDetailEditForm.getForm().isValid())
                this.update();
        }
    },

    updateSumTotals: function () {
        var bagSum = this.getSum(AddDetailGrid, 0);
        var weigthSum = this.getSum(AddDetailGrid, 1);

        AddSumaSacosTxt.setValue(bagSum);
        AddSumaPesoBrutoTxt.setValue(weigthSum);
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
        var items = AddDetailGridStore.data;
        var ret = [];
        for (var i = 0; i < items.length; i++) {
            ret.push({ DETALLES_CANTIDAD_SACOS: items.items[i].data.DETALLES_CANTIDAD_SACOS, DETALLES_PESO: items.items[i].data.DETALLES_PESO });
        }

        return Ext.encode(ret);
    }
};

var EditDetailGrid = null;
var EditDetailGridStore = null;
var EditDetailAddWindow = null;
var EditDetailAddForm = null;
var EditDetailEditWindow = null;
var EditDetailEditForm = null;
var EditDetailEditCalculateDefectWindow = null;
var EditDetailEditCalculateDefectForm = null;

var AlertSelMsgTitle = "Atención";
var AlertSelMsg = "Debe seleccionar 1 elemento";

var ConfirmMsgTitle2 = "Datos de Peso";
var ConfirmUpdate2 = "Seguro desea modificar los datos de peso?";
var ConfirmDelete2 = "Seguro desea eliminar los datos de peso?";

var EditDetailX = {
    _index: 0,

    setReferences: function () {
        EditDetailGrid = EditNotaDetalleGridP;
        EditDetailGridStore = EditNotaDetalleSt;
        EditDetailAddWindow = EditDetailAgregarDetallesWin;
        EditDetailAddForm = EditDetailAgregarDetallesFormPnl;
        EditDetailEditWindow = EditDetailEditarDetallesWin;
        EditDetailEditForm = EditDetailEditarDetallesFormPnl;
        EditDetailEditCalculateDefectWindow = EditCalculateDefectWin;
        EditDetailEditCalculateDefectForm = EditCalculateDefectFormPnl;
    },

    add: function () {
        EditDetailAddWindow.show();
        Ext.getCmp('EditDetailAddBagTxt').focus(false, 200);
    },

    insert: function () {
        var fields = EditDetailAddForm.getForm().getFieldValues(false, "dataIndex");
        EditDetailGrid.insertRecord(0, fields, true);
        EditDetailAddForm.getForm().reset();
        Ext.getCmp('EditDetailAddBagTxt').focus(false, 200);
    },

    insertDefectCalculation: function () {
        var sample = EditCalculateSampleTxt.getValue();
        var badSample = EditCalculateBadSampleTxt.getValue();

        if (badSample > 0) {
            var res = (badSample / sample) * 100;
            EditPorcentajeDefectoTxt.setValue(Ext.util.Format.number(res, '0.00%'));
            EditDetailEditCalculateDefectWindow.hide();
            Ext.getCmp('EditPorcentajeDefectoTxt').focus(false, 200);
        } else
            Ext.Msg.alert('Porcentaje de Defecto', 'El valor de gramos malos debe ser mayor a cero.');
    },

    getIndex: function () {
        return this._index;
    },

    setIndex: function (index) {
        if (index > -1 && index < EditDetailGrid.getStore().getCount()) {
            this._index = index;
        }
    },

    getRecord: function () {
        var rec = EditDetailGrid.getStore().getAt(this.getIndex());  // Get the Record

        if (rec != null) {
            return rec;
        }
    },

    edit: function () {
        if (EditDetailGrid.getSelectionModel().hasSelection()) {
            var record = EditDetailGrid.getSelectionModel().getSelected();
            var index = EditDetailGrid.store.indexOf(record);
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
            EditDetailEditWindow.show();
            EditDetailEditForm.getForm().loadRecord(rec);
            EditDetailEditForm.record = rec;
            Ext.getCmp('EditDetailEditBagTxt').focus(false, 200);
        }
    },

    openDefectCalculation: function () {
        EditDetailEditCalculateDefectWindow.show();
        Ext.getCmp('EditCalculateSampleTxt').focus(false, 200);
    },

    update: function () {
        if (EditDetailEditForm.record == null) {
            return;
        }

        Ext.Msg.confirm(ConfirmMsgTitle2, ConfirmUpdate2, function (btn, text) {
            if (btn == 'yes') {
                EditDetailEditForm.getForm().updateRecord(EditDetailEditForm.record);
                Ext.getCmp('EditDetailEditBagTxt').focus(false, 200);
            }
        });
    },

    remove: function () {
        if (EditDetailGrid.getSelectionModel().hasSelection()) {
            Ext.Msg.confirm(ConfirmMsgTitle2, ConfirmDelete2, function (btn, text) {
                if (btn == 'yes') {
                    var record = EditDetailGrid.getSelectionModel().getSelected();
                    EditDetailGrid.deleteRecord(record);
                    EditEliminarDetalleBtn.focus(false, 200);
                }
            });
        } else {
            var msg = Ext.Msg;
            Ext.Msg.alert(AlertSelMsgTitle, AlertSelMsg);
        }
    },

    keyUpEventAddCalculateDefect: function (sender, e) {
        if (e.getKey() == 13) {
            if (EditDetailEditCalculateDefectForm.getForm().isValid())
                this.insertDefectCalculation();
        }
    },

    keyUpEventAddDetail: function (sender, e) {
        if (e.getKey() == 13) {
            if (EditDetailAddForm.getForm().isValid())
                this.insert();
        }
    },

    keyUpEventEditDetail: function (sender, e) {
        if (e.getKey() == 13) {
            if (EditDetailEditForm.getForm().isValid())
                this.update();
        }
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