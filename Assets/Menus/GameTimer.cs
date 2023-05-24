using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private float _time = 30.0f;

    // Update is called once per frame
    void Update()
    {
        if (_time > 0)
        {
            _time -= Time.deltaTime;
        }
        else
        {
            LoseGame("LostScene"); 
        }
    }

    private void LoseGame(string sceneName)
    {
        ScenesManager.LoadSceneByName(sceneName);
    }

}
