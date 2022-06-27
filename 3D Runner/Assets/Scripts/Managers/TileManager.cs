using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] prefab;
    public float spawn;
    public float roadLength;
    public float lengthToDespawn;
    public int roadOnScreen = 5;

    private List<GameObject> activeRoad = new List<GameObject>();
    public Transform player;

    void Start()
    {
        for (int x = 0; x < roadOnScreen; x++)
        {
            if (!BossVariable.startChasing)
                SpawnTile(0);
            else
                SpawnTile(Random.Range(1, prefab.Length));
        }

    }

    // Update is called once per frame
    void Update()
    {
        //respawn road
        if (player.transform.position.z - lengthToDespawn > spawn - (roadOnScreen * roadLength) && BossVariable.startChasing)
        {
            SpawnTile(Random.Range(1, prefab.Length));

            //if (BossVariable.injuredMultiplier <= 1)
            //    SpawnTile(0);
            //else
            //    SpawnTile(Random.Range(1, prefab.Length));

            DeleteRoad();
        }
    }

    //instantiate random tile patterns
    public void SpawnTile(int index)
    {
        GameObject go = Instantiate(prefab[index], transform.forward * spawn, transform.rotation);
        activeRoad.Add(go);
        spawn += roadLength;
    }

    //despawn road
    void DeleteRoad()
    {
        Destroy(activeRoad[0]);
        activeRoad.RemoveAt(0);
    }
}
