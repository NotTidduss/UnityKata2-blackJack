using UnityEngine;
using UnityEngine.UI;

public class ChipsTextUpdater : MonoBehaviour
{
    public void updateChips() => GetComponent<Text>().text = "Chips: " + PlayerPrefs.GetInt("Kata2_chips");
}
