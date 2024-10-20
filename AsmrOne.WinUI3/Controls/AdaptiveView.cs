using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsmrOne.WinUI3.Common.Bases;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace AsmrOne.WinUI3.Controls
{
    /// <summary>
    /// 弹性布局列表控件，不包含选中事件和选中项目
    /// </summary>
    [TemplatePart(Name = "PART_ItemsRepeater", Type = typeof(ItemsRepeater))]
    public partial class AdaptiveView : ItemControlBase
    {
        public AdaptiveView()
        {
            this.DefaultStyleKey = typeof(AdaptiveView);
        }

        #region 依赖属性


        public object Header
        {
            get { return (object)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            "Header",
            typeof(object),
            typeof(AdaptiveView),
            new PropertyMetadata(default)
        );

        public ItemCollectionTransitionProvider ItemTransitionProvider
        {
            get
            {
                return (ItemCollectionTransitionProvider)GetValue(ItemTransitionProviderProperty);
            }
            set { SetValue(ItemTransitionProviderProperty, value); }
        }

        public static readonly DependencyProperty ItemTransitionProviderProperty =
            DependencyProperty.Register(
                "ItemTransitionProvider",
                typeof(ItemCollectionTransitionProvider),
                typeof(AdaptiveView),
                new PropertyMetadata(null)
            );

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register(
                "ItemTemplate",
                typeof(DataTemplate),
                typeof(AdaptiveView),
                new PropertyMetadata(null)
            );

        public VirtualizingLayout Layout
        {
            get { return (VirtualizingLayout)GetValue(LayoutProperty); }
            set { SetValue(LayoutProperty, value); }
        }

        public static readonly DependencyProperty LayoutProperty = DependencyProperty.Register(
            "Layout",
            typeof(VirtualizingLayout),
            typeof(AdaptiveView),
            new PropertyMetadata(null)
        );
        #endregion


        public override void ChangedViewer() { }

        public virtual void ChangedItemsRepeater() { }
    }
}
