using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Aula4Web
{
    public partial class CadastroEstado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loadGrid();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            Estado estado = new Estado()
            {
                descricao = txtDescricao.Text
            };

            BDAula4Entities context = new BDAula4Entities();

            context.Estado.Add(estado);
            context.SaveChanges();
            loadGrid();
            sendMessage("Registro inserido");
        }

        private void loadGrid()
        {
            gvEstado.DataSource = new BDAula4Entities().Estado.ToList<Estado>();
            gvEstado.DataBind();
        }

        private void sendMessage(string txt)
        {
            lblMsg.Text = txt;
        }
    }
}