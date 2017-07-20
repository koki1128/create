using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMgr : MonoBehaviour {
    const float TIMER_WAIT = 2.0f;
    float _tWait = TIMER_WAIT;

    enum eState {
        Wait,
        Main,
        Gameover,
    }
    eState _state = eState.Wait;

    public enum eSelMode {
        None,
        Buy,
        Upgrade,
    }
    eSelMode _selMode = eSelMode.None;

    GameObject _selObj = null;
    Tower _selTower = null;

	int _tAppear = 0;
	List<Vec2D> _path;
    Cursor _cursor;
    Layer2D _lCollision;
    Gui _gui;
    EnemyGenerator _enemyGenerator;
    WaveStart _waveStart;
    CursorRange _cursorRange;

	void Start () {
        Global.Init();

		Enemy.parent = new TokenMgr<Enemy>("Enemy", 128);
        Shot.parent = new TokenMgr<Shot>("Shot", 128);
        Particle.parent = new TokenMgr<Particle>("Particle", 256);
        Tower.parent = new TokenMgr<Tower>("Tower", 64);

		GameObject prefab = null;
		prefab = Util.GetPrefab(prefab, "Field");
		Field field = Field.CreateInstance2<Field>(prefab, 0, 0);
		field.Load();
		_path = field.Path;
        _lCollision = field.lCollision;
        _cursor = GameObject.Find("Cursor").GetComponent<Cursor>();
        _gui = new Gui();
        _enemyGenerator = new EnemyGenerator(_path);
        _waveStart = MyCanvas.Find<WaveStart>("TextWaveStart");
        _cursorRange = GameObject.Find("CursorRange").GetComponent<CursorRange>();
        ChangeSelMode(eSelMode.None);
	}

	void Update() {
        _gui.Update(_selMode, _selTower);
        _cursor.Proc(_lCollision);

        switch (_state) {
            case eState.Wait:
                _tWait -= Time.deltaTime;
                if(_tWait < 0) {
                    _enemyGenerator.Start(Global.Wave);
                    _waveStart.Begin(Global.Wave);
                    _state = eState.Main;
                }
                break;

            case eState.Main:
                UpdateMain();

                if (Global.Life <= 0) {
                    _state = eState.Gameover;
                    MyCanvas.SetActive("TextGameover", true);
                    break;
                }

                if (IsWaveClear()) {
                    Global.NextWave();
                    _tWait = TIMER_WAIT;
                    _state = eState.Wait;
                }
                break;

            case eState.Gameover:
                if (Input.GetMouseButton(0)) {
                    Application.LoadLevel("Title");
                }

                break;
        }
	}

    void UpdateMain() {
        _enemyGenerator.Update();

        if (false == _cursor.Placeable) {
            return;
        }

        int mask = 1 << LayerMask.NameToLayer("Tower");
        Collider2D col = Physics2D.OverlapPoint(_cursor.GetPosition(), mask);
        _selObj = null;
        if (col != null) {
            _selObj = col.gameObject;
        }

        if (false == Input.GetMouseButtonDown(0)) {
            return;
        }

        if (_selObj != null) {
            _selTower = _selObj.GetComponent<Tower>();

            ChangeSelMode(eSelMode.Upgrade);
        }

        switch (_selMode) {
            case eSelMode.Buy:
                if (null == _cursor.SelObj) {
                    int cost = Cost.TowerProduction();
                    Global.UseMoney(cost);
                    Tower.Add(_cursor.X, _cursor.Y);
                    int cost2 = Cost.TowerProduction();
                    if (Global.Money < cost2) {
                        ChangeSelMode(eSelMode.None);
                    }
                }
                break;
        }
    }

    bool IsWaveClear() {
        if (_enemyGenerator.Number > 0) {
            return false;
        }

        if (Enemy.parent.Count() > 0) {
            return false;
        }

        return true;
    }

    public void OnClickBuy() {
        ChangeSelMode(eSelMode.Buy);
    }

    public void OnClickRange() {
        ExecUpgrade(Tower.eUpgrade.Range);
    }

    public void OnClickFirerate() {
        ExecUpgrade(Tower.eUpgrade.Firerate);
    }

    public void OnClickPower() {
        ExecUpgrade(Tower.eUpgrade.Power);
    }

    void ChangeSelMode(eSelMode mode) {
        switch (mode) {
            case eSelMode.None:
                MyCanvas.SetActive("ButtonBuy", true);
                MyCanvas.SetActive("TextTowerInfo", false);
                _cursorRange.SetVisible(false, 0);
                SetActiveUpgrade(false);
                break;

            case eSelMode.Buy:
                MyCanvas.SetActive("ButtonBuy", false);
                MyCanvas.SetActive("TextTowerInfo", false);
                _cursorRange.SetVisible(false, 0);
                SetActiveUpgrade(false);
                break;

            case eSelMode.Upgrade:
                MyCanvas.SetActive("ButtonBuy", true);
                MyCanvas.SetActive("TextTowerInfo", true);
                _cursorRange.SetVisible(true, _selTower.LvRange);
                _cursorRange.SetPosition(_cursor);
                SetActiveUpgrade(true);
                break;
        }
        _selMode = mode;
    }

    void SetActiveUpgrade(bool b) {
        MyCanvas.SetActive("ButtonRange", b);
        MyCanvas.SetActive("ButtonFirerate", b);
        MyCanvas.SetActive("ButtonPower", b);
    }

    void ExecUpgrade(Tower.eUpgrade type) {
        int cost = _selTower.GetCost(type);
        
        Global.UseMoney(cost);
        _selTower.Upgrade(type);

        _cursorRange.SetVisible(true, _selTower.LvRange);
    }
}
