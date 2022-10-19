using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScenesButton : MonoBehaviour
{
    public void LoadScene(int sceneName){
        SceneManager.LoadScene(sceneName);
    }
}
