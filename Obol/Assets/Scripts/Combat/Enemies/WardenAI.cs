using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WardenAI : MonoBehaviour {

	public int _health = 100;

	public NavMeshAgent _agent;
	public PlayerControls_Combat _player;
	public CombatCounters _counter;
	public Combat_UI _ui;
	public float _range;
	public Animator _anim;
	public bool _attacking;
	public int _damage = 10;
	public Transform _textSpawn;
	public Collider _col;
	public Collider _scytheCol;
	public Collider _burstCol;
	public ParticleSystem _bladePart;
	public ParticleSystem _burstPart;
	public bool _spawned;
	public GameObject _wardenTxt;
	public Text _currentHPText;
	public Text _maxHPText;
	public Animator _doorAnim;

	void Start(){
		_doorAnim = GameObject.Find("MerchantsCrypt").GetComponent<Animator>();
		_wardenTxt.SetActive(true);
		_maxHPText.text = _health.ToString();
		_currentHPText.text = _health.ToString();
		_anim = transform.GetChild(0).GetComponentInChildren<Animator>();
		_col = transform.GetChild(0).GetComponentInChildren<Collider>();
		_scytheCol.enabled = false;
		_burstCol.enabled = false;
		_textSpawn = transform.FindChild("TextSpawn");		
		_agent = gameObject.GetComponent<NavMeshAgent>();
		_player = GameObject.Find("Player").GetComponent<PlayerControls_Combat>();
		_counter = GameObject.Find("Counters").GetComponent<CombatCounters>();
		_ui = GameObject.Find("UI").GetComponent<Combat_UI>();
		_agent.enabled = true;
		StartCoroutine(Spawn());	
	}

	void Update(){
		if (_attacking) RotateToPlayer();		
	}

	public IEnumerator Spawn(){
		yield return new WaitForSeconds (2.33f);
		ChasePlayer();
	}

	void ChasePlayer(){	
		StartCoroutine(ChaseLoop(0.25f));
	}

	void AttackStart(){
		StartCoroutine(Attack());
	}
	void Attack2Start(){
		StartCoroutine(Attack2());
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
		print("Wind Up");
		_agent.SetDestination(transform.position);
		_anim.SetBool("Attack", true);
		yield return new WaitForSeconds(1.3f);			
		_agent.SetDestination(_player.transform.position);
		yield return new WaitForSeconds(0.9f);
		_agent.speed = 500.0f;				
		_scytheCol.enabled = true;		
		yield return new WaitForSeconds(0.5f);
		_agent.speed = 12.0f;		
		_agent.SetDestination(transform.position);		
		yield return new WaitForSeconds(0.4f);
		_scytheCol.enabled = false;
		yield return new WaitForSeconds(0.9f);
		_anim.SetBool("Attack", false);
		_attacking = true;
		_agent.SetDestination(_player.transform.position);
		yield return new WaitForSeconds(1.0f);			
		var dist = Vector3.Distance(transform.position, _player.transform.position);
		if (dist <= 5.0f){
			Attack2Start();
		}
		else if (dist <= _range){
			AttackStart();
		}
		else{
			ChasePlayer();
		}
	}

	public IEnumerator Attack2(){
		_attacking = false;
		print ("Attack2");
		_agent.SetDestination(transform.position);
		_anim.SetBool("Attack2", true);
		yield return new WaitForSeconds(1.5f);
		_burstCol.enabled = true;
		_burstPart.Play();
		_bladePart.Stop();
		yield return new WaitForSeconds(1.16f);
		_burstCol.enabled = false;
		yield return new WaitForSeconds(1.16f);		
		_bladePart.Play();
		_anim.SetBool("Attack2", false);
		_attacking = true;
		yield return new WaitForSeconds(1.0f);
		var dist = Vector3.Distance(transform.position, _player.transform.position);
		if (dist <= 5.0f){
			Attack2Start();
		}
		else if (dist <= _range){
			AttackStart();
		}
		else{
			ChasePlayer();
		}
	}

	public void BeenHit(int damage){	
		_health -= damage;
		_ui.DamageText(_textSpawn, damage, false);
		_currentHPText.text = _health.ToString();
		if (_health <= 0){
			_attacking = false;
			StopAllCoroutines();			
			_agent.enabled = false;
			_counter._totalEnemies++;
			_ui.UpdateUI();	
			_col.enabled = false;
			_anim.SetBool("Dead", true);
			_bladePart.Stop();
			Destroy(_wardenTxt);
			_doorAnim.SetBool("Open", true);
			_manager._npcChat[1] = true;
			_manager._npcChat[2] = true;
			_manager._npcChat[3] = true;
			_manager._npcChat[4] = true;
		}
	}

	void RotateToPlayer(){
		float h = _player.transform.position.x;
		float v = _player.transform.position.z;
		var rot = new Vector3(h, transform.position.y, v);
		Vector3 targetDir = rot - transform.position;
        float step = 4.0f * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        transform.rotation = Quaternion.LookRotation(newDir);
	}
}


