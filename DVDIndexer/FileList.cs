using System;
using System.Collections.Generic;

namespace DVDIndexer
{
    [Serializable]
    public class FileList
    {
        public List<FileInfoSerializable> files { get; set; }

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
