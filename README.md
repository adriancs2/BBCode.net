# BBCode.NET for ASP.NET - Convert BBCode to HTML

![](https://raw.githubusercontent.com/adriancs2/BBCode.net/master/wiki/02.png)

## How to Use The Code

Take the following as sample input data:

```
string input = @"
[color=red][size=20]A Video Tutorial About Freezing HTML Table Headers[/size][/color]
[font=brush script mt,30]Changing the font[/font]
[b]Bolding the text[/b]
[i]Italic the text[/i]
[url]https://adriancs.com[/url]
[url=https://adriancs.com]Visit the author's Website[/url]
[img]https://adriancs.com/wp-content/uploads/2021/12/html-table-freeze-column-and-row2-768x549.png[/img]
[youtube,560,315]_dpSEjaKqSE[/youtube]";
```
Convert the BBCode text into html with basic built-in rules:
```
string output = BBCode.ConvertToHtml(input);
```
Design And Add Your Own BBCode:
```
List<BBCodeRules> rules = BBCode.BasicRules;
string bbcode_syntax = "[size={d1}]{text}[/size]";
string html_syntax = "<span style='font-size: {d1}pt;'>{text}</span>";
string fields = "{d1};{text}";
rules.Add(new BBCodeRules(bbcode_syntax, html_syntax, fields));
string output = BBCode.ConvertToHtml(input, rules);
```
## The Order of Same BBCode

If you have 2 same BBCode rules, but different parameters, for example:
```
[font={d1},{d2}]{text}[/font]
[font={d1}]{text}[/font]
```
The more complex rule must be added first, then follow by less complex rule.

For example, this is correct order:
```
[font={d1},{d2}]{text}[/font]  // this must come first
[font={d1}]{text}[/font]
```
and this is wrong order:
```
[font={d1}]{text}[/font]
[font={d1},{d2}]{text}[/font]  // this must come first
```

## Default Built-in BBCode Rules:

```
[b]{text}[/b]
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
```
