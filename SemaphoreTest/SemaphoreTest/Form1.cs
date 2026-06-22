using System.ComponentModel;

namespace SemaphoreTest
{
    public partial class Form1 : Form
    {
        public Semaphore _semaphore;
        BindingList<MyThreadItem> creatList;
        BindingList<MyThreadItem> waitList;
        BindingList<MyThreadItem> workList;
        public SynchronizationContext uiContext;

        private int _oldValue = 0;
        private int _maxValueSemaphore = 23;

        public Form1()
        {
            InitializeComponent();
            uiContext = SynchronizationContext.Current;

            creatList = new BindingList<MyThreadItem>();
            waitList = new BindingList<MyThreadItem>();
            workList = new BindingList<MyThreadItem>();

            listBox1.DataSource = workList;
            listBox2.DataSource = waitList;
            listBox3.DataSource = creatList;

            Text = "Тест семафору";
            label1.Text = "Працюючі потоки";
            label2.Text = "Очікуючі потоки";
            label3.Text = "Створені потоки";
            label4.Text = "К-сть місць у семафорі";
            button1.Text = "Створити потік";
            numericUpDown1.Value = 0;

            _semaphore = new Semaphore(0, _maxValueSemaphore, "{47552548-0610-4C28-8378-5FB1F1832096}");
        }

        public void Work(MyThreadItem selectedItem)
        {
            _semaphore.WaitOne();
            uiContext.Send(i => { workList.Add(selectedItem); waitList.Remove(selectedItem); }, null);

            while (!selectedItem.IsRunning.WaitOne(0))
            {
                selectedItem.Counter++;
                Thread.Sleep(1000);
                uiContext.Send(i => workList.ResetBindings(), null);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            creatList.Add(new MyThreadItem());
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int diff = Math.Abs((int)numericUpDown1.Value - _oldValue);
                if (numericUpDown1.Value >= 0 && numericUpDown1.Value < _maxValueSemaphore)
                {
                    if (numericUpDown1.Value > _oldValue)
                    {
                        _semaphore.Release(diff);
                    }
                    else if (numericUpDown1.Value < _oldValue)
                    {
                        for (int i = 0; i < diff; i++)
                        {
                            if (workList.Count > 0)
                            {
                                var oldest = workList[0];
                                oldest.IsRunning.Set();
                                workList.Remove(oldest);
                            }
                        }
                    }
                    _oldValue = (int)numericUpDown1.Value;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void listBox3_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                MyThreadItem selectedItem = (MyThreadItem)listBox3.SelectedItem;
                if (selectedItem != null)
                {
                    waitList.Add(selectedItem);
                    creatList.Remove(selectedItem);

                    Task.Run(() => Work(selectedItem));
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if(workList.Count > 0)
                {
                    MyThreadItem selectedItem = (MyThreadItem)listBox1.SelectedItem;
                    if (selectedItem != null)
                    {
                        selectedItem.IsRunning.Set();
                        workList.Remove(selectedItem);
                        _semaphore.Release();
                    }
                }
            }
            catch (SemaphoreFullException) { MessageBox.Show("Семафор уже заповнений!"); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
