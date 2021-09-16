using UnityEngine;

public class BlackJack_System : MonoBehaviour {}

public enum BlackJack_GameState {
    BETTING,
    DEALING,
    PLAYING,
    FINISHING
}

public enum BlackJack_MatchResult {
    WIN,
    NATURAL,
    LOSE,
    BUST,
    DRAW
}