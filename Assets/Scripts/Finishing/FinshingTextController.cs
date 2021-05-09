using UnityEngine;
using UnityEngine.UI;

public class FinshingTextController : MonoBehaviour
{
    public Text enemySumText;
    public Text playerSumText;
    public Text resultText;

    public void finish() {
        int enemySum = PlayerPrefs.GetInt("Kata2_sum_enemy");
        int playerSum = PlayerPrefs.GetInt("Kata2_sum");
        int chips = PlayerPrefs.GetInt("Kata2_chips");
        int bet = PlayerPrefs.GetInt("Kata2_bet");

        PlayerPrefs.SetInt("Kata2_sum_enemy", 0);
        PlayerPrefs.SetInt("Kata2_sum", 0);
        PlayerPrefs.SetInt("Kata2_bet", 0);

        enemySumText.text = "Enemy Sum: " + enemySum;
        playerSumText.text = "Your Sum: " + playerSum;

        if (enemySum > playerSum) {
            PlayerPrefs.SetInt("Kata2_chips", chips - bet);
            resultText.text = "YOU LOSE";
        }
        else if (enemySum == playerSum) {
            resultText.text = "DRAW";
        }
        else if (playerSum > 21) {
            PlayerPrefs.SetInt("Kata2_chips", chips - bet);
            resultText.text = "BUST";
        }
        else if (playerSum == 21) {
            PlayerPrefs.SetInt("Kata2_chips", chips + bet);
            resultText.text = "Natural";
        }
        else {
            PlayerPrefs.SetInt("Kata2_chips", chips + bet);
            resultText.text = "YOU WIN";
        }
    }
}
