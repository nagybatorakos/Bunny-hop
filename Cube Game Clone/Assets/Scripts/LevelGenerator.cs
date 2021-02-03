using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
//using System;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private const float DistanceToSpawn = 80f;
    [SerializeField] private Transform PlayerPosition;

    [SerializeField] private Transform levelpart_Start;
    [SerializeField] private List<Transform> LevelPartsList;
    [SerializeField] private List<Transform> LevelHistory;

    private Vector3 LastEndPosition;



    void Start()
    {
        LastEndPosition = levelpart_Start.Find("EndPosition").position;
        LevelHistory.Add(levelpart_Start);
        Ending();
        Ending();
    }


    void Update()
    {
        //PlayerPosition = GameObject.Find("Player").GetComponent<Transform>();
        if (Vector3.Distance(PlayerPosition.position,LastEndPosition)< DistanceToSpawn)
        {
            Ending();
        }

        Delete();
    }

    private void Ending()
    {
        Transform chosenlevelpart = LevelPartsList[Random.Range(0, LevelPartsList.Count)];
        LevelHistory.Add(chosenlevelpart);
        Transform LastPartTransform = SpawnLevelPart(chosenlevelpart,LastEndPosition);
        LastEndPosition = LastPartTransform.Find("EndPosition").position;
        Delete();
    }

    private Transform SpawnLevelPart(Transform LevelPart,Vector3 SpawnPos)
    {
        Transform LevelPartTransform = Instantiate(LevelPart, SpawnPos, Quaternion.identity);
        return LevelPartTransform;
    }

    private void Delete()
    {
        if(LevelHistory.Count>6)
        {
            Destroy(GameObject.Find(Levagas(LevelHistory[0].ToString())));
            LevelHistory.Remove(LevelHistory[0]);

            GameObject.Find("bunny").GetComponent<PlayerController>().score++;

        }
    }
 
    private string Levagas(string trans)
    {
        if (trans.Length==31)
        {
            return trans.Substring(0,7)+"(Clone)";
        }
        else if(trans.Length == 37)
        {
            return trans.Substring(0, 13);
        }
        else
        {
            return trans;
        }
    }

}
