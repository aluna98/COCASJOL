<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NotasDePesoEnAdministracion.aspx.cs" Inherits="COCASJOL.WEBSITE.Source.Inventario.Ingresos.NotasDePesoEnAdministracion" %>

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
                    //EditNotaDetalleSt.reload();
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

        var EditDetailGrid = null;
        var EditDetailGridStore = null;

        var EditDetailX = {
            setReferences: function () {
                EditDetailGridStore = EditNotaDetalleSt;
                EditDetailGrid = EditNotaDetalleGridP
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
                <DocumentReady Handler="PageX.setReferences(); EditDetailX.setReferences();" />
            </Listeners>
        </ext:ResourceManager>

        <asp:ObjectDataSource ID="NotasDS" runat="server"
                TypeName="COCASJOL.LOGIC.Inventario.Ingresos.NotaDePesoEnAdministracionLogic"
                SelectMethod="GetNotasDePeso" onselecting="NotasDS_Selecting" >
                <SelectParameters>
                    <asp:ControlParameter Name="NOTAS_ID"                        Type="Int32"    ControlID="f_NOTAS_ID"                PropertyName="Text" />
                    <asp:ControlParameter Name="ESTADOS_NOTA_ID"                 Type="Int32"    ControlID="nullHdn"                   PropertyName="Text" />
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
                TypeName="COCASJOL.LOGIC.Inventario.Ingresos.NotaDePesoEnAdministracionLogic"
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
                                                <ext:RecordField Name="ESTADOS_NOTA_ID"                 />
                                                <ext:RecordField Name="ESTADOS_NOTA_NOMBRE"             ServerMapping="estados_nota_de_peso.ESTADOS_NOTA_NOMBRE" />
                                                <ext:RecordField Name="SOCIOS_ID"                       />
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
                                    <%--<ext:Column DataIndex="ESTADOS_NOTA_NOMBRE"         Header="Estado" Sortable="true" Width="150"></ext:Column>--%>
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
                                        <ext:Button ID="EditarBtn" runat="server" Text="Editar" Icon="PageWhiteEdit">
                                            <Listeners>
                                                <Click Handler="PageX.edit();" />
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
                                                <%--<ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:ComboBox
                                                            ID="f_ESTADOS_NOTA_ID" 
                                                            runat="server"
                                                            Icon="Find"
                                                            AllowBlank="true"
                                                            ForceSelection="true"
                                                            StoreID="EstadosNotaSt"
                                                            ValueField="ESTADOS_NOTA_ID" 
                                                            DisplayField="ESTADOS_NOTA_NOMBRE" 
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
                                                </ext:HeaderColumn>--%>
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

        <ext:Store ID="EditNotaDetalleSt" runat="server" WarningOnDirty="false" AutoSave="true" OnRefreshData="EditNotaDetalleSt_Refresh" >
            <Reader>
                <ext:JsonReader>
                    <Fields>
                        <ext:RecordField Name="DETALLES_CANTIDAD_SACOS" />
                        <ext:RecordField Name="DETALLES_PESO" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
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
                        <Show Handler="this.getForm().reset();" />
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
                                                                        <ext:ToolTip runat="server" Html="El numero de nota de peso es de solo lectura." Title="Numero de Nota de Peso" Width="200" TrackMouse="true" />
                                                                    </ToolTips>
                                                                </ext:NumberField>
                                                                <ext:DateField runat="server" ID="EditFechaNotaTxt" DataIndex="NOTAS_FECHA" LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Fecha" AllowBlank="false" MsgTarget="Side" ReadOnly="true">
                                                                    <ToolTips>
                                                                        <ext:ToolTip runat="server" Html="La fecha de nota de peso es de solo lectura." Title="Fecha de Nota de Peso" Width="200" TrackMouse="true" />
                                                                    </ToolTips>
                                                                </ext:DateField>
                                                                <ext:ComboBox  runat="server" ID="EditSociosIdTxt"  DataIndex="SOCIOS_ID"   LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Codigo Socio" AllowBlank="false" MsgTarget="Side" ReadOnly="true"
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
                                                                    <ToolTips>
                                                                        <ext:ToolTip ID="ToolTip1" runat="server" Html="El código de socio de nota de peso es de solo lectura." Title="Socio" Width="200" TrackMouse="true" />
                                                                    </ToolTips>
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
                                                                        <ext:FieldTrigger Icon="Empty" />
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
                                                                <ext:TextField runat="server" ID="EditEstadoNotaCmb" DataIndex="ESTADOS_NOTA_ID" LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Estado de Nota" AllowBlank="false" MsgTarget="Side" ReadOnly="true" Hidden="true" >
                                                                    <ToolTips>
                                                                        <ext:ToolTip runat="server" Html="El estado de la nota de peso es de solo lectura." Title="Estado de Nota de Peso" Width="200" TrackMouse="true" />
                                                                    </ToolTips>
                                                                </ext:TextField>
                                                                <ext:ComboBox runat="server"    ID="EditClasificacionCafeCmb"     DataIndex="CLASIFICACIONES_CAFE_ID"          LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Tipo de Café" AllowBlank="false" MsgTarget="Side" ReadOnly="true" 
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
                                                                    <ToolTips>
                                                                        <ext:ToolTip runat="server" Html="La Clasificación de Café es de solo lectura."
                                                                            Title="Clasificación de Café" Width="200" TrackMouse="true" />
                                                                    </ToolTips>
                                                                </ext:ComboBox>
                                                            </Items>
                                                        </ext:Panel>
                                                    </Items>
                                                </ext:Panel>
                                                <ext:TextField   runat="server" ID="EditNombreTxt" LabelAlign="Right" AnchorHorizontal="95%" FieldLabel="Nombre del Socio" ReadOnly="true" >
                                                    <ToolTips>
                                                        <ext:ToolTip runat="server" Html="El nombre de socio es de solo lectura." Title="Nombre del Socio" Width="200" TrackMouse="true" />
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
                                                                <ext:RadioGroup runat="server" ID="EditTipoTransporteRdGrp" LabelAlign="Right" AnchorHorizontal="100%"  FieldLabel="Forma de Transporte" ColumnsNumber="1" AutoHeight="true" ReadOnly="true">
                                                                    <Items>
                                                                        <ext:Radio ID="EditPropiRadio" runat="server" InputValue="0" BoxLabel="Carro Propio" ColumnWidth=".5" AnchorHorizontal="100%" Checked="true" ReadOnly="true">
                                                                        </ext:Radio>
                                                                        <ext:Radio ID="EditCooperativaRadio" runat="server" InputValue="1" DataIndex="NOTAS_TRANSPORTE_COOPERATIVA" BoxLabel="Carro de la Cooperativa" ColumnWidth=".5" AnchorHorizontal="100%" ReadOnly="true">
                                                                        </ext:Radio>
                                                                    </Items>
                                                                    <ToolTips>
                                                                        <ext:ToolTip ID="ToolTip2" runat="server" Html="La opción de forma de transporte de nota de peso es de solo lectura." Title="Forma de Transporte" Width="200" TrackMouse="true" />
                                                                    </ToolTips>
                                                                </ext:RadioGroup>
                                                                <ext:TextField runat="server" ID="EditPorcentajeHumedadTxt" DataIndex="NOTAS_PORCENTAJE_HUMEDAD" LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Porcentaje de Humedad" AllowBlank="false" MsgTarget="Side"  MaskRe="/[0-9\%\.]/" ReadOnly="true" >
                                                                    <ToolTips>
                                                                        <ext:ToolTip ID="ToolTip6" runat="server" Html="El porcentaje de humedad es de solo lectura." Title="Porcentaje de Humedad" Width="200" TrackMouse="true" />
                                                                    </ToolTips>
                                                                </ext:TextField>
                                                                <ext:TextField runat="server" ID="EditPorcentajeDefectoTxt" DataIndex="NOTAS_PORCENTAJE_DEFECTO" LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Porcentaje de Defecto" AllowBlank="false" MsgTarget="Side"  MaskRe="/[0-9\%\.]/" ReadOnly="true">
                                                                    <ToolTips>
                                                                        <ext:ToolTip ID="ToolTip7" runat="server" Html="El porcentaje de defecto es de solo lectura." Title="Porcentaje de Defecto" Width="200" TrackMouse="true" />
                                                                    </ToolTips>
                                                                </ext:TextField>
                                                            </Items>
                                                        </ext:FieldSet>
                                                        <ext:FieldSet ID="EditSacosFS" runat="server" Title="Sacos" Padding="7" >
                                                            <Items>
                                                                <ext:NumberField runat="server" ID="EditSumaSacosTxt"                                            LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Cantidad de Sacos" Text="0" ReadOnly="true"></ext:NumberField>
                                                                <ext:NumberField runat="server" ID="EditSacosRetenidosTxt" DataIndex="NOTAS_SACOS_RETENIDOS"     LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Sacos Retenidos" AllowBlank="false" MsgTarget="Side" ReadOnly="true">
                                                                    <ToolTips>
                                                                        <ext:ToolTip ID="ToolTip3" runat="server" Html="La cantidad de sacos retenidos es de solo lectura." Title="Sacos Retenidos" Width="200" TrackMouse="true" />
                                                                    </ToolTips>
                                                                </ext:NumberField>
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
                                                                    <LoadMask ShowMask="true" />
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
                                                        <ext:NumberField runat="server" ID="EditTaraTxt"           DataIndex="NOTAS_PESO_TARA"           LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Tara"      AllowBlank="false" MsgTarget="Side" ReadOnly="true">
                                                            <ToolTips>
                                                                <ext:ToolTip ID="ToolTip4" runat="server" Html="La cantidad de peso de tara es de solo lectura."
                                                                    Title="Tara" Width="200" TrackMouse="true" />
                                                            </ToolTips>
                                                        </ext:NumberField>
                                                        <ext:NumberField runat="server" ID="EditDescuentoTxt"      DataIndex="NOTAS_PESO_DESCUENTO"      LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Descuento" AllowBlank="false" ReadOnly="true" >
                                                            <ToolTips>
                                                                <ext:ToolTip ID="ToolTip8" runat="server" Html="La cantidad de descuento de peso es de solo lectura." Title="Descuento" Width="200" TrackMouse="true" />
                                                            </ToolTips>
                                                        </ext:NumberField>
                                                        <ext:NumberField runat="server" ID="EditTotalRecibidoTxt"  DataIndex="NOTAS_PESO_TOTAL_RECIBIDO" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Total"     AllowBlank="false" ReadOnly="true" >
                                                            <ToolTips>
                                                                <ext:ToolTip ID="ToolTip9" runat="server" Html="La cantidad total de peso recibido es de solo lectura." Title="Total Recibido" Width="200" TrackMouse="true" />
                                                            </ToolTips>
                                                        </ext:NumberField>
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
                        <ext:Button ID="EditGuardarBtn" runat="server" Text="Guardar" Icon="Disk" FormBind="true" Hidden="true" >
                            <Listeners>
                                <Click Handler="#{EditCreatedByTxt}.setValue(#{LoggedUserHdn}.getValue()); PageX.update();" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            </Items>
        </ext:Window>
    </div>
    </form>
</body>
</html>
