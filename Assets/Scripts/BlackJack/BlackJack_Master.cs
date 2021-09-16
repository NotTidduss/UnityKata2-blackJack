using UnityEngine;

public class BlackJack_Master : MonoBehaviour
{
    [Header("Scene References")]
    [SerializeField] private BlackJack_UI ui;
    [SerializeField] private BlackJack_Dealer dealer;

    private BlackJack_MatchResult matchResult;


    void Start() {
        if (PlayerPrefs.GetInt("blackJack_chips") == 0) PlayerPrefs.SetInt("blackJack_chips", 100);
        ui.initialize();
        changeGameState(BlackJack_GameState.BETTING);
    }

    public void changeGameState(BlackJack_GameState newState) {
        switch (newState) {
            case BlackJack_GameState.BETTING:
                ui.disableAllMenus();
                break;
            case BlackJack_GameState.DEALING:
                ui.disableAllMenus();
                dealer.deal();
                break;
            case BlackJack_GameState.PLAYING:
                break;
            case BlackJack_GameState.FINISHING:
                ui.disableAllMenus();
                matchResult = getMatchResult(PlayerPrefs.GetInt("blackJack_sum_enemy"), PlayerPrefs.GetInt("blackJack_sum_player"));
                updateChips(matchResult, PlayerPrefs.GetInt("blackJack_chips"), PlayerPrefs.GetInt("blackJack_bet"));
                ui.setFinishTexts(matchResult);
                resetMatchPlayerPrefs();
                break;
        }

        ui.enableMenuByGameState(newState);
    } 

    /*
        Determines the result of of the current match based on the card sums of the player and their opponent.

        @param enemySum: the given points sum of the opponent's cards
        @param playerSum: the given points sum of the player's cards
        @return the player's match result
    */
    private BlackJack_MatchResult getMatchResult(int enemySum, int playerSum) {
        if (playerSum > 21) return BlackJack_MatchResult.BUST;
        else if (playerSum == 21) return BlackJack_MatchResult.NATURAL;
        else if (enemySum > playerSum) return BlackJack_MatchResult.LOSE;
        else if (enemySum == playerSum) return BlackJack_MatchResult.DRAW;
        else return BlackJack_MatchResult.WIN;
    }

    /*
        Updates the player's amount of chips based on the the match result.
        
        @matchResult: the given result of the blackjack match
        @chips: the given amount of chips
        @bet: the bet that was set at the beginning of the match
    */
    private void updateChips(BlackJack_MatchResult matchResult, int chips, int bet) {
        switch (matchResult) {
            case BlackJack_MatchResult.BUST: case BlackJack_MatchResult.LOSE: PlayerPrefs.SetInt("blackJack_chips", chips - bet); return;
            case BlackJack_MatchResult.NATURAL: case BlackJack_MatchResult.WIN: PlayerPrefs.SetInt("blackJack_chips", chips + bet); return;
        }
    }

    private void resetMatchPlayerPrefs() {
        PlayerPrefs.SetInt("blackJack_sum_enemy", 0);
        PlayerPrefs.SetInt("blackJack_sum_player", 0);
        PlayerPrefs.SetInt("blackJack_bet", 0);
    }
}