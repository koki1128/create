/*
 * HandType.java
 */
package janken;

/**
 * 「手を種類」を管理する列挙型です
 * @author 金井　航輝
 * @version 1.0.0
 */
public enum HandType{
    /** グーを表すROCK */
    ROCK(0),
    /** チョキを表すSCIS */
    SCIS(1),
    /** パーを表すPAPE */
    PAPE(2);

    /** それぞれの列挙型が保持するint型の情報を宣言 */
    private final int id;

    /**
     * 列挙型のコンストラクタです.引数のidで初期化します.
     * @param id 列挙型が保持するid
     */
    private HandType(int id){
        this.id = id;
    }


    /**
     * 引数のidを保持している列挙型を返します
     * @param id 探している列挙型が保持するid
     * @return 引数のidを保持している列挙型
     */
    public static HandType valueOf(int id){
        for(HandType type : HandType.values()){
            if(type.getID() == id){
                return type;
            }
        }
        throw new RuntimeException();
    }

    /**
     * 列挙型の保持しているidを返すメソッドです
     * @return 自身が保持しているid
     */
    private final int getID(){
        return this.id;
    }

    /**
     * 引数のidを保持している列挙型が表しているものを文字列で返します
     * @param id 探している列挙型が保持するid
     * @return 引数のidを保持している列挙型が表している文字列
     */
    public static String getName(int id){
        for(HandType type : HandType.values()){
            if(type.getID() == id){
                switch(type) {
                    case ROCK:
                        return "rock";
                    case SCIS:
                        return "scissors";
                    case PAPE:
                        return "paper";
                }
            }
        }
        return null;
    }
}
