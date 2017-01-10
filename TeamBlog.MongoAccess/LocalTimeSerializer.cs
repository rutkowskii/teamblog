using System;
using MongoDB.Bson.Serialization;

namespace TeamBlog.MongoAccess
{
    public class LocalTimeSerializer : IBsonSerializer
    {
        public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var datetime = context.Reader.ReadDateTime();
            var res = new DateTime(datetime);
            return res;
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            context.Writer.WriteDateTime(((DateTime)value).Ticks);
        }

        public Type ValueType => typeof(DateTime);
    }
}