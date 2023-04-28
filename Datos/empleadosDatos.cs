using TEST_CRUD.Models;
using System.Data.SqlClient;
using System.Data;

namespace TEST_CRUD.Datos
{
    public class empleadosDatos
    {
        public List<empleadosModel> Listar()
        {
            var oLista = new List<empleadosModel>();
            var cn = new Conexion();
            using (var con = new SqlConnection(cn.getconexion()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarPersonal", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new empleadosModel()
                        {
                            empleadoId = Convert.ToInt32(dr["PID"]),
                            NPersonal = dr["NP"].ToString(),
							APersonal = dr["AP"].ToString(),
							FN = Convert.ToDateTime(dr["FN"]),
							FI = Convert.ToDateTime(dr["FI"]),
							afp = dr["AFP"].ToString(),
                            sueldo = Convert.ToDouble(dr["sueldo"]),
							NRoles = dr["RN"].ToString()
                        });
                    }
                }
            }
            return oLista;
        }
        
        public List<empleadosModel> ListarR()
        {
            var oRoles = new List<empleadosModel>();
            var cn = new Conexion();
            using (var con = new SqlConnection(cn.getconexion()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarRoles", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oRoles.Add(new empleadosModel()
                        {
                            RolesId = Convert.ToInt32(dr["Roles_id"]),
                            NRoles = dr["name"].ToString(),
                            Descripcion = dr["description"].ToString()
                        });
                    }
                }
            }
            return oRoles;
        }
        
        public empleadosModel ObtenerPId(int ID)
        {
            var oI_ID = new empleadosModel();
            var cn = new Conexion();
            using (var con = new SqlConnection(cn.getconexion()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ObtenerIdPersonal", con);
                cmd.Parameters.AddWithValue("P_ID",ID);
                cmd.CommandType = CommandType.StoredProcedure;

                using(var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oI_ID.empleadoId = Convert.ToInt32(dr["Personas_id"]);
                        oI_ID.NPersonal = dr["nombres"].ToString();
                        oI_ID.APersonal = dr["apellidos"].ToString();
						oI_ID.FN = Convert.ToDateTime(dr["f_nacimiento"]);
						oI_ID.FI = Convert.ToDateTime(dr["f_ingreso"]);
						oI_ID.afp = dr["afp"].ToString();
                        oI_ID.sueldo = Convert.ToDouble(dr["dni"]);
						oI_ID.RolesId = Convert.ToInt32(dr["Roles_id"]);
						oI_ID.NRoles = dr["name"].ToString();
						oI_ID.Descripcion = dr["description"].ToString();
                    }
                }
            }
            return oI_ID;
        }
        
        
        public bool Guardar(empleadosModel oGuardarP)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();
                using (var con = new SqlConnection(cn.getconexion()))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_GuardarPersonal", con);
                    cmd.Parameters.AddWithValue("NP", oGuardarP.NPersonal);
                    cmd.Parameters.AddWithValue("AP", oGuardarP.APersonal);
					cmd.Parameters.AddWithValue("F_N", oGuardarP.FN);
                    cmd.Parameters.AddWithValue("F_I", oGuardarP.FI);
                    cmd.Parameters.AddWithValue("afp", oGuardarP.afp);
					cmd.Parameters.AddWithValue("sueldo", oGuardarP.sueldo);
					cmd.Parameters.AddWithValue("R_id", oGuardarP.RolesId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            }
            catch(Exception e)
            {
                string error = e.Message;
                rpta = false;
            }
            return rpta;
        }

        public bool Editar(empleadosModel oEditarI)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();
                using (var con = new SqlConnection(cn.getconexion()))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_EditarPersonal", con);
                    cmd.Parameters.AddWithValue("P_Id", oEditarI.empleadoId);
                    cmd.Parameters.AddWithValue("NP", oEditarI.NPersonal);
                    cmd.Parameters.AddWithValue("AP", oEditarI.APersonal);
					cmd.Parameters.AddWithValue("F_N", oEditarI.FN);
                    cmd.Parameters.AddWithValue("F_I", oEditarI.FI);
                    cmd.Parameters.AddWithValue("afp", oEditarI.afp);
					cmd.Parameters.AddWithValue("sueldo", oEditarI.sueldo);
					cmd.Parameters.AddWithValue("R_id", oEditarI.RolesId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }
            return rpta;
        }

        public bool Eliminar(int ID)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();
                using (var con = new SqlConnection(cn.getconexion()))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarPersonal", con);
                    cmd.Parameters.AddWithValue("P_Id", ID);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }
            return rpta;
        }
    }
}
