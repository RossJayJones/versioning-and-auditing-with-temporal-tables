using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class Customer
    {
        private CustomerAudit _currentAudit;

        private readonly List<Address> _addresses;
        private readonly List<CustomerAudit> _audits;
        private readonly List<CustomerVersion> _versions;

        internal Customer()
        {
            _addresses = new List<Address>();
            _audits = new List<CustomerAudit>();
            _versions = new List<CustomerVersion>();
        }

        public Customer(string name) : this()
        {
            Name = name;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public IReadOnlyCollection<Address> Addresses => _addresses;

        public IReadOnlyCollection<CustomerAudit> Audits => _audits;

        public IReadOnlyCollection<CustomerVersion> Versions => _versions;
        
        public void Update(string name)
        {
            Name = name;
            OnChanged("Name changed");
        }

        public Address AddAddress(
            string line,
            string suburb,
            string city,
            string province,
            string code)
        {
            var address = new Address(
                customer: this,
                line: line,
                suburb: suburb,
                city: city,
                province: province,
                code: code);
            _addresses.Add(address);
            OnChanged("Address added");
            return address;
        }

        public void UpdateAddress(int addressId,
            string line,
            string suburb,
            string city,
            string province,
            string code)
        {
            var address = _addresses.Single(a => a.Id == addressId);
            address.Update(
                line: line,
                suburb: suburb,
                city: city,
                province: province,
                code: code);
            OnChanged($"Address {addressId} updated");
        }

        public void IncrementVersion(string message)
        {
            var version = new CustomerVersion(
                message: message);
            _versions.Add(version);
        }

        private void OnChanged(string message)
        {
            if (_currentAudit == null)
            {
                _currentAudit = new CustomerAudit();
                _audits.Add(_currentAudit);
            }

            _currentAudit.AddMessage(message);
        }
    }
}