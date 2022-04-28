using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WpfApp2;

namespace UnitTestProject1
{
    [TestClass]
    public class WpfTest
    {

        [TestMethod]
        public void EcnryptRus_OneSpace()
        {
            string text = "Всем привет";
            string password = "Скорпион";
            string expected = "уьуэ ящчпцэ";

            VigenereCipher v = new VigenereCipher();
            string actual = v.Encrypt(text.ToUpper(), password.ToUpper());

            Assert.AreEqual(expected.ToUpper(), actual);
        }

        [TestMethod]
        public void EncryptRus_TwoSpace()
        {
            string text = "Всем  привет";
            string password = "Скорпион";
            string expected = "уьуэ  ящчпцэ";

            VigenereCipher v = new VigenereCipher();
            string actual = v.Encrypt(text.ToUpper(), password.ToUpper());

            Assert.AreEqual(expected.ToUpper(), actual);
        }
        [TestMethod]
        public void EncryptRus_Dot_Eng_Сomma_Digit()
        {
            string text = "ВсемR привет, как де4ла.";
            string password = "Скорпион";
            string expected = "уьуэR ящчпцэ, щръ му4щс.";

            VigenereCipher v = new VigenereCipher();
            string actual = v.Encrypt(text.ToUpper(), password.ToUpper());

            Assert.AreEqual(expected.ToUpper(), actual);
        }


        public void DecryptRus_OneSpace()
        {
            string text = "уьуэ ящчпцэ";
            string password = "Скорпион";
            string expected = " Всем привет";

            VigenereCipher v = new VigenereCipher();
            string actual = v.Decrypt(text.ToUpper(), password.ToUpper());

            Assert.AreEqual(expected.ToUpper(), actual);
        }

        [TestMethod]
        public void DeryptRus_TwoSpace()
        {
            string text = "уьуэ  ящчпцэ";
            string password = "Скорпион";
            string expected = "Всем  привет";

            VigenereCipher v = new VigenereCipher();
            string actual = v.Decrypt(text.ToUpper(), password.ToUpper());

            Assert.AreEqual(expected.ToUpper(), actual);
        }
        [TestMethod]
        public void DecryptRus_Dot_Eng_Сomma_Digit()
        {
            string text = "уьуэR ящчпцэ, щръ му4щс.";
            string password = "Скорпион";
            string expected = "ВсемR привет, как де4ла.";

            VigenereCipher v = new VigenereCipher();
            string actual = v.Decrypt(text.ToUpper(), password.ToUpper());

            Assert.AreEqual(expected.ToUpper(), actual);
        }
    }
}
