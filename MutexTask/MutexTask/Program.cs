namespace MutexTask
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        
        // {EC1FF7B7-B982-4BE9-8FEA-74B145512954}
        // {D4936B34-9C8E-4FF4-8D6D-B9842FAB9909}
        // {112089F7-3393-48C1-BCFE-446487200940}

        [STAThread]
        static void Main()
        {
            Semaphore sem = new Semaphore(3, 3, "{112089F7-3393-48C1-BCFE-446487200940}");
            Semaphore s = Semaphore.OpenExisting("{112089F7-3393-48C1-BCFE-446487200940}");
            if (!s.WaitOne(0))
            {
                MessageBox.Show("Досягнуто ліміт копій! Дозволено одночасний запуск лише 3-х екземплярів застосунку.",
                                 "Помилка запуску", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                ApplicationConfiguration.Initialize();
                Application.Run(new Form1());
            }
            finally
            {
                s.Release();
            }
        }
    }
}