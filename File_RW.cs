//バイナリエディタの読み書き用
using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;

public class File_RW : MonoBehaviour {
    static List<Byte> data = new List<byte>();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public static long Load_HighScore() {
        return load_Number(0,4);
    }
    static long load_Number(int first,int range) {
        string str = "";
        //バイナリ数値が逆だから
        for (int i = first + range - 1; i > first - 1; i--) {
            if (data[i]<10) {
                str+= "0";
            }
            str += Convert.ToString(data[i],16);
        }
        return Convert.ToInt64(str, 16);
    }
    public static void Write_HighScore(long score)
    {
        Write_Number(0,4,score);
    }

    static void Write_Number(int first, int range, long num)
    {
        string str = Convert.ToString(num, 16);

        if (str.Length % 2 == 1)
        {
            str = "0" + str;
        }
        //入れるバイナリデータが少なければ00を入れるため、
        for (int i = str.Length; i < range * 2; i++)
        {
            str = "0" + str;
        }
        //バイナリにするさい半分になるとき
        for (int i = 1; i <= (int)(str.Length / 2); i++) {
            string s = str.Substring(str.Length - 2 * i, 2);
            data[first + i - 1] = Convert.ToByte(s, 16);
        }
    }
    //ファイルの読み込み
    public static void Read_File(string editer) {
        FileStream f = new FileStream(editer, FileMode.Open, FileAccess.Read);
        BinaryReader reader = new BinaryReader(f);

        for (int i = 0; i < f.Length; i++)
        {
            data.Add(reader.ReadByte());
        }
        reader.Close();
    }
    //ファイルの書き込み
    public static void Write_File() {
        FileStream f = new FileStream("Data_t/test.mikanneet", FileMode.Create, FileAccess.Write);
        BinaryWriter writer = new BinaryWriter(f);
        byte[] b = new byte[data.Count];
        for (int i = 0; i < data.Count; i++) {
            b[i] = data[i];
        }
        writer.Write(b,0,b.Length);//数値
        writer.Close();
    }
}
