using UnityEngine;
using Utils.EnumTypes;

public class CustomerManager : MonoBehaviour
{
    private static CustomerManager instance;
    public static CustomerManager Instance { get { return instance; } }

    public GameObject[] productPrefabs;
    public GameObject[] customerPrefabs;
    public CustomerController currentCustomer;

    private Transform customerSpawnPosition;

    private int currentStage = 1;          // 현재 스테이지
    private int customerCount = 2;         // 현재 등장할 손님 수
    private int currentCustomerCount = 0;  // 현재까지 등장한 손님 수

    private bool isAppeared = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        customerSpawnPosition = GameObject.Find("CustomerSpawnPos").GetComponent<Transform>();
    }

    private void Start()
    {
        OnAppeared();
    }

    private void Update()
    {

    }

    // 손님 생성
    private void OnAppeared()
    {
        int _rdmIndex = Random.Range(0, customerPrefabs.Length);
        currentCustomer = Instantiate(customerPrefabs[_rdmIndex], customerSpawnPosition.position, Quaternion.identity)?.GetComponent<CustomerController>();
        currentCustomer.customerState = CustomerState.Greet;

        for(int i = 0; i < currentCustomer.productCount; i++)
        {
            currentCustomer.products.Enqueue(productPrefabs[Random.Range(0, productPrefabs.Length)]);
        }
    }
}