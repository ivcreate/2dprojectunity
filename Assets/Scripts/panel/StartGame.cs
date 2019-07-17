using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public Button LoadButton;
    public Button ExitButton;

    private Animation _animatorController;
    private bool paused = false;

    void Start()
    {
        
        Button btn = LoadButton.GetComponent<Button>();
        _animatorController = LoadButton.GetComponent<Animation>();
        
        btn.onClick.AddListener(Anim);
        ExitButton.onClick.AddListener(Exit);
        //btn.onClick.AddListener(TaskOnClick);
    }
    void Anim()
    {
        _animatorController.Play("New Animation");
    }

    void Exit()
    {
        Application.Quit();
    }

}
