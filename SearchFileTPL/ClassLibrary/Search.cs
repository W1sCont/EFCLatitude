using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ClassLibrary
{
    public class Search
    {
        public static void FindTextInFiles(Regex regText, DirectoryInfo di, Regex regMask, bool chekBox, ManualResetEvent thread_pause, CancellationToken token, Action<string, string, string, string> fileHandler)
        {
            bool chek = chekBox;

            FileInfo[] fi = null;
            try
            {
                fi = di.GetFiles();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            try
            {
                foreach (FileInfo f in fi)
                {
                    token.ThrowIfCancellationRequested();
                    thread_pause.WaitOne();

                    if (regMask.IsMatch(f.Name))
                    {
                        if (regText != null)
                        {
                            try
                            {
                                using StreamReader sr = new StreamReader(f.FullName, Encoding.Default);

                                string Content = sr.ReadToEnd();

                                if (regText.IsMatch(Content))
                                    fileHandler?.Invoke(f.Name, f.DirectoryName, f.Length.ToString(), f.LastWriteTime.ToString());
                            }
                            catch (Exception) { continue; }
                        }
                        else { fileHandler?.Invoke(f.Name, f.DirectoryName, f.Length.ToString(), f.LastWriteTime.ToString()); }
                    }
                }
                if (chek)
                {
                    try
                    {
                        DirectoryInfo[] diSub = di.GetDirectories();

                        foreach (DirectoryInfo diSubDir in diSub)
                        {
                            token.ThrowIfCancellationRequested();
                            FindTextInFiles(regText, diSubDir, regMask, chek, thread_pause, token, fileHandler);
                        }

                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                }

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}
