using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppAspNetMvcCodeFirst.Extensions;
using WebAppAspNetMvcCodeFirst.Models.Attributes;

namespace KinoAfisha.Models
{ 
    public class Description
{
    /// <summary>
    /// Id
    /// </summary> 
    [HiddenInput(DisplayValue = false)]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Название кинокомпании", Order = 2)]
    public String Name { get; set; }

    /// <summary>
    /// Описание Фильма
    /// </summary>    
    [Required]
    [Display(Name = "Описание Кинокомпании", Order = 200)]
    [UIHint("TextArea")]
    public string DescriptionCinemaCorporation { get; set; }

        /// <summary>
        /// Актеры
        /// </summary>    
    [Required]
    [Display(Name = "Фильмы", Order = 201)]
    [UIHint("TextArea")]
    public string DescriptionAllFilms { get; set; }

    /// <summary>
    /// Основные разделы
    /// </summary>    
    [Required]
    [Display(Name = "Дополнительно", Order = 202)]
    [UIHint("TextArea")]
    public string DescriptionAllActors { get; set; }

    ///// <summary>
    ///// Ключ для создания/изменения записи
    ///// </summary>    
    //[Required]
    //[Display(Name = "Ключ для создания/изменения записи", Order = 1000)]
    //[UIHint("Password")]
    //[NotMapped]
    //public string Key { get; set; }

    ///<summary>
    /// Список 
    /// </summary> 
    [ScaffoldColumn(false)]
    public virtual ICollection<Kino> Kinos { get; set; }
}
}


