using System;
using System.Collections.Generic;
using System.IO;
using System.Security;

namespace JMI.General.IO
{
    public static class DirectoryCrawler
    {
        /// <summary>
        /// Creates list of <see cref="DriveInfo"/> from local drives that are ready
        /// </summary>
        /// <returns></returns>
        public static ActionResult<IEnumerable<DriveInfo>> GetDrivesReady()
        {
            List<DriveInfo> drives = new List<DriveInfo>();
            ActionResult<IEnumerable<DriveInfo>> actionResult = new ActionResult<IEnumerable<DriveInfo>>(drives);
            try
            {
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.IsReady)
                    {
                        drives.Add(drive);
                    }
                }
            }
            catch (IOException ex)
            {
                actionResult.AddFailReason($"Failed getting drives, exception message: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                actionResult.AddFailReason($"No access to drives, exception message: {ex.Message}");
            }
            catch (Exception ex)
            {
                actionResult.AddFailReason($"Unknown exception getting drives, exception message: {ex.Message}");
                actionResult.AddReason(ex.StackTrace);
            }
            return actionResult;
        }

        /// <summary>
        /// Creates list of <see cref="DirectoryInfo"/> from directory's sub folders.<br/>
        /// Does not include system folders.
        /// </summary>
        /// <param name="path">Path to directory</param>
        /// <param name="listHidden">If true, lists also hidden folders</param>
        /// <returns></returns>
        public static ActionResult<IEnumerable<DirectoryInfo>> GetSubFolders(string path, bool listHidden)
        {
            List<DirectoryInfo> folders = new List<DirectoryInfo>();
            ActionResult<IEnumerable<DirectoryInfo>> actionResult = new ActionResult<IEnumerable<DirectoryInfo>>(folders);

            try
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                foreach (DirectoryInfo subDir in directory.EnumerateDirectories())
                {
                    bool addToList = true;
                    //Check if directory is system or hidden directory
                    if (subDir.Attributes.HasFlag(FileAttributes.System))
                    {
                        addToList = false;
                    }
                    else
                    {
                        if (subDir.Attributes.HasFlag(FileAttributes.Hidden) && !listHidden)
                        {
                            addToList = false;
                        }
                    }
                    if (addToList) folders.Add(subDir);
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                actionResult.AddFailReason($"Directory '{path}' not found, exception message: {ex.Message}");
            }
            catch (IOException ex)
            {
                actionResult.AddFailReason($"Path '{path}' for folders is wrong, exception message: {ex.Message}");
            }
            catch (SecurityException ex)
            {
                actionResult.AddFailReason($"No access to directory '{path}', exception message: {ex.Message}");
            }
            catch (Exception ex)
            {
                actionResult.AddFailReason($"Unknown exception getting directories, path was: '{path}', exception message: {ex.Message}");
                actionResult.AddReason(ex.StackTrace);
            }
            return actionResult;
        }

        /// <summary>
        /// Creates list of <see cref="FileInfo"/> from directory.<br/>
        /// Does not include system files.
        /// </summary>
        /// <param name="path"></param>
        /// /// <param name="listHidden">If true, lists also hidden files</param>
        /// <returns></returns>
        public static ActionResult<IEnumerable<FileInfo>> GetFiles(string path, bool listHidden)
        {
            List<FileInfo> files = new List<FileInfo>();
            ActionResult<IEnumerable<FileInfo>> actionResult = new ActionResult<IEnumerable<FileInfo>>(files);

            try
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                foreach (FileInfo file in directory.EnumerateFiles())
                {
                    bool addToList = true;
                    //Check if file is system or hidden file
                    if (file.Attributes.HasFlag(FileAttributes.System))
                    {
                        addToList = false;
                    }
                    else
                    {
                        if (file.Attributes.HasFlag(FileAttributes.Hidden) && !listHidden)
                        {
                            addToList = false;
                        }
                    }
                    if (addToList) files.Add(file);
                }
            }
            catch (ArgumentException ex)
            {
                actionResult.AddFailReason($"Path '{path}' for files is wrong, exception message: {ex.Message}");
            }
            catch (DirectoryNotFoundException ex)
            {
                actionResult.AddFailReason($"Directory '{path}' for files not found, exception message: {ex.Message}");
            }
            catch (PathTooLongException ex)
            {
                actionResult.AddFailReason($"Path '{path}' for files is too long, exception message: {ex.Message}");
            }
            catch (IOException ex)
            {
                actionResult.AddFailReason($"Path '{path}' for files is wrong, exception message: {ex.Message}");
            }
            catch (SecurityException ex)
            {
                actionResult.AddFailReason($"No access to directory '{path}', exception message: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                actionResult.AddFailReason($"No access to directory '{path}', exception message: {ex.Message}");
            }
            catch (Exception ex)
            {
                actionResult.AddFailReason($"Unknown exception getting files, path was: '{path}', exception message: {ex.Message}");
                actionResult.AddReason(ex.StackTrace);
            }
            return actionResult;
        }
    }
}
