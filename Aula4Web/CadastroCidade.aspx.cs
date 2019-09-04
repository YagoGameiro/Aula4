using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Aula4Web
{
    public partial class CadastroCidade : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                loadDDL();
                loadGrid();
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            int idEstado = int.Parse(txtEstado.SelectedValue.ToString());

            BDAula4Entities context = new BDAula4Entities();

            Cidade cidade = new Cidade()
            {
                descricao = txtDescricao.Text,
                Estado = context.Estado.First<Estado>(v => v.id == idEstado)
            };

            context.Cidade.Add(cidade);
            context.SaveChanges();
            loadGrid();
            sendMessage("Registro inserido");
        }

        private void loadGrid()
        {
            BDAula4Entities context = new BDAula4Entities();

            var dados = (from c in context.Cidade
                         from e in context.Estado.Where(x => x.id == c.idEstado)
                         select new
                         {
                          IdCidade = c.id,
                          NomeCidade = c.descricao,
                          Estado = e.descricao
                         }).ToList();

            gvCidades.DataSource = dados;
            gvCidades.DataBind();
        }

        private void sendMessage(string txt)
        {
            lblMsg.Text = txt;
        }

        private void loadDDL()
        {
            txtEstado.DataSource = new BDAula4Entities().Estado.ToList<Estado>();
            txtEstado.DataTextField = "Descricao";
            txtEstado.DataValueField = "Id";
            txtEstado.DataBind();
        }
    }
}