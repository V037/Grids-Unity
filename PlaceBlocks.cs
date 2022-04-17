using UnityEngine;

//credits: V.037_

public class PlaceBlocks : MonoBehaviour
{
  public vector3 V3;
  
  private void MoveCurrentObjectToMouse()
  {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit hitInfo;
      
      if (Physics.Raycast(ray, out hitInfo))
      {
        V3 = hitInfo.point + hitInfo.normal /2; //the important part using the power of the math
      }
   }
}
