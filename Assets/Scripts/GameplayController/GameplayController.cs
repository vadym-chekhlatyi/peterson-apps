using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{
    public static GameplayController Instance;

    [SerializeField] private GameObject gameplayUI;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject levelText;

    public int level;
    public int scoresToLevelUp;

    private void Start()
    {
        Instance = this;
    }

    public void Play()
    {
        CreateNewTextures(Color.gray);

        CircleFactory.Instance.started = true;

        ShowGameplayUI(true);
    }

    public void StopGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LevelUp()
    {
        CreateNewTextures(Color.blue);

        level++;

        scoresToLevelUp *= 2;

        CircleFactory.Instance.speed *= 1.3f;
        CircleFactory.Instance.maxCircleCount *= 2;

        levelText.SetActive(true);
        levelText.GetComponent<Text>().text = "Level " + level + "!";

        Time.timeScale = 0.1f;
        StartCoroutine(HideLevelPanel());

        
    }

    public void CreateNewTextures(Color color)
    {
        CircleFactory.Instance.textureList.Clear();
        for(int i = 32; i <= 256; i *= 2)
        {
            Texture2D tex = new Texture2D(i, i); 
            float rSquared = i * i;

            for (int u = 0; u < tex.width; u++)
            {
                for (int v = 0; v < tex.height; v++)
                {
                    if ((i - u) * (i - u) + (i - v) * (i - v) < rSquared) tex.SetPixel(u, v, color);
                }
            }
            CircleFactory.Instance.textureList.Add(tex);
        }
    }

    private IEnumerator HideLevelPanel()
    {
        yield return new WaitForSeconds(.2f);

        Time.timeScale = 1f;
        levelText.SetActive(false);
    }

    private void ShowGameplayUI(bool value)
    {
        gameplayUI.SetActive(value);
        menuUI.SetActive(!value);
    }
}
