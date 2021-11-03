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

namespace КС_1
{ 
    public partial class Form1 : Form
    {
    
        private Color[,] cellColors = null;
        private Random rnd = new Random();
        public Dictionary<char, double> characters = new Dictionary<char, double>();
        public SortedDictionary<char, double> sortedDicForOne = new SortedDictionary<char, double>();
        public Dictionary<char, double> sortedCharacters = new Dictionary<char, double>();

        public Dictionary<string, double> twoCharacters = new Dictionary<string, double>();
        public SortedDictionary<string, double> sortedDicForTwo = new SortedDictionary<string, double>();
        public Dictionary<string, double> sortedCharacters2 = new Dictionary<string, double>();

        public Dictionary<string, double> threeCharacters = new Dictionary<string, double>();
        public SortedDictionary<string, double> sortedDicForThree = new SortedDictionary<string, double>();

        public int quintity = 0;
        public double filesize=0;
        public double datasize;
        public Form1()
        {
            InitializeComponent();
            dataGridView1.ColumnCount = 2;
            dataGridView1.Columns[0].Name = "Char";
            dataGridView1.Columns[1].Name = "quantity";
            tableLayoutPanel1.Hide();

            if (richTextBox1.Text=="")
            {
                dataGridView1.Rows.Clear();
            }

        }

