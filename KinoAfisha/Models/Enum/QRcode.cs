using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinoAfisha.Models
{
    public enum QRcode
    { 
        [Display(Name = "Требуется наличие QR кода")]
        QRcodeYes = 1,
		
		[Display(Name = "QR код не требуется")]
        QRcodeNo = 2,

       
    }
}