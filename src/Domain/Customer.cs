using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class Customer
    {
        private Audit _currentAudit;

        private readonly List<Address> _addresses;
        private readonly List<Audit> _audits;
        private readonly List<Version> _versions;

        internal Customer()
        {
            _addresses = new List<Address>();
            _audits = new List<Audit>();
            _versions = new List<Version>();
        }

        public Customer(string name) : this()
        {
            Name = name;
            OnChanged("Customer created");
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public IReadOnlyCollection<Address> Addresses => _addresses;

        public IReadOnlyCollection<Audit> Audits => _audits;

        public IReadOnlyCollection<Version> Versions => _versions;
        
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
            var version = new Version(
                message: message);
            _versions.Add(version);
        }

        private void OnChanged(string message)
        {
            if (_currentAudit == null)
            {
                _currentAudit = new Audit();
                _audits.Add(_currentAudit);
            }

            _currentAudit.AddMessage(message);
        }
    }
}