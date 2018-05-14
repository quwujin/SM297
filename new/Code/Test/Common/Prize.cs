using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class Prize
    {
        /// <summary>
        /// 一等奖中奖率属性
        /// </summary>
        public int zjl1 = 0;

        /// <summary>
        /// 二等奖中奖率属性
        /// </summary>
        public int zjl2 = 0;

        /// <summary>
        /// 三等奖率属性
        /// </summary>
        public int zjl3 = 0;
        /// <summary>
        /// 四等奖率属性
        /// </summary>
        public int zjl4 = 0;
        public int zjl5 = 0;
        public int zjl6 = 0;
        public int zjl7 = 0;


        //模拟的奖池
        public IList<PrizeModel> prize_arr
        {
            get
            {
                return new List<PrizeModel>{
                    new PrizeModel{pid = 1, min =new int[]{40},max=new int[]{60},odds = zjl1,prize = "一等奖"},
                    new PrizeModel{pid = 2, min =new int[]{0},max=new int[]{20},odds = zjl2,prize = "二等奖"},//二等奖
                    new PrizeModel{pid = 3, min =new int[]{80},max=new int[]{105},odds = zjl3,prize = "三等奖"},//三等奖
                    new PrizeModel{pid = 4, min =new int[]{182},max=new int[]{208},odds = zjl4,prize = "四等奖"},//四等奖
                    new PrizeModel{pid = 5, min =new int[]{80},max=new int[]{105},odds = zjl5,prize = "五等奖"},//五等奖
                    new PrizeModel{pid = 6, min =new int[]{62},max=new int[]{88},odds = zjl6,prize = "六等奖"},//六等奖
                    new PrizeModel{pid = 7, min =new int[]{23,203},max=new int[]{66,246},odds = zjl7,prize = "参与奖"},
                };
            }
        }

        //根据奖品数量/中奖几率随机获取中奖项id
        public int getRand(List<int> proArr)
        {
            int result = 0;
            var proSum = proArr.Sum();
            
            for (var i = 0; i < proArr.Count; i++)
            {
                Random ranDom = new Random(Guid.NewGuid().GetHashCode());
                var ranNum = ranDom.Next(1, proSum + 1);
                if (ranNum <= proArr[i])
                {
                    result = i + 1;
		            break;
                }
                else
                {
                    proSum -= proArr[i];
                }
            }
            return result;
        }
        public Prize(int z1, int z2, int z3, int z4, int z5, int z6, int z7)
        {
            zjl1 = z1;
            zjl2 = z2;
            zjl3 = z3;
            zjl4 = z4;
            zjl5 = z5;
            zjl6 = z6;
            zjl7 = z7;
        }
        public string[] GetPrize()
        {
            var proArr = new List<int>();//几率的集合
            foreach (var i in prize_arr)
            {
                proArr.Add(i.odds);
            }
            var rid = getRand(proArr);//中奖id
            
            var res = prize_arr[rid - 1];//中奖项
            var min = res.min;
            var max = res.max;
            var angle = 0;
            var prize = string.Empty;
            var ranDom = new Random();
            //如果此奖项在转盘上有多个格子
            if (min.Length > 1 && max.Length > 1)
            {
                var i = ranDom.Next(0, max.Length);
                angle = ranDom.Next(min[i], max[i]);
            }
            else
            {
                angle = ranDom.Next(min[0], max[0]);
            }
            prize = res.prize;
             
            return new string[] { prize, angle.ToString() };
        }

        public class PrizeModel
        {
            public int pid { get; set; }//奖项编号id
            public int[] min { get; set; }//最小转动角度
            public int[] max { get; set; }//最大转动角度
            public string prize { get; set; }//奖项名称
            public int odds { get; set; }//概率
        }
         
    }
}
