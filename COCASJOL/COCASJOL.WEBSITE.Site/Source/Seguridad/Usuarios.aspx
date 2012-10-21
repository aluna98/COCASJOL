<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Usuarios.aspx.cs" Inherits="Source_Seguridad_Usuarios" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

    <div>
        <ext:ResourceManager ID="ResourceManager1" runat="server">
        </ext:ResourceManager>

        <asp:ObjectDataSource ID="UsuariosDS" runat="server"
            TypeName="COCASJOL.LOGIC.UsuarioLogic"
            SelectMethod="GetDatos">
        </asp:ObjectDataSource>

        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:Panel ID="Panel1" runat="server" Frame="false" Header="false" Title="Usuarios"
                    Icon="Group" Layout="Fit">
                    <Items>
                        <ext:GridPanel ID="UsuariosGridP" runat="server" AutoExpandColumn="USR_NOMBRE" Height="300"
                            Title="Usuarios" Header="false" Border="false" StripeRows="true" TrackMouseOver="true">
                            <Store>
                                <ext:Store ID="UsuariosSt" runat="server" DataSourceID="UsuariosDS">
                                    <Reader>
                                        <ext:JsonReader IDProperty="username">
                                            <Fields>
                                                <ext:RecordField Name="USR_USERNAME" />
                                                <ext:RecordField Name="USR_NOMBRE" />
                                                <ext:RecordField Name="USR_APELLIDO" />
                                                <ext:RecordField Name="USR_CEDULA" />
                                                <ext:RecordField Name="USR_CORREO" />
                                                <ext:RecordField Name="USR_PUESTO" />
                                                <ext:RecordField Name="USR_PASSWORD" />
                                                <ext:RecordField Name="CREADO_POR" />
                                                <ext:RecordField Name="FECHA_CREACION" />
                                                <ext:RecordField Name="MODIFICADO_POR" />
                                                <ext:RecordField Name="FECHA_MODIFICACION" />
                                            </Fields>
                                        </ext:JsonReader>
                                    </Reader>
                                </ext:Store>
                            </Store>
                            <ColumnModel>
                                <Columns>
                                    <ext:Column DataIndex="USR_USERNAME" Header="Usuario" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="USR_NOMBRE" Header="Nombre" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="USR_APELLIDO" Header="Apellido" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="USR_CEDULA" Header="Cedula" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="USR_CORREO" Header="Email" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="USR_PUESTO" Header="Puesto" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="USR_PASSWORD" Header="Password" Sortable="true"></ext:Column>
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" SingleSelect="true" />
                            </SelectionModel>
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar1" runat="server" PageSize="20" StoreID="UsuariosSt" />
                            </BottomBar>
                            <LoadMask ShowMask="true" />
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
      
    </div>
    
    </form>
</body>
</html>
