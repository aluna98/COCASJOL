/// <reference name="Ext.Net.Build.Ext.Net.extjs.adapter.ext.ext-base-debug.js" assembly="Ext.Net" />
/// <reference name="Ext.Net.Build.Ext.Net.extjs.ext-all-debug-w-comments.js" assembly="Ext.Net" />

var DesktopX = {
    alignPanels: function () {
        pnlSample.getEl().alignTo(Ext.getBody(), "tr", [-455, 5], false)
    },

    createDynamicWindow: function (app, ico, title, url, width, height) {
        var hostName = window.location.protocol + "//" + window.location.host;

        width = width == null ? 640 : width;
        height = height == null ? 480 : height;

        var maximizeWindow = maximizarVentanasHdn.getValue();

        var desk = app.getDesktop();

        var w = desk.getWindow(title + '-win');
        if (!w) {
            var windowId = title + '-win';
            w = desk.createWindow({
                id: windowId,
                iconCls: 'icon-' + ico,
                title: title,
                width: width,
                height: height,
                maximizable: true,
                minimizable: true,
                maximized: maximizeWindow,
                closeAction: 'close',
                shadow: false,
                shim: false,
                animCollapse: false,
                constrainHeader: true,
                layout: 'fit',
                autoLoad: {
                    url: url,
                    mode: "iframe",
                    showMask: true
                },
                tools: [{
                    id: 'search',
                    qtip: 'Abrir en Ventana Propia',
                    handler: function (event, toolEl, panel) {
                        window.open(url, "_blank");
                    }
                }]
            });
        }
        w.show();
    },

    cascadeWindows: function (app) {
        app.getDesktop().cascade();
    },

    tileWindows: function (app) {
        app.getDesktop().tile();
    },

    checkerboardWindows: function (app) {
        var desk = app.getDesktop();

        var availWidth = desk.getWinWidth();
        var availHeight = desk.getWinHeight();

        var x = 0, y = 0;
        var lastx = 0, lasty = 0;

        var square = 400;

        desk.getManager().each(function (win) {
            if (win.isVisible()) {
                win.setWidth(square - 10);
                win.setHeight(square - 10);

                win.setPosition(x, y);
                x += square;

                if (x + square > availWidth) {
                    x = lastx;
                    y += square;

                    if (y > availHeight) {
                        lastx += 20;
                        lasty += 20;
                        x = lastx;
                        y = lasty;
                    }
                }
            }
        }, this);
    },

    tileFitWindows: function (app, horizontal) {
        var desk = app.getDesktop();
        var availWidth = desk.getWinWidth();
        var availHeight = desk.getWinHeight();

        var x = 0, y = 0;

        var snapCount = 0;

        desk.getManager().each(function (win) {
            if (win.isVisible()) {
                snapCount++;
            }
        }, this);

        var snapSize = parseInt(availWidth / snapCount);

        if (!horizontal)
            snapSize = parseInt(availHeight / snapCount);

        if (snapSize > 0) {
            desk.getManager().each(function (win) {
                if (win.isVisible()) {
                    if (horizontal) {
                        win.setWidth(snapSize);
                        win.setHeight(availHeight);
                    } else {
                        win.setWidth(availWidth);
                        win.setHeight(snapSize);
                    }

                    win.setPosition(x, y);

                    if (horizontal)
                        x += snapSize;
                    else
                        y += snapSize;
                }
            }, this);
        }
    },

    closeAllWindows: function (app) {
        Ext.Msg.confirm('Cerrar Ventanas', 'Todo el trabajo sin guardar en cada una de las ventanas se perdera. Seguro desea cerrar todas las ventanas?', function (btn, text) {
            if (btn == 'yes') {
                var desk = app.getDesktop();
                desk.getManager().each(function (win) {
                    var w = desk.getWindow(win.title + '-win');
                    if (w)
                        w.close();
                });
            }
        });
    },

    minimizeAllWindows: function (app) {
        var desk = app.getDesktop();
        desk.getManager().each(function (win) {
            var w = desk.getWindow(win.title + '-win');
            if (w && w.isVisible())
                win.minimize();
        });
    },

    showAllWindows: function (app) {
        var desk = app.getDesktop();
        desk.getManager().each(function (win) {
            var w = desk.getWindow(win.title + '-win');
            if (w && w.minimized)
                win.show();
        });
    },

    initialCheckForNotification: function () {
        Ext.net.DirectMethods.InitialCheckForNotifications();
    },

    markAsReadNotification: function (notificacion_id) {
        Ext.net.DirectMethods.MarkAsReadNotification(notificacion_id);
    },

    deleteReadNotifications: function () {
        if (dsReport.getCount() == 0)
            return;
        Ext.Msg.confirm('Eliminar Notificaciones Leidas', 'Seguro desea eliminar las notificaciones leidas?', function (btn, text) {
            if (btn == 'yes') {
                Ext.net.DirectMethods.DeleteReadNotifications();
            }
        });
    }
};

