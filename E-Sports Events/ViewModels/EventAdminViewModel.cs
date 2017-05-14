namespace ViewModels
{
    public class EventAdminViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public override string ToString()
        {
            return this.UserName;
        }
    }
}
