using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// Version 1.1
// 19 November 2022
// https://github.com/adriancs2/BBCode.net
//
// Author: adriancs
//
// ---------
//  Example
// ---------
// string Fields = "{videocode};{width};{height}";
// string InputSyntax = "[youtube,width={width},height={height}]{videocode}[/youtube]";
// string HtmlSyntax = "<iframe width=\"{width}\" height=\"{height}\" src=\"http://www.youtube.com/embed/{videocode}\" frameborder=\"0\" allowfullscreen></iframe>";
// string Input = "[youtube,width=230,height=340]JiosPxt_5kw[/youtube]";
//
// CreateCustomHtml(Input, InputSyntax, HtmlSyntax, Fields);
//
// ------
//  Note
// -------
// 1. "<" in value will be replace by "&lt;" to avoid Html Injection

namespace BBCode
{
    public class BBCode
    {
        static string _tempValueStr = "^````````^";
        static string _regexValue = ".+?";

        public static string ConvertToHtml(string input, string inputSyntax, string htmlSyntax, string fields)
        {
            return ConvertToHtml(input, inputSyntax, htmlSyntax, fields, false);
        }

        public static string ConvertToHtml(string input, string inputSyntax, string htmlSyntax, string fields, bool allowHtmlScriptTagsAsValue)
        {
            string[] _fields = fields.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < _fields.Length; i++)
            {
                _fields[i] = _fields[i].Trim().ToLower();
            }

            string tempInputSyntax = inputSyntax;
            foreach (string field in _fields)
            {
                tempInputSyntax = tempInputSyntax.Replace(field, _tempValueStr);
            }
            tempInputSyntax = EsceapeForRegex(tempInputSyntax);
            tempInputSyntax = tempInputSyntax.Replace(_tempValueStr, _regexValue);
            MatchCollection mc = Regex.Matches(input, tempInputSyntax, RegexOptions.IgnoreCase);

            foreach (Match m in mc)
            {
                string customInsertPart = m.Value;
                string html = BuildHtml(customInsertPart, inputSyntax, htmlSyntax, _fields, allowHtmlScriptTagsAsValue);
                input = input.Replace(customInsertPart, html);
            }

            input = input.Replace("\r\n", "<br />");
            input = input.Replace("\r", "<br />");
            input = input.Replace("\n", "<br />");

            return input;
        }

