using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSync
{
    class ArchiveTree
    {
        public ArchiveTree(string _fullPath, string _archFullPath)
        {
            FullPath = _fullPath;
            ArchiveFullPath = _archFullPath;
            ArchiveTreeList = new List<ArchiveTree>();
        }

        public string FullPath { get; set; }
        public string ArchiveFullPath { get; set; }
        public List<ArchiveTree> ArchiveTreeList { set; get; }
    }
}
