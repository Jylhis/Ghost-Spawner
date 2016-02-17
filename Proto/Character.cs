using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proto
{
    class Character
    {
        private int level;
        private int health;
        private int defense;
        private int attack;
        private bool IsKill;
        private Weapon weapon;
        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                if (value <= 0)
                {
                    IsKill = true;
                    health = 0;
                }

            }


        }
    }
}
