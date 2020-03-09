using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject _boomPrefab;

    [SerializeField] float _range = 30f;
    [SerializeField] int _shootableMask;
    Ray _touchRay;
    RaycastHit _touchHit;

    private void Start()
    {
        _shootableMask = LayerMask.GetMask("CollideEnvironment");
    }

    void Update()
    {
        // Player Tap
        if (Input.GetMouseButtonDown(0))
        {
          
            _touchRay = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);

            //float hitDistance = 0;
            //Plane plane = new Plane(Vector3.up, transform.position);
            //if (plane.Raycast(touchRay, out hitDistance))
            //{
            //    Vector3 mousePosition = touchRay.GetPoint(hitDistance);

            //    //if (mousePosition.x >= leftMargin && mousePosition.x <= rightMargin)
            //    {
            //        Instantiate(_boomPrefab, new Vector3(mousePosition.x, 0.5f, mousePosition.z), Quaternion.identity);
            //    }       
            //}

            if (Physics.Raycast(_touchRay, out _touchHit, _range, _shootableMask))
            {
                Debug.Log("Hit");
                Instantiate(_boomPrefab, _touchHit.point, Quaternion.identity);
            }
        }
    }

    public void VibrateMyPhone()
    {
        Handheld.Vibrate();
    }
}
