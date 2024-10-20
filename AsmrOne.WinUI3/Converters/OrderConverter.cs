using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsmrOne.WinUI3.Models.AsmrOne;
using Microsoft.UI.Xaml.Data;

namespace AsmrOne.WinUI3.Converters
{
    public partial class OrderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is WorkOrder order)
            {
                switch (order)
                {
                    case WorkOrder.ReleaseDesc:
                        return "发售日期倒序";
                    case WorkOrder.CreateNew:
                        return "最新收录";
                    case WorkOrder.MyCommitDesc:
                        return "我的评价倒序";
                    case WorkOrder.ReleaseAsc:
                        return "发售日期升序";
                    case WorkOrder.DL_CountDesc:
                        return "销量倒序";
                    case WorkOrder.PriceAsc:
                        return "价格升序";
                    case WorkOrder.PriceDesc:
                        return "价格倒序";
                    case WorkOrder.Rate_average_2dpDesc:
                        return "评价倒序";
                    case WorkOrder.Review_countDesc:
                        return "评论数量倒序";
                    case WorkOrder.ID_Desc:
                        return "Rj号倒序";
                    case WorkOrder.ID_Asc:
                        return "Rj号升序";
                    case WorkOrder.NsfwAsc:
                        return "全年龄排序";
                    case WorkOrder.RandomDesc:
                        return "随机排序";
                }
            }
            return "无排序";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is string stringValue)
            {
                switch (stringValue)
                {
                    case "发售日期倒序":
                        return WorkOrder.ReleaseDesc;
                    case "最新收录":
                        return WorkOrder.CreateNew;
                    case "我的评价倒序":
                        return WorkOrder.MyCommitDesc;
                    case "发售日期升序":
                        return WorkOrder.ReleaseAsc;
                    case "销量倒序":
                        return WorkOrder.DL_CountDesc;
                    case "价格升序":
                        return WorkOrder.PriceAsc;
                    case "价格倒序":
                        return WorkOrder.PriceDesc;
                    case "评价倒序":
                        return WorkOrder.Rate_average_2dpDesc;
                    case "评论数量倒序":
                        return WorkOrder.Review_countDesc;
                    case "Rj号倒序":
                        return WorkOrder.ID_Desc;
                    case "Rj号升序":
                        return WorkOrder.ID_Asc;
                    case "全年龄排序":
                        return WorkOrder.NsfwAsc;
                    case "随机排序":
                        return WorkOrder.RandomDesc;
                    default:
                        return WorkOrder.CreateNew; // 默认值可以设置为任意合理的枚举值
                }
            }
            return WorkOrder.CreateNew;
        }
    }
}
