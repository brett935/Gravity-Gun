using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC316_Gravity_Gun
{
    class Entity
    {
        /// <summary>
        /// The name of the entity
        /// </summary>
        string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// The position of the entity
        /// </summary>
        Vector3 positon;
        public Vector3 Position
        {
            get { return positon; }
            set { positon = value; }
        }

        /// <summary>
        /// The velocity of the entity
        /// </summary>
        Vector3 velocity;
        public Vector3 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        /// <summary>
        /// The weight of the entity
        /// </summary>
        float weight;
        public float Weight
        {
            get { return weight; }
            set { weight = value; }
        }
        /// <summary>
        /// The strength of gravity applied to the entity
        /// </summary>
        float gravity; 
        public float Gravity
        {
            get { return gravity; }
            set { gravity = value; }
        }
    }
}
