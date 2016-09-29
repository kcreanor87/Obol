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
	public Color _enemyHit;
	public float _offset;
	public bool _playerHit;

	void Start(){
		_rect = gameObject.GetComponent<RectTransform>();
		_text = gameObject.GetComponent<Text>();
		_offset = Random.Range(-15.0f, 15.0f);
	}

	void Update(){
		var pos = Camera.main.WorldToScreenPoint(_target.position);
		var x = pos.x + _offset;
		var y = pos.y + _riseAmount;
		_rect.anchoredPosition = new Vector3(x, y);
		_timer -= Time.deltaTime;
		_riseAmount += 1.0f;
		if (_timer <= 0.0f) Destroy(gameObject);
		_color.a -= 0.02f;
		_enemyHit.a -= 0.02f;
		_text.color = (!_playerHit) ? _color : _enemyHit;

	}
}
