using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class CursorAnimation2D 
{ 
    public Sprite[] cursorFrames;
    public float frameRatio = 0.1f;
    public string[] objectTags; 
}

public class CursorManager : MonoBehaviour
{
    public CursorAnimation2D defaultAnimation;
    public CursorAnimation2D[] animations;
    public LayerMask interactableLayers;

    private int _currentFrame = 0;
    private Sprite _sprite;
    private Texture2D _texture;
    private float _timer = 0f;
    private bool _isHovering = false;
    private Vector2 _cursorHotspot;
    private CursorAnimation2D _currentAnimation;

    private void Update()
    {
        Vector2 _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D _hit = Physics2D.Raycast(_mousePos, Vector2.zero, Mathf.Infinity,interactableLayers);

        if (_hit.collider != null)
        {
            string _objTag = _hit.collider.gameObject.tag;
            _currentAnimation = GetAnimationForObject(_objTag);

            if (_currentAnimation != null)
            {
                if (!_isHovering)
                {
                    _isHovering = true;
                    _currentFrame = 0;
                }

                AnimateCursor(_currentAnimation);

            }
        }
        else
        {
            if (_isHovering)
            { 
                _isHovering = false;
                _currentFrame = 0;
            }
            AnimateCursor(defaultAnimation);
        }

    }
    void AnimateCursor(CursorAnimation2D animation) 
    { 
        _timer += Time.deltaTime;
        if (_timer >= animation.frameRatio)
        {
            _timer -= animation.frameRatio;
            _currentFrame = (_currentFrame + 1) % animation.cursorFrames.Length;
            _texture = TexturizeSprite(animation.cursorFrames[_currentFrame]);
            _cursorHotspot = new Vector2(_texture.width / 2, _texture.height / 2);
            Cursor.SetCursor(_texture, _cursorHotspot, CursorMode.Auto);
        }
    }

    CursorAnimation2D GetAnimationForObject(string tag)
    {
        for (int i = 0; i < animations.Length; i++)
        {
            for (int j = 0; j < animations[i].objectTags.Length; j++)
            {
                if (animations[i].objectTags[j] == tag)
                { 
                    return animations[i];
                }
            }
        }
        return null;
    }

    Texture2D TexturizeSprite(Sprite sprite)
    {
        Texture2D _newTexture = new Texture2D((int)sprite.textureRect.width, (int)sprite.textureRect.height);
        var _pixels = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                    (int)sprite.textureRect.y,
                                                    (int)sprite.textureRect.width,
                                                    (int)sprite.textureRect.height);
        _newTexture.SetPixels(_pixels);
        _newTexture.Apply();
        return _newTexture;
    }
}
