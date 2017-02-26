using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodebustersAppWMU2.Models
{
    class TaskResponsibilityDto
    {
        public int TaskId { get; set; }

        public string BeginDateTime { get; set; }

        public string DeadlineDateTime { get; set; }

        public string Title { get; set; }

        public string Requirements { get; set; }

        public String Assigned { get; set; } = "Not Assigned";

    }
}
