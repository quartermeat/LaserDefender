using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class LevelManager : MonoBehaviour
    {
        private static int _breakableBrickCount;

        public static int BreakableBrickCount
        {
            get { return _breakableBrickCount; }
            set { _breakableBrickCount = value; }
        }

        public static void CheckWinCondition()
        {
            if (_breakableBrickCount <= 0)
            {
                LoadNextLevel();
            }
        }

        public static void LoadLoseLevel()
        {
            SceneManager.LoadScene("Lose");
        }

        public static void LoadNextLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        }

        //load a new level
        public void LoadLevelRequest(string name)
        {
            Debug.Log("Load level request: " + name);
            SceneManager.LoadScene(name);
        }

        public static void QuitRequest()
        {
            Debug.Log("Quit game request");
            Application.Quit();
        }

    }
}
