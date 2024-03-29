﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DB_lab2
{
    public partial class Film
    {
        public Film()
        {
            FilmActorRelationships = new HashSet<FilmActorRelationship>();
            FilmGanreRelationships = new HashSet<FilmGanreRelationship>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Назва")]
        public string Name { get; set; }
        [Display(Name = "Категорія")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Рік")]
        [Range(1700, 2021, ErrorMessage = "Недопустимий рік")]
        public int Year { get; set; }
        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Опис")]
        public string Info { get; set; }
        [Display(Name = "Категорія")]
        public virtual Category Category { get; set; }
        [Display(Name = "Актори")]
        public virtual ICollection<FilmActorRelationship> FilmActorRelationships { get; set; }
        [Display(Name = "Жанри")]
        public virtual ICollection<FilmGanreRelationship> FilmGanreRelationships { get; set; }

        public virtual ICollection<FilmUserRelationship> FilmUserRelationships { get; set; }


    }
}
