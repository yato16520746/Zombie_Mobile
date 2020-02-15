using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mummy_RagDoll : MonoBehaviour
{
    [SerializeField] List<GameObject> meshGOs;

    float count_Destroy = 2f;

    void Start()
    {
        foreach (GameObject mesh in meshGOs)
        {
            iTween.FadeTo(mesh, 0f, 1.95f);
            SetMaterialTransparent();
        }
    }

    void Update()
    {
        count_Destroy -= Time.deltaTime; // count to destroy this game object
        if (count_Destroy < 0)
            Destroy(gameObject);
    }

    public void SetUp(float pushForce, Transform rigGraphic)
    {


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
                // define the Velocity of the Ragdoll
                float x = Random.Range(-2f, 2f);
                float y = Random.Range(5f, 10f);
                float z = Random.Range(5f, 15f);

                Vector3 newDirection = new Vector3(x, y, z);
                newDirection = newDirection.normalized;

                rb.velocity = newDirection * pushForce;
            }
        }
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
}
