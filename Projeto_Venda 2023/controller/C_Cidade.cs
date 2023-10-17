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

    
    internal class C_Cidade : I_CRUD
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable cidades;
        string sqlInsere = "insert into cidade(nomecidade, coduf_fk) values (@nomecidade, @coduf)";
        string sqlTodosNormalizados = "select c.codcidade as Código, c.nomecidade as Cidade," +
            " u.sigla as Sigla from cidade c, " +
            "uf u where c.coduf_fk = u.coduf";
        string sqlApagar = "delete from cidade where codcidade = @Id";

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
                    MessageBox.Show("Cidade deletada com Sucesso!!!\n" +
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

        public DataTable buscarTodosNormalizados()
        {
           

            ConectaBanco conectaBanco = new ConectaBanco();
            con = conectaBanco.conectaSqlServer();



            //cria o objeto command para executar a instruçao sql
            cmd = new SqlCommand(sqlTodosNormalizados, con);
            //abre a conexao
            con.Open();
            //define o tipo do comando
            cmd.CommandType = CommandType.Text;
            //cria um dataadapter
            da = new SqlDataAdapter(cmd);
            //cria um objeto datatable
            cidades = new DataTable();
            //preenche o datatable via dataadapter
            da.Fill(cidades);
            
            return cidades;
        }
        public DataTable buscarTodos()
        {
            throw new NotImplementedException();
        }

        public void insereDados(object obj)
        {
            Cidade cidade = new Cidade();
            cidade = (Cidade)obj;

            ConectaBanco cb = new ConectaBanco();
            con = cb.conectaSqlServer();
            cmd = new SqlCommand(sqlInsere, con);

            cmd.Parameters.AddWithValue("@nomecidade", cidade.Nomecidade);
            cmd.Parameters.AddWithValue("@coduf", cidade.Uf.Coduf);
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
