using UnityEngine;
using System.Collections;

public class Shard : MonoBehaviour {
	
	public int _type;
	public Transform _player;
	public Vector3 _chasePos;
	public CombatCounters _counters;
	public Combat_UI _ui;
	public int _value = 1;

	public float _speed = 1.0f;

	public bool _active;

	void Awake(){
		_counters = GameObject.Find("Counters").GetComponent<CombatCounters>();
		_player = GameObject.Find("Player").GetComponent<Transform>();
		_ui = GameObject.Find("UI").GetComponent<Combat_UI>();
	}

	void OnTriggerEnter(Collider col){
		_active |= (col.tag == "Player");
	}

	void Update(){
		if (_active){
			float x = _player.position.x;
			float z = _player.position.z;
			_chasePos = new Vector3(x, 1.0f, z);
			 var step = _speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, _chasePos, step);
	     	var dist = Vector3.Distance(transform.position, _player.position);
			//_active |= (dist > 7.0f);
			if (dist <= 3.0f){
				CollectResource();
			}
			else if (dist <= 3.5f){
				Shrink();
			}
			_speed += 0.1f;
			if (_speed > 3.0f){
				Shrink();
			}			
		}		
	}

	void Shrink(){
		float h = Mathf.Max(0, transform.localScale.x - 0.01f);
		float i = Mathf.Max(0, transform.localScale.x - 0.01f);
		float j = Mathf.Max(0, transform.localScale.x - 0.01f);
		transform.localScale = new Vector3(h, i, j);
		_speed += 0.1f;
	}

	void CollectResource(){
		_manager._resources[_type] += _value;
		_counters._resourcesCollected += _value;
		_ui.UpdateUI();
		Destroy(gameObject);
	}
}
