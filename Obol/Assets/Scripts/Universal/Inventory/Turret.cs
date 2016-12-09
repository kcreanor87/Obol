[System.Serializable]
public class Turret {

	public int _id;
	public string _name;
	public int _dam;
	public float _radius;	
	public float _fireRate;
	public int _cost;
	public int _levelReq;
	public bool _bought;
	public float _boostValue;
	public int _type;


	public Turret(int id, string name, float radius, int dam, float fireRate, int cost, int levelReq, bool bought, float boost, int type){
		_id = id;
		_name = name;
		_radius = radius;
		_dam = dam;
		_fireRate = fireRate;
		_cost = cost;
		_levelReq = levelReq;
		_bought = bought;
		_boostValue = boost;
		_type = type;
	}
}