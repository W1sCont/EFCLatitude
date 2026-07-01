namespace MutexTask
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        
        private static Mutex mutex1;  // {EC1FF7B7-B982-4BE9-8FEA-74B145512954}
        private static Mutex mutex2;  // {D4936B34-9C8E-4FF4-8D6D-B9842FAB9909}
        private static Mutex mutex3;  // {112089F7-3393-48C1-BCFE-446487200940}
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            mutex1 = new Mutex(false, "{EC1FF7B7-B982-4BE9-8FEA-74B145512954}", out bool isSlot1Free);
            if (isSlot1Free) { Application.Run(new Form1()); return; }

            mutex2 = new Mutex(false, "{D4936B34-9C8E-4FF4-8D6D-B9842FAB9909}", out bool isSlot2Free);
            if (isSlot2Free) { Application.Run(new Form1()); return; }

            mutex3 = new Mutex(false, "{112089F7-3393-48C1-BCFE-446487200940}", out bool isSlot3Free);
            if (isSlot3Free) { Application.Run(new Form1()); return; }

            MessageBox.Show("Досягнуто ліміт копій! Дозволено одночасний запуск лише 3-х екземплярів застосунку.",
                            "Помилка запуску", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}