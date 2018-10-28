using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diary.Models.Diary
{
    public class Meeting:DiaryRecordFinishable
    {
        public override DiaryRecordType DiaryRecordType { get { return DiaryRecordType.Meeting; } }

        [Display(Name = "Место")]
        [Required, MinLength(4), MaxLength(50)]
        public string Place { get; set; }
    }
}