using System;
using System.Collections.Generic;
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

namespace ViegenerCipher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly char[] Alphabet = new char[35] { 'A', 'Ą', 'B', 'C', 'Ć', 'D', 'E', 'Ę', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'Ł', 'M', 'N', 'Ń', 'O', 'Ó', 'P', 'Q', 'R', 'S', 'Ś', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'Ź', 'Ż' };
        readonly char[,] CipherFieldArr = new char[35, 35];
        public MainWindow()
        {
            InitializeComponent();
            FillField(Alphabet, CipherFieldArr);
        }
        static void FillField(char[] alphabet, char[,] field)
        {
            int len = alphabet.Length;
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    field[i, j] = alphabet[(i + j) % 35];
                }
            }
        }
        static string ParseKeyWithText(string key, string text)
        {
            string output = "";
            int keyLength = key.Length;
            byte spacesCounter = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ' ')
                {
                    output += ' ';
                }
                if (char.IsLetter(text[i]))
                {
                    output += key[(i - spacesCounter) % keyLength];
                }
                else
                    spacesCounter++;

            }
            return output;
        }
        static string Encrypt(string text, string keyToEncrypt, char[,] field)
        {
            string output = "";
            int textLength = text.Length;
            int fieldLength = field.GetLength(0);
            for (int i = 0; i < textLength; i++)
            {
                if (keyToEncrypt[i] == ' ')
                {
                    output += ' ';
                    continue;
                }
                int xPos = -1;
                int yPos = -1;
                for (int x = 0; x < fieldLength; x++)
                {
                    if (text[i] == field[x, 0])
                    {
                        xPos = x;
                        break;
                    }
                }
                for (int y = 0; y < fieldLength; y++)
                {
                    if (keyToEncrypt[i] == field[0, y])
                    {
                        yPos = y;
                        break;
                    }
                }
                if (xPos != -1 && yPos != -1)
                {
                    output += field[xPos, yPos];
                }
                else
                    throw new Exception($"wrong index at text: {output} xPos:{xPos}, yPos:{yPos}");
            }

            return output;
        }
        static string Decrypt(string key, string encryptedText, char[,] field)
        {
            string output = "";
            int textLength = encryptedText.Length;
            int fieldLength = field.GetLength(0);
            for (int i = 0; i < textLength; i++)
            {
                int xPos = -1;
                int yPos = -1;
                if (key[i] == ' ')
                {
                    output += ' ';
                    continue;
                }
                for (int x = 0; x < fieldLength; x++)
                {
                    if (field[x, 0] == key[i])
                    {
                        xPos = x;
                        break;
                    }
                }
                for (int y = 0; y < fieldLength; y++)
                {
                    if (field[xPos, y] == encryptedText[i])
                    {
                        yPos = y;
                        break;
                    }
                }
                if (xPos != -1 && yPos != -1)
                {
                    output += field[0, yPos];
                }
                else
                    throw new Exception($"wrong index at text: {output} xPos:{xPos}, yPos:{yPos}");

            }
            return output;
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            string textToEncrypt = EncryptInput.Text;
            string key = EncryptKey.Text;
            FormatValues(key, textToEncrypt, out string formattedKey, out string formattedText);
            if (AreEntriesEmpty(formattedKey,formattedText))
            {
                EncryptOutputTextBox.Text = "Either key or input text can't be empty or contain non letter characters";
                return;
            }
            string parsedKey = ParseKeyWithText(formattedKey, formattedText);
            string encryptedText = Encrypt(formattedText, parsedKey, CipherFieldArr);
            EncryptOutputTextBox.Text = encryptedText;
            DecryptInput.Text = encryptedText;
            DecryptKey.Text = parsedKey;
        }
        private static bool AreEntriesEmpty(string key, string text)
        {
            return key == "" || text == "" ? true : false;
        }
        private static void FormatValues(string key, string text, out string formattedKey, out string formattedText)
        {
            formattedText = FormatAndRemoveUnwantedChars(text);
            string tempKey = FormatAndRemoveUnwantedChars(key);
            formattedKey = RemoveSpacesFromInside(tempKey);
        }
        private static string RemoveSpacesFromInside(string key)
        {
            string output = "";
            for(int i = 0; i < key.Length; i++)
            {
                if (char.IsLetter(key[i]))
                    output += key[i];
            }
            return output;
        }
        private static string FormatAndRemoveUnwantedChars(string input)
        {
            string text = input.Trim();
            string output = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ' ')
                {
                    output += ' ';
                    continue;
                }
                if (char.IsLetter(text[i]))
                    output += text[i];
            }
            return output.ToUpper();
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            string textToDecrypt = DecryptInput.Text;
            string key = DecryptKey.Text;
            FormatValues(key, textToDecrypt, out string formattedKey, out string formattedText);
            if (AreEntriesEmpty(formattedKey, formattedText))
            {
                DecryptOutputTextBox.Text = "Either key or input text can't be empty or contain non letter characters";
                return;
            }
            string parsedKey = ParseKeyWithText(formattedKey, formattedText);
            string decryptedText = Decrypt(parsedKey, formattedText, CipherFieldArr);
            DecryptOutputTextBox.Text = decryptedText;


        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            EncryptInput.Text = "";
            EncryptKey.Text = "";
            EncryptOutputTextBox.Text = "";
            DecryptInput.Text = "";
            DecryptKey.Text = "";
            DecryptOutputTextBox.Text = "";
        }
    }
}
