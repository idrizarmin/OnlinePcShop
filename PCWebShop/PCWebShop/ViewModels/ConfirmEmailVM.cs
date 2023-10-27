using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCWebShop.ViewModels
{
    public class ConfirmEmailVM
    {
       
        public string Token { get; set; }

        public string UserId { get; set; }
    }
}