var WindowX = {
    usuarios: function (app) {
        DesktopX.createDynamicWindow(app, 'user', 'Usuarios', 'Seguridad/Usuarios.aspx');
    },

    roles: function (app) {
        DesktopX.createDynamicWindow(app, 'cog', 'Roles', 'Seguridad/Roles.aspx');
    },

    configuracionDeSistema: function (app) {
        DesktopX.createDynamicWindow(app, 'serveredit', 'Configuración de Sistema', 'Configuracion/ConfiguracionDeSistema.aspx', 600, 400);
    },

    plantillasNotificaciones: function (app) {
        DesktopX.createDynamicWindow(app, 'plantillasNotificaciones16', 'Plantillas de Notificaciones', 'Utiles/PlantillasDeNotificaciones.aspx', 1000, 600);
    },

    socios: function (app) {
        DesktopX.createDynamicWindow(app, 'group', 'Socios', 'Socios/Socios.aspx', 800, 600);
    },

    estadosNotasDePeso: function (app) {
        DesktopX.createDynamicWindow(app, 'pagego', 'Estados de Notas de Peso', 'Inventario/Ingresos/EstadosNotaDePeso.aspx');
    },
    
    notasDePeso: function (app) {
        DesktopX.createDynamicWindow(app, 'pagewhiteoffice', 'Notas de Peso', 'Inventario/Ingresos/NotasDePeso.aspx', 1000, 600);
    },

    inventarioDeCafePorSocio: function (app) {
        DesktopX.createDynamicWindow(app, 'brick', 'Inventario de Café por Socio', 'Inventario/InventarioDeCafePorSocio.aspx');
    },

    inventarioDeCafe: function (app) {
        DesktopX.createDynamicWindow(app, 'bricks', 'Inventario de Café', 'Inventario/InventarioDeCafe.aspx');
    },

    hojasDeLiquidacion: function (app) {
        DesktopX.createDynamicWindow(app, 'script', 'Hojas de Liquidación', 'Inventario/Salidas/HojasDeLiquidacion.aspx', 1000, 600);
    },

    ajustesDeInventarioDeCafeDeSocios: function (app) {
        DesktopX.createDynamicWindow(app, 'ajustesDeInventarioDeCafeDeSocios16', 'Ajustes de Inventario de Café de Socios', 'Inventario/Salidas/AjustesInventarioDeCafeDeSocios.aspx');
    },

    ventasDeInventarioDeCafe: function (app) {
        DesktopX.createDynamicWindow(app, 'cartfull', 'Ventas de Inventario de Café', 'Inventario/Salidas/VentasInventarioDeCafe.aspx');
    },

    reporteHojasDeLiquidacion: function (app) {
        DesktopX.createDynamicWindow(app, 'reporteHojasDeLiquidacion16', 'Reporte de Hojas de Liquidación', 'Reportes/ReporteHojasDeLiquidacion.aspx', 600, 300);
    },

    reporteDetalleDeNotasDePeso: function (app) {
        DesktopX.createDynamicWindow(app, 'reporteDetalleNotasDePeso16', 'Reporte Detalle de Notas de Peso', 'Reportes/ReporteDetalleDeNotasDePeso.aspx', 600, 400);
    },

    reporteMovimientosInventarioDeCafeDeSocios: function (app) {
        DesktopX.createDynamicWindow(app, 'reporteMovimientosInventarioDeCafeDeSocios16', 'Reporte de Movimientos de Inventario de Café de Socios', 'Reportes/ReporteMovimientosInventarioDeCafeDeSocios.aspx', 600, 400);
    },

    reporteMovimientosInventarioDeCafeDeCooperativa: function (app) {
        DesktopX.createDynamicWindow(app, 'reporteMovimientosInventarioDeCafeDeCooperativa16', 'Reporte de Movimientos de Inventario de Café de Cooperativa', 'Reportes/ReporteMovimientosInventarioDeCafeDeCooperativa.aspx', 600, 400);
    },

    aportacionesPorSocio: function (app) {
        DesktopX.createDynamicWindow(app, 'aportacionesPorSocio16', 'Aportaciones por Socio', 'Aportaciones/AportacionesPorSocio.aspx', 1000, 600);
    },

    retiroDeAportaciones: function (app) {
        DesktopX.createDynamicWindow(app, 'retiroAportaciones16', 'Retiro de Aportaciones', 'Aportaciones/RetiroDeAportaciones.aspx', 1000, 600);
    },

    reporteDetalleDeAportacionesPorSocio: function (app) {
        DesktopX.createDynamicWindow(app, 'reporteDetalleAportacionesPorSocio16', 'Reporte Detalle de Aportaciones por Socio', 'Reportes/ReporteDetalleDeAportacionesPorSocio.aspx', 600, 300);
    },

    prestamos: function (app) {
        DesktopX.createDynamicWindow(app, 'prestamos16', 'Tipos de Prestamo', 'Prestamos/Prestamos.aspx');
    },

    solicitudesDePrestamo: function (app) {
        DesktopX.createDynamicWindow(app, 'pagewhitetext', 'Solicitudes de Prestamo', 'Prestamos/SolicitudesDePrestamos.aspx', 1000, 600);
    },

    prestamosAprobados: function (app) {
        DesktopX.createDynamicWindow(app, 'prestamosAprobados16', 'Solicitudes de Prestamo Aprobado', 'Prestamos/Aprobados.aspx', 1000, 600);
    },

    reportePrestamosPorSocios: function (app) {
        DesktopX.createDynamicWindow(app, 'reportePrestamosPorSocios16', 'Reporte de Prestamos por Socios', 'Reportes/ReportePrestamosPorSocios.aspx', 600, 300);
    },

    clasificacionesDeCafe: function (app) {
        DesktopX.createDynamicWindow(app, 'cup', 'Clasificaciones de Café', 'Inventario/ClasificacionesDeCafe.aspx');
    },

    variablesDeEntorno: function (app) {
        DesktopX.createDynamicWindow(app, 'database', 'Variables de Entorno', 'Entorno/VariablesDeEntorno.aspx');
    },

    applicationLog: function (app) {
        DesktopX.createDynamicWindow(app, 'clipboard', 'Bitácora de Aplicación', 'Logger/ApplicationLog.aspx', 1000, 600);
    },

    settings: function () {
        SettingsWin.show();
    },

    about: function () {
        AboutWin.show();
    }
};

