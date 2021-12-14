using System.ComponentModel.DataAnnotations;

namespace KinoAfisha.Models
{
    public enum ImportDisciplineRowLogType
    {
        [Display(Name = "Успешно")]
        Success = 1,

        [Display(Name = "Ошибка при парсинге строки")]
        ErrorParsed = 2,
    }
}