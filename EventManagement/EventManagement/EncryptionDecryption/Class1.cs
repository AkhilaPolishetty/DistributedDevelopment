using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionDecryption
{
    public class Class1
    {

        public static string Encrypt(string s)
        {
            bool fHasSpace = s.Contains(" ");
            if (fHasSpace)
            {
                return "Space is not allowed in password";
            }
            /**Making a Map for encryption**/
            char[] StringkeyE = new char[72] { '2','b','c','e','d','f','i','j','h','g','k','l','m','n','o','p','q','r','s','t','u','v','w','N','y','z','A', 'B', 'C', 'D','E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'x', 'O', 'P', 'Q',
                'R', 'S', 'T', 'V', 'U', 'W', 'X', 'Y', 'Z', '!','@', '#','$', '%', '^', '&', '*','(',')', '0', '1', 'a', '3', '4', '5','6','7','8','9'};

            bool FirstTimeE = true;
            Dictionary<char, int> dict = new Dictionary<char, int>();

            /**Filling the map just the first time **/
            if (FirstTimeE)
            {
                for (int val = 0; val < 72; val++)
                {
                    dict.Add(StringkeyE[val], val % 72);
                }
                FirstTimeE = false;
            }
            int len = s.Length;
            /** Coverting the string to upper case as the map is in upper**/
            char[] charArray = s.ToCharArray();
            int[] res = new int[len];
            int len_half = len / 2;
            for (int i = 0; i < len; i++)
            {
                if (dict.ContainsKey(charArray[i]))
                {
                    res[i] = dict[charArray[i]];
                }
            }

            /*Reversing the string formed after dictionary conversion**/
            int start = 0;
            int end = len - 1;
            int temp;
            while (start < end)
            {
                temp = res[start];
                res[start] = res[end];
                res[end] = temp;
                start++;
                end--;
            }
            string revandc = "";
            int r = 0;
            /**Introducing new elements into the string**/
            foreach (var item in res)
            {
                revandc += item;
                if (r < res.Length - 1)
                {
                    if (r % 2 == 0)
                        revandc += ";";
                    else
                        revandc += "~";
                }
                r++;
            }

            return revandc;
        }


        public static string Decrypt(string text)
        {

            char[] StringkeyD = new char[72] {'2','b','c','e','d','f','i','j','h','g','k','l','m','n','o','p','q','r','s','t','u','v','w','N','y','z','A', 'B', 'C', 'D','E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'x', 'O', 'P', 'Q',
                'R', 'S', 'T', 'V', 'U', 'W', 'X', 'Y', 'Z', '!','@', '#','$', '%', '^', '&', '*','(',')', '0', '1', 'a', '3', '4', '5','6','7','8','9'};
            bool FirstTimeD = true;
            string temp = "";
            Dictionary<int, char> dictD = new Dictionary<int, char>();
            /**Decrypts the values from the data using dictionary**/
            if (text.Contains("~") || text.Contains(";"))
            {
                if (FirstTimeD)
                {
                    for (int val = 0; val < 72; val++)
                    {
                        dictD.Add(val % 72, StringkeyD[val]);
                    }
                    FirstTimeD = false;
                }
                /**Splits based on special characters**/
                string[] splitD = text.Split(new Char[] { ';', '~' });
                string[] array = new string[splitD.Length];

                char[] resultD = new char[splitD.Length];
                Array.Reverse(splitD);
                /**Finding the letter in dictionary and returning the result**/
                for (int d = 0; d < array.Length; d++)
                {
                    if (dictD.ContainsKey(Convert.ToInt32(splitD[d])))
                    {
                        resultD[d] = dictD[(Convert.ToInt32(splitD[d]))];
                    }
                }
               temp = string.Join("", resultD);
            }else
            {
                temp = null;
            }
            return temp;
        }

    }

}

