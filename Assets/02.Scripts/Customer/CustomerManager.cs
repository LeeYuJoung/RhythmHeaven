using System.Collections.Generic;
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
    public float greetWaitTime;

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

    }

    private void Update()
    {
        if(currentCustomer == null && !GameManager.Instance.IsGameOver && !StageManager.Instance.isStageOver)
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

    // 颊丛 积己
    private void OnAppeared()
    {
        int _rdmIndex = Random.Range(0, customerPrefabs.Length);
        int _rdmBeatIndex = Random.Range(0, 6);

        AudioManager.Instance.SFXPlay(SFXType.Bell, true);
        currentCustomer = Instantiate(customerPrefabs[_rdmIndex], customerSpawnPosition.position, Quaternion.identity)?.GetComponent<CustomerController>();
        currentCustomer.customerState = CustomerState.Greet;
        currentCustomer.greetWaitTime = greetWaitTime;

        // 力前 积己
        for (int i = 0; i < RhythmManager.Instance.currentBeatInMeasure; i++)
        {
            int _poduct = Random.Range(0, (StageManager.Instance.currentStage == 1) ? 11 : productPrefabs.Length);
            currentCustomer.products.Enqueue(productPrefabs[_poduct]);
        }

        for(int i = 0; i < RhythmManager.Instance.currentBeatInMeasure + 1; i++)
        {
            currentCustomer.spawnTimes.Enqueue(RhythmManager.Instance.OnBeat(_rdmBeatIndex, i));
        }
    }
}