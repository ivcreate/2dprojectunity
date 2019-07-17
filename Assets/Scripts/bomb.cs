using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    public GameObject gobj;
    public GameObject Allert;
    private float rand;
    private static float _spown_time;
    public static bool _spown_start;

    void Start(){
        _spown_time = 5f;
        _spown_start = false;
        StartCoroutine("WaitSpawn");
    }
    IEnumerator WaitSpawn()
    {
        yield return new WaitForSeconds(10f);
        ChangeFlag();
    }

    public static void DecreaseSpawnTime(){
        if(_spown_start == true)
            _spown_time = _spown_time/2;
    }

    /* IEnumerator DecreaseSpawnTime()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(15f);

            _spown_time = _spown_time/2;
        }
    }*/

    void ChangeFlag() {
        StartCoroutine("AllertBomb");
        _spown_start = true;
        //StartCoroutine("DecreaseSpawnTime");
    }
    //бесконечный вызов уведомлений о падении бомб скорости
    IEnumerator AllertBomb()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(_spown_time);
            //добавить камере скорости
            IncreaseTimeAllertBomb();
            //добавлять каждую секунд
            yield return new WaitForSeconds(1f);
            //Добавляем бомбу
            IncreaseTimeSpawnBomb();
        }
    }
    
    void IncreaseTimeAllertBomb()
    {
        rand = Random.Range (-4.2f, 4.2f);
        var new_allert = Instantiate (Allert, new Vector2(rand,transform.position.y), Quaternion.identity);
        new_allert.transform.parent = GameObject.Find("Main Camera").transform;
    }
 
    void IncreaseTimeSpawnBomb()
    {
        //rand = Random.Range (-4.4f, 4.4f);
        Instantiate (gobj, new Vector2(rand,transform.position.y), Quaternion.identity);
    }

}
