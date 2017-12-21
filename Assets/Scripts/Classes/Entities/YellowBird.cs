using UnityEngine;

namespace Classes.Entities
{
    public class YellowBird: Bird
    {
        protected bool AbilityUsed;

        /// <summary>
        /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
        /// </summary>
        protected override void FixedUpdate()
        {
            FixedUpdateFunctions();

            if (!AbilityUsed && Input.GetKeyDown(KeyCode.Space))
            {
                Rigidbody.AddForce(Velocity.normalized * 1000);
                AbilityUsed = true;
            }
            
        }
    }
}