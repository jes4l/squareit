using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogLogic : MonoBehaviour
{
    public void LoadRegister()
    {
        SceneManager.LoadScene("RegisterScene");
    }

    public void LoadLogIn()
    {
        SceneManager.LoadScene("LogInScene");
    }

    public void GoToModules()
    {
        SceneManager.LoadScene("ModuleScene");
    }

}

