using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float leftMargin = -5.25f;
    [SerializeField] float rightMargin = 5.25f;

    [SerializeField] GameObject boomPrefab;


    void Update()
    {
        // Player Tap
        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, transform.position);
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            float hitDistance = 0;

            if (plane.Raycast(ray, out hitDistance))
            {
                Vector3 mousePosition = ray.GetPoint(hitDistance);

                if (mousePosition.x >= leftMargin && mousePosition.x <= rightMargin)
                {
                    Instantiate(boomPrefab, new Vector3(mousePosition.x, 0.5f, mousePosition.z), Quaternion.identity);
                }       
            }
        }
    }
}
