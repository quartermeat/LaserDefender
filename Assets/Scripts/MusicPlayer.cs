using UnityEngine;

namespace Assets.Scripts
{
    public class MusicPlayer : MonoBehaviour
    {
        private static MusicPlayer instance = null;

        void Awake()
        {
            Debug.Log("Instance created: " + GetInstanceID());
            if (instance != null)
            {
                Destroy(gameObject);
                Debug.Log("destroyed duplicate Music Player");
            }
            else
            {
                instance = this;
                GameObject.DontDestroyOnLoad(gameObject);
            }
        }
    }
}
