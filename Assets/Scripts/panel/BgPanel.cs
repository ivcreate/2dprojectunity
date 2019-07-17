using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgPanel : MonoBehaviour
{
    private float sky_speed = 0.005f;
    void Start(){
        StartCoroutine("UpdateSpeedBg");
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = transform.position.x - sky_speed;

        transform.position = pos;
    }

    

    //бесконечный вызов увеличения скорости
    IEnumerator UpdateSpeedBg()
    {
        for (; ; )
        {
            //добавить камере скорости
            change_speed();
            //добавлять каждые 60 секунд
            yield return new WaitForSeconds(30f);
        }
    }

    void change_speed(){
        sky_speed *= -1;
    }
}
