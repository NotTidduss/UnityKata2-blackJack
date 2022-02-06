using UnityEngine;
using UnityEngine.UI;

public class BlackJack_Prefab_Card : MonoBehaviour
{
    public Text cardValue;

    public void setCardValue(string value) => cardValue.text = value;
}
