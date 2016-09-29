using UnityEngine;
using System.Collections;

public class EnemyWM : MonoBehaviour {

	public int _health = 100;

	public NavMeshAgent _agent;
	public PlayerControls_Combat _player;
	public CombatCounters _counter;
	public Combat_UI _ui;
	public float _range;
	public Animator _anim;
	public bool _ranged;
	public bool _attacking;
	public int _damage = 10;
	public Shooting _shooting;
	public Transform _textSpawn;

	void Start(){
		_anim = transform.GetChild(0).GetComponentInChildren<Animator>();
		_textSpawn = transform.FindChild("TextSpawn");		
		_agent = gameObject.GetComponent<NavMeshAgent>();
		_player = GameObject.Find("Player").GetComponent<PlayerControls_Combat>();
		if (_ranged) _shooting = GetComponentInChildren<Shooting>();
		_counter = GameObject.Find("Counters").GetComponent<CombatCounters>();
		_ui = GameObject.Find("UI").GetComponent<Combat_UI>();
		_agent.enabled = true;		
		ChasePlayer();		
	}

	void Update(){
		if (_attacking) RotateToPlayer();
	}

	void ChasePlayer(){
		_anim.SetBool("WeaponB", (!_ranged));
		_anim.SetBool("Hit", false);
		_anim.SetBool("Running", true);		
		StartCoroutine(ChaseLoop(0.25f));
	}

	void AttackStart(){
		StartCoroutine(Attack());
	}

	public IEnumerator ChaseLoop(float looptime){		
		yield return new WaitForSeconds(looptime);
		_agent.SetDestination(_player.transform.position);
		var dist = Vector3.Distance(transform.position, _player.transform.position);
		if (dist <= _range){
			AttackStart();
		}
		else{
			ChasePlayer();
		}		
	}

	public IEnumerator Attack(){
		_attacking = true;
		_agent.SetDestination(transform.position);
		_anim.SetBool("Running", false);
		if (_ranged){
			_shooting.CalcVelocity(_player.transform.position);
		}
		_anim.SetBool("Aim", true);
		_anim.SetBool("Attack", true);
		yield return new WaitForSeconds(0.9f);
		var dist = Vector3.Distance(transform.position, _player.transform.position);
		if (dist > _range){
			_attacking = false;
			_anim.SetBool("Aim", false);
			_anim.SetBool("Attack", false);		
			ChasePlayer();
		} 
		else{
			if (!_ranged){
				_CombatManager._currentHealth -= _damage;
				print(_CombatManager._currentHealth);
				_ui.UpdateUI();		
			}
			AttackStart();
		}
	}

	public void BeenHit(int damage){	
		_health -= damage;
		_ui.DamageText(_textSpawn, damage);
		_attacking = false;	
		_anim.SetBool("Hit", true);
		if (_health <= 0){
			StopAllCoroutines();
			_anim.SetBool("Dead", true);
			_agent.Stop();
			_counter._enemiesKilled++;
			_ui.UpdateUI();			
			StartCoroutine(Die());
		}
		else{
			ChasePlayer();
		}
	}

	public IEnumerator Die(){
		yield return new WaitForSeconds(2.5f);
		Destroy(gameObject);
	}

	void RotateToPlayer(){
		Vector3 targetDir = _player.transform.position - transform.position;
        float step = 4.0f * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        transform.rotation = Quaternion.LookRotation(newDir);
	}
}
