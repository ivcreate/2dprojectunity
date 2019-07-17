using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCam : MonoBehaviour
{
    public GameObject player;
    private Vector3 target = new Vector3(0.0f, 10f, -10f);
    public float cum_speed = 0.00005f;
    public static int multiplier;
    
    void Start()
    {
        multiplier = 1;
        StartCoroutine("DoSpawnPlatform");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = transform.position;
        if(player.transform.position.y > transform.position.y)
            pos.y = player.transform.position.y;
        else
            pos.y += cum_speed;

        transform.position = pos;
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            Destroy(player);
        }

        if(other.tag == "Platform"){
            Destroy(other.gameObject);
        }
        
    }

    public static void AddScore(int num){

    }

    IEnumerator DoSpawnPlatform()
    {
        for (; ; )
        {
            SpawnPlatform();
            yield return new WaitForSeconds(5f);
        }
    }
 
    void SpawnPlatform()
    {
        Debug.Log("Hello");
    }
}
