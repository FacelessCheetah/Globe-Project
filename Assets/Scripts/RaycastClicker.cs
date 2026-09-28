using System;
using UnityEngine;

public class RaycastClicker : MonoBehaviour
{
    [SerializeField]LayerMask layerMask;
    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.white);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;

           
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.GetComponent<ChangeCubeSize>());
                hit.transform.GetComponent<ChangeCubeSize>().CubeHasBeenClickedOn();
            }
        }

    }
}