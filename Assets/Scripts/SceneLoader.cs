using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private GameObject preloaderImage;
        private static string nextLevel;
        public Texture2D fadeTexture;
 
        [Range(0.1f,1f)]
        public float fadespeed;
        public int drawDepth = -1000;
 
        private float alpha = 1f;
        private float fadeDir = -1f;

        private void Update()
        {
            if (preloaderImage)
            {
                var rotation = preloaderImage.transform.rotation;
                rotation = new Quaternion(0, 0, rotation.z - 0.01f, rotation.w);
                preloaderImage.transform.rotation = rotation;
            }
        }

        public static void LoadLevel(string level)
        {
            nextLevel = level;
            SceneManager.LoadScene("LoadingScene");
        }

        private IEnumerator Start()
        {
            GameManager.SetGameState(GameState.Loading);
            
            yield return new WaitForSeconds(1f);

            if (string.IsNullOrEmpty(nextLevel))
            {
                SceneManager.LoadScene("MainMenu");
                yield break;
            }

            AsyncOperation loading = null;

            loading = SceneManager.LoadSceneAsync(nextLevel, LoadSceneMode.Additive);

            while (!loading.isDone)
            {
                yield return null;
            }

            nextLevel = null;
            SceneManager.UnloadSceneAsync("LoadingScene");

        }

        private void OnGUI()
        {
            
            alpha += fadeDir * fadespeed * Time.deltaTime;
            alpha = Mathf.Clamp01(alpha);
 
            Color newColor = GUI.color; 
            newColor.a = alpha;
 
            GUI.color = newColor;
 
            GUI.depth = drawDepth;
 
            GUI.DrawTexture( new Rect(0,0, Screen.width, Screen.height), fadeTexture);

        }
    }
}