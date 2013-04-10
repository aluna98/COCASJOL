/// <reference name="Ext.Net.Build.Ext.Net.extjs.adapter.ext.ext-base-debug.js" assembly="Ext.Net" />
/// <reference name="Ext.Net.Build.Ext.Net.extjs.ext-all-debug-w-comments.js" assembly="Ext.Net" />

var Grid = null;
var BenGrid = null;
var EditWindow = null;
var EditForm = null;
var EditWindowBen = null;
var EditFormBen = null;
var AddWindow = null;
var AddForm = null;
var panelper = null;
var AddBenWindow = null;
var addBenForm = null;
var ConfirmMsgTitle = "Socio";
var ConfirmMsgTitleBene = "Beneficiario";
var ConfirmUpdate = "Seguro desea modificar el Socio?";
var ConfirmDelete = "Seguro desea deshabilitar el Socio?";
var ConfirmUpdateBen = "Seguro desea modificar el Beneficiario?";
var ConfirmDeleteBen = "Seguro desea Eliminar el Beneficiario?";
var Confirmacion = "Se ha finalizado correctamente";

var PageX = {
    _index: 0, _indexBen: 0,

    setReferences: function () {
        Grid = SociosGridP;
        EditWindow = EditarSocioWin;
        EditForm = EditarSocioFormP;
        AddWindow = NuevoSocioWin;
        AddForm = NuevoSocioForm;
        panelper = PanelGeneral;
        BenGrid = BeneficiariosGridP;
        AddBenWindow = NuevoBeneficiarioWin;
        addBenForm = NuevoBeneficiarioForm;
        EditFormBen = EditarBeneficiarioForm;
        EditWindowBen = EditarBeneficiarioWin;
    },

    getRecordBen: function () {
        var registro = BenGrid.getStore().getAt(indexBen);

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

    add: function () {
        AddWindow.show();
    },

    addben: function () {
        CompanyX.CienPorciento();
    },

    insert: function () {
        var fields = AddForm.getForm().getFieldValues(false, "");
        Grid.insertRecord(0, fields, false);
    },

    insertben: function () {
        CompanyX.AgregarBeneficiarioBtn_Click();
    },

    editben: function () {
        if (BenGrid.getSelectionModel().hasSelection()) {
            var record = BenGrid.getSelectionModel().getSelected();
            indexBen = BenGrid.store.indexOf(record);
            this.openBen();
        } else {
            var msg = Ext.Msg;
            Ext.Msg.alert('Atención', 'Seleccione al menos 1 elemento');
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

    openBen: function () {
        rec = this.getRecordBen();
        if (rec != null) {
            EditWindowBen.show();
            EditFormBen.getForm().loadRecord(rec);
            EditFormBen.record = rec;
        }
    },

    open: function () {
        rec = this.getRecord();
        if (rec != null) {
            EditWindow.show();
            EditForm.getForm().loadRecord(rec);
            EditForm.record = rec;
        }
    },

    removeben: function () {
        if (EditForm.record == null) {
            return;
        }
        if (BenGrid.getSelectionModel().hasSelection()) {
            Ext.Msg.confirm(ConfirmMsgTitleBene, ConfirmDeleteBen, function (btn, text) {
                if (btn == 'yes') {
                    var record = BenGrid.getSelectionModel().getSelected();
                    if (rec != null) {
                        EditFormBen.getForm().loadRecord(record);
                        EditFormBen.record = record;
                        CompanyX.EliminarBeneficiarioBtn_Click();
                        EditWindowBen.hide();
                        CompanyX.RefrescarBeneficiarios();
                    }
                }
            });
        } else {
            var msg = Ext.Msg;
            Ext.Msg.alert('Atención', 'Seleccione al menos 1 elemento');
        }
    },

    update: function () {
        if (EditForm.record == null) {
            return;
        }

        Ext.Msg.confirm(ConfirmMsgTitle, ConfirmUpdate, function (btn, text) {
            if (btn == 'yes') {
                //EditForm.record.commit(false);
                EditForm.getForm().updateRecord(EditForm.record);
            }
        });
    },

    updateben: function () {
        if (EditFormBen.record == null) {
            return;
        }
        Ext.Msg.confirm(ConfirmMsgTitleBene, ConfirmUpdateBen, function (btn, text) {
            if (btn == 'yes') {
                CompanyX.ActualizarBeneficiarioBtn_Click();
            }
        });
    },

    Disable: function () {
        if (Grid.getSelectionModel().hasSelection()) {
            Ext.Msg.confirm(ConfirmMsgTitle, ConfirmDelete, function (btn, text) {
                if (btn == 'yes') {

                }
            });
        } else {
            var msg = Ext.Msg;
            Ext.Msg.alert(AlertSelMsgTitle, AlertSelMsg);
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
                CompanyX.DoConfirmDisable(); //this.remove();
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
        var socios_id = EditsocioIdTxt.getValue();
        window.open('../Reportes/ImprimirSolicitudesDeIngresoDeSocio.aspx?SOCIOS_ID=' + socios_id, "_blank", "resizable=yes, scrollbars=yes, top=10, left=10");
    },

    openExcelImport: function () {
        ImportarSociosWin.show();
    }
};

/*---------------------*/
var applyFilter = function (field) {                
    var store = SociosGridP.getStore();
    store.suspendEvents();
    store.filterBy(getRecordFilter());                                
    store.resumeEvents();
    SociosGridP.getView().refresh(false);
};
 
var clearFilter = function () {
    FilterSocioId.reset();
    Filter1erNombre.reset();
    Filter2doNombre.reset();
    Filter1erApellido.reset();
    Filter2doApellido.reset();
    FilterResidencia.reset();
    FilterIdentidad.reset();
    FilterRTN.reset();

    SociosSt.clearFilter();
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
            return filterString(FilterSocioId.getValue(), "SOCIOS_ID", record);
        }
    });

    f.push({
        filter: function (record) {                         
            return filterString(Filter1erNombre.getValue(), "SOCIOS_PRIMER_NOMBRE", record);
        }
    });
   
    f.push({
        filter: function (record) {                         
            return filterString(Filter2doNombre.getValue(), "SOCIOS_SEGUNDO_NOMBRE", record);
        }
    });
     
    f.push({
        filter: function (record) {                         
            return filterString(Filter1erApellido.getValue(), "SOCIOS_PRIMER_APELLIDO", record);
        }
    });
     
    f.push({
        filter: function (record) {                         
            return filterString(Filter2doApellido.getValue(), "SOCIOS_SEGUNDO_APELLIDO", record);
        }
    });
     
    f.push({
        filter: function (record) {                         
            return filterString(FilterResidencia.getValue(), "SOCIOS_RESIDENCIA", record);
        }
    });

    f.push({
        filter: function (record) {                         
            return filterNumber(FilterIdentidad.getValue(), "SOCIOS_IDENTIDAD", record);
        }
    });

    f.push({
        filter: function (record) {                         
            return filterNumber(FilterRTN.getValue(), "SOCIOS_RTN", record);
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