using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private float offx;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;

        //transform.position = player.transform.position + offset;
        offx = player.transform.position.x - transform.position.x;
    }


    void LateUpdate()
    {
        //transform.position = player.transform.position + offset;
        transform.position = new Vector3(player.transform.position.x-offx, transform.position.y, transform.position.z);
    }
}
