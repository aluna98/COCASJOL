<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NotasDePesoEnPesaje.aspx.cs" Inherits="COCASJOL.WEBSITE.Source.Inventario.Ingresos.NotasDePesoEnPesaje" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <%--<meta http-equiv="refresh" content="3" />--%>

    <link rel="Stylesheet" type="text/css" href="../../../resources/css/ComboBox_Grid.css" />
    <script type="text/javascript">
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
                Ext.getCmp('f_DATE_FROM').setValue(null);
                Ext.getCmp('f_DATE_TO').setValue(null);
                this.setFecha();
            },

            validateDateRange: function (field) {
                var v = this.processValue(this.getRawValue()), field;

                if (this.startDateField) {
                    field = Ext.getCmp(this.startDateField);
                    field.setMaxValue();
                    this.dateRabgeMax = null;
                } else if (this.endDateField) {
                    field = Ext.getCmp(this.endDateField);
                    field.setMinValue();
                    this.dateRangeMin = null;
                }

                field.validate();
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
                { success: function () {
                    GridStore.reload();
                    Ext.Msg.alert('Agregar Nota de Peso', 'Nota de peso agregada exitosamente.');
                    AddForm.getForm().reset();
                    AddNotaDetalleSt.removeAll();
                }
                },
                { failure: function () {
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

                    this.getNombreDeSocio(Ext.getCmp('EditSociosIdTxt'), Ext.getCmp('EditNombreTxt'));
                    this.getDireccionDeFinca(Ext.getCmp('EditSociosIdTxt'), Ext.getCmp('EditDireccionFincaTxt'));
                    EditPorcentajeDefectoTxt.setValue(Ext.util.Format.number(EditPorcentajeDefectoTxt.getValue().replace(/[\%]/g, ''), '0.00%'));
                    EditPorcentajeHumedadTxt.setValue(Ext.util.Format.number(EditPorcentajeHumedadTxt.getValue().replace(/[\%]/g, ''), '0.00%'));
                }
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
                            { success: function () {
                                GridStore.reload();
                                Ext.Msg.alert('Editar Nota de Peso', 'Nota de peso actualizada exitosamente.');
                            }
                            },
                            { failure: function () {
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
                            Ext.net.DirectMethods.DeleteNotaDePeso_Click(rec.data.NOTAS_ID);
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
                record = comboBox.findRecord(value), index = comboBox.getStore().indexOf(record);

                var nombreCompleto = record.data.SOCIOS_PRIMER_NOMBRE +
                                     (record.data.SOCIOS_SEGUNDO_NOMBRE != '' ? (' ' + record.data.SOCIOS_SEGUNDO_NOMBRE) : '') +
                                     (record.data.SOCIOS_PRIMER_APELLIDO !== '' ? (' ' + record.data.SOCIOS_PRIMER_APELLIDO) : '') +
                                     (record.data.SOCIOS_SEGUNDO_APELLIDO != '' ? (' ' + record.data.SOCIOS_SEGUNDO_APELLIDO) : '');

                nombreTxt.setValue(nombreCompleto);
            },

            getDireccionDeFinca: function (sociosIdTxt, direccionFincaTxt) {
                var comboBox = sociosIdTxt, value = comboBox.getValue();
                record = comboBox.findRecord(value), index = comboBox.getStore().indexOf(record);

                direccionFincaTxt.setValue(record.data.PRODUCCION_UBICACION_FINCA);
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

        var ConfirmMsgTitle = "Datos de Peso";
        var ConfirmUpdate = "Seguro desea modificar los datos de peos?";
        var ConfirmDelete = "Seguro desea eliminar los datos de peso?";

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

                Ext.Msg.confirm(ConfirmMsgTitle, ConfirmUpdate, function (btn, text) {
                    if (btn == 'yes') {
                        AddDetailEditForm.getForm().updateRecord(AddDetailEditForm.record);
                        Ext.getCmp('AddDetailEditBagTxt').focus(false, 200);
                    }
                });
            },

            remove: function () {
                if (AddDetailGrid.getSelectionModel().hasSelection()) {
                    Ext.Msg.confirm(ConfirmMsgTitle, ConfirmDelete, function (btn, text) {
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

        var ConfirmMsgTitle = "Datos de Peso";
        var ConfirmUpdate = "Seguro desea modificar los datos de peso?";
        var ConfirmDelete = "Seguro desea eliminar los datos de peso?";

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

                Ext.Msg.confirm(ConfirmMsgTitle, ConfirmUpdate, function (btn, text) {
                    if (btn == 'yes') {
                        EditDetailEditForm.getForm().updateRecord(EditDetailEditForm.record);
                        Ext.getCmp('EditDetailEditBagTxt').focus(false, 200);
                    }
                });
            },

            remove: function () {
                if (EditDetailGrid.getSelectionModel().hasSelection()) {
                    Ext.Msg.confirm(ConfirmMsgTitle, ConfirmDelete, function (btn, text) {
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
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="ResourceManager1" runat="server" >
            <Listeners>
                <DocumentReady Handler="PageX.setReferences(); AddDetailX.setReferences(); EditDetailX.setReferences();" />
            </Listeners>
        </ext:ResourceManager>

        <asp:ObjectDataSource ID="NotasDS" runat="server"
                TypeName="COCASJOL.LOGIC.Inventario.Ingresos.NotaDePesoEnPesajeLogic"
                SelectMethod="GetNotasDePeso" onselecting="NotasDS_Selecting" >
                <SelectParameters>
                    <asp:ControlParameter Name="NOTAS_ID"                        Type="Int32"    ControlID="f_NOTAS_ID"                PropertyName="Text" />
                    <asp:ControlParameter Name="ESTADOS_NOTA_ID"                 Type="Int32"    ControlID="nullHdn"                   PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="ESTADOS_NOTA_NOMBRE"             Type="String"   ControlID="nullHdn"                   PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="SOCIOS_ID"                       Type="String"   ControlID="f_SOCIOS_ID"               PropertyName="Text" />
                    <asp:ControlParameter Name="CLASIFICACIONES_CAFE_ID"         Type="Int32"    ControlID="f_CLASIFICACIONES_CAFE_ID" PropertyName="Text" />
                    <asp:ControlParameter Name="CLASIFICACIONES_CAFE_NOMBRE"     Type="String"   ControlID="nullHdn"                   PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="NOTAS_FECHA"                     Type="DateTime" ControlID="f_NOTAS_FECHA"             PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="FECHA_DESDE"                     Type="DateTime" ControlID="f_DATE_FROM"               PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="FECHA_HASTA"                     Type="DateTime" ControlID="f_DATE_TO"                 PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="NOTAS_TRANSPORTE_COOPERATIVA"    Type="Boolean"  ControlID="nullHdn"                   PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="NOTAS_PORCENTAJE_DEFECTO"        Type="Decimal"  ControlID="nullHdn"                   PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="NOTAS_PORCENTAJE_HUMEDAD"        Type="Decimal"  ControlID="nullHdn"                   PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="NOTAS_PESO_DEFECTO"              Type="Decimal"  ControlID="nullHdn"                   PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="NOTAS_PESO_HUMEDAD"              Type="Decimal"  ControlID="nullHdn"                   PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="NOTAS_PESO_DESCUENTO"            Type="Decimal"  ControlID="nullHdn"                   PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="NOTAS_PESO_SUMA"                 Type="Decimal"  ControlID="nullHdn"                   PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="NOTAS_PESO_TARA"                 Type="Decimal"  ControlID="nullHdn"                   PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="NOTAS_PESO_TOTAL_RECIBIDO"       Type="Decimal"  ControlID="nullHdn"                   PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="NOTAS_PESO_TOTAL_RECIBIDO_TEXTO" Type="String"   ControlID="nullHdn"                   PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="NOTAS_SACOS_RETENIDOS"           Type="Int32"    ControlID="nullHdn"                   PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="CREADO_POR"                      Type="String"   ControlID="nullHdn"                   PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="FECHA_CREACION"                  Type="DateTime" ControlID="nullHdn"                   PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="MODIFICADO_POR"                  Type="String"   ControlID="nullHdn"                   PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="FECHA_MODIFICACION"              Type="DateTime" ControlID="nullHdn"                   PropertyName="Text" DefaultValue="" />
                </SelectParameters>
        </asp:ObjectDataSource>

        <asp:ObjectDataSource ID="SociosDS" runat="server"
                TypeName="COCASJOL.LOGIC.Socios.SociosLogic"
                SelectMethod="getData" >
        </asp:ObjectDataSource>

        <asp:ObjectDataSource ID="ClasificacionesCafeDS" runat="server"
                TypeName="COCASJOL.LOGIC.Inventario.ClasificacionDeCafeLogic"
                SelectMethod="GetClasificacionesDeCafe" >
        </asp:ObjectDataSource>

        <asp:ObjectDataSource ID="EstadosNotaDS" runat="server"
                TypeName="COCASJOL.LOGIC.Inventario.Ingresos.NotaDePesoEnPesajeLogic"
                SelectMethod="GetEstadosNotaDePeso" >
        </asp:ObjectDataSource>

        <ext:Store ID="SocioSt" runat="server" DataSourceID="SociosDS">
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="SOCIOS_ID" />
                        <ext:RecordField Name="SOCIOS_PRIMER_NOMBRE" />
                        <ext:RecordField Name="SOCIOS_SEGUNDO_NOMBRE" />
                        <ext:RecordField Name="SOCIOS_PRIMER_APELLIDO" />
                        <ext:RecordField Name="SOCIOS_SEGUNDO_APELLIDO" />
                        <ext:RecordField Name="PRODUCCION_UBICACION_FINCA" ServerMapping="socios_produccion.PRODUCCION_UBICACION_FINCA" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>

        <ext:Store ID="ClasificacionesCafeSt" runat="server" DataSourceID="ClasificacionesCafeDS">
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="CLASIFICACIONES_CAFE_ID" />
                        <ext:RecordField Name="CLASIFICACIONES_CAFE_NOMBRE" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>
        
        <ext:Store ID="EstadosNotaSt" runat="server" DataSourceID="EstadosNotaDS">
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="ESTADOS_NOTA_ID" />
                        <ext:RecordField Name="ESTADOS_NOTA_NOMBRE" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>

        <ext:Hidden ID="nullHdn" runat="server" >
        </ext:Hidden>

        <ext:Hidden ID="LoggedUserHdn" runat="server" >
        </ext:Hidden>

        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:Panel ID="Panel1" runat="server" Frame="false" Header="false" Title="Notas de Peso" Icon="PageWhiteCup" Layout="Fit">
                    <Items>
                        <ext:GridPanel ID="NotasGridP" runat="server" AutoExpandColumn="CLASIFICACIONES_CAFE_NOMBRE" Height="300"
                            Title="Usuarios" Header="false" Border="false" StripeRows="true" TrackMouseOver="true">
                            <Store>
                                <ext:Store ID="NotasSt" runat="server" DataSourceID="NotasDS" AutoSave="true" SkipIdForNewRecords="false" >
                                    <Reader>
                                        <ext:JsonReader IDProperty="NOTAS_ID">
                                            <Fields>
                                                <ext:RecordField Name="NOTAS_ID"                        />
                                                <ext:RecordField Name="SOCIOS_ID"                       />
                                                <ext:RecordField Name="ESTADOS_NOTA_ID"                 />
                                                <ext:RecordField Name="CLASIFICACIONES_CAFE_ID"         />
                                                <ext:RecordField Name="CLASIFICACIONES_CAFE_NOMBRE"     ServerMapping="clasificaciones_cafe.CLASIFICACIONES_CAFE_NOMBRE"/>
                                                <ext:RecordField Name="NOTAS_FECHA"                     Type="Date" />
                                                <ext:RecordField Name="FECHA_DESDE"                     Type="Date" DefaultValue="" />
                                                <ext:RecordField Name="FECHA_HASTA"                     Type="Date" DefaultValue="" />
                                                <ext:RecordField Name="NOTAS_TRANSPORTE_COOPERATIVA"    />
                                                <ext:RecordField Name="NOTAS_PORCENTAJE_DEFECTO"        />
                                                <ext:RecordField Name="NOTAS_PORCENTAJE_HUMEDAD"        />
                                                <ext:RecordField Name="NOTAS_PESO_DEFECTO"              />
                                                <ext:RecordField Name="NOTAS_PESO_HUMEDAD"              />
                                                <ext:RecordField Name="NOTAS_PESO_DESCUENTO"            />
                                                <ext:RecordField Name="NOTAS_PESO_SUMA"                 />
                                                <ext:RecordField Name="NOTAS_PESO_TARA"                 />
                                                <ext:RecordField Name="NOTAS_PESO_TOTAL_RECIBIDO"       />
                                                <ext:RecordField Name="NOTAS_PESO_TOTAL_RECIBIDO_TEXTO" />
                                                <ext:RecordField Name="NOTAS_SACOS_RETENIDOS"           />
                                                <ext:RecordField Name="CREADO_POR"                      />
                                                <ext:RecordField Name="FECHA_CREACION"                  Type="Date" />
                                                <ext:RecordField Name="MODIFICADO_POR"                  />
                                                <ext:RecordField Name="FECHA_MODIFICACION"              Type="Date" />
                                            </Fields>
                                        </ext:JsonReader>
                                    </Reader>
                                    <Listeners>
                                        <CommitDone Handler="Ext.Msg.alert('Guardar', 'Cambios guardados exitosamente.');" />
                                    </Listeners>
                                </ext:Store>
                            </Store>
                            <ColumnModel>
                                <Columns>
                                    <ext:Column DataIndex="NOTAS_ID"                    Header="Numero" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="SOCIOS_ID"                   Header="Socio" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="CLASIFICACIONES_CAFE_NOMBRE" Header="Clasificacion de Café" Sortable="true"></ext:Column>
                                    <ext:DateColumn DataIndex="NOTAS_FECHA"             Header="Fecha" Sortable="true" Width="150" ></ext:DateColumn>
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" SingleSelect="true" />
                            </SelectionModel>
                            <TopBar>
                                <ext:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <ext:Button ID="AgregarBtn" runat="server" Text="Agregar" Icon="PageWhiteAdd" >
                                            <Listeners>
                                                <Click Handler="PageX.add();" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button ID="EditarBtn" runat="server" Text="Editar" Icon="PageWhiteEdit">
                                            <Listeners>
                                                <Click Handler="PageX.edit();" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button ID="EliminarBtn" runat="server" Text="Eliminar" Icon="PageWhiteDelete">
                                            <Listeners>
                                                <Click Handler="PageX.remove();" />
                                            </Listeners>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <View>
                                <ext:GridView ID="GridView1" runat="server">
                                    <HeaderRows>
                                        <ext:HeaderRow>
                                            <Columns>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:NumberField ID="f_NOTAS_ID" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:NumberField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="f_SOCIOS_ID" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:ComboBox
                                                            ID="f_CLASIFICACIONES_CAFE_ID" 
                                                            runat="server"
                                                            Icon="Find"
                                                            AllowBlank="true"
                                                            ForceSelection="true"
                                                            StoreID="ClasificacionesCafeSt"
                                                            ValueField="CLASIFICACIONES_CAFE_ID" 
                                                            DisplayField="CLASIFICACIONES_CAFE_NOMBRE" 
                                                            Mode="Local"
                                                            TypeAhead="true">
                                                            <Triggers>
                                                                <ext:FieldTrigger Icon="Clear"/>
                                                            </Triggers>
                                                            <Listeners>
                                                                <Select Handler="PageX.reloadGridStore();" />
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                                <TriggerClick Handler="this.clearValue();" />
                                                            </Listeners>
                                                        </ext:ComboBox>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:DropDownField ID="f_NOTAS_FECHA" AllowBlur="true" runat="server" Editable="false"
                                                            Mode="ValueText" Icon="Find" TriggerIcon="SimpleArrowDown" CollapseMode="Default">
                                                            <Component>
                                                                <ext:FormPanel ID="FormPanel1" runat="server" Height="100" Width="170" Frame="true"
                                                                    LabelWidth="50" ButtonAlign="Left" BodyStyle="padding:2px 2px;">
                                                                    <Items>
                                                                        <ext:CompositeField ID="CompositeField1" runat="server" FieldLabel="Desde" LabelWidth="50">
                                                                            <Items>
                                                                                <ext:DateField ID="f_DATE_FROM" Vtype="daterange" runat="server" Flex="1" Width="100"
                                                                                    CausesValidation="false">
                                                                                    <CustomConfig>
                                                                                        <ext:ConfigItem Name="endDateField" Value="#{f_DATE_TO}" Mode="Value" />
                                                                                    </CustomConfig>
                                                                                    <Listeners>
                                                                                        <KeyUp Fn="calendar.validateDateRange" />
                                                                                    </Listeners>
                                                                                </ext:DateField>
                                                                            </Items>
                                                                        </ext:CompositeField>
                                                                        <ext:CompositeField ID="CompositeField2" runat="server" FieldLabel="Hasta" LabelWidth="50">
                                                                            <Items>
                                                                                <ext:DateField ID="f_DATE_TO" runat="server" Vtype="daterange" Width="100">
                                                                                    <CustomConfig>
                                                                                        <ext:ConfigItem Name="startDateField" Value="#{f_DATE_FROM}" Mode="Value" />
                                                                                    </CustomConfig>
                                                                                    <Listeners>
                                                                                        <KeyUp Fn="calendar.validateDateRange" />
                                                                                    </Listeners>
                                                                                </ext:DateField>
                                                                            </Items>
                                                                        </ext:CompositeField>
                                                                    </Items>
                                                                    <Buttons>
                                                                        <ext:Button ID="Button1" Text="Ok" Icon="Accept" runat="server">
                                                                            <Listeners>
                                                                                <Click Handler="calendar.setFecha();" />
                                                                            </Listeners>
                                                                        </ext:Button>
                                                                        <ext:Button ID="Button2" Text="Cancelar" Icon="Cancel" runat="server">
                                                                            <Listeners>
                                                                                <Click Handler="calendar.clearFecha();" />
                                                                            </Listeners>
                                                                        </ext:Button>
                                                                    </Buttons>
                                                                </ext:FormPanel>
                                                                </Component>
                                                        </ext:DropDownField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                            </Columns>
                                        </ext:HeaderRow>
                                    </HeaderRows>
                                </ext:GridView>
                            </View>
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar1" runat="server" PageSize="20" StoreID="NotasSt" />
                            </BottomBar>
                            <LoadMask ShowMask="true" />
                            <SaveMask ShowMask="true" />
                            <Listeners>
                                <RowDblClick Handler="PageX.edit();" />
                            </Listeners>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>

        <ext:Store ID="AddNotaDetalleSt" runat="server" WarningOnDirty="false" IgnoreExtraFields="true" AutoSave="true" >
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="DETALLES_CANTIDAD_SACOS" />
                        <ext:RecordField Name="DETALLES_PESO" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
            <Listeners>
                <BeforeSave Handler="AddDetailX.updateSumTotals(); return false;" />
            </Listeners>
        </ext:Store>

        <ext:Window ID="AgregarNotasWin"
            runat="server"
            Hidden="true"
            Icon="PageWhiteAdd"
            Title="Agregar Notas de Peso"
            Width="640"
            AutoHeight="True"
            Resizable="false"
            Shadow="None"
            Modal="true"
            Maximizable="true"
            InitCenter="true"
            ConstrainHeader="true">
            <Listeners>
                <Show Handler="#{AddFechaNotaTxt}.setValue(new Date()); #{AddFechaNotaTxt}.focus(false,200);" />
                <Hide Handler="#{AddNotaDetalleSt}.removeAll(); #{AgregarNotasFormP}.getForm().reset();" />
            </Listeners>
            <Items>
                <ext:FormPanel ID="AgregarNotasFormP" runat="server" Title="Form Panel" Header="false" ButtonAlign="Right" MonitorValid="true" LabelAlign="Right" LabelWidth="130">
                    <Listeners>
                        <Show Handler="this.getForm().reset();" />
                    </Listeners>
                    <Items>
                        <ext:Panel ID="Panel2" runat="server" Title="Nota de Peso" Header="false" Layout="AnchorLayout" AutoHeight="True" Resizable="false" AnchorHorizontal="100%">
                            <Items>
                                <ext:Panel ID="Panel3" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" AnchorHorizontal="100%" Border="false">
                                    <Items>
                                        <ext:FieldSet ID="AddNotaHeaderFS" runat="server" Header="false" Padding="5">
                                            <Items>
                                                <ext:Panel ID="AddSocioPnl" runat ="server" Layout="ColumnLayout" Border="false" AnchorHorizontal="95%" >
                                                    <LayoutConfig>
                                                        <ext:ColumnLayoutConfig FitHeight="false" />
                                                    </LayoutConfig>
                                                    <Items>
                                                        <ext:Panel ID="Panel4" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5">
                                                            <Items>
                                                                <ext:DateField runat="server" ID="AddFechaNotaTxt" LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Fecha" AllowBlank="false" MsgTarget="Side" ></ext:DateField>
                                                                <ext:ComboBox  runat="server" ID="AddSociosIdTxt"  LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Codigo Socio" AllowBlank="false" MsgTarget="Side"
                                                                    TypeAhead="true"
                                                                    EmptyText="Seleccione un Socio"
                                                                    ForceSelection="true" 
                                                                    StoreID="SocioSt"
                                                                    Mode="Local" 
                                                                    DisplayField="SOCIOS_ID"
                                                                    ValueField="SOCIOS_ID"
                                                                    MinChars="2" 
                                                                    ListWidth="450" 
                                                                    PageSize="10" 
                                                                    ItemSelector="tr.list-item" >
                                                                    <Template ID="Template1" runat="server" Width="200">
                                                                        <Html>
					                                                        <tpl for=".">
						                                                        <tpl if="[xindex] == 1">
							                                                        <table class="cbStates-list">
								                                                        <tr>
								                	                                        <th>SOCIOS_ID</th>
								                	                                        <th>SOCIOS_PRIMER_NOMBRE</th>
                                                                                            <th>SOCIOS_PRIMER_APELLIDO</th>
								                                                        </tr>
						                                                        </tpl>
						                                                        <tr class="list-item">
							                                                        <td style="padding:3px 0px;">{SOCIOS_ID}</td>
							                                                        <td>{SOCIOS_PRIMER_NOMBRE}</td>
                                                                                    <td>{SOCIOS_PRIMER_APELLIDO}</td>
						                                                        </tr>
						                                                        <tpl if="[xcount-xindex]==0">
							                                                        </table>
						                                                        </tpl>
					                                                        </tpl>
				                                                        </Html>
                                                                    </Template>
                                                                    <Triggers>
                                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                    </Triggers>
                                                                    <Listeners>
                                                                        <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                                        <TriggerClick Handler="if (index == 0) { this.focus().clearValue(); trigger.hide(); Ext.getCmp('AddNombreTxt').reset(); Ext.getCmp('AddDireccionFincaTxt').reset(); }" />
                                                                        <Select Handler="this.triggers[0].show(); PageX.getNombreDeSocio(Ext.getCmp('AddSociosIdTxt'), Ext.getCmp('AddNombreTxt')); PageX.getDireccionDeFinca(Ext.getCmp('AddSociosIdTxt'), Ext.getCmp('AddDireccionFincaTxt'));" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                            </Items>
                                                        </ext:Panel>
                                                        <ext:Panel ID="Panel5" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5">
                                                            <Items>
                                                                <ext:ComboBox runat="server" ID="AddEstadoNotaCmb"           LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Estado de Nota" AllowBlank="false" MsgTarget="Side"
                                                                    StoreID="EstadosNotaSt"
                                                                    EmptyText="Seleccione un Estado"
                                                                    ValueField="ESTADOS_NOTA_ID" 
                                                                    DisplayField="ESTADOS_NOTA_NOMBRE"
                                                                    ForceSelection="true"
                                                                    Mode="Local"
                                                                    TypeAhead="true">
                                                                    <Triggers>
                                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                    </Triggers>
                                                                    <Listeners>
                                                                        <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                                        <TriggerClick Handler="if (index == 0) { this.focus().clearValue(); trigger.hide();}" />
                                                                        <Select Handler="this.triggers[0].show();" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                                <ext:ComboBox runat="server"    ID="AddClasificacionCafeCmb" LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Tipo de Café" AllowBlank="false" MsgTarget="Side"
                                                                    StoreID="ClasificacionesCafeSt"
                                                                    EmptyText="Seleccione un Tipo"
                                                                    ValueField="CLASIFICACIONES_CAFE_ID" 
                                                                    DisplayField="CLASIFICACIONES_CAFE_NOMBRE"
                                                                    ForceSelection="true"
                                                                    Mode="Local"
                                                                    TypeAhead="true">
                                                                    <Triggers>
                                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                    </Triggers>
                                                                    <Listeners>
                                                                        <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                                        <TriggerClick Handler="if (index == 0) { this.focus().clearValue(); trigger.hide();}" />
                                                                        <Select Handler="this.triggers[0].show();" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                            </Items>
                                                        </ext:Panel>
                                                    </Items>
                                                </ext:Panel>
                                                <ext:TextField   runat="server" ID="AddNombreTxt" LabelAlign="Right" AnchorHorizontal="95%" FieldLabel="Nombre del Socio" ReadOnly="true" >
                                                    <ToolTips>
                                                        <ext:ToolTip ID="ToolTip1" runat="server" Html="El nombre de socio es de solo lectura." Title="Nombre del Socio" Width="200" TrackMouse="true" />
                                                    </ToolTips>
                                                </ext:TextField>
                                                <ext:TextField   runat="server" ID="AddDireccionFincaTxt" LabelAlign="Right" AnchorHorizontal="95%" FieldLabel="Dirección de la Finca" ReadOnly="true" >
                                                    <ToolTips>
                                                        <ext:ToolTip ID="ToolTip2" runat="server" Html="La dirección de la finca de solo lectura." Title="Dirección de la Finca" Width="200" TrackMouse="true" />
                                                    </ToolTips>
                                                </ext:TextField>
                                            </Items>
                                        </ext:FieldSet>

                                        <ext:Panel ID="AddPorcentajeDetalleSacosPnl" runat ="server" Layout="ColumnLayout" Border="false" AnchorHorizontal="100%" >
                                            <LayoutConfig>
                                                <ext:ColumnLayoutConfig FitHeight="false" />
                                            </LayoutConfig>
                                            <Items>
                                                <ext:Panel ID="Panel6" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5" >
                                                    <Items>
                                                        <ext:FieldSet ID="AddTransportePorcentajeFS" runat="server" Title="Forma de Entrega" Padding="5">
                                                            <Items>
                                                                <ext:RadioGroup runat="server" ID="AddTipoTransporteRdGrp" LabelAlign="Right" AnchorHorizontal="100%" DataIndex="NOTAS_TRANSPORTE_COOPERATIVA" FieldLabel="Forma de Transporte" ColumnsNumber="1" AutoHeight="true">
                                                                    <Items>
                                                                        <ext:Radio ID="AddPropiRadio" runat="server" InputValue="0" BoxLabel="Carro Propio" ColumnWidth=".5" AnchorHorizontal="100%" Checked="true"></ext:Radio>
                                                                        <ext:Radio ID="AddCooperativaRadio" runat="server" InputValue="1" BoxLabel="Carro de la Cooperativa" ColumnWidth=".5" AnchorHorizontal="100%" Checked="false"></ext:Radio>
                                                                    </Items>
                                                                </ext:RadioGroup>
                                                                <ext:TextField runat="server" ID="AddPorcentajeHumedadTxt" DataIndex="NOTAS_PORCENTAJE_HUMEDAD" LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Porcentaje de Humedad" AllowBlank="false" MsgTarget="Side"  MaskRe="/[0-9\%\.]/" >
                                                                    <Listeners>
                                                                        <Change Handler="this.getEl().setValue(Ext.util.Format.number(newValue.replace(/[\%]/g, ''), '0.00%'));" />
                                                                    </Listeners>
                                                                </ext:TextField>
                                                                <ext:TriggerField runat="server" ID="AddPorcentajeDefectoTxt" DataIndex="NOTAS_PORCENTAJE_DEFECTO" LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Porcentaje de Defecto" AllowBlank="false" MsgTarget="Side"  MaskRe="/[0-9\%\.]/" >
                                                                    <Triggers>
                                                                        <ext:FieldTrigger Icon="SimpleEllipsis" Tag="Calcular" Qtip="Calcular" />
                                                                    </Triggers>
                                                                    <Listeners>
                                                                        <Change Handler="this.getEl().setValue(Ext.util.Format.number(newValue.replace(/[\%]/g, ''), '0.00%'));" />
                                                                        <TriggerClick Handler="AddDetailX.openDefectCalculation();" />
                                                                    </Listeners>
                                                                </ext:TriggerField>
                                                        </Items>
                                                        </ext:FieldSet>
                                                        <ext:FieldSet ID="AddSacosFS" runat="server" Title="Sacos" Padding="7">
                                                            <Items>
                                                                <ext:NumberField runat="server" ID="AddSumaSacosTxt"                                        LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Cantidad de Sacos" Text="0" ReadOnly="true"></ext:NumberField>
                                                                <ext:NumberField runat="server" ID="AddSacosRetenidosTxt" DataIndex="NOTAS_SACOS_RETENIDOS" LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Sacos Retenidos" AllowBlank="false" MsgTarget="Side"></ext:NumberField>
                                                            </Items>
                                                        </ext:FieldSet>
                                                    </Items>
                                                </ext:Panel>
                                                <ext:Panel ID="Panel7" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5" >
                                                    <Items>
                                                        <ext:Panel ID="AddDetallPnl" runat="server" Frame="false" Padding="5" Layout="FitLayout" Border="false">
                                                            <Items>
                                                                <ext:GridPanel ID="AddNotaDetalleGridP" runat="server" AutoExpandColumn="DETALLES_PESO"
                                                                    Height="250" Title="Detalle" Header="true" Border="true" StripeRows="true"
                                                                    TrackMouseOver="true" SelectionMemory="Disabled" StoreID="AddNotaDetalleSt" >
                                                                    <ColumnModel>
                                                                        <Columns>
                                                                            <ext:Column DataIndex="DETALLES_CANTIDAD_SACOS" Header="Sacos" MenuDisabled="true" Sortable="false" Hideable="false"></ext:Column>
                                                                            <ext:Column DataIndex="DETALLES_PESO" Header="Peso Bruto" MenuDisabled="true" Sortable="false" Hideable="false"></ext:Column>
                                                                        </Columns>
                                                                    </ColumnModel>
                                                                    <View>
                                                                        <ext:GridView ForceFit="true" MarkDirty="false" />
                                                                    </View>
                                                                    <SelectionModel>
                                                                        <ext:RowSelectionModel ID="RowSelectionModel2" runat="server" SingleSelect="true" />
                                                                    </SelectionModel>
                                                                    <TopBar>
                                                                        <ext:Toolbar ID="Toolbar2" runat="server">
                                                                            <Items>
                                                                                <ext:Button ID="AddAgregarDetalleBtn" runat="server" Text="Agregar" Icon="Add">
                                                                                    <Listeners>
                                                                                        <Click Handler="AddDetailX.add();" />
                                                                                    </Listeners>
                                                                                </ext:Button>
                                                                                <ext:Button ID="AddEditarDetalleBtn" runat="server" Text="Editar" Icon="Pencil">
                                                                                    <Listeners>
                                                                                        <Click Handler="AddDetailX.edit();" />
                                                                                    </Listeners>
                                                                                </ext:Button>
                                                                                <ext:Button ID="AddEliminarDetalleBtn" runat="server" Text="Eliminar" Icon="Delete">
                                                                                    <Listeners>
                                                                                        <Click Handler="AddDetailX.remove();" />
                                                                                    </Listeners>
                                                                                </ext:Button>
                                                                            </Items>
                                                                        </ext:Toolbar>
                                                                    </TopBar>
                                                                    <LoadMask ShowMask="true" />
                                                                    <Listeners>
                                                                        <RowDblClick Handler="AddDetailX.edit();" />
                                                                    </Listeners>
                                                                </ext:GridPanel>
                                                            </Items>
                                                        </ext:Panel>
                                                    </Items>
                                                </ext:Panel>
                                            </Items>
                                        </ext:Panel>

                                        <ext:Panel ID="AddResumenPnl" runat ="server" Layout="ColumnLayout" Border="false" AnchorHorizontal="100%" >
                                            <LayoutConfig>
                                                <ext:ColumnLayoutConfig FitHeight="false" />
                                            </LayoutConfig>
                                            <Items>
                                                <ext:Panel ID="Panel8" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5" >
                                                    <Items>
                                                    </Items>
                                                </ext:Panel>
                                                <ext:Panel ID="Panel9" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5" >
                                                    <Items>
                                                        <ext:NumberField runat="server" ID="AddSumaPesoBrutoTxt"  DataIndex="NOTAS_PESO_SUMA"           LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Suma" AllowBlank="false" MsgTarget="Side" Text="0" ReadOnly="true" ></ext:NumberField>
                                                        <ext:NumberField runat="server" ID="AddTaraTxt"           DataIndex="NOTAS_PESO_TARA"           LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Tara" AllowBlank="false" MsgTarget="Side" ></ext:NumberField>
                                                    </Items>
                                                </ext:Panel>
                                            </Items>
                                        </ext:Panel>
                                        <ext:TextField   runat="server" ID="AddCreatedByTxt"        DataIndex="CREADO_POR"               LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Creado por" Hidden="true" ></ext:TextField>
                                        <ext:TextField   runat="server" ID="AddCreatedDateTxt"      DataIndex="FECHA_CREACION"           LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Fecha de Creacion" Hidden="true" ></ext:TextField>
                                        <ext:TextField   runat="server" ID="AddModifiedByTxt"       DataIndex="MODIFICADO_POR"           LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Modificado por" Hidden="true" ></ext:TextField>
                                        <ext:TextField   runat="server" ID="AddModificationDateTxt" DataIndex="FECHA_MODIFICACION"       LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Fecha de Modificacion" Hidden="true" ></ext:TextField>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:Panel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="AddGuardarBtn" runat="server" Text="Guardar" Icon="Disk" FormBind="true">
                            <Listeners>
                                <Click Handler="#{AddCreatedByTxt}.setValue(#{LoggedUserHdn}.getValue()); PageX.insert();" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            </Items>
        </ext:Window>

        <ext:Window 
            ID="AddCalculateDefectWin" 
            runat="server" 
            Title="Calcular Defecto" 
            CenterOnLoad="true"
            Width="420" 
            AutoHeight="true"
            InitCenter="true"
            Padding="10" 
            Resizable="false" 
            Closable="true"
            Layout="Fit"
            Hidden="true"
            Modal="true"
            Constrain="true">
            <Listeners>
                <Show Handler="#{AddCalculateDefectFormPnl}.getForm().reset();" />
                <Hide Handler="#{AddPorcentajeDefectoTxt}.focus(false, 200);" />
            </Listeners>
            <Items>
                <ext:FormPanel 
                    ID="AddCalculateDefectFormPnl" 
                    runat="server" 
                    Border="false" 
                    MonitorValid="true"
                    BodyStyle="background-color:transparent;"
                    Layout="Form"
                    AutoHeight="true">
                    <Items>
                        <ext:NumberField ID="AddCalculateSampleTxt"    runat="server" MsgTarget="Side" AllowBlank="false" FieldLabel="Peso de Muestra" Width="260" EnableKeyEvents="true" >
                            <Listeners>
                                <KeyUp Handler="AddDetailX.keyUpEventAddCalculateDefect(this, e);"/>
                            </Listeners>
                        </ext:NumberField>
                        <ext:NumberField ID="AddCalculateBadSampleTxt" runat="server" MsgTarget="Side" AllowBlank="false" FieldLabel="peso de Gramos Malos"  Width="260" EnableKeyEvents="true" >
                            <Listeners>
                                <KeyUp Handler="AddDetailX.keyUpEventAddCalculateDefect(this, e);"/>
                            </Listeners>
                        </ext:NumberField>
                    </Items>
                    <Buttons>
                    <ext:Button ID="Button3" runat="server" Text="Guardar" Icon="Disk" FormBind="true">
                        <Listeners>
                            <Click Handler="AddDetailX.insertDefectCalculation();" />
                        </Listeners>
                    </ext:Button>
            </Buttons>
                </ext:FormPanel>
            </Items>
        </ext:Window>

        <ext:Window 
            ID="AddDetailAgregarDetallesWin" 
            runat="server" 
            Title="Agregar Datos de Peso" 
            CenterOnLoad="true"
            Width="420" 
            AutoHeight="true"
            InitCenter="true"
            Padding="10" 
            Resizable="false" 
            Closable="true"
            Layout="Fit"
            Hidden="true"
            Constrain="true">
            <Listeners>
                <Show Handler="#{AddDetailAgregarDetallesFormPnl}.getForm().reset();" />
                <Hide Handler="#{AddAgregarDetalleBtn}.focus(false, 200);" />
            </Listeners>
            <Items>
                <ext:FormPanel 
                    ID="AddDetailAgregarDetallesFormPnl" 
                    runat="server" 
                    Border="false" 
                    MonitorValid="true"
                    BodyStyle="background-color:transparent;"
                    Layout="Form"
                    AutoHeight="true">
                    <Items>
                        <ext:NumberField ID="AddDetailAddBagTxt"    runat="server" DataIndex="DETALLES_CANTIDAD_SACOS" MsgTarget="Side" AllowBlank="false" FieldLabel="Cantidad de Sacos" Width="260" EnableKeyEvents="true" >
                            <Listeners>
                                <KeyUp Handler="AddDetailX.keyUpEventAddDetail(this, e);"/>
                            </Listeners>
                        </ext:NumberField>
                        <ext:NumberField ID="AddDetailAddWeigthTxt" runat="server" DataIndex="DETALLES_PESO" MsgTarget="Side" AllowBlank="false" FieldLabel="Peso Bruto"  Width="260" EnableKeyEvents="true" >
                            <Listeners>
                                <KeyUp Handler="AddDetailX.keyUpEventAddDetail(this, e);"/>
                            </Listeners>
                        </ext:NumberField>
                    </Items>
                    <Buttons>
                    <ext:Button ID="AddDetailAddSaveBtn" runat="server" Text="Guardar" Icon="Disk" FormBind="true">
                        <Listeners>
                            <Click Handler="AddDetailX.insert();" />
                        </Listeners>
                    </ext:Button>
            </Buttons>
                </ext:FormPanel>
            </Items>
        </ext:Window>

        <ext:Window 
            ID="AddDetailEditarDetallesWin"
            runat="server" 
            Title="Editar Datos de Peso" 
            CenterOnLoad="true"
            Width="420" 
            AutoHeight="true"
            InitCenter="true"
            Padding="10" 
            Resizable="false" 
            Closable="true"
            Layout="Fit"
            Modal="true"
            Hidden="true"
            Constrain="true">
            <Listeners>
                <Show Handler="#{AddDetailEditarDetallesFormPnl}.getForm().reset();" />
                <Hide Handler="#{AddEditarDetalleBtn}.focus(false, 200);" />
            </Listeners>
            <Items>
                <ext:FormPanel 
                    ID="AddDetailEditarDetallesFormPnl" 
                    runat="server" 
                    Border="false" 
                    MonitorValid="true"
                    BodyStyle="background-color:transparent;"
                    Layout="Form"
                    AutoHeight="true">
                    <Items>
                        <ext:NumberField ID="AddDetailEditBagTxt"    runat="server" DataIndex="DETALLES_CANTIDAD_SACOS" MsgTarget="Side" AllowBlank="false" FieldLabel="Cantidad de Sacos" Width="260" EnableKeyEvents="true" >
                            <Listeners>
                                <KeyUp Handler="AddDetailX.keyUpEventEditDetail(this, e);" />
                            </Listeners>
                        </ext:NumberField>
                        <ext:NumberField ID="AddDetailEditWeigthTxt" runat="server" DataIndex="DETALLES_PESO" MsgTarget="Side" AllowBlank="false" FieldLabel="Peso Bruto"  Width="260" EnableKeyEvents="true" >
                            <Listeners>
                                <KeyUp Handler="AddDetailX.keyUpEventEditDetail(this, e);" />
                            </Listeners>
                        </ext:NumberField>
                    </Items>
                </ext:FormPanel>
            </Items>
            <Buttons>
                <ext:Button ID="AddDetailEditPreviousBtn" runat="server" Text="Anterior" Icon="PreviousGreen">
                    <Listeners>
                        <Click Handler="AddDetailX.previous();" />
                    </Listeners>
                </ext:Button>
                <ext:Button ID="AddDetailEditNextBtn" runat="server" Text="Siguiente" Icon="NextGreen">
                    <Listeners>
                        <Click Handler="AddDetailX.next();" />
                    </Listeners>
                </ext:Button>
                <ext:Button ID="AddDetailEditSaveBtn" runat="server" Text="Guardar" Icon="Disk" FormBind="true">
                    <Listeners>
                        <Click Handler="AddDetailX.update();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>

        <ext:Store ID="EditNotaDetalleSt" runat="server" WarningOnDirty="false" AutoSave="true" IgnoreExtraFields="true" OnRefreshData="EditNotaDetalleSt_Refresh" >
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="DETALLES_CANTIDAD_SACOS" />
                        <ext:RecordField Name="DETALLES_PESO" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
            <Listeners>
                <Remove Handler="EditDetailX.updateSumTotals();" />
                <BeforeSave Handler="EditDetailX.updateSumTotals(); return false;" />
            </Listeners>
        </ext:Store>

        <ext:Window ID="EditarNotasWin"
            runat="server"
            Hidden="true"
            Icon="PageWhiteEdit"
            Title="Editar Notas de Peso"
            Width="640"
            AutoHeight="True"
            Resizable="false"
            Shadow="None"
            Modal="true"
            Maximizable="true"
            InitCenter="true"
            ConstrainHeader="true">
            <Listeners>
                <Hide Handler="#{EditNotaDetalleSt}.removeAll(); #{EditarNotasFormP}.getForm().reset();" />
            </Listeners>
            <Items>
                <ext:FormPanel ID="EditarNotasFormP" runat="server" Title="Form Panel" Header="false" ButtonAlign="Right" MonitorValid="true" LabelAlign="Right" LabelWidth="130" >
                    <Listeners>
                        <Show Handler="this.getForm().reset(); #{EditFechaNotaTxt}.focus(false, 200);" />
                    </Listeners>
                    <Items>
                        <ext:Panel ID="Panel10" runat="server" Title="Nota de Peso" Header="false" Layout="AnchorLayout" AutoHeight="True" Resizable="false" AnchorHorizontal="100%">
                            <Items>
                                <ext:Panel ID="Panel11" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" AnchorHorizontal="100%" Border="false">
                                    <Items>
                                        <ext:FieldSet ID="EditNotaHeaderFS" runat="server" Header="false" Padding="5">
                                            <Items>
                                                <ext:Panel ID="EditSocioPnl" runat ="server" Layout="ColumnLayout" Border="false" AnchorHorizontal="95%" >
                                                    <LayoutConfig>
                                                        <ext:ColumnLayoutConfig FitHeight="false" />
                                                    </LayoutConfig>
                                                    <Items>
                                                        <ext:Panel ID="Panel12" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5">
                                                            <Items>
                                                                <ext:NumberField runat="server" ID="EditNotaIdTxt"  DataIndex="NOTAS_ID" LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Nota Numero" AllowBlank="false" ReadOnly="true" MsgTarget="Side">
                                                                    <ToolTips>
                                                                        <ext:ToolTip ID="ToolTip3" runat="server" Html="El numero de nota de peso es de solo lectura." Title="Numero de Nota de Peso" Width="200" TrackMouse="true" />
                                                                    </ToolTips>
                                                                </ext:NumberField>
                                                                <ext:DateField runat="server" ID="EditFechaNotaTxt" DataIndex="NOTAS_FECHA" LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Fecha" AllowBlank="false" MsgTarget="Side" ></ext:DateField>
                                                                <ext:ComboBox  runat="server" ID="EditSociosIdTxt"  DataIndex="SOCIOS_ID"   LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Codigo Socio" AllowBlank="false" MsgTarget="Side"
                                                                    TypeAhead="true"
                                                                    EmptyText="Seleccione un Socio"
                                                                    ForceSelection="true" 
                                                                    StoreID="SocioSt"
                                                                    Mode="Local" 
                                                                    DisplayField="SOCIOS_ID"
                                                                    ValueField="SOCIOS_ID"
                                                                    MinChars="2" 
                                                                    ListWidth="450" 
                                                                    PageSize="10" 
                                                                    ItemSelector="tr.list-item" >
                                                                    <Template ID="Template2" runat="server" Width="200">
                                                                        <Html>
					                                                        <tpl for=".">
						                                                        <tpl if="[xindex] == 1">
							                                                        <table class="cbStates-list">
								                                                        <tr>
								                	                                        <th>SOCIOS_ID</th>
								                	                                        <th>SOCIOS_PRIMER_NOMBRE</th>
                                                                                            <th>SOCIOS_PRIMER_APELLIDO</th>
								                                                        </tr>
						                                                        </tpl>
						                                                        <tr class="list-item">
							                                                        <td style="padding:3px 0px;">{SOCIOS_ID}</td>
							                                                        <td>{SOCIOS_PRIMER_NOMBRE}</td>
                                                                                    <td>{SOCIOS_PRIMER_APELLIDO}</td>
						                                                        </tr>
						                                                        <tpl if="[xcount-xindex]==0">
							                                                        </table>
						                                                        </tpl>
					                                                        </tpl>
				                                                        </Html>
                                                                    </Template>
                                                                    <Triggers>
                                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                    </Triggers>
                                                                    <Listeners>
                                                                        <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                                        <TriggerClick Handler="if (index == 0) { this.focus().clearValue(); trigger.hide(); Ext.getCmp('EditNombreTxt').reset(); Ext.getCmp('EditDireccionFincaTxt').reset(); }" />
                                                                        <Select Handler="this.triggers[0].show(); PageX.getNombreDeSocio(Ext.getCmp('EditSociosIdTxt'), Ext.getCmp('EditNombreTxt')); PageX.getDireccionDeFinca(Ext.getCmp('EditSociosIdTxt'), Ext.getCmp('EditDireccionFincaTxt'));" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                            </Items>
                                                        </ext:Panel>
                                                        <ext:Panel ID="Panel13" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5">
                                                            <Items>
                                                                <ext:ComboBox runat="server" ID="EditEstadoNotaCmb" DataIndex="ESTADOS_NOTA_ID" LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Estado de Nota" AllowBlank="false" MsgTarget="Side"
                                                                    StoreID="EstadosNotaSt"
                                                                    EmptyText="Seleccione un Estado"
                                                                    ValueField="ESTADOS_NOTA_ID" 
                                                                    DisplayField="ESTADOS_NOTA_NOMBRE"
                                                                    ForceSelection="true"
                                                                    Mode="Local"
                                                                    TypeAhead="true">
                                                                    <Triggers>
                                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                    </Triggers>
                                                                    <Listeners>
                                                                        <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                                        <TriggerClick Handler="if (index == 0) { this.focus().clearValue(); trigger.hide();}" />
                                                                        <Select Handler="this.triggers[0].show();" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                                <ext:ComboBox runat="server"    ID="EditClasificacionCafeCmb"     DataIndex="CLASIFICACIONES_CAFE_ID"          LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Tipo de Café" AllowBlank="false" MsgTarget="Side"
                                                                    StoreID="ClasificacionesCafeSt"
                                                                    EmptyText="Seleccione un Tipo"
                                                                    ValueField="CLASIFICACIONES_CAFE_ID" 
                                                                    DisplayField="CLASIFICACIONES_CAFE_NOMBRE"
                                                                    ForceSelection="true"
                                                                    Mode="Local"
                                                                    TypeAhead="true">
                                                                    <Triggers>
                                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                                    </Triggers>
                                                                    <Listeners>
                                                                        <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                                        <TriggerClick Handler="if (index == 0) { this.focus().clearValue(); trigger.hide();}" />
                                                                        <Select Handler="this.triggers[0].show();" />
                                                                    </Listeners>
                                                                </ext:ComboBox>
                                                            </Items>
                                                        </ext:Panel>
                                                    </Items>
                                                </ext:Panel>
                                                <ext:TextField   runat="server" ID="EditNombreTxt" LabelAlign="Right" AnchorHorizontal="95%" FieldLabel="Nombre del Socio" ReadOnly="true" >
                                                    <ToolTips>
                                                        <ext:ToolTip ID="ToolTip4" runat="server" Html="El nombre de socio es de solo lectura." Title="Nombre del Socio" Width="200" TrackMouse="true" />
                                                    </ToolTips>
                                                </ext:TextField>
                                                <ext:TextField   runat="server" ID="EditDireccionFincaTxt" LabelAlign="Right" AnchorHorizontal="95%" FieldLabel="Dirección de la Finca" ReadOnly="true" >
                                                    <ToolTips>
                                                        <ext:ToolTip ID="ToolTip5" runat="server" Html="La dirección de la finca de solo lectura." Title="Dirección de la Finca" Width="200" TrackMouse="true" />
                                                    </ToolTips>
                                                </ext:TextField>
                                            </Items>
                                        </ext:FieldSet>

                                        <ext:Panel ID="EditPorcentajeDetallePnl" runat ="server" Layout="ColumnLayout" Border="false" AnchorHorizontal="100%" >
                                            <LayoutConfig>
                                                <ext:ColumnLayoutConfig FitHeight="false" />
                                            </LayoutConfig>
                                            <Items>
                                                <ext:Panel ID="Panel14" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5" >
                                                    <Items>
                                                        <ext:FieldSet ID="EditTransportePorcentajeFS" runat="server" Title="Forma de Entrega" Padding="5">
                                                            <Items>
                                                                <ext:RadioGroup runat="server" ID="EditTipoTransporteRdGrp" LabelAlign="Right" AnchorHorizontal="100%"  FieldLabel="Forma de Transporte" ColumnsNumber="1" AutoHeight="true">
                                                                    <Items>
                                                                        <ext:Radio ID="EditPropiRadio" runat="server" InputValue="0" BoxLabel="Carro Propio" ColumnWidth=".5" AnchorHorizontal="100%" Checked="true">
                                                                        </ext:Radio>
                                                                        <ext:Radio ID="EditCooperativaRadio" runat="server" InputValue="1" DataIndex="NOTAS_TRANSPORTE_COOPERATIVA" BoxLabel="Carro de la Cooperativa" ColumnWidth=".5" AnchorHorizontal="100%">
                                                                        </ext:Radio>
                                                                    </Items>
                                                                </ext:RadioGroup>
                                                                <ext:TextField runat="server" ID="EditPorcentajeHumedadTxt" DataIndex="NOTAS_PORCENTAJE_HUMEDAD" LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Porcentaje de Humedad" AllowBlank="false" MsgTarget="Side"  MaskRe="/[0-9\%\.]/" >
                                                                    <Listeners>
                                                                        <Change Handler="this.getEl().setValue(Ext.util.Format.number(newValue.replace(/[\%]/g, ''), '0.00%'));" />
                                                                    </Listeners>
                                                                </ext:TextField>
                                                                <ext:TriggerField runat="server" ID="EditPorcentajeDefectoTxt" DataIndex="NOTAS_PORCENTAJE_DEFECTO" LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Porcentaje de Defecto" AllowBlank="false" MsgTarget="Side"  MaskRe="/[0-9\%\.]/" >
                                                                    <Triggers>
                                                                        <ext:FieldTrigger Icon="SimpleEllipsis" Tag="Calcular" Qtip="Calcular" />
                                                                    </Triggers>
                                                                    <Listeners>
                                                                        <Change Handler="this.getEl().setValue(Ext.util.Format.number(newValue.replace(/[\%]/g, ''), '0.00%'));" />
                                                                        <TriggerClick Handler="EditDetailX.openDefectCalculation();" />
                                                                    </Listeners>
                                                                </ext:TriggerField>
                                                            </Items>
                                                        </ext:FieldSet>
                                                        <ext:FieldSet ID="EditSacosFS" runat="server" Title="Sacos" Padding="7" >
                                                            <Items>
                                                                <ext:NumberField runat="server" ID="EditSumaSacosTxt"                                            LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Cantidad de Sacos" Text="0" ReadOnly="true"></ext:NumberField>
                                                                <ext:NumberField runat="server" ID="EditSacosRetenidosTxt" DataIndex="NOTAS_SACOS_RETENIDOS"     LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Sacos Retenidos" AllowBlank="false" MsgTarget="Side"></ext:NumberField>
                                                            </Items>
                                                        </ext:FieldSet>
                                                    </Items>
                                                </ext:Panel>
                                                <ext:Panel ID="Panel15" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5" >
                                                    <Items>
                                                        <ext:Panel ID="EditDetallPnl" runat="server" Frame="false" Padding="5" Layout="FitLayout" Border="false">
                                                            <Items>
                                                                <ext:GridPanel ID="EditNotaDetalleGridP" runat="server" AutoExpandColumn="DETALLES_PESO"
                                                                    Height="250" Title="Detalle" Header="true" Border="true" StripeRows="true"
                                                                    TrackMouseOver="true" SelectionMemory="Disabled" StoreID="EditNotaDetalleSt" >
                                                                    <ColumnModel>
                                                                        <Columns>
                                                                            <ext:Column DataIndex="DETALLES_CANTIDAD_SACOS" Header="Sacos" MenuDisabled="true" Sortable="false" Hideable="false"></ext:Column>
                                                                            <ext:Column DataIndex="DETALLES_PESO" Header="Peso Bruto" MenuDisabled="true" Sortable="false" Hideable="false"></ext:Column>
                                                                        </Columns>
                                                                    </ColumnModel>
                                                                    <View>
                                                                        <ext:GridView ForceFit="true" MarkDirty="false" />
                                                                    </View>
                                                                    <SelectionModel>
                                                                        <ext:RowSelectionModel ID="RowSelectionModel3" runat="server" SingleSelect="true" />
                                                                    </SelectionModel>
                                                                    <TopBar>
                                                                        <ext:Toolbar ID="Toolbar3" runat="server">
                                                                            <Items>
                                                                                <ext:Button ID="EditAgregarDetalleBtn" runat="server" Text="Agregar" Icon="Add">
                                                                                    <Listeners>
                                                                                        <Click Handler="EditDetailX.add();" />
                                                                                    </Listeners>
                                                                                </ext:Button>
                                                                                <ext:Button ID="EditEditarDetalleBtn" runat="server" Text="Editar" Icon="Pencil">
                                                                                    <Listeners>
                                                                                        <Click Handler="EditDetailX.edit();" />
                                                                                    </Listeners>
                                                                                </ext:Button>
                                                                                <ext:Button ID="EditEliminarDetalleBtn" runat="server" Text="Eliminar" Icon="Delete">
                                                                                    <Listeners>
                                                                                        <Click Handler="EditDetailX.remove();" />
                                                                                    </Listeners>
                                                                                </ext:Button>
                                                                            </Items>
                                                                        </ext:Toolbar>
                                                                    </TopBar>
                                                                    <LoadMask ShowMask="true" />
                                                                    <Listeners>
                                                                        <RowDblClick Handler="EditDetailX.edit();" />
                                                                    </Listeners>
                                                                </ext:GridPanel>
                                                            </Items>
                                                        </ext:Panel>
                                                    </Items>
                                                </ext:Panel>
                                            </Items>
                                        </ext:Panel>

                                        <ext:Panel ID="EditSacosResumenPnl" runat ="server" Layout="ColumnLayout" Border="false" AnchorHorizontal="100%" >
                                            <LayoutConfig>
                                                <ext:ColumnLayoutConfig FitHeight="false" />
                                            </LayoutConfig>
                                            <Items>
                                                <ext:Panel ID="Panel16" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5" >
                                                    <Items>
                                                    </Items>
                                                </ext:Panel>
                                                <ext:Panel ID="Panel17" runat="server" Layout="AnchorLayout" Border="false" ColumnWidth=".5" >
                                                    <Items>
                                                        <ext:NumberField runat="server" ID="EditSumaPesoBrutoTxt"  DataIndex="NOTAS_PESO_SUMA"           LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Suma"      AllowBlank="false" Text="0" ReadOnly="true" MsgTarget="Side"></ext:NumberField>
                                                        <ext:NumberField runat="server" ID="EditTaraTxt"           DataIndex="NOTAS_PESO_TARA"           LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Tara"      AllowBlank="false" MsgTarget="Side" ></ext:NumberField>
                                                    </Items>
                                                </ext:Panel>
                                            </Items>
                                        </ext:Panel>
                                        <ext:TextField   runat="server" ID="EditCreatedByTxt"        DataIndex="CREADO_POR"               LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Creado por" Hidden="true" ></ext:TextField>
                                        <ext:TextField   runat="server" ID="EditCreatedDateTxt"      DataIndex="FECHA_CREACION"           LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Fecha de Creacion" Hidden="true" ></ext:TextField>
                                        <ext:TextField   runat="server" ID="EditModifiedByTxt"       DataIndex="MODIFICADO_POR"           LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Modificado por" Hidden="true" ></ext:TextField>
                                        <ext:TextField   runat="server" ID="EditModificationDateTxt" DataIndex="FECHA_MODIFICACION"       LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Fecha de Modificacion" Hidden="true" ></ext:TextField>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:Panel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="EditPreviousBtn" runat="server" Text="Anterior" Icon="PreviousGreen">
                            <Listeners>
                                <Click Handler="PageX.previous();" />
                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="EditNextBtn" runat="server" Text="Siguiente" Icon="NextGreen">
                            <Listeners>
                                <Click Handler="PageX.next();" />
                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="EditGuardarBtn" runat="server" Text="Guardar" Icon="Disk" FormBind="true" >
                            <Listeners>
                                <Click Handler="#{EditCreatedByTxt}.setValue(#{LoggedUserHdn}.getValue()); PageX.update();" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            </Items>
        </ext:Window>

        <ext:Window 
            ID="EditCalculateDefectWin" 
            runat="server" 
            Title="Calcular Defecto" 
            CenterOnLoad="true"
            Width="420" 
            AutoHeight="true"
            InitCenter="true"
            Padding="10" 
            Resizable="false" 
            Closable="true"
            Layout="Fit"
            Hidden="true"
            Modal="true"
            Constrain="true">
            <Listeners>
                <Show Handler="#{EditCalculateDefectFormPnl}.getForm().reset();" />
                <Hide Handler="#{EditPorcentajeDefectoTxt}.focus(false, 200);" />
            </Listeners>
            <Items>
                <ext:FormPanel 
                    ID="EditCalculateDefectFormPnl" 
                    runat="server" 
                    Border="false" 
                    MonitorValid="true"
                    BodyStyle="background-color:transparent;"
                    Layout="Form"
                    AutoHeight="true">
                    <Items>
                        <ext:NumberField ID="EditCalculateSampleTxt"    runat="server" MsgTarget="Side" AllowBlank="false" FieldLabel="Peso de Muestra" Width="260" EnableKeyEvents="true" >
                            <Listeners>
                                <KeyUp Handler="EditDetailX.keyUpEventEditCalculateDefect(this, e);"/>
                            </Listeners>
                        </ext:NumberField>
                        <ext:NumberField ID="EditCalculateBadSampleTxt" runat="server" MsgTarget="Side" AllowBlank="false" FieldLabel="peso de Gramos Malos"  Width="260" EnableKeyEvents="true" >
                            <Listeners>
                                <KeyUp Handler="EditDetailX.keyUpEventEditCalculateDefect(this, e);"/>
                            </Listeners>
                        </ext:NumberField>
                    </Items>
                    <Buttons>
                    <ext:Button ID="Button7" runat="server" Text="Guardar" Icon="Disk" FormBind="true">
                        <Listeners>
                            <Click Handler="EditDetailX.insertDefectCalculation();" />
                        </Listeners>
                    </ext:Button>
            </Buttons>
                </ext:FormPanel>
            </Items>
        </ext:Window>

        <ext:Window 
            ID="EditDetailAgregarDetallesWin" 
            runat="server" 
            Title="Agregar Datos de Peso" 
            CenterOnLoad="true"
            Width="420" 
            AutoHeight="true"
            InitCenter="true"
            Padding="10" 
            Resizable="false" 
            Closable="true"
            Layout="Fit"
            Hidden="true"
            Constrain="true">
            <Listeners>
                <Show Handler="#{EditDetailAgregarDetallesFormPnl}.getForm().reset();" />
                <Hide Handler="#{EditAgregarDetalleBtn}.focus(false, 200);" />
            </Listeners>
            <Items>
                <ext:FormPanel 
                    ID="EditDetailAgregarDetallesFormPnl" 
                    runat="server" 
                    Border="false" 
                    MonitorValid="true"
                    BodyStyle="background-color:transparent;"
                    Layout="Form"
                    AutoHeight="true">
                    <Items>
                        <ext:NumberField ID="EditDetailAddBagTxt"    runat="server" DataIndex="DETALLES_CANTIDAD_SACOS" MsgTarget="Side" AllowBlank="false" FieldLabel="Cantidad de Sacos" Width="260" EnableKeyEvents="true" >
                            <Listeners>
                                <KeyUp Handler="EditDetailX.keyUpEventAddDetail(this, e);"/>
                            </Listeners>
                        </ext:NumberField>
                        <ext:NumberField ID="EditDetailAddWeigthTxt" runat="server" DataIndex="DETALLES_PESO" MsgTarget="Side" AllowBlank="false" FieldLabel="Peso Bruto"  Width="260" EnableKeyEvents="true" >
                            <Listeners>
                                <KeyUp Handler="EditDetailX.keyUpEventAddDetail(this, e);"/>
                            </Listeners>
                        </ext:NumberField>
                    </Items>
                    <Buttons>
                    <ext:Button ID="EditDetailAddSaveBtn" runat="server" Text="Guardar" Icon="Disk" FormBind="true">
                        <Listeners>
                            <Click Handler="EditDetailX.insert();" />
                        </Listeners>
                    </ext:Button>
            </Buttons>
                </ext:FormPanel>
            </Items>
        </ext:Window>

        <ext:Window 
            ID="EditDetailEditarDetallesWin"
            runat="server" 
            Title="Editar Datos de Peso" 
            CenterOnLoad="true"
            Width="420" 
            AutoHeight="true"
            InitCenter="true"
            Padding="10" 
            Resizable="false" 
            Closable="true"
            Layout="Fit"
            Modal="true"
            Hidden="true"
            Constrain="true">
            <Listeners>
                <Show Handler="#{EditDetailEditarDetallesFormPnl}.getForm().reset();" />
                <Hide Handler="#{EditEditarDetalleBtn}.focus(false, 200);" />
            </Listeners>
            <Items>
                <ext:FormPanel 
                    ID="EditDetailEditarDetallesFormPnl" 
                    runat="server" 
                    Border="false" 
                    MonitorValid="true"
                    BodyStyle="background-color:transparent;"
                    Layout="Form"
                    AutoHeight="true">
                    <Items>
                        <ext:NumberField ID="EditDetailEditBagTxt"    runat="server" DataIndex="DETALLES_CANTIDAD_SACOS" MsgTarget="Side" AllowBlank="false" FieldLabel="Cantidad de Sacos" Width="260" EnableKeyEvents="true" >
                            <Listeners>
                                <KeyUp Handler="EditDetailX.keyUpEventEditDetail(this, e);" />
                            </Listeners>
                        </ext:NumberField>
                        <ext:NumberField ID="EditDetailEditWeigthTxt" runat="server" DataIndex="DETALLES_PESO" MsgTarget="Side" AllowBlank="false" FieldLabel="Peso Bruto"  Width="260" EnableKeyEvents="true" >
                            <Listeners>
                                <KeyUp Handler="EditDetailX.keyUpEventEditDetail(this, e);" />
                            </Listeners>
                        </ext:NumberField>
                    </Items>
                </ext:FormPanel>
            </Items>
            <Buttons>
                <ext:Button ID="EditDetailEditPreviousBtn" runat="server" Text="Anterior" Icon="PreviousGreen">
                    <Listeners>
                        <Click Handler="EditDetailX.previous();" />
                    </Listeners>
                </ext:Button>
                <ext:Button ID="EditDetailEditNextBtn" runat="server" Text="Siguiente" Icon="NextGreen">
                    <Listeners>
                        <Click Handler="EditDetailX.next();" />
                    </Listeners>
                </ext:Button>
                <ext:Button ID="EditDetailEditSaveBtn" runat="server" Text="Guardar" Icon="Disk" FormBind="true">
                    <Listeners>
                        <Click Handler="EditDetailX.update();" />
                    </Listeners>
                </ext:Button>
            </Buttons>
        </ext:Window>
    </div>
    </form>
</body>
</html>
