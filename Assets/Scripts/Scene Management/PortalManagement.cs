using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class PortalManagement : MonoBehaviour
    {
        [SerializeField] int sceneToLoad = -1;


        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            else 
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            Object.DontDestroyOnLoad(gameObject);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            print("Scene Loaded");
            Destroy(gameObject);
        }
    } 
}
