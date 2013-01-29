﻿/// <reference name="Ext.Net.Build.Ext.Net.extjs.adapter.ext.ext-base-debug.js" assembly="Ext.Net" />
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
var ConfirmMsgTitleRef = "Referencia";
var ConfirmUpdateRef = "Seguro que desea editar la Referencia?";
var ConfirmMsgTitle = "Socio";
var ConfirmUpdate = "Seguro que desea editar la Solicitud?";
var ConfirmDelete = "Seguro desea rechazar la Solicitud?";
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

    addRef: function () {
        AddRefWin.show();
    },

    add: function () {
        AddWindow.show();
    },

    removeRef: function () {
        if (EditRefForm.record == null) {
            return;
        }
        if (GridRef.getSelectionModel().hasSelection()) {
            Ext.Msg.confirm(ConfirmMsgTitleRef, ConfirmDelete, function (btn, text) {
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

    insertRef: function () {
        DirectX.InsertarReferencias();
    },

    insert: function () {
        var fields = AddForm.getForm().getFieldValues(false, "");
        Grid.insertRecord(0, fields, false);
    },

    editRef: function () {
        if (GridRef.getSelectionModel().hasSelection()) {
            var record = GridRef.getSelectionModel().getSelected();
            var index = GridRef.store.indexOf(record);
            this.setIndexRef(index);
            this.openRef();
        } else {
            var msg = Ext
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

    edit2Ref: function (index) {
        this.setIndexRef(index);
        this.openRef();
    },

    edit2: function (index) {
        this.setIndex(index);
        this.open();
    },

    nextRef: function () {
        this.edit2Ref(this.getIndexRef() + 1);
    },

    next: function () {
        this.edit2(this.getIndex() + 1);
    },

    PreviousRef: function () {
        this.edit2Ref(this.getIndexRef() - 1);
    },

    previous: function () {
        this.edit2(this.getIndex() - 1);
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
            DirectX.RefrescarReferencias(rec.data.SOLICITUDES_ID);
        }
    },

    updateRef: function () {
        if (EditRefForm.record == null) {
            return;
        }

        Ext.Msg.confirm(ConfirmMsgTitle, ConfirmUpdate, function (btn, text) {
            if (btn == 'yes') {
                EditRefForm.getForm().updateRecord(EditRefForm.record);
            }
        });
    },

    update: function () {
        if (EditForm.record == null) {
            return;
        }

        Ext.Msg.confirm(ConfirmMsgTitleRef, ConfirmUpdateRef, function (btn, text) {
            if (btn == 'yes') {
                EditForm.getForm().updateRecord(EditForm.record);
            }
        });
    },

    getIndex: function () {
        return this._index;
    },

    getIndexRef: function () {
        return this._indexRef;
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
    }
};

/*---------------------*/
var applyFilter = function (field) {                
    var store = SolicitudesGriP.getStore();
    store.suspendEvents();
    store.filterBy(getRecordFilter());                                
    store.resumeEvents();
    SolicitudesGriP.getView().refresh(false);
};
 
var clearFilter = function () {
    FilterSolicitudSocioId.reset();
    FilterSolicitudId.reset();
    FilterSolicitudNombre.reset();
    FilterSolicitudMonto.reset();

    SolicitudesSt.clearFilter();
}

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