using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinoAfisha.Models
{
    public class FilmCover
    {
        public int Id { get; set; }
        public IEnumerable<HttpPostedFile> Image { get; set; }
        public long Size { get; set; }
        public string Path { get; set; }
        public Guid Guid { get; set; }

        [Required]
        public byte[] Data { get; set; }

        [StringLength(100)]
        public string ContentType { get; set; }
        public DateTime? DateChanged { get; set; }
        public string FileName { get; set; }
    }
}