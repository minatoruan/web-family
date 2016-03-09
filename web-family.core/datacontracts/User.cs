namespace web_family.core.datacontracts
{
    public abstract class EntityBase 
    {
        //[BsonElement("id")]
        public string Id { get; set; }
    }

    public class User : EntityBase
    {
        //[BsonElement("fullname")]
        public string Fullname { get; set; }

        //[BsonElement("username")]
        public string Username { get; set; }

        //[BsonElement("password")]
        public string Password { get; set; }
    }
}