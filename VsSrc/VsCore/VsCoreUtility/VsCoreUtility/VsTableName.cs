// btgw	 IMPORTANT: READ BEFORE DOWNLOADING, COPYING, INSTALLING OR USING. 
// teuu	
// sudi	 By downloading, copying, installing or using the software you agree to this license.
// gftg	 If you do not agree to this license, do not download, install,
// pyom	 copy or use the software.
// xtqt	
// bstr	                          License Agreement
// imze	         For OpenVSS - Open Source Video Surveillance System
// cpzi	
// fvtn	Copyright (C) 2007-2010, Prince of Songkla University, All rights reserved.
// fswm	
// wrho	Third party copyrights are property of their respective owners.
// optf	
// heji	Redistribution and use in source and binary forms, with or without modification,
// dmvb	are permitted provided that the following conditions are met:
// kdlg	
// uzfr	  * Redistribution's of source code must retain the above copyright notice,
// glat	    this list of conditions and the following disclaimer.
// fahf	
// wdxj	  * Redistribution's in binary form must reproduce the above copyright notice,
// lfdt	    this list of conditions and the following disclaimer in the documentation
// suxx	    and/or other materials provided with the distribution.
// fqfs	
// ardb	  * Neither the name of the copyright holders nor the names of its contributors 
// gqsx	    may not be used to endorse or promote products derived from this software 
// lpic	    without specific prior written permission.
// ozzg	
// wemz	This software is provided by the copyright holders and contributors "as is" and
// plpe	any express or implied warranties, including, but not limited to, the implied
// zsnz	warranties of merchantability and fitness for a particular purpose are disclaimed.
// chmp	In no event shall the Prince of Songkla University or contributors be liable 
// fitn	for any direct, indirect, incidental, special, exemplary, or consequential damages
// xgyf	(including, but not limited to, procurement of substitute goods or services;
// laxv	loss of use, data, or profits; or business interruption) however caused
// wafs	and on any theory of liability, whether in contract, strict liability,
// vqba	or tort (including negligence or otherwise) arising in any way out of
// dfco	the use of this software, even if advised of the possibility of such damage.
// cybo	
// csue	Intelligent Systems Laboratory (iSys Lab)
// dswg	Faculty of Engineering, Prince of Songkla University, THAILAND
// gmjq	Project leader by Nikom SUVONVORN
// fhop	Project website at http://code.google.com/p/openvss/

using System;
using System.Collections.Generic;
using System.Text;

namespace VsTableNameManage
{
    public class VsTableName
    {
        #region for weekTable
        public static DateTime[] getStartAndEndOfWeek(DateTime now)
        {
            DateTime[] result = new DateTime[2];

            int startAdder = (DayOfWeek.Monday - now.DayOfWeek);
            if (startAdder > 0)
            {
                startAdder = -6;
            }

            int endAdder = startAdder + 6;

            result[0] = now.AddDays(startAdder);
            result[1] = now.AddDays(endAdder);

            return result;
        }

        public static string getWeekTableName(string tableType, DateTime dt)
        {
            DateTime[] SEd = getStartAndEndOfWeek(dt);

            return string.Format("{0}_{1}_{2}_{3}_to_{4}_{5}_{6}", tableType,
                SEd[0].Day, SEd[0].Month, SEd[0].Year,
                SEd[1].Day, SEd[1].Month, SEd[1].Year);
        }

        public static string[] getWeekTableName(string tableType, DateTime dtStart, DateTime dtEnd)
        {
            List<string> tableNameList = new List<string>();
            string tableName;

            DateTime[] SEd;

            do
            {
                SEd = getStartAndEndOfWeek(dtStart);

                tableName = string.Format("{0}_{1}_{2}_{3}_to_{4}_{5}_{6}", tableType,
                        SEd[0].Day, SEd[0].Month, SEd[0].Year,
                        SEd[1].Day, SEd[1].Month, SEd[1].Year);

                tableNameList.Add(tableName);

                dtStart = SEd[1].AddDays(1);
            }
            while (dtEnd.CompareTo(SEd[1]) > 0);

            return tableNameList.ToArray();
        }
        #endregion

        #region for monthTable
        public static string getMonthTableName(string tableType, DateTime dt)
        {
            return string.Format("{0}_{1}_{2}", tableType, dt.Month, dt.Year);
        }

        public static string[] getMonthTableName(string tableType, DateTime dtStart, DateTime dtEnd)
        {
            List<string> tableNameList = new List<string>();
            string tableName;

            do
            {
                tableName = string.Format("{0}_{1}_{2}", tableType, dtStart.Month, dtStart.Year);

                tableNameList.Add(tableName);

                dtStart = dtStart.AddMonths(1);
            }
            while ((dtEnd.Year > dtStart.Year) ||
                (dtEnd.Year == dtStart.Year && dtEnd.Month >= dtStart.Month));


            return tableNameList.ToArray();
        }


        #endregion

        #region common

        public static string dateTimeToTableName(string tableType, DateTime date)
        {
            return String.Format("{0}_{1}_{2}", tableType, date.Month, date.Year);
        }

        public static string getTableName(string tableType, DateTime dt)
        {
            return getMonthTableName(tableType, dt);
        }
        public static string[] getTableName(string tableType, DateTime dtStart, DateTime dtEnd)
        {
            return getMonthTableName(tableType, dtStart, dtEnd);
        }

        public static DateTime tableNameToDateTime(string tableName)
        {
            if (tableName != "" && tableName != null)
            {
                int m = int.Parse(tableName.Split('_')[1]);
                int y = int.Parse(tableName.Split('_')[2]);

                return new DateTime(y, m, 1);
            }
            else
            {
                return DateTime.Now;
            }
        }

        public static int compareTable(string t1, string t2)
        {

            int m1 = int.Parse(t1.Split('_')[1]);
            int y1 = int.Parse(t1.Split('_')[2]);

            int m2 = int.Parse(t2.Split('_')[1]);
            int y2 = int.Parse(t2.Split('_')[2]);

            if (y1 > y2)
            {
                return 1;
            }
            else if (y1 == y2)
            {
                if (m1 > m2)
                {
                    return 1;
                }
                else if (m1 == m2)
                {
                    return 0;
                }
            }
            return -1;
        }

        public static string getOldestTimeTable(string[] tables, DateTime now)
        {
            DateTime oldest = tableNameToDateTime(tables[0]);
            string oldestTable = tables[0];
            foreach (string table in tables)
            {
                DateTime time =tableNameToDateTime(table);
                if (oldest.CompareTo(time) > 0)
                {
                    oldest = time;
                    oldestTable = table;
                }
            }
            return oldestTable;
        }

        #endregion
    }

}
