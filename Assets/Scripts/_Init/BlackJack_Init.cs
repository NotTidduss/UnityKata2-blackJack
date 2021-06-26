using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackJack_Init : MonoBehaviour
{
    void Awake() {
        resetPlayerPrefs();
        SceneManager.LoadScene("1_BlackJack");
    }

    /// <summary>
    /// Resets all non-permamant PlayerPrefs to their supposed start value
    /// in order to avoid bugs.
    /// </summary>
    /// <param name="t"> the given UI Text reference </param>
    /// <param name="s"> the given text that should be displayed </param>
    private void resetPlayerPrefs() {
        /** 
            permament PlayerPrefs
            
            blackJack_chips: the current amount of chips available to the player
        **/

        // blackJack_bet: the currently set bet. Default 0
        PlayerPrefs.SetInt("blackJack_bet", 0);

        // blackJack_sum_player: the current sum of the player's card's values
        PlayerPrefs.SetInt("blackJack_sum_player", 0);

        // blackJack_sum_enemy: a hidden value that is shown when finishing
        PlayerPrefs.SetInt("blackJack_sum_enemy", 0);
    }
}
