using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace map.backend.shared.Helper
{
    public static class Const
    {
        public const string Date = "yyyy-MM-dd";
        public const string DateTime = "yyyy-MM-dd HH:mm:ss";
        public const string FullDateTime = "dd/MM/yyyy HH:mm:ss.fff";
        public const string DateText = "yyyyMMdd";
        public const string DateText_yyMMdd = "yyMMdd";
        public const string DateText_HHmmss = "HHmmss";
        public const string FullDateTimeText = "yyyyMMddHHmmss";
        public const string TEXT = "TEXT";
        public const string NUMBER = "NUMBER";
        public const string FullTime = "HH:mm:ss.fff";
        public const string FullTimeHHmmss = "HH:mm:ss";
        public const string FullTimeHHmm = "HH:mm";
    }
    public static class ListConst
    {
        public static List<dropdown> lstLocationStatus = new List<dropdown>
        {
            new dropdown {value = "", text = "Tất cả"},
            new dropdown {value = "0", text = "Không trồng cây"},
            new dropdown {value = "1", text = "Đã trồng cây"},
        };
        public static List<dropdown> lstRecordStat = new List<dropdown>
        {
            new dropdown {value = "", text = "Tất cả"},
            new dropdown {value = "O", text = "Mở"},
            new dropdown {value = "C", text = "Đóng"},
        };
    }
}
public class dropdown
{
    public string value { get; set; }
    public string text { get; set; }
}
