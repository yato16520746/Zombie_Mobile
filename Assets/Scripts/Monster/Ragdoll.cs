using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] List<GameObject> meshGOs;

    bool _transparent = false;
    [SerializeField] float _countExist = 1.5f;
    [SerializeField] float _effectTime = 0.4f;

    private void OnValidate()
    {
        meshGOs.Clear();

        // get all mesh game objects
        foreach (SkinnedMeshRenderer skin in GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            meshGOs.Add(skin.gameObject);
        }

        foreach (MeshRenderer skin in GetComponentsInChildren<MeshRenderer>())
        {
            meshGOs.Add(skin.gameObject);
        }
    }

    void Update()
    {
        _countExist -= Time.deltaTime; // count to destroy this game object
        if (_countExist < 0)
            Destroy(gameObject);

        if (!_transparent && _countExist < _effectTime)
        {
            _transparent = true;

            // transparent game object overtime
            foreach (GameObject mesh in meshGOs)
            {
                iTween.FadeTo(mesh, 0f, _effectTime - 0.05f);
            }
            SetMaterialTransparent();
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

    public void SetUp_SpaceMan(float pushForce, Transform rigGraphic, Color color)
    {
        SetUp(pushForce, rigGraphic, color);
        SetMaterialTransparent();
        // transparent game object overtime
        foreach (GameObject mesh in meshGOs)
        {
            iTween.FadeTo(mesh, 1f, 0.2f);
        }
    }

    public void SetUp(float pushForce, Transform rigGraphic)
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
                float x = Random.Range(-3f, 3f);
                float y = Random.Range(-2f, 0f);
                float z = 0;
                if (Random.Range(-1f, 1f) > 0)
                {
                    z = Random.Range(-10f, -5f);
                }
                else
                {
                    z = Random.Range(5f, 10f);
                }

                Vector3 newDirection = new Vector3(x, y, z);

                rb.velocity = newDirection.normalized * pushForce;
            }
        }
    }

    public void SetUp_NoPushForce(Transform rigGraphic)
    {
        // set up the Ragdool in the right transform
        Transform[] inputRigs = rigGraphic.transform.GetComponentsInChildren<Transform>();
        Transform[] myRigs = transform.GetComponentsInChildren<Transform>();

        for (int i = 0; i < myRigs.Length; i++)
        {
            myRigs[i].position = inputRigs[i].position;
            myRigs[i].rotation = inputRigs[i].rotation;
        }
    }

    private void SetMaterialTransparent() // set the mesh can transparent
    {
        foreach (GameObject go in meshGOs)
        {
            foreach (Material material in go.GetComponent<Renderer>().materials)
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
}
