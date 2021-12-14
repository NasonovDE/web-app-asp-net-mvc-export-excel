
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.Web.Mvc;

namespace KinoAfisha.Models
{
    public class ImportDisciplineData
    {
        public string Name { get; set; }
        public string DescriptionCinemaCorporation { get; set; }
        public string DescriptionAllFilms { get; set; }
        public string DescriptionAllActors { get; set; }

        
    }
}
