using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{

    public class CinematicTrigger : MonoBehaviour
    {
        bool wasTriggered = false;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            if (!wasTriggered)
            {
                GetComponent<PlayableDirector>().Play();
                wasTriggered = true;
            }
        }
    }
}
