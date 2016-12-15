using UnityEngine;
using System.Collections.Generic;

public class Destructibles : MonoBehaviour {

	public int _currentHP;
	public int _maxHP;
	public bool _hiddenBool;
	public GameObject _hiddenObj;
	public GameObject _mainObj;
	public List <GameObject> _highlightGOs = new List <GameObject>();
	public Animator _anim;
	public Collider _col;
	public Material _matA;
	public Material _matB;
	public bool _mouseOver;
	public List <GameObject> _nextSpawns = new List <GameObject>();
	public List <GameObject> _lastSpawns = new List <GameObject>();

	public bool _gate;
	public GameObject _spawnPoint;

	public CombatCounters _counters;
	public Combat_UI _ui;
	public GameObject _healthGO;
	public HealthBar _hbScript;

	public Transform _textSpawn;
	public bool _destroyed;

	// Use this for initialization
	void Start () {
		_counters = GameObject.Find("Counters").GetComponent<CombatCounters>();
		_currentHP = _maxHP;
		_col = gameObject.GetComponent<Collider>();
		_anim = gameObject.GetComponentInChildren<Animator>();
		_ui = GameObject.Find("Combat UI").GetComponent<Combat_UI>();
		_textSpawn = transform.FindChild("TextSpawn").transform;
		var healthbar = (GameObject) Instantiate(_healthGO, _textSpawn.position, Quaternion.identity, _ui.transform);		
		_hbScript = healthbar.GetComponent<HealthBar>();
		_hbScript._dbScript = this;
		_hbScript._target = _textSpawn;
		_hbScript._maxHealth = _maxHP;
		if (_hiddenBool) _hiddenObj.SetActive(false);
	}
	
	public void BeenHit(int damage){
		_currentHP -= damage;
		if (!_destroyed) _hbScript.UpdateDestructable();
		if (_currentHP <= 0){
			Destroy();
			ChangeSpawns();
		}
	}

	void Destroy(){
		_hbScript.DestroyGO();
		if (_hiddenBool) {
			_hiddenObj.SetActive(true);
			_mainObj.SetActive(false);
		}
		//_anim.SetBool("Destroyed", true);
		_col.enabled = false;
		/*for (int i = 0; i < _highlightGOs.Count; i++){
			_highlightGOs[i].GetComponent<MeshRenderer>().material = _matA;
		}*/
		if (_gate){
			Destroy(_spawnPoint);
			_counters._spawnPoints--;
			if (_counters._spawnPoints == 0 && _counters._enemiesKilled >= _counters._enemiesSpawned) _ui.LevelEnd(true);
		}
		_ui.UpdateUI();
	}

	void ChangeSpawns(){
		for (int i = 0; i < _nextSpawns.Count; i++){
			_nextSpawns[i].GetComponent<SpawnPoint>().CheckCurrentSpawn();
		}
		for (int i = 0; i < _lastSpawns.Count; i++){
			_lastSpawns[i].GetComponent<SpawnPoint>().Stop();
		}
	}
	
	public void OnMouseOver(){
		if (!_mouseOver){
			if (Input.GetMouseButton(1) && _currentHP > 0){
				for (int i = 0; i < _highlightGOs.Count; i++){
					_highlightGOs[i].GetComponent<MeshRenderer>().material = _matB;
				}
				_mouseOver = true;
			}			
		}			
	}

	public void OnMouseExit(){
		for (int i = 0; i < _highlightGOs.Count; i++){
			_highlightGOs[i].GetComponent<MeshRenderer>().material = _matA;
		}
		_mouseOver = false;
	}
}
