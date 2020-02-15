using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] GameObject ragdollPrefab;
    [SerializeField] Transform rigGraphic;

    [SerializeField] int maxHP;
    int currentHP;

    private void OnTriggerEnter(Collider other)
    {   
        if (other.tag == Tags.Player_Attack)
        {
            Create_Ragdoll();

            Destroy(gameObject);
        }
    }

    void Create_Ragdoll()
    {
        GameObject ragdoll = Instantiate(ragdollPrefab, rigGraphic.position, rigGraphic.rotation);
        Mummy_RagDoll mumRagdoll = ragdoll.GetComponent<Mummy_RagDoll>();
        mumRagdoll.SetUp(4f, rigGraphic);

    }

    public void AddDamage(int amount)
    {
        currentHP -= amount;
    }
}
