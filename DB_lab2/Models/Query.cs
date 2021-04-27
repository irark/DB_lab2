using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DB_lab2.Models
{
    public class Query
    {
        public string QueryId { get; set; }

        public string Error { get; set; }
        public int ErrorFlag { get; set; }
        public string ActorName { get; set; }

        public string GanreName { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Рік")]
        [Range(1700, 2021, ErrorMessage = "Недопустимий рік")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Range(0, 2021, ErrorMessage = "Недопустиме значення")]
        public int CountOfGanres { get; set; } 
        public string FilmName { get; set; }

        public List<string> FilmsNames { get; set; }
        public List<string> CategoriesNames { get; set; }
        public List<string> ActorsNames { get; set; }
        public List<string> GanresNames { get; set; }
        public List<int> FilmsYears { get; set; }
        public List<string> FilmsDescription { get; set; }

    }
}
