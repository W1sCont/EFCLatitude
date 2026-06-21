namespace SearchFile
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Semaphore
            
            Semaphore sem = new Semaphore(3, 3, "{A8EB7186-2C33-4E87-A809-9B1E469FE21C}");
            Semaphore s = Semaphore.OpenExisting("{A8EB7186-2C33-4E87-A809-9B1E469FE21C}");

            if (!s.WaitOne(0))
            {
                MessageBox.Show("Обмеження");
                return;
            }

            try
            {
                // To customize application configuration such as set high DPI settings or default font,
                // see https://aka.ms/applicationconfiguration.
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