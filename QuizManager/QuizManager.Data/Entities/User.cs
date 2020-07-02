using System;
using System.Collections.Generic;
using System.Text;

namespace QuizManager.Data
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool IsEditor { get; set; }

        public bool IsViewer { get; set; }

        public bool IsRestricted { get; set; }
    }
}
