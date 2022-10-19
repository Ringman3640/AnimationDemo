using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraFollowerPlayer : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Assert.IsNotNull(player);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = player.transform.position;
        newPos.z = transform.position.z;
        transform.position = newPos;
    }
}
