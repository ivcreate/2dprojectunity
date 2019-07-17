using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplo : MonoBehaviour
{

    private void Explosion(){
        GameObject GameController = GameObject.Find("GameController");
        GameController contr = GameController.GetComponent<GameController>();
        contr.GameOver();
    }
}
