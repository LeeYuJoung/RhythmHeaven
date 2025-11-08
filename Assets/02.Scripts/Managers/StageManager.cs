using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.UIElements;

public class StageManager : MonoBehaviour
{
    private static StageManager instance;
    public static StageManager Instance { get { return instance; } }

    public Sprite[] bgs;
    private SpriteRenderer bgRenderer;
    private SpriteRenderer blinkObject;

    public float flashDuration = 0.5f;    // 깜빡이는 속도
    public Color flashColor = Color.black; // 깜빡이는 색상

    private float currentTime = 0.0f;
    private float blinkTime = 2.5f;

    private float currentStageTime = 0.0f;
    private float stageTime = 60.0f;

    public float productMoveSpeed = 2.0f;

    public int currentStage = 0;    // 현재 스테이지
    public bool isStageOver = false;    // 스테이지 종료 상태인가

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        bgRenderer = GameObject.Find("Background").GetComponent<SpriteRenderer>();
        blinkObject = GameObject.Find("Blink").GetComponent<SpriteRenderer>();
        // 초기 상태 투명
        blinkObject.color = new Color(flashColor.r, flashColor.g, flashColor.b, 0f);
        OnNextStage();
        UIManager.Instance.OnGuidBookActive();
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameOver || isStageOver)
            return;

        if(currentStageTime >= stageTime)
        {
            // 스테이지 종료
            isStageOver = true;
            currentStageTime = 0.0f;

            // 남아 있는 손님 및 제품 제거
            if(CustomerManager.Instance.currentCustomer != null)
            {
                CustomerManager.Instance.currentCustomer.isSuccess = true;
                CustomerManager.Instance.currentCustomer.OnLeave();
            }

            GameObject[] _products = GameObject.FindGameObjectsWithTag("Product");
            for (int i = 0; i < _products.Length; i++)
                Destroy(_products[i]);

            OnNextStage();
            UIManager.Instance.OnGuidBookActive();
            AudioManager.Instance.BGMPlay(Utils.EnumTypes.BGMType.Rule);
        }
        else
        {
            currentStageTime += Time.deltaTime;
        }


        if(currentTime >= blinkTime && currentStage == 3)
        {
            currentTime = 0.0f;
            StartCoroutine(Blink());
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }

    // 스테이지별 이벤트 관리
    public void OnStageEvent()
    {
        switch(currentStage)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
        }
    }

    // 정전 이벤트
    private IEnumerator Blink()
    {
        blinkObject.color = new Color(255.0f, 255.0f, 255.0f, 255.0f);
        yield return new WaitForSeconds(0.5f);
        blinkObject.color = new Color(255.0f, 255.0f, 255.0f, 0.0f);
    }

    public void OnNextStage()
    {
        // 다음 스테이지로 이동
        if (currentStage >= 3)
        {
            GameManager.Instance.OnHappyGameOver();
            return;
        }

        currentStage++;
        bgRenderer.sprite = bgs[currentStage - 1];
        RhythmManager.Instance.currentBeatInMeasure++;
        CustomerManager.Instance.greetWaitTime = (currentStage == 1) ? 10.0f : 3.0f;
        UIManager.Instance.MoveGuidBook();
        productMoveSpeed += 1.0f;
    }
}
