using CommonModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineBusHos9_Common.HISModels;
using OnlineBusHos9_Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBusHos9_Common.BUS
{
    public class GETGOODSLIST
    {
        public static string B_GETGOODSLIST(string json_in)
        {
            GETGOODSLIST_M.In @in = new GETGOODSLIST_M.In();
            @in = JsonConvert.DeserializeObject<GETGOODSLIST_M.In>(json_in);
            GETGOODSLIST_M.Out @out = new GETGOODSLIST_M.Out();

            DataReturn dataReturn = new DataReturn();
            @out.ITEMLIST = new List<GETGOODSLIST_M.ITEM>();
            List<GETGOODSLIST_M.ITEM> list = new List<GETGOODSLIST_M.ITEM>();
            string pinyin = string.IsNullOrEmpty(@in.PINYINCODE) ? "" : @in.PINYINCODE;


            /*

            A	西药
            B	中草药
            C	化验
            D	检查
            E	治疗
            F	手术
            G	麻醉
            H	血费
            I	材料
            J	床位
            K	护理
            L	膳食
            M	中成药
            N	诊查
             */
            string ItemClass = "A";

            //4药品 2项目 3材料
            switch (@in.CATE_CODE)
            {
                case "4":
                    ItemClass = "A,B,M";
                    break;

                case "2":
                    ItemClass = "C,D,F,J,K";
                    break;

                case "3":
                    ItemClass = "I";
                    break;

                default:
                    break;
            }
            List<PRICE001.data> datas = new List<PRICE001.data>();

            try
            {
                if (ItemClass.Contains(','))
                {
                    string[] itemclasses = ItemClass.Split(',');

                    foreach (var item in itemclasses)
                    {
                        QueryServiceResult<List<PRICE001.data>> result = RedisDataHelper.GetPrice001(item);

                        if (result.Head.TradeStatus != "AA")
                        {
                            dataReturn.Code = 66;
                            dataReturn.Msg = result.Head.TradeMessage;
                            return JsonConvert.SerializeObject(dataReturn);
                        }

                        datas.AddRange(result.Body);
                    }
                }
                else
                {
                    JObject jin = new JObject
                {
                    { "ItemClass", ItemClass }
                };
                    QueryServiceResult<List<PRICE001.data>> result = RedisDataHelper.GetPrice001(ItemClass);

                    if (result.Head.TradeStatus != "AA")
                    {
                        dataReturn.Code = 66;
                        dataReturn.Msg = result.Head.TradeMessage;
                        return JsonConvert.SerializeObject(dataReturn);
                    }

                    datas.AddRange(result.Body);
                }
                int COUNT=0 ;

                //@out.ITEMLIST = new List<GETGOODSLIST_M.ITEM>();
                //List<GETGOODSLIST_M.ITEM> list = new List<GETGOODSLIST_M.ITEM>();
                foreach (var item in datas)
                {
                    if (!(item.InputCode.ToUpper().Contains(pinyin.ToUpper())))
                    {
                        continue;
                    }

                    COUNT++;
                    
                    GETGOODSLIST_M.ITEM goods = new GETGOODSLIST_M.ITEM()
                    {
                        ITEM_CODE = item.ItemId,
                        ITEM_NAME = item.ItemName,
                        ITEM_GG = item.ItemSpec,
                        ITEM_PRICE = item.Price +"元",
                        ITEM_UNIT = item.Units,
                        YL_PREC = item.Zlbl,
                        INSU_TYPE = item.YbBllx
                    };
                    @out.TOTAL_NUM = COUNT.ToString();
                    list.Add(goods);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("OnlineBusHos9_Common", "B_GETGOODSLIST", JsonConvert.SerializeObject(ex));
            }

            try
            {
                //PAGE_INDEX: "1",//分页索引(第多少页)
                // PAGE_SIZE: "20"//分页大小(每页都少个)
                int pageInedx = Convert.ToInt32(@in.PAGE_INDEX);
                int pageSize = Convert.ToInt32(@in.PAGE_SIZE);
                int begSize = pageInedx == 1 ? 1 : (pageInedx - 1) * pageSize;
                int endSize = pageInedx == 1 ? list.Count : pageSize;

                list = list.OrderBy(t => t.ITEM_NAME).ToList(); //按照升序排列
                if (list.Count == 0)
                {
                    @out.ITEMLIST = list;
                }
                else
                {
                    if (begSize + endSize > list.Count)
                    {
                        @out.ITEMLIST = list.GetRange(begSize, list.Count - begSize);
                    }
                    else
                    {
                        @out.ITEMLIST = list.GetRange(begSize, endSize);
                    }
                }
               
                #region
                //if (list.Count > begSize+endSize)
                //{
                //    @out.ITEMLIST = list.GetRange(begSize, endSize);
                //}
                //else if (list.Count < begSize+ pageSize)
                //{
                //    @out.ITEMLIST = list.GetRange(begSize, list.Count-begSize);
                //}
                //else
                //{
                //    @out.ITEMLIST = list;
                //}
                #endregion
            }
            catch (Exception ex)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = ex.ToString() ;
                return JsonConvert.SerializeObject(dataReturn);
            }
            
            dataReturn.Code = 0;
            dataReturn.Msg = "success";

            dataReturn.Param = JsonConvert.SerializeObject(@out);
            return JsonConvert.SerializeObject(dataReturn);
        }
    }
}