using System.Collections.Generic;
using UnityEngine;

public class BlackJack_DealerDeck : MonoBehaviour
{
    public List<BlackJack_Card> cards { get; private set; }


    //* private vars
    private int shufflePower;


    public void initialize()
    {
        resetDeck();
        shufflePower = cards.Count * BlackJack_System.shuffleAmplifier;
        shuffleDeck();
    }


    // picks card at index 0, puts it at the end of the deck and returns it
    public BlackJack_Card pickFirst()
    {
        BlackJack_Card card = cards[0];
        cards.RemoveAt(0);
        cards.Add(card);

        return card;
    }


    private void resetDeck() => cards = BlackJack_Deck.cards;

    private void shuffleDeck()
    {
        for (int i = 0; i < shufflePower; i++)
        {
            int rng = Random.Range(0, cards.Count);
            BlackJack_Card card = cards[rng];
            cards.RemoveAt(rng);

            rng = Random.Range(0, cards.Count);
            cards.Insert(rng, card);
        }
    }
}
