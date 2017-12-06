using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Base controller for the whole game
    /// </summary>
    internal class CoreController : MonoBehaviour
    {
        internal static PlayerMode PlayerMode = PlayerMode.ShootMode;
        
        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update()
        {
            if (PlayerMode == PlayerMode.ShootMode)
            {
                
            }
        }

        // Awake is called when the script instance is being loaded
        private void Awake()
        {
            DontDestroyOnLoad(transform.gameObject);
        }


    }

}