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
        Vector3 positon { get; set; }

        /// <summary>
        /// The velocity of the entity
        /// </summary>
        Vector3 velocity { get; set; }

        /// <summary>
        /// The weight of the entity
        /// </summary>
        float weight { get; set; }
        
        /// <summary>
        /// The strength of gravity applied to the entity
        /// </summary>
        float gravity { get; set; }       
    }
}
