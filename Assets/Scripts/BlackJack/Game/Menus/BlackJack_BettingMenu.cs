using UnityEngine;

public class BlackJack_BettingMenu : MonoBehaviour
{
    //* private vars
    private BlackJack_UI ui;


    public void Initialize(BlackJack_UI uiRef) 
    {
        ui = uiRef;
    }


#region Menu Management
    public void hide() => this.gameObject.SetActive(false);
    public void show() => this.gameObject.SetActive(true);
#endregion
#region Button Functions
    // If current amount of chips is greater than [bet], set bet and progress the game.
    // @param bet: the given bet </param>
    public void OnClick_SetBet(int bet) 
    {
        if (PlayerPrefs.GetInt("blackJack_chips") >= bet) {
            PlayerPrefs.SetInt("blackJack_bet", bet);
            ui.setBetText("Current Bet: " + PlayerPrefs.GetInt("blackJack_bet"));
            ui.master.changeGameState(BlackJack_GameState.DEALING);
        }
    } 
    public void OnClick_SetBetAllIn() => OnClick_SetBet(PlayerPrefs.GetInt("blackJack_chips"));
#endregion
}
