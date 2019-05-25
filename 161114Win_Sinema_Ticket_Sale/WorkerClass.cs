using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _161114Win_Sinema_Ticket_Sale
{
  public  class WorkerClass
    {
        private string Name;
        private string Surname;
        private string Birthdate;
        private string Email;
        private string Phone;
        private string PersonalityNo;
        private string Post;
        private string UserName;
        private string Password;

        public WorkerClass(string Name,string Surname)
        {
            this.Name = Name;
            this.Surname = Surname;
        }
        public string Name1
        {
            get
            {
                return Name;
            }

            set
            {
                Name = value;
            }
        }

        public string Surname1
        {
            get
            {
                return Surname;
            }

            set
            {
                Surname = value;
            }
        }

        public string Birthdate1
        {
            get
            {
                return Birthdate;
            }

            set
            {
                Birthdate = value;
            }
        }

        public string Email1
        {
            get
            {
                return Email;
            }

            set
            {
                Email = value;
            }
        }

        public string Phone1
        {
            get
            {
                return Phone;
            }

            set
            {
                Phone = value;
            }
        }

        public string PersonalityNo1
        {
            get
            {
                return PersonalityNo;
            }

            set
            {
                PersonalityNo = value;
            }
        }

        public string Post1
        {
            get
            {
                return Post;
            }

            set
            {
                Post = value;
            }
        }

        public string UserName1
        {
            get
            {
                return UserName;
            }

            set
            {
                UserName = value;
            }
        }

        public string Password1
        {
            get
            {
                return Password;
            }

            set
            {
                Password = value;
            }
        }
    }
}
