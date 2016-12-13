using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretControls : MonoBehaviour {

	public List <EnemyAI> _enemiesInRange = new List <EnemyAI>();
	public bool _playerInRange;
	public int _type;
	public float _fireRate;
	public int _damage;
	public EnemyAI _targetScript;
	public bool _active;
	public bool _static;
	public Transform _player;
	public NavMeshAgent _agent;
	public Transform _target;
	public bool _offensive;

	void Start(){
		CollectData();			
	}

	void CollectData(){
		_type = _CombatManager._turretSlot._type;
		_agent = gameObject.GetComponentInParent<NavMeshAgent>();
		_player = GameObject.Find("Player").GetComponent<Transform>();
		_fireRate = _CombatManager._turretSlot._fireRate;
		_damage = _CombatManager._turretSlot._dam;
	}

	void FixedUpdate(){
		if (_enemiesInRange.Count > 0 && _type != 3){
			if (!_static && _offensive) _target = _enemiesInRange[0].transform;						
			if (_type == 0){
				RotateToTarget();
			}			
			if (!_active){
				_active = true;
				switch (_type){
					case 0:
					StartCoroutine(SingleFireRate());
					break;
					case 1:
					StartCoroutine(AreaFireRate());
					break;
					case 2:
					StartCoroutine(Slow());
					break;
					case 4:
					StartCoroutine(Boost());
					break;
				}
			}			
		}
		else{
			_target = _player;
		}
		if (!_static) FollowPlayer();	
	}	

	void RotateToTarget(){
		Quaternion newRotation = Quaternion.LookRotation(_enemiesInRange[0].transform.position - transform.position);
		newRotation.x = 0f;
       	newRotation.z = 0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 15);
	}

	void FollowPlayer(){
		if (!_static) _agent.SetDestination(_target.position);
	}

	void BoostPlayer(bool active){
		if (active) _CombatManager.Boost(_CombatManager._turretSlot._boostValue);
		if (!active) _CombatManager.RemoveBoost(_CombatManager._turretSlot._boostValue);
	}

	public IEnumerator SingleFireRate(){
		AttackSingleTarget();
		yield return new WaitForSeconds(_fireRate);
		print("Attack single target");
		_active = false;
	}

	public IEnumerator AreaFireRate(){
		AreaDamage();		
		yield return new WaitForSeconds(_fireRate);	
		_active = false;
	}

	public IEnumerator Slow(){
		ImplementSlow();
		yield return new WaitForSeconds(_fireRate);	
		_active = false;
	}

	public IEnumerator Boost(){		
		ImplementResourceBoost();
		yield return new WaitForSeconds(_fireRate);	
		_active = false;
	}

	void AttackSingleTarget(){
		_enemiesInRange[0].BeenHit(_damage);
		if (_enemiesInRange[0]._health <= 0){
		_enemiesInRange.RemoveAt(0);
		}		
	}

	void AreaDamage(){
		for (int i = 0; i < _enemiesInRange.Count; i++){
			_enemiesInRange[i].BeenHit(_damage);			
		}
		for (int i = _enemiesInRange.Count - 1; i >= 0; i--){
			if (_enemiesInRange[i]._health <= 0){
				_enemiesInRange.RemoveAt(i);
			}		
		}

	}

	void ImplementSlow(){
		for (int i = 0; i < _enemiesInRange.Count; i++){
			if (!(_enemiesInRange[i]._agent.speed < _enemiesInRange[i]._speed)) _enemiesInRange[i].Slowed(5.0f);
			if (_enemiesInRange[i]._health <= 0){
				_enemiesInRange.RemoveAt(i);
			}
		}
	}

	void ImplementResourceBoost(){
		for (int i = 0; i < _enemiesInRange.Count; i++){
			if (!(_enemiesInRange[i]._dropBoost)) _enemiesInRange[i].DropBoost(2.0f, _CombatManager._turretSlot._boostValue);
			if (_enemiesInRange[i]._health <= 0){
				_enemiesInRange.RemoveAt(i);
			}
		}
	}		

	void OnTriggerEnter(Collider col){
		if (_type == 3){
			if (col.tag == "Player" && !_active){
				_playerInRange = true;
				BoostPlayer(true);
				_active = true;
			}
		}
		else{
			if (col.tag == "Enemy"){
				_enemiesInRange.Add(col.gameObject.GetComponentInParent<EnemyAI>());
			}
		}		
	}

	void OnTriggerExit(Collider col){
		if (_type == 3){
			if (col.tag == "Player" && _active){
				_playerInRange = false;
				BoostPlayer(false);
				_active = false;
			}
		}
		else{
			if (col.tag == "Enemy"){
				_enemiesInRange.Remove(col.gameObject.GetComponentInParent<EnemyAI>());
			}
		}
	}	

	public void SwitchStatic(){
		_agent.SetDestination(transform.position);
		_static = !_static;
	}
}
