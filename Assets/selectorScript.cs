using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectorScript : MonoBehaviour
{
    public Sprite EmptySprite;
    public Sprite WorkSprite;
    public Sprite FamilySprite;
    public Sprite FunSprite;
    public int currentSpriteIndex;
    public int fieldNumber;
    private SpriteRenderer sr;
    
    private void Start()
    {
        currentSpriteIndex = 0;
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    private void updateStatic()
    {
        // not optimal, I know :P
        switch(fieldNumber)
        {
            case 1:
                staticData.Field1 = currentSpriteIndex;
                break;
            case 2:
                staticData.Field2 = currentSpriteIndex;
                break;
            case 3:
                staticData.Field3 = currentSpriteIndex;
                break;
            case 4:
                staticData.Field4 = currentSpriteIndex;
                break;
            case 5:
                staticData.Field5 = currentSpriteIndex;
                break;
            case 6:
                staticData.Field6 = currentSpriteIndex;
                break;
            case 7:
                staticData.Field7 = currentSpriteIndex;
                break;
            case 8:
                staticData.Field8 = currentSpriteIndex;
                break;
            case 9:
                staticData.Field9 = currentSpriteIndex;
                break;
            default: break;
        }
    }

    // Start is called before the first frame update
    private void OnMouseDown()
    {
        GetComponent<AudioSource>().Play();
        currentSpriteIndex++;
        if (currentSpriteIndex > 3)
            currentSpriteIndex = 0;

        updateStatic();

        switch(currentSpriteIndex)
        {
            case 1:
                sr.sprite = FunSprite;
                break;
            case 2:
                sr.sprite = FamilySprite;
                break;
            case 3:
                sr.sprite = WorkSprite;
                break;
            default:
                sr.sprite = EmptySprite;
                break;
        }
        
    }
    private void OnMouseUp()
    {
        GetComponent<AudioSource>().Stop();
    }

}
