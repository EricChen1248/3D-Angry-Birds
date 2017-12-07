using UnityEngine;

namespace Classes
{
    internal class SlingshotPouch : MonoBehaviour
    {
        public GameObject CurrentAmmo;
        public static readonly Vector3 StartingPosition = new Vector3(0, 1.8f, 0);
        public static GameObject Self;

        private void Start()
        {
            Self = gameObject;
        }

        public void Reset()
        {
            transform.localPosition = StartingPosition;

            var bird = transform.parent.GetComponent<Slingshot>().Refill();
            if (bird != null)
            {
                var newBird = Instantiate(bird);
                newBird.transform.parent = transform;
                newBird.transform.localPosition = new Vector3(0, 0, 0);
                CurrentAmmo = newBird;
            }
        }
        
    }
}
