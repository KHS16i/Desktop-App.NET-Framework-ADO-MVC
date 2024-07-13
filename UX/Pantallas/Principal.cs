using BLL.AccesoDatos;
using DAL.Entidades;
using DAL.Entidades.Olas;
using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace UX_WinForms_.Pantallas
{
    public partial class Principal : Form
    {
        #region VARIABLES GLOBALES

        cls_EConsultaOla Obj_ConsultaOla = new cls_EConsultaOla();
        cls_Olas_DAL Obj_Ola_DAL = new cls_Olas_DAL();
        cls_Olas_BLL Obj_Ola_BLL = new cls_Olas_BLL(ConfigurationManager.ConnectionStrings["SQL_AUT"].ToString()); // <---Cadena de conexion por Inyeccion de Dependencias
        cls_Pedidos_DAL Obj_Pedido_DAL = new cls_Pedidos_DAL();
        cls_DetallePedido_DAL Obj_DetPedidos_DAL = new cls_DetallePedido_DAL();

        double cantidadSoli, cantidadRestante, cantidadCert;
        string idInterno;
        int numeroRowArticulo, indexPedidoSelect;

        private Timer _scannerSSCCtimer, _scannerIdInternoTimer;

        #endregion


        #region EVENTOS

        public Principal()
        {
            InitializeComponent();

            _scannerSSCCtimer = new Timer();
            _scannerSSCCtimer.Interval = 500; // 500 ms
            _scannerSSCCtimer.Tick += SSCC_Timer_Tick;

            _scannerIdInternoTimer = new Timer();
            _scannerIdInternoTimer.Interval = 500; // 500 ms
            _scannerIdInternoTimer.Tick += IdInterno_Timer_Tick;
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            // Deshabilitamos la generación automática de columnas
            dgv_Olas.AutoGenerateColumns = false;
            dgv_Pedidos.AutoGenerateColumns = false;
            dgv_Articulos.AutoGenerateColumns = false;

            dgv_Olas.Height = 300;

            cal_fechaInicio.MaxDate = DateTime.Now.Date;
            cal_fechaFinal.MaxDate = DateTime.Now.Date;

            txt_fechaInicio.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txt_fechaFinal.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

            label2.Text = cls_Login_DAL.sNombre;

            //Da foco al txt del SSCC
            txt_numOlaCertificar.Focus();

            //Se consultan las Olas colocando la fecha 01 del 01 de 1900 para que se entienda que se necesitan traer solo las Olas pendientes.
            Obj_ConsultaOla.sFechaInicio = new DateTime(1900, 01, 01);
            Obj_ConsultaOla.sFechaFinal = new DateTime(1900, 01, 01);
            Obj_ConsultaOla.sIdInterno = "";

            //Carga las Olas al DataGridView correspondiente
            cargarOlas();
        }

        private void btn_Buscar_Click(object sender, EventArgs e)
        {
            limpiar();

            Obj_ConsultaOla.sIdInterno = txt_numOlaCertificar.Text.Trim();

            cargarOlas();

            txt_numOlaCertificar.Text = string.Empty;
        }

        private void txt_numOlaCertificar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                limpiar();

                Obj_ConsultaOla.sIdInterno = txt_numOlaCertificar.Text.Trim();

                cargarOlas();

                txt_numOlaCertificar.Text = string.Empty;
            }
        }


        #region EVENTOS FECHA INICIO

        private void cal_fechaInicio_DateSelected(object sender, DateRangeEventArgs e)
        {
            cal_fechaInicio.Visible = false;

            Obj_ConsultaOla.sFechaInicio = cal_fechaInicio.SelectionStart;

            txt_fechaInicio.Text = cal_fechaInicio.SelectionStart.ToString("dd/MM/yyyy");

        }

        private void txt_fechaInicio_Click(object sender, EventArgs e)
        {
            cal_fechaInicio.Visible = true;
        }

        #endregion

        #region EVENTOS FECHA FINAL

        private void cal_fechaFinal_DateSelected(object sender, DateRangeEventArgs e)
        {
            cal_fechaFinal.Visible = false;

            Obj_ConsultaOla.sFechaFinal = cal_fechaFinal.SelectionStart;

            txt_fechaFinal.Text = cal_fechaFinal.SelectionStart.ToString("dd/MM/yyyy");
        }

        private void txt_fechaFinal_Click(object sender, EventArgs e)
        {
            cal_fechaFinal.Visible = true;
        }

        #endregion


        private void Principal_FormClosed(object sender, FormClosedEventArgs e)
        {
            limpiar();

            tbc_Principal.SelectedIndex = 0;

            txt_numOlaCertificar.Focus();
        }

        private void tbp_Olas_Click(object sender, EventArgs e)
        {
            cal_fechaInicio.Visible = false;
            cal_fechaFinal.Visible = false;
        }

        private void btn_Volver_Click(object sender, EventArgs e)
        {
            if (!((bool)dgv_Pedidos.Rows[indexPedidoSelect].Cells[11].Value))
            {
                //Hace un update a la tablas OPESALMaestroSolicitud(Ola) y OPESALPreMaestroSolicitud(Pedido) para indicar que ya están certificada
                ProcesarCertificacionOlaPedido();
            }

            dgv_Articulos.DataSource = null;

            cantidadSoli = 0;
            cantidadRestante = 0;
            cantidadCert = 0;

            tbc_Principal.SelectedIndex = 0;
        }

        private void dgv_Pedidos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Se valida que no se de doble click al header
            if (!(e.RowIndex == -1))
            {
                //Cambia a la pestaña donde se escanea
                tbc_Principal.SelectedIndex = 1;

                if (dgv_Pedidos.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow;
                    selectedRow = dgv_Pedidos.SelectedRows[0];
                    Obj_Pedido_DAL.iNumOla = Convert.ToInt32(selectedRow.Cells[0].Value);
                }

                //Se coloca el Nombre del Pedido en el label
                lbl_nombrePedido.Text = dgv_Pedidos.Rows[e.RowIndex].Cells[3].Value.ToString() + "      Destino: " + dgv_Pedidos.Rows[e.RowIndex].Cells[2].Value.ToString();

                //Carga los artículos del Pedido seleccionado en la anterior pestaña
                cargarDetallePedidos();

                if (dgv_Articulos.SelectedRows.Count > 0)
                {
                    dgv_Articulos.Rows[0].Selected = true;

                    cantidadSoli = Convert.ToInt32(dgv_Articulos.Rows[0].Cells[4].Value);
                    cantidadRestante = Convert.ToDouble(dgv_Articulos.Rows[0].Cells[3].Value);

                    Obj_DetPedidos_DAL.iIdArticulo = Convert.ToInt32(dgv_Articulos.Rows[0].Cells[2].Value);
                    Obj_DetPedidos_DAL.iIdLineaDetalleSolicitud = Convert.ToInt32(dgv_Articulos.Rows[0].Cells[7].Value);

                    idInterno = dgv_Articulos.Rows[0].Cells[1].Value.ToString();
                    numeroRowArticulo = dgv_Articulos.CurrentCell.RowIndex;
                }

                indexPedidoSelect = dgv_Pedidos.CurrentCell.RowIndex;

                txt_Escaner.Focus();
            }
        }

        private void dgv_Articulos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                dgv_Articulos.ClearSelection();
            }
            else
            {
                cantidadSoli = Convert.ToInt32(dgv_Articulos.Rows[e.RowIndex].Cells[4].Value);
                cantidadRestante = Convert.ToDouble(dgv_Articulos.Rows[e.RowIndex].Cells[3].Value);
                cantidadCert = Convert.ToInt32(dgv_Articulos.Rows[e.RowIndex].Cells[5].Value);

                Obj_DetPedidos_DAL.iIdArticulo = Convert.ToInt32(dgv_Articulos.Rows[e.RowIndex].Cells[2].Value);
                Obj_DetPedidos_DAL.iIdLineaDetalleSolicitud = Convert.ToInt32(dgv_Articulos.Rows[e.RowIndex].Cells[7].Value);

                idInterno = dgv_Articulos.Rows[e.RowIndex].Cells[1].Value.ToString();
                numeroRowArticulo = dgv_Articulos.CurrentCell.RowIndex;
            }

            txt_Escaner.Focus();
        }

        private void txt_SSCC_TextChanged(object sender, EventArgs e)
        {
            //Detiene el timer si es que está en ejecución
            _scannerSSCCtimer.Stop();
            //Inicia el timer e invoca su evento timer_tick
            _scannerSSCCtimer.Start();
        }

        private void txt_Escaner_TextChanged(object sender, EventArgs e)
        {
            //Detiene el timer si es que está en ejecución
            _scannerIdInternoTimer.Stop();
            //Inicia el timer e invoca su evento timer_tick
            _scannerIdInternoTimer.Start();
        }

        private void SSCC_Timer_Tick(object sender, EventArgs e)
        {
            _scannerSSCCtimer.Stop();

            string barCode = txt_SSCC.Text;

            if (!string.IsNullOrEmpty(barCode) && !(barCode.Equals("SSCC a Certificar")))
            {
                if (barCode.Length > 20)
                {
                    //Se elimina el resto del código escaneado, este lo autogenera el scanner
                    barCode = barCode.Substring(0, barCode.Length - 2);

                    //Validación por standart WMS
                    if (barCode.StartsWith("00"))
                    {
                        //Se otiene el SSCC al ser escaneado en el textox txt_SSCC y se le sustraen los 00
                        Obj_ConsultaOla.sSSCC = barCode.Substring(2);

                        cargarOlas();

                        if (dgv_Olas.RowCount != 0)
                        {
                            //Se compruea con la variable estática si el SSCC ha sido ubicado en la zona de Certificación
                            if (cls_Olas_DAL._bSsccUbicado)
                            {
                                //Obtiene el valor del Id del Maestro Solicitud de la Ola para consultar sus pedidos
                                Obj_Ola_DAL.iMaestroSolicitud = Convert.ToInt32(dgv_Olas.Rows[0].Cells[0].Value);

                                //Carga los pedidos de las Olas al DataGridView correspondiente
                                cargarPedidos();

                                //Se extrae el SSCC cuando nos aseguramos que está ubicado
                                Obj_DetPedidos_DAL.sSSCCC = Obj_ConsultaOla.sSSCC;

                                dgv_Olas.Height = 90;

                                label3.Visible = true;
                                dgv_Pedidos.Visible = true;
                            }
                            else
                            {
                                MessageBox.Show("SSCC no ubicado!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                limpiar();
                            }
                        }
                        else
                        {
                            MessageBox.Show("SSCC no encontrado!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            limpiar();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Etiqueta no válida!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                txt_SSCC.Text = string.Empty;
            }
        }

        private void IdInterno_Timer_Tick(object sender, EventArgs e)
        {
            _scannerIdInternoTimer.Stop();

            if (!string.IsNullOrEmpty(txt_Escaner.Text))
            {
                if (Convert.ToInt32(dgv_Articulos.Rows[numeroRowArticulo].Cells[3].Value) != 0)
                {
                    if (!validaEscaner())
                    {
                        MessageBox.Show("El Id Interno no es el correcto!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Los artículos de esta línea ya han sido certificados!", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            txt_Escaner.Focus();
            txt_Escaner.Text = string.Empty;
        }

        private void btn_Aceptar_Click(object sender, EventArgs e)
        {
            //Se valida que la cantidad solicitada que viene desde la BD sea igual a la cantidad certificada en el DataGridView
            if (Convert.ToInt32(dgv_Articulos.Rows[numeroRowArticulo].Cells[4].Value) == Convert.ToInt32(dgv_Articulos.Rows[numeroRowArticulo].Cells[5].Value))
            {

                //if (Obj_DetPedidos_DAL.iCantidadCertificada == 0)  <-- NO certifica lineas de articulos repetidos en la Ola, problema en la Operativa de los SPs ya creados, no de la Mesa de Empaque
                if (Convert.ToInt32(dgv_Articulos.Rows[numeroRowArticulo].Cells[3].Value) == 0)
                {
                    MessageBox.Show(certificarLineaSSCC(), "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("La cantidad Certificada no es igual que la cantidad Alistada!", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            txt_Escaner.Text = string.Empty;

            txt_Escaner.Focus();
        }

        #region EVENTOS SIMULACION PLACEHOLDER

        private void txt_SSCC_Enter(object sender, EventArgs e)
        {
            txt_SSCC.Text = "";
            txt_SSCC.ForeColor = Color.Black;
        }

        private void txt_SSCC_Leave(object sender, EventArgs e)
        {
            txt_SSCC.Text = "SSCC a Certificar";
            txt_SSCC.ForeColor = Color.Gray;
        }

        #endregion

        private void dgv_Olas_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 8 && e.Value != null)
            {
                bool certificado = Convert.ToBoolean(e.Value);

                e.Value = certificado ? "✓" : "x";
                e.CellStyle.BackColor = certificado ? Color.LightGreen : Color.Red;
                e.CellStyle.SelectionBackColor = certificado ? Color.LightGreen : Color.Red;
                e.CellStyle.ForeColor = Color.White;
                e.CellStyle.Font = new Font("Microsoft YaHei UI", 14, FontStyle.Bold);
            }
        }

        private void dgv_Pedidos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 10 || e.ColumnIndex == 11)
            {
                if (e.Value != null)
                {
                    bool certificado = Convert.ToBoolean(e.Value);

                    e.Value = certificado ? "✓" : "x";
                    e.CellStyle.BackColor = certificado ? Color.LightGreen : Color.Red;
                    e.CellStyle.SelectionBackColor = certificado ? Color.LightGreen : Color.Red;
                    e.CellStyle.ForeColor = Color.White;
                    e.CellStyle.Font = new Font("Microsoft YaHei UI", 14, FontStyle.Bold);
                }
            }
        }

        private void dgv_Articulos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Se verifica que estás en la columna deseada y no en la fila de encabezados
            if (e.ColumnIndex == dgv_Articulos.Columns[3].Index && e.RowIndex >= 0)
            {
                // Verifica si el valor de la columna de la Cantidad Restante es 0
                if (Convert.ToInt32(e.Value) == 0)
                {
                    dgv_Articulos.Rows[e.RowIndex].Cells[5].Style.Font = new Font("Microsoft YaHei UI", 12, FontStyle.Bold);
                }
                else
                {
                    dgv_Articulos.Rows[e.RowIndex].Cells[5].Style.Font = new Font("Microsoft YaHei UI", 12, FontStyle.Regular); //Formato de letra predeterminado
                }
            }

            if (e.ColumnIndex == dgv_Articulos.Columns[8].Index && e.RowIndex >= 0)
            {
                if (Convert.ToInt32(dgv_Articulos.Rows[e.RowIndex].Cells[3].Value) == 0)
                {
                    e.Value = "✓"; // Mostrar un check
                    e.CellStyle.BackColor = Color.LightGreen;
                    e.CellStyle.SelectionBackColor = Color.LightGreen;
                    
                }
                else
                {
                    e.Value = "x"; // Mostrar una X
                    e.CellStyle.BackColor = Color.Red;
                    e.CellStyle.SelectionBackColor = Color.Red;
                }
                e.CellStyle.ForeColor = Color.White;
                e.CellStyle.Font = new Font("Microsoft YaHei UI", 14, FontStyle.Bold);
            }
        }

        #endregion


        #region METODOS

        public void cargarOlas()
        {
            if (Obj_ConsultaOla.sSSCC == null)
            {
                dgv_Olas.DataSource = Obj_Ola_BLL.Obtener_Ecabezado_Olas_a_Certificar(ref Obj_ConsultaOla);
            }
            else
            {
                dgv_Olas.DataSource = Obj_Ola_BLL.GetSSCCProducts(ref Obj_ConsultaOla);
            }

            dgv_Olas.Columns[0].DataPropertyName = "iMaestroSolicitud";
            dgv_Olas.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv_Olas.Columns[1].DataPropertyName = "sComentarios";
            dgv_Olas.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dgv_Olas.Columns[2].DataPropertyName = "sDestinoOla";
            dgv_Olas.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dgv_Olas.Columns[3].DataPropertyName = "sNombreOla";
            dgv_Olas.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dgv_Olas.Columns[4].DataPropertyName = "sPrioridad";
            dgv_Olas.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgv_Olas.Columns[5].DataPropertyName = "dtFechaEntrega";
            dgv_Olas.Columns[5].DefaultCellStyle.Format = "dd/MM/yyyy"; // formato fecha
            dgv_Olas.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgv_Olas.Columns[6].DataPropertyName = "dtFechaCreacion";
            dgv_Olas.Columns[6].DefaultCellStyle.Format = "dd/MM/yyyy"; // formato fecha
            dgv_Olas.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv_Olas.Columns[7].DataPropertyName = "sPorcentajeAlisto";
            dgv_Olas.Columns[7].DefaultCellStyle.Format = "N2"; // formato numérico con 2 decimales
            dgv_Olas.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv_Olas.Columns[8].DataPropertyName = "bCertificado";
            dgv_Olas.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgv_Olas.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public void cargarPedidos()
        {
            dgv_Pedidos.DataSource = Obj_Ola_BLL.Obtener_PedidosOla(ref Obj_Ola_DAL);

            dgv_Pedidos.Columns[0].DataPropertyName = "iNumOla";
            dgv_Pedidos.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv_Pedidos.Columns[1].DataPropertyName = "sNombrePedido";
            dgv_Pedidos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv_Pedidos.Columns[2].DataPropertyName = "sDestino";
            dgv_Pedidos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv_Pedidos.Columns[3].DataPropertyName = "sComentarios";
            dgv_Pedidos.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv_Pedidos.Columns[4].DataPropertyName = "sBodega";
            dgv_Pedidos.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv_Pedidos.Columns[5].DataPropertyName = "sPrioridad";
            dgv_Pedidos.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv_Pedidos.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgv_Pedidos.Columns[6].DataPropertyName = "sDireccion";
            dgv_Pedidos.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv_Pedidos.Columns[7].DataPropertyName = "dtFechaCreacion";
            dgv_Pedidos.Columns[7].DefaultCellStyle.Format = "dd/MM/yyyy"; // formato fecha
            dgv_Pedidos.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv_Pedidos.Columns[8].DataPropertyName = "dtFechaProcesamiento";
            dgv_Pedidos.Columns[8].DefaultCellStyle.Format = "dd/MM/yyyy"; // formato fecha
            dgv_Pedidos.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgv_Pedidos.Columns[9].DataPropertyName = "shEstado";
            dgv_Pedidos.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv_Pedidos.Columns[10].DataPropertyName = "bProcesada";
            dgv_Pedidos.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv_Pedidos.Columns[11].DataPropertyName = "bPedidoCert";
            dgv_Pedidos.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public void cargarDetallePedidos()
        {
            dgv_Articulos.DataSource = Obj_Ola_BLL.Obtener_DetallePedido(ref Obj_Pedido_DAL);

            dgv_Articulos.Columns[0].DataPropertyName = "sNombre";
            dgv_Articulos.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgv_Articulos.Columns[1].DataPropertyName = "sIdInterno";
            dgv_Articulos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dgv_Articulos.Columns[2].DataPropertyName = "iIdArticulo";
            dgv_Articulos.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv_Articulos.Columns[3].DataPropertyName = "dCantidadRestante";
            dgv_Articulos.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv_Articulos.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv_Articulos.Columns[4].DataPropertyName = "dCantidadSolicitada";
            dgv_Articulos.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv_Articulos.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv_Articulos.Columns[5].DataPropertyName = "iCantidadCertificada";
            dgv_Articulos.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv_Articulos.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv_Articulos.Columns[6].DataPropertyName = "iEstado";
            dgv_Articulos.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dgv_Articulos.Columns[7].DataPropertyName = "iIdLineaDetalleSolicitud";
            dgv_Articulos.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //Indica en la conlumna de Certificado si está certificado o no.
            foreach (DataGridViewRow row in dgv_Articulos.Rows)
            {
                if (Convert.ToInt32(row.Cells[3].Value) == 0)
                {
                    row.Cells[8].Value = true;
                }
                else
                {
                    row.Cells[8].Value = false;
                }
            }
            dgv_Articulos.Columns[8].Width = 115;
            dgv_Articulos.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public string certificarLineaSSCC()
        {
            Obj_DetPedidos_DAL.dCantidadRestante = cantidadRestante;
            Obj_DetPedidos_DAL.iCantidadCertificada = cantidadCert;
            Obj_DetPedidos_DAL.dCantidadSolicitada = cantidadSoli;

            return Obj_Ola_BLL.procesarLineaPedido(ref Obj_DetPedidos_DAL);
        }

        public void limpiar()
        {
            dgv_Olas.DataSource = null;
            dgv_Pedidos.DataSource = null;
            dgv_Articulos.DataSource = null;

            Obj_ConsultaOla.sSSCC = null;

            lbl_nombrePedido.Text = string.Empty;

            dgv_Olas.Height = 300;

            label3.Visible = false;
            dgv_Pedidos.Visible = false;
        }

        public bool validaEscaner()
        {
            string barCode = txt_Escaner.Text;

            //Se elimina caracteres agregados por scanner
            barCode = barCode.Substring(0, barCode.Length - 2);

            if (barCode.Equals(idInterno))
            {
                cantidadRestante--;

                cantidadCert++;

                dgv_Articulos.Rows[numeroRowArticulo].Cells[3].Value = cantidadRestante;

                dgv_Articulos.Rows[numeroRowArticulo].Cells[5].Value = cantidadCert;

                return true;
            }
            else
            {
                return false;
            }
        }

        public void ProcesarCertificacionOlaPedido()
        {
            // Certificar pedido
            if (esCertificadoPedido())
            {
                Obj_Ola_BLL.certificarPedido(cls_Login_DAL.shIdBodega, Obj_Pedido_DAL.iNumOla, Obj_ConsultaOla.sSSCC, Obj_DetPedidos_DAL.iIdArticulo);
                cargarPedidos();
            }

            // Certificar ola
            if (esCertificadaOla())
            {
                MessageBox.Show(Obj_Ola_BLL.certificarOla(cls_Login_DAL.iIdUsuario, Obj_Ola_DAL.iMaestroSolicitud), "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cargarOlas();
            }
        }


        private bool esCertificadoPedido()
        {
            bool esCertificado = true;
            if (dgv_Articulos.Rows.Count > 0)
            {
                foreach (DataGridViewRow fila in dgv_Articulos.Rows)
                {
                    //Validamos que si la fila "Cantidad Certificada" no sea null o que el valor de la fila "Cantidad Solicitada" y "Cantidad Certificada" no tengan el mismo valor para que la respuesta se mantenga en true, esto para certificar automaticamente el Pedido.
                    if (fila.Cells[3].Value == null || (Convert.ToInt32(fila.Cells[4].Value) != (Convert.ToInt32(fila.Cells[5].Value))))
                    {
                        esCertificado = false;
                        break;
                    }
                }
            }

            return esCertificado;
        }


        private bool esCertificadaOla()
        {
            bool esCertificado = true;

            if (dgv_Pedidos.Rows.Count > 0)
            {
                foreach (DataGridViewRow fila in dgv_Pedidos.Rows)
                {
                    //Validamos que si la fila "Cantidad Certificada" no sea null o que el valor de la fila "Certificado" no sea false para que el retorno se mantenga en true, esto para certificar automaticamente la Ola
                    if (fila.Cells[11].Value == null || !(bool)fila.Cells[11].Value)
                    {
                        esCertificado = false;
                        break;
                    }
                }
            }

            return esCertificado;
        }

        #endregion
    }
}
