using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//credits: V.037_

public class GridPrototype : MonoBehaviour
{
    private bool buildModeOn = false;
    private bool canBuild = false;

    [SerializeField]
    private Vector3 V3;
    [SerializeField]
    private int GridSize;

    public float GridSensibility;

    [SerializeField]
    private GameObject placeableObjectPrefab;

    [SerializeField]
    private KeyCode newObjectHotkey = KeyCode.A;

    private GameObject currentPlaceableObject;

    private float mouseWheelRotation = 0.1f;

    private void Update()
    {
        HandleNewObjectHotkey();

        if (currentPlaceableObject != null)
        {
            MoveCurrentObjectToMouse();
            RotateFromMouseWheel();
            ReleaseIfClicked();


            currentPlaceableObject.transform.position = V3;
            //currentPlaceableObject.transform.position = hit.transform.position + hit.normal;
        }
        GridSize = (int)Mathf.Round(0.1f / GridSensibility);
    }

    private void HandleNewObjectHotkey()
    {
        if (Input.GetKeyDown(newObjectHotkey))
        {
            if (currentPlaceableObject != null)
            {
                Destroy(currentPlaceableObject);
            }
            else
            {
                currentPlaceableObject = Instantiate(placeableObjectPrefab);
            }
        }
    }

    private void MoveCurrentObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            V3.x = (float) System.Math.Round(hitInfo.point.x + hitInfo.normal.x /2, GridSize);
            V3.y = (float) System.Math.Round(hitInfo.point.y + hitInfo.normal.y /2, GridSize);
            V3.z = (float) System.Math.Round(hitInfo.point.z + hitInfo.normal.z /2, GridSize);
            //V3 = hitInfo.point + hitInfo.normal /2;
            currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
    }

    private void RotateFromMouseWheel()
    {
        Debug.Log(Input.mouseScrollDelta);
        mouseWheelRotation += Input.mouseScrollDelta.y;
        currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
    }

    private void ReleaseIfClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentPlaceableObject = null;
        }
    }
}

