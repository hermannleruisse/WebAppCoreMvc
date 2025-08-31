using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppCoreMVC.Models
{
    public class Social
    {
        [Display(Name = "Lien twitter")]
        public string UrlTwitter { get; set; }

        [Display(Name = "Lien facebook")]
        public string UrlFacebook { get; set; }

        [Display(Name = "Lien instagram")]
        public string UrlInstagram { get; set; }

        [Display(Name = "Lien linkedin")]
        public string UrlLinkedin { get; set; }

        [Display(Name = "Lien google map")]
        public string UrlMap { get; set; }
    }
}