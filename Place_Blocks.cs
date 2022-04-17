using UnityEngine;
public class GridPrototype : MonoBehaviour
{
  public vector3 V3;
  Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
  RaycastHit hitInfo;
  
  if (Physics.Raycast(ray, out hitInfo))
  {
    V3 = hitInfo.point + hitInfo.normal /2; //using raycast
  }
  
}
