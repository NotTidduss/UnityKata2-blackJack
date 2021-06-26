using UnityEngine;

public class BlackJack_Master : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BlackJack_UI ui;

    private int chips;
    private BlackJack_GameState currentState;
    private BlackJack_MatchResult matchResult;

    public DealingMenuController dealingMenuController;

    void Start() {
        if (PlayerPrefs.GetInt("blackJack_chips") == 0) PlayerPrefs.SetInt("blackJack_chips", 100);
        ui.initialize();
        changeGameState(BlackJack_GameState.BETTING);
    }

    public void changeGameState(BlackJack_GameState newState) {
        currentState = newState;

        switch (currentState) {
            case BlackJack_GameState.BETTING:
                ui.disableAllMenus();
                ui.enableMenuById(0);
                break;
            case BlackJack_GameState.DEALING:
                ui.disableAllMenus();
                ui.enableMenuById(1);
                dealingMenuController.deal();
                break;
            case BlackJack_GameState.PLAYING:
                ui.enableMenuById(2);
                break;
            case BlackJack_GameState.FINISHING:
                ui.disableAllMenus();
                ui.enableMenuById(3);
                matchResult = getMatchResult(PlayerPrefs.GetInt("blackJack_sum_enemy"), PlayerPrefs.GetInt("blackJack_sum_player"));
                updateChips(matchResult, PlayerPrefs.GetInt("blackJack_chips"), PlayerPrefs.GetInt("blackJack_bet"));
                ui.updateFinishTexts(matchResult);
                resetMatchStats();
                break;
        }
    } 

    /// <summary>
    /// Determines the result of of the current match based on the card sums of the player and their opponent.
    /// </summary>
    /// <param name="enemySum"> the given points sum of the opponent's cards </param>
    /// <param name="playerSum"> the given points sum of the player's cards </param>
    private BlackJack_MatchResult getMatchResult(int enemySum, int playerSum) {
        if (playerSum > 21) return BlackJack_MatchResult.BUST;
        else if (playerSum == 21) return BlackJack_MatchResult.NATURAL;
        else if (enemySum > playerSum) return BlackJack_MatchResult.LOSE;
        else if (enemySum == playerSum) return BlackJack_MatchResult.DRAW;
        else return BlackJack_MatchResult.WIN;
    }

    /// <summary>
    /// Updates the player's amount of chips based on the the match result.
    /// </summary>
    /// <param name="mr"> the given result of the blackjack match </param>
    /// <param name="chips"> the given amount of chips </param>
    /// <param name="bet"> the bet that was set at the beginning of the match </param>
    private void updateChips(BlackJack_MatchResult mr, int chips, int bet) {
        switch (mr) {
            case BlackJack_MatchResult.BUST: case BlackJack_MatchResult.LOSE:
                PlayerPrefs.SetInt("blackJack_chips", chips - bet);
                return;
            case BlackJack_MatchResult.NATURAL: case BlackJack_MatchResult.WIN:
                PlayerPrefs.SetInt("blackJack_chips", chips + bet);
                return;
        }
    }

    private void resetMatchStats() {
        PlayerPrefs.SetInt("blackJack_sum_enemy", 0);
        PlayerPrefs.SetInt("blackJack_sum_player", 0);
        PlayerPrefs.SetInt("blackJack_bet", 0);
    }
}