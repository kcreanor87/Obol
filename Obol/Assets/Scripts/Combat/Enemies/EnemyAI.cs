using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

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
	public bool _exploder;
	public GameObject _mainGO;
	public Collider _col;
	public GameObject _exploderProjectile;
	public GameObject _attackGO;
	public GameObject _deathGO;

	void Start(){
		_anim = transform.GetChild(0).GetComponentInChildren<Animator>();
		_col = transform.GetChild(0).GetComponentInChildren<Collider>();
		_textSpawn = transform.FindChild("TextSpawn");		
		_agent = gameObject.GetComponent<NavMeshAgent>();
		_player = GameObject.Find("Player").GetComponent<PlayerControls_Combat>();
		if (_ranged) _shooting = GetComponentInChildren<Shooting>();
		_counter = GameObject.Find("Counters").GetComponent<CombatCounters>();
		_ui = GameObject.Find("UI").GetComponent<Combat_UI>();
		_agent.enabled = true;	
		if (_exploder){
			_attackGO.SetActive(false);
			_deathGO.SetActive(false);
		}		
		ChasePlayer();		
	}

	void Update(){
		if (_attacking) RotateToPlayer();
	}

	void ChasePlayer(){
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
		if (!_exploder) _agent.SetDestination(transform.position);
		_anim.SetBool("Running", false);
		if (_ranged){
			print("Shoot!");
			_shooting.CalcVelocity(_player.transform.position);
		}
		_anim.SetBool("Attack", true);
		yield return new WaitForSeconds(0.5f);
		if (!_exploder){
			var dist = Vector3.Distance(transform.position, _player.transform.position);
			if (dist > _range){
				_attacking = false;
				_anim.SetBool("Attack", false);		
				ChasePlayer();
			} 
			else{
				if (!_ranged){
					_player.BeenHit(_damage);	
				}
				yield return new WaitForSeconds(0.25f);
				AttackStart();
			}
		}
		else{
			_agent.enabled = false;
			_attackGO.SetActive(true);
			_mainGO.SetActive(false);
			_col.enabled = false;
			Instantiate(_exploderProjectile, transform.position, Quaternion.identity);
			_attacking = false;	
			_counter._totalEnemies++;
			_ui.UpdateUI();	
			StartCoroutine(Die());
		}	
	}

	public void BeenHit(int damage){	
		_health -= damage;
		_ui.DamageText(_textSpawn, damage, false);
		_attacking = false;
		if (_health <= 0){
			StopAllCoroutines();			
			_agent.enabled = false;
			_counter._totalEnemies++;
			_ui.UpdateUI();	
			_col.enabled = false;			
			if (_exploder){				
				_mainGO.SetActive(false);
				_deathGO.SetActive(true);
				Instantiate(_exploderProjectile, transform.position, Quaternion.identity);
			}
			else{
				_anim.SetBool("Dead", true);
			}
			StartCoroutine(Die());
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

