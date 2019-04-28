using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faceScript : MonoBehaviour
{
    public Sprite frownFace;
    public Sprite sadFace;
    public Sprite neutralFace;
    public Sprite happyFace;
    public Sprite ecstaticFace;
    public string faceType;

    public GameObject plusSign;
    public GameObject minusSign;

    private SpriteRenderer sr;
    
    private int typeValue;
    private int oldTypeValue;
    private float originalPosY;

    private void Start()
    {
        staticData.HappyStatus = 2;
        staticData.MoneyStatus = 2;
        staticData.PopularityStatus = 2;
        sr = GetComponent<SpriteRenderer>();

        originalPosY = sr.transform.position.y;

        getTypeValue();
        oldTypeValue = typeValue;

    }

    private void getTypeValue()
    {
        switch (faceType)
        {
            case "money":
                typeValue = staticData.MoneyStatus;
                break;
            case "happiness":
                typeValue = staticData.HappyStatus;
                break;
            case "popularity":
                typeValue = staticData.PopularityStatus;
                break;
            default: break;
        }
    }

    private void Update()
    {
        sr.transform.position = new Vector2(sr.transform.position.x, originalPosY + Mathf.PingPong(Time.time*0.05f, 0.05f));

        // Cap the values
        if (staticData.MoneyStatus < 0) staticData.MoneyStatus = 0;
        if (staticData.HappyStatus < 0) staticData.HappyStatus = 0;
        if (staticData.PopularityStatus < 0) staticData.PopularityStatus = 0;

        if (staticData.MoneyStatus > 4) staticData.MoneyStatus = 4;
        if (staticData.HappyStatus > 4) staticData.HappyStatus = 4;
        if (staticData.PopularityStatus > 4) staticData.PopularityStatus = 4;

        getTypeValue();

        switch (typeValue)
        {
            case 0:
                sr.sprite = frownFace;
                break;
            case 1:
                sr.sprite = sadFace;
                break;
            case 2:
                sr.sprite = neutralFace;
                break;
            case 3:
                sr.sprite = happyFace;
                break;
            case 4:
                sr.sprite = ecstaticFace;
                break;
            default:
                sr.sprite = neutralFace;
                break;
        }

        if (oldTypeValue != typeValue)
        {   
            if (oldTypeValue > typeValue)
                StartCoroutine(Flash(1f, minusSign.GetComponent<SpriteRenderer>()));
            
            if (oldTypeValue < typeValue)
                StartCoroutine(Flash(1f, plusSign.GetComponent<SpriteRenderer>()));
        }
        oldTypeValue = typeValue;
    }

    IEnumerator Flash(float x, SpriteRenderer srObj)
    {
        srObj.enabled = true;
        yield return new WaitForSeconds(x);
        srObj.enabled = false;
    }
}
