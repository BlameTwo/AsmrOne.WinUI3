using AsmrOne.WinUI3.Models.AsmrOne;

namespace AsmrOne.WinUI3.Models;

public class QueryWorkOrderWrapper
{
    public WorkOrder WordOrder { get; }

    public string DisplayName { get; }

    public QueryWorkOrderWrapper(WorkOrder wordOrder, string displayName)
    {
        DisplayName = displayName;
        WordOrder = wordOrder;
    }
}
