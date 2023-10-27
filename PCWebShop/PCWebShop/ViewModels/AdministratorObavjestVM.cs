using PCWebShop.Core.Infrastructure.Enums;
using PCWebShop.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PCWebShop.ViewModels
{
    public class AdministratorObavjestVM
    {
        public int ID { get; set; }

        [ForeignKey(nameof(AdministratorId))]
        public Administrator Administrator { get; set; }
        public int AdministratorId { get; set; }
        public string Content { get; set; }
        public bool Read { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DateRead { get; set; }
        public TipObavjesti TipObavjesti { get; set; }
        public DateTime? SendOnDate { get; set; }
        public DateTime? Seen { get; set; }
    }
}
