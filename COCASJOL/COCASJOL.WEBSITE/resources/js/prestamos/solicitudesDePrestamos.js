/// <reference name="Ext.Net.Build.Ext.Net.extjs.adapter.ext.ext-base-debug.js" assembly="Ext.Net" />
/// <reference name="Ext.Net.Build.Ext.Net.extjs.ext-all-debug-w-comments.js" assembly="Ext.Net" />

var Grid = null;
var GridRef = null;
var GridAval = null;
var EditWindow = null;
var EditRefWindow = null;
var EditAvalWindow = null;
var EditRefForm = null;
var EditForm = null;
var EditAvalForm = null;
var AddWindow = null;
var AddForm = null;
var AddRefForm = null;
var AddWindowAval = null;
var AddAvalForm = null;
var ConfirmMsgTitleAval = "Aval";
var ConfirmMsgTitleRef = "Referencia";
var ConfirmUpdateRef = "Seguro que desea editar la Referencia?";
var ConfirmMsgTitle = "Solicitud de Prestamo";
var ConfirmUpdate = "Seguro que desea editar la Solicitud?";
var ConfirmAvalUpdate = "Seguro desea editar el aval?";
var ConfirmDelete = "Seguro desea rechazar la Solicitud?";
var ConfirmDeleteAval = "Seguro desea eliminar el aval de la solicitud?";
var ConfirmDeleteRef = "Seguro desea eliminar la referencia de la solicitud?";
var ConfirmAprobbe = "Seguro desea aprobar la Solicitud?";
var Confirmacion = "Se ha finalizado correctamente";

