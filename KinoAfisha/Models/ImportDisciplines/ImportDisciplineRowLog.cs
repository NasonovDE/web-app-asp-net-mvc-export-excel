
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.Web.Mvc;

namespace KinoAfisha.Models
{
    public class ImportDisciplineRowLog
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public ImportDisciplineRowLogType Type { get; set; }
    }
}
//E:\СОХРАНИТЬ\КнАГУ\Кожин\ИТ\web-app-asp-net-mvc-export-excel\web-app-asp-net-mvc-export-excel\KinoAfisha\Models\Xlsx\