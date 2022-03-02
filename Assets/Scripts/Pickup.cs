using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // TO USE
    // 1. make pickup in hierarchy
    //    add rigidbody (no gravity) and collider (trigger) and add pickup script
    // 2. make zeroed out prefab with just mesh or whatever interactions on prefab
    // 3. to make prefab look correct on body pickup in playmode and then pause and
    //      setup correct look. Then copy component transform before unpausing.
    //      then paste component values on prefab

   
    [SerializeField] GameObject whoToPickup;  //add player here
    [SerializeField] GameObject whatToPickup; //if want to put on body make prefab
    [SerializeField] Transform whereToPut;   //where to put on body skeleton or empty

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == whoToPickup)
        {
            //if want to put on body
             if (whatToPickup != null)
             {
                Instantiate(whatToPickup, whereToPut);
             }

             //can add something to do on pickup

             Destroy(this.gameObject); //if you want to destroy
        }
    }
}
