using JMI.General.IO;
using System.Collections.Generic;

namespace JMI.General.VM.IO.Picker
{
    public static class DefaultPickers
    {
        private static readonly IEnumerable<IFileFilter> allFilesFilter
            = new List<IFileFilter>() { FileFilters.All };

        public static DirectoryPickerViewModel DirectoryPicker { get; } =
            new DirectoryPickerViewModel();

        public static OpenSingleFilePickerViewModel OpenSingleFilePicker { get; } =
            new OpenSingleFilePickerViewModel() { FileFilters = FileFilters.GetDialogFileTypeFilter(allFilesFilter) };

        public static SaveFilePickerViewModel SaveFilePicker { get; } =
            new SaveFilePickerViewModel() { FileFilters = FileFilters.GetDialogFileTypeFilter(allFilesFilter) };
    }
}
