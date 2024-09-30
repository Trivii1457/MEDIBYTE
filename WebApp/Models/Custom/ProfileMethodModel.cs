namespace Blazor.WebApp.Models
{

    public class ProfileMethodModel
    {

        public long ProfileNavigationId { get; set; }
        public long ProfileId { get; set; }
        public long MenuId { get; set; }
        public long MenuActionId { get; set; }
        public string Module { get; set; }
        public string ResourceKeyMaster { get; set; }
        public string ResourceKeyMethod { get; set; }
        public string MenuName { get; set; }
        public string MenuActionName { get; set; }
        public bool HavePermision { get; set; } = false;
    }

}
