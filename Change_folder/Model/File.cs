using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompareFolder.Model
{
    public class File
    {
        public enum Statuses
        {
            ExistsOnlyInFirstFolder,
            ExistsOnlyInSecondFolder,
            ExistsInBothFoldersWithTheSameSizes,
            ExistsInBothFoldersWithDifferentSizes,
        }

        private Statuses status;

        private string name;
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        private long size;
        public long Size
        {
            get
            {
                return size;
            }

            set
            {
                size = value;
            }
        }

        private DateTime modifyDate;
        public DateTime ModifyDate
        {
            get
            {
                return modifyDate;
            }

            set
            {
                modifyDate = value;
            }
        }

        public Statuses Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }
    }
}
