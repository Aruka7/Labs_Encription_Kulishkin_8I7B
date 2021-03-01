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

namespace Lab2Encryption
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private char[] Rect = new char[]{
            'а','б','в','г','д','е','ё','ж','з',
            'и','й','к','л','м','н','о','п','р',
            'с','т','у','ф','х','ц','ч','ш','щ',
            'ъ','ы','ь','э','ю','я','А','Б','В',
            'Г','Д','Е','Ё','Ж','З','И','Й','К',
            'Л','М','Н','О','П','Р','С','Т','У',
            'Ф','Х','Ц','Ч','Ш','Щ','Ъ','Ы','Ь',
            'Э','Ю','Я','.',',',':',' ','?','!'};
        public MainWindow()
        {
            InitializeComponent();
        }

        private List<int> GetIntListFromString(string str)
        {
            List<int> keyList = new List<int>();
            bool flag;
            foreach (var c in str)
            {
                flag = true;
                for (int i = 0; i < 72; i++)
                {
                    if (c == Rect[i])
                    {
                        flag = false;
                        keyList.Add(i);
                    }
                }

                if (flag == true)
                {
                    ErrorTB.Content = "Введен пропащий символ";
                    keyList.Clear();
                    return keyList;
                }
            }

            return keyList;
        }

        private void Encypt_BTN_Click(object sender, RoutedEventArgs e)
        {
            ErrorTB.Content = "";
            string enterText = EncryptEnter_TB.Text;
            string key = Key_TB.Text;
            if (Key_TB.Text.Length < 1)
            {
                ErrorTB.Content = "Вы не ввели ключ";
                return;
            }
            if (EncryptEnter_TB.Text.Length < 1)
            {
                ErrorTB.Content = "Вы не ввели текст";
                return;
            }
            
            
            string result = "";
            List<int> enterTextint = GetIntListFromString(enterText);
            List<int> keyList = GetIntListFromString(key);
            if (enterTextint.Count == 0 || keyList.Count == 0)
            {
                return;
            }
            int counter = 0;
            foreach (var v in enterTextint)
            {
                result += Rect[(v + keyList[counter % keyList.Count]) % 72];
                counter++;
            }

            EncryptResult_TB.Text = result;
        }

        private void Decrypt_BTN_Click(object sender, RoutedEventArgs e)
        {
            ErrorTB.Content = "";
            string enterText = EncryptEnter_TB.Text;
            string key = Key_TB.Text;
            if (Key_TB.Text.Length < 1)
            {
                ErrorTB.Content = "Вы не ввели ключ";
                return;
            }
            if (EncryptEnter_TB.Text.Length < 1)
            {
                ErrorTB.Content = "Вы не ввели текст";
                return;
            }
            string result = "";
            List<int> enterTextint = GetIntListFromString(enterText);
            List<int> keyList = GetIntListFromString(key);
            if (enterTextint.Count == 0 || keyList.Count == 0)
            {
                return;
            }
            int counter = 0;
            foreach (var v in enterTextint)
            {
                int tmp = v - keyList[counter % keyList.Count];
                counter++;
                if (tmp < 0) tmp += 72;
                result += Rect[tmp];
            }
            EncryptResult_TB.Text = result;
        }
    }
}
