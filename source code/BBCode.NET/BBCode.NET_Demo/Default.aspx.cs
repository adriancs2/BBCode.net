using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BBCode.NET_Demo
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtInput.Text = @"[color=red][size=20]A Video Tutorial About Freezing HTML Table Headers[/size][/color]
[font=brush script mt,30]Changing the font[/font]
[b]Bolding the text[/b]
[i]Italic the text[/i]
[url]https://adriancs.com[/url]
[url=https://adriancs.com]Visit the author's Website[/url]
[img]https://adriancs.com/wp-content/uploads/2021/12/html-table-freeze-column-and-row2-768x549.png[/img]
[youtube,560,315]_dpSEjaKqSE[/youtube]";

                txtTagAllow.Text = "p;br;hr;strong;b;u;em;h1;h2;h3;h4;h5;h6;span;body;style;table;tr;td;img;a";
                txtTagBlock.Text = "iframe;script;form;object;h1;h2";
            }
        }

        protected void btSubmitAllowTag_Click(object sender, EventArgs e)
        {
            string output = txtInput.Text;
            output = BBCode.AllowTags(output, txtTagAllow.Text);

            List<BBCodeRules> rules = BBCode.BasicRules;
            if(txtInputSyntax.Text.Trim().Length>0)
            {
                rules.Add(new BBCodeRules(txtInputSyntax.Text, txtHtmlSyntax.Text, txtFields.Text));
            }
            output = BBCode.ConvertToHtml(output, rules);

            phText.Controls.Add(new LiteralControl(Server.HtmlEncode(output)));
            phHtml.Controls.Add(new LiteralControl(output));
        }

        protected void btSubmitBlockTag_Click(object sender, EventArgs e)
        {
            string output = txtInput.Text;
            output = BBCode.BlockTags(output, txtTagBlock.Text);

            List<BBCodeRules> rules = BBCode.BasicRules;
            if (txtInputSyntax.Text.Trim().Length > 0)
            {
                rules.Add(new BBCodeRules(txtInputSyntax.Text, txtHtmlSyntax.Text, txtFields.Text));
            }
            output = BBCode.ConvertToHtml(output, rules);

            phText.Controls.Add(new LiteralControl(Server.HtmlEncode(output)));
            phHtml.Controls.Add(new LiteralControl(output));
        }
    }
}