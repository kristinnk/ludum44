using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class checkButtonStatus : MonoBehaviour
{
    public GameObject handler;
    public Sprite EnabledButton;
    public Sprite DisabledButton;
    public Sprite PressedButton;

    private SpriteRenderer sr;
    private confirmHandler ch;

    // Start is called before the first frame update
    void Start()
    {
        ch = handler.GetComponent<confirmHandler>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = DisabledButton;
    }

    // Update is called once per frame
    void Update()
    {   
        if (sr.sprite != PressedButton) { 
            if (ch.EmptyFields > 0)
            {
                sr.sprite = DisabledButton;
            } else
            {
                sr.sprite = EnabledButton;
            }
        }
    }

    private void OnMouseDown()
    {
        if (staticData.Field1 != 0 &&
            staticData.Field2 != 0 &&
            staticData.Field3 != 0 &&
            staticData.Field4 != 0 &&
            staticData.Field5 != 0 &&
            staticData.Field6 != 0 &&
            staticData.Field7 != 0 &&
            staticData.Field8 != 0 &&
            staticData.Field9 != 0)
        {
            GetComponent<AudioSource>().Play();
            sr.sprite = PressedButton;
        }
    }

    private void OnMouseUp()
    {
        if (staticData.Field1 != 0 &&
            staticData.Field2 != 0 &&
            staticData.Field3 != 0 &&
            staticData.Field4 != 0 &&
            staticData.Field5 != 0 &&
            staticData.Field6 != 0 &&
            staticData.Field7 != 0 &&
            staticData.Field8 != 0 &&
            staticData.Field9 != 0)
        {
            sr.sprite = EnabledButton;
            staticData.ChoiceHasBeenMade = false;
            staticData.LifeProgress = 1;
            GetComponent<AudioSource>().Stop();
            SceneManager.LoadScene(sceneName: "LiveLife");
        }
    }
}
