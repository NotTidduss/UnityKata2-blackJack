using UnityEngine;
using UnityEngine.UI;

public class BlackJack_FinishingMenu : MonoBehaviour
{
    [Header ("Text References")]
    [SerializeField] private Text enemySumText;
    [SerializeField] private Text playerSumText;
    [SerializeField] private Text resultText;


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
#region Texts
    private void setText(Text t, string s) => t.text = s;

    // Sets all the texts displayed in the finishing menu based on the given [matchResult].
    // @matchResult - the given result of the blackjack match
    public void setFinishTexts(BlackJack_MatchResult matchResult) {
        refreshEnemySumText();
        refreshPlayerSumText();

        switch (matchResult) {
            case BlackJack_MatchResult.BUST: setText(resultText, "BUST"); return;
            case BlackJack_MatchResult.LOSE: setText(resultText, "YOU LOSE"); return;
            case BlackJack_MatchResult.NATURAL: setText(resultText, "NATURAL"); return;
            case BlackJack_MatchResult.WIN: setText(resultText, "YOU WIN"); return;
            case BlackJack_MatchResult.DRAW: setText(resultText, "DRAW"); return;
        }
    }
    private void refreshEnemySumText() => setText(enemySumText, "Enemy Sum: " + PlayerPrefs.GetInt("blackJack_enemySum"));
    private void refreshPlayerSumText() => setText(playerSumText, "Your Sum: " + PlayerPrefs.GetInt("blackJack_playerSum"));
#endregion
#region Button Functions
    public void OnClick_PlayAgain() => ui.master.sys.loadGameScene();
#endregion
}
