using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class VigenereCipher
    {
        public static string engAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string rusAlphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        public static string letters = rusAlphabet;


        private string GetRepeatKey(string s, int n)
        {
            string p = s;
            while (p.Length < n)
            {
                p += p;
            }

            return p.Substring(0, n);
        }

        private string Vigenere(string text, string password, bool encrypting = true)
        {
            string passKey = GetRepeatKey(password, text.Length);
            string result = "";
            int alphLen = letters.Length;

            int alphIndex;
            int codeIndex;
            int n = 0;
            for (int i = 0; i < text.Length; i++)
            {
                alphIndex = letters.IndexOf(text[i]);
                codeIndex = letters.IndexOf(passKey[i - n]);
                if (alphIndex < 0)
                {
                    //если буква не найдена, добавляем её в исходном виде
                    result += text[i].ToString();
                    n++;
                }
                else
                {
                    result += letters[(alphLen + alphIndex + ((encrypting ? 1 : -1) * codeIndex)) % alphLen].ToString();
                }
            }

            return result;
        }

        //шифрование текста
        public string Encrypt(string plainMessage, string password)
            => Vigenere(plainMessage, password);

        //дешифрование текста
        public string Decrypt(string encryptedMessage, string password)
            => Vigenere(encryptedMessage, password, false);
    }
}
