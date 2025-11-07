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

    private float currentTime = 0.0f;
    private float customerSpawnTime = 2.0f;

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
        if(currentCustomer == null && !GameManager.Instance.IsGameOver)
        {
            if(currentTime < customerSpawnTime)
            {
                currentTime += Time.deltaTime;
            }
            else
            {
                currentTime = 0.0f;
                OnAppeared();
            }
        }
    }

    // ¼Õ´Ô »ý¼º
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