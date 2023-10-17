using Projeto_Venda_2023.conexao;
using Projeto_Venda_2023.model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_Venda_2023.controller
{
    internal class C_Uf : I_CRUD
    {
        SqlConnection con;
        SqlCommand cmd;

        string sqlInsere = "insert into uf (nomeuf, sigla) values (@Nome, @Sigla)";
        string sqlEditar = "update uf set nomeuf = @Nome, sigla = @Sigla where id = @Nome";
        string sqlApagar = "delete from uf where coduf = @Id";
        string sqlTodos = "select * from uf order by nomeuf"; //retorna todos os sexos
        string sqlBuscarId = "select * from uf where coduf = @Id";
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
                    MessageBox.Show("Estado deletado com Sucesso!!!\n" +
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
        public List<Uf> carregaDados()
        {
            List<Uf> lista_uf = new List<Uf>();

            ConectaBanco cb = new ConectaBanco();
            con = cb.conectaSqlServer();
            cmd = new SqlCommand(sqlTodos, con);

            cmd.CommandType = CommandType.Text;

            SqlDataReader tabUf; //Representa uma Tabela Virtual para a leitura de dados
            con.Open();


            try
            {
                tabUf = cmd.ExecuteReader();
                while (tabUf.Read())
                {
                    Uf aux = new Uf();

                    aux.Coduf = Int32.Parse(tabUf["coduf"].ToString());
                    aux.Nomeuf = tabUf["nomeuf"].ToString();
                    aux.Sigla = tabUf["sigla"].ToString();

                    lista_uf.Add(aux);
                }
            }
            catch (Exception ex)
            {

            }
            finally { con.Close(); }

            return lista_uf;
        }

        public void insereDados(object obj)
        {
            Uf uf = new Uf();
            uf = (Uf)obj;

            ConectaBanco cb = new ConectaBanco();
            con = cb.conectaSqlServer();
            cmd = new SqlCommand(sqlInsere, con);

            cmd.Parameters.AddWithValue("@Nome", uf.Nomeuf);
            cmd.Parameters.AddWithValue("@Sigla", uf.Sigla);
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