        private void symbol1button_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Hide();
            dataGridView1.Rows.Clear();
            characters.Clear();
            sortedCharacters.Clear();
            sortedDicForOne.Clear();
            chart1.Series["Series1"].Points.Clear();
            quintity = MakeDictionary(richTextBox1.Text, characters);
            Calc_probability(characters,quintity);
            sortedDicForOne = new SortedDictionary<char, double>(characters);
            foreach (var char_ in sortedDicForOne)
            {
                dataGridView1.Rows.Add(char_.Key, char_.Value);
                sortedCharacters.Add(char_.Key, char_.Value);
            }
            createGraf(characters);


        }
        private void symbo21button_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Hide();
            dataGridView1.Rows.Clear();
            twoCharacters.Clear();
            dataGridView1.Columns[0].Name = "2 Chars";
            chart1.Series["Series1"].Points.Clear();
            quintity = MakeDictionaryforTwo(richTextBox1.Text, twoCharacters);
            Calc_probabilityForString(twoCharacters, quintity);
            foreach (var char_ in twoCharacters)
            {
                dataGridView1.Rows.Add(char_.Key, char_.Value);
            }
            createGrafString(twoCharacters);

        }
        private void symbol3button_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Hide();
            dataGridView1.Rows.Clear();
            threeCharacters.Clear();
            dataGridView1.Columns[0].Name = "3 Chars";
            chart1.Series["Series1"].Points.Clear();
            quintity = MakeDictionaryforThree(richTextBox1.Text, threeCharacters);
            Calc_probabilityForString(threeCharacters, quintity);
            foreach (var char_ in threeCharacters)
            {
                dataGridView1.Rows.Add(char_.Key, char_.Value);
            }
            createGrafString(threeCharacters);
        }
        public static int MakeDictionary(string text, Dictionary<char, double> characters)
        {
            char[] CharArray = text.ToCharArray();
            int allquintity = 0;
            for (int i = 0; i < CharArray.Length; i++)
            {
                if(Char.IsLetter(CharArray[i]) || CharArray[i]==' ')
                {
                    if (characters.ContainsKey(CharArray[i]))
                    {
                        characters[CharArray[i]]++;
                    }
                    else
                    {
                        characters.Add(CharArray[i], 1);
                    }
                    allquintity++;
                }
                
            }
            return allquintity;

        }
        public static int MakeDictionaryforTwo(string text, Dictionary<string, double> twoCharacters)
        {
            char[] CharArray = text.ToCharArray();
            List<string> twoCharsList = new List<string>();
            for (int i = 0; i <= CharArray.Count() - 1; i++)
            {
                if (CharArray[i] != ' ' && !Char.IsLetter(CharArray[i]))
                {
                    CharArray[i] = ' ';
                }
            }
            for (int i = 0; i<= CharArray.Count()-2;)
            {
                char[] chars = { CharArray[i], CharArray[i + 1] };
                string twoChars = new string(chars);
                twoCharsList.Add(twoChars);
                i = i + 2;
            }
            int allquintity = 0;
            for (int i = 0; i < twoCharsList.Count; i++)
            {
                    if (twoCharacters.ContainsKey(twoCharsList[i]))
                    {
                        twoCharacters[twoCharsList[i]]++;
                    }
                    else
                    {
                        twoCharacters.Add(twoCharsList[i], 1);
                    }
                    allquintity++;

            }
            return allquintity;

        }
        public static int MakeDictionaryforThree(string text, Dictionary<string, double> threeCharacters)
        {

            char[] CharArray = text.ToCharArray();
            List<string> threeCharsList = new List<string>();
            for (int i = 0; i <= CharArray.Count() - 1; i++)
            {
                if (CharArray[i] != ' ' && !Char.IsLetter(CharArray[i]))
                {
                    CharArray[i] = ' ';
                }
            }
            for (int i = 0; i <= CharArray.Count() - 3;)
            {
                char[] chars = { CharArray[i], CharArray[i + 1], CharArray[i + 3] };
                string twoChars = new string(chars);
                threeCharsList.Add(twoChars);
                i = i + 3;
            }
            int allquintity = 0;
            for (int i = 0; i < threeCharsList.Count; i++)
            {
                if (threeCharacters.ContainsKey(threeCharsList[i]))
                {
                    threeCharacters[threeCharsList[i]]++;
                }
                else
                {
                    threeCharacters.Add(threeCharsList[i], 1);
                }
                allquintity++;

            }
            return allquintity;
        }
        public void Calc_probability(Dictionary<char, double> characters, int allquintity)
        {
            int countKeys = characters.Keys.Count;
            char[] keysDict = new char[countKeys];
            characters.Keys.CopyTo(keysDict, 0);
            for (int i = 0; i < characters.Count; i++)
            {
               characters[keysDict[i]]= characters[keysDict[i]]/allquintity;
            }
        }
        public void Calc_probabilityForString(Dictionary<string, double> twoCharacters, int allquintity)
        {
            int countKeys = twoCharacters.Keys.Count;
            string[] keysDict = new string[countKeys];
            twoCharacters.Keys.CopyTo(keysDict, 0);
            for (int i = 0; i < twoCharacters.Count; i++)
            {
                twoCharacters[keysDict[i]] = twoCharacters[keysDict[i]] / allquintity;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
                richTextBox1.Text = fileContent;
                FileInfo fileSize = new FileInfo(filePath);
                filesize = (fileSize.Length * 8);
            }

            MessageBox.Show(fileContent, "File Content at path: " + filePath, MessageBoxButtons.OK);
            
        }
        private void createGraf(Dictionary<char, double> characters)
        {
            chart1.Show();
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.Series["Series1"].IsXValueIndexed = true;
            chart1.Series["Series1"].IsValueShownAsLabel = true;
            int countKeys = characters.Keys.Count;
            char[] keysDict = new char[countKeys];
            characters.Keys.CopyTo(keysDict, 0);
            int forCount = characters.Count;
            if (forCount > 30)
            {
                forCount = 30;
            }
            for (int i = 0; i < forCount; i++)
            {
                chart1.Series["Series1"].Points.AddXY(keysDict[i].ToString(), Math.Round(characters[keysDict[i]],3));
            }

        }
        private void createGrafString(Dictionary<string, double> characters)
        {
            chart1.Show();
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.Series["Series1"].IsXValueIndexed = true;
            chart1.Series["Series1"].IsValueShownAsLabel = true;
            int forCount = characters.Count;
            if (forCount > 30)
            {
                forCount = 30;
            }
            for (int i = 0; i < forCount; i++)
            {
                chart1.Series["Series1"].Points.AddXY(characters.Keys.ElementAt(i), Math.Round(characters.Values.ElementAt(i), 3));
            }

        }

        private void SymbolCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            tableLayoutPanel1.Hide();
            if (SymbolCombobox.SelectedItem.ToString() == "sort alphabetically")
            {
                chart1.Series["Series1"].Points.Clear();
                createGraf(sortedCharacters);

            }
            if(SymbolCombobox.SelectedItem.ToString() == "frequency increase")
            {
                chart1.Series["Series1"].Points.Clear();
                Dictionary<char, double> increaseSortedCharacters = getfrequencyIncrease(characters);
                createGraf(increaseSortedCharacters);
            }
            if(SymbolCombobox.SelectedItem.ToString() == "frequency decrease")
            {
                chart1.Series["Series1"].Points.Clear();
                Dictionary<char, double> decreaseSortedCharacter = getfrequencyDecrease(characters);
                createGraf(decreaseSortedCharacter);
            }
        }
        private void SymbolCombobox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            tableLayoutPanel1.Hide();
            if (SymbolCombobox2.SelectedItem.ToString() == "30 most likely")
            {
                Dictionary<string, double> increaseSortedCharacters = getfrequencyIncreaseString(twoCharacters);
                chart1.Series["Series1"].Points.Clear();
                createGrafString(increaseSortedCharacters);

            }
            if (SymbolCombobox2.SelectedItem.ToString() == "matrix")
            {
                chart1.Series["Series1"].Points.Clear();
                chart1.Hide();
                makeMatrix();

            }
        }
 
        private void SymbolCombobox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            tableLayoutPanel1.Hide();
            if (SymbolCombobox3.SelectedItem.ToString() == "30 most likely")
            {
                Dictionary<string, double> increaseSortedCharacters = getfrequencyIncreaseString(threeCharacters);
                chart1.Series["Series1"].Points.Clear();
                createGrafString(increaseSortedCharacters);

            }
        }
        private Dictionary<char,double> getfrequencyIncrease(Dictionary<char, double> characters)
        {
            Dictionary<char, double> increaseSortedCharacters = new Dictionary<char, double>();
            foreach (KeyValuePair<char, double> pare in characters.OrderBy(key => key.Value))
            {
                increaseSortedCharacters.Add(pare.Key, pare.Value);
            }
            return increaseSortedCharacters;
        }
        private Dictionary<string, double> getfrequencyIncreaseString(Dictionary<string,double> characters)
        {
            Dictionary<string, double> increaseSortedCharacters = new Dictionary<string, double>();
            Dictionary<string, double> decreaseSortedCharacters = new Dictionary<string, double>();
            foreach (KeyValuePair<string, double> pare in characters.OrderBy(key => key.Value))
            {
                increaseSortedCharacters.Add(pare.Key, pare.Value);
            }
            foreach (var item in increaseSortedCharacters.Reverse())
            {
                decreaseSortedCharacters.Add(item.Key, item.Value);
            }
            return decreaseSortedCharacters;
        }

        private Dictionary<char, double> getfrequencyDecrease(Dictionary<char, double> characters)
        {
            Dictionary<char, double> SortedCharacters = getfrequencyIncrease(characters);
            Dictionary<char, double> decreaseSortedCharacters = new Dictionary<char, double>();
            foreach (var item in SortedCharacters.Reverse())
            {
                decreaseSortedCharacters.Add(item.Key, item.Value);
            }
            return decreaseSortedCharacters;
        }

        private void makeMatrix()
        {
            tableLayoutPanel1.Show();
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.Refresh();
            //tableLayoutPanel1.Hide();
            List<double> quantList = new List<double>();
            char[] alphabet ={ 'а','б','в','г','ґ', 'д','е','є','ж', 'з', 'и', 'і', 'ї','й', 'к', 'л', 'м', 'н', 'о', 'п','р', 'с', 'т', 'у','ф','х', 'ц', 'ч','ш','щ','ь','ю','я'};
            string[,] matrix = new string[34,34];
            for (int i = 1; i < 34; i++)
            {
                matrix[0, i] = alphabet[i-1].ToString();
            }
            for (int i = 1; i < 34; i++)
            {
                matrix[i, 0] = alphabet[i-1].ToString();
            }
            var columns = tableLayoutPanel1.ColumnCount;
            var rows = tableLayoutPanel1.RowCount;
            for (int i = 1; i < columns; i++)
            {
                Label l = new Label();
                l.Text = matrix[0, i];
                tableLayoutPanel1.Controls.Add(l, 0, i);
            }
            for (int i = 1; i < rows; i++)
            {
                Label l = new Label();
                l.Text = matrix[i, 0];
                tableLayoutPanel1.Controls.Add(l, i, 0);
            }
            tableLayoutPanel1.Refresh();

            cellColors = new Color[columns, rows];
            for (int i = 1; i < columns; i++)
            {
                for (int j = 1; j < rows; j++)
                {
                    char[] chars = { alphabet[i-1], alphabet[j-1] };
                    string twoChars = new string(chars);
                    if (twoCharacters.ContainsKey(twoChars))
                    {
                        quantList.Add(twoCharacters[twoChars]);
                       // matrix[i, j];
                        //cellColors[i, j] = GetRandomColor();
                    }
                }

            }
            quantList.Sort();
            for (int i = 1; i < columns; i++)
            {
                for (int j = 1; j < rows; j++)
                {
                    char[] chars = { alphabet[i - 1], alphabet[j - 1] };
                    string twoChars = new string(chars);
                    if (twoCharacters.ContainsKey(twoChars))
                    {
                        cellColors[i, j] = GetRandomColor(twoCharacters[twoChars], quantList[0], quantList[quantList.Count-1]);
                    }
                    else
                    {
                        cellColors[i, j] = GetRandomColor(0.00, quantList[0], quantList[quantList.Count - 1]);
                    }
                }

            }
            tableLayoutPanel1.Refresh();

        }
        private Color GetRandomColor(double currvalue, double minvalue,double maxvalue)
        {
            double step = (maxvalue - currvalue) / 7;
            int f= 0, s = 0, t = 0;
            if(currvalue == minvalue)
            {
                f = 0;
                s = 4;
                t = 57;
            }
            if (currvalue == 0.0)
            {
                f = 0;
                s = 1;
                t = 8;
            }
            if(currvalue>minvalue && currvalue<= (maxvalue - 7 * step))
            {
                f = 0;
                s = 10;
                t = 131;
            }
            if (currvalue > (maxvalue - 7 * step) && currvalue <= (maxvalue - 6 * step))
            {
                f = 1;
                s = 15;
                t = 204;
            }
            if (currvalue > (maxvalue - 6 * step) && currvalue <= (maxvalue - 5 * step))
            {
                f = 0;
                s = 0;
                t = 255;
            }
            if (currvalue > (maxvalue - 5 * step) && currvalue <= (maxvalue - 4 * step))
            {
                f = 31;
                s = 31;
                t = 255;
            }
            if (currvalue > (maxvalue - 4 * step) && currvalue <= (maxvalue - 3 * step))
            {
                f = 73;
                s = 73;
                t = 255;
            }
            if (currvalue > (maxvalue - 3 * step) && currvalue <= (maxvalue - 2 * step))
            {
                f = 120;
                s = 121;
                t = 255;
            }
            if (currvalue > (maxvalue - 2 * step) && currvalue <= (maxvalue - 1 * step))
            {
                f = 163;
                s = 163;
                t = 225;
            }
            if (currvalue > (maxvalue -  step) && currvalue <= maxvalue)
            {
                f = 191;
                s = 191;
                t = 255;
            }

            return Color.FromArgb(f, s, t);
        }
        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (cellColors != null)
            {
                var color = cellColors[e.Column, e.Row];
                e.Graphics.FillRectangle(new SolidBrush(color), e.CellBounds);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        
    }
}
