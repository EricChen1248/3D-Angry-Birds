using UnityEngine;

namespace Classes
{
    internal class SlingshotPouch : MonoBehaviour
    {
        public GameObject CurrentAmmo;
        public static readonly Vector3 StartingPosition = new Vector3(0, 1.7f, 0);
        public static GameObject Self;

        private void Start()
        {
            Self = gameObject;
        }

        public void Reset()
        {
            transform.localPosition = StartingPosition;
        }



    }
}
