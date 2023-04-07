using CV19.ViewModels.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CV19.ViewModels;

class DirectoryViewModel : ViewModel
{
    private readonly DirectoryInfo _directoryInfo;

    public IEnumerable<DirectoryViewModel> SubDirectories
    {
        get
        {
            try
            {
                var directories = _directoryInfo
                        .EnumerateDirectories()
                        .Select(dirInfo => new DirectoryViewModel(dirInfo.FullName));
                return directories;
            }
            catch (UnauthorizedAccessException e)
            {
                Debug.WriteLine(e.ToString());
            }
            return Enumerable.Empty<DirectoryViewModel>();
        }
    }
        

    public IEnumerable<FileViewModel> Files
    {
        get
        {
            try
            {
                var files = _directoryInfo
                        .EnumerateFiles()
                        .Select(file => new FileViewModel(file.FullName));
                return files;
            }
            catch (UnauthorizedAccessException e)
            {
                Debug.WriteLine(e.ToString());
            }
            return Enumerable.Empty<FileViewModel>();
        }
        
    }

    public IEnumerable<object> DirectoryItems =>
        SubDirectories.Cast<object>()
        .Concat(Files);

    public string Name => _directoryInfo.Name;
    public string Path => _directoryInfo.FullName;
    public DateTime CreationTime => _directoryInfo.CreationTime;

    public DirectoryViewModel(string path)
    {
        _directoryInfo = new DirectoryInfo(path);
    }
}


class FileViewModel : ViewModel
{
    private readonly FileInfo _fileInfo;

    public FileViewModel(string path) 
    {
        _fileInfo = new FileInfo(path);
    }

    public string Name => _fileInfo.Name;
    public string Path => _fileInfo.FullName;
    public DateTime CreationTime => _fileInfo.CreationTime;

}
