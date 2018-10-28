using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diary.Models.Diary;
using Diary.Models.Contacts;

namespace Diary.DataAccessLayer
{
    public class DiaryInitializer:System.Data.Entity.CreateDatabaseIfNotExists<DiaryContext>
    {
        protected override void Seed (DiaryContext context)
        {
            var diaryRecords = new List<DiaryRecord>()
            {
                new Meeting
                {
                    Theme = "Встреча 1",
                    StartDateTime = new DateTime(2018,10,19,12,00,00),
                    EndDateTime = new DateTime(2018,10,19,14,00,00),
                    Place = "Дома",
                    IsDone = true
                },
                new Meeting
                {
                    Theme = "Встреча 2",
                    StartDateTime = new DateTime(2018,10,20,13,35,00),
                    EndDateTime = new DateTime(2018,10,20,14,57,00),
                    Place = "На работе",
                    IsDone = false
                },
                new Note
                {
                    Theme = "Памятка 1",
                    StartDateTime = new DateTime(2018,10,30,15,00,00),
                    IsDone = false
                },
                new Note
                {
                    Theme = "Памятка 2",
                    StartDateTime = new DateTime(2018,11,30,15,00,00),
                    IsDone = false
                },
                new Thing
                {
                    Theme = "Дело 1",
                    StartDateTime = new DateTime(2018,10,17,10,00,00),
                    EndDateTime  = new DateTime(2018,10,17,13,00,00),
                    IsDone = false
                },
                new Thing
                {
                    Theme = "Дело 2",
                    StartDateTime = new DateTime(2018,10,18,08,00,00),
                    EndDateTime  = new DateTime(2018,10,18,17,00,00),
                    IsDone = false
                }
            };

            diaryRecords.ForEach(z => context.DiaryRecords.Add(z));
            context.SaveChanges();

            var contacts = new List<ContactRecord>
            {
                new ContactRecord
                {
                    LastName = "Иванов",
                    FirstName = "Иван",
                    Patronymic = "Иванович",
                    BirthDate = new DateTime(1980,01,01),
                    Company = "Microsoft"
                },
                new ContactRecord
                {
                    LastName = "Петров",
                    FirstName = "Петр",
                    Patronymic = "Петрович",
                    BirthDate = new DateTime(1970,11,11),
                    Company = "Крошка-картошка"                    
                },
                new ContactRecord
                {
                    LastName = "Ламарр",
                    FirstName = "Хеди",
                    BirthDate = new DateTime(1970,11,11),
                    Company = "IEEE"                    
                }
            };
            contacts[0].ContactInformation = new List<ContactInformation>
                    {
                        new ContactInformation
                        {
                            ContactInformationType = ContactInformationType.PhoneNumber,
                            Value = "+79999999999",
                            ContactRecord = contacts[0]
                        },
                        new ContactInformation
                        {
                            ContactInformationType = ContactInformationType.Email,
                            Value = "ivanovii@microsoft.com",
                            ContactRecord = contacts[0]
                        },
                    };
            contacts[1].ContactInformation = new List<ContactInformation>
                    {
                        new ContactInformation
                        {
                            ContactInformationType = ContactInformationType.PhoneNumber,
                            Value = "+79998888888",
                            ContactRecord = contacts[1]
                        },
                        new ContactInformation
                        {
                            ContactInformationType = ContactInformationType.Email,
                            Value = "potatoholder@gmail.com",
                            ContactRecord = contacts[1]
                        },
                        new ContactInformation
                        {
                            ContactInformationType = ContactInformationType.Skype,
                            Value = "potatoholder",
                            ContactRecord = contacts[1]
                        }
                    };
            contacts[2].ContactInformation = new List<ContactInformation>
                    {
                        new ContactInformation
                        {
                            ContactInformationType = ContactInformationType.Another,
                            Value = "Viber: +79999999999",
                            ContactRecord = contacts[2]
                        },
                        new ContactInformation
                        {
                            ContactInformationType = ContactInformationType.Email,
                            Value = "lamarrh@yahoo.com",
                            ContactRecord = contacts[2]
                        }
                    };
            contacts.ForEach(z => context.Contacts.Add(z));
            context.SaveChanges();
        }
    }
}