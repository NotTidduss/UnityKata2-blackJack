using UnityEngine;
using UnityEngine.UI;

public class BettingMenuButtonController : MonoBehaviour
{
    public Text betText;

    public void setBet(int bet){
        int currentChips = PlayerPrefs.GetInt("Kata2_chips");
        if (currentChips >= bet) {
            PlayerPrefs.SetInt("Kata2_bet", bet);
            betText.text = "Current Bet: " + PlayerPrefs.GetInt("Kata2_bet");

            GameObject.Find("Master").GetComponent<MasterController>().changeGameState(GameState.DEALING);
        }
    } 

    public void allIn() => setBet(PlayerPrefs.GetInt("Kata2_chips"));
}
