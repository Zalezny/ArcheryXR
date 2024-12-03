using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject game;


    private void Start()
    {
        menuUI.SetActive(true);

        game.SetActive(false);
    }

    public void onStart()
    {
        menuUI.SetActive(false);
        game.SetActive(true);
    }


    public void onEnd()
    {
        Application.Quit();
    }
}
