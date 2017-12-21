using UnityEngine;
namespace Classes.Entities
{
    public class BlueBird: Bird
    {
        protected bool AbilityUsed;

        /// <summary>
        /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
        /// </summary>
        protected override void FixedUpdate()
        {
            FixedUpdateFunctions();

            if (AbilityUsed == false && Input.GetKeyDown(KeyCode.Space) && transform.parent == null)
            {
                Debug.Log("BlueBird ability use");
                for (int i = 0; i < 3; i++)
                {
                    var babyBird = Instantiate(gameObject);
                    babyBird.transform.position = transform.position;
                    babyBird.transform.position += new Vector3(0, (i - 1) * 1, 0);
                    var velocity = GetComponent<Rigidbody>().velocity;
                    babyBird.GetComponent<Rigidbody>().velocity = velocity;
                    babyBird.GetComponent<Rigidbody>().mass = babyBird.GetComponent<Rigidbody>().mass / 2;
                    babyBird.GetComponent<Rigidbody>().AddForce(new Vector3(0, (i - 1) * 50, 0));
                    
                    velocity.Normalize();
                    velocity /= 1.5f;
                    babyBird.transform.position -= velocity * i;
                    babyBird.transform.localScale /= 1.5f;

                    babyBird.GetComponent<BlueBird>().AbilityUsed = true;
                    babyBird.GetComponent<BlueBird>().IsShot = false;
                }

                Destroy(gameObject);
            }
            
        }
    }    
}