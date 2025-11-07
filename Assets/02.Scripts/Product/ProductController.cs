using UnityEngine;
using Utils.EnumTypes;

public class ProductController : MonoBehaviour
{
    public ProductType productType;

    private float moveSpeed = 4.0f;
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