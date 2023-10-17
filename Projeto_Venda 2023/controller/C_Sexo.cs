using Projeto_Venda_2023.conexao;
using Projeto_Venda_2023.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_Venda_2023.controller
{
    internal class C_Sexo : I_CRUD
    {
        SqlConnection con;
        SqlCommand cmd;

        string sqlInsere = "insert into sexo (nomesexo) values (@Nome)";
        string sqlEditar = "update sexo set nomesexo = @Nome where id = @Nome";
        string sqlApagar = "delete from sexo where codsexo = @Id";
        string sqlTodos = "select * from sexo"; //retorna todos os sexos
        string sqlBuscarId = "select * from sexo where codsexo = @Id";
        public void apagaDados(int cod)
        {
            ConectaBanco cb = new ConectaBanco();
            con = cb.conectaSqlServer();
            cmd = new SqlCommand(sqlApagar, con);

            //Passando parâmetros para a sentença SQL
            cmd.Parameters.AddWithValue("@Id", cod);
            cmd.CommandType = CommandType.Text;
            con.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Sexo deletado com Sucesso!!!\n" +
                        "Código: " + cod);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Apagar!\nErro:" + ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public DataTable buscarTodos()
        {
            throw new NotImplementedException();
        }


        //List para carregar ComboBox da aplicação.
        public List<Sexo> carregaDados()
        {
            List<Sexo> lista_sexo = new List<Sexo>();

            ConectaBanco cb = new ConectaBanco();
            con = cb.conectaSqlServer();
            cmd = new SqlCommand(sqlTodos, con);

            cmd.CommandType = CommandType.Text;
            
            SqlDataReader tabSexo; //Representa uma Tabela Virtual para a leitura de dados
            con.Open();


            try
            {
                tabSexo = cmd.ExecuteReader();
                while (tabSexo.Read())
                {
                   Sexo aux = new Sexo();
                    
                    aux.Codsexo = Int32.Parse(tabSexo["codsexo"].ToString());
                    aux.Nomesexo = tabSexo["nomesexo"].ToString();

                    lista_sexo.Add(aux);
                }
            }
            catch (Exception ex)
            {

            }
            finally { con.Close(); }

            return lista_sexo;
        }

        public void insereDados(object obj)
        {
            Sexo sexo = new Sexo();
            sexo = (Sexo)obj;

            ConectaBanco cb = new ConectaBanco();
            con = cb.conectaSqlServer();
            cmd = new SqlCommand(sqlInsere, con);

            cmd.Parameters.AddWithValue("@Nome", sexo.Nomesexo);
            cmd.CommandType = CommandType.Text;
            con.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Registro incluído com sucesso");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }
    }
}
