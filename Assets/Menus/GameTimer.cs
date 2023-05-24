using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private float _time = 30.0f;
    [SerializeField] private Image uiFill;
    [SerializeField] private TMP_Text uiText;


    private float duration;

    void Start()
    {
        duration = _time;
        StartCoroutine(UpdateTimer());
    }
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

    private IEnumerator UpdateTimer(){
        while(_time >= 0){
            uiText.text = $"{_time / 60:00} : {_time % 60:00}";
            uiFill.fillAmount = Mathf.InverseLerp(0, duration, _time);
            yield return new WaitForSeconds(1f);
        }
    }

}
