using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var text = GetComponent<Text>();
        if(PlayerPrefs.HasKey("BestScoreInt")){
            var score = PlayerPrefs.GetInt("BestScoreInt");
            text.text = "Best Score: "+score.ToString();
        } 
    }
}
