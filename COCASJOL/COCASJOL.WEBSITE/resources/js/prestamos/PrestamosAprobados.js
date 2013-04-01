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


var SolicitudX = {
    _index: 0, _indexRef: 0, _indexAval: 0,

    setReferences: function () {
        Grid = SolicitudesGriP;
        GridRef = ReferenciasGridP;
        GridAval = AvalesGridP;
        EditWindow = EditarSolicitudWin;
        EditForm = EditarSolicitudFormP;
        EditRefWindow = EditarReferenciaWin;
        EditRefForm = EditarReferenciaForm;
        EditAvalWindow = EditarAvalWin;
        EditAvalForm = EditarAvalForm;
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
        var registro = GridAval.getStore().getAt(this.getIndexAval()); ;
        if (registro != null) {
            return registro;
        }
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
    FilterPrestamoSocioId.reset();
    FilterPrestamoId.reset();
    FilterPrestamoNombre.reset();
    FilterPrestamoMonto.reset();
    FilterPrestamoEstado.reset();
    PrestamosSt.clearFilter();
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
            return filterString(FilterPrestamoSocioId.getValue(), "SOCIOS_ID", record);
        }
    });

    f.push({
        filter: function (record) {                      
            return filterNumber(FilterPrestamoId.getValue(), "SOLICITUDES_ID", record);
        }
    });

    f.push({
        filter: function (record) {                         
            return filterString(FilterPrestamoNombre.getValue(), "SOCIOS_PRIMER_NOMBRE", record);
        }
    });
     
    f.push({
        filter: function (record) {                      
            return filterNumber(FilterPrestamoMonto.getValue(), "SOLICITUDES_MONTO", record);
        }
    });

    f.push({
        filter: function (record) {
            return filterString(FilterPrestamoEstado.getValue(), "SOLICITUD_ESTADO", record);
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