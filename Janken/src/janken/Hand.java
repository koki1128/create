/*
 * Hand.java
 */
package janken;

/**
 * ルールとして実装したinterfaceです
 * @author 金井 航輝
 * @version 1.0.0
 */
public interface Hand {
  /**
   * 自身と引数のじゃんけん結果を返すメソッドです
   * @param me プレイヤーの「手」
   * @return 戦った結果
   */
    String fight(int me);
}
