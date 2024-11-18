using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> targets;
    [SerializeField] private float spawnRate = 1.0f;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    private int _score;
    // Start is called before the first frame update
    void Start()
    {
        gameOverText.gameObject.SetActive(false);
        _score = 0;
        scoreText.text = "Score: " + _score;
        StartCoroutine(SpawnTarget());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            var index = Random.Range(0, targets.Count);
            Instantiate(targets[index],transform.position,targets[index].transform.rotation);
        }
    }

    public void UpdateSocre(int value)
    {
        _score += value;
        scoreText.text = "Score: " + _score;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
    }
}
