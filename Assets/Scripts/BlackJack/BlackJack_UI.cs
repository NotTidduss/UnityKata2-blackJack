using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BlackJack_UI : MonoBehaviour
{
    [Header("Scene References")]
    [SerializeField] private BlackJack_Master master;
    [SerializeField] private BlackJack_Dealer dealer;

    [Header("Menu References")]
    [SerializeField] private GameObject bettingMenu;
    [SerializeField] private GameObject dealingMenu;
    [SerializeField] private GameObject playingMenu;
    [SerializeField] private GameObject finishingMenu;

    [Header("Text References")]
    [SerializeField] private Text dealingPlayerSumText;
    [SerializeField] private Text chipsText;
    [SerializeField] private Text betText;
    [SerializeField] private Text enemySumText;
    [SerializeField] private Text playerSumText;
    [SerializeField] private Text resultText;

    [Header("Custom UI Object References")]
    [SerializeField] private GameObject playerCardArea1;
    [SerializeField] private GameObject playerCardArea2;
    [SerializeField] private GameObject playerCardArea3;
    [SerializeField] private GameObject playerCardArea4;
    [SerializeField] private GameObject playerCardArea5;
    [SerializeField] private GameObject playerCardArea6;
    [SerializeField] private GameObject playerCardArea7;
    [SerializeField] private GameObject playerCardArea8;
    [SerializeField] private GameObject playerCardArea9;
    [SerializeField] private GameObject opponentCardArea1;
    [SerializeField] private GameObject opponentCardArea2;
    private int currentCardIndexAfterDealing = 3;


    public void initialize() => updateChipsText();

    #region Menus
    public void disableAllMenus() {
        bettingMenu.SetActive(false);
        dealingMenu.SetActive(false);
        playingMenu.SetActive(false);
        finishingMenu.SetActive(false);
    }

    public void enableMenuByGameState(BlackJack_GameState gameState) {
        switch (gameState) {
            case BlackJack_GameState.BETTING: bettingMenu.SetActive(true); return;
            case BlackJack_GameState.DEALING: dealingMenu.SetActive(true); return;
            case BlackJack_GameState.PLAYING: playingMenu.SetActive(true); return;
            case BlackJack_GameState.FINISHING: finishingMenu.SetActive(true); return;
        }
    }
    #endregion

    #region Texts
    private void setText(Text t, string s) => t.text = s;
    
    /*
        Sets all the texts displayed in the finish based on the given [mr].
        @matchResult the given result of the blackjack match </param>
    */
    public void setFinishTexts(BlackJack_MatchResult matchResult) {
        updateEnemySumText();
        updatePlayerSumText();

        switch (matchResult) {
            case BlackJack_MatchResult.BUST: setText(resultText, "BUST"); return;
            case BlackJack_MatchResult.LOSE: setText(resultText, "YOU LOSE"); return;
            case BlackJack_MatchResult.NATURAL: setText(resultText, "NATURAL"); return;
            case BlackJack_MatchResult.WIN: setText(resultText, "YOU WIN"); return;
            case BlackJack_MatchResult.DRAW: setText(resultText, "DRAW"); return;
        }
    }
    public void updateDealingPlayerSumText() => setText(dealingPlayerSumText, "Your Sum: " + PlayerPrefs.GetInt("blackJack_sum_player"));
    private void updateEnemySumText() => setText(enemySumText, "Enemy Sum: " + PlayerPrefs.GetInt("blackJack_sum_enemy"));
    private void updatePlayerSumText() => setText(playerSumText, "Your Sum: " + PlayerPrefs.GetInt("blackJack_sum_player"));
    private void updateChipsText() => setText(chipsText, "Chips: " + PlayerPrefs.GetInt("blackJack_chips"));
    #endregion

    #region Buttons
    /*
        If current amount of chips is greater than [bet], set bet and progress the game.
        @param bet: the given bet </param>
    */
    public void setBet(int bet) {
        if (PlayerPrefs.GetInt("blackJack_chips") >= bet) {
            PlayerPrefs.SetInt("blackJack_bet", bet);
            betText.text = "Current Bet: " + PlayerPrefs.GetInt("blackJack_bet");
            master.changeGameState(BlackJack_GameState.DEALING);
        }
    } 
    public void setBetAllIn() => setBet(PlayerPrefs.GetInt("blackJack_chips"));
    public void playAgain() => SceneManager.LoadScene("1_BlackJack");
    
    /*
        Tell the dealer to add another card to the player's pile.
    */
    public void hit() {
        GameObject playerCardArea = getPlayerCardAreaById(currentCardIndexAfterDealing);
        currentCardIndexAfterDealing++;
        dealer.hitCard(playerCardArea);
        if (PlayerPrefs.GetInt("blackJack_sum_player") > 20) stand();
    }

    private GameObject getPlayerCardAreaById(int index) {
        switch (currentCardIndexAfterDealing) {
            case 4: return playerCardArea4;
            case 5: return playerCardArea5;
            case 6: return playerCardArea6;
            case 7: return playerCardArea7;
            case 8: return playerCardArea8;
            case 9: return playerCardArea9;
            default:
                currentCardIndexAfterDealing = 3;
                return playerCardArea3;
        }
    }

    public void stand() => master.changeGameState(BlackJack_GameState.FINISHING);
    #endregion
}
