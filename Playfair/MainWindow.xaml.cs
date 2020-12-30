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

namespace Playfair
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly char[] Alphabet = new char[35] { 'A', 'Ą', 'B', 'C', 'Ć', 'D', 'E', 'Ę', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'Ł', 'M', 'N', 'Ń', 'O', 'Ó', 'P', 'Q', 'R', 'S', 'Ś', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'Ź', 'Ż' };
        char[,] CipherField;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            EncryptInput.Text = "";
            EncryptKey.Text = "";
            EncryptOutput.Text = "";
            DecryptInput.Text = "";
            DecryptKey.Text = "";
            DecryptOutput.Text = "";
            KeyTable.Text = "";
        }
        private void DecryptBtn_Click(object sender, RoutedEventArgs e)
        {
            CipherField = new char[7, 5];
            KeyTable.Text = "";
            string encryptedText = DecryptInput.Text;
            string key = DecryptKey.Text;
            if (InputFormattingSucces(encryptedText, key, out string text))
            {
                string[] encryptedTextSlicedToDigrams = SliceTextToDigrams(text);
                string decryptedText = Decrypt(CipherField, encryptedTextSlicedToDigrams);
                DecryptOutput.Text = decryptedText;
            }
        }
        private bool InputFormattingSucces(string textIn, string key, out string text)
        {
            text = textIn;
            text = FormatToCorrectForm(text);
            key = FormatToCorrectForm(key);
            if (AreEntriesEmpty(key, text))
            {
                EncryptOutput.Text = "Either key or input text can't be empty or contain non letter characters";
                return false;
            }
            key = RemoveLetterDuplicates(key);
            FillCipherField(Alphabet, CipherField, key);
            DisplayField(CipherField);
            return true;
        }
        private void EncryptBtn_Click(object sender, RoutedEventArgs e)
        {
            CipherField = new char[7, 5];
            KeyTable.Text = "";
            string inputText = EncryptInput.Text;
            string key = EncryptKey.Text;
            if (InputFormattingSucces(inputText, key, out string text))
            {
                text = LookForDoubledLetters(text);
                string[] slicedText = SliceTextToDigrams(text);
                string encryptedText = Encrypt(CipherField, slicedText);
                EncryptOutput.Text = encryptedText;
            }
        }
        private static string Decrypt(char[,] field, string[] textArr)
        {
            string output = "";
            for (int i = 0; i < textArr.Length; i++)
            {
                string digram = textArr[i];
                char first = digram[0];
                char second = digram[1];
                string decryptedDigram = DecryptDigram(first, second, field);
                output += $"{decryptedDigram}";
            }
            return output;
        }
        private static string DecryptDigram(char first, char second, char[,] field)
        {
            string output = "";
            GetDigramPositionsOnField(first, second, field, out int firstXPos, out int firstYPos, out int secondXPos, out int secondYPos, out int xAxisLen, out int yAxisLen);
            if (firstXPos == secondXPos)
            {
                int fY;
                int sY;
                if (firstYPos - 1 == -1)
                    fY = xAxisLen - 1;
                else
                    fY = firstYPos - 1;
                if (secondYPos - 1 == -1)
                    sY = xAxisLen - 1;
                else
                    sY = secondYPos - 1;
                output += field[firstXPos, fY];
                output += field[secondXPos, sY];
            }
            else if (firstYPos == secondYPos)
            {
                int fX;
                int sX;
                if (firstXPos - 1 == -1)
                    fX = yAxisLen - 1;
                else
                    fX = firstXPos - 1;
                if (secondXPos - 1 == -1)
                    sX = yAxisLen - 1;
                else
                    sX = secondXPos - 1;
                output += field[fX, firstYPos];
                output += field[sX, secondYPos];
            }
            else
            {
                output += field[firstXPos, secondYPos];
                output += field[secondXPos, firstYPos];
            }
            return output;

        }
        private static void GetDigramPositionsOnField(char first, char second, char[,] field, out int firstXPos, out int firstYPos, out int secondXPos, out int secondYPos, out int xAxisLen, out int yAxisLen)
        {
            xAxisLen = field.GetLength(1);
            yAxisLen = field.GetLength(0);
            firstXPos = -1;
            firstYPos = -1;
            secondXPos = 1;
            secondYPos = -1;
            for (int i = 0; i < yAxisLen; i++)
            {
                for (int j = 0; j < xAxisLen; j++)
                {
                    if (field[i, j] == first)
                    {
                        firstXPos = i;
                        firstYPos = j;
                    }
                    if (field[i, j] == second)
                    {
                        secondXPos = i;
                        secondYPos = j;
                    }
                }
            }
        }
        private static string EncryptDigram(char first, char second, char[,] field)
        {
            string output = "";
            GetDigramPositionsOnField(first, second, field, out int firstXPos, out int firstYPos, out int secondXPos, out int secondYPos, out int xAxisLen, out int yAxisLen);
            if (firstXPos == secondXPos)
            {
                output += field[firstXPos, (firstYPos + 1) % xAxisLen];
                output += field[secondXPos, (secondYPos + 1) % xAxisLen];
            }
            else if (firstYPos == secondYPos)
            {
                output += field[(firstXPos + 1) % yAxisLen, firstYPos];
                output += field[(secondXPos + 1) % yAxisLen, secondYPos];
            }
            else
            {
                output += field[firstXPos, secondYPos];
                output += field[secondXPos, firstYPos];
            }
            return output;
        }
        private static string Encrypt(char[,] field, string[] textArr)
        {
            string output = "";
            for (int i = 0; i < textArr.Length; i++)
            {
                string digram = textArr[i];
                char first = digram[0];
                char second = digram[1];
                string encryptedDigram = EncryptDigram(first, second, field);
                output += $"{encryptedDigram} ";
            }
            return output;
        }
        private static string[] SliceTextToDigrams(string text)
        {
            if (text.Length % 2 != 0)
                text = $"{text}X";
            string[] arr = new string[text.Length / 2];
            string temp = "";
            for (int i = 0; i < text.Length; i++)
            {
                if ((i + 1) % 2 != 0)
                    temp += text[i];
                else
                {
                    temp += text[i];
                    arr[i / 2] = temp;
                    temp = "";
                }
            }
            return arr;
        }
        private static string LookForDoubledLetters(string text)
        {
            //additional '.' not to get out of array len
            text = $"{text}.";
            string output = "";
            for (int i = 0; i < text.Length - 1; i++)
            {
                if (text[i] != text[i + 1])
                    output += text[i];
                else if (text[i] == 'X' && text[i + 1] == 'X')
                    output += $"{text[i]}Ź";
                else
                    output += $"{text[i]}X";
            }
            return output;
        }
        private static void FillCipherField(char[] alphabet, char[,] cipherField, string key)
        {
            PutKeyIntoField(key, cipherField);
            PutRestLettersIntoField(alphabet, cipherField, key);
        }
        private static void PutKeyIntoField(string text, char[,] field)
        {
            int xAxisLen = field.GetLength(1);
            int yAxisLen = field.GetLength(0);
            int counter = 0;
            for (int i = 0; i < yAxisLen; i++)
            {
                for (int j = 0; j < xAxisLen; j++)
                {
                    if (counter == text.Length)
                        return;
                    field[i, j] = text[counter];
                    counter++;
                }
            }
        }
        private static void PutRestLettersIntoField(char[] alphabet, char[,] field, string key)
        {
            int xAxisLen = field.GetLength(1);
            int yAxisLen = field.GetLength(0);
            int counter = 0;
            for (int i = 0; i < yAxisLen; i++)
            {
                for (int j = 0; j < xAxisLen; j++)
                {
                    if (field[i, j] == '\x0000')
                    {
                        while (true)
                        {
                            if (!key.Contains(alphabet[counter]))
                            {
                                break;
                            }
                            counter++;
                        }
                        field[i, j] = alphabet[counter];
                        counter++;
                    }
                }
            }
        }
        private void DisplayField(char[,] field)
        {
            int xAxisLen = field.GetLength(1);
            int yAxisLen = field.GetLength(0);
            for (int i = 0; i < yAxisLen; i++)
            {
                for (int j = 0; j < xAxisLen; j++)
                {
                    KeyTable.Text += ($"{field[i, j]} ");
                }
                KeyTable.Text += "\n";
            }
        }
        private static string RemoveLetterDuplicates(string key)
        {
            string output = "";
            for (int i = 0; i < key.Length; i++)
            {
                if (!output.Contains(key[i]))
                    output += key[i];
            }
            return output;
        }
        private static bool AreEntriesEmpty(string key, string text)
        {
            return key == "" || text == "" ? true : false;
        }
        private static string FormatToCorrectForm(string text)
        {
            string temp = FormatAndRemoveUnwantedChars(text);
            return RemoveSpacesFromInside(temp);
        }
        private static string RemoveSpacesFromInside(string text)
        {
            string output = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsLetter(text[i]))
                    output += text[i];
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

    }
}
