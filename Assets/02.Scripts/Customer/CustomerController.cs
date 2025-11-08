using UnityEngine;
using Utils.EnumTypes;
using System.Collections.Generic;

public class CustomerController : MonoBehaviour
{
    [SerializeField]
    public CustomerType customerType;
    public CustomerState customerState = CustomerState.Idle;

    public Queue<GameObject> products = new Queue<GameObject>();  // 계산할 제품 리스트
    //public int productCount = 4;   // 계산할 제품 갯수

    private Transform productSpawnPos;

    public float currentTime = 0.0f;
    public float greetWaitTime = 10.0f;       // 인사 기다리는 시간

    public Queue<float> spawnTimes = new Queue<float>();
    public float productSpawnTime;           // 제품 생성 시간

    private bool isSuccess = false;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameOver || StageManager.Instance.isStageOver)
            return;

        OnProductStock();
        CheckCustomerState();
    }

    public void Init()
    {
        productSpawnTime = 0.0f;
        productSpawnPos = GameObject.Find("ProductSpawnPos").GetComponent<Transform>();
    }

    public void CheckCustomerState()
    {
        switch (customerState)
        {
            case CustomerState.Greet:
                OnGreetWait();
                break;
            case CustomerState.Calculate:
                OnCalculate();
                break;
            case CustomerState.Leave:
                OnLeave();
                break;
        }
    }

    // 인사 기다리는 중
    public void OnGreetWait()
    {
        if(currentTime < greetWaitTime)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            isSuccess = false;
            customerState = CustomerState.Leave;
        }
    }

    // 계산 중
    public void OnCalculate()
    {
        if (products.Count <= 0)
            return;

        if(currentTime < productSpawnTime)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            currentTime = 0.0f;

            productSpawnTime = spawnTimes.Dequeue();
            ProductController _product = Instantiate(products.Dequeue(), productSpawnPos.position, Quaternion.identity).GetComponent<ProductController>();
            _product.moveSpeed = StageManager.Instance.productMoveSpeed;

            if (_product.productType == ProductType.Basic)
                AudioManager.Instance.SFXPlay(SFXType.Beat, false);
            else
                AudioManager.Instance.SFXPlay(SFXType.StrageBeat, false);
        }
    }

    // 남은 제품 수량 확인
    public void OnProductStock()
    {
        if (GameObject.FindGameObjectsWithTag("Product").Length <= 0 && products.Count <= 0)
        {
            isSuccess = true;
            customerState = CustomerState.Leave;
            return;
        }
    }

    // 손님 퇴장
    public void OnLeave()
    {
        if (isSuccess)
        {

        }
        else
        {
            GameManager.Instance.OnWarning();
        }

        Destroy(gameObject);
    }
}