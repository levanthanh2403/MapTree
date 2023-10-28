using Microsoft.AspNetCore.Http;
using NPOI.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace map.backend.shared.Helper
{
    public class Utils
    {
        public static string ConvertDatetimeToString(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }
        public static DateTime ConvertStringtoDatetime(string dt, string type)
        {
            return DateTime.ParseExact(dt, type, CultureInfo.InvariantCulture);
        }
        public static DateTime? ConvertStringToDateTime(string? dateString)
        {
            DateTime _date = DateTime.Now;
            try
            {
                if (!string.IsNullOrEmpty(dateString))
                {
                    DateTime.TryParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _date);
                }
                else return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            return _date;
        }
        public static string getDayFromDate(DateTime? date)
        {
            string _day = null;
            if(date.HasValue)
            {
                _day = date.Value.Day.ToString();
            }
            return _day;
        }
        public static string getMonthFromDate(DateTime? date)
        {
            string _day = null;
            if(date.HasValue)
            {
                _day = date.Value.Month.ToString();
            }
            return _day;
        }
        public static string getYearFromDate(DateTime? date)
        {
            string _day = null;
            if(date.HasValue)
            {
                _day = date.Value.Year.ToString();
            }
            return _day;
        }
        public static string buildAddress(string address_detail, string ward, string distict, string province)
        {
            return (string.IsNullOrEmpty(address_detail) ? "" : address_detail + ", ")  + ward + ", " + distict + ", " + province; 
        }
    }
}
