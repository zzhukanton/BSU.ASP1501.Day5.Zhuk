using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Book : IEquatable<Book>, IComparable<Book>
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public uint PublishingYear { get; set; }
        public int NumberOfPages { get; set; }
        public string Publisher { get; set; }

        public bool Equals(Book other)
        {
            if (ReferenceEquals(other, null))
                return false;
            if (ReferenceEquals(other, this))
                return true;

            return (Author == other.Author && Title == other.Title && PublishingYear == other.PublishingYear && 
                NumberOfPages == other.NumberOfPages && Publisher == other.Publisher);
        }

        public int CompareTo(Book other)
        {
            if (other == null)
                return 1;
            else
                return PublishingYear.CompareTo(other.PublishingYear);
        }
    }
}