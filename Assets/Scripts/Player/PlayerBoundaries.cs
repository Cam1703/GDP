using UnityEngine;

public class PlayerBoundaries : MonoBehaviour
{
    private Vector2 _screenBounds;

    private float _objectWidth;
    private float _objectHeight;

    void Start()
    {
        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        _objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2f;
        _objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2f;
    }

    void LateUpdate()
    {
        Vector3 _viewPos = transform.position;
        _viewPos.x = Mathf.Clamp(_viewPos.x, _screenBounds.x * -1 + _objectWidth, _screenBounds.x - _objectWidth);
        _viewPos.y = Mathf.Clamp(_viewPos.y, _screenBounds.y * -1 + _objectHeight, _screenBounds.y - _objectHeight);
        transform.position = _viewPos;
    }
}
