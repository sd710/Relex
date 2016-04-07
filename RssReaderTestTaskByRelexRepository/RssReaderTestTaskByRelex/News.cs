using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RssReaderTestTaskByRelex
{
    public class News
    {
        private int _id;
        private string _title;
        private string _discription;
        private string _link;
        private DateTime _publicationDate;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string Discription
        {
            get { return _discription; }
            set { _discription = value; }
        }

        public string Link
        {
            get { return _link; }
            set { _link = value; }
        }

        public DateTime PublicationDate
        {
            get { return _publicationDate; }
            set { _publicationDate = value; }
        }
    }
}
