using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scripts.SoundManager;

namespace GeneralSctipts
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private SceneTransitionAnimation transitionAnimation;

        private static SceneController _instance;

        SceneController sceneManager;

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;

                DontDestroyOnLoad(gameObject);
            }
        }

        public static void LoadScene(int id)
        {
         
            _instance.StartCoroutine(LoadSceneCoroutine(id));
        }

        public static void Quit()
        {

            _instance.StartCoroutine(QuitCoroutine());
        }

        private static IEnumerator LoadSceneCoroutine(int id)
        {
            AsyncOperation handler =  SceneManager.LoadSceneAsync(id);
            handler.allowSceneActivation = false;

            yield return _instance.transitionAnimation.PlayAnimation(true);

            while (handler.progress < 0.9f)
            {
                Debug.Log(handler.progress);
                yield return null;
            }

            handler.allowSceneActivation = true;
            SoundManager.Stop(Scripts.SoundManager.AudioType.Music);

            yield return _instance.transitionAnimation.PlayAnimation(false);
        }

        private static IEnumerator QuitCoroutine()
        {
            yield return _instance.transitionAnimation.PlayAnimation(true);

            UnityEngine.Application.Quit();
        }
    }
}

