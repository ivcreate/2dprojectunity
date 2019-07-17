using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject player;

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            if(player.transform.position.x < -3.92f){
               player.transform.position = new Vector3(-player.transform.position.x-0.2f,player.transform.position.y,player.transform.position.z);
            }else if(player.transform.position.x > 3.92f){
                player.transform.position = new Vector3(-player.transform.position.x+0.2f,player.transform.position.y,player.transform.position.z);
            }
        }
    }
}
