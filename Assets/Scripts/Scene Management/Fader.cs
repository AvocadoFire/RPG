using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {

        CanvasGroup canvasGroup;

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public IEnumerator FadeOut(float waitTime)
        {
            while (canvasGroup.alpha < 1) 
            {
                canvasGroup.alpha += Time.deltaTime / waitTime;
                yield return null;
            }
        }

        public IEnumerator FadeIn(float waitTime)
        {
            while (canvasGroup.alpha > 0) //generally should not do comparisons between floats
            {
                canvasGroup.alpha -= Time.deltaTime / waitTime;
                yield return null;
            }
        }
    }
}
