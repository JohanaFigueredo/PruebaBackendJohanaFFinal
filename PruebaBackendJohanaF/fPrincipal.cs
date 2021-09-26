using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;



namespace PruebaBackendJohanaF
{
    public partial class pnlPrincipal : Form
    {
        public pnlPrincipal()
        {
            InitializeComponent();
        }

        //declaro variables 
        int ValidacionData = 0;
        int DataDuplicada = 0;

        //valido que el campo nombre solo acepte letras
        private void tbNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //valido que solo acepte solo acepte letras
                if (e.KeyChar >= 33 && e.KeyChar <= 64 || e.KeyChar >= 91 && e.KeyChar <= 96 )
                {
                    MessageBox.Show("Introducir solo letras", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    e.Handled = true;

                    LogEventos("I", 1, "Validación del campo nombres, función tbNombre_KeyPress, de manera exitosa.");

                    return;
                }
            }
            catch (Exception ex)
            {
                LogEventos("E", 1, "Error en la funcion tbNombre_KeyPress." + ex);
            }
        }

        //valido que el campo Apellidos solo acepte letras
        private void tbApellidos_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //valido que solo acepte solo acepte letras
                if (e.KeyChar >= 33 && e.KeyChar <= 64 || e.KeyChar >= 91 && e.KeyChar <= 96)
                {
                    MessageBox.Show("Introducir solo letras", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    e.Handled = true;

                    LogEventos("I", 2, "Validación del campo Apellidos, función tbApellidos_KeyPress, de manera exitosa.");

                    return;
                }
            }
            catch (Exception ex)
            {
                LogEventos("E", 2, "Error en la funcion tbApellidos_KeyPress." + ex);
            }
        }

        //valido que el campo fecha de nacimiento solo acepte numeros
        private void tbFechaNacimiento_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //valido que solo acepte solo acepte numeros y el separador de fecha
                if (e.KeyChar >= 32 && e.KeyChar <= 46 || e.KeyChar >= 58 && e.KeyChar <= 126)
                {
                    MessageBox.Show("Introducir solo números y separador  de fecha / .", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    e.Handled = true;

                    LogEventos("I", 3, "Validación del campo fecha de nacimiento, función tbFechaNacimiento_KeyPress, de manera exitosa.");

                    return;
                }
            }
            catch (Exception ex)
            {
                LogEventos("E", 3, "Error en la funcion tbFechaNacimiento_KeyPress." + ex);
            }
        }

        //valido que el campo cedula solo acepte numeros
        public void tbCedula_KeyPress(object sender, KeyPressEventArgs e)
      {
            try
            {
                //valido que solo acepte valores numericos
                if (e.KeyChar >= 32 && e.KeyChar <= 47 || e.KeyChar >= 58 && e.KeyChar <= 255)
                {
                    MessageBox.Show("Introducir solo números", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    e.Handled = true;

                    LogEventos("I", 4, "Validación de cédula, función tbCedula_KeyPress, de manera exitosa.");

                    return;
                }
            }
            catch (Exception ex) {
                LogEventos("E",4, "Error en la funcion tbCedula_KeyPress." + ex);
            }
        }

        //valido que el campo telefono solo acepte numeros
        public void tbTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            try {
                //valido que solo acepte valores numericos
                if (e.KeyChar >= 32 && e.KeyChar <= 47 || e.KeyChar >= 58 && e.KeyChar <= 255)
                {
                    MessageBox.Show("Introducir solo números", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    e.Handled = true;

                    LogEventos("I", 13, "Validación de teléfono, función tbTelefono_KeyPress, de manera exitosa.");

                    return;
                }
             }
            catch (Exception ex){
                LogEventos("E", 13, "Error en la funcion tbTelefono_KeyPress." + ex);
            }
        }

        //boton para guardar en base d edatos
        public void btnGuardar_Click(object sender, EventArgs e)
        {
            try {
                string Nombres = tbNombre.Text;
                string Apellidos = tbApellidos.Text;
                string Cedula = tbCedula.Text;
                string FechaNac = tbFechaNacimiento.Text;
                string Direccion = tbDireccion.Text;
                string correo = tbCorreo.Text;
                string Telefono = tbTelefono.Text;
                string Organizacion = tbOrganizacion.Text;

                //Valido los datos antes de guardar en BD
                ValidarData(Nombres, Apellidos, Cedula, FechaNac, Direccion, correo, Telefono, Organizacion);

                if (ValidacionData == -1)
                {
                    MessageBox.Show("Error en validacion de datos.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    ValidacionData = 0;
                }
                else
                {
                    //valido que la data no este duplicada (campos claves cedula, correo y telefono)
                    ValidarDataDuplicada(Cedula, correo, Telefono);
                    if (DataDuplicada == -1)
                    {
                        //MessageBox.Show("Error, los datos ya existen en BD.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                    else
                    {
                        GuardarBaseDatos(Nombres, Apellidos, Cedula, FechaNac, Direccion, correo, Telefono, Organizacion);
                    }
                }
                LogEventos("I", 3, "Procesamiento exitoso en la funcion btnGuardar_Click.");
            }
            catch (Exception ex) {
                LogEventos("E", 3, "Error en la funcion btnGuardar_Click." + ex);
            }

        }

        //Funcion que realiza las diferentes validaciones de data antes de cargar en la base de datos
        public void ValidarData(string Nombres, string Apellidos, string Cedula, string FechaNac, string Direccion, string correo, string Telefono, string Organizacion) {
            try
            {   //valido que sea un correo valido
                if (!IsValidEmail(correo))
                {
                    MessageBox.Show("Dirección de correo electrónico no válida. Por favor corrija e intente guardar los datos nuevamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    LogEventos("I", 4, "Dirección de correo no valida para el registro " + Cedula);
                    ValidacionData = -1;
                }
                //valido que los campos no esten vacios
                if (!CamposCargados(Nombres, Apellidos, Cedula, FechaNac, Direccion, correo, Telefono, Organizacion))
                {
                    MessageBox.Show("No puede haber campos vacíos. Por favor valide e intente guardar los datos nuevamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    LogEventos("I", 4, "Existen campos vacios para el registro " + Cedula);
                    ValidacionData = -1;
                }
                LogEventos("I", 4, "Validacion exitosa en la funcion ValidarData para el registro " + Cedula);
            }
            catch (Exception ex)
            {
                LogEventos("E", 4, "Error en la funcion ValidarData." + ex);
                return;
            }
        }//fin ValidarData

        //funcion creada para validar las direcciones de correo 
        public static bool IsValidEmail(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    return false;
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        //funcion creada para validar que todas las casillas esten cargadas
        public static bool CamposCargados(string Nombres, string Apellidos, string Cedula, string FechaNac, string Direccion, string correo, string Telefono, string Organizacion)
        {
            try
            {
                if (String.IsNullOrEmpty(Nombres.ToString()) || String.IsNullOrEmpty(Apellidos.ToString()) || String.IsNullOrEmpty(Cedula.ToString()) || String.IsNullOrEmpty(FechaNac.ToString()) || String.IsNullOrEmpty(Direccion.ToString()) || String.IsNullOrEmpty(correo.ToString()) || String.IsNullOrEmpty(Telefono.ToString()) || String.IsNullOrEmpty(Organizacion.ToString()))
                    return false;
            }
            catch
            {
                return false;
            }
            return true;
        }

        //funcion que guarda ya la data verificada en la base de datos
        public void GuardarBaseDatos(string Nombres, string Apellidos, string Cedula, string FechaNac, string Direccion, string correo, string Telefono, string Organizacion)
        {
            try {
                SqlCommand oCommand = null;
                SqlConnection oConnection = null;
                string sConnection = "Data Source=DESKTOP-HGI\\SQLEXPRESS;Initial Catalog=PruebaBackend; Integrated Security=True";
                oConnection = new SqlConnection(sConnection);
                oConnection.Open();

                oCommand = new SqlCommand("sp_GuardarBD", oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;

                oCommand.Parameters.Add("@Nombres", SqlDbType.VarChar, 50).Value = Nombres;
                oCommand.Parameters.Add("@Apellidos", SqlDbType.VarChar, 50).Value = Apellidos;
                oCommand.Parameters.Add("@Cedula", SqlDbType.VarChar, 50).Value = Cedula;
                oCommand.Parameters.Add("@FechaNac", SqlDbType.VarChar, 50).Value = FechaNac;
                oCommand.Parameters.Add("@Direccion", SqlDbType.VarChar, 50).Value = Direccion;
                oCommand.Parameters.Add("@correo", SqlDbType.VarChar, 50).Value = correo;
                oCommand.Parameters.Add("@Telefono", SqlDbType.VarChar, 50).Value = Telefono;
                oCommand.Parameters.Add("@Organizacion", SqlDbType.VarChar, 50).Value = Organizacion;
                Int32 iExecute = oCommand.ExecuteNonQuery();
                if (iExecute == -1)
                {
                    GuardarTablaOrganizacion(Nombres, Apellidos, Direccion);
                    MessageBox.Show("Los datos fueron guardados exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.tbNombre.Text = "";
                    this.tbApellidos.Text = "";
                    this.tbCedula.Text = "";
                    this.tbCorreo.Text = "";
                    this.tbDireccion.Text = "";
                    this.tbFechaNacimiento.Text = "";
                    this.tbOrganizacion.Text = "";
                    this.tbTelefono.Text = "";
                    this.tbNombre.Focus();
                    LogEventos("I", 5, "Registro " + Cedula + " guardado exitosamente en la base de datos");
                }
                else {
                    MessageBox.Show("Los datos no fueron guardados exitosamente. Contacte a su proveedor.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LogEventos("E",5, "Problemas guardando el registro " + Cedula + " en la base de datos");
                }
                oConnection.Close();
            }
            catch (Exception ex) {
                LogEventos("E", 5, "Error en la funcion GuardarBaseDatos." + ex);
            }

        }

        //funcion que valida que los campos clave (Cedula, correo, Telefono) no se dupliquen en BD
        public void ValidarDataDuplicada (string Cedula, string correo, string Telefono)
        {
            try
            {
                SqlDataAdapter oAdapter = null;
                SqlConnection oConnection = null;
                SqlCommand oCommand = null;
                DataSet dtsDatos = new DataSet();
                DataDuplicada = 0;

                System.Data.DataSet oDataSet = new System.Data.DataSet();
                string sConnection = "Data Source=DESKTOP-HGI\\SQLEXPRESS;Initial Catalog=PruebaBackend; Integrated Security=True";
                oConnection = new SqlConnection(sConnection);
                oConnection.Open();

                //valido duplicidada de la cedula
                oCommand = new SqlCommand("sp_VerDuplicidadCedula", oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                oAdapter = new SqlDataAdapter();
                oCommand.Parameters.Add("@Cedula", SqlDbType.VarChar, 50).Value = Cedula;
                oAdapter.SelectCommand = oCommand;
                oAdapter.Fill(oDataSet);

                if (oDataSet.Tables[0].Rows.Count != 0)
                {
                    MessageBox.Show("El número de cédula de identidad ya existe en la base de datos.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    LogEventos("I", 6, "Registro cedula: " + Cedula + " ya existe en BD. Fue validado exitosamente.");
                    DataDuplicada = -1;
                }
                //valido duplicidada del correo
                System.Data.DataSet oDataSet2 = new System.Data.DataSet();
                oCommand = new SqlCommand("sp_VerDuplicidadCorreo", oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                oAdapter = new SqlDataAdapter();
                oCommand.Parameters.Add("@correo", SqlDbType.VarChar, 50).Value = correo;
                oAdapter.SelectCommand = oCommand;
                oAdapter.Fill(oDataSet2);

                if (oDataSet2.Tables[0].Rows.Count != 0)
                {
                    MessageBox.Show("La dirección de correo eletrónico ya existe en la base de datos.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    LogEventos("I", 6, "Registro correo:  " + correo + " ya existe en BD. Fue validado exitosamente.");
                    DataDuplicada = -1;
                }
                //valido duplicidada del telefono
                System.Data.DataSet oDataSet3 = new System.Data.DataSet();
                oCommand = new SqlCommand("sp_VerDuplicidadTelefono", oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                oAdapter = new SqlDataAdapter();
                oCommand.Parameters.Add("@Telefono", SqlDbType.VarChar, 50).Value = Telefono;
                oAdapter.SelectCommand = oCommand;
                oAdapter.Fill(oDataSet3);

                if (oDataSet3.Tables[0].Rows.Count != 0)
                {
                    MessageBox.Show("El número telefónico ya existe en la base de datos.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    LogEventos("I", 6, "Registro teléfono:  " + Telefono + " ya existe en BD. Fue validado exitosamente.");
                    DataDuplicada = -1;
                }
                else {
                    LogEventos("I", 6, "Registro " + Cedula + " no está duplicado en BD. Fue validado exitosamente.");
                }
                oDataSet = null;
                oDataSet2 = null;
                oDataSet3 = null;
                oConnection.Close();
            }
            catch (Exception ex)
            {
                LogEventos("E", 6, "Error en la funcion ValidarDataDuplicada." + ex);
            }
        }

        // Funcion para guardar registrso en la tabla Organizacion 
        public void GuardarTablaOrganizacion(string Nombres, string Apellidos, String Direccion)
        {
            try
            {
                SqlCommand oCommand = null;
                SqlConnection oConnection = null;
                string sConnection = "Data Source=DESKTOP-HGI\\SQLEXPRESS;Initial Catalog=PruebaBackend; Integrated Security=True";
                oConnection = new SqlConnection(sConnection);
                oConnection.Open();

                oCommand = new SqlCommand("sp_GuardarOrganizacion", oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;

                oCommand.Parameters.Add("@Nombres", SqlDbType.VarChar, 50).Value = Nombres;
                oCommand.Parameters.Add("@Apellidos", SqlDbType.VarChar, 50).Value = Apellidos;
                oCommand.Parameters.Add("@Direccion", SqlDbType.VarChar, 50).Value = Direccion;
                Int32 iExecute = oCommand.ExecuteNonQuery();
                if (iExecute == -1)
                {
                    LogEventos("I", 7, "Registro guardado exitosamente en la base de datos");
                }
                LogEventos("I", 7, "Registro no fue guardado exitosamente en la base de datos");
            }
            catch (Exception ex)
            {
                LogEventos("E", 7, "Error en la funcion GurdarTablaOrganizacion." + ex);
            }
        }

        //Funcion que guarda en BD los eventos del sistema
        private void LogEventos(string IdTipoEvento, int idFuncion, string DescrpcionEvento)
    {
        try
        {
            SqlCommand oCommand = null;
            SqlConnection oConnection = null;
            string sConnection = "Data Source=DESKTOP-HGI\\SQLEXPRESS;Initial Catalog=PruebaBackend; Integrated Security=True";
            oConnection = new SqlConnection(sConnection);
            oConnection.Open();

            oCommand = new SqlCommand("sp_RegistrarEvento", oConnection);
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.Parameters.Add("@IdTipoEvento", SqlDbType.VarChar, 10).Value = IdTipoEvento;
            oCommand.Parameters.Add("@IdFuncion", SqlDbType.Int).Value = idFuncion;
            oCommand.Parameters.Add("@DescripcionEvento", SqlDbType.VarChar, 4000).Value = DescrpcionEvento;
            Int32 iExecute = oCommand.ExecuteNonQuery();
            if (iExecute != -1)
            {
                MessageBox.Show("Error guardando en el log del sistema. Por favor contacte a su proveedor de servicio", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error en función LogEventos. Por favor contacte a su proveedor de servicio" + ex, "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
        }

    }

        private void tbnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string Cedula = tbCedula.Text;
                string correo = tbCorreo.Text;
                string Telefono = tbTelefono.Text;

                //llamo la funcion BuscarRegistros
                BuscarRegistros(Cedula, correo, Telefono);
               
                LogEventos("I", 8, "Procesamiento exitoso en la funcion tbnBuscar_Click.");
            }
            catch (Exception ex)
            {
                LogEventos("E", 8, "Error en la funcion tbnBuscar_Click." + ex);
            }

        }

        public void BuscarRegistros(string Cedula, string correo, string Telefono)
        {
            string DatoConsulta = null;
            string Nombre = null;
            string Apellido = null;
            string sCedula = null;
            string FechaNacimiento = null;
            string Direccion = null;
            string sCorreo = null;
            string stelefono = null;
            string sOrganizacion = null;
            int NumTabla = 0;
            try
            {
                if (Cedula != "")
                {
                    DatoConsulta = Cedula;
                    NumTabla = 0;
                }
                if (correo != "")
                {
                    DatoConsulta = correo;
                    NumTabla = 1;
                }
                if (Telefono != "")
                {
                    DatoConsulta = Telefono;
                    NumTabla = 2;
                }

                if (DatoConsulta == "") {
                    MessageBox.Show("Para realizar la busqueda debe ingresar algunos de los datos como cédula, correo o correo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                SqlDataAdapter oAdapter = null;
                SqlConnection oConnection = null;
                SqlCommand oCommand = null;
                DataSet dtsDatos = new DataSet();

                System.Data.DataSet oDataSet = new System.Data.DataSet();
                string sConnection = "Data Source=DESKTOP-HGI\\SQLEXPRESS;Initial Catalog=PruebaBackend; Integrated Security=True";
                oConnection = new SqlConnection(sConnection);
                oConnection.Open();

                //valido duplicidada de la cedula
                oCommand = new SqlCommand("sp_ConsultarData", oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                oAdapter = new SqlDataAdapter();
                oCommand.Parameters.Add("@DatoConsulta", SqlDbType.VarChar, 50).Value = DatoConsulta;
                oAdapter.SelectCommand = oCommand;
                oAdapter.Fill(oDataSet);

                if (oDataSet.Tables[NumTabla].Rows.Count != 0)
                {
                    for (int j = 0; j < oDataSet.Tables[NumTabla].Rows.Count; j++)
                    {
                        Nombre = oDataSet.Tables[NumTabla].Rows[0]["Nombres"].ToString();
                        Apellido = oDataSet.Tables[NumTabla].Rows[0]["Apellidos"].ToString();
                        sCedula = oDataSet.Tables[NumTabla].Rows[0]["CI"].ToString();
                        FechaNacimiento = oDataSet.Tables[NumTabla].Rows[0]["FechaNacimiento"].ToString();
                        Direccion = oDataSet.Tables[NumTabla].Rows[0]["Direccion"].ToString();
                        sCorreo = oDataSet.Tables[NumTabla].Rows[0]["Correo"].ToString();
                        stelefono = oDataSet.Tables[NumTabla].Rows[0]["Telefono"].ToString();
                        sOrganizacion = oDataSet.Tables[NumTabla].Rows[0]["OrganizacionLaboral"].ToString();

                        tbNombre.Text = Nombre;
                        tbApellidos.Text = Apellido;
                        tbCedula.Text = sCedula;
                        tbFechaNacimiento.Text = FechaNacimiento;
                        tbDireccion.Text = Direccion;
                        tbCorreo.Text = sCorreo;
                        tbTelefono.Text = stelefono;
                        tbOrganizacion.Text = sOrganizacion;
                    }
                }
                else {
                    MessageBox.Show("El registro no se encuentra en nuestra BD", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogEventos("I", 9, "El registro "+ DatoConsulta+" no se encuentra en BD.");
                }
                LogEventos("I", 9, "Busqueda exitosa del registro en BD." );
                oDataSet = null;
                oConnection.Close();
            }
            catch (Exception ex)
            {
                LogEventos("E", 9, "Error en la funcion BuscarRegistros." + ex);
            }
        }

        //Funcion creada para limpiar el formulario y realizar otras consultas
        private void btnLimpieza_Click(object sender, EventArgs e)
        {
            try
            {
                this.tbNombre.Text = "";
                this.tbApellidos.Text = "";
                this.tbCedula.Text = "";
                this.tbCorreo.Text = "";
                this.tbDireccion.Text = "";
                this.tbFechaNacimiento.Text = "";
                this.tbOrganizacion.Text = "";
                this.tbTelefono.Text = "";
                this.tbNombre.Focus();

                LogEventos("I", 10, "Limpieza de formulario de manera exitosa");
            }
            catch (Exception ex){
                LogEventos("E", 10, "Error en la funcion btnLimpieza_Click." + ex);
            }
        }

        //Boton para eliminar registros
        private void btnEliminarRegistro_Click(object sender, EventArgs e)
        {
            try
            {
                string Cedula = tbCedula.Text;
                string correo = tbCorreo.Text;
                string Telefono = tbTelefono.Text;

                //llamo la funcion eliminar registro
                EliminarRegistro(Cedula, correo, Telefono);

                LogEventos("I", 11, "Procesamiento exitoso en la funcion btnEliminarRegistro_Click.");
            }
            catch (Exception ex)
            {
                LogEventos("E", 11, "Error en la funcion btnEliminarRegistro_Click." + ex);
            }

        }

        //Funcion creada para eliminar registros en la base de datos
        public void EliminarRegistro(string Cedula, string correo, string Telefono)
        {
            string DatoConsulta = null;
            int NumTabla = 0;
            try
            {
                if (Cedula != "")
                {
                    DatoConsulta = Cedula;
                    NumTabla = 0;
                }
                if (correo != "")
                {
                    DatoConsulta = correo;
                    NumTabla = 1;
                }
                if (Telefono != "")
                {
                    DatoConsulta = Telefono;
                    NumTabla = 2;
                }
                if (DatoConsulta == "")
                {
                    MessageBox.Show("Para eliminar un registro debe ingresar algunos de los datos como cédula, correo o correo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                SqlDataAdapter oAdapter = null;
                SqlCommand oCommand = null;
                SqlConnection oConnection = null;
                DataSet dtsDatos = new DataSet();
                System.Data.DataSet oDataSet = new System.Data.DataSet();
                string sConnection = "Data Source=DESKTOP-HGI\\SQLEXPRESS;Initial Catalog=PruebaBackend; Integrated Security=True";
                oConnection = new SqlConnection(sConnection);
                oConnection.Open();

                oCommand = new SqlCommand("sp_ConsultarData", oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                oAdapter = new SqlDataAdapter();
                oCommand.Parameters.Add("@DatoConsulta", SqlDbType.VarChar, 50).Value = DatoConsulta;
                oAdapter.SelectCommand = oCommand;
                oAdapter.Fill(oDataSet);

                if (oDataSet.Tables[NumTabla].Rows.Count != 0)
                {
                    oCommand = new SqlCommand("sp_EliminarRegistroBD", oConnection);
                    oCommand.CommandType = CommandType.StoredProcedure;

                    oCommand.Parameters.Add("@DatoConsulta", SqlDbType.VarChar, 50).Value = DatoConsulta;
                    Int32 iExecute = oCommand.ExecuteNonQuery();
                    if (iExecute == -1)
                    {
                        MessageBox.Show("Registro eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.tbNombre.Clear();
                        this.tbApellidos.Clear();
                        this.tbFechaNacimiento.Clear();
                        this.tbDireccion.Clear();
                        this.tbCedula.Clear();
                        this.tbCorreo.Clear();
                        this.tbTelefono.Clear();
                        this.tbOrganizacion.Clear();
                        this.tbNombre.Focus();
                        LogEventos("I", 11, "Registro " + DatoConsulta + "eliminado exitosamente en la base de datos");
                    }
                }
                else {
                    MessageBox.Show("El Registro no existe en la base de datos.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogEventos("I", 11, "Registro " + DatoConsulta + "no existe en la base de datos");
                }
                oDataSet = null;
                oConnection.Close();
            }
            catch (Exception ex)
            {
                LogEventos("E", 11, "Error en la funcion EliminarRegistro." + ex);
            }
        }

        //Boton para actualizar registros
        private void btnActualizarRegistros_Click(object sender, EventArgs e)
        {
            try
            {
                string Nombres = tbNombre.Text;
                string Apellidos = tbApellidos.Text;
                string Cedula = tbCedula.Text;
                string FechaNac = tbFechaNacimiento.Text;
                string Direccion = tbDireccion.Text;
                string correo = tbCorreo.Text;
                string Telefono = tbTelefono.Text;
                string Organizacion = tbOrganizacion.Text;

                //envío mensaje de aviso al usuario para que actualice la informacion deseada
                MessageBox.Show("El campo Cédula De Identidad no puede ser actualizado, debe eliminarlo y guardar los datos nuevamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //llamo a la funcion validar data para validar los registrso
                ValidarData(Nombres, Apellidos, Cedula, FechaNac, Direccion, correo, Telefono, Organizacion);

                if (ValidacionData == -1)
                {
                    MessageBox.Show("Error en validacion de datos.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    ValidacionData = 0;
                }
                else
                {
                    //llamo la funcion Actualizar reg
                    ActualizarRegistro();
                    LogEventos("I", 12, "Procesamiento exitoso en la funcion btnEliminarRegistro_Click.");
                }
            }
            catch (Exception ex)
            {
                LogEventos("E", 12, "Error en la funcion btnEliminarRegistro_Click." + ex);
            }
        }

        //funcion creada para actualizar registros en la base de datos
        public void ActualizarRegistro()
        {
            string Nombre = null;
            string Apellido = null;
            string Cedula = null;
            string FechaNacimiento = null;
            string Direccion = null;
            string Correo = null;
            string telefono = null;
            string Organizacion = null;
            try
            {
                Nombre = tbNombre.Text;
                Apellido = tbApellidos.Text;
                Cedula = tbCedula.Text; ;
                FechaNacimiento = tbFechaNacimiento.Text;
                Direccion = tbDireccion.Text;
                Correo = tbCorreo.Text; ;
                telefono = tbTelefono.Text;
                Organizacion = tbOrganizacion.Text;

                SqlCommand oCommand = null;
                SqlConnection oConnection = null;
                string sConnection = "Data Source=DESKTOP-HGI\\SQLEXPRESS;Initial Catalog=PruebaBackend; Integrated Security=True";
                oConnection = new SqlConnection(sConnection);
                oConnection.Open();

                oCommand = new SqlCommand("sp_ActualizarRegistro", oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;

                oCommand.Parameters.Add("@Nombres", SqlDbType.VarChar, 50).Value = Nombre;
                oCommand.Parameters.Add("@Apellidos", SqlDbType.VarChar, 50).Value = Apellido;
                oCommand.Parameters.Add("@Cedula", SqlDbType.VarChar, 50).Value = Cedula;
                oCommand.Parameters.Add("@FechaNac", SqlDbType.VarChar, 50).Value = FechaNacimiento;
                oCommand.Parameters.Add("@Direccion", SqlDbType.VarChar, 50).Value = Direccion;
                oCommand.Parameters.Add("@correo", SqlDbType.VarChar, 50).Value = Correo;
                oCommand.Parameters.Add("@Telefono", SqlDbType.VarChar, 50).Value = telefono;
                oCommand.Parameters.Add("@Organizacion", SqlDbType.VarChar, 50).Value = Organizacion;
                Int32 iExecute = oCommand.ExecuteNonQuery();
                if (iExecute == -1)
                {
                    MessageBox.Show("Datos fueron actualizados exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.tbNombre.Text = "";
                    this.tbApellidos.Text = "";
                    this.tbCedula.Text = "";
                    this.tbCorreo.Text = "";
                    this.tbDireccion.Text = "";
                    this.tbFechaNacimiento.Text = "";
                    this.tbOrganizacion.Text = "";
                    this.tbTelefono.Text = "";
                    this.tbNombre.Focus();
                    LogEventos("I", 12, "Registro " + Cedula + " actualizado exitosamente en la base de datos");
                }
                oConnection.Close();
            }
            catch (Exception ex)
            {
                LogEventos("E", 12, "Error en la funcion ActualizarRegistro." + ex);
            }
        }


    }//fin clase

}//fin namespace
