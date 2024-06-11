using System;
using System.Collections.Generic;
using System.Data;
namespace OnlineBusHos185_GJYB.BLL
{
    //hos_opter
    public partial class hos_opter
    {

        private readonly DAL.hos_opter dal = new DAL.hos_opter();
        public hos_opter()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string HOS_ID, string opter_no)
        {
            return dal.Exists(HOS_ID, opter_no);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(Model.hos_opter model)
        {
            dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.hos_opter model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string HOS_ID, string opter_no)
        {

            return dal.Delete(HOS_ID, opter_no);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.hos_opter GetModel(string HOS_ID, string opter_no,string ybtype)
        {

            return dal.GetModel(HOS_ID, opter_no, ybtype);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.hos_opter> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.hos_opter> DataTableToList(DataTable dt)
        {
            List<Model.hos_opter> modelList = new List<Model.hos_opter>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.hos_opter model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.hos_opter();
                    model.HOS_ID = dt.Rows[n]["HOS_ID"].ToString();
                    if (dt.Rows[n]["opter_type"].ToString() != "")
                    {
                        model.opter_type = int.Parse(dt.Rows[n]["opter_type"].ToString());
                    }
                    model.opter_no = dt.Rows[n]["opter_no"].ToString();
                    model.opter_name = dt.Rows[n]["opter_name"].ToString();
                    model.sign_no = dt.Rows[n]["sign_no"].ToString();
                    model.sign_date = dt.Rows[n]["sign_date"].ToString();


                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }
        #endregion

    }
}