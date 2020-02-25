using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] List<GameObject> meshGOs;

    bool _transparent = false;
    [SerializeField] float count_Destroy = 1.5f;
    [SerializeField] float _effectTime = 0.4f;

    bool _notCollide = false;

    void Start()
    {
        //// transparent game object overtime
        //foreach (GameObject mesh in meshGOs)
        //{
        //    iTween.FadeTo(mesh, 0f, 0.95f);
        //    SetMaterialTransparent();
        //}
    }

    void Update()
    {
        count_Destroy -= Time.deltaTime; // count to destroy this game object
        if (count_Destroy < 0)
            Destroy(gameObject);

        if (!_transparent && count_Destroy < _effectTime)
        {
            _transparent = true;

            // transparent game object overtime
            foreach (GameObject mesh in meshGOs)
            {
                iTween.FadeTo(mesh, 0f, _effectTime - 0.05f);
                SetMaterialTransparent();

                if (_notCollide)
                {
                    SetNotCollide();
                }
            }
        }
    }

    public void SetUp(float pushForce, Transform rigGraphic, Color skinColor)
    {
        // set color like his origin game object
        foreach (GameObject graphicGO in meshGOs)
        {
            foreach (Material material in graphicGO.GetComponent<Renderer>().materials)
            {
                material.SetColor("_Color", skinColor);
            }
        }

        SetUp(pushForce, rigGraphic);
    }

    public void SetUp(float pushForce, Transform rigGraphic, bool notCollide = false)
    {
        // set up the Ragdool in the right transform
        Transform[] inputRigs = rigGraphic.transform.GetComponentsInChildren<Transform>();

        Transform[] myRigs = transform.GetComponentsInChildren<Transform>();
        for (int i = 0; i < myRigs.Length; i++)
        {
            myRigs[i].position = inputRigs[i].position;
            myRigs[i].rotation = inputRigs[i].rotation;

            // if this rig have rigidbody, aplly Velocity to it
            Rigidbody rb = myRigs[i].gameObject.GetComponent<Rigidbody>();
            if (rb)
            {
                // define the Velocity of the Ragdolls
                float x = Random.Range(-3, 3);
                float y = Random.Range(-2, 0);
                float z = 0;
                if (Random.Range(-1f, 1f) > 0)
                {
                    z = Random.Range(-10, -5);
                }
                else
                {
                    z = Random.Range(5, 10);
                }

                Vector3 newDirection = new Vector3(x, y, z);
                newDirection = newDirection.normalized;

                rb.velocity = newDirection * pushForce;
            }
        }

        _notCollide = notCollide;
    }

    public void SetUp_Small(float pushForce, Transform rigGraphic, bool notCollide = false)
    {
        // set up the Ragdool in the right transform
        Transform[] inputRigs = rigGraphic.transform.GetComponentsInChildren<Transform>();

        Transform[] myRigs = transform.GetComponentsInChildren<Transform>();
        for (int i = 0; i < myRigs.Length; i++)
        {
            myRigs[i].position = inputRigs[i].position;
            myRigs[i].rotation = inputRigs[i].rotation;

            // if this rig have rigidbody, aplly Velocity to it
            Rigidbody rb = myRigs[i].gameObject.GetComponent<Rigidbody>();
            if (rb)
            {
                // define the Velocity of the Ragdolls
                float x = Random.Range(-1.5f, 1.5f);
                float y = Random.Range(-1f, 1f);
                float z = 0;
                if (Random.Range(-1f, 1f) > 0)
                {
                    z = Random.Range(-3f, -0.5f);
                }
                else
                {
                    z = Random.Range(0.5f, 3f);
                }

                Vector3 newDirection = new Vector3(x, y, z);
                newDirection = newDirection.normalized;

                rb.velocity = newDirection * pushForce;
            }
        }

        _notCollide = notCollide;
    }

    private void SetMaterialTransparent()
    {
        foreach (GameObject mesh in meshGOs)
        {
            foreach (Material material in mesh.GetComponent<Renderer>().materials)
            {
                material.SetFloat("_Mode", 2);
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.EnableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMMULTIPLY_ON");
                material.renderQueue = 3000;
            }
        }
    }

    private void SetNotCollide()
    {
        foreach (Collider collider in GetComponentsInChildren<Collider>())
        {
            collider.enabled = false;
        }
    }
}
