using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    void Start()
    {
        GameObject.FindGameObjectWithTag("Score").GetComponent<Text>().text = Score.getBestScore().ToString();
    } 

    public void loadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void realoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void setActiveCanvas(string name)
    {
        foreach (GameObject canvas in GameObject.FindGameObjectsWithTag("MenuCanvas"))
        {
            canvas.GetComponent<Canvas>().enabled = false;
        }
        GameObject.Find(name).GetComponent<Canvas>().enabled = true;
    }
}
