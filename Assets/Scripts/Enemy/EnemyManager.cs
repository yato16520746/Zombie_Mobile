using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] float leftMargin;
    public float LeftMargin { get { return leftMargin; } }
    [SerializeField] float rightMargin;
    public float RightMargin { get { return rightMargin; } }

    [SerializeField] List<float> spawnTimeList;

    [SerializeField] GameObject MummyPref;

    double time = 0;

    void Update()
    {
        time += Time.deltaTime;

        if (spawnTimeList.Count > 0 && time > spawnTimeList[0])
        {
            float randomX = Random.Range(leftMargin, rightMargin);
            Instantiate(MummyPref, new Vector3(randomX, 0, 25), Quaternion.identity);
            spawnTimeList.RemoveAt(0);
        }
    }
}
