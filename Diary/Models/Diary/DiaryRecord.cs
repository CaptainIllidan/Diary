using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diary.Models.Diary
{
    public enum DiaryRecordType
    {
        [Display(Name = "Встреча")]
        Meeting,
        [Display(Name = "Дело")]
        Thing,
        [Display(Name = "Памятка")]
        Note
    }

    public enum DiaryRecordPeriod
    {
        [Display(Name = "День")]
        Day,
        [Display(Name = "Неделя")]
        Week,
        [Display(Name = "Месяц")]
        Month,
        [Display(Name = "Список")]
        All
    }

    public class DiaryRecord
    { 
        public int ID { get; set; }

        [Display(Name = "Тип записи")]
        [Required, EnumDataType(typeof(DiaryRecordType))]
        public virtual DiaryRecordType DiaryRecordType { get; set; }

        [Display(Name = "Тема")]
        [Required, MinLength(4), MaxLength(50)]
        public string Theme { get; set; }

        [Display(Name = "Начало")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}",ApplyFormatInEditMode =true)]
        [Required]
        public DateTime StartDateTime { get; set; }

        [Display(Name = "Выполнено")]
        public bool IsDone { get; set; }
    }
}