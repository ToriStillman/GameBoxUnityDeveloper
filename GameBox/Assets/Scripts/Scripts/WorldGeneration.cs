using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    [SerializeField] private GameObject[] roadPrefab;
    [SerializeField] private GameObject[] trapsPrefab;
    [SerializeField] private GameObject[] bonusPrefab;
    [SerializeField] private GameObject[] grassPrefab;
    [SerializeField] private GameObject[] decorPrefab;
    [SerializeField] private GameObject[] linePrefab;

    [SerializeField] private Transform playerPosition;

    private List<GameObject> roadActive = new List<GameObject>();

    private int max;
    private int maxTrap = 0;
    private int maxBonus = 0;
    private int spawnPos;
    private int roadLenght = 2;
    private int size;
    private int maxRoads = 30;

    private bool nullObj;

    private void Awake()
    {
        max = player.MaxRoads();
        size = (max - 1) / 2;
        spawnPos = 8;

        for (int i = 0; i < maxRoads + 1; i++)
        {
            CreateRoad();
            spawnPos += roadLenght;
            nullObj = !nullObj;
        }
    }

    private void Update()
    {
        if (playerPosition.position.z - roadLenght > spawnPos - (maxRoads * roadLenght))
        {
            CreateRoad();
            DeleteRoad();
            spawnPos += roadLenght;
            nullObj = !nullObj;
        }
    }

    private void CreateRoad()
    {
        CreateDecor();

        for (int i = - size; i < size + 1; i++)
        {
            GameObject[] massive = roadPrefab;
            GameObject nextRoad = Instantiate(massive[Random.Range(0, massive.Length)], new Vector3(i * roadLenght, 0, spawnPos), transform.rotation);
            roadActive.Add(nextRoad);
            CreateTraps(i);
        }

        maxTrap = 0;
        maxBonus = 0;
        max = player.MaxRoads();
        size = (max - 1) / 2;
    }

    private void DeleteRoad()
    {
        for (int i = 0; i < size + 3; i++)
        {
            Destroy(roadActive[i]);
            roadActive.RemoveAt(i);
        }
    }

    private void CreateDecor()
    {
        GameObject nextRoad = Instantiate(linePrefab[Random.Range(0, linePrefab.Length)], new Vector3(-size * roadLenght - roadLenght * 2, 1, spawnPos), transform.rotation);
        roadActive.Add(nextRoad);

        nextRoad = Instantiate(linePrefab[Random.Range(0, linePrefab.Length)], new Vector3(size * roadLenght + roadLenght, 0, spawnPos), transform.rotation);
        roadActive.Add(nextRoad);
    }

    private void CreateTraps(int i)
    {
        GameObject[] massive = roadPrefab;

        if (i == size && maxBonus == 0)
        {
            massive = bonusPrefab;
            maxBonus++;
        }
        else
        {
            int a = Random.Range(1, 4);

            switch (a)
            {
                case 1:
                    if (maxTrap == max - size || nullObj)
                    {
                        massive = grassPrefab;
                    }
                    else
                    {
                        massive = trapsPrefab;
                        maxTrap++;
                    }
                    break;

                case 2:
                    if (maxTrap < max - size && !nullObj && maxBonus != 0)
                    {
                        massive = trapsPrefab;
                        maxTrap++;
                    }
                    else
                    {
                        massive = bonusPrefab;
                        maxBonus++;
                    }
                    break;

                case 3:
                    if (maxTrap < size && !nullObj)
                    {
                        massive = trapsPrefab;
                        maxTrap++;
                    }
                    else
                    {
                        massive = grassPrefab;
                    }
                    break;
            }

        }

        GameObject nextRoad = Instantiate(massive[Random.Range(0, massive.Length)], new Vector3(i * roadLenght, 0, spawnPos), transform.rotation);
        roadActive.Add(nextRoad);
    }
}
