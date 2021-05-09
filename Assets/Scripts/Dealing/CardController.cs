using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    public Text cardValue;

    public void updateCardValue(string value) => cardValue.text = value;
}
