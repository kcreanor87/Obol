using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	public Transform _target;
	public RectTransform _hp;
	public EnemyAI _parent;
	public int _maxHealth;
	public Image _background;
	public bool _destructable;
	public Destructibles _dbScript;

	// Use this for initialization
	void Start () {
		_hp = transform.FindChild("EnemyHP").GetComponent<RectTransform>();
		_hp.sizeDelta = new Vector2(0, 0);	
		_background = gameObject.GetComponent<Image>();
		_background.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (_target == null) Destroy(gameObject);
		var wantedPos = Camera.main.WorldToScreenPoint (_target.position);
     	transform.position = wantedPos;     	
	}

	public void UpdateHealth(){
		if (_background == null) Destroy(gameObject);
		_background.enabled = true;
		_hp.sizeDelta = new Vector2(100 * _parent._health / _maxHealth, 16.5f);
	}

	public void DestroyGO(){
		Destroy(gameObject);
	}

	public void UpdateDestructable(){
		if (_background == null) Destroy(gameObject);
		_background.enabled = true;
		_hp.sizeDelta = new Vector2(100 * _dbScript._currentHP / _maxHealth, 16.5f);
	}
}
