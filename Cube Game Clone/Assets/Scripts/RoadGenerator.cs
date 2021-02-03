using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
//using System;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] private const float DistanceToSpawn = 10f;
    [SerializeField] private Transform PlayerPosition;

    [SerializeField] private Transform roadpart_Start;
    [SerializeField] private List<Transform> RoadPartsList;
    [SerializeField] private List<Transform> RoadHistory;

    private Vector3 LastEndPosition;

    void Start()
    {
        LastEndPosition = roadpart_Start.Find("EndPosition").position;
        RoadHistory.Add(roadpart_Start);
        Ending();
        Ending();
    }


    void Update()
    {

        PlayerPosition = GameObject.Find("Player").GetComponent<Transform>();
        if (Vector3.Distance(PlayerPosition.position, LastEndPosition) < DistanceToSpawn)
        {
            Ending();
        }

        Delete();

    }

    private void Ending()
    {
        Transform chosenroadpart = RoadPartsList[Random.Range(0, RoadPartsList.Count)];
        RoadHistory.Add(chosenroadpart);

        Transform LastPartTransform = SpawnRoadPart(chosenroadpart, LastEndPosition);
        LastEndPosition = LastPartTransform.Find("EndPosition").position;
        Delete();
    }

    private Transform SpawnRoadPart(Transform LevelPart, Vector3 SpawnPos)
    {
        Transform LevelPartTransform = Instantiate(LevelPart, SpawnPos, Quaternion.identity);
        return LevelPartTransform;
    }

    private void Delete()
    {
        if (RoadHistory.Count > 3)
        {
            Destroy(GameObject.Find(Levagas(RoadHistory[0].ToString())));
            RoadHistory.Remove(RoadHistory[0]);
        }
    }

    private string Levagas(string trans)
    {
        if (trans.Length == 28)
        {
            return trans.Substring(0, 4) + "(Clone)";
        }
        else if (trans.Length == 36)
        {
            return trans.Substring(0, 12);
        }
        else
        {
            return trans;
        }
    }

}
