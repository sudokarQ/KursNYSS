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
