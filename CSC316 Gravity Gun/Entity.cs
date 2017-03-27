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
        public Vector3 position { get; set; }

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
        /// The strength/direction of the temporary gravity
        /// </summary>
        Vector3 effectGravity { get; set; }

        /// <summary>
        /// Amount of time in milliseconds that an effect last on an Entity
        /// </summary>
        float effectDuration { get; set; }

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
            effectDuration = 0;
            effectGravity = new Vector3(0, 0, 0);
        }
        
        /// <summary>
        /// Updates the Entity when called
        /// </summary>
        /// <param name="gameTime">The game time provided by monogame</param>
        public void Update(GameTime gameTime)
        {
            //if temporary gravity effect is being applied
            if(effectDuration > 0)
            {
                velocity += (effectGravity * (float)gameTime.ElapsedGameTime.TotalSeconds); //update velocity relative to temporary gravity effect
                position += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds); //update position relative to velocity
                effectDuration -= (float)gameTime.ElapsedGameTime.TotalMilliseconds; //decrement effect time by elapsed time since last update
            }
            //if using default gravity
            else
            {
                velocity += (gravity * (float)gameTime.ElapsedGameTime.TotalSeconds); //update velocity relative to gravity
                position += (velocity * (float)gameTime.ElapsedGameTime.TotalSeconds); //update position relative to velocity
            }
        }
        
        /// <summary>
        /// Temporarily changes an entity's gravity vector.
        /// </summary>
        /// <param name="v">The temporary gravity vector</param>
        /// <param name="d">The time to use the gravity vector for (milliseconds)</param>
        public void applyGravityEffect(Vector3 v, float d)
        {
            effectGravity = v;
            effectDuration = d;
        }       
    }
}
