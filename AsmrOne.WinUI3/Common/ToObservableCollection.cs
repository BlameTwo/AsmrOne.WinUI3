using AsmrOne.WinUI3.Models.AsmrOne;
using AsmrOne.WinUI3.Models.ItemWrapper;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AsmrOne.WinUI3.Common
{
    public static class ToObservableCollection
    {
        public static ObservableCollection<T> ToObservable<T>(this IEnumerable<T> value) =>
            new ObservableCollection<T>(value);


        public static List<SearchTagWrapper> ToTags(this IEnumerable<TagList> tag)
        {
            IList<SearchTagWrapper> searchWrapper = new List<SearchTagWrapper>();
            foreach (var item in tag)
            {
                searchWrapper.Add(new SearchTagWrapper()
                {
                     DisplayName = $"Tag:{item.Name}({item.Count})",
                     Name = item.Name,
                     Type = "tag"
                });
            }
            return (List<SearchTagWrapper>)searchWrapper;
        }

        public static List<SearchTagWrapper> ToCircles(this IEnumerable<TagList> circles)
        {
            IList<SearchTagWrapper> searchWrapper = new List<SearchTagWrapper>();
            foreach (var item in circles)
            {
                searchWrapper.Add(new SearchTagWrapper()
                {
                    DisplayName = $"Circle:{item.Name}({item.Count})",
                    Name = item.Name,
                    Type = "circle"
                });
            }
            return (List<SearchTagWrapper>)searchWrapper;
        }

        public static List<SearchTagWrapper> ToVas(this IEnumerable<VasList> circles)
        {
            IList<SearchTagWrapper> searchWrapper = new List<SearchTagWrapper>();
            foreach (var item in circles)
            {
                searchWrapper.Add(new SearchTagWrapper()
                {
                    DisplayName = $"Vas:{item.Name}({item.Count})",
                    Name = item.Name,
                    Type = "va"
                });
            }
            return (List<SearchTagWrapper>)searchWrapper;
        }
    }
}
