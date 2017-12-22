using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PASS.Models
{
    public class ScoreDistributed
    {
        public int _score0to10 { get; set; }
        public int _score11to20 { get; set; }
        public int _score21to30 { get; set; }
        public int _score31to40 { get; set; }
        public int _score41to50 { get; set; }
        public int _score51to60 { get; set; }
        public int _score61to70 { get; set; }
        public int _score71to80 { get; set; }
        public int _score81to90 { get; set; }
        public int _score91to100 { get; set; }

        public ScoreDistributed(int score0to10,int score11to20, int score21to30, int score31to40, int score41to50, int score51to60, int score61to70, int score71to80, int score81to90, int score91to100)
        {
            _score0to10 = score0to10;
            _score11to20 = score11to20;
            _score21to30 = score21to30;
            _score31to40 = score31to40;
            _score41to50 = score41to50;
            _score51to60 = score51to60;
            _score61to70 = score61to70;
            _score71to80 = score71to80;
            _score81to90 = score81to90;
            _score91to100 = score91to100;
        }
        public ScoreDistributed()
        {
            _score0to10 = 0;
            _score11to20 = 0;
            _score21to30 = 0;
            _score31to40 = 0;
            _score41to50 = 0;
            _score51to60 = 0;
            _score61to70 = 0;
            _score71to80 = 0;
            _score81to90 = 0;
            _score91to100 = 0;
        }
    }
}