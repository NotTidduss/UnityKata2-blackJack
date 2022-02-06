using UnityEngine;

public class BlackJack_PlayerPrefsMaster : MonoBehaviour
{
    public static void initializePlayerPrefs() {
        // INT blackJack_chips: the current amount of chips available to the player
        if (PlayerPrefs.GetInt("blackJack_chips") == 0)
            PlayerPrefs.SetInt("blackJack_chips", 100);
        
        // INT blackJack_bet: the currently set bet. Default 0
        PlayerPrefs.SetInt("blackJack_bet", 0);

        // INT blackJack_playerSum: the current sum of the player's card's values
        PlayerPrefs.SetInt("blackJack_playerSum", 0);

        // INT blackJack_enemySum: a hidden value that is shown when finishing
        PlayerPrefs.SetInt("blackJack_enemySum", 0);
    }

    public static void resetAllPlayerPrefs() {
        PlayerPrefs.DeleteAll();
        initializePlayerPrefs();
    }
}
