using System.IO;

namespace knxprod_ns
{
    class DirectoryHelper
    {
        public static bool DirectoryDelete(string path)
        {
            if (Directory.Exists(path))
            {
                try
                {
                    Directory.Delete(path, true); //löschen alter Daten aus dem Ordner .\unzippedKnxprod
                }
                catch (IOException ioEx)
                {
                    return false;
                    //MessageBox.Show("Bitte Ordner schließen oder Verküpfung löschen:" + ioEx.Source + ioEx.Message);
                }
                return true;
            }
            return false;
        }

        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it.       
            Directory.CreateDirectory(destDirName);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destDirName, file.Name);
                file.CopyTo(tempPath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string tempPath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
                }
            }
        }
    }
}
