/*
 * Janken.java
 */
package janken;

import java.util.Random;
/**
 *Jankenを実行するクラスです。
 *@author 金井　航輝
 *@version 1.0.0
 */
public final class Janken {
  /** グーを表すHandクラスを宣言 */
    private final Hand r;
  /** チョキを表すHandクラスを宣言 */
    private final Hand s;
  /** パーを表すHandクラスを宣言 */
    private final Hand p;
  /** 敵AIの入力を生成するRandomを0Lをシード値としてインスタンス生成 */
    private static Random rand = new Random(0L);

  /**
   *  コンストラクタ
   *  それぞれの変数をインスタンス生成する
   */
    public Janken(){
        r = new Rock();
        s = new Scis();
        p = new Pape();
    }

  /**
   * inputMyHandメソッド
   * このメソッドはプレイヤーの入力を受け付ける唯一のメソッドです
   * @param x プレイヤーの出す「手」
   * @return じゃんけんの結果を表す文字列
   */
    public final String inputMyHand(int x){
        return result(x);
    }

  /**
   * resultメソッド
   * 使用者からの可視性が届かない場所で処理するメソッドです
   * 数字が対応するHandTypeによってそれぞれのHandオブジェクトのfightに入れています
   * @param me プレイヤーが入力した「手」を引数に持ってくる
   * @return 課題の出力を返す
   */
    private final String result(int me){
        int enemy = rand.nextInt(3);
        String ans;
        switch(HandType.valueOf(enemy)){
            case ROCK:
                ans = r.fight(me);
                break;
            case SCIS:
                ans = s.fight(me);
                break;
            case PAPE:
                ans = p.fight(me);
                break;
            default:
                ans = null;
                break;
        }
        return "your_hand: " + HandType.getName(me) + ", enemy_hand: " + HandType.getName(enemy) + ", judge: " + ans;
    }
}
