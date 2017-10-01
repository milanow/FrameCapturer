using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameCapturer
{
    class Program
    {
        static void Main(string[] args)
        {
            FrameManager fm = new FrameManager(5);

            Console.WriteLine($"Max length is: {fm.TargetLength*4/3}, Min length is: {fm.TargetLength*2/3}");
            Frame[] input= new Frame[9];
            for(int i = 1; i <= input.Length; ++i)
            {
                input[i-1] = new Frame { frameId = i, frameResource = "Frame" + i };
                fm.AddFrame(input[i-1]);

                if(i == 6 || i == 7 || i == 9)
                {
                    IEnumerable<Frame> res = fm.GetFrame();
                    foreach(var f in res)
                    {
                        Console.Write(f.frameId + ", ");
                    }
                    Console.WriteLine("\n" + "----------------------------------------------------------------------------");
                }
            }
        }
    }
}
