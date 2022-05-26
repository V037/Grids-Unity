using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggascr2 : MonoBehaviour
{
    public bool touchExist = false;

    [SerializeField]
    private GridPrototype gridPrototype;

    private List <Rigidbody> currentCollisions = new List <Rigidbody> ();
    private List <FixedJoint> currentJoints = new List <FixedJoint> ();

    private void Start()
    {
        gridPrototype = GameObject.Find("GridManager").GetComponent<GridPrototype>();
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "BuildExist")
        {
            touchExist = true;
            //otherObject = collider.GetComponent<Rigidbody>();
        }
    }

    public void AddHinge()
    {
        if(touchExist)
        {
            for (int i = 0; i < currentCollisions.Count; i++)
            {
                currentJoints.Add(gridPrototype.currentPlaceableObject.AddComponent<FixedJoint>());
                currentJoints[i].connectedBody = currentCollisions[i];
                //currentCollisions[i].isKinematic = false;
                currentCollisions[i].useGravity = true;
            }
        }
    }

    private void OnTriggerEnter (Collider collider)
    {
        if (collider.tag == "BuildExist")
        {
            touchExist = true;
            currentCollisions.Add (collider.GetComponent<Rigidbody>());      
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "BuildExist")
        {
            currentCollisions.Remove (collider.GetComponent<Rigidbody>());
            touchExist = false;
        }
    }
}
