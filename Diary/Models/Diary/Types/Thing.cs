using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diary.Models.Diary
{
    public class Thing : DiaryRecordFinishable
    {
        public override DiaryRecordType DiaryRecordType { get { return DiaryRecordType.Thing; } }
    }
}