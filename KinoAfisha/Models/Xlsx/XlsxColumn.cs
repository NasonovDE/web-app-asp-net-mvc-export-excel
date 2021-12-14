using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinoAfisha.Models
{
    public class XlsxColumn
    {
        public string DisplayName { get; set; }
        public Type ColumnType { get; set; }
        public int? Order { get; set; }
    }
}