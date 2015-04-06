﻿using System;

namespace BeginMobile.Services.Utils
{
    public class DateConverter
    {
        private const int One = 1;
        private const int Minute = 60;
        private const int Hour = 60;
        private const int Day = 24;
        private const int Week = 7;
        private const int Year = 365;

        public static string GetTimeSpan(DateTime postDate)
        {
            var stringy = string.Empty;
            var diff = DateTime.Now.Subtract(postDate);
            double days = diff.Days;
            var hours = diff.Hours + days*Day;
            var minutes = diff.Minutes + hours*Minute;

            if (minutes <= One)
            {
                return "Just Now";
            }

            var years = Math.Floor(diff.TotalDays/Year);
            if (years >= One)
            {
                return string.Format("active {0} year{1} ago", years, years >= 2 ? "s" : null);
            }

            var weeks = Math.Floor(diff.TotalDays/Week);
            if (weeks >= One)
            {
                double partOfWeek = days - weeks*Week;
                if (partOfWeek > 0)
                {
                    stringy = string.Format(", {0} day{1}", partOfWeek, partOfWeek > 1 ? "s" : null);
                }
                return string.Format("active {0} week{1}{2} ago", weeks, weeks >= 2 ? "s" : null, stringy);
            }

            if (days >= One)
            {
                var partOfDay = hours - days*Day;
                if (partOfDay > 0)
                {
                    stringy = string.Format(", {0} hour{1}", partOfDay, partOfDay > 1 ? "s" : null);
                }
                return string.Format("active {0} day{1}{2} ago", days, days >= 2 ? "s" : null, stringy);
            }

            if (!(hours >= One)) return minutes.ToString("active {0} minutes ago");
            var partOfHour = minutes - hours*Hour;
            if (partOfHour > 0)
            {
                stringy = string.Format(", {0} minute{1}", partOfHour, partOfHour > 1 ? "s" : null);
            }
            return string.Format("active {0} hour{1}{2} ago", hours, hours >= 2 ? "s" : null, stringy);

            // Only condition left is minutes > 1
        }
    }
}