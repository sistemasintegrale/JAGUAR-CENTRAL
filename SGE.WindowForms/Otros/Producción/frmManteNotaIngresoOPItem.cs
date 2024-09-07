using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using SGE.BusinessLogic;
using SGE.Common;
using SGE.Entity;
using SGE.WindowForms.Modules;
using SGE.WindowForms.Otros.Produccion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static SGE.Common.Enums;

namespace SGE.WindowForms.Otros.Producción
{
    public partial class frmManteNotaIngresoOPItem : XtraForm
    {
        public List<ENotaIngresoOPDetalle> listDetalle = new List<ENotaIngresoOPDetalle>();
        public ENotaIngresoOP objCab = new ENotaIngresoOP();
        List<BaseEdit> Tallas;
        List<BaseEdit> CantidadPediente;
        List<BaseEdit> CantidadOP;
        List<BaseEdit> Ingresos;
        List<BaseEdit> Saldos;

        public ENotaIngresoOPDetalle objDetalle = new ENotaIngresoOPDetalle();
        public frmManteNotaIngresoOPItem() => InitializeComponent();
        private void frmManteNotaIngresoOPItem_Load(object sender, EventArgs e) => Cargar();
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e) => Dispose();
        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e) => SetSave();
        private void txtCantidad_KeyUp(object sender, KeyEventArgs e) => SumCantidades();
        private void txtIngreso_KeyUp(object sender, KeyEventArgs e) => SumIngresos();
        private void Cargar()
        {
            EProdTablaRegistro ob = new EProdTablaRegistro();
            ob.iid_tipo_tabla = 11;
            BSControls.LoaderLook(LkpMarca, new BCentral().ListarProdTablaRegistro(ob), "descripcion", "icod_tabla", true);
            ob.iid_tipo_tabla = 8;
            BSControls.LoaderLook(LkpColor, new BCentral().ListarProdTablaRegistro(ob), "descripcion", "icod_tabla", true);
            Tallas = new List<BaseEdit> { txtTalla1, txtTalla2, txtTalla3, txtTalla4, txtTalla5, txtTalla6, txtTalla7, txtTalla8, txtTalla9, txtTalla10 };
            CantidadPediente = new List<BaseEdit> { txtCantidadPendiente1, txtCantidadPendiente2, txtCantidadPendiente3, txtCantidadPendiente4, txtCantidadPendiente5, txtCantidadPendiente6, txtCantidadPendiente7, txtCantidadPendiente8, txtCantidadPendiente9, txtCantidadPendiente10 };
            Ingresos = new List<BaseEdit> { txtIngreso1, txtIngreso2, txtIngreso3, txtIngreso4, txtIngreso5, txtIngreso6, txtIngreso7, txtIngreso8, txtIngreso9, txtIngreso10 };
            Saldos = new List<BaseEdit> { txtSaldo1, txtSaldo2, txtSaldo3, txtSaldo4, txtSaldo5, txtSaldo6, txtSaldo7, txtSaldo8, txtSaldo9, txtSaldo10 };
            CantidadOP = new List<BaseEdit> { txtCantidadPedida1, txtCantidadPedida2, txtCantidadPedida3, txtCantidadPedida4, txtCantidadPedida5, txtCantidadPedida6, txtCantidadPedida7, txtCantidadPedida8, txtCantidadPedida9, txtCantidadPedida10 };
            if (objDetalle.Operacion == Operacion.Ver)
                SetValues();
        }

        private void SetValues()
        {
            txtItem.Text = objDetalle.niopd_iitem_nota_ingreso_detalle.ToString("d2");
            btnItemOP.Tag = objDetalle.orped_icod_item_orden_pedido;
            btnItemOP.Text = objDetalle.NumeroItemOP;
            
            var OrdenPedidoDet = new BCentral().OrdenPedidoDetalleGetById(objDetalle.orped_icod_item_orden_pedido);
            var SaldosOPDetalle = new BCentral().OrdenPedidoDetSaldo(objDetalle.orped_icod_item_orden_pedido);
            for (int i = 0; i < 10; i++)
            {
                int index = i + 1;
                Tallas[i].Text = CommonFunctions.GetValueItem(OrdenPedidoDet, $"orped_talla{index}");
                CantidadPediente[i].Text = CommonFunctions.GetValueItem(SaldosOPDetalle, $"niopds_saldos_{index}");
                Ingresos[i].Text = CommonFunctions.GetValueItem(objDetalle, $"niopd_cant_{index}");
                CantidadOP[i].Text = CommonFunctions.GetValueItem(OrdenPedidoDet, $"orped_cant_talla{index}");
                if (objDetalle.niopd_icod_nota_ingreso_detalle != 0)
                {
                    int cantidad = Convert.ToInt32(CommonFunctions.GetValueItem(objDetalle, $"niopd_cant_{index}")) + Convert.ToInt32(CommonFunctions.GetValueItem(SaldosOPDetalle, $"niopds_saldos_{index}"));
                    CantidadPediente[i].Text = cantidad.ToString();
                }
                Ingresos[i].ReadOnly = CantidadPediente[i].Text == "0";
            }
            objDetalle.orped_imarca = OrdenPedidoDet.orped_imarca;
            objDetalle.orped_imodelo = OrdenPedidoDet.orped_imodelo;
            objDetalle.orped_icolor = OrdenPedidoDet.orped_icolor;
            btnItemOP.Enabled = false;
            LkpMarca.EditValue = OrdenPedidoDet.orped_imarca;
            btnmodelo.Tag = OrdenPedidoDet.orped_imodelo;
            btnmodelo.Text = OrdenPedidoDet.strmodelo;
            LkpColor.EditValue = OrdenPedidoDet.orped_icolor;
            SumCantidades();
            SumIngresos();
        }

        private void SetSave()
        {
            BaseEdit oBase = null;
            bool Flag = true;
            try
            {
                if (Convert.ToInt32(btnItemOP.Tag) == 0)
                {
                    oBase = btnItemOP;
                    throw new ArgumentException("Seleccione el Item de la OP");
                }
                if (Convert.ToInt32(txtTotalIngresado.Text) == 0)
                {
                    throw new ArgumentException("Ingrese una cantidad");
                }

                var saldoZero = Saldos.Select(x => Convert.ToInt32(x.Text)).ToList();
                if (saldoZero.Exists(x=>x<0))
                {
                    int index = saldoZero.FindIndex(x => x < 0);
                    oBase = Saldos[index];
                    throw new ArgumentException("No se puede ingresar una cantidad mayor a la cantidad especificada en el Item de la OP");
                }

                objDetalle.niopd_iitem_nota_ingreso_detalle = Convert.ToInt32(txtItem.Text);
                objDetalle.orped_icod_item_orden_pedido = Convert.ToInt32(btnItemOP.Tag);
                objDetalle.NumeroItemOP = btnItemOP.Text;
                objDetalle.Operacion = Operacion.Modificar;
                for (int i = 0; i < 10; i++)
                {
                    int index = i + 1;
                    CommonFunctions.SetValueItem(objDetalle, $"niopd_cant_{index}", Convert.ToInt32(Ingresos[i].Text));
                }
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Services.SetErrorInput(oBase, ex.Message);
                Flag = false;
            }
            finally
            {
                if (Flag)
                {
                    Close();
                }
            }
        }


        private void SumCantidades()
        {
            var lstNum = CantidadPediente.Select(x => Convert.ToInt32(x.Text));
            txtCantidadPedido.Text = lstNum.Sum(x => x).ToString();
        }
        private void SumIngresos()
        {
            var lstNum = Ingresos.Select(x => Convert.ToInt32(x.Text));
            txtTotalIngresado.Text = lstNum.Sum(x => x).ToString();
            for (int i = 0; i < 10; i++)
            {
                int saldo = Convert.ToInt32(CantidadPediente[i].Text) - Convert.ToInt32(Ingresos[i].Text);
                Saldos[i].Text = saldo.ToString() ;
            }
            SumSaldos();
        }

        private void SumSaldos()
        {
            var lstNum = Saldos.Select(x => Convert.ToInt32(x.Text));
            txtSaldo.Text = lstNum.Sum(x => x).ToString();
        }

        

        private void btnItemOP_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            using (ListarOrdenPedidoDetalle frm = new ListarOrdenPedidoDetalle())
            {
                frm.icod = objCab.orpec_iid_orden_pedido;
                frm.validarFichaTec = false;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (listDetalle.Exists(x => x.orped_icod_item_orden_pedido == frm._Be.orped_icod_item_orden_pedido))
                    {
                        Services.SetErrorInput(btnItemOP, "Ya tiene un Registro con el Mismo Item de la OP");
                        return;
                    }
                    btnItemOP.Tag = frm._Be.orped_icod_item_orden_pedido;
                    btnItemOP.Text = frm._Be.orped_iitem_orden_pedido.ToString();
                    objDetalle.orped_imarca = frm._Be.orped_imarca;
                    objDetalle.orped_imodelo = frm._Be.orped_imodelo;
                    objDetalle.orped_icolor = frm._Be.orped_icolor;
                    LkpMarca.EditValue = frm._Be.orped_imarca;
                    btnmodelo.Tag = frm._Be.orped_imodelo;
                    btnmodelo.Text = frm._Be.strmodelo;
                    LkpColor.EditValue = frm._Be.orped_icolor;

                    var SaldosOPDetalle = new BCentral().OrdenPedidoDetSaldo(frm._Be.orped_icod_item_orden_pedido);
                    for (int i = 0; i < 10; i++)
                    {
                        int index = i + 1;
                        Tallas[i].Text = CommonFunctions.GetValueItem(frm._Be, $"orped_talla{index}");
                        CantidadPediente[i].Text = CommonFunctions.GetValueItem(SaldosOPDetalle, $"niopds_saldos_{index}");
                        Ingresos[i].ReadOnly = CantidadPediente[i].Text == "0";
                        CantidadOP[i].Text = CommonFunctions.GetValueItem(frm._Be, $"orped_cant_talla{index}");

                    }
                    SumCantidades();
                    SumIngresos();
                }
            }
        }


    }
}