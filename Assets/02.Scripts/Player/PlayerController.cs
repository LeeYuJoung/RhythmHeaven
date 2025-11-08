using UnityEngine;
using Utils.EnumTypes;

public class PlayerController : MonoBehaviour
{
    public PlayerState playerState = PlayerState.Idle;

    public Animator playerAnimation;
    public RuntimeAnimatorController[] animatorControllers;

    private int mistakeCount = 0;
    private const int maxMistakeCount = 2;

    private void Awake()
    {
        playerAnimation = GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameOver || StageManager.Instance.isStageOver)
            return;

        if (Input.GetKeyDown(KeyCode.Return) && CustomerManager.Instance.currentCustomer != null)
        {
            if(CustomerManager.Instance.currentCustomer.customerState == CustomerState.Greet)
            {
                if (StageManager.Instance.currentStage == 3)
                    OnMistake();

                playerAnimation.SetTrigger("Greet");
                CustomerManager.Instance.currentCustomer.currentTime = 0.0f;
                CustomerManager.Instance.currentCustomer.customerState = CustomerState.Calculate;
                CustomerManager.Instance.currentCustomer.productSpawnTime = CustomerManager.Instance.currentCustomer.spawnTimes.Dequeue();
            }
            else
            {
                OnScanner();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            OnScanner();
        }
    }

    public void OnScanner()
    {
        AudioManager.Instance.SFXPlay(SFXType.Scanner, true);
        playerAnimation.SetTrigger("Scanner");
    }

    public void OnMistake()
    {
        mistakeCount++;
        AudioManager.Instance.SFXPlay(SFXType.Wrong, false);

        if (mistakeCount >= maxMistakeCount)
        {
            GameManager.Instance.OnWarning();
            mistakeCount = 0;
        }
    }

    public void RemoveObject(GameObject _product)
    {
        // 즉시 눈에 안 보이게
        _product.GetComponent<SpriteRenderer>().enabled = false;
        _product.GetComponent<Collider2D>().enabled = false;

        // 실제 파괴는 다음 프레임에
        Destroy(_product);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnScanner();

            if (CustomerManager.Instance.currentCustomer.customerState == CustomerState.Calculate)
            {
                ProductController _product = collision.gameObject.GetComponent<ProductController>();
                if (_product != null && _product.productType == ProductType.Basic)
                {
                    _product.isActive = true;
                    RemoveObject(collision.gameObject);
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            OnScanner();

            if (CustomerManager.Instance.currentCustomer.customerState == CustomerState.Calculate)
            {
                ProductController _product = collision.gameObject.GetComponent<ProductController>();
                if (_product != null && _product.productType == ProductType.Strange)
                {
                    _product.isActive = true;
                    RemoveObject(collision.gameObject);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (CustomerManager.Instance.currentCustomer.customerState == CustomerState.Calculate)
        {
            ProductController _product = collision.gameObject.GetComponent<ProductController>();
            if (_product != null && !_product.isActive)
            {
                OnMistake();
                RemoveObject(collision.gameObject);
            }
        }
    }
}