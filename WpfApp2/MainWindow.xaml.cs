using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Office.Interop.Word;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
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

    
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }



        private void ButtonOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                txtEditor.Text = File.ReadAllText(openFileDialog.FileName);
        }

        
        public void BtnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            string inputText = txtEditor.Text.ToUpper();
            string password = txtKey.Text.ToUpper();
            VigenereCipher cipher = new VigenereCipher();
            txtOut.Text = cipher.Encrypt(inputText, password);
        }

        private void BtnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            string inputText = txtEditor.Text.ToUpper();
            string password = txtKey.Text.ToUpper();
            VigenereCipher cipher = new VigenereCipher();
            txtOut.Text = cipher.Decrypt(inputText, password);
        }

        private void SaveToTxt_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt"; 
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, txtOut.Text);
        }

        private void BtnOpenFileDocx_Click(object sender, RoutedEventArgs e)
        {
            // Open a doc file.
            Microsoft.Office.Interop.Word.Application application = new Microsoft.Office.Interop.Word.Application();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string text = "";
            if (openFileDialog.ShowDialog() == true)
            {
                Document document = application.Documents.Open(openFileDialog.FileName);

                // Loop through all words in the document.
                int count = document.Words.Count;
                
                for (int i = 1; i <= count; i++)
                {
                    // Write the word.
                    text += document.Words[i].Text;
                    
                }
                // Close word.
                application.Quit();
            }
            txtEditor.Text = text;
        }

        private void SaveToDocx_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Word.Application application = new Microsoft.Office.Interop.Word.Application();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.docx)|*.docx";
            if (saveFileDialog.ShowDialog() == true)
            {
                 Document document = application.Documents.Add();
                 Microsoft.Office.Interop.Word.Paragraph paragraph = document.Paragraphs.Add();
                 paragraph.Range.Text = txtOut.Text;
                 application.ActiveDocument.SaveAs2(saveFileDialog.FileName);
                 document.Close();
            }
                
        }

        

        private void RBtnrusLang_Checked(object sender, RoutedEventArgs e)
        {
            VigenereCipher.letters = VigenereCipher.rusAlphabet;
            
        }

        private void RBtnEngLang_Checked(object sender, RoutedEventArgs e)
        {
            VigenereCipher.letters = VigenereCipher.engAlphabet;
        }
    }
}
