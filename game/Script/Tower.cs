using UnityEngine;
using System.Collections;

public class Tower : Token {
    public int CostRange {
        get { return Cost.TowerUpgrade(eUpgrade.Range, _lvRange); }
    }
    
    public int CostFirerate {
        get { return Cost.TowerUpgrade(eUpgrade.Firerate, _lvFirerate); }
    }
    
    public int CostPower {
        get { return Cost.TowerUpgrade(eUpgrade.Power, _lvPower); }
    }
    
    public int GetCost(eUpgrade type) {
        switch (type) {
            case eUpgrade.Range:
                return CostRange;
            case eUpgrade.Firerate:
                return CostFirerate;
            case eUpgrade.Power: 
                return CostPower;
        }
        return 0;
    }

    public enum eUpgrade {
        Range,
        Firerate,
        Power,
    }

    public static TokenMgr<Tower> parent;
    
    public static Tower Add(float px, float py) {
        Tower t = parent.Add(px, py);
        if(null == t) {
            return null;
        }
        t.Init();
        return t;
    }

    const float SHOT_SPEED = 5.0f;

	float _range;
    float _firerate;
    float _tFirerate;
    int _power;

    int _lvRange;
    public int LvRange {
        get { return _lvRange; }
    }
    
    int _lvFirerate;
    public int LvFirerate {
        get { return _lvFirerate; }
    }
    
    int _lvPower;
    public int LvPower {
        get { return _lvPower; }
    }

    void UpdateParam() {
        _range = TowerParam.Range(_lvRange);
        _firerate = TowerParam.Firerate(_lvFirerate);
        _power = TowerParam.Power(_lvPower);

        float avg = (_lvRange + _lvFirerate + _lvPower) / 3.0f;
        int avgLv = Mathf.CeilToInt(avg);
        Color c;
        switch (avgLv) {
            case 1:
                c = Color.white; 
                break;
            case 2: 
                c = Color.cyan; 
                break;
            case 3: 
                c = Color.green; 
                break;
            case 4: 
                c = Color.yellow; 
                break;
            default: 
                c = Color.red; 
                break;
        }
        c.r += 0.3f;
        c.g += 0.3f;
        c.b += 0.3f;
        SetColor(c);
    }

    void Init()	{
        _tFirerate = 0;

        _lvRange = 1;
        _lvFirerate = 1;
        _lvPower = 1;

        UpdateParam();
	}

	void Update () {
        _tFirerate += Time.deltaTime;

		Enemy e = Enemy.parent.Nearest(this);
		if(e == null) {
			return;
		}
        
		float dist = Util.DistanceBetween(this, e);
		if(dist > _range) {
			return;
		}

		float targetAngle = Util.AngleBetween(this, e);
		float dAngle = Mathf.DeltaAngle(Angle, targetAngle);
		Angle += dAngle * 0.2f;
		float dAngle2 = Mathf.DeltaAngle(Angle, targetAngle);
		if(Mathf.Abs(dAngle2) > 16) {
			return;
		}

        if(_tFirerate < _firerate) {
            return;
        }

        Shot.Add(X, Y, Angle, SHOT_SPEED, _power);

        _tFirerate = 0;
	}

    public void Upgrade(eUpgrade type) {
        switch (type) {
            case eUpgrade.Range:
                _lvRange++;
                break;

            case eUpgrade.Firerate:
                _lvFirerate++;
                break;

            case eUpgrade.Power:
                _lvPower++;
                break;
        }
        
        UpdateParam();

        Particle p = Particle.Add(Particle.eType.Ellipse, 20, X, Y, 0, 0);
        if (p) {
            p.SetColor(0.2f, 0.2f, 1);
        }
    }

}
