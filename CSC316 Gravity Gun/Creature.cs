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
        /// Health of the Creature
        /// </summary>
        int health;
        /// <summary>
        /// Speed that the Creature moves at
        /// </summary>
        int speed;

        /// <summary>
        /// The amount of damage that a Creature can do to another Creature
        /// </summary>
        int damageDealable;

        /// <summary>
        /// The distance that a Creature can attack another Creature from (X,Y,Z)
        /// </summary>
        int attackRange;

        /// <summary>
        /// Default constructor for a Creature
        /// </summary>
        public Creature() : base()
        {
            health = 1000;
            speed = 1;
            damageDealable = 10;
            attackRange = 10;
        }
        /// <summary>
        /// Constructor to set health, speed, damageDealable and attackRange of a Creature
        /// </summary>
        /// <param name="h">Health of the Creature</param>
        /// <param name="s">Speed the Creature moves at</param>
        /// <param name="d">Damage the Creature moves at</param>
        /// <param name="r">Range that a Creature can attack from</param>
        public Creature(int h, int s, int d, int r) : base()
        {
            health = h;
            speed = s;
            damageDealable = d;
            attackRange = r;
        }
        /// <summary>
        /// Constructor to set name, position, velocity, health, speed, damageDealable and attackRange of a Creature
        /// </summary>
        /// <param name="n">Name to give the Creature</param>
        /// <param name="p">Position to give the Creature</param>
        /// <param name="v">Velocity to give the Creature</param>
        /// <param name="h">Health of the Creature</param>
        /// <param name="s">Speed the Creature moves at</param>
        /// <param name="d">The amount of damage that a Creature can deal another Creature</param>
        /// <param name="r">Range that a Creature can attack from</param>
        public Creature(string n, Vector3 p, Vector3 v, int h, int s, int d, int r) : base(n, p, v)
        {
            health = h;
            speed = s;
            damageDealable = d;
            attackRange = r;
        }
        /// <summary>
        /// Deals damage to another Creature
        /// </summary>
        /// <param name="creature"></param>
        void dealDamage(Creature creature)
        {
            creature.takeDamage(damageDealable);
        }
        /// <summary>
        /// Takes damage from another Creature
        /// </summary>
        /// <param name="amount">Amount of damage to take</param>
        public void takeDamage(int amount)
        {
            health -= amount; //reduce health by damage amount
        }

        /// <summary>
        /// Decides if an enemy is within attacking range by using a bounding box around this creature
        /// </summary>
        /// <param name="creature">The Creature that this Creature decide to attack or not</param>
        public void attackCheck(Creature creature)
        {
            //distance components between this Creature and Creature c
            int xDistance = (int)(creature.position.X - position.X);
            int yDistance = (int)(creature.position.Y - position.Y);
            int zDistance = (int)(creature.position.Z - position.Z);

            //get distance components as a positive numbers
            int absoluteXDistance = Math.Abs(xDistance);
            int absoluteYDistance = Math.Abs(yDistance);
            int absoluteZDistance = Math.Abs(zDistance);

            //if distance to Creature is larger than the attackRange then the result will be positive
            int xRange = absoluteXDistance - attackRange;
            int yRange = absoluteYDistance - attackRange;
            int zRange = absoluteZDistance - attackRange;

            //decide if the creature is within attacking range
            if ( (xRange <= 0) && (yRange <= 0) && (zRange <= 0))
            {
                dealDamage(creature); //deal damage to the Creature that is within attacking range   
            }
            
        }
    }
}
