using System;
using System.Collections.Generic;
using System.Text;
using AglTest.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AglTest.Tests.Builders
{
    public class OwnerBuilder
    {
	    private Owner _owner;

	    public OwnerBuilder()
	    {
		    _owner = new Owner();
	    }

	    public OwnerBuilder WithGender(GenderEnum gender)
	    {
		    _owner.Gender = gender.ToString();
		    return this;
	    }

	    public OwnerBuilder WithName(string name)
	    {
		    _owner.Name = name;
		    return this;
	    }

	    public OwnerBuilder WithPet(Pet pet)
	    {
		    if (_owner.Pets == null)
			    _owner.Pets = new List<Pet>();
			_owner.Pets.Add(pet);
		    return this;
	    }
	    public OwnerBuilder WithPets(List<Pet> pets)
	    {
		    if (_owner.Pets == null)
			    _owner.Pets = new List<Pet>();
		    _owner.Pets.AddRange(pets);
		    return this;
	    }

	    public Owner Build() => _owner;

	    public string BuildAsJson()
	    {
		    return JsonConvert.SerializeObject(_owner, new JsonSerializerSettings(){ContractResolver = new CamelCasePropertyNamesContractResolver()});
	    }
    }
}
