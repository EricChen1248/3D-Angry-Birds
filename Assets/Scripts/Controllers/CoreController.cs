using UnityEngine;

namespace Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Base controller for the whole game
    /// </summary>
    internal class CoreController : MonoBehaviour
    {
        
        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update()
        {
        }

        // Awake is called when the script instance is being loaded
        private void Awake()
        {
            DontDestroyOnLoad(transform.gameObject);
        }


    }

}