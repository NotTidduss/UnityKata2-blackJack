using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackJack_System : MonoBehaviour 
{
public static int shuffleAmplifier = 2;


#region SceneManagement

    [Header("Scene Names")]
    public string blackJackGameSceneName = "1x_BlackJack_Game";


    public void loadGameScene() => SceneManager.LoadScene(blackJackGameSceneName);

#endregion    
}
