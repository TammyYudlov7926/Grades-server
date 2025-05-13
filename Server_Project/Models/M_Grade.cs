using GradeDO;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Server_Project.Models
{
    public class M_Grade
    {
        [BindRequired]
        [Range(1, 100)]
        public int ExeNumber { get; set; }//1 ,2 99= for the test
        [MaxLength(50)]
        public string Name { get; set; }

        public DateTime Date { get; set; }
        public int GradeNumber { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Comment { get; set; } = "defult comment";
        public Grade Convert()
        {
            Grade g = new Grade()
            {
                ExeNumber = ExeNumber,
                Name = Name,
                Date = DateTime.Now,
                Comment = Comment,
                GradeNumber = GradeNumber
            };
            return g;

        }
    }
}
