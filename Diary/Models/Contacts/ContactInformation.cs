using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diary.Models.Contacts
{
    public enum ContactInformationType
    {
        [Display(Name = "Телефон")]
        PhoneNumber,
        [Display(Name = "Email")]
        Email,
        [Display(Name = "Skype")]
        Skype,
        [Display(Name = "Другое")]
        Another
    }

    public class ContactInformation
    {
        public int ID { get; set; }

        [Display(Name = "Тип контактной информации")]
        [Required, EnumDataType(typeof(ContactInformationType))]
        public ContactInformationType ContactInformationType { get; set; }

        [Display(Name = "Значение")]
        [MinLength(4), MaxLength(50)]
        public string Value { get; set; }

        public ContactRecord ContactRecord { get; set; }
    }
}