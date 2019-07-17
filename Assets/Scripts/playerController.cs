using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    public GameObject MainCamera;
    private MainCam MainCam;
    public Button left_btn;
    public Button right_btn;
    public Button StartBut;
    // variable to hold a reference to our SpriteRenderer component
    private SpriteRenderer heroSprite;
    public float speed = 20f;
    public float fallMultiplier = 2.5f;
    public float lowJump = 2f;
    //[Range(1,10)]
    public float jump_velocity = 20f;
    private Rigidbody2D rb;
    public static int lives = 3;
    public Text UiLives;

    public float invul_sec = 3f;
    public bool invul_flag = false;

    public AudioSource JumpAudio;
    public AudioSource ExploAudio;

    private Animator _anim_hero;


    public int directionInput;

    public GameObject[] platforms;
    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
        left_btn.onClick.AddListener(UpdateUiLives);
        right_btn.onClick.AddListener(UpdateUiLives);
        MainCam = MainCamera.GetComponent<MainCam>();
        rb = GetComponent <Rigidbody2D> ();
        _anim_hero = GetComponent <Animator> ();
        JumpAudio = GetComponent <AudioSource> ();
        if(GameController.sound_flag == false)
            JumpAudio.Stop();
        // get a reference to the SpriteRenderer component on this gameObject
        heroSprite = GetComponent<SpriteRenderer>();
        //обновление жизни
        UpdateUiLives();
    }


    void Update(){
        //смена позиции при нажатии вправо или лево
        float moveX = directionInput* speed * Time.deltaTime;
        //if(Input.acceleration.x != 0)
          //  moveX = Input.acceleration.x*100* speed * Time.deltaTime;
        //поворачиваем героя относительно движения
        if((Input.GetAxis ("Horizontal") > 0 || directionInput > 0 )&&  heroSprite.flipX == true)
            heroSprite.flipX = false;
        else if((Input.GetAxis ("Horizontal") < 0 || directionInput < 0) &&  heroSprite.flipX == false)
            heroSprite.flipX = true;
        //прыжки
        if(rb.velocity.y < 0){
            rb.velocity = new Vector2 (moveX,rb.velocity.y + Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime); 
        }else{
            rb.velocity = new Vector2 (moveX,rb.velocity.y + Physics2D.gravity.y * (lowJump - 1) * Time.deltaTime);  
        }
    }

    public void Move(int InputAxis)
    {
        directionInput = InputAxis;
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Platform"){
            
            rb.velocity = new Vector2(0f, jump_velocity);
            if(GameController.sound_flag == true)
                JumpAudio.Play();
        }

        if(col.tag == "Coin"){
            MainCam.AddScore(500);
            Destroy(col.gameObject);
        }
        
       if(col.tag == "MainCamera" && lives <= 1){
            StartCoroutine("InvulTime");
            Handheld.Vibrate();
            lives--;
            UpdateUiLives();
            GameObject GameController = GameObject.Find("GameController");
            GameController contr = GameController.GetComponent<GameController>();
            contr.GameOver();
        }else if(col.tag == "MainCamera"){
            MainCam.multiplier = 1;
            StartCoroutine("InvulTime");
            Handheld.Vibrate();
            lives--;
            UpdateUiLives();
            PlayerOnPlatform();
        }
        
        if(invul_flag == false)
            if(col.tag == "Bomb" && lives <= 1){
                StartCoroutine("InvulTime");
                Handheld.Vibrate();
                lives--;
                UpdateUiLives();
                Renderer rend = GetComponent<Renderer>();
                rend.enabled = false;
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                //AudioSource audio = col.GetComponent<AudioSource>();
                //audio.Play(0);
                Animator bomb = col.GetComponent<Animator>();
                Rigidbody2D pos_bomb = col.GetComponent<Rigidbody2D>();
                pos_bomb.constraints = RigidbodyConstraints2D.FreezePositionY;
                //bomb.wrapMode = WrapMode.Once;
                bomb.Play("explo");
            }else if(col.tag == "Bomb"){
                MainCam.multiplier = 1;
                StartCoroutine("InvulTime");
                Handheld.Vibrate();
                lives--;
                UpdateUiLives();
            }
    }

    public void UpdateUiLives(){
        UiLives.text = lives.ToString();
    }

    public void PlayerOnPlatform(){
        platforms = GameObject.FindGameObjectsWithTag("Platform");
        transform.position = new Vector3(platforms[platforms.Length - 3].transform.position.x,platforms[platforms.Length - 3].transform.position.y,platforms[platforms.Length - 3].transform.position.z);
    }

    public static void IncreaseLives(){
        lives++;
    }

    IEnumerator InvulTime()
    {
        _anim_hero.Play("invul");
        InvulChange();
        yield return new WaitForSeconds(invul_sec);
        _anim_hero.Play("wink");
        InvulChange();
    }

    public void InvulChange(){
        invul_flag  = !invul_flag;
    }
}
