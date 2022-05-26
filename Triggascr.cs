using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggascr : MonoBehaviour
{
    public bool canBuildTr = true;

    private void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "BuildExist")
        {
            canBuildTr = false;
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "BuildExist")
        {
            canBuildTr = true;
        }
    }
}
