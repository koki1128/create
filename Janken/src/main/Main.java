/*
 * Main.java
 */
package main;

import janken.*;
import java.util.Scanner;

/**
 * oop的なじゃんけんゲーム
 * @author 金井 航輝
 * @version 1.0.0
 */
public class Main {

    /**
     * mainメソッド。
     * @param args コマンドライン引数
     */
    public static void main(String[] args) {
        Janken j = new Janken();
        Scanner scan = new Scanner(System.in);

  		while(scan.hasNext())　{
            if(!scan.hasNextInt())　{
                System.out.println("input error");
                System.exit(1);
            }
            System.out.println(j.inputMyHand(scan.nextInt()));
        }
    }
}
