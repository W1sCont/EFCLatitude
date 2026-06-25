using System.Text;
namespace XOR
{
    public partial class Form1 : Form
    {
        private string _stringSource;
        private byte[] _inputBytes;
        private byte[] _result;
        private byte[] _keyBytes;
        private string _path;
        private string _folder;
        private string _fileName;
        private string _extension;

        private SynchronizationContext uiContext;
        private CancellationTokenSource tokenSource;
        private CancellationToken token;
        public Form1()
        {
            InitializeComponent();
            uiContext = SynchronizationContext.Current;

            Text = "Алгоритм шифрування XOR";
            label1.Text = "Ключ";
            radioButton1.Text = "Шифрувати";
            radioButton1.Checked = true;
            radioButton2.Text = "Розшифрувати";
            button1.Text = "Файл...";
            button2.Text = "Пуск";
            button3.Text = "Скасування";
            textBox2.PasswordChar = '*';
        }

        private async Task XorAsync(CancellationToken token)
        {
            await Task.Run(() =>
            {
                int lastPercent = 0;
                for (int i = 0; i < _inputBytes.Length; i++)
                {
                    token.ThrowIfCancellationRequested();
                    _result[i] = (byte)(_inputBytes[i] ^ _keyBytes[i % _keyBytes.Length]);
                    int curentPercent = (int)(((double)i / _inputBytes.Length) * 100);

                    if (curentPercent > lastPercent)
                    {
                        lastPercent = curentPercent;
                        uiContext.Post(state => { progressBar1.Value = (int)state; }, curentPercent);
                    }
                }
            }, token);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _stringSource = openFileDialog1.FileName;
                textBox1.Text = _stringSource;
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Виберіть файл!");
                return;
            }
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Введіть ключ шифрування!");
                return;
            }
            try
            {
                tokenSource = new CancellationTokenSource();
                token = tokenSource.Token;

                _inputBytes = File.ReadAllBytes(textBox1.Text.Trim());
                _result = new byte[_inputBytes.Length];

                _keyBytes = Encoding.UTF8.GetBytes(textBox2.Text.Trim());

                _folder = Path.GetDirectoryName(_stringSource);
                _fileName = Path.GetFileNameWithoutExtension(_stringSource);
                _extension = Path.GetExtension(_stringSource);

                await XorAsync(token);
                progressBar1.Value = 100;
                if (radioButton1.Checked)
                {
                    _path = Path.Combine(_folder, $"{_fileName}_encrypted{_extension}");
                }
                else if (radioButton2.Checked)
                {
                    if (_fileName.EndsWith("_encrypted", StringComparison.OrdinalIgnoreCase))
                    {
                        string cleanName = _fileName.Substring(0, _fileName.Length - "_encrypted".Length);
                        _path = Path.Combine(_folder, $"{cleanName}_decrypted{_extension}");
                    }
                    else
                    {
                        _path = Path.Combine(_folder, $"{_fileName}_decrypted{_extension}");
                    }
                }
                File.WriteAllBytes(_path, _result);
                MessageBox.Show("Файл успішно оброблено!");
            }
            catch (OperationCanceledException) 
            {
                MessageBox.Show("Операцію скасовано користувачем.");
                progressBar1.Value = 0;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tokenSource?.Cancel();
        }
    }
}
