using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCampus
{
    public static class Statics
    {
        public static string GetClassName(this int c)
        {
            switch (c)
            {
                case -2:
                    return "Infantry";
                case -1:
                    return "Nursery";
                case 0:
                    return "Kg-1";
                case 1:
                    return "One";
                case 2:
                    return "Two";
                case 3:
                    return "Three";
                case 4:
                    return "Four";
                case 5:
                    return "Five";
                case 6:
                    return "Six";
                case 7:
                    return "Seven";
                case 8:
                    return "Eight";
                case 9:
                    return "Nine";
                case 10:
                    return "Ten";
                default:
                    return "";
            }
        }

        public static string GetClassName(this string c)
        {
            switch (c)
            {
                case "IN":
                    return "Infantry";
                case "NR":
                    return "Nursery";
                case "K1":
                    return "Kg-1";
                default:
                    return "";
            }
        }

        public static int GetClassNumber(this string c)
        {
            switch (c)
            {
                case "IN":
                    return -2;
                case "Infantry":
                    return -2;
                case "NR":
                    return -1;
                case "Nursery":
                    return -1;
                case "K1":
                    return 0;
                case "Kg-1":
                    return 0;
                case "01":
                    return 1;
                case "One":
                    return 1;
                case "02":
                    return 2;
                case "Two":
                    return 2;
                case "03":
                    return 3;
                case "Three":
                    return 3;
                case "04":
                    return 4;
                case "Four":
                    return 4;
                case "05":
                    return 5;
                case "Five":
                    return 5;
                case "06":
                    return 6;
                case "Six":
                    return 6;
                case "07":
                    return 7;
                case "Seven":
                    return 7;
                case "08":
                    return 8;
                case "Eight":
                    return 8;
                case "09":
                    return 9;
                case "Nine":
                    return 9;
                case "10":
                    return 10;
                case "Ten":
                    return 10;
                default:
                    return 0;
            }
        }

        public static string GetClassCode(this string c)
        {
            switch (c)
            {
                case "Infantry":
                    return "IN";
                case "Nursery":
                    return "NR";
                case "Kg-1":
                    return "K1";
                case "1":
                    return "01";
                case "One":
                    return "01";
                case "2":
                    return "02";
                case "Two":
                    return "02";
                case "3":
                    return "03";
                case "Three":
                    return "03";
                case "4":
                    return "04";
                case "Four":
                    return "04";
                case "5":
                    return "05";
                case "Five":
                    return "05";
                case "6":
                    return "06";
                case "Six":
                    return "06";
                case "7":
                    return "07";
                case "Seven":
                    return "07";
                case "8":
                    return "08";
                case "Eight":
                    return "08";
                case "9":
                    return "09";
                case "Nine":
                    return "09";
                case "10":
                    return "10";
                case "Ten":
                    return "10";
                default:
                    return "";
            }
        }
        public static string GetSubjectName(this string c)
        {
            switch(c)
            {
                case "01":
                    return "Bangla 1st";
                case "02":
                    return "Bangla 2nd";
                case "03":
                    return "English 1st";
                case "04":
                    return "English 2nd";
                case "05":
                    return "G.Math";
                case "06":
                    return "H.Math";
                case "07":
                    return "G.Science";
                case "08":
                    return "Religion";
                case "09":
                    return "B.G.S";
                case "10":
                    return "Arts & Crafts";
                case "11":
                    return "Sp.English";
                case "12":
                    return "Computer";
                default:
                    return "";
            }
        }

        public static string GetSubjectCode(this string c)
        {
            switch (c)
            {
                case "Bangla 1st":
                    return "01";
                case "Bangla 2nd":
                    return "02";
                case "English 1st":
                    return "03";
                case "English 2nd":
                    return "04";
                case "G.Math":
                    return "05";
                case "H.Math":
                    return "06";
                case "G.Science":
                    return "07";
                case "Religion":
                    return "08";
                case "B.G.S":
                    return "09";
                case "Arts & Crafts":
                    return "10";
                case "Sp.English":
                    return "11";
                case "Computer":
                    return "12";
                default:
                    return "";
            }
        }

        public static string GetExamName(this int c)
        {
            switch(c)
            {
                case 1:
                    return "Quiz 1";
                case 2:
                    return "Quiz 2";
                case 3:
                    return "Semester Final";
                default: 
                    return "";
            }
        }

        public static int GetExamCode(this string c)
        {
            switch (c)
            {
                case "Quiz 1":
                    return 1;
                case "Quiz 2":
                    return 2;
                case "Semester Final":
                    return 3;
                default:
                    return 0;
            }
        }

        public static string GetGrade(this string m)
        {
            if (m == "--") return m;

            double c = Convert.ToDouble(m);
            if (c >= 80 && c <= 100)
            {
                return "A+";
            }
            else if (c >= 70 && c <= 79)
            {
                return "A";
            }
            else if (c >= 60 && c <= 69)
            {
                return "A-";
            }
            else if (c >= 50 && c <= 59)
            {
                return "B";
            }
            else if (c >= 40 && c <= 49)
            {
                return "C";
            }
            else return "F";
        }

        public static string GetGradePoint(this string m)
        {
            if (m == "--") return m;

            double c = Convert.ToDouble(m);
            if (c >= 80 && c <= 100)
            {
                return "5.00";
            }
            else if (c >= 70 && c <= 79)
            {
                return "4.00";
            }
            else if (c >= 60 && c <= 69)
            {
                return "3.50";
            }
            else if (c >= 50 && c <= 59)
            {
                return "3.00";
            }
            else if (c >= 40 && c <= 49)
            {
                return "2.00";
            }
            else return "0.00";
        }
    }
}
