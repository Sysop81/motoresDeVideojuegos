using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float displayImageDuration = 1f;
    [SerializeField] private GameObject player;
    [SerializeField] private CanvasGroup exitBackgroundImageCanvasGroup;
    [SerializeField] private CanvasGroup caughtBackgroundImageCanvasGroup;
    private bool _isPlayerAtExit;
    private bool _isPlayerCaught;
    private float _timer;

    // Update is called once per frame
    void Update()
    {
        if (_isPlayerAtExit)
        {
            /*_timer += Time.deltaTime;
            exitBackgroundImageCanvasGroup.alpha = _timer / fadeDuration;
            if (_timer > fadeDuration + displayImageDuration) EndLevel();*/
            EndLevel(exitBackgroundImageCanvasGroup,false);
        }
        else if (_isPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup,true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            _isPlayerAtExit = true;
        }
    }

    public void EndLevel(CanvasGroup canvas, bool doRestart)
    {
        _timer += Time.deltaTime;
        canvas.alpha = _timer / fadeDuration;
        if (_timer > fadeDuration + displayImageDuration)
        {
            if(doRestart) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            else Application.Quit();
        }
    }

    public void CaughtPlayer()
    {
        _isPlayerCaught = true;
    }
}
