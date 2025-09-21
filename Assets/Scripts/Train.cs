using UnityEngine;

public class Train : MonoBehaviour
{
    public float speed = 2f;
    public float resetX = -15f; // vị trí bên trái reset
    public float startX = 15f; // vị trí xuất hiện lại

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Khi ra khỏi màn hình bên trái thì reset lại bên phải
        if (transform.position.x < resetX)
        {
            Vector3 pos = transform.position;
            pos.x = startX;
            transform.position = pos;
        }
    }
}
