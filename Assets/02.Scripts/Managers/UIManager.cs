using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance {  get { return instance; } }

    private Canvas canvas;
    private GameObject gameOverPhanel;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Init();
        instance = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            // 행동 지침서 활성화 및 비활성화
        }
    }

    private void Init()
    {
        canvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
        gameOverPhanel = canvas.transform.GetChild(1).gameObject;
    }

    public void GameOver()
    {
        gameOverPhanel.SetActive(true);
    }
}
