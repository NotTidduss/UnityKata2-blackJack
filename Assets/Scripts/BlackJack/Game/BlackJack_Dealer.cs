using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackJack_Dealer : MonoBehaviour
{
    [Header("Scene References")]
    [SerializeField] private BlackJack_DealerDeck dealerDeck;
    [SerializeField] private GameObject playerCard1Area;
    [SerializeField] private GameObject playerCard2Area;
    [SerializeField] private GameObject enemyCard1Area;
    [SerializeField] private GameObject enemyCard2Area;


    [Header("Prefab References")]
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject cardOverlayPrefab;


    //* private vars
    private BlackJack_Master master;
    private int actualCardValue;


    public void initialize(BlackJack_Master masterRef)
    {
        master = masterRef;

        // prepare and shuffle deck 
        dealerDeck.initialize();
    }


    // Deal individual cards one at a time, and then check for a Natural.
    IEnumerator Deal()
    {
        for (int i = 0; i < 3; i++)
        {
            switch (i)
            {
                case 0: dealCard(playerCard1Area, enemyCard1Area, true); break;
                case 1: dealCard(playerCard2Area, enemyCard2Area, false); break;
                case 2: checkForNatural(); break;
            }

            yield return new WaitForSeconds(1);
        }
    }

    // Pick first card from deck, calcalute new Player score, and deal the card
    IEnumerator DealCardForPlayer(GameObject playerCardArea)
    {
        string playerCardValue = dealerDeck.pickFirst();
        int actualPlayerCardValue = mapCardValue(playerCardValue);
        PlayerPrefs.SetInt("blackJack_playerSum", PlayerPrefs.GetInt("blackJack_playerSum") + actualPlayerCardValue);

        yield return new WaitForSeconds(1f);

        master.ui.updateDealingPlayerSumText();
        placeCard(playerCardValue, playerCardArea);
    }

    // Pick first card from deck, calcalute new Enemy score, and deal the card. Use overlay for first card.
    IEnumerator DealCardForEnemy(GameObject enemyCardArea, bool useOverlay)
    {
        string enemyCardValue = dealerDeck.pickFirst();
        int actualEnemyCardValue = mapCardValue(enemyCardValue);
        PlayerPrefs.SetInt("blackJack_enemySum", PlayerPrefs.GetInt("blackJack_enemySum") + actualEnemyCardValue);

        yield return new WaitForSeconds(1f);

        placeCard(enemyCardValue, enemyCardArea);
        if (useOverlay) Instantiate(cardOverlayPrefab, enemyCardArea.transform);
    }


    public void deal() => StartCoroutine(Deal());
    public void hit(GameObject playerCardArea) => StartCoroutine(DealCardForPlayer(playerCardArea));

    private void dealCard(GameObject playerCardArea, GameObject enemyCardArea, bool useOverlay)
    {
        StartCoroutine(DealCardForPlayer(playerCardArea));
        StartCoroutine(DealCardForEnemy(enemyCardArea, useOverlay));
    }

    private void placeCard(string value, GameObject area)
    {
        GameObject card = cardPrefab;
        card.GetComponent<BlackJack_Prefab_Card>().setCardValue(value);
        Instantiate(card, area.transform);
    }

    private int mapCardValue(string value)
    {
        string splitValue = value.Split(' ')[1];
        switch (splitValue)
        {
            case "Ace":
                return 11;
            case "10":
            case "Jack":
            case "Queen":
            case "King":
                return 10;
            default:
                return int.Parse(splitValue);
        }
    }

    private void checkForNatural()
    {
        if (PlayerPrefs.GetInt("blackJack_playerSum") == 21) master.changeGameState(BlackJack_GameState.FINISHING);
        else master.changeGameState(BlackJack_GameState.PLAYING);
    }
}