var ShorcutClickHandler = function (app, id) {
    var d = app.getDesktop();

    if (id == 'scUsuarios') {
        WindowX.usuarios(app);
    } else if (id == 'scRoles') {
        WindowX.roles(app);
    } else if (id == 'scConfiguracionDeSistema') {
        WindowX.configuracionDeSistema(app);
    } else if (id == 'scPlantillasNotificaciones') {
        WindowX.plantillasNotificaciones(app);
    } else if (id == 'scSocios') {
        WindowX.socios(app);
    } else if (id == 'scEstadosNotasDePeso') {
        WindowX.estadosNotasDePeso(app);
    } else if (id == 'scNotasDePeso') {
        WindowX.notasDePeso(app);
    } else if (id == 'scInventarioDeCafePorSocio') {
        WindowX.inventarioDeCafePorSocio(app);
    } else if (id == 'scInventarioDeCafe') {
        WindowX.inventarioDeCafe(app);
    } else if (id == 'scHojasDeLiquidacion') {
        WindowX.hojasDeLiquidacion(app);
    } else if (id == 'scReporteDeHojasDeLiquidacion') {
        WindowX.reporteHojasDeLiquidacion(app);
    } else if (id == 'scReporteDetalleDeNotasDePeso') {
        WindowX.reporteDetalleDeNotasDePeso(app);
    } else if (id == 'scAjustesDeInventarioDeCafeDeSocios') {
        WindowX.ajustesDeInventarioDeCafeDeSocios(app);
    } else if (id == 'scVentasDeInventarioDeCafe') {
        WindowX.ventasDeInventarioDeCafe(app);
    } else if (id == 'scReporteMovimientosInventarioDeCafeDeSocios') {
        WindowX.reporteMovimientosInventarioDeCafeDeSocios(app);
    } else if (id == 'scReporteMovimientosInventarioDeCafeDeCooperativa') {
        WindowX.reporteMovimientosInventarioDeCafeDeCooperativa(app);
    } else if (id == 'scAportacionesPorSocio') {
        WindowX.aportacionesPorSocio(app);
    } else if (id == 'scRetiroDeAportaciones') {
        WindowX.retiroDeAportaciones(app);
    } else if (id == 'scReporteDetalleDeAportacionesPorSocio') {
        WindowX.reporteDetalleDeAportacionesPorSocio(app);
    } else if (id == 'scPrestamos') {
        WindowX.prestamos(app);
    } else if (id == 'scSolicitudesDePrestamo') {
        WindowX.solicitudesDePrestamo(app);
    } else if (id == 'scPrestamosAprobados') {
        WindowX.prestamosAprobados(app);
    } else if (id == 'scReportePrestamosPorSocios') {
        WindowX.reportePrestamosPorSocios(app);
    } else if (id == 'scClasificacionesDeCafe') {
        WindowX.clasificacionesDeCafe(app);
    } else if (id == 'scVariablesDeEntorno') {
        WindowX.variablesDeEntorno(app);
    } else if (id == 'scApplicationLog') {
        WindowX.applicationLog(app);
    }
};