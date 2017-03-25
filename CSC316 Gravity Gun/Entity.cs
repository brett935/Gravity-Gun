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
        string name { get; set; }

        /// <summary>
        /// The position of the entity
        /// </summary>
        Vector3 position { get; set; }

        /// <summary>
        /// The velocity of the entity
        /// </summary>
        Vector3 velocity { get; set; }

        /// <summary>
        /// The weight of the entity
        /// </summary>
        float weight { get; set; }
        
        /// <summary>
        /// The strength/direction of gravity applied to the entity
        /// </summary>
        Vector3 gravity { get; set; }
        
        /// <summary>
        /// Constructor for Entity
        /// </summary>
        /// <param name="n">Name to give the Entity</param>
        /// <param name="p">Position to give the Entity</param>
        /// <param name="v">Velocity to give the Entity</param>
        public Entity(string n, Vector3 p, Vector3 v)
        {
            name = n;
            position = p;
            velocity = v;
            gravity = new Vector3(0, -9.81f, 0); //default strength/direction of gravity for this Entity
        }
        
        /// <summary>
        /// Updates the Entity when called
        /// </summary>
        /// <param name="gameTime">The game time provided by monogame</param>
        public void Update(GameTime gameTime)
        {
            velocity +=  ( gravity * (float)gameTime.ElapsedGameTime.TotalSeconds ); //update velocity relative to gravity
            position += ( velocity * (float)gameTime.ElapsedGameTime.TotalSeconds ); //update position relative to velocity
        }       
    }
}