        static string BuildHtml(string extractedBBCodeInput, string inputSyntax, string htmlSyntax, string[] _fields, bool allowHtmlScriptTagsAsValue)
        {
            string customPart = extractedBBCodeInput;

            #region Dictionaries

            // Store the index of fields
            Dictionary<int, string> _idxFields = new Dictionary<int, string>();

            // Store the index of values
            Dictionary<int, string> _idxValues = new Dictionary<int, string>();

            // Store non field index & length from OriInputText
            Dictionary<int, int> _idxNonFieldLength = new Dictionary<int, int>();
            #endregion

            #region Study the structure of BBCode, Retrieve block index of non-Field and Field
            string oriInputSyntax = inputSyntax;

            // Replace all fields with temporary string
            foreach (string a in _fields)
            {
                oriInputSyntax = oriInputSyntax.Replace(a, _tempValueStr);
            }

            // Get non Field Text Array
            string[] nonFieldArray = oriInputSyntax.Split(new string[] { _tempValueStr }, StringSplitOptions.RemoveEmptyEntries);

            // Get non Field Text's Block Length
            for (int i = 0; i < nonFieldArray.Length; i++)
            {
                _idxNonFieldLength[i] = nonFieldArray[i].Length;
            }

            // Get Field index
            for (int i = 0; i < nonFieldArray.Length; i++)
            {
                // Remove non Field Block
                inputSyntax = inputSyntax.Substring(nonFieldArray[i].Length, inputSyntax.Length - nonFieldArray[i].Length);

                // Get Field index
                foreach (string s in _fields)
                {
                    if (inputSyntax.Length < s.Length)
                        break;

                    // Calculate the Field's Length
                    string b = inputSyntax.Substring(0, s.Length);

                    // Check, if the current field's name
                    // If match
                    if (b == s)
                    {
                        // Add the field and index into dictionary
                        _idxFields[i] = b;

                        // Remove field from inputSyntax
                        inputSyntax = inputSyntax.Substring(s.Length, inputSyntax.Length - s.Length);
                        break;
                    }
                }
            }
            #endregion

            #region Extract Values
            // Remove Non Field Text
            for (int i = 0; i < nonFieldArray.Length; i++)
            {
                // Remove non field block
                customPart = customPart.Substring(nonFieldArray[i].Length, customPart.Length - nonFieldArray[i].Length);

                // Current non-field block is the last block
                // Terminate the loop.
                // No more value block should exist after last block
                if (i + 1 >= nonFieldArray.Length)
                    break;

                // Detect next non-field block and calculate value length
                int v = customPart.IndexOf(nonFieldArray[i + 1]);

                // Get the index and value into dictionary
                _idxValues[i] = customPart.Substring(0, v);

                // Remove the added value from input text
                customPart = customPart.Substring(v, customPart.Length - v);
            }
            #endregion

            #region Prevent Html/Script Injection
            // Avoid Html/Script Injection, Abondon Html Conversion if detected
            if (!allowHtmlScriptTagsAsValue)
            {
                foreach (KeyValuePair<int, string> kv in _idxValues)
                {
                    bool portentialScriptExists = false;
                    if (kv.Value.Contains("<") || kv.Value.Contains("&lt;"))
                    {
                        _idxValues[kv.Key] = "";
                        portentialScriptExists = true; ;
                    }
                    if (portentialScriptExists)
                    {
                        StringBuilder sb = new StringBuilder();
                        for (int n = 0; n < nonFieldArray.Length; n++)
                        {
                            sb.Append(nonFieldArray[n]);
                            if (_idxFields.ContainsKey(n))
                                sb.Append(_idxValues[n]);
                        }
                        return sb.ToString();
                    }
                }
            }
            #endregion

            #region Fill the Values into HtmlSyntax
            // Store the value to the field
            foreach (KeyValuePair<int, string> kv in _idxFields)
            {
                // Replace the field in html with value
                htmlSyntax = htmlSyntax.Replace(kv.Value, _idxValues[kv.Key].Replace("<", "&lt;"));
            }
            #endregion

            return htmlSyntax;
        }

        static string EsceapeForRegex(string inputSyntax)
        {
            return inputSyntax.Replace("\\", "\\\\")
                    .Replace(".", "\\.")
                    .Replace("{", "\\{")
                    .Replace("}", "\\}")
                    .Replace("[", "\\[")
                    .Replace("]", "\\]")
                    .Replace("+", "\\+")
                    .Replace("$", "\\$")
                    .Replace(" ", "\\s")
                    .Replace("#", "[0-9]")
                    .Replace("?", ".")
                    .Replace("*", "\\w*")
                    .Replace("%", ".*");
        }

        public static string AllowTags(string html, string allowedTags)
        {
            if (allowedTags == null || allowedTags.Trim() == "")
                return html;

            string[] sa = allowedTags.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            html = html.Replace("<", "&lt;");
            foreach (string s in sa)
            {
                html = Regex.Replace(html, "&lt;" + s, "<" + s, RegexOptions.IgnoreCase);
                html = Regex.Replace(html, "&lt;/" + s, "</" + s, RegexOptions.IgnoreCase);
            }
            return html;
        }

        public static string BlockTags(string html, string blockedTags)
        {
            string[] sa = blockedTags.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in sa)
            {
                html = Regex.Replace(html, "</\\W{0,}" + s, "&lt;/" + s, RegexOptions.IgnoreCase);
                html = Regex.Replace(html, "<\\W{0,}" + s, "&lt;" + s, RegexOptions.IgnoreCase);
            }
            return html;
        }
    }
}
