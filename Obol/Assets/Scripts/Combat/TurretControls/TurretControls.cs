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
	public bool _active;
	public bool _static;
	public Transform _player;

	public PlayerControls_Combat _pcCombat;
	public PlayerControls_NonCombat _pcNonCombat;

	public NavMeshAgent _agent;

	void Start(){
		CollectData();			
	}

	void CollectData(){
		_type = _CombatManager._turretSlot._type;
		_agent = gameObject.GetComponent<NavMeshAgent>();
		_player = GameObject.Find("Player").GetComponent<Transform>();
		_fireRate = _CombatManager._turretSlot._fireRate;
		_damage = _CombatManager._turretSlot._dam;
	}

	void Update(){
		if (_enemiesInRange.Count > 0){						
			if (_type == 0){
				RotateToTarget();
			}			
			if (!_active){
				switch (_type){
					case 0:
					DamageSingle();
					break;
					case 1:
					DamageArea();
					break;
					case 2:
					SlowEnemies();
					break;
					case 3:
					//**Boost player stats
					break;
					case 4:
					BoostResources();
					break;
				}
			}			
		}
		else{
			//Play idleAnim;
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
			StartCoroutine(CheckPlayerDistance());
	}	

	public IEnumerator CheckPlayerDistance(){
		if (Vector3.Distance(transform.position, _player.position) < 6.0f){
			_agent.Stop();
		}
		else{
			_agent.SetDestination(_player.position);
			_agent.Resume();
		}
		yield return new WaitForSeconds (1.0f);
	}	

	void DamageSingle(){
		StartCoroutine(SingleFireRate());
	}

	void DamageArea(){
		StartCoroutine(AreaFireRate());
	}

	void SlowEnemies(){
		StartCoroutine(Slow());
	}

	void BoostPlayerDamage(){

	}

	void BoostPlayerDefense(){

	}

	void BoostResources(){
		StartCoroutine(Boost());

	}

	public IEnumerator SingleFireRate(){
		AttackSingleTarget();
		_active = true;
		yield return new WaitForSeconds(_fireRate);
		print("Attack single target");
		_active = false;
	}

	public IEnumerator AreaFireRate(){
		AreaDamage();
		_active = true;
		yield return new WaitForSeconds(_fireRate);	
		_active = false;
	}

	public IEnumerator Slow(){
		ImplementSlow();
		_active = true;
		yield return new WaitForSeconds(_fireRate);	
		_active = false;
	}

	public IEnumerator Boost(){
		ImplementBoost();
		_active = true;
		yield return new WaitForSeconds(_fireRate);	
		_active = false;
	}

	void AttackSingleTarget(){
		var script = _enemiesInRange[0].GetComponentInParent<EnemyAI>();
		script.BeenHit(_damage);
		if (script._health <= 0){
		_enemiesInRange.RemoveAt(0);
		}		
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

	void ImplementSlow(){
		for (int i = 0; i < _enemiesInRange.Count; i++){
			var script = _enemiesInRange[i].GetComponentInParent<EnemyAI>();
			if (!(script._agent.speed < script._speed)) script.Slowed(5.0f);
			if (script._health <= 0){
				_enemiesInRange.RemoveAt(i);
			}
		}
	}

	void ImplementBoost(){
		for (int i = 0; i < _enemiesInRange.Count; i++){
			var script = _enemiesInRange[i].GetComponentInParent<EnemyAI>();
			if (!(script._dropBoost)) script.Boost(2.0f);
			if (script._health <= 0){
				_enemiesInRange.RemoveAt(i);
			}
		}
	}		

	void OnTriggerEnter(Collider col){
		if (col.tag == "Enemy"){
			_enemiesInRange.Add(col.gameObject);
		}
		_playerInRange |= (col.tag == "Player");
	}

	void OnTriggerExit(Collider col){
		if (col.tag == "Enemy"){
			_enemiesInRange.Remove(col.gameObject);
		}
		else if (col.tag == "Player"){
			_playerInRange = false;
		}
	}	

	public void SwitchStatic(){
		_static = !_static;
		if (_static) _agent.Stop();
	}
}
