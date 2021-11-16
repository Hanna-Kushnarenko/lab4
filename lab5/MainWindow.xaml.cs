
using System.Windows;
using System.Windows.Controls;


namespace lab5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string variant = "";
        public string key = "";
        public string vector = "";
        public string inputText = "";
        private string outputtest ="";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (variant == "Encript")
            {
                Chilkat.Crypt2 crypt = new Chilkat.Crypt2();

                crypt.CryptAlgorithm = "aes";
                crypt.CipherMode = "ctr";
                crypt.KeyLength = 256;
                crypt.EncodingMode = "base64";
                crypt.SetEncodedKey(key, "ascii");
                crypt.SetEncodedIV(vector, "ascii");

                string encText = crypt.EncryptStringENC(inputText);
                MessageBox.Show(encText);
            }
            else
                if (variant == "Decript")
            {
                Chilkat.Crypt2 decrypt = new Chilkat.Crypt2();
                decrypt.CryptAlgorithm = "aes";
                decrypt.CipherMode = "ctr";
                decrypt.KeyLength = 256;
                decrypt.EncodingMode = "base64";
                decrypt.SetEncodedIV(vector, "ascii");
                decrypt.SetEncodedKey(key, "ascii");
                string decText = decrypt.DecryptStringENC(inputText);
                MessageBox.Show(decText);
            }
        }

       
    
       private void Key_TextChanged(object sender, TextChangedEventArgs e)
       {
             TextBox textBox = (TextBox)sender;
             key=textBox.Text;
       }
       private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
       {
            ComboBox comboBox = (ComboBox)sender;
            TextBlock selectedItem = (TextBlock)comboBox.SelectedItem;
            variant = selectedItem.Text;
       }

        private void Vector_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            TextBox textBox = (TextBox)sender;
            vector = textBox.Text;
        }

        private void Input_TextChenged(object sender, TextChangedEventArgs e)
        {

            TextBox textBox = (TextBox)sender;
            inputText = textBox.Text;

        }
    }
    
}
