using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor.Infrastructure.Entities
{

    public partial class Audit 
    {
        [NotMapped]
        public List<TableColumns> ListOldValues { get; set; } = new List<TableColumns>();

        [NotMapped]
        public List<TableColumns> ListNewValues { get; set; } = new List<TableColumns>();
    }

    public class TableColumns
    {
        public string Column { get; set; }

        private object data { get; set; }
        public object Value
        {
            set { data = value; }
            get
            {
                DateTime dateValue;
                if (data != null && DateTime.TryParse(data.ToString(), out dateValue))
                {
                    return dateValue.ToString("dd/MM/yyyy HH:mm:ss");
                }
                return data;
            }
        }
    }
 }
