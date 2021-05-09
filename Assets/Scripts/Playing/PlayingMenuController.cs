using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingMenuController : MonoBehaviour
{
    public DealingMenuController dmc;
    public GameObject playerCardArea3;
    public GameObject playerCardArea4;
    public GameObject playerCardArea5;
    public GameObject playerCardArea6;
    public GameObject playerCardArea7;
    public GameObject playerCardArea8;
    public GameObject playerCardArea9;
    private int currentCardIndex = 3;

    public void hit() {
        GameObject playerCardArea = getPlayerCardArea(currentCardIndex);
        currentCardIndex++;
        dmc.hitCard(playerCardArea);
        if (PlayerPrefs.GetInt("Kata2_sum") > 20) stand();
    }

    private GameObject getPlayerCardArea(int index) {
        switch (currentCardIndex) {
            case 4:
                return playerCardArea4;
            case 5:
                return playerCardArea5;
            case 6:
                return playerCardArea6;
            case 7:
                return playerCardArea7;
            case 8:
                return playerCardArea8;
            case 9:
                return playerCardArea9;
            default:
                currentCardIndex = 3;
                return playerCardArea3;
        }
    }

    public void stand() {
        GameObject.Find("Master").GetComponent<MasterController>().changeGameState(GameState.FINISHING);
    }
}
