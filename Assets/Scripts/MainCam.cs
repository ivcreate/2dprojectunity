using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainCam : MonoBehaviour
{
    public GameObject player;
    public float cum_speed = 0.00005f;
    public static int multiplier;
    public Text UiMultiplier;
    public Text UiScore;
    public static int score;
    
    void Start()
    {
        multiplier = 1;
        score = 0;
        SetScore();
        SetMultiplier();
        StartCoroutine("DoSpawnPlatform");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        SetScore();
        SetMultiplier();
        Vector3 pos = transform.position;
        if(player.transform.position.y > transform.position.y){
            pos.y = player.transform.position.y;
            AddScore(1);
        }else
            pos.y += cum_speed;

        transform.position = pos;
        
    }

    void OnTriggerEnter2D(Collider2D other){
        
        if(other.tag == "Platform"){
            Destroy(other.gameObject);
        }
        if(other.tag == "Bomb"){
            Destroy(other.gameObject);
        }
        if(other.tag == "Coin"){
            Destroy(other.gameObject);
        }
        
    }
    public void SetScore(){
        UiScore.text = score.ToString(); 
    }
    public void SetMultiplier(){
        UiMultiplier.text = multiplier.ToString();
    }
    public static void AddScore(int num){
        score += num*multiplier;
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
