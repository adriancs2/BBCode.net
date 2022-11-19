<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BBCode.NET_Demo.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BBCode.NET for ASP.NET - Convert BBCode to HTML</title>

    <style type="text/css">
        body, td {
            font-family: Calibri, Arial, Helvetica, sans-serif;
            font-size: 12pt;
            line-height: 180%;
        }

        h1 {
            margin: 0;
        }

        h2 {
            margin: 0;
        }

        .maintb {
            table-layout: fixed;
            width: calc(100vw - 60px);
            border-collapse: collapse;
        }

            .maintb td {
                vertical-align: top;
            }

                .maintb td:first-child {
                    border-right: 1px solid #bdbdbd;
                    width: 600px;
                }

                .maintb td:last-child {
                    padding-left: 10px;
                }

        textarea, input[type=text] {
            font-family: "Cascadia Mono", Consolas, Courier New;
            font-size: 10.5pt;
            color: #525252;
            width: 95%;
            padding: 2px 5px;
            margin-bottom: 5px;
            background: #fff9d3;
            border: 2px solid #9b9b9b;
        }

        .code {
            font-family: "Cascadia Mono", Consolas, Courier New;
            font-size: 10.5pt;
            color: #525252;
            line-height: 150%;
        }

        textarea {
            height: 210px;
            line-height: 150%;
        }

        .lable {
            display: inline-block;
            width: 120px;
        }
    </style>
</head>
<body>

    <h1>BBCode.NET for ASP.NET - Convert BBCode to HTML</h1>

    Project Website: <a href="https://github.com/adriancs2/BBCode.net">https://github.com/adriancs2/BBCode.net</a>

    <form id="form1" runat="server">

        <table class="maintb">
            <tr>
                <td>Enter Text Here with BBCode and Press the Button: [<b>Convert to Html</b>]<br />
                    <asp:TextBox ID="txtInput" TextMode="MultiLine" runat="server" ValidateRequestMode="Disabled" spellcheck="false"></asp:TextBox>

                    <asp:Button ID="btSubmitAllowTag" runat="server" Text="Covert to Html (White List Tags)" OnClick="btSubmitAllowTag_Click" />
                    Allowed Html Tags:<br />

                    <asp:TextBox ID="txtTagAllow" runat="server" spellcheck="false"></asp:TextBox>

                    <asp:Button ID="btSubmitBlockTag" runat="server" Text="Covert to Html (Black List Tags)" OnClick="btSubmitBlockTag_Click" />
                    Blocked Html Tags:<br />

                    <asp:TextBox ID="txtTagBlock" runat="server" spellcheck="false"></asp:TextBox>

                    <br />
                    <br />

                    <h2>Design Your Own BBCode</h2>

                    <span class="lable">BBCode Syntax</span>:
                    <asp:TextBox ID="txtInputSyntax" runat="server" spellcheck="false" Width="300px"></asp:TextBox><br />

                    <span class="lable">HTML Syntax</span>:
                    <asp:TextBox ID="txtHtmlSyntax" runat="server" ValidateRequestMode="Disabled" spellcheck="false" Width="300px"></asp:TextBox><br />

                    <span class="lable">Fields</span>:
                    <asp:TextBox ID="txtFields" runat="server" spellcheck="false" Width="300px"></asp:TextBox>

                    <br />
                    <br />

                    <h2>Example of BBCode Designing</h2>

                    <pre class="code">
BBCode Syntax  =  [color={colorcode}]{text}[/color]
HTML Syntax    =  &lt;span style=&quot;color: {colorcode};&quot;&gt;{text}&lt;/span&gt;
Fields         =  {colorcode};{text}
                    </pre>

                    <h2>Default Built-In BBCode Rules</h2>
                    <pre class="code">[b]{text}[/b]
Example: [b]your text here[/b]

[u]{text}[/u]
Example: [u]your text here[/u]

[i]{text}[/i]
Example: [i]your text here[/i]

[color={d1}]{text}[/color]
Example: [color=red]your text here[/color]
Example: [color=#525252]your text here[/color]

[size={d1}]{text}[/size]
Example: [size=20]your text here[/size]

[font={d1},{d2}]{text}[/font]
Example: [font=brush script mt,30]Changing the font[/font]

[font={d1}]{text}[/font]
Example: [font=brush script mt]Changing the font[/font]

[code]{text}[/code]
Example: [code]your text here[/code]

[url]{text}[/url]
Example: [url]https://adriancs.com[/url]

[url={d1}]{text}[/url]
Example: [url=https://adriancs.com]Visit the Authur's Website[/url]

[img]{text}[/img]
Example: [img]https://yourwebsite.com/someimg.jpg[/img]

[img,{width},{height}]{text}[/img]
Example: [img,300,200]https://yourwebsite.com/someimg.jpg[/img]

[youtube,{width},{height}]{videocode}[/youtube]
Example: [youtube,560,315]_dpSEjaKqSE[/youtube]

[youtube]{videocode}[/youtube]
Example: [youtube]_dpSEjaKqSE[/youtube]
                    </pre>
                </td>
                <td>
                    <h2>Generated HTML:</h2>
                    <hr />
                    <div class="code">
                        <asp:PlaceHolder ID="phText" runat="server"></asp:PlaceHolder>
                    </div>
                    <br />
                    <h2>Rendered Output</h2>
                    <hr />
                    <asp:PlaceHolder ID="phHtml" runat="server"></asp:PlaceHolder>
                </td>
            </tr>
        </table>

    </form>
</body>
</html>
