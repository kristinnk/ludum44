using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifeprogress : MonoBehaviour
{
    public int fieldnr;
    private int value;

    public Sprite workSprite;
    public Sprite funSprite;
    public Sprite popSprite;
    public GameObject indicator;

    SpriteRenderer sr = new SpriteRenderer();

    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();

        switch (fieldnr)
        {
            case 1:
                value = staticData.Field1;
                break;
            case 2:
                value = staticData.Field2;
                break;
            case 3:
                value = staticData.Field3;
                break;
            case 4:
                value = staticData.Field4;
                break;
            case 5:
                value = staticData.Field5;
                break;
            case 6:
                value = staticData.Field6;
                break;
            case 7:
                value = staticData.Field7;
                break;
            case 8:
                value = staticData.Field8;
                break;
            case 9:
                value = staticData.Field9;
                break;
            default: break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(value)
        {
            case 1:
                sr.sprite = funSprite;
                break;
            case 2:
                sr.sprite = popSprite;
                break;
            case 3:
                sr.sprite = workSprite;
                break;
        }

        int currentProgress = (staticData.LifeProgress + 1) / 2;
        if (currentProgress == fieldnr)
        {
            indicator.SetActive(true);
        }
        else
        {
            indicator.SetActive(false);
        }

    }
}
