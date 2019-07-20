using UnityEngine;
using UnityEngine.UI;

public class MainCam : MonoBehaviour
{
    public GameObject player;
    public float cum_speed = 0.00005f;
    public static int multiplier;
    public static float multiplier_timer;
    public Text UiMultiplier;
    public Text UiScore;
    public static int score;
    private static int _prev_score_multiplier;
    private static int _prev_score_live;
    
    void Start()
    {
        multiplier = 1;
        score = 0;
        _prev_score_multiplier = 0;
        _prev_score_live = 0;
        multiplier_timer = Time.time;
        SetScore();
        SetMultiplier();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        SetScore();
        SetMultiplier();
        Vector3 pos = transform.position;
        if(player.transform.position.y > transform.position.y){
            pos.y = player.transform.position.y;
            multiplier_timer = Time.time;
            AddScore(1);
        }else
            pos.y += cum_speed;

        if(multiplier_timer + 2f < Time.time)
            AddMultiplier(false);
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
        if(_prev_score_multiplier + 200*multiplier < score){
            AddMultiplier(true);
            _prev_score_multiplier = score;
        }
        if(_prev_score_live + 10 < score){
            playerController.IncreaseLives();
            _prev_score_live = score;
        }
    }

    public static void AddMultiplier(bool Add = true){
        if(Add == true){
            if(multiplier < 5){
                multiplier++;
            }
        }else{
            multiplier = 1;
            multiplier_timer = Time.time;
            _prev_score_multiplier = score;
        }
    }

}
