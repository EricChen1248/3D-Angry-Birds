﻿using Controllers;
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
            if (bird == null)
            {
                StartReloadLevel();
                return;
            }
            var newBird = Instantiate(bird).transform;
            CurrentAmmo = newBird.gameObject;
            newBird.parent = transform;
            newBird.localPosition = new Vector3(0, 0, 0);
        }

        private int reloadTime = -1;
        private void StartReloadLevel()
        {
            reloadTime = 800;
        }

        private void FixedUpdate()
        {
            if (reloadTime == -1)
            {
                return;
            }
            if (--reloadTime <= 0 && LevelController.Instance.pigsCount > 0)
            {
                LevelController.Instance.ReloadLevel();
            }

        }
        
    }
}
