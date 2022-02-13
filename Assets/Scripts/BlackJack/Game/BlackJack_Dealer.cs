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
    private List<BlackJack_Card> playerCards;
    private List<BlackJack_Card> enemyCards;
    private int actualCardValue;


    public void initialize(BlackJack_Master masterRef)
    {
        master = masterRef;

        // prepare card lists
        playerCards = new List<BlackJack_Card>();
        enemyCards = new List<BlackJack_Card>();

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

    // Pick first card from deck, calcalute new Player score, wait a second, and place the card on the table
    IEnumerator DealCardForPlayer(GameObject playerCardArea)
    {
        BlackJack_Card pickedPlayerCard = dealerDeck.pickFirst();
        playerCards.Add(pickedPlayerCard);
        PlayerPrefs.SetInt("blackJack_playerSum", calculateCardSum(playerCards));

        yield return new WaitForSeconds(1f);

        master.ui.updateDealingPlayerSumText();
        placeCard(pickedPlayerCard.cardName, playerCardArea);
    }

    // Pick first card from deck, calcalute new Enemy score, and deal the card. Use overlay for first card.
    IEnumerator DealCardForEnemy(GameObject enemyCardArea, bool useOverlay)
    {
        BlackJack_Card pickedEnemyCard = dealerDeck.pickFirst();
        enemyCards.Add(pickedEnemyCard);
        PlayerPrefs.SetInt("blackJack_enemySum", calculateCardSum(enemyCards));

        yield return new WaitForSeconds(1f);

        placeCard(pickedEnemyCard.cardName, enemyCardArea);
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

    private int calculateCardSum(List<BlackJack_Card> cards) 
    {
        int newSum = 0;
        cards.ForEach( card => 
        {
            // if sum gets too high, the ace value changes.
            if (newSum < 11)
                newSum += mapCardValue(card.cardName, 11);
            else
                newSum += mapCardValue(card.cardName, 1);
        });

        return newSum;
    }

    private int mapCardValue(string cardName, int aceValue)
    {
        string splitValue = cardName.Split(' ')[1];
        switch (splitValue)
        {
            case "Ace":
                return aceValue;
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
