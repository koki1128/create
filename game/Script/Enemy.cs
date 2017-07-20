using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Token {
    public static TokenMgr<Enemy> parent = null;

    public static Enemy Add(List<Vec2D> path) {
        Enemy e = parent.Add(0, 0);
        if(null == e) {
            return null;
        }
        e.Init(path);
        return e;
    }

	public Sprite spr0;
	public Sprite spr1;
	public Sprite spr2;
	public Sprite spr3;
	public Sprite spr4;
	public Sprite spr5;
	public Sprite spr6;
	public Sprite spr7;

    int _hp;
    int _money;
	int _tAnim = 0;
    float _speed = 0;
    float _tSpeed = 0;
    List<Vec2D> _path;
    int _pathIdx;
    Vec2D _prev;
    Vec2D _next;

    void UpdateAngle() {
        float dx = _next.x - _prev.x;
        float dy = _next.y - _prev.y;
        Angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
    }

    public void Init(List<Vec2D> path) {
        _path = path;
        _pathIdx = 0;
        _speed = EnemyParam.Speed();
        _tSpeed = 0;

        MoveNext();
        _prev.Copy(_next);
        _prev.x -= Field.GetChipSize();
        FixedUpdate();
        _hp = EnemyParam.Hp();
        _money = EnemyParam.Money();
    }

	void FixedUpdate() {
		float dx = _next.x - _prev.x;
		float dy = _next.y - _prev.y;
		float ang = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        const float ALLOW_ERRAR = 0.0000001f;
        
		_tAnim++;
		if(_tAnim%32 < 16) {
			if(System.Math.Abs(ang-90) <= ALLOW_ERRAR) {
				SetSprite(spr0);
			} else if(System.Math.Abs(ang-180) <= ALLOW_ERRAR 
                        || System.Math.Abs(ang+180) <= ALLOW_ERRAR) {
				SetSprite(spr1);
			} else if(System.Math.Abs(ang+90) <= ALLOW_ERRAR) {
				SetSprite(spr2);
			} else {
				SetSprite(spr3);
			}
		} else {
			if(System.Math.Abs(ang-90) <= ALLOW_ERRAR) {
				SetSprite(spr4);
			} else if(System.Math.Abs(ang-180) <= ALLOW_ERRAR 
                        || System.Math.Abs(ang+180) <= ALLOW_ERRAR) {
				SetSprite(spr5);
			} else if(System.Math.Abs(ang+90) <= ALLOW_ERRAR) {
				SetSprite(spr6);
			} else {
				SetSprite(spr7);
			}
		}

        _tSpeed += _speed;
        if(_tSpeed >= 100.0f) {
            _tSpeed -= 100.0f;
            MoveNext();
        }

        X = Mathf.Lerp(_prev.x, _next.x, _tSpeed / 100.0f);
        Y = Mathf.Lerp(_prev.y, _next.y, _tSpeed / 100.0f);
	}

    void MoveNext() {
        if(_pathIdx >= _path.Count) {
            _tSpeed = 100.0f;
            Global.Damage();
            Vanish();

            return;
        }
        _prev.Copy(_next);

        Vec2D v = _path[_pathIdx];
        _next.x = Field.ToWorldX(v.X);
        _next.y = Field.ToWorldY(v.Y);
        _pathIdx++;
    }

    void OnTriggerEnter2D(Collider2D other) {
        string name = LayerMask.LayerToName(other.gameObject.layer);
        if("Shot" == name) {
            Shot s = other.gameObject.GetComponent<Shot>();
            s.Vanish();

            Damage(s.Power);

            if (false == Exists) {
                Global.AddMoney(_money);
            }
        }
    }

    void Damage(int val) {
        _hp -= val;
        if(_hp <= 0) {
            Vanish();
        }
    }

    public override void Vanish() {
        {
            Particle p = Particle.Add(Particle.eType.Ring, 30, X, Y, 0, 0);
            if(p) {
                p.SetColor(0.7f, 1, 0.7f);
            }
        }

        float dir = Random.Range(35, 55);
        for(int i = 0; i < 8; i++) {
            int timer = Random.Range(20, 40);
            float spd = Random.Range(0.5f, 2.5f);
            Particle p = Particle.Add(Particle.eType.Ball, timer, X, Y, dir, spd);
            dir += Random.Range(35, 55);
            if(p) {
                p.SetColor(0.0f, 1, 0.0f);
                p.Scale = 0.8f;
            }
        }

        base.Vanish();
    }
}
