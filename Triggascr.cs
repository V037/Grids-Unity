using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//credits: V.O37_

public class Triggascr : MonoBehaviour
{
    public bool canBuildTr = true; //another condition for make the other script work

    private void OnTriggerEnter(Collider collider) //on object touch or intersection you will not build
    {

        if (collider.tag == "NoBuild") //create a tag from your unity project like this one
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
