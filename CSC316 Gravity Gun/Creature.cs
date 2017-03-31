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
        /// The amount of damage that a creature can do to another creature
        /// </summary>
        int damageDealable;

        /// <summary>
        /// Default constructor for a creature
        /// </summary>
        public Creature() : base()
        {
            health = 1000;
            speed = 1;
            damageDealable = 10;
        }
        /// <summary>
        /// Constructor to set health and speed of a Creature
        /// </summary>
        /// <param name="h">Health of the Creature</param>
        /// <param name="s">Speed the Creature moves at</param>
        public Creature(int h, int s, int d) : base()
        {
            health = h;
            speed = s;
            damageDealable = d;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="n">Name to give the Creature</param>
        /// <param name="p">Position to give the Creature</param>
        /// <param name="v">Velocity to give the Creature</param>
        /// <param name="h">Health of the Creature</param>
        /// <param name="s">Speed the Creature moves at</param>
        public Creature(string n, Vector3 p, Vector3 v, int h, int s, int d) : base(n, p, v)
        {
            health = h;
            speed = s;
            damageDealable = d;
        }
        /// <summary>
        /// Deals damage to another creature
        /// </summary>
        /// <param name="creature"></param>
        public void dealDamage(Creature creature)
        {
            creature.takeDamage(damageDealable);
        }
        /// <summary>
        /// Takes damage from another creature
        /// </summary>
        /// <param name="amount">Amount of damage to take</param>
        public void takeDamage(int amount)
        {
            health -= amount; //reduce health by damage amount
        }

    }
}
