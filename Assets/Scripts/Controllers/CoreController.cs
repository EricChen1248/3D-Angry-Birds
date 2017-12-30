using UnityEngine;

namespace Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Base controller for the whole game
    /// </summary>
    public class CoreController : MonoBehaviour
    {
        public static CoreController Instance;
        // Awake is called when the script instance is being loaded
        private void Awake()
        {
            DontDestroyOnLoad(transform.gameObject);
            Instance = this;
        }


        

    }

}