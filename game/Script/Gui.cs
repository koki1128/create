using UnityEngine;
using System.Collections;

public class Gui {
    TextObj _txtWave;
    TextObj _txtMoney;
    TextObj _txtCost;
    ButtonObj _btnBuy;
    TextObj _txtTowerInfo;
    ButtonObj _btnRange;
    ButtonObj _btnFirerate;
    ButtonObj _btnPower;

    public Gui() {
        _txtWave = MyCanvas.Find<TextObj>("TextWave");
        _txtMoney = MyCanvas.Find<TextObj>("TextMoney");
        _txtCost = MyCanvas.Find<TextObj>("TextCost");
        _txtCost.Label = "";
        _btnBuy = MyCanvas.Find<ButtonObj>("ButtonBuy");
        _txtTowerInfo = MyCanvas.Find<TextObj>("TextTowerInfo");
        _btnRange = MyCanvas.Find<ButtonObj>("ButtonRange");
        _btnFirerate = MyCanvas.Find<ButtonObj>("ButtonFirerate");
        _btnPower = MyCanvas.Find<ButtonObj>("ButtonPower");
    }

    public void Update(GameMgr.eSelMode selMode, Tower tower) {
        _txtWave.SetLabelFormat("Wave: {0}", Global.Wave);
        _txtMoney.SetLabelFormat("Money: ${0}", Global.Money);
        int cost = Cost.TowerProduction();
        _txtCost.Label = "";
        if (GameMgr.eSelMode.Buy == selMode) {
            _txtCost.SetLabelFormat("(cost ${0})", cost);
        }
        _btnBuy.Enabled = (Global.Money >= cost);
        _btnBuy.FormatLabel("Buy (${0})", cost);
        for (int i = 0; i < Global.LIFE_MAX; i++) {
            bool b = (Global.Life > i);
            MyCanvas.SetActive("ImageLife" + i, b);
        }

        if (GameMgr.eSelMode.Upgrade == selMode) {
            _txtTowerInfo.SetLabelFormat(
                "<<Tower Info>>\n    Range: Lv{0}\n    Firerate: Lv{1}\n    Power: Lv{2}",
                tower.LvRange,
                tower.LvFirerate,
                tower.LvPower
            );

            int money = Global.Money;
            _btnRange.Enabled = (money >= tower.CostRange);
            _btnRange.FormatLabel("Range (${0})", tower.CostRange);
            _btnFirerate.Enabled = (money >= tower.CostFirerate);
            _btnFirerate.FormatLabel("Firerate (${0})", tower.CostFirerate);
            _btnPower.Enabled = (money >= tower.CostPower);
            _btnPower.FormatLabel("Power (${0})", tower.CostPower);
        }
    }
}
