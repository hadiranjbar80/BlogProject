using BlogProject.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using BlogProject.Domain.Models;

namespace BlogProject.Presentation.Utilities
{
    public static class CommonMethods<T>
    {

        public static List<T> CalculatePagingNumber(List<T> data, out Pager outPager, 
            int pageSize = 10, int pageId = 1)
        {
            if(pageId < 1) pageId = 1;

            var dataCount = data.Count;
            var pager = new Pager(dataCount,pageId,pageSize);
            var skip = (pageId - 1) * pageSize;
            
            var filteredData = data.Skip(skip).Take(pager.PageSize).ToList();

            outPager = pager;

            return filteredData;
        }

        public static string GetPersianDayOfTheWeek(DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            string day = string.Empty;

            switch (pc.GetDayOfWeek(date))
            {
                case DayOfWeek.Saturday:
                    day = "شنبه";
                    break;
                case DayOfWeek.Sunday:
                    day = "یکشنبه";
                    break;
                case DayOfWeek.Monday:
                    day = "دوشنبه";
                    break;
                case DayOfWeek.Tuesday:
                    day = "سه‌شنبه";
                    break;
                case DayOfWeek.Wednesday:
                    day = "چهارشنبه";
                    break;
                case DayOfWeek.Thursday:
                    day = "پنج‌شنبه";
                    break;
                case DayOfWeek.Friday:
                    day = "جمعه";
                    break;
            }

            return day;
        }

        public static string GetPersianDate(DateTime date)
        {
            PersianCalendar pc = new();

            return pc.GetYear(date) + "/" + pc.GetMonth(date) + "/" + pc.GetDayOfMonth(date);
        }

        public static string ReadEmailTemplate(string link, string title, string templateName)
        {
            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "EmailTemplates", templateName);
            StreamReader reader = new(templatePath);
            string emailBody = reader.ReadToEnd();

            emailBody = emailBody.Replace("{{link}}", link);
            emailBody = emailBody.Replace("{{title}}", title);

            return emailBody;
        }
    }
}
