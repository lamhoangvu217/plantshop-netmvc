using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace ElectroShop
{
    public static class MyString
    {
        public static String ToMD5(this String s)
        {
            var bytes = Encoding.UTF8.GetBytes(s);
            var hash = MD5.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
        public static String ToAscii(this String s)
        {
            String[][] symbols = {
                                 new String[] { "[áàảãạăắằẳẵặâấầẩẫậ]", "a" },
                                 new String[] { "[đ]", "d" },
                                 new String[] { "[éèẻẽẹêếềểễệ]", "e" },
                                 new String[] { "[íìỉĩị]", "i" },
                                 new String[] { "[óòỏõọôốồổỗộơớờởỡợ]", "o" },
                                 new String[] { "[úùủũụưứừửữự]", "u" },
                                 new String[] { "[ýỳỷỹỵ]", "y" },
                                 new String[] { "[\\s'\";,]", "-" }
                             };
            s = s.ToLower();
            foreach (var ss in symbols)
            {
                s = Regex.Replace(s, ss[0], ss[1]);
            }
            return s;
        }
        public static string str_slug(string s)
        {
            String[][] symbols ={
                                    new String[]{"[áàảãạăắằẳẵặâấầẩẫậ] ","a"},
                                    new String[]{"[đ]","d"},
                                    new String[]{"[éèẻẽẹêếềểễệ]","e"},
                                    new String[]{"[íìỉĩị]","i"},
                                    new String[]{"[óòỏõọôốồổỗộơớờởỡợ]","o"},
                                    new String[]{"[úùủũụưứừửữự]","u"},
                                    new String[]{"[ýỳỷỹỵ]","y"},
                                    new String[]{"[\\s'\";,]","-"},

                                    };
            s = s.ToLower();
            foreach (var ss in symbols)
            {
                s = Regex.Replace(s, ss[0], ss[1]);
            }
            return s;
        }
    }
}