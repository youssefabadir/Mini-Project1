using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class FloorManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] Environmnet;
    public Transform PlayerPostion;
    float Spawn = -1.0f;
    float FloorLength = 100;
    int NOF = 3;
    float SafeZone = 120;
    int LastIndex = 0;
    List<GameObject> CurrentFloor = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < NOF; i++)
        {
            if (i == 0)
                SpawnFloor(0);
            else
                SpawnFloor();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPostion.position.z - SafeZone > (Spawn - NOF * FloorLength))
        {
            SpawnFloor();
            DeleteFloor();
        }
    }

    void SpawnFloor(int index = -1)
    {
        GameObject Move;
        if (index == -1)
            Move = Instantiate(Environmnet[0]) as GameObject;
        else
            Move = Instantiate(Environmnet[index]) as GameObject;
        Move.transform.SetParent(transform);
        Move.transform.position = Vector3.forward * Spawn;
        Spawn += FloorLength;
        CurrentFloor.Add(Move);
    }

    void DeleteFloor()
    {
        Destroy(CurrentFloor[0]);
        CurrentFloor.RemoveAt(0);

    }

    private int RandomObstacle()
    {
        if (Environmnet.Length <= 1)
        {
            return 0;
        }
        int RandomIndex = LastIndex;
        while (RandomIndex == LastIndex)
        {
            RandomIndex = Random.Range(0, Environmnet.Length);
        }

        LastIndex = RandomIndex;
        return RandomIndex;
    }
}
