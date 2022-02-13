using UnityEngine;
using UnityEngine.UI;

public class BlackJack_UI : MonoBehaviour
{
    [Header("Menu References")]
    [SerializeField] private BlackJack_BettingMenu bettingMenu;
    [SerializeField] private BlackJack_DealingMenu dealingMenu;
    [SerializeField] private BlackJack_FinishingMenu finishingMenu;


    [Header("Text References")]
    [SerializeField] private Text betText;
    [SerializeField] private Text chipsText;


    //* private vars
    public BlackJack_Master master { get; private set; }


    public void Initialize(BlackJack_Master masterRef) 
    {
        master = masterRef;

        // prepare menus
        bettingMenu.Initialize(this);
        dealingMenu.Initialize(this);
        finishingMenu.Initialize(this);

        // prepare other UI elements
        refreshChipsText();
    }


#region Menu Management
    public void disableAllMenus() {
        bettingMenu.hide();
        dealingMenu.hide();
        finishingMenu.hide();
    }

    public void enableMenuByGameState(BlackJack_GameState gameState) {
        switch (gameState) {
            case BlackJack_GameState.BETTING: bettingMenu.show(); return;
            case BlackJack_GameState.DEALING: dealingMenu.show(); return;
            case BlackJack_GameState.PLAYING: dealingMenu.showPlayingMenu(); return;
            case BlackJack_GameState.FINISHING: finishingMenu.show(); return;
        }
    }

    public void refreshDealingMenu() => dealingMenu.refreshDealingPlayerSumText();

    public void setFinishingMenuTexts(BlackJack_MatchResult mr) => finishingMenu.setFinishTexts(mr);
#endregion
#region Texts
    public void setBetText(string s) => betText.text = s;
    private void refreshChipsText() => chipsText.text = "Chips: " + PlayerPrefs.GetInt("blackJack_chips");
#endregion
}
