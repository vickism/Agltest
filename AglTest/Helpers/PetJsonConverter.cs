using System;
using AglTest.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AglTest.Helpers
{
	public class PetJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => objectType == typeof(Pet);

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var jObject = JObject.Load(reader);
			switch (jObject["type"].Value<string>().ToLower())
			{
				case "cat":
					return jObject.ToObject<Cat>(serializer);
				case "dog":
					return jObject.ToObject<Dog>(serializer);
				case "fish":
					return jObject.ToObject<Fish>(serializer);
				default:
					return jObject.ToObject<DefaultPet>(serializer);
			}
		}

		public override bool CanWrite => false;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}