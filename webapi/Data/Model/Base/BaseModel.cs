namespace webapi.Data.Model.Base
{
    public class BaseModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
