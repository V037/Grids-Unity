using UnityEngine;

//credits: V037_

public class GridPrototype : MonoBehaviour
{
  private void MoveCurrentObjectToMouse()
  {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      public vector3 V3;
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit hitInfo;
      
      if (Physics.Raycast(ray, out hitInfo))
      {
        V3 = hitInfo.point + hitInfo.normal /2; //the important part using the power of the math
      }
   }
}
