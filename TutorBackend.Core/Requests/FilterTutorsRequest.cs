using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorBackend.Core.Requests
{
    public class FilterTutorsRequest
    {
        public string SearchString { get; set; }

        public IList<string> SelectedTopics { get; set; }

        public bool? RemoteLessons { get; set; }

        public bool? LocalLessons { get; set; }

        public string Localization { get; set; }

        public int Page { get; set; } = 1;

        public int Limit { get; set; } = 15;

        public DateTime SearchFrom { get; set; }

        public DateTime SearchTo { get; set; }
    }
}
