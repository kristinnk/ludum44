using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class handleEndMessage : MonoBehaviour
{
    public GameObject deathText;

    // Start is called before the first frame update
    void Start()
    {
        string finalText = "";

        string moneyText = "";
        int finalMoneyScore = staticData.MoneyStatus;

        switch (finalMoneyScore)
        {
            case 0:
                moneyText = "You died a broke man. Your family had to pitch in to afford your burial. ";
                break;
            case 1:
                moneyText = "You died a man of little means. You could just afford your own funeral. ";
                break;
            case 2:
                moneyText = "You died a man of average means. Your family inherits some keepsakes after you are gone. ";
                break;
            case 3:
                moneyText = "Your family could afford a pretty good burial plot for you with a view of a nearby lake. ";
                break;
            case 4:
                moneyText = "You died a wealthy man. Your headstone is of the finest granite and studded with precious stones. ";
                break;
        }

        string happyText = "";
        int finalHappinessScore = staticData.HappyStatus;

        switch (finalHappinessScore)
        {
            case 0:
                happyText = "Life beat you down, and you took it with a frown. ";
                break;
            case 1:
                happyText = "Life regularly disappointed you, but you could have had it worse. ";
                break;
            case 2:
                happyText = "Life treated you neither bad nor well. You were pretty satisfied with your lot in life. ";
                break;
            case 3:
                happyText = "You had many a happy time and good memories of your life. ";
                break;
            case 4:
                happyText = "If you had any complaints you never showed it. Life treated you well. ";
                break;
        }

        string popText = "";
        int finalPopularityStatus = staticData.PopularityStatus;

        switch (finalPopularityStatus)
        {
            case 0:
                popText = "You were a miserable bastard to others, who left you well enough alone. ";
                break;
            case 1:
                popText = "You were not the most likeable person, but deep within lived a good soul. ";
                break;
            case 2:
                popText = "Nobody had any complaints about you, you were liked well enough. ";
                break;
            case 3:
                popText = "You were well liked and had good friends. People mourned you after you left. ";
                break;
            case 4:
                popText = "You had friends in every town. Your death was newsworthy in the local paper. ";
                break;
        }

        finalText = moneyText + happyText + popText;

        TextMeshProUGUI endTextObj = deathText.GetComponent<TextMeshProUGUI>();
        endTextObj.text = finalText;
    }
}
