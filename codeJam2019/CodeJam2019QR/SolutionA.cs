using System;
using System.Text;

public class Solution {
    public static void Main() {
        var t = int.Parse(Console.ReadLine());
        
        var k = 1;
        while (k <= t) {
            var a = Console.ReadLine();
            var aBuilder = new StringBuilder(a.Length);
            var bBuilder = new StringBuilder();
            
            foreach(var ch in a) {
                if (ch == '4') {
                    aBuilder.Append('2');
                    bBuilder.Append('2');
                }
                else {
                    aBuilder.Append(ch);
                    if(bBuilder.Length > 0) {
                        bBuilder.Append('0');
                    }
                }
            }
        
            Console.WriteLine($"Case #{k}: {aBuilder} {bBuilder}");
            k++;
        }
    }
}