using AglTest.Models;

namespace AglTest.Tests.Builders
{
    class PetBuilder
    {
	    private Pet _pet;

	    public  PetBuilder AsADog()
	    {
		    _pet = new Dog();
		    _pet.Type = "Dog";
		    return this;
	    }

	    public PetBuilder AsACat()
	    {
		    _pet = new Cat();
		    _pet.Type = "Cat";
			return this;
	    }

	    public PetBuilder WithName(string name)
	    {
		    _pet.Name = name;
		    return this;
	    }

	    public Pet Build() => _pet;
    }
}
