using System.Collections.Generic;
using System.Linq;

namespace Bank.Domain.Validators
{
    public abstract class EntityValidator
    {
        private List<string> _messageValidator { get; set; }

        private List<string> _message
        {
            get
            {
                return (_messageValidator ?? (_messageValidator = new List<string>()));
            }
        }

        public bool IsValid
        {
            get
            {
                return (!_message.Any());
            }
        }

        public abstract void Validate();

        protected void ClearValidateMessage()
        {
            _message.Clear();
        }

        protected void AddValidateMessage(string message)
        {
            _message.Add(message);
        }

        public string GetValidateMessage()
        {
            return string.Join("\n", _message);
        }
    }
}
