using System;
using System.Collections.Generic;
using System.IO;

namespace DVDIndexer
{
    [Serializable]
    public class FileList
    {
        public List<FileInfoSerializable> files { get; set; }

        public DriveType Source { get; set; }

        public string SourceName { get; set; }

        public string Id { get; set; }

        public FileList()
        {
            files = new List<FileInfoSerializable>();
        }

        public void Add(FileInfoSerializable m)
        {
            files.Add(m);
        }
    }
}
