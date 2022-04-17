using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggascr : MonoBehaviour
{
    public bool canBuildTr = true;

    public Collider collider;

    private void Update()
    {
        collider = this.gameObject.GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider collider)
    {

        //using tags for detect with what this object is colliding, this object should be a trigger
        //If your player have the tag "player", this object will detect when your player is touching it
        //remember to set the collider to trigger for make that work
        if (collider.tag == "NoBuild")
        {
            canBuildTr = false;
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "NoBuild")
        {
            canBuildTr = true;
        }
    }
}