var SolicitudX = {
    _index: 0, _indexRef: 0, _indexAval: 0,

    setReferences: function () {
        Grid = SolicitudesGriP;
        GridRef = ReferenciasGridP;
        GridAval = AvalesGridP;
        EditRefWindow = EditarReferenciaWin;
        EditRefForm = EditarReferenciaForm;
        EditAvalWindow = EditarAvalWin;
        EditAvalForm = EditarAvalForm;
        AddWindowAval = NuevoAvalWin;
        AddAvalForm = NuevoAvalForm;
        EditWindow = EditarSolicitudWin;
        EditForm = EditarSolicitudFormP;
        AddWindow = NuevaSolicitudWin;
        AddForm = NuevaSolicitudFormP;
        AddRefWin = NuevaReferenciaWin;
        AddRefForm = NuevaReferenciaForm;
    },

    getRecordRef: function () {
        var registro = GridRef.getStore().getAt(this.getIndexRef());
        if (registro != null) {
            return registro;
        }
    },

    getRecord: function () {
        var registro = Grid.getStore().getAt(this.getIndex());
        if (registro != null) {
            return registro;
        }
    },

    getRecordAval: function () {
        var registro = GridAval.getStore().getAt(this.getIndexAval());
        if (registro != null) {
            return registro;
        }
    },

    addRef: function () {
        AddRefWin.show();
    },

    add: function () {
        AddWindow.show();
    },

    addAval: function () {
        AddWindowAval.show();
    },

    removeRef: function () {
        if (GridRef.getSelectionModel().hasSelection()) {
            Ext.Msg.confirm(ConfirmMsgTitleRef, ConfirmDeleteRef, function (btn, text) {
                if (btn == 'yes') {
                    var record = GridRef.getSelectionModel().getSelected();
                    if (rec != null) {
                        EditRefForm.getForm().loadRecord(record);
                        EditRefForm.record = record;
                        DirectX.EliminarReferencias();
                    }
                }
            });
        } else {
            var msg = Ext.Msg;
            Ext.Msg.alert('Atencion', 'Seleccione al menos 1 elemento');
        }
    },

    removeAval: function () {
        if (GridAval.getSelectionModel().hasSelection()) {
            Ext.Msg.confirm(ConfirmMsgTitleAval, ConfirmDeleteAval, function (btn, text) {
                if (btn == 'yes') {
                    var record = GridAval.getSelectionModel().getSelected();
                    if (rec != null) {
                        EditAvalForm.getForm().loadRecord(record);
                        EditAvalForm.record = record;
                        DirectX.EliminarAvales();
                    }
                }
            });
        } else {
            var msg = Ext.Msg;
            Ext.Msg.alert('Atencion', 'Seleccione al menos 1 elemento');
        }
    },

    insertAval: function () {
        DirectX.InsertarAvales();
    },

    insertRef: function () {
        DirectX.InsertarReferencias();
    },

    insert: function () {
        var fields = AddForm.getForm().getFieldValues(false, "");
        Grid.insertRecord(0, fields, false);
    },

    editAval: function () {
        if (GridAval.getSelectionModel().hasSelection()) {
            var record = GridAval.getSelectionModel().getSelected();
            var index = GridAval.store.indexOf(record);
            this.setIndexAval(index);
            this.openAval();
        } else {
            var msg = Ext.Msg;
            Ext.Msg.alert('Atencion', 'Seleccione al menos 1 elemento');
        }
    },

    editRef: function () {
        if (GridRef.getSelectionModel().hasSelection()) {
            var record = GridRef.getSelectionModel().getSelected();
            var index = GridRef.store.indexOf(record);
            this.setIndexRef(index);
            this.openRef();
        } else {
            var msg = Ext.Msg;
            Ext.Msg.alert('Atencion', 'Seleccione al menos 1 elemento');
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
            Ext.Msg.alert('Atención', 'Seleccione al menos 1 elemento');
        }
    },

    edit2Aval: function (index) {
        this.SetIndexAval(index);
        this.openAval();
    },

    edit2Ref: function (index) {
        this.setIndexRef(index);
        this.openRef();
    },

    edit2: function (index) {
        this.setIndex(index);
        this.open();
    },

    nextAval: function () {
        this.edit2Aval(this.getIndexAval() + 1);
    },

    nextRef: function () {
        this.edit2Ref(this.getIndexRef() + 1);
    },

    next: function () {
        this.edit2(this.getIndex() + 1);
    },

    PreviosAval: function () {
        this.edit2Aval(this.getIndexAval() - 1);
    },

    PreviousRef: function () {
        this.edit2Ref(this.getIndexRef() - 1);
    },

    previous: function () {
        this.edit2(this.getIndex() - 1);
    },

    openAval: function () {
        rec = this.getRecordAval();
        if (rec != null) {
            EditAvalWindow.show();
            EditAvalForm.getForm().loadRecord(rec);
            EditAvalForm.record = rec;
            DirectX.SetDatosAval();
        }
    },

    openRef: function () {
        rec = this.getRecordRef();
        if (rec != null) {
            EditRefWindow.show();
            EditRefForm.getForm().loadRecord(rec);
            EditRefForm.record = rec;
        }
    },

    open: function () {
        rec = this.getRecord();
        if (rec != null) {
            EditWindow.show();
            EditForm.getForm().loadRecord(rec);
            EditForm.record = rec;
            DirectX.RefrescarSocio(rec.data.SOCIOS_ID);
            DirectX.SetIdSolicitud(rec.data.SOLICITUDES_ID);
        }
    },

    updateAval: function () {
        if (EditAvalForm.record == null) {
            return;
        }
        Ext.Msg.confirm(ConfirmMsgTitleAval, ConfirmAvalUpdate, function (btn, text) {
            if (btn == 'yes') {
                EditAvalForm.getForm().updateRecord(EditAvalForm.record);
            }
        });
    },

    updateRef: function () {
        if (EditRefForm.record == null) {
            return;
        }

        Ext.Msg.confirm(ConfirmMsgTitle, ConfirmUpdateRef, function (btn, text) {
            if (btn == 'yes') {
                EditRefForm.getForm().updateRecord(EditRefForm.record);
            }
        });
    },

    update: function () {
        if (EditForm.record == null) {
            return;
        }

        Ext.Msg.confirm(ConfirmMsgTitleRef, ConfirmUpdate, function (btn, text) {
            if (btn == 'yes') {
                EditForm.getForm().updateRecord(EditForm.record);
            }
        });
    },

    getIndexAval: function () {
        return this._indexAval;
    },

    getIndex: function () {
        return this._index;
    },

    getIndexRef: function () {
        return this._indexRef;
    },

    setIndexAval: function (index) {
        if (index > -1 && index < GridAval.getStore().getCount()) {
            this._indexAval = index;
        }
    },

    setIndex: function (index) {
        if (index > -1 && index < Grid.getStore().getCount()) {
            this._index = index;
        }
    },

    setIndexRef: function (index) {
        if (index > -1 && index < GridRef.getStore().getCount()) {
            this._indexRef = index;
        }
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
        Paginacion1.moveFirst();
    },

    navPrev: function () {
        if (Grid.getStore().getTotalCount() > 0)
            Paginacion1.movePrevious();
    },

    navNext: function () {
        if (Grid.getStore().getTotalCount() > 0)
            if (Grid.getStore().getTotalCount() > (Paginacion1.cursor + Paginacion1.pageSize))
                Paginacion1.moveNext();
    },

    navEnd: function () {
        if (Grid.getStore().getTotalCount() == 0)
            Grid.getStore().reload();
        Paginacion1.moveLast();
    },

    print: function () {
        var solicitud_id = EditIdSolicitud.getValue();
        window.open('../Reportes/ImprimirSolicitudDePrestamo.aspx?SOLICITUDES_ID=' + solicitud_id, "_blank", "resizable=yes, scrollbars=yes, top=10, left=10");
    }
};

