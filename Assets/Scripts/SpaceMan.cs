using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceMan : Monster
{
    [Header("Mesh")]
    [SerializeField] List<GameObject> _meshs;

    [Header("Ragdoll")]
    [SerializeField] GameObject _ragdollPref;
    [SerializeField] Transform _graphicRoot;

    // Space Man change velocity when him invisible
    Vector3 _direction = new Vector3(0, 0, -1);
    float _timeChange;
    bool _invisible = false;
    bool _changeVelocityFlag = true;

    float _countTime = 0; // counting time to make Space Man invisible
    int _countChanged = 0;

    #region Override function
    protected override void Start()
    {
        base.Start();

        // random time for invisible
        _timeChange = Random.Range(6f, 10f) / _moveSpeed;
    }
    
    protected override void OnValidate()
    {
        base.OnValidate();

        _meshs.Clear();

        // get all mesh game objects
        foreach (SkinnedMeshRenderer skin in GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            _meshs.Add(skin.gameObject);
        }

        foreach (MeshRenderer skin in GetComponentsInChildren<MeshRenderer>())
        {
            _meshs.Add(skin.gameObject);
        }
    }

    protected override void Create_Ragdoll()
    {
        GameObject ragdoll = Instantiate(_ragdollPref, _graphicRoot.position, _graphicRoot.rotation);
        Ragdoll mumRagdoll = ragdoll.GetComponent<Ragdoll>();

        Material mat = _meshs[0].GetComponent<Renderer>().materials[0];
        mumRagdoll.SetUp_SpaceMan(4f, _graphicRoot, mat.GetColor("_Color"));
    }
    #endregion

    void Update()
    {
        _countTime += Time.deltaTime;

        if (!_invisible) // you art visible
        {
            // when he is visible, just go straight
            if (!_changeVelocityFlag)
            {
                _changeVelocityFlag = true;
                _moveSpeed /= 1.7f; // slow down movement
                _direction = new Vector3(0, 0, -1);
            }

            if (_countTime > _timeChange && _countChanged < 2) // go to invisible
            {
                _countChanged++;
                _invisible = true;
                _countTime = 0;
                _timeChange = Random.Range(4f, 7f) / _moveSpeed;
                _changeVelocityFlag = false;

                SetMaterialTransparent();
                // transparent game object overtime
                foreach (GameObject go in _meshs)
                {
                    iTween.FadeTo(go, 0.25f, 0.2f);
                }
            }
        }
        else
        {
            // when he is invisible, go random
            if (!_changeVelocityFlag && _countTime > 0.2f)
            {
                _changeVelocityFlag = true;
                _moveSpeed *= 1.7f; // speed up movement

                int random = Random.Range(0, 3);
                if (random == 0)
                {
                    _direction.x = Random.Range(-0.9f, -0.6f);
                }
                else if (random == 2)
                {
                    _direction.x = Random.Range(0.6f, 0.9f);
                }
            }

            if (_countTime > _timeChange) // go to visible
            {
                _invisible = false;
                _countTime = 0;
                _timeChange = Random.Range(15f, 24f) / _moveSpeed;
                _changeVelocityFlag = false;

                foreach (GameObject go in _meshs)
                {
                    iTween.FadeTo(go, 1f, 0.2f);
                }
            }
        }

        // apply velocity
        _rb.velocity = _direction.normalized * _moveSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == Tags.Wall)
        {
            _direction.x = -_direction.x;
        }
    }

    private void SetMaterialTransparent() // set the mesh can transparent
    {
        foreach (GameObject mesh in _meshs)
        {
            foreach (Material mat in mesh.GetComponent<Renderer>().materials)
            {
                mat.SetFloat("_Mode", 2);
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                mat.SetInt("_ZWrite", 1);
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.EnableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                mat.renderQueue = 3000;
            }
        }
    }
}
