using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {
GameObject pauseObjects;

// Use this for initialization
void Start()
{
    Time.timeScale = 1;
    pauseObjects = GameObject.FindGameObjectsWithTag("showonpause");
    hidePaused();
}

// Update is called once per frame
void Update()
{

    //uses the p button to pause and unpause the game
    if (Input.GetKeyDown(KeyCode.P))
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            showPaused();
        }
        else if (Time.timeScale == 0)
        {
            Debug.Log("high");
            Time.timeScale = 1;
            hidePaused();
        }
    }
}


//Reloads the Level
public void Reload()
{
    Application.LoadLevel(Application.loadedLevel);
}

//controls the pausing of the scene
public void pauseControl()
{
    if (Time.timeScale == 1)
    {
        Time.timeScale = 0;
        showPaused();
    }
    else if (Time.timeScale == 0)
    {
        Time.timeScale = 1;
        hidePaused();
    }
}

//shows objects with ShowOnPause tag
public void showPaused()
{
    foreach (GameObject g in pauseObjects)
    {
        g.SetActive(true);
    }
}

//hides objects with ShowOnPause tag
public void hidePaused()
{
    foreach (GameObject g in pauseObjects)
    {
        g.SetActive(false);
    }
}

//loads inputted level
public void LoadLevel(string level)
{
    Application.LoadLevel(level);
}
		
}