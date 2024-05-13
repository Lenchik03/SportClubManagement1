using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportClubManagement.mvvm.model
{
    public class ActiveManager
    {
        public Manager Manager { get; set; }

        public ActiveManager()
        {
        }

        static ActiveManager instance;
        public static ActiveManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ActiveManager();
                return instance;
            }
        }
    }
}
