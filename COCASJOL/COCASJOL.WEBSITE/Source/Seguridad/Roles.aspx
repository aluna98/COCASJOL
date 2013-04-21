<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="COCASJOL.WEBSITE.Source.Seguridad.Roles" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="~/Source/Auditoria/Auditoria.ascx" TagPrefix="aud" TagName="Auditoria" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Roles</title>
    <script type="text/javascript" src="../../resources/js/seguridad/roles.js" ></script>
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

        <asp:ObjectDataSource ID="RolDS" runat="server"
                TypeName="COCASJOL.LOGIC.Seguridad.RolLogic"
                SelectMethod="GetRoles"
                InsertMethod="InsertarRol"
                UpdateMethod="ActualizarRol"
                DeleteMethod="EliminarRol" onselecting="RolDS_Selecting" >
                <SelectParameters>
                    <asp:ControlParameter Name="ROL_ID"             Type="Int32"    ControlID="f_ROL_ID"          PropertyName="Text" />
                    <asp:ControlParameter Name="ROL_NOMBRE"         Type="String"   ControlID="f_ROL_NOMBRE"      PropertyName="Text" />
                    <asp:ControlParameter Name="ROL_DESCRIPCION"    Type="String"   ControlID="f_ROL_DESCRIPCION" PropertyName="Text" />
                    <asp:ControlParameter Name="CREADO_POR"         Type="String"   ControlID="nullHdn"           PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="FECHA_CREACION"     Type="DateTime" ControlID="nullHdn"           PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="MODIFICADO_POR"     Type="String"   ControlID="nullHdn"           PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="FECHA_MODIFICACION" Type="DateTime" ControlID="nullHdn"           PropertyName="Text" DefaultValue="" />
                </SelectParameters>
                <InsertParameters>
                    <asp:Parameter Name="ROL_ID"                Type="Int32" />
                    <asp:Parameter Name="ROL_NOMBRE"            Type="String" />
                    <asp:Parameter Name="ROL_DESCRIPCION"       Type="String" />
                    <asp:Parameter Name="CREADO_POR"            Type="String" />
                    <asp:Parameter Name="FECHA_CREACION"        Type="DateTime" />
                    <asp:Parameter Name="MODIFICADO_POR"        Type="String" />
                    <asp:Parameter Name="FECHA_MODIFICACION"    Type="DateTime" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="ROL_ID"                Type="Int32" />
                    <asp:Parameter Name="ROL_NOMBRE"            Type="String" />
                    <asp:Parameter Name="ROL_DESCRIPCION"       Type="String" />
                    <asp:Parameter Name="CREADO_POR"            Type="String" />
                    <asp:Parameter Name="FECHA_CREACION"        Type="DateTime" />
                    <asp:Parameter Name="MODIFICADO_POR"        Type="String" />
                    <asp:Parameter Name="FECHA_MODIFICACION"    Type="DateTime" />
                </UpdateParameters>
                <DeleteParameters>
                    <asp:Parameter Name="ROL_ID" Type="Int32" />
                </DeleteParameters>
        </asp:ObjectDataSource>
        
        <ext:Hidden ID="nullHdn" runat="server" >
        </ext:Hidden>

        <ext:Hidden ID="LoggedUserHdn" runat="server" >
        </ext:Hidden>

        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:Panel ID="Panel1" runat="server" Frame="false" Header="false" Title="Roles" Icon="GroupGear" Layout="Fit">
                    <Items>
                        <ext:GridPanel ID="RolesGridP" runat="server" AutoExpandColumn="ROL_DESCRIPCION" Height="300"
                            Title="Roles" Header="false" Border="false" StripeRows="true" TrackMouseOver="true">
                            <KeyMap>
                                <ext:KeyBinding Ctrl="true" >
                                    <Keys>
                                        <ext:Key Code="INSERT" />
                                        <ext:Key Code="ENTER" />
                                        <ext:Key Code="DELETE" />
                                    </Keys>
                                    <Listeners>
                                        <Event Handler="PageX.gridKeyUpEvent(this, e);" />
                                    </Listeners>
                                </ext:KeyBinding>
                            </KeyMap>
                            <Store>
                                <ext:Store ID="RolesSt" runat="server" DataSourceID="RolDS" AutoSave="true" SkipIdForNewRecords="false" >
                                    <Reader>
                                        <ext:JsonReader IDProperty="ROL_ID">
                                            <Fields>
                                                <ext:RecordField Name="ROL_ID"              />
                                                <ext:RecordField Name="ROL_NOMBRE"          />
                                                <ext:RecordField Name="ROL_DESCRIPCION"     />
                                                <ext:RecordField Name="CREADO_POR"          />
                                                <ext:RecordField Name="FECHA_CREACION"      Type="Date" />
                                                <ext:RecordField Name="MODIFICADO_POR"      />
                                                <ext:RecordField Name="FECHA_MODIFICACION"  Type="Date" />
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
                                    <ext:Column DataIndex="ROL_ID"          Header="Id de Rol" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="ROL_NOMBRE"      Header="Nombre" Sortable="true" Width="150"></ext:Column>
                                    <ext:Column DataIndex="ROL_DESCRIPCION" Header="Descripción" Sortable="true"></ext:Column>

                                    <ext:Column DataIndex="ROL_ID" Width="28" Sortable="false" MenuDisabled="true" Header="&nbsp;" Fixed="true">
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
                                        <ext:Button ID="AgregarRolBtn" runat="server" Text="Agregar" Icon="CogAdd" >
                                            <Listeners>
                                                <Click Handler="PageX.add();" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button ID="EditarRolBtn" runat="server" Text="Editar" Icon="CogEdit">
                                            <Listeners>
                                                <Click Handler="PageX.edit();" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button ID="EliminarRolBtn" runat="server" Text="Eliminar" Icon="CogDelete">
                                            <Listeners>
                                                <Click Handler="PageX.remove();" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:ToolbarFill ID="ToolbarFill1" runat="server" />
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
                                                        <ext:NumberField ID="f_ROL_ID" runat="server" EnableKeyEvents="true" Icon="Find">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:NumberField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="f_ROL_NOMBRE" runat="server" EnableKeyEvents="true" Icon="Find" MaxLength="45">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="f_ROL_DESCRIPCION" runat="server" EnableKeyEvents="true" Icon="Find" MaxLength="100">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:TextField>
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
                                <ext:PagingToolbar ID="PagingToolbar1" runat="server" PageSize="20" StoreID="RolesSt" />
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

        <ext:Window ID="AgregarRolWin"
            runat="server"
            Hidden="true"
            Icon="CogAdd"
            Title="Agregar Rol"
            Width="500"
            Layout="FormLayout"
            AutoHeight="True"
            Resizable="false"
            Shadow="None"
            Modal="true"
            X="10" Y="30">
            <Items>
                <ext:FormPanel ID="AddRolFormP" runat="server" Title="Form Panel" Header="false" ButtonAlign="Right" MonitorValid="true" LabelWidth="120">
                    <Listeners>
                        <Show Handler="this.getForm().reset();" />
                    </Listeners>
                    <Items>
                        <ext:TabPanel ID="TabPanel1" runat="server">
                            <Items>
                                <ext:Panel ID="Panel2" runat="server" Title="Información" Layout="AnchorLayout" AutoHeight="True"
                                    Resizable="false">
                                    <Items>
                                        <ext:Panel ID="Panel3" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="true">
                                            <Items>
                                                <ext:NumberField runat="server" ID="AddIdTxt"               DataIndex="ROL_ID"             LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Id de Rol" AllowBlank="false" Text="0" Hidden="true" ReadOnly="true"></ext:NumberField>
                                                <ext:TextField   runat="server" ID="AddNombreTxt"           DataIndex="ROL_NOMBRE"         LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Nombre" AllowBlank="false" MsgTarget="Side" MaxLength="45"></ext:TextField>
                                                <ext:TextArea   runat="server" ID="AddDescripcionTxt"      DataIndex="ROL_DESCRIPCION"    LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Descripción" MaxLength="100" Height="50" ></ext:TextArea>
                                                <ext:TextField   runat="server" ID="AddCreatedByTxt"        DataIndex="CREADO_POR"         LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Creado por" Hidden="true" ></ext:TextField>
                                                <ext:TextField   runat="server" ID="AddCreatedDateTxt"      DataIndex="FECHA_CREACION"     LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Fecha de Creacion" Hidden="true" ></ext:TextField>
                                                <ext:TextField   runat="server" ID="AddModifiedByTxt"       DataIndex="MODIFICADO_POR"     LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Modificado por" Hidden="true" ></ext:TextField>
                                                <ext:TextField   runat="server" ID="AddModificationDateTxt" DataIndex="FECHA_MODIFICACION" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Fecha de Modificacion" Hidden="true" ></ext:TextField>
                                            </Items>
                                        </ext:Panel>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:TabPanel>
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

        <ext:Window ID="EditarRolWin"
            runat="server"
            Hidden="true"
            Icon="CogEdit"
            Title="Editar Rol"
            Width="500"
            Layout="FormLayout"
            AutoHeight="True"
            Resizable="false"
            Shadow="None"
            Modal="true"
            X="10" Y="30">
            <Listeners>
                <Show Handler="#{PrivilegiosDeRolSt}.removeAll();" />
            </Listeners>
            <Items>
                <ext:FormPanel ID="EditarRolFormP" runat="server" Title="Form Panel" Header="false" ButtonAlign="Right" MonitorValid="true" LabelWidth="120">
                    <Listeners>
                        <Show Handler="this.getForm().reset();" />
                    </Listeners>
                    <Items>
                        <ext:TabPanel ID="TabPanel11" runat="server">
                            <Items>
                                <ext:Panel ID="Panel12" runat="server" Title="Información" Layout="AnchorLayout" AutoHeight="True"
                                    Resizable="false">
                                    <Listeners>
                                        <Activate Handler="PageX.ShowButtons();" />
                                    </Listeners>
                                    <Items>
                                        <ext:Panel ID="Panel13" runat="server" Frame="false" Padding="5" Layout="AnchorLayout" Border="false">
                                            <Items>
                                                <ext:NumberField runat="server" ID="EditIdTxt"            DataIndex="ROL_ID"             LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Id de Rol" AllowBlank="false" ReadOnly="true" Hidden="true"></ext:NumberField>
                                                <ext:TextField runat="server"   ID="EditNombreTxt"        DataIndex="ROL_NOMBRE"         LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Nombre" AllowBlank="false" MsgTarget="Side" MaxLength="45"></ext:TextField>
                                                <ext:TextArea runat="server"   ID="EditDescripcionTxt"   DataIndex="ROL_DESCRIPCION"    LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Descripción" MaxLength="100" Height="50" ></ext:TextArea>
                                                <ext:TextField runat="server"   ID="EditCreatedByTxt"     DataIndex="CREADO_POR"         LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Creado_por" Hidden="true" ></ext:TextField>
                                                <ext:TextField runat="server"   ID="EditCreationDateTxt"  DataIndex="FECHA_CREACION"     LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Fecha de Creacion" Hidden="true" ></ext:TextField>
                                                <ext:TextField runat="server"   ID="EditModifiedByTxt"    DataIndex="MODIFICADO_POR"     LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Modificado por" Hidden="true" ></ext:TextField>
                                                <ext:TextField runat="server"   ID="EditModificationDate" DataIndex="FECHA_MODIFICACION" LabelAlign="Right" AnchorHorizontal="90%" FieldLabel="Fecha de Modificacion" Hidden="true" ></ext:TextField>
                                            </Items>
                                        </ext:Panel>
                                    </Items>
                                </ext:Panel>
                                <ext:Panel ID="Panel14" runat="server" Title="Privilegios" Layout="AnchorLayout" AutoHeight="True"
                                    Resizable="false">
                                    <Listeners>
                                        <Activate Handler="PageX.HideButtons(); #{PrivilegiosDeRolSt}.reload();" />
                                        <Deactivate Handler="#{PrivilegiosDeRolSt}.removeAll();" />
                                    </Listeners>
                                    <Items>
                                        <ext:Panel ID="Panel16" runat="server" Frame="false" Padding="5" Layout="AnchorLayout"
                                            Border="false">
                                            <Items>
                                                <ext:GridPanel ID="PrivilegiosDeRolGridP" runat="server" AutoExpandColumn="PRIV_NOMBRE"
                                                    Height="250" Title="Privilegios de Rol" Header="false" Border="true" StripeRows="true"
                                                    TrackMouseOver="true" SelectionMemory="Disabled">
                                                    <Store>   
                                                        <ext:Store ID="PrivilegiosDeRolSt" runat="server" AutoSave="true" SkipIdForNewRecords="false" AutoLoad="false" OnRefreshData="PrivilegiosDeRolSt_Refresh" >
                                                            <Reader>
                                                                <ext:JsonReader IDProperty="PRIV_ID">
                                                                    <Fields>
                                                                        <ext:RecordField Name="PRIV_ID" />
                                                                        <ext:RecordField Name="PRIV_LLAVE" />
                                                                        <ext:RecordField Name="PRIV_NOMBRE" />
                                                                        <ext:RecordField Name="PRIV_DESCRIPCION" />
                                                                    </Fields>
                                                                </ext:JsonReader>
                                                            </Reader>
                                                            <BaseParams>
                                                                <ext:Parameter Name="PRIV_ID" Mode="Raw" Value="#{EditIdTxt}.getValue()"></ext:Parameter>
                                                            </BaseParams>
                                                        </ext:Store>
                                                    </Store>
                                                    <ColumnModel>
                                                        <Columns>
                                                            <ext:Column DataIndex="PRIV_ID" Header="Id" Width="40" Sortable="true"></ext:Column>
                                                            <ext:Column DataIndex="PRIV_LLAVE" Header="Llave" Width="230" Sortable="true"></ext:Column>
                                                            <ext:Column DataIndex="PRIV_NOMBRE" Header="Nombre" Width="270" Sortable="true"></ext:Column>

                                                            <ext:Column DataIndex="PRIV_ID" Width="28" Sortable="false" MenuDisabled="true" Header="&nbsp;" Fixed="true">
                                                                <Renderer Handler="return '';" />
                                                            </ext:Column>
                                                        </Columns>
                                                    </ColumnModel>
                                                    <View>
                                                        <ext:GridView ID="GridView2" runat="server" >
                                                            <HeaderRows>
                                                                <ext:HeaderRow>
                                                                    <Columns>
                                                                        <ext:HeaderColumn />
                                                                        <ext:HeaderColumn Cls="x-small-editor">
                                                                            <Component>
                                                                                <ext:NumberField ID="f_PRIV_ID" runat="server" EnableKeyEvents="true" Icon="Find">
                                                                                    <Listeners>
                                                                                        <KeyUp Handler="PageX.keyUpEvent2(this, e);" />
                                                                                    </Listeners>
                                                                                </ext:NumberField>
                                                                            </Component>
                                                                        </ext:HeaderColumn>
                                                                        <ext:HeaderColumn Cls="x-small-editor">
                                                                            <Component>
                                                                                <ext:TextField ID="f_PRIV_LLAVE" runat="server" EnableKeyEvents="true" Icon="Find" MaxLength="15">
                                                                                    <Listeners>
                                                                                        <KeyUp Handler="PageX.keyUpEvent2(this, e);" />
                                                                                    </Listeners>
                                                                                </ext:TextField>
                                                                            </Component>
                                                                        </ext:HeaderColumn>
                                                                        <ext:HeaderColumn Cls="x-small-editor">
                                                                            <Component>
                                                                                <ext:TextField ID="f_PRIV_NOMBRE" runat="server" EnableKeyEvents="true" Icon="Find" MaxLength="45">
                                                                                    <Listeners>
                                                                                        <KeyUp Handler="PageX.keyUpEvent2(this, e);" />
                                                                                    </Listeners>
                                                                                </ext:TextField>
                                                                            </Component>
                                                                        </ext:HeaderColumn>
                                                                                       
                                                                        <ext:HeaderColumn AutoWidthElement="false">
                                                                            <Component>
                                                                                <ext:Button ID="ClearFilterButtonPrivs" runat="server" Icon="Cancel">
                                                                                    <ToolTips>
                                                                                        <ext:ToolTip ID="ToolTip1" runat="server" Html="Clear filter" />
                                                                                    </ToolTips>
                                                                                    <Listeners>
                                                                                        <Click Handler="PageX.clearFilterPrivs();" />
                                                                                    </Listeners>                                            
                                                                                </ext:Button>
                                                                            </Component>
                                                                        </ext:HeaderColumn>
                                                                    </Columns>
                                                                </ext:HeaderRow>
                                                            </HeaderRows>
                                                        </ext:GridView>
                                                    </View>
                                                    <SelectionModel>
                                                        <ext:CheckboxSelectionModel ID="PrivilegiosDeRolSelectionM" runat="server">
                                                        </ext:CheckboxSelectionModel>
                                                    </SelectionModel>
                                                    <TopBar>
                                                        <ext:Toolbar ID="Toolbar2" runat="server">
                                                            <Items>
                                                                <ext:Button ID="EditRolAddPrivilegioBtn" runat="server" Text="Agregar" Icon="KeyAdd">
                                                                    <Listeners>
                                                                        <Click Handler="#{AgregarPrivilegiosWin}.show();" />
                                                                    </Listeners>
                                                                </ext:Button>
                                                                <ext:Button ID="EditRolDeletePrivilegioBtn" runat="server" Text="Eliminar" Icon="KeyDelete">
                                                                    <Listeners>
                                                                        <Click Handler="PageX.removePrivilege();" />
                                                                    </Listeners>
                                                                </ext:Button>
                                                            </Items>
                                                        </ext:Toolbar>
                                                    </TopBar>
                                                    <BottomBar>
                                                        <ext:PagingToolbar ID="PagingToolbar2" runat="server" PageSize="20" StoreID="PrivilegiosDeRolSt" />
                                                    </BottomBar>
                                                    <LoadMask ShowMask="true" />
                                                    <SaveMask ShowMask="true" />
                                                </ext:GridPanel>
                                            </Items>
                                        </ext:Panel>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:TabPanel>
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
                        <ext:Button ID="EditGuardarBtn" runat="server" Text="Guardar" Icon="Disk" FormBind="true">
                            <Listeners>
                                <Click Handler="#{EditModifiedByTxt}.setValue(#{LoggedUserHdn}.getValue()); PageX.update();" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            </Items>
        </ext:Window>

        <ext:ToolTip 
            ID="EditarPrivilegiosRowTipo" 
            runat="server" 
            Target="={#{PrivilegiosDeRolGridP}.getView().mainBody}"
            Delegate=".x-grid3-row"
            TrackMouse="true">
            <Listeners>
                <Show Handler="var rowIndex = #{PrivilegiosDeRolGridP}.view.findRowIndex(this.triggerElement); this.body.dom.innerHTML = #{PrivilegiosDeRolGridP}.getStore().getAt(rowIndex).get('PRIV_DESCRIPCION');" />
            </Listeners>
        </ext:ToolTip>

        <ext:Window ID="AgregarPrivilegiosWin" runat="server" Hidden="true" Icon="KeyAdd" Title="Agregar Privilegios"
            Width="500" Layout="FormLayout" AutoHeight="True" Resizable="false" Shadow="None"
            X="30" Y="70" Modal="true">
            <Listeners>
                <Show Handler="#{PrivilegiosNoDeRolesSt}.reload();" />
                <Hide Handler="#{PrivilegiosNoDeRolesSt}.removeAll();" />
            </Listeners>
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="Form Panel" Header="false" ButtonAlign="Right">
                    <Items>
                        <ext:Panel ID="Panel9" runat="server" Frame="false" Padding="5">
                            <Items>
                                <ext:GridPanel ID="PrivilegiosNoDeRolGridP" runat="server" AutoExpandColumn="PRIV_NOMBRE"
                                    Height="250" Title="Agregar Privilegios" Header="false" Border="true" StripeRows="true"
                                    TrackMouseOver="true" SelectionMemory="Enabled">
                                    <Store>
                                        <ext:Store ID="PrivilegiosNoDeRolesSt" runat="server" OnRefreshData="PrivilegiosNoDeRolesSt_Refresh"
                                            WarningOnDirty="false">
                                            <Reader>
                                                <ext:JsonReader IDProperty="PRIV_ID">
                                                    <Fields>
                                                        <ext:RecordField Name="PRIV_ID" />
                                                        <ext:RecordField Name="PRIV_NOMBRE" />
                                                        <ext:RecordField Name="PRIV_DESCRIPCION" />
                                                        <ext:RecordField Name="PRIV_LLAVE" />
                                                    </Fields>
                                                </ext:JsonReader>
                                            </Reader>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="ColumnModel2">
                                        <Columns>
                                            <ext:Column DataIndex="PRIV_ID" Header="Id" Width="40" Sortable="true"></ext:Column>
                                            <ext:Column DataIndex="PRIV_LLAVE" Header="Llave" Width="230" Sortable="true"></ext:Column>
                                            <ext:Column DataIndex="PRIV_NOMBRE" Header="Nombre" Width="270" Sortable="true"></ext:Column>

                                            <ext:Column DataIndex="PRIV_ID" Width="28" Sortable="false" MenuDisabled="true" Header="&nbsp;" Fixed="true">
                                                <Renderer Handler="return '';" />
                                            </ext:Column>
                                        </Columns>
                                    </ColumnModel>
                                    <View>
                                        <ext:GridView ID="GridView3" runat="server" >
                                            <HeaderRows>
                                                <ext:HeaderRow>
                                                    <Columns>
                                                        <ext:HeaderColumn />
                                                        <ext:HeaderColumn Cls="x-small-editor">
                                                            <Component>
                                                                <ext:NumberField ID="f2_PRIV_ID" runat="server" EnableKeyEvents="true" Icon="Find">
                                                                    <Listeners>
                                                                        <KeyUp Handler="PageX.keyUpEvent3(this, e);" />
                                                                    </Listeners>
                                                                </ext:NumberField>
                                                            </Component>
                                                        </ext:HeaderColumn>
                                                        <ext:HeaderColumn Cls="x-small-editor">
                                                            <Component>
                                                                <ext:TextField ID="f2_PRIV_LLAVE" runat="server" EnableKeyEvents="true" Icon="Find" MaxLength="15">
                                                                    <Listeners>
                                                                        <KeyUp Handler="PageX.keyUpEvent3(this, e);" />
                                                                    </Listeners>
                                                                </ext:TextField>
                                                            </Component>
                                                        </ext:HeaderColumn>
                                                        <ext:HeaderColumn Cls="x-small-editor">
                                                            <Component>
                                                                <ext:TextField ID="f2_PRIV_NOMBRE" runat="server" EnableKeyEvents="true" Icon="Find" MaxLength="45">
                                                                    <Listeners>
                                                                        <KeyUp Handler="PageX.keyUpEvent3(this, e);" />
                                                                    </Listeners>
                                                                </ext:TextField>
                                                            </Component>
                                                        </ext:HeaderColumn>
                                                                       
                                                        <ext:HeaderColumn AutoWidthElement="false">
                                                            <Component>
                                                                <ext:Button ID="ClearFilterButtonNotPrivs" runat="server" Icon="Cancel">
                                                                    <ToolTips>
                                                                        <ext:ToolTip ID="ToolTip2" runat="server" Html="Clear filter" />
                                                                    </ToolTips>
                                                                    <Listeners>
                                                                        <Click Handler="PageX.clearFilterNotPrivs();" />
                                                                    </Listeners>                                            
                                                                </ext:Button>
                                                            </Component>
                                                        </ext:HeaderColumn>
                                                    </Columns>
                                                </ext:HeaderRow>
                                            </HeaderRows>
                                        </ext:GridView>
                                    </View>
                                    <SelectionModel>
                                        <ext:CheckboxSelectionModel ID="PrivilegiosNoDeRolSelectionM" runat="server">
                                        </ext:CheckboxSelectionModel>
                                    </SelectionModel>
                                    <TopBar>
                                        <ext:Toolbar ID="Toolbar3" runat="server">
                                            <Items>
                                                <ext:Button ID="AddPrivilegiosAddPrivilegioBtn" runat="server" Text="Agregar" Icon="KeyAdd">
                                                    <Listeners>
                                                        <Click Handler="PageX.insertPrivilege();" />
                                                    </Listeners>
                                                </ext:Button>
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                    <BottomBar>
                                        <ext:PagingToolbar ID="PagingToolbar4" runat="server" PageSize="10" StoreID="PrivilegiosNoDeRolesSt" />
                                    </BottomBar>
                                    <LoadMask ShowMask="true" />
                                </ext:GridPanel>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:FormPanel>
            </Items>
        </ext:Window>

        <ext:ToolTip 
            ID="AgregarPrivilegiosRowTip" 
            runat="server" 
            Target="={#{PrivilegiosNoDeRolGridP}.getView().mainBody}"
            Delegate=".x-grid3-row"
            TrackMouse="true">
            <Listeners>
                <Show Handler="var rowIndex = #{PrivilegiosNoDeRolGridP}.view.findRowIndex(this.triggerElement); this.body.dom.innerHTML = #{PrivilegiosNoDeRolGridP}.getStore().getAt(rowIndex).get('PRIV_DESCRIPCION');" />
            </Listeners>
        </ext:ToolTip>
    </div>
    </form>
</body>
</html>
