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

namespace Lab3Encryption
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
        private Matrix keyMatrix = new Matrix(3, 3);
        private Matrix inverseKeyMatrix;

        private double[,] array = new double[3, 3]
        {
            {1,3,2},
            {2,1,5},
            {3,2,1}
        };
        
        public MainWindow()
        {
            InitializeComponent();
            keyMatrix.SetField(array);
            inverseKeyMatrix = keyMatrix.Inverse();
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

                if (flag)
                {
                    ErrorTB.Content = "Введен пропащий символ";
                    keyList.Clear();
                    return keyList;
                }
            }

            return keyList;
        }
        private List<Matrix> CreateMatrixArr(string text)
        {
            List<Matrix> result = new List<Matrix>();
            List<int> intList = GetIntListFromString(text);
            if (intList.Count == 0) return result;
            int countTriplets = (text.Length + 2) / 3;
            for (int i = 0; i < countTriplets; i++)
            {
                Matrix tmp = new Matrix(3, 1);
                if (i == countTriplets - 1 && text.Length % 3 != 0)
                {
                    int remain = text.Length % 3;
                    for (int j = 0; j < remain; j++)
                    {
                        tmp[j, 0] = intList[i * 3 + j];
                    }
                    for (int j = remain; j < 3; j++)
                    {
                        tmp[j, 0] = 69;
                    }
                }
                else
                {
                    for (int j = 0; j < 3; j++)
                    {
                        tmp[j, 0] = intList[i * 3 + j];
                    }
                }
                
                result.Add(tmp);
            }

            return result;
        }
        private string CreateString(List<Matrix> list)
        {
            string result = "";
            foreach (var elem in list)
            {
                for (int i = 0; i < 3; i++)
                {
                    result += elem[i, 0] + " ";
                }
            }

            return result;
        }
        private string CreateStringABC(List<Matrix> list)
        {
            string result = "";
            foreach (var elem in list)
            {
                for (int i = 0; i < 3; i++)
                {
                    result += Rect[(int)(elem[i, 0]+0.5)];
                }
            }

            return result;
        }
        private void Encrypt_Btn_Click(object sender, RoutedEventArgs e)
        {
            List<Matrix> m = CreateMatrixArr(TextEnter_TB.Text);
            if(m.Count == 0) return;
            List<Matrix> result = new List<Matrix>();
            foreach (var matrix in m)
            {
                result.Add(keyMatrix*matrix);
            }

            TextResult_TB.Text = CreateString(result);
        }

        private void Decrypt_Btn_Click(object sender, RoutedEventArgs e)
        {
            string[] input = TextEnter_TB.Text.Split(' ');
            if (input.Length % 3 != 0)
            {
                ErrorTB.Content = "Длина введенного текста не верна";
                return;
            }
            int[] intArr = new int[input.Length];
            try
            {
                for (int i = 0; i < input.Length; i++)
                {
                    intArr[i] = Int32.Parse(input[i]);
                }
            }
            catch (Exception ex)
            {
                ErrorTB.Content = ex.Message;
            }
            List<Matrix> matrixList = new List<Matrix>();
            List<Matrix> result = new List<Matrix>();
            for (int i = 0; i < intArr.Length/3; i++)
            {
                Matrix m = new Matrix(3,1);
                for (int j = 0; j < 3; j++)
                {
                    m[j, 0] = intArr[i * 3 + j];
                }
                matrixList.Add(m);
            }

            foreach (var elem in matrixList)
            {
                result.Add(inverseKeyMatrix*elem);
            }

            TextResult_TB.Text = CreateStringABC(result);
        }
    }
}
