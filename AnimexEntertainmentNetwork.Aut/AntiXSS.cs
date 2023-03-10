using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Util;

namespace AnimexEntertainmentNetwork.Aut
{
    public class AntiXSS : HttpEncoder
    {
        public AntiXSS()
        {

        }

        protected override void HtmlEncode(string value, TextWriter output)
        {
            base.HtmlEncode(value, output);
        }
        protected override void HtmlAttributeEncode(string value, TextWriter output)
        {
            base.HtmlAttributeEncode(value, output);
        }
        protected override void HeaderNameValueEncode(string headerName, string headerValue, out string encodedHeaderName, out string encodedHeaderValue)
        {
            base.HeaderNameValueEncode(headerName, headerValue, out encodedHeaderName, out encodedHeaderValue);
        }
        protected override void HtmlDecode(string value, System.IO.TextWriter output)
        {
            base.HtmlDecode(value, output);
        }

        protected override byte[] UrlEncode(byte[] bytes, int offset, int count)
        {
            return base.UrlEncode(bytes, offset, count);
        }

        protected override string JavaScriptStringEncode(string value)
        {
            return base.JavaScriptStringEncode(value);
        }

        protected override string UrlPathEncode(string value)
        {
            return base.UrlPathEncode(value);
        }



    }
}
