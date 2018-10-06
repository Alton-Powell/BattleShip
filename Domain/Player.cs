using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Player
    {
        private readonly string _name;

        public Player(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }

    }
}
