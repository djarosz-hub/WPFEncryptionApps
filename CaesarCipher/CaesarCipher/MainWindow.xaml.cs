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

namespace CaesarCipher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly char[] Alphabet;
        private int LastestCipherValue;
        public string LastestOutput;
        public MainWindow()
        {
            InitializeComponent();
            Alphabet = new char[35] { 'A', 'Ą', 'B', 'C', 'Ć', 'D', 'E', 'Ę', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'Ł', 'M', 'N', 'Ń', 'O', 'Ó', 'P', 'Q', 'R', 'S', 'Ś', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'Ź', 'Ż' };
        }

        private void Rot13Btn_Click(object sender, RoutedEventArgs e)
        {
            InitialVar(out string cleanInput);
            if (cleanInput == "")
            {
                outputText.Text = "Invalid format, try again.";
            }
            else
            {
                string output = Encrypt(13, cleanInput);
                OutputValues(output, 13);
            }

        }
        private void RandomValBtn_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            InitialVar(out string cleanInput);
            if (cleanInput == "")
            {
                outputText.Text = "Invalid format, try again.";
            }
            else
            {
                int cipherValue = rnd.Next(2, Alphabet.Length - 2);
                string output = Encrypt(cipherValue, cleanInput);
                OutputValues(output, cipherValue);
            }
        }
        private void OutputValues(string output, int value)
        {
            outputText.Text = output;
            LastestCipherValue = value;
            LastestOutput = output;
        }
        private string Encrypt(int cipherValue, string input)
        {
            string output = "";
            for (int i = 0; i < input.Length; i++)
            {
                int index = Array.IndexOf(Alphabet, input[i]);
                index = (index + cipherValue) % 35;
                output += Alphabet[index];
            }
            return output;
        }
        private void InitialVar(out string cleanInput)
        {
            string input = inputText.Text;
            cleanInput = ClearInputFromUnwantedChars(input);
        }
        private string ClearInputFromUnwantedChars(string input)
        {
            string output = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsLetter(input[i]))
                {
                    output += char.ToUpper(input[i]);
                }
            }
            return output;
        }

        private void PrevValBtn_Click(object sender, RoutedEventArgs e)
        {
            if (LastestCipherValue != 0)
            {
                inputText.Text = LastestOutput;
                //string input = LastestOutput;
                //int cipValue = LastestCipherValue;
                string output = "";
                for(int i = 0; i < LastestOutput.Length; i++)
                {
                    int index = Array.IndexOf(Alphabet, LastestOutput[i]) - LastestCipherValue;
                    if (index >= 0)
                        output += Alphabet[index];
                    else
                    {
                        index += 35;
                        output += Alphabet[index];
                    }
                }
                outputText.Text = output;
                LastestOutput = output;              
            }
            else
            {
                outputText.Text = "No values has been encrypted yet.";
            }
        }
    }
}
