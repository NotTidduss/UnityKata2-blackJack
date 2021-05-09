using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgainController : MonoBehaviour
{
    public void playAgain() => SceneManager.LoadScene("Main");
}
