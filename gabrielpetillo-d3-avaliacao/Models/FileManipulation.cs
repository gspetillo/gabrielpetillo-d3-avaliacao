using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace gabrielpetillo_d3_avaliacao.Models
{
    /// <summary>
    /// FileManipulations manipulation methods
    /// </summary>
    public class FileManipulation
    {
        /// <summary>
        /// Create folder and file to manipulate data
        /// </summary>
        /// <param name="path">Folder and file path</param>
        public static void CreateFolderAndFile(string path)
        {
            string folder = path.Split("/")[0];

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            if (!File.Exists(path))
            {
                File.Create(path).Close();
                string[] header = { "Date;Action;ID User;Name;Email" };
                File.AppendAllLines(path, header);
            }

        }

        /// <summary>
        /// Read all CSV lines
        /// </summary>
        /// <param name="path">Path to folder and file</param>
        /// <returns>Returns all file lines</returns>
        public List<string> ReadAllLinesCSV(string path)
        {
            List<string> lines = new List<string>();

            using (StreamReader FileManipulation = new StreamReader(path))
            {
                string line;

                while ((line = FileManipulation.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            return lines;
        }

        /// <summary>
        /// Reescreve todas as linhas do CSV
        /// </summary>
        /// <param name="path">Path to folder and file</param>
        /// <param name="lines">List of lines to be overwritten</param>
        public void RewriteCSV(string path, List<string> lines)
        {
            using (StreamWriter output = new StreamWriter(path))
            {
                foreach (var line in lines)
                {
                    output.Write(line + "\n");
                }
            }
        }
    }
}