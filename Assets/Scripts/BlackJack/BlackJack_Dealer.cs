using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackJack_Dealer : MonoBehaviour
{
    [Header("Scene References")]
    [SerializeField] private BlackJack_Master master;
    [SerializeField] private BlackJack_UI ui;
    [SerializeField] private GameObject playerCard1Area;
    [SerializeField] private GameObject playerCard2Area;
    [SerializeField] private GameObject enemyCard1Area;
    [SerializeField] private GameObject enemyCard2Area;

    [Header("Prefab References")]
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject cardOverlayPrefab;

    private List<string> deck = new List<string>();
    private List<string> helperDeck = new List<string>();
    private int actualCardValue;

    public void deal() => StartCoroutine("Deal");

    private IEnumerator Deal() {
        for (int i = 0; i < 1; i++) {
            deck = resetDeck();
            helperDeck.Clear();

            deck = shuffleDeck(deck, helperDeck);
            new WaitForSeconds(1);

            dealCard(playerCard1Area, enemyCard1Area, true);
            new WaitForSeconds(1);

            dealCard(playerCard2Area, enemyCard2Area, false);
            new WaitForSeconds(1);

            if (PlayerPrefs.GetInt("blackJack_sum_player") == 21) master.changeGameState(BlackJack_GameState.FINISHING);
            else master.changeGameState(BlackJack_GameState.PLAYING);

            yield return null;
        }
    }

    private List<string> resetDeck() {
        return new List<string>() {
            "Clubs Ace",
            "Clubs 2",
            "Clubs 3",
            "Clubs 4",
            "Clubs 5",
            "Clubs 6",
            "Clubs 7",
            "Clubs 8",
            "Clubs 9",
            "Clubs 10",
            "Clubs Jack",
            "Clubs Queen",
            "Clubs King",
            "Diamonds Ace",
            "Diamonds 2",
            "Diamonds 3",
            "Diamonds 4",
            "Diamonds 5",
            "Diamonds 6",
            "Diamonds 7",
            "Diamonds 8",
            "Diamonds 9",
            "Diamonds 10",
            "Diamonds Jack",
            "Diamonds Queen",
            "Diamonds King",
            "Hearts Ace",
            "Hearts 2",
            "Hearts 3",
            "Hearts 4",
            "Hearts 5",
            "Hearts 6",
            "Hearts 7",
            "Hearts 8",
            "Hearts 9",
            "Hearts 10",
            "Hearts Jack",
            "Hearts Queen",
            "Hearts King",
            "Spades Ace",
            "Spades 2",
            "Spades 3",
            "Spades 4",
            "Spades 5",
            "Spades 6",
            "Spades 7",
            "Spades 8",
            "Spades 9",
            "Spades 10",
            "Spades Jack",
            "Spades Queen",
            "Spades King",
        };
    }

    private List<string> shuffleDeck(List<string> freshDeck, List<string> shuffledDeck) {
        if (freshDeck.Count == 0) return shuffledDeck;

        int randomIndex = Random.Range(0, freshDeck.Count);
        shuffledDeck.Add(freshDeck[randomIndex]);
        freshDeck.RemoveAt(randomIndex);

        return shuffleDeck(freshDeck, shuffledDeck);
    }
    
    private void dealCard(GameObject playerCardArea, GameObject enemyCardArea, bool hideCard) {
        hitCard(playerCardArea);

        new WaitForSeconds(1);

        string enemyCardValue = pickFirstCardFromDeck(deck);
        int actualEnemyCardValue = mapCardValue(enemyCardValue);
        PlayerPrefs.SetInt("blackJack_sum_enemy", PlayerPrefs.GetInt("blackJack_sum_enemy") + actualEnemyCardValue);
        dealCard(enemyCardValue, enemyCardArea);

        if (hideCard) Instantiate(cardOverlayPrefab, enemyCardArea.transform);
    }

    private string pickFirstCardFromDeck(List<string> deck) {
        string cardValue = deck[0];
        deck.RemoveAt(0);

        return cardValue;
    }

    private void dealCard(string value, GameObject area) {
        GameObject card = cardPrefab;
        card.GetComponent<BlackJack_Card>().updateCardValue(value);
        Instantiate(card, area.transform);
    }

    private int mapCardValue(string value) {
        string splitValue = value.Split(' ')[1];
        switch (splitValue) {
            case "Ace":
                return 11;
            case "10": case "Jack": case "Queen": case "King":
                return 10;
            default:
                return int.Parse(splitValue);
        }
    }

    public void hitCard(GameObject playerCardArea) {
        string playerCardValue = pickFirstCardFromDeck(deck);
        int actualPlayerCardValue = mapCardValue(playerCardValue);
        PlayerPrefs.SetInt("blackJack_sum_player", PlayerPrefs.GetInt("blackJack_sum_player") + actualPlayerCardValue);
        ui.updateDealingPlayerSumText();
        dealCard(playerCardValue, playerCardArea);
    }
}
