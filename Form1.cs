using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Homework1
{
    public partial class Form1 : Form
    {
        enum NumberType {Zero, Digit, Teen, Dozen, Hundred };

        readonly Dictionary<string, int> digits = new Dictionary<string, int>(9)
        {
            { "one", 1},
            { "two", 2},
            { "three", 3},
            { "four", 4},
            { "five", 5},
            { "six", 6},
            { "seven", 7},
            { "eight", 8},
            { "nine", 9}
        };

        readonly Dictionary<string, int> teens = new Dictionary<string, int>(10)
        {
            ["ten"] = 10,
            ["eleven"] = 11,
            ["twelve"] = 12,
            ["thirteen"] = 13,
            ["fourteen"] = 14,
            ["fifteen"] = 15,
            ["sixteen"] = 16,
            ["seventeen"] = 17,
            ["eighteen"] = 18,
            ["nineteen"] = 19
        };

        readonly Dictionary<string, int> dozens = new Dictionary<string, int>(8)
        {
            ["twenty"] = 20,
            ["thirty"] = 30,
            ["forty"] = 40,
            ["fifty"] = 50,
            ["sixty"] = 60,
            ["seventy"] = 70,
            ["eighty"] = 80,
            ["ninety"] = 90
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button_Click(object sender, EventArgs e)
        {
            labelError.Visible = false;
            textBoxErrors.Visible = false;
            labelAnswer.Visible = false;
            textBoxErrors.Text = string.Empty;
            labelAnswer.Text = string.Empty;

            string s = textBoxInput.Text.Trim().ToLower();
            while (s.Contains("  "))
                s = s.Replace("  ", " ");

            string[] words = s.Split();

            if (words.Length == 0) return;

            foreach (string word in words)
            {
                if (!(word == "hundred" || word == "zero" || digits.ContainsKey(word) 
                    || teens.ContainsKey(word) || dozens.ContainsKey(word)))
                {
                    textBoxErrors.AppendText($"Орфографическая ошибка в слове '{word}'. \r\n");
                }
            }
            
            if (textBoxErrors.Text != string.Empty)
            {
                labelError.Visible = true;
                textBoxErrors.Visible = true;
                return;
            }

            int cnt = 0;
            NumberType curr = Typeof(words[0]);
            for (int i = 0; i < words.Length - 1; i++)
            {
                NumberType next = Typeof(words[i + 1]);

                switch (curr) 
                {
                    case NumberType.Zero:
                        textBoxErrors.AppendText($"Синтаксическая ошибка: после 'zero' не может ничего стоять. \r\n");
                        break;

                    case NumberType.Hundred:
                        cnt++;
                        if (next == NumberType.Zero)
                        {
                            textBoxErrors.AppendText($"Синтаксическая ошибка: 'zero' не может стоять после 'hundred'. \r\n");
                        }
                        else if (next == NumberType.Hundred)
                        {
                            textBoxErrors.AppendText($"Синтаксическая ошибка: повторение слова 'hundred'. \r\n");
                        }
                        break;

                    case NumberType.Digit:
                        if (next == NumberType.Zero)
                        {
                            textBoxErrors.AppendText($"Синтаксическая ошибка: 'zero' не может стоять после числа еденичного формата. \r\n");
                        }
                        else if (next == NumberType.Digit)
                        {
                            textBoxErrors.AppendText($"Синтаксическая ошибка: числа еденичного формата не могут стоять рядом. \r\n");
                        }
                        else if (next == NumberType.Teen)
                        {
                            textBoxErrors.AppendText($"Синтаксическая ошибка: после числа еденичного формата не может идти число формата 10-19. \r\n");
                        }
                        else if (next == NumberType.Dozen)
                        {
                            textBoxErrors.AppendText($"Синтаксическая ошибка: после числа еденичного формата не может идти число десятичного формата. \r\n");
                        }
                        break;

                    case NumberType.Teen:
                        if (next == NumberType.Zero)
                        {
                            textBoxErrors.AppendText($"Синтаксическая ошибка: 'zero' не может стоять после числа формата 10-19. \r\n");
                        }
                        else if (next == NumberType.Teen)
                        {
                            textBoxErrors.AppendText($"Синтаксическая ошибка: числа формата 10-19 не могут стоять рядом. \r\n");
                        }
                        else if (next == NumberType.Digit)
                        {
                            textBoxErrors.AppendText($"Синтаксическая ошибка: после числа формата 10-19 не может идти число еденичного формата. \r\n");
                        }
                        else if (next == NumberType.Dozen)
                        {
                            textBoxErrors.AppendText($"Синтаксическая ошибка: после числа формата 10-19 не может идти число десятичного формата. \r\n");
                        }
                        
                        for (int j = i + 1; j < words.Length; j++)
                        {
                            if (words[j] == "hundred")
                            {
                                textBoxErrors.AppendText($"Синтаксическая ошибка: число формата 10-19 не может стоять перед 'hundred'. \r\n");
                                break;
                            }
                        }
                        break;

                    case NumberType.Dozen:
                        if (next == NumberType.Zero)
                        {
                            textBoxErrors.AppendText($"Синтаксическая ошибка: 'zero' не может стоять после числа десятичного формата. \r\n");
                        }
                        else if (next == NumberType.Dozen)
                        {
                            textBoxErrors.AppendText($"Синтаксическая ошибка: числа десятичного формата не могут стоять рядом. \r\n");
                        }
                        else if (next == NumberType.Teen)
                        {
                            textBoxErrors.AppendText($"Синтаксическая ошибка: после числа десятичного формата не может идти число формата 10-19. \r\n");
                        }
                        for (int j = i + 1; j < words.Length; j++)
                        {
                            if (words[j] == "hundred")
                            {
                                textBoxErrors.AppendText($"Синтаксическая ошибка: число десятичного формата не может стоять перед 'hundred'. \r\n");
                                break;
                            }
                        }
                        break;
                }

                curr = next;
            }
            if (curr == NumberType.Hundred) 
                cnt++;
            if (cnt>1 && !textBoxErrors.Text.Contains("повторение слова 'hundred'."))
                textBoxErrors.AppendText("Синтаксическая ошибка: повторение слова 'hundred'. \r\n");

            if (textBoxErrors.Text != string.Empty)
            {
                labelError.Visible = true;
                textBoxErrors.Visible = true;
                return;
            }

            int ans = 0;
            foreach (string word in words)
            {
                switch (Typeof(word))
                {
                    case NumberType.Zero:
                        ans = 0;
                        break;

                    case NumberType.Digit:
                        ans += digits[word];
                        break;

                    case NumberType.Teen:
                        ans += teens[word];
                        break;

                    case NumberType.Dozen:
                        ans += dozens[word];
                        break;

                    case NumberType.Hundred:
                        ans *= 100;
                        break;

                }
            }
            labelAnswer.Text = ans.ToString(); 
            labelAnswer.Visible = true;
        }

        private NumberType Typeof(string word)
        {
            if (word == "zero")
                return NumberType.Zero;
            if (word == "hundred")
                return NumberType.Hundred;
            if (digits.ContainsKey(word))
                return NumberType.Digit;
            if (dozens.ContainsKey(word))
                return NumberType.Dozen;
            
            return NumberType.Teen;
        }

        private void TextBoxInput_TextChanged(object sender, EventArgs e)
        {
            button.Enabled = textBoxInput.Text.Trim().Length != 0;
        }
    }
}