/*---------------------*/
var applyFilter = function (field) {
    var store = SolicitudesGriP.getStore();
    store.suspendEvents();
    var filter = getRecordFilter();
    store.filterBy(filter);
    store.resumeEvents();
    SolicitudesGriP.getView().refresh(false);
};

var clearFilter = function () {
    FilterSolicitudSocioId.reset();
    FilterSolicitudId.reset();
    FilterSolicitudNombre.reset();
    FilterSolicitudMonto.reset();
    FilterSolicitudEstado.reset();
    SolicitudesSt.clearFilter();
};

var filterString = function (value, dataIndex, record) {
    var val = record.get(dataIndex);

    if (typeof val != "string") {
        return value.length == 0;
    }

    return val.toLowerCase().indexOf(value.toLowerCase()) > -1;
};

var filterDate = function (value, dataIndex, record) {
    var val = record.get(dataIndex).clearTime(true).getTime();

    if (!Ext.isEmpty(value, false) && val != value.clearTime(true).getTime()) {
        return false;
    }
    return true;
};

var filterNumber = function (value, dataIndex, record) {
    var val = record.get(dataIndex);

    if (!Ext.isEmpty(value, false) && val != value) {
        return false;
    }

    return true;
};

var getRecordFilter = function () {
    var f = [];

    f.push({
        filter: function (record) {
            return filterString(FilterSolicitudSocioId.getValue(), "SOCIOS_ID", record);
        }
    });

    f.push({
        filter: function (record) {
            return filterNumber(FilterSolicitudId.getValue(), "SOLICITUDES_ID", record);
        }
    });

    f.push({
        filter: function (record) {
            return filterString(FilterSolicitudNombre.getValue(), "SOCIOS_PRIMER_NOMBRE", record);
        }
    });

    f.push({
        filter: function (record) {
            return filterNumber(FilterSolicitudMonto.getValue(), "SOLICITUDES_MONTO", record);
        }
    });

    f.push({
        filter: function (record) {
            return filterString(FilterSolicitudEstado.getValue(), "SOLICITUD_ESTADO", record);
        }
    });

    var len = f.length;

    return function (record) {
        for (var i = 0; i < len; i++) {
            if (!f[i].filter(record)) {
                return false;
            }
        }
        return true;
    };
};