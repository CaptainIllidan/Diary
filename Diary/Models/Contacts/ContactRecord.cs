using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diary.Models.Contacts
{
    public class ContactRecord
    {
        public int ID { get; set; }

        [Display(Name = "Имя")]
        [Required, MinLength(2), MaxLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [MinLength(2), MaxLength(50)]
        public string LastName { get; set; }

        [Display(Name = "Отчество")]
        [MinLength(4), MaxLength(50)]
        public string Patronymic { get; set; }

        [Display(Name = "Дата рождения")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}",ApplyFormatInEditMode = true)]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Организация")]
        [MinLength(4), MaxLength(50)]
        public string Company { get; set; }

        [Display(Name = "Контактная информация")]
        public virtual IList<ContactInformation> ContactInformation { get; set; }
    }
}