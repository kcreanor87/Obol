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
	public float _armour = 1.0f;
	public Shooting _shooting;
	public Transform _textSpawn;
	public bool _exploder;
	public GameObject _mainGO;
	public Collider _col;
	public GameObject _exploderProjectile;
	public GameObject _attackGO;
	public GameObject _deathGO;

	public int _exp = 200;
	public int _goldChance = 50;
	public int _goldDropped;
	public int _minGold = 10;
	public int _maxGold = 100;

	public GameObject _coin;

	void Start(){
		_anim = transform.GetChild(0).GetComponentInChildren<Animator>();
		_col = transform.GetChild(0).GetComponentInChildren<Collider>();
		_textSpawn = transform.FindChild("TextSpawn");		
		_agent = gameObject.GetComponent<NavMeshAgent>();
		_player = GameObject.Find("Player").GetComponent<PlayerControls_Combat>();
		if (_ranged) _shooting = GetComponentInChildren<Shooting>();
		_counter = GameObject.Find("Counters").GetComponent<CombatCounters>();
		_ui = GameObject.Find("Combat UI").GetComponent<Combat_UI>();
		_agent.enabled = true;	
		_goldDropped = Random.Range(_minGold, _maxGold);
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
			_counter._enemiesKilled++;
			_ui.UpdateUI();	
			StartCoroutine(Die());
		}	
	}

	public void BeenHit(int damage){
		var dam = Mathf.FloorToInt((float) damage * _armour);	
		_health -= dam;
		_ui.DamageText(_textSpawn, dam, false);
		_attacking = false;
		if (_health <= 0){
			OnDeath();
		}
	}

	void OnDeath(){
		StopAllCoroutines();			
		_agent.enabled = false;
		_counter._enemiesKilled++;
		_manager._currentXP += _exp;
		_manager.CheckXP();
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
		GoldDrop();
		_ui.ExpText(_exp);
		StartCoroutine(Die());
	}

	void GoldDrop(){
		var chance = Random.Range(1, 101);
		if (chance >= _goldChance){
			var coin = (GameObject) Instantiate(_coin, transform.position, Quaternion.identity);
			var coinScript = coin.GetComponent<Coin>();
			coinScript._value = _goldDropped;
		}
	}

	public IEnumerator Die(){
		yield return new WaitForSeconds(2.5f);
		if (_counter._spawnPoints == 0){
			if (_counter._enemiesKilled == _counter._enemiesSpawned){
				_ui.LevelEnd(true);
			}
		}
		Destroy(gameObject);
	}

	void RotateToPlayer(){
		Vector3 targetDir = _player.transform.position - transform.position;
        float step = 4.0f * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        transform.rotation = Quaternion.LookRotation(newDir);
	}
}

