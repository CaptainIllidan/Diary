using Diary.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diary.Models.Diary
{
    public abstract class DiaryRecordFinishable: DiaryRecord
    {
        [Display(Name = "Конец")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)]

        [Required]
        public DateTime EndDateTime { get; set; }
    }
}