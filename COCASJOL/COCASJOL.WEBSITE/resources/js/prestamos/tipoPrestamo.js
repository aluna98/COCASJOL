/// <reference name="Ext.Net.Build.Ext.Net.extjs.adapter.ext.ext-base-debug.js" assembly="Ext.Net" />
/// <reference name="Ext.Net.Build.Ext.Net.extjs.ext-all-debug-w-comments.js" assembly="Ext.Net" />

var Grid = null;
var EditWindow = null;
var EditForm = null;
var AddWindow = null;
var AddForm = null;
var ConfirmMsgTitle = "Prestamos";
var ConfirmEdit = "Seguro desea modificar la informacion  del prestamo?";
var Confirmacion = "Se ha finalizado correctamente";
var ConfirmDelete = "Esta seguro que desea eliminar el prestamo?";

var PageX = {
    _index: 0,

    setReferences: function () {
        Grid = PrestamosGridP;
        EditWindow = EditarPrestamosWin;
        EditForm = EditarPrestamoFormP;
        AddWindow = AgregarPrestamosWin;
        AddForm = AgregarPrestamoFormP;
    },

    getRecord: function () {
        var registro = Grid.getStore().getAt(this.getIndex());

        if (registro != null) {
            return registro;
        }
    },

    getIndex: function () {
        return this._index;
    },

    setIndex: function (index) {
        if (index > -1 && index < Grid.getStore().getCount()) {
            this._index = index;
        }
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

    update: function () {
        if (EditForm.record == null) {
            return;
        }

        Ext.Msg.confirm(ConfirmMsgTitle, ConfirmEdit, function (btn, text) {
            if (btn == 'yes') {
                EditForm.getForm().updateRecord(EditForm.record);
            }
        });
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

    edit2: function (index) {
        this.setIndex(index);
        this.open();
    },

    add: function () {
        AddWindow.show();
    },

    insert: function () {
        var fields = AddForm.getForm().getFieldValues(false, "");
        Grid.insertRecord(0, fields, false);
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
            Ext.Msg.alert('Atención', 'Seleccione al menos 1 elemento');
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
        pagingtoolbar.moveFirst();
    },

    navPrev: function () {
        if (Grid.getStore().getTotalCount() > 0)
            pagingtoolbar.movePrevious();
    },

    navNext: function () {
        if (Grid.getStore().getTotalCount() > 0)
            if (Grid.getStore().getTotalCount() > (pagingtoolbar.cursor + pagingtoolbar.pageSize))
                pagingtoolbar.moveNext();
    },

    navEnd: function () {
        if (Grid.getStore().getTotalCount() == 0)
            Grid.getStore().reload();
        pagingtoolbar.moveLast();
    }
};


var applyFilter = function (field) {                
    var store = PrestamosGridP.getStore();
    store.suspendEvents();
    store.filterBy(getRecordFilter());
    store.resumeEvents();
    PrestamosGridP.getView().refresh(false);
};
             
var clearFilter = function () {
    FilterPrestamoId.reset();
    FilterPrestamoNombre.reset();
    FilterPrestamoDescrip.reset();
    FilterPrestamoCantMax.reset();
    FilterPrestamosInteres.reset();

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
            return filterString(FilterPrestamoId.getValue(), "PRESTAMOS_ID", record);
        }
    });

    f.push({
        filter: function (record) {                         
            return filterString(FilterPrestamoNombre.getValue(), "PRESTAMOS_NOMBRE", record);
        }
    });
   
    f.push({
        filter: function (record) {                         
            return filterString(FilterPrestamoDescrip.getValue(), "PRESTAMOS_DESCRIPCION", record);
        }
    });
     
    f.push({
        filter: function (record) {                      
            return filterNumber(FilterPrestamoCantMax.getValue(), "PRESTAMOS_CANT_MAXIMA", record);
        }
    });
     
    f.push({
        filter: function (record) {                         
            return filterNumber(FilterPrestamosInteres.getValue(), "PRESTAMOS_INTERES", record);
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
