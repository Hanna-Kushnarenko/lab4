﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Affine_cipher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string allTextTocript;
        StringBuilder inputStrBldr;
        private Dictionary<string, double> frqncy = new Dictionary<string, double>();

        private void button2_Click(object sender, EventArgs e)
        {
                richTextBox2.Clear();
                inputStrBldr = new StringBuilder(richTextBox1.Text.ToLower());
                for (int i = 0; i < inputStrBldr.Length; i++)
                {
                    if (!alphabet.Contains(inputStrBldr[i]))
                    {
                        inputStrBldr.Remove(i, 1);
                        i--;
                    }
                }
            EncriptText();   
        }

        private void EncriptText()
        {
            int currentText;
            StringBuilder encryptedText = new StringBuilder();
            for (int i = 0; i < inputStrBldr.Length; i++)
            {
                currentText = (alphabet.IndexOf(inputStrBldr[i]) * Convert.ToInt32(textBox1.Text) + Convert.ToInt32(textBox2.Text)) % alphabet.Count;
                encryptedText.Append(alphabet.ElementAt(currentText));
            }
            richTextBox2.Text = encryptedText.ToString();
        }

        private void DecriptText(Tuple<int, int> aAndB)
        {
            int helper = 0;
            StringBuilder decryptedText = new StringBuilder();
            for (int i = 0; i < inputStrBldr.Length; i++)
            {
                helper = modInverse(aAndB.Item1, alphabet.Count) * (alphabet.IndexOf(inputStrBldr[i]) + alphabet.Count - aAndB.Item2) % alphabet.Count;
                decryptedText.Append(alphabet.ElementAt(helper));
            }
            richTextBox4.Text = decryptedText.ToString();
        }

        private int modInverse(int a, int n)
        {
            int i = n, 
                v = 0, 
                d = 1;
            while (a > 0)
            {
                int t = i / a, x = a;
                a = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }
            v %= n;
            if (v < 0) 
                v = (v + n) % n;
            return v;
        }

        private Tuple<int, int> SolveEqtn(List<string> twoCharWithMaxFrequency)
        {
            int index1char = alphabet.IndexOf(twoCharWithMaxFrequency[0].ToCharArray()[0]);
            int index2char = alphabet.IndexOf(twoCharWithMaxFrequency[1].ToCharArray()[0]);

            int indexFirstMaxFreqChar = alphabet.IndexOf(textBox3.Text.ToCharArray()[0]);
            int indexSecondMaxFreqChar = alphabet.IndexOf(textBox4.Text.ToCharArray()[0]);

            for (int i = 0; i < possibleAlpha.Count; i++)
            {
                for (int j = 0; j < possibleBeta.Count; j++)
                {
                    var solution1 = (possibleAlpha[i] * indexFirstMaxFreqChar + possibleBeta[j]) % alphabet.Count == index1char;
                    var solution2 = (possibleAlpha[i] * indexSecondMaxFreqChar + possibleBeta[j]) % alphabet.Count == index2char;

                    if (solution1 && solution2)
                    {
                        return new Tuple<int, int>(possibleAlpha[i], possibleBeta[j]);
                    }
                }
            }

            return null;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int count = 0;
            inputStrBldr = new StringBuilder(richTextBox3.Text);
            for (int i = 0; i < inputStrBldr.Length; i++)
            {
                count++;
                var key = inputStrBldr[i].ToString();
                if (frqncy.ContainsKey(key))
                {
                    frqncy[key]++;
                    continue;
                }
                frqncy.Add(key, 1);
            }

            var twoCharWithMaxFrequency = frqncy.OrderByDescending(f => f.Value).Select(f => f.Key).Take(2).ToList();

            var aAndB = SolveEqtn(twoCharWithMaxFrequency);
            if (aAndB == null)
            {
                MessageBox.Show("ERROR");
            }

            DecriptText(aAndB);
            textBox1.Text = aAndB.Item1.ToString();
            textBox2.Text = aAndB.Item2.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public static List<char> alphabet = new List<char>() { 'а', 'б', 'в', 'г', 'ґ', 'д', 'е',
                                                               'є', 'ж', 'з', 'и', 'і', 'ї', 'й',
                                                               'к', 'л', 'м', 'н', 'о', 'п', 'р',
                                                               'с', 'т', 'у', 'ф', 'х', 'ц', 'ч',
                                                               'ш', 'щ', 'ь', 'ю', 'я' };

        private readonly List<int> possibleAlpha = new List<int>() { 1, 2, 3, 4, 5, 7,
                                                                    8, 10, 13, 14, 16,
                                                                    17, 19, 20, 23, 25,
                                                                    26, 28, 29, 31, 32 };

        private readonly List<int> possibleBeta = new List<int>() { 0, 1, 2, 3, 4, 5,
                                                                    6, 7, 8, 9, 10, 11,
                                                                    12, 13, 14, 15, 16,
                                                                    17, 18, 19, 20, 21,
                                                                    22, 23, 24, 25, 26,
                                                                    27, 28, 29, 30, 31, 32 };

    }
}
