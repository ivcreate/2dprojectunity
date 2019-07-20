using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Monetization;

public class GameController : MonoBehaviour
{
    public Button Pause;
    public Button StartBut;
    public Button Home;
    public Button Restart;
    public Button DeathHome;
    public Button DeathRestart;
    public Button Sound;
    public Button Video;
    public GameObject Player;
    public Canvas PauseUi;
    public Canvas DeathUi;
    private bool _pause_flag = false;
    private int _score;
    private bool _click_home = false;
    public static bool sound_flag = true;
    public bool monet_flag = false;
    
    

    void Start(){
        if(Monetization.isSupported) Monetization.Initialize("3220934",false);
        PauseUi.enabled = _pause_flag;
        DeathUi.enabled = false;
        Pause.onClick.AddListener(GamePause);
        StartBut.onClick.AddListener(GamePause);
        Video.onClick.AddListener(OnVideo);
        Sound.onClick.AddListener(OnSound);
        Home.onClick.AddListener(ClickHome);
        Restart.onClick.AddListener(RestartGame);
        DeathHome.onClick.AddListener(ClickHome);
        DeathRestart.onClick.AddListener(RestartGame);
    }

    public void OnSound(){
        sound_flag = !sound_flag;
    }

    public void OnVideo(){
        ShowAdCallbacks options = new ShowAdCallbacks();
        options.finishCallback = HandleShowResalt;
        ShowAdPlacementContent ad = Monetization.GetPlacementContent("rewardedVideo") as ShowAdPlacementContent;
        ad.Show(options);
    }

    void HandleShowResalt(ShowResult result){
        if(result == ShowResult.Finished){
            monet_flag = true;
            playerController.IncreaseLives();
        }else if(result == ShowResult.Skipped){
            
        }else if(result == ShowResult.Failed){

        }
    }

    public void RestartGame(){
        Debug.Log("fdsaf");
        Time.timeScale = 1;
        GetScore();
        if(_score > PlayerPrefs.GetInt("BestScoreInt"))
            PlayerPrefs.SetInt("BestScoreInt", _score);

        LoadNewScene("SampleScene"); 
    }

    public void GamePause(){
        if(_pause_flag == false){
            Time.timeScale = 0;
            _pause_flag = true;
        }else{
            Time.timeScale = 1;
            _pause_flag = false;
        }
        PauseUi.enabled = _pause_flag;
    }
    public void LoadNewScene(string scene){
        SceneManager.LoadScene(scene);
    }
    public void ClickHome(){
        _click_home = true;
        GameOver();
    }
    public void GameOver(){
        GetScore();
        if(_score > PlayerPrefs.GetInt("BestScoreInt"))
            PlayerPrefs.SetInt("BestScoreInt", _score);
        Color sprite = Player.GetComponent<SpriteRenderer>().color;
        sprite.a = 0f;
        Player.GetComponent<SpriteRenderer>().color = sprite;
        if(isDead()){
            Time.timeScale = 0;
            DeathUi.enabled = true;
        }else{
            Time.timeScale = 1;
            LoadNewScene("StartPanel");
        }
    }

    public bool isDead(){
        if(playerController.lives == 0 && _click_home == false)
            return true;
        
        return false;
    }

    public void GetScore(){
        GameObject Score = GameObject.Find("Score");
        var text = Score.GetComponent<Text>();
        _score = int.Parse(text.text);
        //return int.Parse(text.text);
    }


}
