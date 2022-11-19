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
                width: 50%;
                vertical-align: top;
            }

                .maintb td:first-child {
                    border-right: 1px solid #bdbdbd;
                }

                .maintb td:last-child {
                    padding-left: 10px;
                }

        textarea, input[type=text] {
            font-family: "Cascadia Mono", Consolas, Courier New;
            font-size: 10.5pt;
            color: #525252;
            width: 96%;
            padding: 2px 5px;
            margin-bottom: 5px;
        }

        .code {
            font-family: "Cascadia Mono", Consolas, Courier New;
            font-size: 10.5pt;
            color: #525252;
            line-height: 150%;
        }

        textarea {
            height: 100px;
            line-height: 150%;
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
                    <asp:TextBox ID="txtInput" TextMode="MultiLine" runat="server" ValidateRequestMode="Disabled"></asp:TextBox>

                    <asp:Button ID="btSubmitAllowTag" runat="server" Text="Covert to Html (White List Tags)" OnClick="btSubmitAllowTag_Click" />
                    Allowed Html Tags:<br />

                    <asp:TextBox ID="txtTagAllow" runat="server"></asp:TextBox>

                    <asp:Button ID="btSubmitBlockTag" runat="server" Text="Covert to Html (Black List Tags)" OnClick="btSubmitBlockTag_Click" />
                    Blocked Html Tags:<br />

                    <asp:TextBox ID="txtTagBlock" runat="server"></asp:TextBox>

                    <br />
                    <br />

                    <h2>Demo of Defining BBCode Syntax (Design Your Own BBCode):</h2>

                    BBCode Syntax:<br />
                    <asp:TextBox ID="txtInputSyntax" runat="server"></asp:TextBox>

                    Convert to HTML:<br />
                    <asp:TextBox ID="txtHtmlSyntax" runat="server" TextMode="MultiLine" ValidateRequestMode="Disabled"></asp:TextBox>

                    Fields:<br />
                    <asp:TextBox ID="txtFields" runat="server"></asp:TextBox>
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
