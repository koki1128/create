using UnityEngine;
using System.Collections;

public class Global {
    public static void Init() {
        _wave = 1;
        _money = MONEY_INIT;
        _life = LIFE_INIT;
    }

    const int MONEY_INIT = 30;
    static int _money;
    public static int Money {
        get { return _money; }
    }
    
    public static void AddMoney(int v) {
        _money += v;
    }
    
    public static void UseMoney(int v) {
        _money -= v;
        if (_money < 0) {
            _money = 0;
        }
    }

    const int LIFE_INIT = 3;
    public const int LIFE_MAX = 3;
    static int _life;
    public static int Life {
        get { return _life; }
    }
    public static void Damage() {
        _life--;
        if (_life < 0) {
            _life = 0;
        }
    }

    static int _wave = 1;
    public static int Wave {
        get { return _wave; }
    }
    
    public static void NextWave() {
        _wave++;
    }
}
