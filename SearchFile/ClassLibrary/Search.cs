using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ClassLibrary
{
    public class Search
    {
        static ulong FindTextInFiles(Regex regText, DirectoryInfo di, Regex regMask)
        {
            StreamReader sr = null;
            MatchCollection mc = null;

            ulong CountOfMatchFiles = 0;

            FileInfo[] fi = null;
            try
            {
                fi = di.GetFiles();
            }
            catch
            {
                return CountOfMatchFiles;
            }

            foreach (FileInfo f in fi)
            {
                if (regMask.IsMatch(f.Name))
                {
                    ++CountOfMatchFiles;
                    Console.WriteLine("File " + f.Name);
                    if (regText != null)
                    {
                        try
                        {
                            sr = new StreamReader(di.FullName + @"\" + f.Name,
                                Encoding.Default);

                            string Content = sr.ReadToEnd();
                            sr.Close();
                            mc = regText.Matches(Content);
                            foreach (Match m in mc)
                            {
                                Console.WriteLine("Текст знайдено в позиції {0}.", m.Index);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }

            DirectoryInfo[] diSub = di.GetDirectories();

            foreach (DirectoryInfo diSubDir in diSub)
                CountOfMatchFiles += FindTextInFiles(regText, diSubDir, regMask);

            return CountOfMatchFiles;
        }
    }
}
