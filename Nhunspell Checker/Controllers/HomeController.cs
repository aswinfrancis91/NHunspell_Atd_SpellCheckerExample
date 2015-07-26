using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nhunspell_Checker.Models;

namespace Nhunspell_Checker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult SpellChecker()
        {
            SpellChecker spellCheckModel = new SpellChecker();
            return View(spellCheckModel);
        }

        public string CheckSpelling(string synopsis, string language)
        {
            SpellChecker spellCheckModel = new SpellChecker();
            spellCheckModel.Language = language;
            string xmlString = string.Empty;
            //We use language code to identify which spell checking needs to be done
            if (language == "en")
            {
                spellCheckModel.EnglishSynopsis = synopsis;
                xmlString = spellCheckModel.CheckEnglishSpelling();
            }
            else
            {
                spellCheckModel.SpanishSynopsis = synopsis;
                xmlString = spellCheckModel.CheckSpanishSpelling();
            }
            return xmlString;
        }
    }
}