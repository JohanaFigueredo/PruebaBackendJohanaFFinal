//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms;
//using System.Data.SqlClient;

//namespace PruebaBackendJohanaF
//{
//    class ConexionBD
//    {
//        string sConnection = "Data Source=DESKTOP-HGI\\SQLEXPRESS;Initial Catalog=PruebaBackend; Integrated Security=True";
//        public SqlConnection ConectarBD = new SqlConnection();

//        public ConexionBD()
//        {
//            ConectarBD.ConnectionString = sConnection;
//        }

//        public void AbrirBD() {
//            try{
//                ConectarBD.Open();
//                MessageBox.Show("Coneccion exitosa  BD");
//            }
//            catch (Exception ex){
//                MessageBox.Show("Error abriendo  BD");
//            }

//        }

//    }
//}
