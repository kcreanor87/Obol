using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour {

	public float _timer = 1.5f;
	public Transform _target;
	public RectTransform _rect;
	public float _riseAmount;
	public Text _text;
	public float _alpha = 0.5f;
	public Color _color;

	void Start(){
		_rect = gameObject.GetComponent<RectTransform>();
		_text = gameObject.GetComponent<Text>();
	}

	void Update(){
		var pos = Camera.main.WorldToScreenPoint(_target.position);
		var y = pos.y + _riseAmount;
		_rect.anchoredPosition = new Vector3(pos.x, y);
		_timer -= Time.deltaTime;
		_riseAmount += 1.0f;
		if (_timer <= 0.0f) Destroy(gameObject);
		_color.a -= 0.02f;
		_text.color = _color;
	}
}
