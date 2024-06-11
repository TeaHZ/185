using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using CommonModel;
using Newtonsoft.Json;
using OnlineBusHos9_OutHos.HISModels;

namespace OnlineBusHos9_OutHos.BUS
{

    internal class GETSELFBILLINGLIST
    {
        public static string B_GETSELFBILLINGLIST(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            Model.GETSELFBILLINGLIST_M.GETSELFBILLINGLIST_IN _in = JsonConvert.DeserializeObject<Model.GETSELFBILLINGLIST_M.GETSELFBILLINGLIST_IN>(json_in);
            Model.GETSELFBILLINGLIST_M.GETSELFBILLINGLIST_OUT _out = new Model.GETSELFBILLINGLIST_M.GETSELFBILLINGLIST_OUT();

            HISModels.T6001.Input input = new HISModels.T6001.Input()
            {
                operatorId = _in.LTERMINAL_SN,
                clinicItemType = _in.ITEM_TYPE == "" ? "4" : _in.ITEM_TYPE,
            };

            PushServiceResult<List<T6001.Outdata>> result = HerenHelper<List<T6001.Outdata>>.pushService("6001-QHZZJ", JsonConvert.SerializeObject(input));
            if (result.code != 1)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = result.msg;

                return JsonConvert.SerializeObject(dataReturn);
            }
            _out.ITEMLIST = new List<Model.GETSELFBILLINGLIST_M.ITEMLISTItem>();

            Dictionary<string, string> itemdic = new Dictionary<string, string>
                {
                    { "C", "检验" },
                    { "D", "检查" },
                    { "E", "治疗" },
                    
                };

            foreach (var item in result.data)
            {
                Model.GETSELFBILLINGLIST_M.ITEMLISTItem iTEMLIST = new Model.GETSELFBILLINGLIST_M.ITEMLISTItem();

                iTEMLIST.ITEM_CODE = item.itemCode;
                iTEMLIST.ITEM_NAME = item.clinicItemName;
                iTEMLIST.PRICE = item.price;
                iTEMLIST.ITEM_TYPE =item.itemClass ;
                iTEMLIST.ITEM_TYPE_NAME = itemdic[item.itemClass];

                _out.ITEMLIST.Add(iTEMLIST);

            }

            dataReturn.Code = 0;
            dataReturn.Msg = "success";
            dataReturn.Param = JsonConvert.SerializeObject(_out);
            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }

    }
}