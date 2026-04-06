using UnityEngine;

public class MenuCamSway : MonoBehaviour
{

    public float maxOffset = 2f;
    public float smoothSpeed = 5f;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        Vector2 mousePos = Input.mousePosition;

        float x = (mousePos.x / Screen.width) * 2f - 1f;
        float y = (mousePos.y / Screen.height) * 2f - 1f;
        Vector3 targetOffset = new Vector3(-x, -y, 0f) * maxOffset;
        Vector3 targetPos = startPos + targetOffset;
        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            targetPos,
            smoothSpeed * Time.deltaTime
        );
    }
}

