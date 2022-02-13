using UnityEngine;
using UnityEngine.UI;

public class BlackJack_DealingMenu : MonoBehaviour
{
    [Header ("Menu References")]
    [SerializeField] private BlackJack_PlayingMenu playingMenu;


    [Header("Text References")]
    [SerializeField] private Text playerSumText;


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


    //* private vars
    public BlackJack_UI ui {get; private set;}
    private int currentCardIndexAfterDealing;


    public void Initialize(BlackJack_UI uiRef)
    {
        ui = uiRef;

        // prepare privates
        currentCardIndexAfterDealing = 3;
        playingMenu.Initialize(this);
        hidePlayingMenu();
    }


    public GameObject getCurrentPlayerCardArea() => getPlayerCardAreaById(currentCardIndexAfterDealing);
    public void incrementCurrentCardIndex() => currentCardIndexAfterDealing += 1;


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


#region Scene Management
    public void hide() => this.gameObject.SetActive(false);
    public void show() => this.gameObject.SetActive(true);
    public void hidePlayingMenu() => playingMenu.gameObject.SetActive(false);
    public void showPlayingMenu() => playingMenu.gameObject.SetActive(true);
#endregion
#region Texts
    public void refreshDealingPlayerSumText() => playerSumText.text = "Your Sum: " + PlayerPrefs.GetInt("blackJack_playerSum");
#endregion
}
