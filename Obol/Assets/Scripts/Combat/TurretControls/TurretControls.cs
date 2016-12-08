using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretControls : MonoBehaviour {

	public List <GameObject> _enemiesInRange = new List <GameObject>();
	public bool _playerInRange;
	public int _type;
	public float _fireRate;
	public int _damage;
	public EnemyAI _targetScript;
	public bool _attacking;
	public bool _static;
	public Transform _player;

	public NavMeshAgent _agent;

	void Start(){
		CollectData();			
	}

	void Update(){
		if (_enemiesInRange.Count > 0){						
			if (_type == 0){
				RotateToTarget();
			}			
			if (!_attacking){
				switch (_type){
					case 0:
					SingleTarget();
					break;
					case 1:
					AreaEffect();
					break;
				}
			}			
		}
		else{
			//Play idleAnim;
		}
		if (!_static) FollowPlayer();	
	}

	void FollowPlayer(){
			StartCoroutine(CheckPlayerDistance());
	}

	public void SwitchStatic(){
		_static = !_static;
		if (_static) _agent.Stop();
	}

	public IEnumerator CheckPlayerDistance(){
		if (Vector3.Distance(transform.position, _player.position) < 8.0f){
			_agent.Stop();
		}
		else{
			_agent.SetDestination(_player.position);
			_agent.Resume();
		}
		yield return new WaitForSeconds (1.0f);
	}

	void CollectData(){
		_agent = gameObject.GetComponent<NavMeshAgent>();
		_player = GameObject.Find("Player").GetComponent<Transform>();
		_fireRate = _CombatManager._turretSlot._fireRate;
		_damage = _CombatManager._turretSlot._dam;
	}

	void SingleTarget(){
		StartCoroutine(SingleFireRate());
	}

	void AreaEffect(){
		StartCoroutine(AreaFireRate());
	}

	public IEnumerator SingleFireRate(){
		AttackSingleTarget();
		_attacking = true;
		yield return new WaitForSeconds(_fireRate);
		print("Attack single target");
		_attacking = false;
	}

	void AttackSingleTarget(){
		var script = _enemiesInRange[0].GetComponentInParent<EnemyAI>();
		script.BeenHit(_damage);
		if (script._health <= 0){
		_enemiesInRange.RemoveAt(0);
		}		
	}

	public IEnumerator AreaFireRate(){
		AreaDamage();
		_attacking = true;
		yield return new WaitForSeconds(_fireRate);	
		_attacking = false;
	}

	void AreaDamage(){
		for (int i = 0; i < _enemiesInRange.Count; i++){
			var script = _enemiesInRange[i].GetComponentInParent<EnemyAI>();
			script.BeenHit(_damage);
			if (script._health <= 0){
				_enemiesInRange.RemoveAt(i);
			}
		}
	}

	void RotateToTarget(){
		Quaternion newRotation = Quaternion.LookRotation(_enemiesInRange[0].transform.position - transform.position);
		newRotation.x = 0f;
       	newRotation.z = 0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 15);
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "Enemy"){
			_enemiesInRange.Add(col.gameObject);
		}
		else if (col.tag == "Player"){
			_playerInRange = true;
		}
	}

	void OnTriggerExit(Collider col){
		if (col.tag == "Enemy"){
			_enemiesInRange.Remove(col.gameObject);
		}
		else if (col.tag == "Player"){
			_playerInRange = false;
		}
	}	
}
