using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScenesButtons : MonoBehaviour
{
    void Start(){

    }
    public void LoadScene(int sceneName){
        SceneManager.LoadScene(sceneName);
    }
}
