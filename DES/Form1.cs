//способ проверки описан в "README"

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace DES
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static string sKey;

        //кнопка "зашифровать"
        private void button1_Click(object sender, EventArgs e)
        {
            sKey = textBox1.Text;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string source = openFileDialog1.FileName;

                saveFileDialog1.Filter = "txt files |*.txt"; //разрешение зашифрованного файла
                //условие сохранения файла
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string destination = saveFileDialog1.FileName; //переменная для записи зашифрованного файла
                    EncryptFile(source, destination, sKey);
                }

            }

        }
        //шифрование
        private void EncryptFile(string source, string destination, string sKey)
        {
            FileStream fsInput = new FileStream(source, FileMode.Open, FileAccess.Read);
            FileStream fsEncrypted = new FileStream(destination, FileMode.Create, FileAccess.Write); //запись зашифрованного файла
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            try
            {
                DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey); //вектор инициализации
                ICryptoTransform desencrypt = DES.CreateEncryptor();
                CryptoStream cryptostream = new CryptoStream(fsEncrypted, desencrypt, CryptoStreamMode.Write);
                byte[] bytearrayinput = new byte[fsInput.Length - 0];
                fsInput.Read(bytearrayinput, 0, bytearrayinput.Length); //чтение блока байт
                cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length); //запись последовательности байтов в текущий cryptostream
                cryptostream.Close();
            }
            //Если ключ больше или меньше 8 символов или он неверен
            catch
            {
                MessageBox.Show("Ошибка");
                return; 
            }
            fsInput.Close();
            fsEncrypted.Close();
        }

        //дешифрование
        private void DecryptFile(string source, string destination, string sKey)
        {
            FileStream fsInput = new FileStream(source, FileMode.Open, FileAccess.Read);
            FileStream fsEncrypted = new FileStream(destination, FileMode.Create, FileAccess.Write); //запись зашифрованного файла
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            try
            {
                DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey); //вектор инициализации
                ICryptoTransform desencrypt = DES.CreateDecryptor();
                CryptoStream cryptostream = new CryptoStream(fsEncrypted, desencrypt, CryptoStreamMode.Write);
                byte[] bytearrayinput = new byte[fsInput.Length - 0];
                fsInput.Read(bytearrayinput, 0, bytearrayinput.Length); //чтение блока байт
                cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length); //запись последовательности байтов в текущий cryptostream
                cryptostream.Close();
            }
            //Если ключ больше или меньше 8 символов или он неверен
            catch
            {
                MessageBox.Show("Ошибка");
                return;
            }
            fsInput.Close();
            fsEncrypted.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sKey = textBox1.Text;
            openFileDialog1.Filter = "txt files |*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string source = openFileDialog1.FileName;

                saveFileDialog1.Filter = "txt files |*.txt"; //разрешение зашифрованного файла
                //условие сохранения файла
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string destination = saveFileDialog1.FileName; //переменная для записи зашифрованного файла
                    DecryptFile(source, destination, sKey);
                }

            }
        }
    }
}
