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

namespace Lab1Encryption
{
    
    public partial class MainWindow : Window
    {
        private char[,] Rect = new char[,]{ 
            {'а','б','в','г','д','е','ё','ж','з'},
            {'и','й','к','л','м','н','о','п','р'},
            {'с','т','у','ф','х','ц','ч','ш','щ'},
            {'ъ','ы','ь','э','ю','я','А','Б','В'},
            {'Г','Д','Е','Ё','Ж','З','И','Й','К'},
            {'Л','М','Н','О','П','Р','С','Т','У'},
            {'Ф','Х','Ц','Ч','Ш','Щ','Ъ','Ы','Ь'},
            {'Э','Ю','Я','.',',',':',' ','?','!'}};
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            Error_lbl.Content = "";
            string input = Input.Text;
            string buffer = "";
            int counter = 0;
            while (counter < input.Length)
            {
                bool flag = false;
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (input[counter] == Rect[i,j])
                        {
                            buffer += i.ToString() + j.ToString();
                            counter++;
                            flag = true;
                            break;
                        }

                        if (i == 7 && j == 8)
                        {
                            Error_lbl.Content = "Вы ошиблись с вводом, молодой человек";
                            return;
                        }
                        
                    }
                    if (flag) break;
                }
            }

            Output.Text = buffer;
        }

        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            string input = Input.Text;
            string buffer = "";
            int counter = 0;
            while (counter < input.Length / 2)
            {
                int i = input[counter * 2] - '0';
                int j = (int) input[counter * 2 + 1] - '0';
                buffer += Rect[i,j];
                counter++;
            }

            Output.Text = buffer;
        }
    }
}
