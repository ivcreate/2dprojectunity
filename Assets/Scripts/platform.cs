using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
    public GameObject gobj;
    public GameObject coin;
    private Vector3 position;
    private bool _coinflag = false;
    private bool _doublu = false;

    void Start(){
        position = transform.position;
    }

    void Update(){
        if(position.y + 3f < transform.position.y){
            float rand = Random.Range (-4.6f, 4.6f);
            var pos = new Vector2(rand,transform.position.y);
            Instantiate (gobj, pos, Quaternion.identity);
            if(_doublu){
                _doublu = false;
                rand = Random.Range (-4.6f, 4.6f);
                pos = new Vector2(rand,transform.position.y);
                Instantiate (gobj, pos, Quaternion.identity);
            }
            if(_coinflag == true){
                rand = Random.Range (-4.4f, 4.4f);
                Instantiate (coin, pos + new Vector2(rand,1f), Quaternion.identity);
                _coinflag = false;
            }
            position = transform.position;
        }
        if(Random.Range(0,250) == 10){
            _coinflag = true; 
        }
        /*if(Random.Range(0,150) == 10){
            _doublu = true; 
        }*/
    }
}
