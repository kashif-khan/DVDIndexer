using System;
using System.IO;

namespace DVDIndexer
{
    [Serializable]
    public class FileInfoSerializable
    {

        private readonly FileInfo _fileInfo;

        #region ~~~ Constructors ~~~

        public FileInfoSerializable() { }

        public FileInfoSerializable(FileInfo FileInfo) { _fileInfo = FileInfo; }

        #endregion


        #region ~~~ Properties ~~~

        private string name;

        public string Name
        {
            get { return _fileInfo != null ? _fileInfo.Name : name; }
            set { name = value; }
        }

        private string fullName;

        public string FullName
        {
            get { return _fileInfo != null ? _fileInfo.FullName : fullName; }
            set { fullName = value; }
        }

        private long length;

        public long Length
        {
            get { return _fileInfo != null ? _fileInfo.Length : length; }
            set { length = value; }
        }

        private string extension;

        public string Extension
        {
            get { return _fileInfo != null ? _fileInfo.Extension : extension; }
            set { extension = value; }
        }

        private DateTime lastWriteTime;

        public DateTime LastWriteTime
        {
            get { return _fileInfo != null ? _fileInfo.LastWriteTime : lastWriteTime; }
            set { lastWriteTime = value; }
        }

        private string directoryName;

        public string DirectoryName
        {
            get { return _fileInfo != null ? _fileInfo.DirectoryName : directoryName; }
            set { directoryName = value; }
        }

        #endregion
    }
}
