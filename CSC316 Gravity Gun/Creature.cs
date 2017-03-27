using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC316_Gravity_Gun
{
    class Creature : Entity
    {
        /// <summary>
        /// Health of the creature
        /// </summary>
        int health;
        /// <summary>
        /// Speed that the creature moves at
        /// </summary>
        int speed;

        /// <summary>
        /// Default constructor for a creature
        /// </summary>
        public Creature() : base()
        {
            health = 1000;
            speed = 1;
        }
        /// <summary>
        /// Constructor to set health and speed of a Creature
        /// </summary>
        /// <param name="h">Health of the Creature</param>
        /// <param name="s">Speed the Creature moves at</param>
        public Creature(int h, int s) : base()
        {
            health = h;
            speed = s;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="n">Name to give the Creature</param>
        /// <param name="p">Position to give the Creature</param>
        /// <param name="v">Velocity to give the Creature</param>
        /// <param name="h">Health of the Creature</param>
        /// <param name="s">Speed the Creature moves at</param>
        public Creature(string n, Vector3 p, Vector3 v, int h, int s) : base(n, p, v)
        {
            health = h;
            speed = s;
        }
    }
}
