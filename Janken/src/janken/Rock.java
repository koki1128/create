/*
 * Rock.java
 */
package janken;

/**
 * グーの処理を行うクラスです
 * @author 金井　航輝
 * @version 1.0.0
 */
public final class Rock implements Hand{
    /**
     * 自身と引数のじゃんけん結果を返すメソッドです
     * @param me プレイヤーの「手」
     * @return 戦った結果
     */
    public String fight(int me) {
        switch(HandType.valueOf(me)){
            case ROCK:
                return "draw";
            case SCIS:
                return "lose";
            case PAPE:
                return "win";
            default:
                return null;
        }
    }

}
