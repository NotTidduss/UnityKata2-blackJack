using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BlackJack_UI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BlackJack_Master master;
    [SerializeField] private GameObject bettingMenu;
    [SerializeField] private GameObject dealingMenu;
    [SerializeField] private GameObject playingMenu;
    [SerializeField] private GameObject finishingMenu;
    [SerializeField] private Text chipsText;
    [SerializeField] private Text betText;
    [SerializeField] private Text enemySumText;
    [SerializeField] private Text playerSumText;
    [SerializeField] private Text resultText;

    public void initialize() => updateChipsText();

    #region Menus
    public void disableAllMenus() {
        bettingMenu.SetActive(false);
        dealingMenu.SetActive(false);
        playingMenu.SetActive(false);
        finishingMenu.SetActive(false);
    }

    public void enableMenuById(int id) {
        switch (id) {
            case 0: bettingMenu.SetActive(true); return;
            case 1: dealingMenu.SetActive(true); return;
            case 2: playingMenu.SetActive(true); return;
            case 3: finishingMenu.SetActive(true); return;
        }
    }
    #endregion

    #region Texts
    /// <summary>
    /// Sets given string [s] as the given Text [t]'s text.
    /// </summary>
    /// <param name="t"> the given UI Text reference </param>
    /// <param name="s"> the given text that should be displayed </param>
    private void updateText(Text t, string s) => t.text = s;
    
    /// <summary>
    /// Sets all the texts displayed in the finish based on the given [mr].
    /// </summary>
    /// <param name="mr"> the given result of the blackjack match </param>
    public void updateFinishTexts(BlackJack_MatchResult mr) {
        updateEnemySumText();
        updatePlayerSumText();

        switch (mr) {
            case BlackJack_MatchResult.BUST: updateText(resultText, "BUST"); return;
            case BlackJack_MatchResult.LOSE: updateText(resultText, "YOU LOSE"); return;
            case BlackJack_MatchResult.NATURAL: updateText(resultText, "NATURAL"); return;
            case BlackJack_MatchResult.WIN: updateText(resultText, "YOU WIN"); return;
            case BlackJack_MatchResult.DRAW: updateText(resultText, "DRAW"); return;
        }
    }
    private void updateEnemySumText() => updateText(enemySumText, "Enemy Sum: " + PlayerPrefs.GetInt("blackJack_sum_enemy"));
    private void updatePlayerSumText() => updateText(playerSumText, "Your Sum: " + PlayerPrefs.GetInt("blackJack_sum_player"));
    private void updateChipsText() => updateText(chipsText, "Chips: " + PlayerPrefs.GetInt("blackJack_chips"));
    #endregion

    #region Buttons
    /// <summary>
    /// If current amount of chips is greater than [bet], set bet and progress the game.
    /// </summary>
    /// <param name="bet"> the given bet </param>
    public void setBet(int bet) {
        if (PlayerPrefs.GetInt("Kata2_chips") >= bet) {
            PlayerPrefs.SetInt("Kata2_bet", bet);
            betText.text = "Current Bet: " + PlayerPrefs.GetInt("Kata2_bet");
            master.changeGameState(BlackJack_GameState.DEALING);
        }
    } 
    public void setBetAllIn() => setBet(PlayerPrefs.GetInt("Kata2_chips"));
    public void playAgain() => SceneManager.LoadScene("1_BlackJack");
    #endregion
}
