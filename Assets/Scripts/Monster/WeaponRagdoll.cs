using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRagdoll : MonoBehaviour
{
    [Header("On Editor")]
    [SerializeField] List<GameObject> _meshs;
    [SerializeField] Rigidbody _rb;

    [Space]
    [SerializeField] float _existTime = 0.4f;
    float _countTime = 0f;
    bool _transparent = false;

    private void Start()
    {
        // transparent immediately when start
        foreach (GameObject mesh in _meshs)
        {
            iTween.FadeTo(mesh, 0f, _existTime - 0.05f);
            SetMaterialTransparent();
        }

        // weapon get a up speed
        _rb.velocity = new Vector3(0f, 10f, 0f);
    }

    private void Update()
    {
        _countTime += Time.deltaTime;
        if (_countTime > _existTime)
        {
            Destroy(gameObject);
        }
    }

    private void SetMaterialTransparent() // set material can transparent
    {
        foreach (GameObject mesh in _meshs)
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
