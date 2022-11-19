using System;
using System.Collections.Generic;
using System.Linq;
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
                txtInput.Text = @"<h1>A Video Tutorial About Freezing HTML Table Headers</h1>
[youtube,560,315]_dpSEjaKqSE[/youtube]
<h2>Here's Another Cool Video About Learning Basic HTML</h2>
[youtube,560,315]PlxWf493en4[/youtube]";
                txtFields.Text = "{videocode};{width};{height}";
                txtInputSyntax.Text = "[youtube,{width},{height}]{videocode}[/youtube]";
                //txtHtmlSyntax.Text = "<object width=\"{width}\" height=\"{height}\"><param name=\"movie\" value=\"http://www.youtube.com/v/{videocode}?hl=en_US&amp;version=3\"></param><param name=\"allowFullScreen\" value=\"true\"></param><param name=\"allowscriptaccess\" value=\"always\"></param><embed src=\"http://www.youtube.com/v/{videocode}?hl=en_US&amp;version=3\" type=\"application/x-shockwave-flash\" width=\"{width}\" height=\"{height}\" allowscriptaccess=\"always\" allowfullscreen=\"true\"></embed></object>";

                txtHtmlSyntax.Text = "<iframe width=\"{width}\" height=\"{height}\" src=\"https://www.youtube.com/embed/{videocode}\" title=\"YouTube video player\" frameborder=\"0\" allow=\"accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture\" allowfullscreen></iframe>";

                txtTagAllow.Text = "p;br;hr;strong;b;u;em;h1;h2;h3;h4;h5;h6;span;body;style;table;tr;td;img;a";
                txtTagBlock.Text = "iframe;script;form;object;h1;h2";
            }
        }

        protected void btSubmitAllowTag_Click(object sender, EventArgs e)
        {
            string output = txtInput.Text;
            output = BBCode.AllowTags(output, txtTagAllow.Text);
            output = BBCode.ConvertToHtml(output, txtInputSyntax.Text, txtHtmlSyntax.Text, txtFields.Text);

            phText.Controls.Add(new LiteralControl(Server.HtmlEncode(output)));
            phHtml.Controls.Add(new LiteralControl(output));
        }

        protected void btSubmitBlockTag_Click(object sender, EventArgs e)
        {
            string output = txtInput.Text;
            output = BBCode.BlockTags(output, txtTagBlock.Text);
            output = BBCode.ConvertToHtml(output, txtInputSyntax.Text, txtHtmlSyntax.Text, txtFields.Text);

            phText.Controls.Add(new LiteralControl(Server.HtmlEncode(output)));
            phHtml.Controls.Add(new LiteralControl(output));
        }
    }
}