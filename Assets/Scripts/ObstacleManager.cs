using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] Environmnet;
    public Transform PlayerPostion;
    float Spawn = -1.0f;
    float FloorLength = 30;
    int NOF = 9;
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
            Move = Instantiate(Environmnet[RandomObstacle()]) as GameObject;
        else
            Move = Instantiate(Environmnet[index]) as GameObject;
        Move.transform.SetParent(transform);
        Move.transform.position = Vector3.forward * Spawn;
        if (Move.tag == "F1")
            Move.transform.position = new Vector3(0, 1, Move.transform.position.z);

        if (Move.tag == "F1R")
            Move.transform.position = new Vector3(-2, 1, Move.transform.position.z);

        if (Move.tag == "F1L")
            Move.transform.position = new Vector3(2, 1, Move.transform.position.z);

        if (Move.tag == "F2R")
            Move.transform.position = new Vector3(1, 1, Move.transform.position.z);

        if (Move.tag == "F2L")
            Move.transform.position = new Vector3(-1, 1, Move.transform.position.z);

        if (Move.tag == "H3U")
            Move.transform.position = new Vector3(0, 2, Move.transform.position.z);

        if (Move.tag == "H3D")
            Move.transform.position = new Vector3(0, 0, Move.transform.position.z);

        if (Move.tag == "H2RU")
            Move.transform.position = new Vector3(1, 2, Move.transform.position.z);

        if (Move.tag == "H2RD")
            Move.transform.position = new Vector3(1, 0, Move.transform.position.z);

        if (Move.tag == "H2LU")
            Move.transform.position = new Vector3(-1, 2, Move.transform.position.z);

        if (Move.tag == "H3LD")
            Move.transform.position = new Vector3(-2, 0, Move.transform.position.z);
    
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
