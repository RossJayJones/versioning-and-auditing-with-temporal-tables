namespace Domain
{
    public class Address
    {
        internal Address()
        {
            
        }

        public Address(Customer customer,
            string line,
            string suburb,
            string city,
            string province,
            string code) : this()
        {
            Customer = customer;
            Line = line;
            Suburb = suburb;
            City = city;
            Province = province;
            Code = code;
        }

        public int Id { get; private set; }

        public Customer Customer { get; private set; }

        public string Line { get; private set; }

        public string Suburb { get; private set; }

        public string City { get; private set; }

        public string Province { get; private set; }

        public string Code { get; private set; }

        internal void Update(
            string line,
            string suburb,
            string city,
            string province,
            string code)
        {
            Line = line;
            Suburb = suburb;
            City = city;
            Province = province;
            Code = code;
        }
    }
}
