<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AportacionesPorSocio.aspx.cs" Inherits="COCASJOL.WEBSITE.Source.Aportaciones.AportacionesPorSocio" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="~/Source/Auditoria/Auditoria.ascx" TagPrefix="aud" TagName="Auditoria" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Aportaciones por Socio</title>
    <script type="text/javascript" src="../../resources/js/aportaciones/aportacionesPorSocio.js" ></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="ResourceManager1" runat="server"  DisableViewState="true" >
            <Listeners>
                <DocumentReady Handler="PageX.setReferences();" />
            </Listeners>
        </ext:ResourceManager>

        <ext:KeyNav ID="KeyNav1" runat="server" Target="={document.body}" >
            <Home Handler="PageX.navHome();" />
            <PageUp Handler="PageX.navPrev();" />
            <PageDown Handler="PageX.navNext();" />
            <End Handler="PageX.navEnd();" />
        </ext:KeyNav>

        <aud:Auditoria runat="server" ID="AudWin" />

        <asp:ObjectDataSource ID="AportacionesDS" runat="server"
                TypeName="COCASJOL.LOGIC.Aportaciones.AportacionLogic"
                SelectMethod="GetAportaciones" onselecting="AportacionesDS_Selecting" >
                <SelectParameters>
                    <asp:ControlParameter Name="SOCIOS_ID"                                   Type="String"   ControlID="f_SOCIOS_ID"                PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="SOCIOS_NOMBRE_COMPLETO"                      Type="String"   ControlID="f_SOCIOS_NOMBRE_COMPLETO"   PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="APORTACIONES_ORDINARIA_SALDO"                Type="Decimal"  ControlID="nullHdn"                    PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="APORTACIONES_EXTRAORDINARIA_SALDO"           Type="Decimal"  ControlID="nullHdn"                    PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="APORTACIONES_CAPITALIZACION_RETENCION_SALDO" Type="Decimal"  ControlID="nullHdn"                    PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="APORTACIONES_INTERESES_S_APORTACION_SALDO"   Type="Decimal"  ControlID="nullHdn"                    PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="APORTACIONES_EXCEDENTE_PERIODO_SALDO"        Type="Decimal"  ControlID="nullHdn"                    PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="APORTACIONES_SALDO_TOTAL"                    Type="Decimal"  ControlID="f_APORTACIONES_SALDO_TOTAL" PropertyName="Text" DefaultValue="-1" />
                    <asp:ControlParameter Name="CREADO_POR"                                  Type="String"   ControlID="nullHdn"                    PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="FECHA_CREACION"                              Type="DateTime" ControlID="nullHdn"                    PropertyName="Text" DefaultValue="" />
                </SelectParameters>
        </asp:ObjectDataSource>

        <asp:ObjectDataSource ID="SociosDS" runat="server"
                TypeName="COCASJOL.LOGIC.Socios.SociosLogic"
                SelectMethod="getData" >
        </asp:ObjectDataSource>

        <ext:Store ID="SocioSt" runat="server" DataSourceID="SociosDS">
            <Reader>
                <ext:JsonReader IDProperty="SOCIOS_ID">
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
        
        <ext:Hidden ID="nullHdn" runat="server" >
        </ext:Hidden>

        <ext:Hidden ID="LoggedUserHdn" runat="server" >
        </ext:Hidden>

        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:Panel ID="Panel1" runat="server" Frame="false" Header="false" Title="Aportaciones" Icon="Coins" Layout="Fit">
                    <Items>
                        <ext:GridPanel ID="AportacionesGridP" runat="server" AutoExpandColumn="SOCIOS_NOMBRE_COMPLETO" Height="300"
                            Title="Aportaciones por Socio" Header="false" Border="false" StripeRows="true" TrackMouseOver="true">
                            <KeyMap>
                                <ext:KeyBinding Ctrl="true" >
                                    <Keys>
                                        <ext:Key Code="ENTER" />
                                    </Keys>
                                    <Listeners>
                                        <Event Handler="PageX.gridKeyUpEvent(this, e);" />
                                    </Listeners>
                                </ext:KeyBinding>
                            </KeyMap>
                            <Store>
                                <ext:Store ID="AportacionesSt" runat="server" DataSourceID="AportacionesDS" AutoSave="true" SkipIdForNewRecords="false" >
                                    <Reader>
                                        <ext:JsonReader IDProperty="SOCIOS_ID">
                                            <Fields>
                                                <ext:RecordField Name="SOCIOS_ID"                                   />
                                                <ext:RecordField Name="SOCIOS_NOMBRE_COMPLETO"                      />
                                                <ext:RecordField Name="APORTACIONES_ORDINARIA_SALDO"                />
                                                <ext:RecordField Name="APORTACIONES_EXTRAORDINARIA_SALDO"           />
                                                <ext:RecordField Name="APORTACIONES_CAPITALIZACION_RETENCION_SALDO" />
                                                <ext:RecordField Name="APORTACIONES_INTERESES_S_APORTACION_SALDO"   />
                                                <ext:RecordField Name="APORTACIONES_EXCEDENTE_PERIODO_SALDO"        />
                                                <ext:RecordField Name="APORTACIONES_SALDO_TOTAL"                    />
                                                <ext:RecordField Name="CREADO_POR"                                  />
                                                <ext:RecordField Name="FECHA_CREACION"                              Type="Date" />
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
                                    <ext:Column       DataIndex="SOCIOS_ID"                                   Header="Id de Socio" Sortable="true"></ext:Column>
                                    <ext:Column       DataIndex="SOCIOS_NOMBRE_COMPLETO"                      Header="Nombre Completo de Socio" Sortable="true"></ext:Column>
                                    <ext:NumberColumn DataIndex="APORTACIONES_ORDINARIA_SALDO"                Header="Saldo Aportación Ordinaria" Sortable="true"></ext:NumberColumn>
                                    <ext:NumberColumn DataIndex="APORTACIONES_EXTRAORDINARIA_SALDO"           Header="Saldo Aportación Extraordinaria" Sortable="true"></ext:NumberColumn>
                                    <ext:NumberColumn DataIndex="APORTACIONES_CAPITALIZACION_RETENCION_SALDO" Header="Saldo Capitalización por Retención" Sortable="true"></ext:NumberColumn>
                                    <ext:NumberColumn DataIndex="APORTACIONES_INTERESES_S_APORTACION_SALDO"   Header="Saldo Intereses sobre Aportaciones" Sortable="true"></ext:NumberColumn>
                                    <ext:NumberColumn DataIndex="APORTACIONES_EXCEDENTE_PERIODO_SALDO"        Header="Saldo Excedente de Período" Sortable="true"></ext:NumberColumn>
                                    <ext:NumberColumn DataIndex="APORTACIONES_SALDO_TOTAL"                    Header="Saldo Total" Sortable="true"></ext:NumberColumn>

                                    <ext:Column DataIndex="SOCIOS_ID" Width="28" Sortable="false" MenuDisabled="true" Header="&nbsp;" Fixed="true">
                                        <Renderer Handler="return '';" />
                                    </ext:Column>
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" SingleSelect="true" />
                            </SelectionModel>
                            <TopBar>
                                <ext:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <ext:Button ID="EditarBtn" runat="server" Text="Ver" Icon="Coins">
                                            <Listeners>
                                                <Click Handler="PageX.edit();" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:ToolbarFill ID="ToolbarFill1" runat="server" />
                                        <ext:Button ID="Export_PDFBtn" runat="server" Text="Exportar PDF" Icon="PageWhiteAcrobat">
                                            <DirectEvents>
                                                <Click OnEvent="Export_PDFBtn_Click" IsUpload="true"></Click>
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:Button ID="Export_ExcelBtn" runat="server" Text="Exportar Excel" Icon="PageExcel">
                                            <DirectEvents>
                                                <Click OnEvent="Export_ExcelBtn_Click" IsUpload="true"></Click>
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:Button ID="AuditoriaBtn" runat="server" Text="Auditoria" Icon="Cog">
                                            <Listeners>
                                                <Click Handler="PageX.showAudit();" />
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
                                                        <ext:TextField ID="f_SOCIOS_ID" runat="server" EnableKeyEvents="true" Icon="Find" MaxLength="5">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="f_SOCIOS_NOMBRE_COMPLETO" runat="server" EnableKeyEvents="true" Icon="Find" MaxLength="5">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>

                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:NumberField ID="f_APORTACIONES_ORDINARIA_SALDO" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:NumberField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:NumberField ID="f_APORTACIONES_EXTRAORDINARIA_SALDO" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:NumberField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:NumberField ID="f_APORTACIONES_CAPITALIZACION_RETENCION_SALDO" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:NumberField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:NumberField ID="f_APORTACIONES_INTERESES_S_APORTACION_SALDO" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:NumberField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:NumberField ID="f_APORTACIONES_EXCEDENTE_PERIODO_SALDO" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:NumberField>
                                                    </Component>
                                                </ext:HeaderColumn>

                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:NumberField ID="f_APORTACIONES_SALDO_TOTAL" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:NumberField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                               
                                                <ext:HeaderColumn AutoWidthElement="false">
                                                    <Component>
                                                        <ext:Button ID="ClearFilterButton" runat="server" Icon="Cancel">
                                                            <ToolTips>
                                                                <ext:ToolTip ID="ToolTip4" runat="server" Html="Clear filter" />
                                                            </ToolTips>
                                                            <Listeners>
                                                                <Click Handler="PageX.clearFilter();" />
                                                            </Listeners>                                            
                                                        </ext:Button>
                                                    </Component>
                                                </ext:HeaderColumn>
                                            </Columns>
                                        </ext:HeaderRow>
                                    </HeaderRows>
                                </ext:GridView>
                            </View>
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar1" runat="server" PageSize="20" StoreID="AportacionesSt" />
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

        <ext:Window ID="EditarAportacionWin"
            runat="server"
            Hidden="true"
            Icon="Coins"
            Title="Aportaciones de Socio"
            Width="500"
            Layout="FormLayout"
            AutoHeight="True"
            Resizable="false"
            Shadow="None"
            Modal="true"
            X="10" Y="30">
            <Items>
                <ext:FormPanel ID="EditarAportacionFormP" runat="server" Title="Form Panel" Header="false" ButtonAlign="Right" MonitorValid="true" LabelWidth="200">
                    <Items>
                        <ext:Panel ID="Panel12" runat="server" Title="Aportaciones" Layout="AnchorLayout" AutoHeight="True"
                            Resizable="false">
                            <Items>
                                <ext:Panel ID="Panel13" runat="server" Frame="false" Padding="5" Layout="FormLayout" Border="false" AnchorHorizontal="100%" >
                                    <Items>
                                        <ext:TextField runat="server" ID="EditSociosIdTxt"             DataIndex="SOCIOS_ID"          LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Id de Socio" AllowBlank="false" ReadOnly="true">
                                            <ToolTips>
                                                <ext:ToolTip ID="ToolTip1" runat="server" Title="Id de Socio" Html="El Id de Socio es de solo lectura." Width="200" TrackMouse="true" />
                                            </ToolTips>
                                        </ext:TextField>
                                        <ext:TextField   runat="server" ID="EditNombreTxt" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Nombre del Socio" ReadOnly="true" >
                                            <ToolTips>
                                                <ext:ToolTip ID="ToolTip2" runat="server" Html="El nombre de socio es de solo lectura." Title="Nombre del Socio" Width="200" TrackMouse="true" />
                                            </ToolTips>
                                        </ext:TextField>

                                        <ext:FieldSet runat="server" ID="EditAportacionesFS" Title="Saldos" Padding="5" LabelWidth="200" >
                                            <Items>
                                                <ext:NumberField runat="server" ID="EditAportacionOrdinariaSaldoTxt"  DataIndex="APORTACIONES_ORDINARIA_SALDO" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Saldo Aportación Ordinaria" AllowBlank="false" ReadOnly="true" MsgTarget="Side" >
                                                    <ToolTips>
                                                        <ext:ToolTip ID="ToolTip5" runat="server" Title="Saldo Aportación Ordinaria" Html="El saldo aportación ordinaria es de solo lectura." Width="200" TrackMouse="true" />
                                                    </ToolTips>
                                                </ext:NumberField>
                                                <ext:NumberField runat="server" ID="EditAportacionExtraordinariaSaldoTxt"  DataIndex="APORTACIONES_EXTRAORDINARIA_SALDO" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Saldo Aportación Extraordinaria" AllowBlank="false" ReadOnly="true" MsgTarget="Side" >
                                                    <ToolTips>
                                                        <ext:ToolTip ID="ToolTip6" runat="server" Title="Saldo Aportación Extraordinaria" Html="El saldo aportación extraordinaria es de solo lectura." Width="200" TrackMouse="true" />
                                                    </ToolTips>
                                                </ext:NumberField>
                                                <ext:NumberField runat="server" ID="EditAportacionCapRetencionSaldoTxt"  DataIndex="APORTACIONES_CAPITALIZACION_RETENCION_SALDO" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Saldo Capitalización por Retención" AllowBlank="false" ReadOnly="true" MsgTarget="Side" >
                                                    <ToolTips>
                                                        <ext:ToolTip ID="ToolTip7" runat="server" Title="Saldo Capitalización por Retención" Html="El saldo capitalización por retención es de solo lectura." Width="200" TrackMouse="true" />
                                                    </ToolTips>
                                                </ext:NumberField>
                                                <ext:NumberField runat="server" ID="EditAportacionInteresesSAportacionesSaldoTxt"  DataIndex="APORTACIONES_INTERESES_S_APORTACION_SALDO" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Saldo Intereses sobre Aportaciones" AllowBlank="false" ReadOnly="true" MsgTarget="Side" >
                                                    <ToolTips>
                                                        <ext:ToolTip ID="ToolTip8" runat="server" Title="Saldo Intereses sobre Aportaciones" Html="El saldo intereses sobre aportaciones es de solo lectura." Width="200" TrackMouse="true" />
                                                    </ToolTips>
                                                </ext:NumberField>
                                                <ext:NumberField runat="server" ID="EditAportacionExcedentePeriodoSaldoTxt"  DataIndex="APORTACIONES_EXCEDENTE_PERIODO_SALDO" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Saldo Excedente de Período" AllowBlank="false" ReadOnly="true" MsgTarget="Side" >
                                                    <ToolTips>
                                                        <ext:ToolTip ID="ToolTip9" runat="server" Title="Saldo Excedente de Período" Html="El saldo excedente de período es de solo lectura." Width="200" TrackMouse="true" />
                                                    </ToolTips>
                                                </ext:NumberField>

                                                <ext:NumberField runat="server" ID="EditAportacionTotalSaldoTxt"  DataIndex="APORTACIONES_SALDO_TOTAL" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Saldo Total" AllowBlank="false" ReadOnly="true" MsgTarget="Side" >
                                                    <ToolTips>
                                                        <ext:ToolTip ID="ToolTip3" runat="server" Title="Saldo Total" Html="El saldo es de solo lectura." Width="200" TrackMouse="true" />
                                                    </ToolTips>
                                                </ext:NumberField>
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:TextField runat="server"   ID="EditCreatedByTxt"     DataIndex="CREADO_POR"          LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Creado_por" Hidden="true" ></ext:TextField>
                                        <ext:TextField runat="server"   ID="EditCreationDateTxt"  DataIndex="FECHA_CREACION"      LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Fecha de Creacion" Hidden="true" ></ext:TextField>
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
                    </Buttons>
                </ext:FormPanel>
            </Items>
        </ext:Window>
    </div>
    </form>
</body>
</html>