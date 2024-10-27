using CommunityToolkit.Mvvm.Messaging.Messages;

namespace C971_Grant_Putnam.Models
{
    public class UpdateTermMessage : ValueChangedMessage<Term>
    {
        public UpdateTermMessage(Term term) : base(term) {  }
    }
}
