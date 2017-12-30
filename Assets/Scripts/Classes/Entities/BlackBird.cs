using Classes.Objects;
using UnityEngine;

namespace Classes.Entities
{
    public class BlackBird: Bird
    {
        protected bool AbilityUsed;

        public GameObject ExplosionFragment;
        
        private readonly System.Random random = new System.Random();


        /// <summary>
        /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
        /// </summary>
        protected override void FixedUpdate()
        {
            FixedUpdateFunctions();

            if (AbilityUsed || !Input.GetKeyDown(KeyCode.Space) || transform.parent != null) return;
            Destroy(gameObject);
            Helper.GenerateExplosion(Position);
            

        }
    }
}