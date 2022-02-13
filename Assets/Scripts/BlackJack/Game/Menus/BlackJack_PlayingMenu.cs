using UnityEngine;

public class BlackJack_PlayingMenu : MonoBehaviour
{
    //* private vars
    private BlackJack_DealingMenu dealingMenu;


    public void Initialize(BlackJack_DealingMenu dealingMenuRef)
    {
        dealingMenu = dealingMenuRef;
    }


#region Scene Management
    public void hide() => this.gameObject.SetActive(false);
    public void show() => this.gameObject.SetActive(true);
#endregion
#region Button Functions
    // Manage card areas in dealing menu and tell the dealer to add another card to the player's pile.
    public void OnClick_Hit() 
    {
        GameObject playerCardArea = dealingMenu.getCurrentPlayerCardArea();
        dealingMenu.incrementCurrentCardIndex();

        dealingMenu.ui.master.dealer.hit(playerCardArea);

        if (PlayerPrefs.GetInt("blackJack_playerSum") > 20) 
            OnClick_Stand();
    }
    public void OnClick_Stand() => dealingMenu.ui.master.changeGameState(BlackJack_GameState.FINISHING);
#endregion
}
