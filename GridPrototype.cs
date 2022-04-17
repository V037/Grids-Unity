using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//credits: V.037_

public class GridPrototype : MonoBehaviour
{
    private bool buildModeOn = false; //conditions for build
    private bool canBuild = false;

    public Triggascr Triggascr;

    private Vector3 VR3; //raw coordinates
    private Vector3 V3; //improved coordinates

    [SerializeField]
    private float GridSize;

    private float mouseWheelRotation = 0f; //Suggested value: 0

    [SerializeField]
    private KeyCode newObjectHotkey = KeyCode.A; //key for build
    [SerializeField]
    private GameObject placeableObjectPrefab; //the prefab of your object
    private GameObject currentPlaceableObject; //the spawned instance of your object

    private void Update()
    {
        HandleNewObjectHotkey();

        if (currentPlaceableObject != null && buildModeOn)
        {
            MoveCurrentObjectToMouse();
            RotateFromMouseWheel();
            ReleaseIfClicked();
            Findscr();
        }
    }

    private void LateUpdate()
    {
        V3.x = Mathf.Floor(VR3.x / GridSize) * GridSize; //the power of Mathf
        V3.y = Mathf.Floor(VR3.y / GridSize) * GridSize;
        V3.z = Mathf.Floor(VR3.z / GridSize) * GridSize;

        if(buildModeOn)
        {
            currentPlaceableObject.transform.position = V3; //coordinates converted to grid size
        }
    }

    private void HandleNewObjectHotkey()
    {
        if (Input.GetKeyDown(newObjectHotkey))
        {
            if (currentPlaceableObject != null)
            {
                Destroy(currentPlaceableObject);
                buildModeOn = false;
            }
            else
            {
                currentPlaceableObject = Instantiate(placeableObjectPrefab);
                buildModeOn = true;
            }
        }
    }

    private void Findscr()
    {
        Triggascr = currentPlaceableObject.GetComponent<Triggascr>(); //this the script you need to place in the prefab
    }

    private void MoveCurrentObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            VR3 = hitInfo.point + hitInfo.normal /2;
            currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            canBuild = true;
        }
        else
        {
            canBuild = false;
        }
    }

    private void RotateFromMouseWheel()
    {
        mouseWheelRotation += Input.mouseScrollDelta.y;
        currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
    }

    private void ReleaseIfClicked()
    {
        if (Input.GetMouseButtonDown(0) && canBuild && Triggascr.canBuildTr) //left mouse click and voila you placed a Cubic Abort
        {
            currentPlaceableObject.layer = LayerMask.NameToLayer("Default"); //the prefab need the layer "ignore raycast" and when you place it would become a normal placed block
            buildModeOn = false;
            currentPlaceableObject = null;
        }
    }
}

