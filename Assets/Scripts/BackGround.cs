using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    private float sky_speed = 0.005f;
    void Start(){
        StartCoroutine("UpdateSpeed");
    }
    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale != 0){
            Vector3 pos = transform.position;
            pos.x = transform.position.x - sky_speed;

            transform.position = pos;
            }
    }

        //бесконечный вызов увеличения скорости
    IEnumerator UpdateSpeed()
    {
        for (; ; )
        {
            //добавить камере скорости
            change_speed();
            //добавлять каждые 60 секунд
            yield return new WaitForSeconds(25f);
        }
    }

    void change_speed(){
        sky_speed *= -1;
    }
}
