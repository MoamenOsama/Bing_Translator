using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Bing_Translator
{
    public class AdmAccessToken
    {

        public string access_token { get; set; }

        public string token_type { get; set; }

        public string expires_in { get; set; }

        public string scope { get; set; }
    }

    static class Translator
    {
        static public string headerValue;

        public static void SetAuthorization(string clientID, string clientSecret,bool cachedtoken)
        {
            if (cachedtoken)
            {
                if (File.Exists("headerValue.txt"))
                {
                    StreamReader sr = new StreamReader("headerValue.txt");
                    headerValue = sr.ReadToEnd();
                    sr.Close();
                    return;
                }
            }
            String strTranslatorAccessURI = "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";
            String strRequestDetails = string.Format("grant_type=client_credentials&client_id={0}&client_secret={1}&scope=http://api.microsofttranslator.com", HttpUtility.UrlEncode(clientID), HttpUtility.UrlEncode(clientSecret));

            System.Net.WebRequest webRequest = System.Net.WebRequest.Create(strTranslatorAccessURI);
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Method = "POST";

            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(strRequestDetails);
            webRequest.ContentLength = bytes.Length;
            using (System.IO.Stream outputStream = webRequest.GetRequestStream())
            {
                outputStream.Write(bytes, 0, bytes.Length);
            }
            System.Net.WebResponse webResponse = webRequest.GetResponse();

            System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(AdmAccessToken));
            //Get deserialized object from JSON stream 
            AdmAccessToken token = (AdmAccessToken)serializer.ReadObject(webResponse.GetResponseStream());

            headerValue = "Bearer " + token.access_token;
            if (cachedtoken)
            {
                StreamWriter sw = new StreamWriter("headerValue.txt");
                sw.WriteLine(headerValue);
                sw.Close();
            }
        }

        public static string Translate(string Text,Languages fromLang,Languages toLang)
        {
            string txtToTranslate = Text;
            string uri = "http://api.microsofttranslator.com/v2/Http.svc/Translate?text=" + System.Web.HttpUtility.UrlEncode(txtToTranslate) + "&from=" + fromLang + "&to=" + toLang;
            System.Net.WebRequest translationWebRequest = System.Net.WebRequest.Create(uri);
            translationWebRequest.Headers.Add("Authorization", headerValue);
            System.Net.WebResponse response = null;
            response = translationWebRequest.GetResponse();
            System.IO.Stream stream = response.GetResponseStream();
            System.Text.Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            System.IO.StreamReader translatedStream = new System.IO.StreamReader(stream, encode);
            System.Xml.XmlDocument xTranslation = new System.Xml.XmlDocument();
            xTranslation.LoadXml(translatedStream.ReadToEnd());
            return xTranslation.InnerText;
        }
    }
    public enum Languages
	{
		en,
        ar,
        es
	}
}
