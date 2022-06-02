using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GridPrototype : MonoBehaviour
{

    //modalità arcade dove raccogli i blocchi e rimangono quando li usi e li puoi riusare all'infinito
    //modalità normale dove raccogli i materiali e devi craftare i blocchi e rimangono quando li usi e li puoi riusare all'infinito
    //modalità realistica dove dove raccogli i materiali, dove devi fabbricarli, e spariscono una volta usati ma si possono recuperare se si arriva alla fine
    //e non si muore, tuttavia non ci sarà la barra della vità, solo suoni
    //per tutte le modalità ceh comunque uno shop con le casse

    [SerializeField]
    private bool buildModeOn = false;
    [SerializeField]
    private bool destroyModeOn = false;
    [SerializeField]
    private bool canBuild = false;
    public bool gamestart = false;

    [SerializeField]
    private Triggascr Triggascr;
    [SerializeField]
    private Triggascr2 Triggascr2;

    [SerializeField]
    private Vector3 Vrot3;
    [SerializeField]
    private Vector3 VR3;
    [SerializeField]
    private Vector3 V3;

    [SerializeField]
    private List <GameObject> currentObjects = new List <GameObject>();

    [SerializeField]
    private float GridSize;
    [SerializeField]
    private float blockettis = 1000;

    [SerializeField]
    private int rKeyRot = 15;

    [SerializeField]
    private int tKeyRot = 15;

    [SerializeField]
    private int objNum = 0;

    [SerializeField]
    private KeyCode newObjectHotkeyB = KeyCode.B;
    [SerializeField]
    private KeyCode newObjectHotkeyN = KeyCode.N;
    [SerializeField]
    private KeyCode newObjectHotkeyR = KeyCode.R;
    [SerializeField]
    private KeyCode newObjectHotkeyT = KeyCode.T;
    [SerializeField]
    private KeyCode newObjectHotkeyQ = KeyCode.Q;
    [SerializeField]
    private KeyCode newObjectHotkeyE = KeyCode.E;

    public GameObject currentPlaceableObject;
    [SerializeField]
    private GameObject placeableObjectPrefab;
    [SerializeField]
    private GameObject vectorsss;

    [SerializeField]
    private TMP_InputField SensibilityField;
    [SerializeField]
    private TMP_InputField rRotInput;

    private void Start()
    {
        numbMathe();
    }

    private void Update()
    {
        HandleNewObjectHotkey();
        if (currentPlaceableObject != null && buildModeOn)
        {
            MoveCurrentObjectToMouse();
            RotateFromMouseWheel();
            ReleaseIfClicked();
            //Findscr();
            //numbMathe();
        }
    }

    private void FixedUpdate()
    {
        if(gamestart)
        {
            for (int i = 0; i < currentObjects.Count; i++)
            {
                currentObjects[i].GetComponent<Rigidbody>().isKinematic = false;
            }
        }
        //using FixedUpdate for do 60 objects for 60 frames and don't make pc crash, slow loading method, ez :)
    }

    private void LateUpdate()
    {
        V3.x = Mathf.Round(VR3.x / GridSize) * GridSize;
        V3.y = Mathf.Round(VR3.y / GridSize) * GridSize;
        V3.z = Mathf.Round(VR3.z / GridSize) * GridSize;

        if(buildModeOn)
        {
            currentPlaceableObject.transform.position = V3;
        }
    }

    public void numbMathe() //executed from UI changes
    {   
        GridSize = float.Parse(SensibilityField.text);
    }

    private void RestoreDefault()
    {
        buildModeOn = false;
        canBuild = false;
        Destroy(currentPlaceableObject);
        Vrot3.x = 0;
        Vrot3.y = 0;
    }

    private void HandleNewObjectHotkey()
    {
        if (Input.GetKeyDown(newObjectHotkeyB))
        {
            if (currentPlaceableObject != null)
            {
                RestoreDefault();
            }
            else
            {
                Vrot3.x = 0;
                Vrot3.y = 0;
                destroyModeOn = false;
                buildModeOn = true;
                currentPlaceableObject = Instantiate(placeableObjectPrefab);
                Findscr();
            }
        }

        if (Input.GetKeyDown(newObjectHotkeyN))
        {
            if (currentPlaceableObject != null)
            {
                RestoreDefault();
            }

            if(!destroyModeOn)
            {
                destroyModeOn = true;
            }
            else
            {
                destroyModeOn = false;
            }
            
        }
    }

    private void Findscr()
    {
        Triggascr = currentPlaceableObject.GetComponentInChildren<Triggascr>();
        Triggascr2 = currentPlaceableObject.GetComponentInChildren<Triggascr2>();
    }

    private void MoveCurrentObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            //Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

            VR3.y = hitInfo.point.y + hitInfo.normal.y / 2;
            VR3.x = hitInfo.point.x + hitInfo.normal.x / 2;
            VR3.z = hitInfo.point.z + hitInfo.normal.z / 2;

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
        if(Input.GetKeyDown(newObjectHotkeyQ))
        {
            Vrot3.y += rKeyRot;
        }
        if(Input.GetKeyDown(newObjectHotkeyE))
        {
            Vrot3.y -= rKeyRot;
        }
        if(Input.GetKeyDown(newObjectHotkeyT))
        {
            Vrot3.x += tKeyRot;
        }
        if(Input.GetKeyDown(newObjectHotkeyR))
        {
            Vrot3.x -= tKeyRot;
        }
        currentPlaceableObject.transform.Rotate(Vector3.up, Vrot3.y);
        currentPlaceableObject.transform.Rotate(Vector3.right, -Vrot3.x);
        vectorsss.transform.localEulerAngles = Vrot3;
    }

    private void ReleaseIfClicked()
    {
        if (Input.GetMouseButtonDown(1) && canBuild && Triggascr.canBuildTr)
        {
            //Il centro geometrico del riquadro di delimitazione allineato all'asse contenente l'oggetto è a
            //gameObject.renderer.bounds.center
            Triggascr2.AddHinge();
            currentPlaceableObject.tag = "BuildExist";
            Destroy(currentPlaceableObject.transform.Find("Tr").gameObject);
            currentPlaceableObject.layer = LayerMask.NameToLayer("Default");
            currentPlaceableObject.AddComponent<BoxCollider>();
            blockettis = blockettis - 1;
            objNum = objNum + 1;
            currentPlaceableObject.name = "Obj" + objNum;
            currentObjects.Add (currentPlaceableObject);
            buildModeOn = false;
            currentPlaceableObject = null;
        }
    }
}

