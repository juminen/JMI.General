using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JMI.General.IO
{
    public class LocalDriveCrawler
    {
        #region constructors
        #endregion

        #region properties

        #endregion

        #region methods
        public static async Task<IEnumerable<DriveInfo>> GetAllDrivesAsync()
        {
            List<DriveInfo> drives = new List<DriveInfo>();
            try
            {
                drives = await Task.Run(() => DriveInfo.GetDrives().ToList());
            }
            catch (IOException ex)
            {
#if DEBUG
                Console.WriteLine($"Failed getting drives, exception message: {ex.Message}");
#endif
            }
            catch (UnauthorizedAccessException ex)
            {
#if DEBUG
                Console.WriteLine($"No access to drives, exception message: {ex.Message}");
#endif
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine($"Unknown exception getting drives, exception message: {ex.Message}\n{ex.StackTrace}");
#endif
            }

            return drives;
        }

        public static async Task<IEnumerable<DriveInfo>> GetReadyDrivesAsync()
        {
            var all = await GetAllDrivesAsync();
            List<DriveInfo> drives = new List<DriveInfo>();
            drives = await Task.Run(() => DriveInfo.GetDrives().ToList());
            foreach (DriveInfo drive in all)
            {
                if (drive.IsReady)
                {
                    drives.Add(drive);
                }
            }
            return drives;
        }
        #endregion

        #region events
        #endregion

        #region event handlers
        #endregion

    }
}
