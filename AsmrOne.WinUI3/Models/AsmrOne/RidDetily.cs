using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AsmrOne.WinUI3.Models.AsmrOne
{
    public enum WorkOrder : uint
    {
        /// <summary>
        /// 发售日期倒序
        /// </summary>
        ReleaseDesc,

        /// <summary>
        /// 最新收录
        /// </summary>
        CreateNew,

        /// <summary>
        /// 我的评价倒序
        /// </summary>
        MyCommitDesc,

        /// <summary>
        /// 发售日期顺序
        /// </summary>
        ReleaseAsc,

        /// <summary>
        /// 销量倒序
        /// </summary>
        DL_CountDesc,

        /// <summary>
        /// 价格顺序
        /// </summary>
        PriceAsc,

        /// <summary>
        /// 价格倒序
        /// </summary>
        PriceDesc,

        /// <summary>
        /// 评价倒序
        /// </summary>
        Rate_average_2dpDesc,

        /// <summary>
        /// 评论数量倒序
        /// </summary>
        Review_countDesc,

        /// <summary>
        /// RJ号倒序
        /// </summary>
        ID_Desc,

        /// <summary>
        /// RJ号顺序
        /// </summary>
        ID_Asc,

        /// <summary>
        /// 全年龄顺序
        /// </summary>
        NsfwAsc,

        /// <summary>
        /// 随机排序
        /// </summary>
        RandomDesc,
    }

    //https://as.131433.xyz/
    public static class WorkOrderExtensions
    {
        public static Dictionary<string, object> GetValue(this WorkOrder workOrder)
        {
            string order = null;
            string value = null;
            switch (workOrder)
            {
                case WorkOrder.ReleaseDesc:
                    order = "release";
                    value = "desc";
                    break;
                case WorkOrder.CreateNew:
                    order = "create_date";
                    value = "desc";
                    break;
                case WorkOrder.MyCommitDesc:
                    order = "rating";
                    value = "desc";
                    break;
                case WorkOrder.ReleaseAsc:
                    order = "release";
                    value = "asc";
                    break;
                case WorkOrder.DL_CountDesc:
                    order = "dl_count";
                    value = "desc";
                    break;
                case WorkOrder.PriceAsc:
                    order = "price";
                    value = "asc";
                    break;
                case WorkOrder.PriceDesc:
                    order = "price";
                    value = "desc";
                    break;
                case WorkOrder.Rate_average_2dpDesc:

                    order = "rate_average_2dp";
                    value = "desc";
                    break;
                case WorkOrder.Review_countDesc:

                    order = "review_count";
                    value = "desc";
                    break;
                case WorkOrder.ID_Desc:
                    order = "id";
                    value = "desc";
                    break;
                case WorkOrder.ID_Asc:
                    order = "id";
                    value = "asc";
                    break;
                case WorkOrder.NsfwAsc:
                    order = "nsfw";
                    value = "asc";
                    break;
                case WorkOrder.RandomDesc:
                    order = "random";
                    value = "desc";
                    break;
                default:
                    break;
            }
            return new Dictionary<string, object>() { { "order", order }, { "sort", value } };
        }

        public static ObservableCollection<QueryWorkOrderWrapper> GetWorkOrders()
        {
            return new ObservableCollection<QueryWorkOrderWrapper>()
            {
                new(WorkOrder.ReleaseDesc, "发售日期倒序"),
                new(WorkOrder.CreateNew, "最新收录"),
                new(WorkOrder.MyCommitDesc, "我的评价倒序"),
                new(WorkOrder.ReleaseAsc, "发售日期升序"),
                new(WorkOrder.DL_CountDesc, "销量倒叙"),
                new(WorkOrder.PriceAsc, "价格升序"),
                new(WorkOrder.PriceDesc, "价格倒序"),
                new(WorkOrder.Rate_average_2dpDesc, "评价倒序"),
                new(WorkOrder.Review_countDesc, "评论数量倒序"),
                new(WorkOrder.ID_Desc, "Rj号倒序"),
                new(WorkOrder.ID_Desc, "Rj号升序"),
                new(WorkOrder.NsfwAsc, "全年龄排序"),
                new(WorkOrder.RandomDesc, "随机排序"),
            };
        }
    }
}
