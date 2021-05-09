using UnityEngine;
using UnityEngine.SceneManagement;

public class InitController : MonoBehaviour
{
    void Awake() {
        resetPlayerPrefs();
        SceneManager.LoadScene("Main");
    }

    private void resetPlayerPrefs() {
        PlayerPrefs.DeleteAll();

        // Kata2_chips: the current amount of chips available to the player. Default 100
        PlayerPrefs.SetInt("Kata2_chips", 100);

        // Kata2_bet: the currently set bet. Default 0
        PlayerPrefs.SetInt("Kata2_bet", 0);

        // Kata2_sum: the current sum of the player's card's values
        PlayerPrefs.SetInt("Kata2_sum", 0);

        // Kata2_sum_enemy: a hidden value that is shown when finishing
        PlayerPrefs.SetInt("Kata2_sum_enemy", 0);
    }
}
