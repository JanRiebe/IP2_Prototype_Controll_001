/*
 * The purpose of this script is to handle the death of a body. 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MementoMori : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Killer")
        {
            OnDeath();
        }
    }


    private void OnDeath()
    {
        // Inform the game manager that this body has died.
        GameManager.instance.BodyDied(this);

        //TODO Destroy all the limbs associated with this body.
        //List<Limb> bodyParts = new List<Limb>();
        
    }
}
