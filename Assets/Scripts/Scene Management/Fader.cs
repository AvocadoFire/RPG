using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] float fadeOutTime = 3f;
        [SerializeField] float fadeInTime = 1f;
        CanvasGroup canvasGroup;

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            StartCoroutine(FadeOutIn());
        }

        IEnumerator FadeOutIn()
        {
            yield return FadeOut(fadeOutTime);
            yield return FadeIn(fadeInTime);
        }

        public IEnumerator FadeOut(float waitTime)
        {
            print("fade out");
            print("alpha " + canvasGroup.alpha);

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
