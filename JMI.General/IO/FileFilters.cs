using System.Collections.Generic;
using System.Text;

namespace JMI.General.IO
{
    public static class FileFilters
    {
        public static IFileFilter All = new FileFilter("All files", "*");
        public static IFileFilter Bmp = new FileFilter("BMP files", "bmp");
        public static IFileFilter Dxf = new FileFilter("DXF files", "dxf");
        public static IFileFilter Dwg = new FileFilter("DWG files", "dwg");
        public static IFileFilter Gif = new FileFilter("GIF files", "gif");
        public static IFileFilter Jpeg = new FileFilter("JPEG files", "jpeg");
        public static IFileFilter Jpg = new FileFilter("JPG files", "jpg");
        public static IFileFilter Png = new FileFilter("PNG files", "png");
        public static IFileFilter SQLite = new FileFilter("SQLITE files", "SQLite");
        public static IFileFilter Tiff = new FileFilter("TIFF files", "tiff");
        public static IFileFilter Xls = new FileFilter("XLS files", "xls");
        public static IFileFilter Xlsb = new FileFilter("XLSB files", "xlsb");
        public static IFileFilter Xlsm = new FileFilter("XLSM files", "xlsm");
        public static IFileFilter Xlsx = new FileFilter("XLSX files", "xlsx");

        #region Application specific filters
        public static IEnumerable<IFileFilter> AutocadFileTypes
        {
            get
            {
                return new List<IFileFilter>()
                {
                    Dwg,
                    Dxf
                };
            }
        }

        public static IEnumerable<IFileFilter> ExcelFileTypes
        {
            get
            {
                return new List<IFileFilter>()
                {
                    Xls,
                    Xlsb,
                    Xlsm,
                    Xlsx
                };
            }
        }
        #endregion

        /// <summary>
        /// Checks if file type is in allowed types.
        /// </summary>
        /// <param name="allowedFileTypes">List of allowed file types.</param>
        /// <param name="filePath">File Path</param>
        /// <returns>True if file type extension extracted from file path equals some file type in allowed file types</returns>
        public static bool IsAllowedFileType(IEnumerable<IFileFilter> allowedFileTypes, string filePath)
        {
            //System.IO.Path.GetExtension returns extension with period (.ext)
            string extension = System.IO.Path.GetExtension(filePath).ToLower().TrimStart(new char[] { '.' }); ;
            foreach (IFileFilter ff in allowedFileTypes)
            {
                if (extension.Equals(ff.FileType.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Builds string used in open and save file dialog boxes.
        /// </summary>
        /// <param name="allowedFileTypes">List of file types allowed in file dialog box.</param>
        /// <returns>Example: "txt files (*.txt)|*.txt|All files (*.*)|*.*"</returns>
        public static string GetDialogFileTypeFilter(IEnumerable<IFileFilter> allowedFileTypes)
        {
            StringBuilder sb = new StringBuilder();

            foreach (IFileFilter item in allowedFileTypes)
            {
                sb.Append($"{item.Filter}|");
            }
            return sb.ToString().Substring(0, sb.Length - 1);
        }
    }
}
