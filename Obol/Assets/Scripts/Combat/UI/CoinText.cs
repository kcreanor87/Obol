using UnityEngine;
using UnityEngine.UI;

public class CoinText : MonoBehaviour {

	public float _timer = 2.5f;
	public RectTransform _rect;
	public float _riseAmount;
	public Text _text;
	public Color _color;
	public Vector2 _pos;

	void Start(){
		_rect = gameObject.GetComponent<RectTransform>();
		_text = gameObject.GetComponent<Text>();
		_rect.anchoredPosition = new Vector3(_pos.x, _pos.y);
	}

	void Update(){
		_text.color = _color;
		var y = _pos.y + _riseAmount;
		_rect.anchoredPosition = new Vector3(_pos.x, y);
		_timer -= Time.deltaTime;
		_riseAmount += 1.0f;
		if (_timer <= 0.0f) Destroy(gameObject);
		_color.a -= 0.01f;
	}
}
