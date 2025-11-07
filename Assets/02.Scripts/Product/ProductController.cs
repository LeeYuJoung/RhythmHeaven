using UnityEngine;
using Utils.EnumTypes;

public class ProductController : MonoBehaviour
{
    public ProductType productType;

    public float moveSpeed = 3.0f;
    public bool isActive = false;

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }
}