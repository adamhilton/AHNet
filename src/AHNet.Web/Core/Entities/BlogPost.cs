
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AHNet.Web.Core.Entities
{
    public class BlogPost : BaseEntity
    {
        public string Body { get; set; }
        public DateTime DatePublished { get; set; }
        public string Title { get; set; }
        public bool IsPublished { get; set; }

        [NotMapped]
        public string UrlTitle
        {
            get
            {
                var stringBuilder = new StringBuilder();

                var evaluatedString = Title.Replace(" ", "-");

                foreach (var value in evaluatedString)
                {
                    if ((value >= '0' && value <= '9') || (value >= 'A' && value <= 'Z') || (value >= 'a' && value <= 'z') || value == '-' || value == '_')
                    {
                        stringBuilder.Append(value);
                    }
                }
                return stringBuilder.ToString();
            }
        }
    }
}