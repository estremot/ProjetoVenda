using Aula_11_08;
using Projeto_Venda_2023.controller;
using Projeto_Venda_2023.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_Venda_2023
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
            carregaSexo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmCadastroCidades frc = new frmCadastroCidades();
            frc.ShowDialog();
        }

        public void carregaSexo()
        {
            C_Sexo cs = new C_Sexo();

            List<Sexo> aux = new List<Sexo>();

            aux = cs.carregaDados();

            comboBox1.DataSource = aux;
            comboBox1.DisplayMember = "nomesexo";
            comboBox1.ValueMember = "codsexo";
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void cidadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadastroCidades cadcidade = new frmCadastroCidades();
            cadcidade.ShowDialog();
        }
    }
}
