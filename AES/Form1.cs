using System.ComponentModel.Design;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace AES
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public uint[] expansionKey;
        public byte[] key;
        int rounds;
        public byte[,] matrixSource = new byte[4, 4];

        static public byte[] Sbox = new byte[256]{
                                                0x63, 0x7C, 0x77, 0x7B, 0xF2, 0x6B, 0x6F, 0xC5, 0x30, 0x01, 0x67, 0x2B, 0xFE, 0xD7, 0xAB, 0x76,
                                                0xCA, 0x82, 0xC9, 0x7D, 0xFA, 0x59, 0x47, 0xF0, 0xAD, 0xD4, 0xA2, 0xAF, 0x9C, 0xA4, 0x72, 0xC0,
                                                0xB7, 0xFD, 0x93, 0x26, 0x36, 0x3F, 0xF7, 0xCC, 0x34, 0xA5, 0xE5, 0xF1, 0x71, 0xD8, 0x31, 0x15,
                                                0x04, 0xC7, 0x23, 0xC3, 0x18, 0x96, 0x05, 0x9A, 0x07, 0x12, 0x80, 0xE2, 0xEB, 0x27, 0xB2, 0x75,
                                                0x09, 0x83, 0x2C, 0x1A, 0x1B, 0x6E, 0x5A, 0xA0, 0x52, 0x3B, 0xD6, 0xB3, 0x29, 0xE3, 0x2F, 0x84,
                                                0x53, 0xD1, 0x00, 0xED, 0x20, 0xFC, 0xB1, 0x5B, 0x6A, 0xCB, 0xBE, 0x39, 0x4A, 0x4C, 0x58, 0xCF,
                                                0xD0, 0xEF, 0xAA, 0xFB, 0x43, 0x4D, 0x33, 0x85, 0x45, 0xF9, 0x02, 0x7F, 0x50, 0x3C, 0x9F, 0xA8,
                                                0x51, 0xA3, 0x40, 0x8F, 0x92, 0x9D, 0x38, 0xF5, 0xBC, 0xB6, 0xDA, 0x21, 0x10, 0xFF, 0xF3, 0xD2,
                                                0xCD, 0x0C, 0x13, 0xEC, 0x5F, 0x97, 0x44, 0x17, 0xC4, 0xA7, 0x7E, 0x3D, 0x64, 0x5D, 0x19, 0x73,
                                                0x60, 0x81, 0x4F, 0xDC, 0x22, 0x2A, 0x90, 0x88, 0x46, 0xEE, 0xB8, 0x14, 0xDE, 0x5E, 0x0B, 0xDB,
                                                0xE0, 0x32, 0x3A, 0x0A, 0x49, 0x06, 0x24, 0x5C, 0xC2, 0xD3, 0xAC, 0x62, 0x91, 0x95, 0xE4, 0x79,
                                                0xE7, 0xC8, 0x37, 0x6D, 0x8D, 0xD5, 0x4E, 0xA9, 0x6C, 0x56, 0xF4, 0xEA, 0x65, 0x7A, 0xAE, 0x08,
                                                0xBA, 0x78, 0x25, 0x2E, 0x1C, 0xA6, 0xB4, 0xC6, 0xE8, 0xDD, 0x74, 0x1F, 0x4B, 0xBD, 0x8B, 0x8A,
                                                0x70, 0x3E, 0xB5, 0x66, 0x48, 0x03, 0xF6, 0x0E, 0x61, 0x35, 0x57, 0xB9, 0x86, 0xC1, 0x1D, 0x9E,
                                                0xE1, 0xF8, 0x98, 0x11, 0x69, 0xD9, 0x8E, 0x94, 0x9B, 0x1E, 0x87, 0xE9, 0xCE, 0x55, 0x28, 0xDF,
                                                0x8C, 0xA1, 0x89, 0x0D, 0xBF, 0xE6, 0x42, 0x68, 0x41, 0x99, 0x2D, 0x0F, 0xB0, 0x54, 0xBB, 0x16
                                                };
        static public byte[] inverseSbox = new byte[256] {
                                                0x52, 0x09, 0x6A, 0xD5, 0x30, 0x36, 0xA5, 0x38, 0xBF, 0x40, 0xA3, 0x9E, 0x81, 0xF3, 0xD7, 0xFB,
                                                0x7C, 0xE3, 0x39, 0x82, 0x9B, 0x2F, 0xFF, 0x87, 0x34, 0x8E, 0x43, 0x44, 0xC4, 0xDE, 0xE9, 0xCB,
                                                0x54, 0x7B, 0x94, 0x32, 0xA6, 0xC2, 0x23, 0x3D, 0xEE, 0x4C, 0x95, 0x0B, 0x42, 0xFA, 0xC3, 0x4E,
                                                0x08, 0x2E, 0xA1, 0x66, 0x28, 0xD9, 0x24, 0xB2, 0x76, 0x5B, 0xA2, 0x49, 0x6D, 0x8B, 0xD1, 0x25,
                                                0x72, 0xF8, 0xF6, 0x64, 0x86, 0x68, 0x98, 0x16, 0xD4, 0xA4, 0x5C, 0xCC, 0x5D, 0x65, 0xB6, 0x92,
                                                0x6C, 0x70, 0x48, 0x50, 0xFD, 0xED, 0xB9, 0xDA, 0x5E, 0x15, 0x46, 0x57, 0xA7, 0x8D, 0x9D, 0x84,
                                                0x90, 0xD8, 0xAB, 0x00, 0x8C, 0xBC, 0xD3, 0x0A, 0xF7, 0xE4, 0x58, 0x05, 0xB8, 0xB3, 0x45, 0x06,
                                                0xD0, 0x2C, 0x1E, 0x8F, 0xCA, 0x3F, 0x0F, 0x02, 0xC1, 0xAF, 0xBD, 0x03, 0x01, 0x13, 0x8A, 0x6B,
                                                0x3A, 0x91, 0x11, 0x41, 0x4F, 0x67, 0xDC, 0xEA, 0x97, 0xF2, 0xCF, 0xCE, 0xF0, 0xB4, 0xE6, 0x73,
                                                0x96, 0xAC, 0x74, 0x22, 0xE7, 0xAD, 0x35, 0x85, 0xE2, 0xF9, 0x37, 0xE8, 0x1C, 0x75, 0xDF, 0x6E,
                                                0x47, 0xF1, 0x1A, 0x71, 0x1D, 0x29, 0xC5, 0x89, 0x6F, 0xB7, 0x62, 0x0E, 0xAA, 0x18, 0xBE, 0x1B,
                                                0xFC, 0x56, 0x3E, 0x4B, 0xC6, 0xD2, 0x79, 0x20, 0x9A, 0xDB, 0xC0, 0xFE, 0x78, 0xCD, 0x5A, 0xF4,
                                                0x1F, 0xDD, 0xA8, 0x33, 0x88, 0x07, 0xC7, 0x31, 0xB1, 0x12, 0x10, 0x59, 0x27, 0x80, 0xEC, 0x5F,
                                                0x60, 0x51, 0x7F, 0xA9, 0x19, 0xB5, 0x4A, 0x0D, 0x2D, 0xE5, 0x7A, 0x9F, 0x93, 0xC9, 0x9C, 0xEF,
                                                0xA0, 0xE0, 0x3B, 0x4D, 0xAE, 0x2A, 0xF5, 0xB0, 0xC8, 0xEB, 0xBB, 0x3C, 0x83, 0x53, 0x99, 0x61,
                                                0x17, 0x2B, 0x04, 0x7E, 0xBA, 0x77, 0xD6, 0x26, 0xE1, 0x69, 0x14, 0x63, 0x55, 0x21, 0x0C, 0x7D
                                                };
        static uint[] RCon = new uint[10]{
        0x01000000, 0x02000000, 0x04000000, 0x08000000, 0x10000000,
        0x20000000, 0x40000000, 0x80000000, 0x1B000000, 0x36000000
        };


        // --------------------- КНОПКА ШИФРОВАТЬ --------------------- \\
        private void button1_Click(object sender, EventArgs e)
        {
            GetKey();
            string sourceText = sourceTb.Text;
            cipherTb.Text = string.Empty;
            while (sourceText != String.Empty) // потом проверь со 
            {
                if (rbText.Checked) { matrixSource = TextToMatrix(ref sourceText); }
                else { matrixSource = NumberToMatrix(ref sourceText); }
                Cipher();
                cipherTb.Text += ToNumber(); 
            }
        }

        // --------------------- КНОПКА РАСШИФРОВАТЬ --------------------- \\
        private void button2_Click(object sender, EventArgs e)
        {
            GetKey();
            string sourceText = sourceTb.Text;
            cipherTb.Text = string.Empty;
            byte[,] matrix;
            while (sourceText != String.Empty) // потом проверь со 
            {
                matrix = NumberToMatrix(ref sourceText);
                matrixSource = matrix;
                DeCipher();
                if (rbText.Checked) { cipherTb.Text += ToString(); }
                else { cipherTb.Text += ToNumber(); }
            }
        }

        private byte[,] TextToMatrix(ref string sourceText)
        {
            byte[,] matrix = new byte[4, 4];
            int iterator = 0;

            // Заполняем матрицу построчно из строки
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    // Если еще не достигли конца строки, берем следующий символ
                    if (iterator < sourceText.Length)
                    {
                        matrix[j, i] = (byte)sourceText[iterator];
                        iterator++;
                    }
                    // Если строка закончилась, заполняем оставшиеся ячейки нулями
                    else matrix[j, i] = 0;
                }
            }

            // Удаляем из строки очередные первые 16 символов для дальнейших итераций
            sourceText = sourceText.Length > 16 ? sourceText.Substring(16) : "";
            return matrix;
        }


        private byte[,] NumberToMatrix(ref string sourceText)
        {
            byte[,] matrix = new byte[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (sourceText.Length == 0 || (sourceText.Length == 1 && sourceText[0] == ' '))
                    {
                        matrix[j, i] = 0;
                    }
                    else
                    {
                        matrix[j, i] = (byte)Convert.ToInt32(GetNumber(ref sourceText), 16);
                    }
                }
            }
            return matrix;
        }

        public string GetNumber(ref string sourceText)
        {
            StringBuilder number = new StringBuilder();
            for (int i = 0; i < sourceText.Length && sourceText[i] != ' '; i++)
            {
                number.Append(sourceText[i]);
            }
            //sourceText = sourceText.Length > 16 ? sourceText.Substring(16) : "";
            // урезаем
            sourceText = sourceText.Remove(0, Math.Min(number.Length + 1, sourceText.Length));
            return number.ToString();
        }



        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (matrixSource[j, i] != 0)
                    {
                        str += (char)matrixSource[j, i];
                    }
                }
            }
            return str;
        }

        public string ToNumber()
        {
            StringBuilder str = new StringBuilder();
            byte[] matrix = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    matrix[j] = matrixSource[j, i];
                }
                str.Append(BitConverter.ToString(matrix).Replace("-", " ") + " ");
            }
            return str.ToString().Replace('-', ' ');
        }


        // ----------------- ШИФР ----------------- \\
        public void Cipher()
        {
            KeyExpansion();
            AddRoundKey(0);
            for (int i = 1; i < rounds; i++)
            {
                SubBytes();
                ShiftRows();
                MixColumns();
                AddRoundKey(i);
            }
            SubBytes();
            ShiftRows();
            AddRoundKey(rounds);
        }

        public void DeCipher()
        {
            //cipherTb.Text = string.Empty;

            KeyExpansion();
            InverseAddRoundKey(0);
            for (int i = 1; i < rounds; i++)
            {
                InverseSubBytes(); 
                InverseShiftRows(); 
                InverseMixColumns(); 
                InverseAddRoundKey(i); 
            }

            InverseSubBytes();
            InverseShiftRows();
            InverseAddRoundKey(rounds);
        }

        public void GetKey()
        {
            key = new byte[textBox2.Text.Length];
            for (int i = 0; i < textBox2.Text.Length; i++)
            {
                key[i] = (byte)(textBox2.Text[i]);
            }
            rounds = textBox2.Text.Length;
            // кол-во раундов в зависимости от длины указанного ключа
            if (rounds == 16) { rounds = 10; }        // 128 бит
            else if (rounds == 24) { rounds = 12; }   // 192 бит
            else if (rounds == 32) { rounds = 14; }   // 256 бит
            else { rounds = 0; }

            keyLabel.Text = "Длина ключа = " + textBox2.Text.Length + " символов";
        }

        public void KeyExpansion()
        {
            expansionKey = new uint[4 * rounds + 4];

            // определение длины ключа в словах, каждое слово = 4 байта
            int keyLength = key.Length / 4;

            // заполним расширенный ключ исходным ключом
            for (int i = 0; i < keyLength; i++)
            {
                uint wordCreate = 0;
                // создание 4-байтного слова из 4 байтов исходного ключа
                for (int j = 0; j < 4; j++)
                {
                    // wordCreate << 8  - освобождение места для следующего байта
                    wordCreate = (wordCreate << 8) | key[i * 4 + j];
                }
                expansionKey[i] = wordCreate;
            }

            // генерация оставшихся слов расширенного ключа
            for (int i = keyLength; i < (rounds + 1) * 4; i++)
            {
                // если i кратно длине ключа, выполняется дополнительная операция
                if (i % keyLength == 0)
                {
                    // выполнение перестановки слова
                    uint temp = expansionKey[i - 1];
                    temp = (temp << 8) | (temp >> 24);
                    // применение замены байтов из Sbox к каждому байту слова
                    temp = (uint)Sbox[temp >> 24] << 24 | (uint)Sbox[(temp >> 16) & 0xFF] << 16 | (uint)Sbox[(temp >> 8) & 0xFF] << 8 | (uint)Sbox[temp & 0xFF];
                    // применение Rcon и XOR с предыдущим расширенным ключом
                    expansionKey[i] = expansionKey[i - keyLength] ^ temp ^ RCon[i / keyLength - 1];
                }
                else
                {
                    // применение XOR с предыдущим расширенным ключом
                    expansionKey[i] = expansionKey[i - keyLength] ^ expansionKey[i - 1];
                }
            }
        }

        public void AddRoundKey(int round)
        {
            for (int column = 0; column < 4; column++)
            {
                // Маска для выделения отдельных байтов из раундового ключа
                uint mask = 0xff000000;
                for (int row = 0; row < 4; row++)
                {
                    // Выделение отдельного байта из раундового ключа и применение маски
                    byte temp = (byte)(expansionKey[4 * round + column] & mask >> (8 * (3 - row)));
                    matrixSource[row, column] ^= temp;
                    // выбора следующего байта из ключа
                    mask = mask >> 8;
                }
            }
        }

        public void InverseAddRoundKey(int round)
        {
            round = rounds - round;
            for (int column = 0; column < 4; column++)
            {
                // маска для выделения отдельных байтов из раундового ключа
                uint mask = 0xff000000;
                for (int row = 0; row < 4; row++)
                {
                    // выделение отдельного байта из раундового ключа и применение маски
                    byte temp = (byte)(expansionKey[4 * round + column] & mask >> (8 * (3 - row)));
                    // для дешифровки нужно применить XOR с тем же значением из ключа
                    matrixSource[row, column] ^= temp;
                    // выбор следующего байта из ключа
                    mask = mask >> 8;
                }
            }
        }


        public void SubBytes()
        {
            for (int row = 0; row < 4; row++)
            {
                for (int column = 0; column < 4; column++)
                {
                    matrixSource[column, row] = Sbox[matrixSource[column, row]];
                }
            }
        }


        public void InverseSubBytes()
        {
            for (int row = 0; row < 4; row++)
            {
                for (int column = 0; column < 4; column++)
                {
                    matrixSource[column, row] = inverseSbox[matrixSource[column, row]];
                }
            }
        }


        public void ShiftRows()
        {

            byte[,] tempMatrix = new byte[4, 4];

            // Копируем первую строку без изменений
            for (int i = 0; i < 4; i++)
            {
                tempMatrix[0, i] = matrixSource[0, i];
            }

            tempMatrix[1, 0] = matrixSource[1, 1];
            tempMatrix[1, 1] = matrixSource[1, 2];
            tempMatrix[1, 2] = matrixSource[1, 3];
            tempMatrix[1, 3] = matrixSource[1, 0];

            tempMatrix[2, 0] = matrixSource[2, 2];
            tempMatrix[2, 1] = matrixSource[2, 3];
            tempMatrix[2, 2] = matrixSource[2, 0];
            tempMatrix[2, 3] = matrixSource[2, 1];

            tempMatrix[3, 0] = matrixSource[3, 3];
            tempMatrix[3, 1] = matrixSource[3, 0];
            tempMatrix[3, 2] = matrixSource[3, 1];
            tempMatrix[3, 3] = matrixSource[3, 2];
            matrixSource = tempMatrix;
        }

        public void InverseShiftRows() // проверить потом
        {
            byte[,] matrixCipher = new byte[4, 4];

            // Копируем первую строку без изменений
            for (int i = 0; i < 4; i++)
            {
                matrixCipher[0, i] = matrixSource[0, i];
            }

            // Сдвигаем остальные строки без циклов
            matrixCipher[1, 0] = matrixSource[1, 3];
            matrixCipher[1, 1] = matrixSource[1, 0];
            matrixCipher[1, 2] = matrixSource[1, 1];
            matrixCipher[1, 3] = matrixSource[1, 2];

            matrixCipher[2, 0] = matrixSource[2, 2];
            matrixCipher[2, 1] = matrixSource[2, 3];
            matrixCipher[2, 2] = matrixSource[2, 0];
            matrixCipher[2, 3] = matrixSource[2, 1];

            matrixCipher[3, 0] = matrixSource[3, 1];
            matrixCipher[3, 1] = matrixSource[3, 2];
            matrixCipher[3, 2] = matrixSource[3, 3];
            matrixCipher[3, 3] = matrixSource[3, 0];

            // Присваиваем измененную матрицу исходной матрице
            matrixSource = matrixCipher;
        }


        public void MixColumns()
        {
            byte[,] tmp = new byte[4, 4];
            byte[,] order4Matrix = new byte[4, 4] { {2, 3, 1, 1},
                                         {1, 2, 3, 1},
                                         {1, 1, 2, 3},
                                         {3, 1, 1, 2} }; // задана в методических указаниях как матрица 4го порядка
            Array.Copy(matrixSource, tmp, matrixSource.Length);

            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    byte result = 0;

                    // Вычисляем результат для каждого столбца в матрице
                    for (int k = 0; k < 4; ++k)
                    {
                        result ^= GaloisMultiply(order4Matrix[i, k], tmp[k, j]);
                    }

                    matrixSource[i, j] = result;
                }
            }
        }

        public void InverseMixColumns()
        {
            byte[,] tmp = new byte[4, 4];
            byte[,] inverseOrder4Matrix = new byte[4, 4] { { 0x0E, 0x0B, 0x0D, 0x09},
                                                   { 0x09, 0x0E, 0x0B, 0x0D},
                                                   { 0x0D, 0x09, 0x0E, 0x0B},
                                                   { 0x0B, 0x0D, 0x09, 0x0E} 
            };  // задана в методических указаниях как матрица 4го порядка
            Array.Copy(matrixSource, tmp, matrixSource.Length);

            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    byte result = 0;

                    // Вычисляем результат для каждого столбца в матрице
                    for (int k = 0; k < 4; ++k)
                    {
                        result ^= GaloisMultiply(inverseOrder4Matrix[i, k], tmp[k, j]);
                    }

                    matrixSource[i, j] = result;
                }
            }

        }


        byte GaloisMultiply(byte a, byte b)
        {
            byte p = 0; // хранить результат умножения
            byte counter; // переменная для счетчика цикла
            byte topBit; // переменная для хранения старшего бита a

            for (counter = 0; counter < 8; counter++) // 8 бит
            {
                // если младший бит b равен 1, выполняется операция xor с a и сохраняется в p
                if ((b & 1) == 1)
                    p ^= a;

                // получаем старший бит a
                topBit = (byte)(a & 0x80);

                // умножение на 2 в поле Галуа
                a <<= 1;

                // если старший бит a был установлен в 1, выполняется операция xor с полиномом 0x1b
                if (topBit == 1)
                    a ^= 0x1b;

                // сдвигаем b на один бит вправо, для проверки следующего бита на следующей итерации цикла
                b >>= 1;
            }
            return p;
        }
    }
}
