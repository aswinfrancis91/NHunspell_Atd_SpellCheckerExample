using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using NHunspell;

namespace Nhunspell_Checker.Models
{
    public class SpellChecker
    {
        public string EnglishSynopsis { get; set; }

        public string SpanishSynopsis { get; set; }

        public string Language { get; set; }

        internal string CheckEnglishSpelling()
        {
            bool correct;
            List<string> suggestions = new List<string>();
            //Generate XML for the AtD plugin to read.
            StringWriter stringwriter = new StringWriter();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            XmlWriter xmlWriter = XmlWriter.Create(stringwriter, settings);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("results");
            string lastWord = string.Empty;
            //We create hunspell object and pass the location of the english dictionary.
            using (Hunspell hunspell = new Hunspell(System.Web.HttpContext.Current.Server.MapPath("~/Content/Dictionaries/en_US.aff"),
    System.Web.HttpContext.Current.Server.MapPath("~/Content/Dictionaries/en_US.dic")))
            {
                //Split the paragraph to words
                List<string> words = Regex.Split(EnglishSynopsis, @"\W+").ToList();
                foreach (string word in words)
                {
                    //Check the spelling and returns true or false
                    correct = hunspell.Spell(word);

                    if (!correct)
                    {
                        xmlWriter.WriteStartElement("error");
                        xmlWriter.WriteElementString("string", word);
                        xmlWriter.WriteElementString("description", "Spelling");
                        xmlWriter.WriteElementString("precontext", lastWord);
                        xmlWriter.WriteStartElement("suggestions");
                        //Returns list of suggestion for the incorrect word
                        suggestions = hunspell.Suggest(word);
                        foreach (string suggestion in suggestions)
                        {
                            xmlWriter.WriteElementString("option", suggestion);
                        }
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteElementString("type", "spelling");
                        xmlWriter.WriteEndElement();
                    }
                    lastWord = word;
                }
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            return stringwriter.ToString();
        }

        internal string CheckSpanishSpelling()
        {
            bool correct;
            List<string> suggestions = new List<string>();
            //Generate XML for the AtD plugin to read.
            StringWriter stringwriter = new StringWriter();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            XmlWriter xmlWriter = XmlWriter.Create(stringwriter, settings);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("results");
            string lastWord = string.Empty;
            //We create hunspell object and pass the location of the spanish dictionary.
            using (Hunspell hunspell = new Hunspell(System.Web.HttpContext.Current.Server.MapPath("~/Content/Dictionaries/es_ES.aff"),
   System.Web.HttpContext.Current.Server.MapPath("~/Content/Dictionaries/es_ES.dic")))
            {
                //Split the paragraph to words
                List<string> words = Regex.Split(SpanishSynopsis, @"\W+").ToList();
                foreach (string word in words)
                {
                    correct = hunspell.Spell(word);

                    if (!correct)
                    {
                        xmlWriter.WriteStartElement("error");
                        xmlWriter.WriteElementString("string", word);
                        xmlWriter.WriteElementString("description", "Spelling");
                        xmlWriter.WriteElementString("precontext", lastWord);
                        xmlWriter.WriteStartElement("suggestions");
                        //Returns list of suggestion for the incorrect word
                        suggestions = hunspell.Suggest(word);
                        foreach (string suggestion in suggestions)
                        {
                            xmlWriter.WriteElementString("option", suggestion);
                        }
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteElementString("type", "spelling");
                        xmlWriter.WriteEndElement();
                    }
                    lastWord = word;
                }
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            return stringwriter.ToString();
        }
    }
}