using UnityEngine;

namespace Classes
{
    internal class SlingshotPouch : MonoBehaviour
    {
        public static SlingshotPouch Instance;
        public GameObject CurrentAmmo;
        public static readonly Vector3 StartingPosition = new Vector3(0, 2f, 0);

        private void Start()
        {
            Instance = this;
        }

        internal void Reset()
        {
            transform.localPosition = StartingPosition;

            var bird = transform.parent.GetComponent<Slingshot>().Refill();
            if (bird == null) return;
            var newBird = Instantiate(bird).transform;
            newBird.parent = transform;
            newBird.localPosition = new Vector3(0, 0, 0);
            CurrentAmmo = newBird.gameObject;
        }
        
    }
}
