using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterController : MonoBehaviour
{
    public GameObject chipsText;
    public GameObject bettingMenu;
    public GameObject dealingMenu;
    public GameObject playingMenu;
    public GameObject finishingMenu;

    private ChipsTextUpdater ctu;
    private int chips;
    private GameState currentState;

    void Start() {
        ctu = chipsText.GetComponent<ChipsTextUpdater>();
        chips = PlayerPrefs.GetInt("Kata2_chips");
        changeGameState(GameState.BETTING);

        ctu.updateChips();
    }

    public void changeGameState(GameState newState) {
        Debug.Log("new state: " + newState);
        currentState = newState;

        switch (currentState) {
            case GameState.BETTING:
                updateMenus(bettingMenu);
                break;
            case GameState.DEALING:
                updateMenus(dealingMenu);
                dealingMenu.GetComponent<DealingMenuController>().deal();
                break;
            case GameState.PLAYING:
                playingMenu.SetActive(true);
                break;
            case GameState.FINISHING:
                updateMenus(finishingMenu);
                finishingMenu.GetComponent<FinshingTextController>().finish();
                break;
        }
    } 

    private void updateMenus(GameObject currentMenu) {
        bettingMenu.SetActive(false);
        dealingMenu.SetActive(false);
        playingMenu.SetActive(false);
        finishingMenu.SetActive(false);

        currentMenu.SetActive(true);
    }
}

public enum GameState {
    BETTING,
    DEALING,
    PLAYING,
    FINISHING
}