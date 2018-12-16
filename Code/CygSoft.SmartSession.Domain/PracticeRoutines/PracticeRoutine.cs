using CygSoft.SmartSession.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Domain.PracticeRoutines
{
    public class PracticeRoutine : Entity
    {
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Title { get; set; }
    }
}
