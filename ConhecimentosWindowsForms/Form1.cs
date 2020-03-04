using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConhecimentosWindowsForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Sexo_Click(object sender, EventArgs e)
        {

        }

        private void Cadastrar_Click(object sender, EventArgs e)
        {
            //Limpar box para novo cadastro
            lstSaida.Items.Clear();

            //Converter a data e fazer o calculo da idade
            DateTime data = Convert.ToDateTime(dateNascimento.Value);
            int anos = DateTime.Today.Year - data.Year;

            string sexo = "", nome = textNome.Text, sobrenome = txtSobrenome.Text, estado = txtCidade.Text, cidade = txtEstado.Text;

            //Atribuindo o sexo
            if (btnFeminino.Checked)
            {

                sexo = "Feminino";
            }
            else
            {
                if (btnMasculino.Checked)
                {

                    sexo = "Masculino";
                }


            }

            //Validando os campos em branco 
            if (nome == "" || sobrenome == "" || estado == "" || cidade == "" || sexo == "")
            {

                if (nome == "")
                {
                    lstSaida.Items.Add("Por favor preencher o campo 'Nome'");

                }

                if (sobrenome == "")
                {
                    lstSaida.Items.Add("Por favor preencher o campo 'Sobrenome'");

                }

                if (estado == "")
                {
                    lstSaida.Items.Add("Por favor preencher o campo 'Estado'");

                }

                if (cidade == "")
                {
                    lstSaida.Items.Add("Por favor preencher o campo 'Cidade'");

                }

                if (sexo == "")
                {
                    lstSaida.Items.Add("Por favor preencher o campo 'Sexo'");

                }

            }

            //Se tudo estiver correto mostrar o cadastro
            else
            {
                lstSaida.Items.Add("Você realizou o cadastro de:");
                lstSaida.Items.Add($"Nome: {nome} {sobrenome}");
                lstSaida.Items.Add($"Mora em: {cidade}, {estado}");
                lstSaida.Items.Add($"Idade: {anos} anos");
                lstSaida.Items.Add($"Sexo: {sexo} ");

                //Banco de dados 
                //Conexao SQL
                SqlConnection con = new SqlConnection("Data Source = ; Initial Catalog = ; Integrated Security = True");

                //Insert SQL
                string sql = $"Insert into Cadastro(Nome,Sobrenome,Cidade,Estado,Idade,Sexo) values(@nome,@sobrenome,@cidade,@estado,@idade,@sexo)";


                try
                {
                    //Criando o objeto para passar a string e fazer a conexão
                    SqlCommand entrada = new SqlCommand(sql, con);

                    //Enviando os parametros
                    entrada.Parameters.Add(new SqlParameter("@nome", nome));
                    entrada.Parameters.Add(new SqlParameter("@sobrenome", sobrenome));
                    entrada.Parameters.Add(new SqlParameter("@cidade", cidade));
                    entrada.Parameters.Add(new SqlParameter("@estado", estado));
                    entrada.Parameters.Add(new SqlParameter("@idade", anos));
                    entrada.Parameters.Add(new SqlParameter("@sexo", sexo));

                    //abrindo a conexão
                    con.Open();

                    //Executar comando 
                    entrada.ExecuteNonQuery();

                    //Fechando a conexão
                    con.Close();

                    lstSaida.Items.Add("Usuário cadastrado no banco");
                }
                catch (SqlException ex)
                {
                    lstSaida.Items.Add(ex);
                }
                finally
                {
                    con.Close();
                }

                //Limpando o formulário para novo cadastro
                btnFeminino.Checked = false;
                btnMasculino.Checked = false;
                textNome.Text = "";
                txtEstado.Text = "";
                txtCidade.Text = "";
                txtSobrenome.Text = "";
                dateNascimento.Value = DateTime.Today;
            }

        }
    }
}
