using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRayCaster : MonoBehaviour
{
   
   const string ClkLayer = "Clickable";

    [SerializeField] float CamRayLength = 20;

    int Layer;

    void Awake()
    {
        
    }

 
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LeftClick();
        }

        if (Input.GetMouseButtonDown(1))
        {
            RightClick();
        }
    }

    void LeftClick()
    {
       
        Layer = LayerMask.GetMask(ClkLayer);
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(camRay, out hit, CamRayLength, Layer))
        {
            Debug.Log("Left");
            if (hit.collider.tag == "Ground")
            {
                Ground G = hit.collider.GetComponent<Ground>();
                if (!G.isSelected && G.GetState() == 0)
                    G.SpawnBox();
            }
            else
            {
                if (hit.collider.tag == "Box")
                    Destroy(hit.collider.gameObject);
            }
        }
    }

    void RightClick()
    {
        Debug.Log("Right");
        Layer = LayerMask.GetMask(ClkLayer);
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(camRay, out hit, CamRayLength, Layer))
        {
            if(hit.collider.tag == "Ground")
            {
                Ground G = hit.collider.GetComponent<Ground>();
                G.Select();
                
            }
        }
    }
}
