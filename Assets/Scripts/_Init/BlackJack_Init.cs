using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackJack_Init : MonoBehaviour
{
    void Awake() {
        resetPlayerPrefs();
        SceneManager.LoadScene("1_BlackJack");
    }

    private void resetPlayerPrefs() {
        /** 
            permament PlayerPrefs
            INT blackJack_chips: the current amount of chips available to the player
        **/

        // INT blackJack_bet: the currently set bet. Default 0
        PlayerPrefs.SetInt("blackJack_bet", 0);

        // INT blackJack_sum_player: the current sum of the player's card's values
        PlayerPrefs.SetInt("blackJack_sum_player", 0);

        // INT blackJack_sum_enemy: a hidden value that is shown when finishing
        PlayerPrefs.SetInt("blackJack_sum_enemy", 0);
    }
}
