using Dapper;
using MySql.Data.MySqlClient;

namespace API.Data.Repositories
{
    public static class AuxiliarDb
    {
        public static List<TEntity> RodaQuery<TEntity>(string sql, string connectionString) where TEntity : new()
        {
            List<TEntity> retorno;
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(sql.ToString(), con);

                    retorno = con.Query<TEntity>(sql).ToList();


                }
                catch (Exception ex)
                {
                    try
                    {
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand(sql.ToString(), con);

                        retorno = con.Query<TEntity>(sql).ToList();
                    }
                    catch (Exception ex2)
                    {
                        throw ex2;
                    }

                }
                finally
                {
                    con.Close();
                }
            }

            return retorno;
        }




        public static string IncliurUnidadeUsuario(int? usuarioId)
        {
            if (usuarioId.HasValue)
                return $" join ws_pmp.tb_usuario_unidade us on u.unidade_id = us.unidade_id and us.usuario_id = {usuarioId}";
            else
                return string.Empty;
        }

        public static string RetornarFormatoAno(string campo)
        {
            return $"YEAR({campo})";
        }

        public static string RetornarFormatoData()
        {
            return $"yyyy-MM-dd";
        }

        public static string LimitarLinhas(int linhas)
        {
            return "LIMIT " + linhas;
        }
    }
}
