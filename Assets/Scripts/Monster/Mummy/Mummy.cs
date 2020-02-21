using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mummy : Monster
{
    [Header("Auto Skin Color")]
    [SerializeField] List<Color> _skinColors;
    [SerializeField] List<GameObject> _graphicGameObjects;
    Color _myColor;

    [Header("Ragdoll")]
    [SerializeField] GameObject _ragdollPref;
    [SerializeField] Transform _graphicRoot;

    protected override void Start()
    {
        base.Start();

        // set random skin color for mummy
        int random = Random.Range(0, _skinColors.Count);
        _myColor = _skinColors[random];

        foreach (GameObject graphicGO in _graphicGameObjects)
        {
            foreach (Material material in graphicGO.GetComponent<Renderer>().materials)
            {
                material.SetColor("_Color", _myColor);
            }
        }
    }

    private void Update()
    {
        // apply velocity alway go down
        _rb.velocity = new Vector3(0, 0, -1) * _moveSpeed;
    }


    #region Override function
    // Override function
    protected override void OnValidate()
    {
        base.OnValidate();
    }

    protected override void Create_Ragdoll()
    {
        GameObject ragdoll = Instantiate(_ragdollPref, _graphicRoot.position, _graphicRoot.rotation);
        Mummy_RagDoll mumRagdoll = ragdoll.GetComponent<Mummy_RagDoll>();
        mumRagdoll.SetUp(4f, _graphicRoot, _myColor);
    }
    #endregion
}
